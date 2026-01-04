using CoreCompetencyInterviewGenerator.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Threading;


namespace CoreCompetencyInterviewGenerator.Components.Pages
{
    public partial class InterviewList
    {
        [Inject] public IJSRuntime JS { get; set; }

        string ErrorMessage = string.Empty;
        string SuccessMessage = string.Empty;

        bool _isPicking = false;
        protected override void OnInitialized()
        {
            ViewModel.OnChange += OnViewModelChanged;
        }
        private void AddNewInterview()
        {
            ViewModel.IsAddMode = true;
            ViewModel.IsEditMode = true;
            ViewModel.AddNewInterview();
        }

        private void EditInterview(Interview interview)
        {
            ViewModel.IsAddMode = false;
            ViewModel.IsEditMode = true;
            ViewModel.LoadInterviewById(interview.Id);
            Navigation.NavigateTo($"/interview");
        }

        private async Task DeleteInterviewWithConfirm(Interview interview)
        {
            ViewModel.DeleteInterview(interview.Id);
        }

        private void OnViewModelChanged() => InvokeAsync(StateHasChanged);

        public void Dispose()
        {
            ViewModel.OnChange -= OnViewModelChanged;
        }

        private async Task GenerateInterview(Interview interview)
        {
                using MemoryStream stream = ViewModel.GenerateInterviewDoc(interview.Id);
                byte[] fileBytes = stream.ToArray();
                string base64Data = Convert.ToBase64String(fileBytes);

                // 3. Define the filename
                string fileName = $"{DateTime.Now:yyyy-MM-dd}-Interview.docx";

                // 4. Trigger the download immediately
                await JS.InvokeVoidAsync("downloadFile", fileName, base64Data);
        }

        private void DisplayErrorMessage()
        {
            ErrorMessage = "<p class=\"alert-danger\">The file couldn't be saved. There was an error. Please try again." +
                            "<br/>";
        }

        private void DisplaySuccessMessage(string savedPath)
        {
            SuccessMessage = $"The file was successfully saved to: {savedPath}";
        }
    }
}

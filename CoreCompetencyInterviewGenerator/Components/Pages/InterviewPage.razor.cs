using CoreCompetencyInterviewGenerator.ViewModels;
using Microsoft.AspNetCore.Components;

namespace CoreCompetencyInterviewGenerator.Components.Pages
{
    public partial class InterviewPage
    {
        // ensure the component re-renders when the viewmodel state changes
        protected override void OnInitialized()
        {
            ViewModel.OnChange += OnViewModelChanged;
        }

        private void OnViewModelChanged() => InvokeAsync(StateHasChanged);

        public void Dispose()
        {
            ViewModel.OnChange -= OnViewModelChanged;
        }
    }
}

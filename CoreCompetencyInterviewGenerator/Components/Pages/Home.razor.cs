using InterviewGeneratorBlazorHybrid.Data;

namespace InterviewGeneratorBlazorHybrid.Components.Pages
{
    public partial class Home
    {
        protected override void OnInitialized() {
            // Check if database is still valid/available. If not, redirect to settings page.
            // This is used for when the app first opens, but also when returning from the settings page.
            ViewModel.IsDatabaseAvailable();

            if (!ViewModel.DatabaseIsAvailable)
                Navigation.NavigateTo("/settings");
        }
        private void AddNewInterview()
        {
            ViewModel.AddNewInterview();
            Navigation.NavigateTo($"/interview");
        }
    }
}

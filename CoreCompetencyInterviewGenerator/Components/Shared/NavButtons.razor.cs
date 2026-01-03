using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCompetencyInterviewGenerator.Components.Shared
{
    public partial class NavButtons
    {
        public void GoToInterviewsList()
        {
            ViewModel.ResetForm();
            Navigation.NavigateTo($"/interviews");
        }
    }
}

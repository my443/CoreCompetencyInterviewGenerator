using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewGeneratorBlazorHybrid.Components.Pages
{
    public partial class InterviewConstruction
    {
        private void OnCategoryChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out var catId))
            {
                ViewModel.SelectedCategoryId = catId;
                ViewModel.LoadQuestionsForCategory();
                ViewModel.SelectedQuestionId = ViewModel.AvailableQuestions.Select(q => q.Id).FirstOrDefault();
            }
        }
        private void OnQuestionChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out var qId))
            {
                ViewModel.SelectedQuestionId = qId;
            }
        }

        private void AddQuestion()
        {
            ViewModel.AddQuestionToInterview();
            ViewModel.SaveInterview();
        }

        private void DeleteQuestion(int questionId)
        {
            ViewModel.RemoveQuestionFromInterview(questionId);
            ViewModel.SaveInterview();
        }
    }
}

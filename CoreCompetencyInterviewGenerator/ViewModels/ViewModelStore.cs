using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewGeneratorBlazorHybrid.ViewModels
{
    /// <summary>
    /// This is a holder of all of the injected ViewModels for easy access.
    /// This makes it easier to pass it around to reset each one when needed. 
    /// </summary>
    public class ViewModelStore
    {
        public CategoryViewModel CategoryViewModel { get; }
        public QuestionViewModel QuestionViewModel { get; }
        public InterviewViewModel InterviewViewModel { get; }
        public ViewModelStore(
            CategoryViewModel categoryViewModel,
            QuestionViewModel questionViewModel,
            InterviewViewModel interviewViewModel)
        {
            CategoryViewModel = categoryViewModel;
            QuestionViewModel = questionViewModel;
            InterviewViewModel = interviewViewModel;
        }
    }
}

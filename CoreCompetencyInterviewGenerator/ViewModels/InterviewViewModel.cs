using CoreCompetencyInterviewGenerator.Data;
using CoreCompetencyInterviewGenerator.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreCompetencyInterviewGenerator.ViewModels
{
    public class InterviewViewModel
    {
        //[Inject] public IConfiguration Configuration { get; set; }

        private readonly AppDbContextFactory _contextFactory;
        public AppDbContext _context { get; set; }

        // Change notification
        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        // SetProperty helper
        private void SetProperty<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            NotifyStateChanged();
        }

        // For the list
        private List<Interview> _interviews = new();
        public List<Interview> Interviews { get => _interviews; set => SetProperty(ref _interviews, value); }

        // For the single 'Interview' View
        private Interview _interview = new();
        public Interview Interview { get => _interview; set => SetProperty(ref _interview, value); }

        private List<Category> _categories = new();
        public List<Category> Categories { get => _categories; set => SetProperty(ref _categories, value); }

        private List<Question> _availableQuestions = new();
        public List<Question> AvailableQuestions { get => _availableQuestions; set => SetProperty(ref _availableQuestions, value); }

        private string? _errorMessage;
        public string? ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }

        private string? _successMessage;
        public string? SuccessMessage { get => _successMessage; set => SetProperty(ref _successMessage, value); }

        private int? _selectedCategoryId;
        public int? SelectedCategoryId { get => _selectedCategoryId; set => SetProperty(ref _selectedCategoryId, value); }

        private int? _selectedQuestionId;
        public int? SelectedQuestionId { get => _selectedQuestionId; set => SetProperty(ref _selectedQuestionId, value); }

        private int _selectedInterviewId;
        public int SelectedInterviewId { get => _selectedInterviewId; set => SetProperty(ref _selectedInterviewId, value); }

        private string _interviewName = string.Empty;
        public string InterviewName { get => _interviewName; set => SetProperty(ref _interviewName, value); }

        private DateTime _interviewDate = DateTime.Today;
        public DateTime InterviewDate { get => _interviewDate; set => SetProperty(ref _interviewDate, value); }

        private bool _interviewIsActive = true;
        public bool InterviewIsActive { get => _interviewIsActive; set => SetProperty(ref _interviewIsActive, value); }


        public string InterviewIsActiveString
        {
            get => Interview?.IsActive.ToString().ToLower() ?? "true";
            set
            {
                if (Interview != null && bool.TryParse(value, out var b))
                {
                    Interview.IsActive = b;
                    NotifyStateChanged();
                }
            }
        }

        private bool _isEditMode = false;
        public bool IsEditMode { get => _isEditMode; set => SetProperty(ref _isEditMode, value); }

        private bool _isAddMode = false;
        public bool IsAddMode { get => _isAddMode; set => SetProperty(ref _isAddMode, value); }

        private bool _isConstructMode = false;
        public bool IsConstructMode { get => _isConstructMode; set => SetProperty(ref _isConstructMode, value); }

        private bool _databaseIsAvailable = true;
        public bool DatabaseIsAvailable { get => _databaseIsAvailable; set => SetProperty(ref _databaseIsAvailable, value); }
        public string InterviewTemplatePath { get; set; }
        public string DatabaseFilePath { get; set; }
        private IConfiguration Configuration { get; set; }

        public InterviewViewModel(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();

            //InterviewTemplatePath = Configuration["DatabaseSettings:DatabaseFilePath"]; 
            //DatabaseFilePath = Configuration["DatabaseSettings:DatabaseFilePath"];

            LoadCategories();
            LoadInterviews();
        }

        public void LoadCategories()
        {
            using var _context = _contextFactory.CreateDbContext();
            Categories = _context.Categories.ToList();
        }

        public void LoadQuestionsForCategory()
        {
            if (SelectedCategoryId == null) return;

            AvailableQuestions = _context.Questions
                .Include(q => q.Category)
                .Where(q => q.CategoryId == SelectedCategoryId)
                .ToList();
        }

        public void LoadInterviews()
        {
            using var _context = _contextFactory.CreateDbContext();
            Interviews = _context.Interviews
                .Include(i => i.Questions)
                .OrderByDescending(i => i.DateCreated)
                .ToList();
        }

        public void AddNewInterview()
        {
            using var _context = _contextFactory.CreateDbContext();
            InterviewName = "<<New Interview>>";
            InterviewDate = DateTime.Today;
            IsAddMode = true;
            IsEditMode = true;

            var interview = new Interview
            {
                InterviewName = InterviewName,
                DateCreated = InterviewDate,
                IsActive = true,
                Questions = new List<Question>()
            };

            _context.Interviews.Add(interview);
            _context.SaveChanges();

            Interview = interview;
            LoadInterviews();
            NotifyStateChanged();
        }

        public void SaveInterview()
        {
            using var _context = _contextFactory.CreateDbContext();
            var dbInterview = _context.Interviews
                                    .Include(i => i.Questions)
                                    .FirstOrDefault(i => i.Id == Interview.Id);

            if (dbInterview == null) return;

            dbInterview.InterviewName = InterviewName;
            dbInterview.DateCreated = InterviewDate;
            dbInterview.IsActive = InterviewIsActive;
            if (_context == null) {
                return;
            }

            foreach (var q in Interview.Questions)
            {
                // 1. Check if the context is already tracking a Question with this ID
                var trackedQuestion = _context.Questions.Local.FirstOrDefault(x => x.Id == q.Id);

                if (trackedQuestion != null)
                {
                    // 2. If it's already tracked, use the version the context already knows about
                    dbInterview.Questions.Add(trackedQuestion);
                }
                else
                {
                    // 3. If it's NOT tracked, attach it now
                    _context.Questions.Attach(q);
                    dbInterview.Questions.Add(q);
                }
            }


            _context.SaveChanges();

            if (!IsConstructMode)
            {
                SuccessMessage = "Interview Name, Date and Status Updated.";
            }

            LoadInterviews();
            NotifyStateChanged();
        }
        public void LoadInterviewById(int interviewId)
        {

            // Force a Detach if the entity is currently tracked. This is just in case you deleted a question upstream. 
            if (Interview != null)
            {
                // Ensure the entry is being tracked before trying to change its state
                if (_context.Entry(Interview).State != Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    // Detach the existing entity, forcing EF to load a fresh copy from the database.
                    _context.Entry(Interview).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                }
            }

            Interview = _context.Interviews
                .Include(i => i.Questions)
                .ThenInclude(q => q.Category)
                .FirstOrDefault(i => i.Id == interviewId);

            if (Interview != null)
            {
                InterviewName = Interview.InterviewName;
                InterviewDate = Interview.DateCreated;
                InterviewIsActive = Interview.IsActive;
            }
            NotifyStateChanged();
        }
        public void DeleteInterview(int id)
        {
            try
            {
                var interview = _context.Interviews.Find(id);
                if (interview != null)
                {
                    _context.Interviews.Remove(interview);
                    _context.SaveChanges();
                    LoadInterviews();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting interview: {ex.Message}";
            }
            NotifyStateChanged();
        }

        public void EnterConstructionMode()
        {
            LoadCategories();
            LoadQuestionsForCategory();
            SaveInterview();
            ResetMessages();
            LoadInterviewById(Interview.Id);
            IsConstructMode = true;
            NotifyStateChanged();
        }

        public void ExitConstructionMode()
        {
            LoadInterviews();
            SaveInterview();
            IsConstructMode = false;
            NotifyStateChanged();
        }

        public void AddQuestionToInterview()
        {
            if (SelectedQuestionId == null) return;

            using var _context = _contextFactory.CreateDbContext();

            // Load the interview from the database including its questions
            var dbInterview = _context.Interviews
                                .Include(i => i.Questions)
                                .FirstOrDefault(i => i.Id == Interview.Id);
            
            if (dbInterview == null) return;

            var dbQuestion = _context.Questions.Find(SelectedQuestionId);

            Question? question = AvailableQuestions.FirstOrDefault(q => q.Id == SelectedQuestionId);

            if (question != null && !Interview.Questions.Any(q => q.Id == question.Id))
            {
                // Add to the database relationship
                dbInterview.Questions.Add(dbQuestion);
                _context.SaveChanges();

                // Add to the list
                Interview.Questions.Add(question);
                SaveInterview();
                NotifyStateChanged();
            }
        }

        public void RemoveQuestionFromInterview(int questionId)
        {
            using var _context = _contextFactory.CreateDbContext();
            var question = Interview.Questions.FirstOrDefault(q => q.Id == questionId);

            var dbInterview = _context.Interviews
                    .Include(i => i.Questions)
                    .FirstOrDefault(i => i.Id == Interview.Id);

            var dbQuestion = _context.Questions.Find(questionId);

            if (question != null)
            {
                // Remove from the database relationship
                dbInterview.Questions.Remove(dbQuestion);
                _context.SaveChanges();

                Interview.Questions.Remove(question);
                SaveInterview();
                NotifyStateChanged();
            }

        }

        public MemoryStream GenerateInterviewDoc(int interviewId)
        {
            var wordHelper = new Helpers.MSWordHelper(_contextFactory);
            return wordHelper.GenerateInterviewDoc(interviewId);
        }

        public bool IsDatabaseAvailable()
        {
            var integrityCheck = new AppDbIntegrityCheck(_contextFactory);

            //if (DatabaseFilePath == null)
            //{
            //    DatabaseIsAvailable = false;
            //}
            //else
            //{
            //    DatabaseIsAvailable = integrityCheck.IsValidDatabase();
            //}
            return DatabaseIsAvailable;
        }

        public void ResetMessages()
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
            NotifyStateChanged();
        }
        public void ResetForm()
        {
            SaveInterview();
            Interview = new Interview();

            IsEditMode = false;
            IsAddMode = false;
            IsConstructMode = false;

            ErrorMessage = null;
            SuccessMessage = null;
            NotifyStateChanged();
        }

        public void ResetViewModel()
        {
            //_contextFactory = new AppDbContextFactory();
            // Reset properties to default values
            _context = _contextFactory.CreateDbContext();

            _interviews = new List<Interview>();
            _interview = new Interview();
            _categories = new List<Category>();
            _availableQuestions = new List<Question>();
            _errorMessage = string.Empty;
            _successMessage = string.Empty;
            _selectedCategoryId = null;
            _selectedQuestionId = null;
            _selectedInterviewId = 0;
            _interviewName = string.Empty;
            _interviewDate = DateTime.Today;
            _interviewIsActive = true;
            _isEditMode = false;
            _isAddMode = false;
            _isConstructMode = false;
            _databaseIsAvailable = false;

            LoadCategories();
            LoadInterviews();
        }

        internal void UpdateTemplatePath(string selectedPath)
        {
            //Preferences.Set("TemplateDocumentPath", selectedPath);
            //InterviewTemplatePath = Preferences.Get("TemplateDocumentPath", string.Empty);
            NotifyStateChanged();
        }
    }
}
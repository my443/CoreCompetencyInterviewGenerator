using InterviewGeneratorBlazorHybrid.Data;
using InterviewGeneratorBlazorHybrid.Models;

namespace InterviewGeneratorBlazorHybrid.ViewModels
{
    public class QuestionViewModel
    {
        private readonly AppDbContextFactory _contextFactory;
        private AppDbContext _context;

        private int _categoryId;

        public List<Question> Questions { get; set; } = new();
        public Question QuestionModel { get; set; } = new();
        public bool IsEditMode { get; set; } = false;
        public string? ErrorMessage { get; set; }
        public List<Category> Categories { get; set; } = new();
        public Category Category { get; set; }
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                if (_categoryId != value)
                {
                    ChangeCategory(value);
                }
            }
        }
        public bool IsAddMode { get; set; } = false;


        public QuestionViewModel(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
            //_categoryId = categoryId;
            LoadCategories();
            LoadQuestions();
        }
        public void LoadQuestions()
        {
            Questions = _context.Questions
                .Where(q => q.CategoryId == _categoryId)
                .ToList();
        }

        public void LoadCategories()
        {
            Category = _context.Categories.Find(_categoryId) ?? new Category();
            Categories = _context.Categories.ToList();
        }

        public void EditQuestion(Question question)
        {
            IsEditMode = true;

            QuestionModel = new Question
            {
                Id = question.Id,
                Text = question.Text,
                Answer = "-",
                CategoryId = question.CategoryId
            };
        }

        public void AddNewQuestion()
        {
            IsEditMode = true;
            IsAddMode = true;
            QuestionModel = new Question();
        }
        public void DeleteQuestion(int id)
        {
            var q = _context.Questions.Find(id);
            if (q != null)
            {
                _context.Questions.Remove(q);
                _context.SaveChanges();
                LoadQuestions();
            }
            if (IsEditMode && QuestionModel.Id == id)
            {
                ResetForm();
            }
        }

        public void SaveQuestion()
        {
            ErrorMessage = null;
            QuestionModel.Answer = "-";                 // So that the answer is always reset to "-"

            if (string.IsNullOrWhiteSpace(QuestionModel.Text))
            {
                ErrorMessage = "Question text is required.";
                return;
            }
            if (string.IsNullOrWhiteSpace(QuestionModel.Answer))
            {
                ErrorMessage = "Answer is required.";
                return;
            }

            if (!IsAddMode)
            {
                var q = _context.Questions.Find(QuestionModel.Id);
                if (q != null)
                {
                    q.Text = QuestionModel.Text;
                    q.Answer = "-";
                    _context.SaveChanges();
                    LoadQuestions();
                }
            }
            else
            {
                var newQuestion = new Question
                {
                    Text = QuestionModel.Text,
                    Answer = "-",
                    CategoryId = _categoryId
                };
                _context.Questions.Add(newQuestion);
                _context.SaveChanges();
                LoadQuestions();
            }
            ResetForm();
        }

        public void ChangeCategory(int categoryId)
        {
            _categoryId = categoryId;
            LoadCategories();
            LoadQuestions();
            Category = Categories.FirstOrDefault(c => c.Id == _categoryId);
            ResetForm();
        }

        public void ResetForm()
        {
            QuestionModel = new Question { CategoryId = _categoryId };
            IsEditMode = false;
            IsAddMode = false;
            ErrorMessage = null;
        }
        public void ResetViewModel()
        {
            //_contextFactory = new AppDbContextFactory();
            // Reset properties to default values
            _context = _contextFactory.CreateDbContext();
            _categoryId = 0;

            Questions = new List<Question>();
            QuestionModel = new Question();
            IsEditMode = false;
            ErrorMessage = string.Empty;
            Categories = new List<Category>();
            Category = new Category();

            LoadCategories();

        }
    }
}

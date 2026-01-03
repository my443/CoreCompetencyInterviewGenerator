using InterviewGeneratorBlazorHybrid.Data;
using InterviewGeneratorBlazorHybrid.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace InterviewGeneratorBlazorHybrid.ViewModels
{
    public class CategoryViewModel
    {
        private readonly AppDbContextFactory _contextFactory;
        private AppDbContext _context;

        public List<Category> Categories { get; set; } = new();
        public Category CategoryModel { get; set; } = new();
        public bool IsEditMode { get; set; } = false;
        public bool IsAddMode { get; set; } = false;
        public string? ErrorMessage { get; set; }


        public CategoryViewModel(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
            LoadCategories();
        }


        public void LoadCategories()
        {
            Categories = _context.Categories.Include(c => c.Questions).ToList();
        }

        public void AddCategory()
        {
            IsEditMode = true;
            IsAddMode = true;
            CategoryModel = new Category();
        }

        public void EditCategory(Category category)
        {
            IsEditMode = true;
            CategoryModel = new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Questions = category.Questions.ToList()
            };

            //_context.SaveChanges(CategoryModel);
        }

        public void DeleteCategory(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var cat = db.Categories.Find(id);
            if (cat != null)
            {
                db.Categories.Remove(cat);
                db.SaveChanges();
                LoadCategories();
            }
            if (IsEditMode && CategoryModel.Id == id)
            {
                ResetForm();
            }
        }

        public void SaveCategory()
        {
            ErrorMessage = null;
            //using var db = _contextFactory.CreateDbContext();
            if (string.IsNullOrWhiteSpace(CategoryModel.Name))
            {
                ErrorMessage = "Name is required.";
                return;
            }

            var cat = _context.Categories.Find(CategoryModel.Id);
            if (cat != null)
            {
                cat.Name = CategoryModel.Name;
                cat.Description = CategoryModel.Description;
                _context.SaveChanges();
                LoadCategories();
            }
            else
            {
                var newCategory = new Category
                {
                    Name = CategoryModel.Name,
                    Description = CategoryModel.Description,
                    Questions = new List<Question>()
                };
                _context.Categories.Add(newCategory);
                _context.SaveChanges();
                LoadCategories();
            }
            ResetForm();
        }

        public void ResetForm()
        {
            CategoryModel = new Category();
            IsEditMode = false;
            IsAddMode = false;
            ErrorMessage = null;
        }

        public void ResetViewModel()
        {
            //_contextFactory = new AppDbContextFactory();
            _context = _contextFactory.CreateDbContext();
            Categories = new List<Category>();
            CategoryModel = new Category();
            IsEditMode = false;
            IsAddMode = false;
            ErrorMessage = string.Empty;
            LoadCategories();
        }
    }
}

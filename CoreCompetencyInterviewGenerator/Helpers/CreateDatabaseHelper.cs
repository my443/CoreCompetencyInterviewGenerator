using InterviewGeneratorBlazorHybrid.Data;
using InterviewGeneratorBlazorHybrid.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InterviewGeneratorBlazorHybrid.Helpers
{
    internal class CreateDatabaseHelper
    {
        private readonly AppDbContextFactory _contextFactory;


        public CreateDatabaseHelper(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void CreateDatabase(string databaseFilePath)
        {
            Preferences.Set("DatabaseFilePath", databaseFilePath);

            using var context = _contextFactory.CreateDbContext();
            // Ensure the database is deleted, and then is created if needed.
            context.Database.EnsureDeletedAsync();
            context.Database.EnsureCreated();
        }

        public void AddSampleData() { 
            using var context = _contextFactory.CreateDbContext();

            context.Database.ExecuteSqlRaw(SampleDatabaseHelper.CategoriesInsertQuery);
            context.Database.ExecuteSqlRaw(SampleDatabaseHelper.QuestionsInsertQuery);
            context.Database.ExecuteSqlRaw(SampleDatabaseHelper.InterviewsInsertQuery);
            context.Database.ExecuteSqlRaw(SampleDatabaseHelper.InterviewQuestionsInsertQuery);
        }
    }
}

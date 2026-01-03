using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCompetencyInterviewGenerator.Data
{
    public class AppDbContextFactory
    {
        public IConfiguration Configuration { get; set; }
        private readonly string _connectionString;
        public bool IsDatabaseAvailable { get; set; }

        public AppDbContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
            //_connectionString = $"Data Source={Preferences.Get("DatabaseFilePath", "")}";
            //_connectionString = "Data Source=c:\\temp\\app.db";
        }


        public AppDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            //var dbPath = Preferences.Get("DatabaseFilePath", string.Empty);
            var dbPath = Configuration["DatabaseSettings:DatabaseFilePath"];

            // If no path is set in preferences, default to a file on the desktop
            if (string.IsNullOrWhiteSpace(dbPath)) {
                string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dbPath = Path.Combine(desktopDirectory, "interviews.db");
            }

            string connectionString = $"Data Source={dbPath}";
            optionsBuilder.UseSqlite(connectionString);

            var context = new AppDbContext(optionsBuilder.Options);

            // Ensure the database and schema exist
            context.Database.EnsureCreated();

            return context;
        }
    }
}

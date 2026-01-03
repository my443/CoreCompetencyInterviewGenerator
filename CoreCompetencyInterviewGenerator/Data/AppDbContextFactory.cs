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

        public AppDbContextFactory(){}


        public AppDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Define your path manually here
            string dbPath = @"C:\Temp\InterviewApp.db";

            // Ensure the directory exists so it doesn't crash
            var folder = Path.GetDirectoryName(dbPath);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

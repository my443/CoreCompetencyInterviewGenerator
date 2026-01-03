using Microsoft.EntityFrameworkCore;
using InterviewGeneratorBlazorHybrid.Models;

namespace InterviewGeneratorBlazorHybrid.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Interview> Interviews { get; set; }
    }
}

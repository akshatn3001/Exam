using Exam.Models;
using Microsoft.EntityFrameworkCore;
namespace Exam.DBContext
{
    public class ExamDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database connection string
            optionsBuilder.UseSqlServer("DefaultConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the model if needed
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            // Additional configurations...
        }
    }
}

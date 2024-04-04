using Exam.Models;
using Microsoft.EntityFrameworkCore;

public class ExamDbContext : DbContext
{
    public ExamDbContext(DbContextOptions<ExamDbContext> options)
        : base(options)
    {
    }

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

    public DbSet<User> Users { get; set; }
    public DbSet<ExamModel> Exams { get; set; }

}


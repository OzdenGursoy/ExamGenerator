using ExamGenerator.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExamGenerator.Models;

namespace ExamGenerator.Data;

public class ExamGeneratorDbContext : IdentityDbContext<ExamGeneratorUser>
{
    public ExamGeneratorDbContext(DbContextOptions<ExamGeneratorDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<ExamGenerator.Models.Exam> Exam { get; set; }
    public DbSet<ExamGenerator.Models.Question> Question { get; set; }
}

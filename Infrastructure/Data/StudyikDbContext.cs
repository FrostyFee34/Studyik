using Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
    public class StudyikDbContext : DbContext
    {
        public StudyikDbContext(DbContextOptions<StudyikDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Video> Videos { get; set; }
    }
}
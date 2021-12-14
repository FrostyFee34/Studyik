using Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
    public class StudyikDbContext : DbContext
    {
        public StudyikDbContext(DbContextOptions<StudyikDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Material>()
                .HasMany(e => e.Notes)
                .WithOne(e => e.Material)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
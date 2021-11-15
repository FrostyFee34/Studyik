using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StudyikDbContextSeed
    {
        public static async Task SeedAsync(StudyikDbContext context, ILoggerFactory loggerFactory)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category {Id = 1, Name = "File"},
                    new Category {Id = 2, Name = "Video"},
                    new Category {Id = 3, Name = "Text"});
                await context.SaveChangesAsync();
            }
        }

    }
}
using Core.Entities;
using Core.Specifications.Params;

namespace Core.Specifications
{
    public class ArticlesWithNotesSpec : BaseSpecification<Article>
    {
        public ArticlesWithNotesSpec(ArticleSpecParams noteParams) : base(x =>
            // If left side of condition is false, execute the right side
            // Filter by name
            string.IsNullOrEmpty(noteParams.Search) || x.Header.ToLower().Contains(noteParams.Search))
        { 
            if(noteParams.Notes == true) AddInclude(x => x.Notes);

            switch (noteParams.Sort)
            {
                case "date":
                    AddOrderByDescending(x => x.ReminderTime);
                    break;
                case "isDone":
                    AddOrderBy(x => x.IsDone);
                    break;
                case "alphabetical":
                    AddOrderBy(x => x.Header);
                    break;
                default:
                    AddOrderBy(j => j.Id);
                    break;
            }
            

        }

        public ArticlesWithNotesSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Notes);
        }
    }
}
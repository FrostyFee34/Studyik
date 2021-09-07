using Core.Entities;
using Core.Specifications.Params;

namespace Core.Specifications
{
    public class JobsWithArticlesNotesAndVideosSpec : BaseSpecification<Job>
    {
        public JobsWithArticlesNotesAndVideosSpec(JobSpecParams jobParams) : base(x =>
            // If left side of condition is false, execute the right side
            // Filter by name
            string.IsNullOrEmpty(jobParams.Search) || x.Header.ToLower().Contains(jobParams.Search))
        { 
            if(jobParams.Articles == true) AddInclude(x => x.Articles);
            if(jobParams.Videos == true) AddInclude(x => x.Videos);
            if(jobParams.Notes == true) AddInclude(x => x.Notes);

            switch (jobParams.Sort)
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

        public JobsWithArticlesNotesAndVideosSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Articles);
            AddInclude(x => x.Videos);
            AddInclude(x => x.Notes);
        }

    }
}
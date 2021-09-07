using Core.Entities;
using Core.Specifications.Params;

namespace Core.Specifications
{
    public class VideosWithNotesSpec : BaseSpecification<Video>
    {
        public VideosWithNotesSpec(VideoSpecParams videoParams) : base(x =>
            string.IsNullOrEmpty(videoParams.Search) || x.Header.ToLower().Contains(videoParams.Search))
        {
            if (videoParams.Notes == true) AddInclude(x => x.Notes);
            
            switch (videoParams.Sort)
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
        public VideosWithNotesSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Notes);
        }
    }
}
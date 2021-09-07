using System.Runtime.InteropServices.ComTypes;
using Core.Entities;
using Core.Specifications.Params;

namespace Core.Specifications
{
    public class NotesWithSearchSpec : BaseSpecification<Note>
    {
        public NotesWithSearchSpec(NoteSpecParams noteParams) : base(x =>
            string.IsNullOrEmpty(noteParams.Search) || x.Header.ToLower().Contains(noteParams.Search))
        {
            switch (noteParams.Sort)
            {
                case "alphabetical":
                    AddOrderBy(x => x.Header);
                    break;
                default:
                    AddOrderBy(j => j.Id);
                    break;
            }
        }

        public NotesWithSearchSpec(int id) : base(x => x.Id == id)
        {

        }
    }
}
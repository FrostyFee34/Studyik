using Core.Entities;

namespace Core.Specifications
{
    public class GroupsByUserIdSpec : BaseSpecification<Group>
    {
        public GroupsByUserIdSpec(string userUid) : base(g => g.UserUid == userUid)
        {
        }
    }
}
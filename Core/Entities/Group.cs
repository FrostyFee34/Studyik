namespace Core.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserUid { get; set; }
    }
}
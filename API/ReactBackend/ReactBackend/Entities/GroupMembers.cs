namespace ReactBackend.Entities
{
    public class GroupMembers
    {
        public int Id { get; set; }
        public int groupID { get; set; }
        public int memberID { get; set; }
        public int isAdmin { get; set; }
    }
}

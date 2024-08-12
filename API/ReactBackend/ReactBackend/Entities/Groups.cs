namespace ReactBackend.Entities
{
    public class Groups
    {
        public int groupID { get; set; } 
        public string groupName { get; set; }
        public int groupCreatorID { get; set; }
        public DateTime groupDate { get; set; }
        public byte[] groupIMG { get; set; }
    }
}

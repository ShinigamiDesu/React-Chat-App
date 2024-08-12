namespace ReactBackend.Entities
{
    public class GroupMessages
    {
        public int Id { get; set; }
        public int groupID { get; set; }
        public int group_message_fromID { get; set; }
        public string group_message { get; set; }
        public DateTime group_message_date { get; set; }
    }
}

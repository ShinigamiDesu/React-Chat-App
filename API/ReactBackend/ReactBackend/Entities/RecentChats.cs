namespace ReactBackend.Entities
{
    public class RecentChats
    {
        public int ID { get; set; }
        public int chat_fromID { get; set; }
        public int chat_toID { get; set; }
        public DateTime chat_date { get; set; }
    }
}

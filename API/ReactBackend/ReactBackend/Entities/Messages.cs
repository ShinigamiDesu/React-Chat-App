namespace ReactBackend.Entities
{
    public class Messages
    {
        public int message_ID { get; set; }
        public int message_fromID { get; set; }
        public int message_toID { get; set; }
        public string message { get; set; }
        public DateTime message_date { get; set; }
    }
}

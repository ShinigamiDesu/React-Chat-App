namespace ReactBackend.Entities
{
    public class FriendRequests
    {
        public int ID { get; set; }
        public int request_fromID { get; set; }
        public int request_toID { get; set; }
    }
}

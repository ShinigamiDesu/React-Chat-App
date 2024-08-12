namespace ReactBackend.Entities
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public byte[] PFP { get; set; }  // Store image as byte array
        public int Status { get; set; }
    }
}

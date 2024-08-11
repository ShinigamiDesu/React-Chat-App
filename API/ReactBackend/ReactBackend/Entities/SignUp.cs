namespace ReactBackend.Entities
{
    public class SignUp
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public IFormFile PFP { get; set; }
    }
}

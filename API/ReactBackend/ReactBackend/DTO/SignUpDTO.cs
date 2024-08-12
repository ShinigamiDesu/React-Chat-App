namespace ReactBackend.DTO
{
    public class SignUpDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public IFormFile PFP { get; set; }
    }
}

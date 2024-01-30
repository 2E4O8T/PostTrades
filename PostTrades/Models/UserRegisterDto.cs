namespace PostTrades.Models
{
    public class UserRegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Fullname { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}

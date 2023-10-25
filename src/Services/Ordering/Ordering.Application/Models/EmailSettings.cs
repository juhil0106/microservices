namespace Ordering.Application.Models
{
    public class EmailSettings
    {
        public string FromEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Host { get; set; } = null!;
        public string Port { get; set; } = null!;   
    }
}

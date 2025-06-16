namespace Pokemon.Data.Models
{
    public class UserSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SessionToken { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; } = false;
        public string? Ip { get; set; }
    }
}

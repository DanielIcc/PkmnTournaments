using Pokemon.Data.Models;

namespace Pokemon.Data
{
    public static class Seed
    {
        public static void SeedAdmin(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Username = "admin",
                    PasswordHash = HashPassword("admin123"),
                    Role = UserRole.Admin
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }

        private static string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pokemon.Data;
using Pokemon.Data.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Pokemon.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<SessionSettings> _sessionSettings;
        public AuthService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IOptions<SessionSettings> sessionSettings)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _sessionSettings = sessionSettings;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || user.PasswordHash != HashPassword(password))
                return false;

            var sessionToken = Guid.NewGuid().ToString();

            string ip = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            if (_httpContextAccessor?.HttpContext?.Request?.Headers?.ContainsKey("X-Forwarded-For") == true)
            {
                var forwardedFor = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                if (!string.IsNullOrEmpty(forwardedFor))
                {
                    ip = forwardedFor.Split(',')[0];
                }
            }

            var userSession = new UserSession
            {
                UserId = user.Id,
                SessionToken = sessionToken,
                CreatedAt = DateTime.UtcNow,
                Ip = ip,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_sessionSettings.Value.SessionDurationMinutes)
            };

            _context.UserSessions.Add(userSession);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("SessionToken", sessionToken)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            var authProps = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await _httpContextAccessor.HttpContext!.SignInAsync("Cookies", principal, authProps);

            return true;
        }

        public async Task LogoutAsync(string sessionToken)
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(s => s.SessionToken == sessionToken);

            if (session != null)
            {
                session.IsRevoked = true;
                await _context.SaveChangesAsync();
            }

            await _httpContextAccessor.HttpContext!.SignOutAsync("Cookies");
        }

        public bool IsLoggedIn => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public string? CurrentUsername => _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

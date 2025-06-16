using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokemon.Services;

namespace Pokemon.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly AuthService _authService;

        public LogoutModel(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var sessionToken = User.FindFirst("SessionToken")?.Value;
            if (!string.IsNullOrEmpty(sessionToken))
            {
                await _authService.LogoutAsync(sessionToken);
            }
            return RedirectToPage("/login");
        }
    }
}

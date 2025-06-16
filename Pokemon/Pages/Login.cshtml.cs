using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokemon.Services;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        [Required(ErrorMessage = "El campo de usuario es obligatorio.")]        
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "El campo de contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            ViewData["OcultarHeader"] = true;

            if (_authService.IsLoggedIn)
            {
                Response.Redirect("/");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["OcultarHeader"] = true;

            if (!ModelState.IsValid)
                return Page();

            var result = await _authService.LoginAsync(Username, Password);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Pokemon.Data;
using Pokemon.Data.Models;

namespace Pokemon.Pages.Jugadores
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Jugador Jugador { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                _context.Jugadores.Add(Jugador);
                await _context.SaveChangesAsync();
                ModelState.Clear();
                Jugador = new Jugador();
                return Page();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19)
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un jugador con ese nombre y apodo.");
                    return Page();
                }

                throw;
            }

        }
    }
}

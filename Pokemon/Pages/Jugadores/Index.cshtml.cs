using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pokemon.Data;
using Pokemon.Data.Models;

namespace Pokemon.Pages.Jugadores
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Jugador> Jugadores { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            const int pageSize = 5;

            var totalCount = await _context.Jugadores.CountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            CurrentPage = pageNumber;

            Jugadores = await _context.Jugadores
                .OrderBy(j => j.Nombre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}

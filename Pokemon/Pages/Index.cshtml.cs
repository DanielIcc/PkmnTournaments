using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pokemon.Data;
using Pokemon.Data.Models;

namespace Pokemon.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        public List<Jugador> Jugadores { get; set; }
        public List<Torneo> Torneos { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task OnGetAsync()
        {
            Jugadores = await _context.Jugadores
                                      .OrderByDescending(j => j.Nombre)
                                      .ToListAsync();

            Torneos = await _context.Torneos
                          .OrderByDescending(j => j.FechaInicio)
                          .ToListAsync();
        }


    }
}

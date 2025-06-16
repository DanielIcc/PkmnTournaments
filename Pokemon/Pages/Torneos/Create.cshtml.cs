using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pokemon.Data;
using Pokemon.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Pages.Torneos
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
        public Torneo Torneo { get; set; }
        public SelectList TiposTorneo { get; set; }

        public void OnGet()
        {
            TiposTorneo = new SelectList(Enum.GetValues(typeof(TipoTorneo)).Cast<TipoTorneo>());
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var torneo = new Torneo
            {
                Nombre = Torneo.Nombre,
                FechaInicio = Torneo.FechaInicio,
                FechaFin = Torneo.FechaFin,
                Descripcion = Torneo.Descripcion,
                Estado = EstadoTorneo.Preparando,
                Tipo = Torneo.Tipo
            };

            _context.Torneos.Add(torneo);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index"); // Ajusta la ruta donde quieras redirigir
        }
    }
}

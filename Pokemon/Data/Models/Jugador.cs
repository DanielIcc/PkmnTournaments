using System.ComponentModel.DataAnnotations;

namespace Pokemon.Data.Models
{
    public class Jugador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string Apodo { get; set; } = string.Empty;

        public ICollection<JugadorTorneo>? TorneosJugados { get; set; }
    }
}

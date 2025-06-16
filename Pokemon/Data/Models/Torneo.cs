using System.ComponentModel.DataAnnotations;

namespace Pokemon.Data.Models
{
    public enum TipoTorneo
    {
        Liguilla,
        Eliminatoria,
        Mixto
    }

    public enum EstadoTorneo
    {
        Preparando,
        Iniciado,
        Finalizado,
        Cancelado
    }

    public class Torneo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del torneo es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Date)]
        public DateTime? FechaFin { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        // Tipo de torneo (Liguilla, Eliminatoria, Mixto)
        [Required]
        public TipoTorneo Tipo { get; set; }

        [Required]
        public EstadoTorneo Estado { get; set; }

        // Para ordenar torneos en alguna lista o vista
        public int Orden { get; set; } = 0;

        // Relaciones
        public ICollection<JugadorTorneo>? Participantes { get; set; }
        public ICollection<Partido>? Partidos { get; set; }
        public ICollection<ResultadoTorneo>? Resultados { get; set; }

        // Ganador
        public int? GanadorId { get; set; }
        public User? Ganador { get; set; }
    }
}

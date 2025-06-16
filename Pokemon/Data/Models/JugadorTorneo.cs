using System.ComponentModel.DataAnnotations;

namespace Pokemon.Data.Models
{
    public class JugadorTorneo
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int TorneoId { get; set; }
        public Torneo Torneo { get; set; } = null!;

        // Estadísticas para liguilla
        public int PartidosJugados { get; set; }
        public int PartidosGanados { get; set; }
        public int PartidosEmpatados { get; set; }
        public int PartidosPerdidos { get; set; }
        public int Puntos { get; set; }

    }
}

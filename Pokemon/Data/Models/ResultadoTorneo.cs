namespace Pokemon.Data.Models
{
    public class ResultadoTorneo
    {
        public int Id { get; set; }

        public int TorneoId { get; set; }
        public Torneo Torneo { get; set; } = null!;

        public int JugadorId { get; set; }
        public User Jugador { get; set; } = null!;

        public int Posicion { get; set; }
    }
}

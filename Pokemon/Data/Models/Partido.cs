namespace Pokemon.Data.Models
{
    public enum Fase
    {
        Liguilla = 0,
        Dieciseisavos = 1,
        Octavos = 2,
        Cuartos = 3,
        Semifinal = 4,
        Final = 5,
        TercerLugar = 6
    }
    public class Partido
    {
        public int Id { get; set; }

        public int TorneoId { get; set; }
        public Torneo Torneo { get; set; } = null!;

        public int Jugador1Id { get; set; }
        public User Jugador1 { get; set; } = null!;

        public int Jugador2Id { get; set; }
        public User Jugador2 { get; set; } = null!;

        public int? PuntosJugador1 { get; set; }
        public int? PuntosJugador2 { get; set; }

        public int? GanadorId { get; set; }
        public User? Ganador { get; set; }

        public Fase Fase { get; set; }
    }

}

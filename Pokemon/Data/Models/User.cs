using System.ComponentModel.DataAnnotations;

namespace Pokemon.Data.Models
{
    public enum UserRole
    {
        Admin,
        Jugador
    }
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        public ICollection<JugadorTorneo>? TorneosJugados { get; set; }
    }

}

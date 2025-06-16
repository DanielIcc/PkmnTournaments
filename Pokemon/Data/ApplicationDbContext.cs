
// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using Pokemon.Data.Models;


namespace Pokemon.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Jugador> Jugadores { get; set; } = null!;
        public DbSet<Torneo> Torneos { get; set; } = null!;
        public DbSet<JugadorTorneo> JugadoresTorneos { get; set; } = null!;
        public DbSet<Partido> Partidos { get; set; } = null!;
        public DbSet<UserSession> UserSessions { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JugadorTorneo>()
                .HasIndex(jt => new { jt.UserId, jt.TorneoId })
                .IsUnique();

            modelBuilder.Entity<Partido>()
                .HasOne(p => p.Jugador1)
                .WithMany()
                .HasForeignKey(p => p.Jugador1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Partido>()
                .HasOne(p => p.Jugador2)
                .WithMany()
                .HasForeignKey(p => p.Jugador2Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Partido>()
                .HasOne(p => p.Ganador)
                .WithMany()
                .HasForeignKey(p => p.GanadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Jugador>()
                .HasIndex(j => new { j.Nombre, j.Apodo })
                .IsUnique();
        }
    }

}

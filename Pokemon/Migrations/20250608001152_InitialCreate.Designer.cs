﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pokemon.Data;

#nullable disable

namespace Pokemon.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250608001152_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.16");

            modelBuilder.Entity("Pokemon.Data.Models.JugadorTorneo", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TorneoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Puntaje")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "TorneoId");

                    b.HasIndex("TorneoId");

                    b.ToTable("JugadoresTorneos");
                });

            modelBuilder.Entity("Pokemon.Data.Models.Torneo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Torneos");
                });

            modelBuilder.Entity("Pokemon.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Pokemon.Data.Models.JugadorTorneo", b =>
                {
                    b.HasOne("Pokemon.Data.Models.Torneo", "Torneo")
                        .WithMany("Jugadores")
                        .HasForeignKey("TorneoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pokemon.Data.Models.User", "User")
                        .WithMany("TorneosJugados")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Torneo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pokemon.Data.Models.Torneo", b =>
                {
                    b.Navigation("Jugadores");
                });

            modelBuilder.Entity("Pokemon.Data.Models.User", b =>
                {
                    b.Navigation("TorneosJugados");
                });
#pragma warning restore 612, 618
        }
    }
}

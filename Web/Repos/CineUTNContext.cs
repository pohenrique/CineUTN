using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Repos.Models;

namespace Web.Repos;

public partial class CineUTNContext : DbContext
{
    public CineUTNContext()
    {
    }

    public CineUTNContext(DbContextOptions<CineUTNContext> options)
        : base(options)
    {
    }


    public virtual DbSet<CondicionPago> CondicionPagos { get; set; }
    public virtual DbSet<Funcion> Funciones { get; set; }
    public virtual DbSet<ListaPrecio> ListaPrecios { get; set; }
    public virtual DbSet<Tarifa> Tarifas { get; set; }
    public virtual DbSet<Subtitulo> Subtitulos { get; set; }
    public virtual DbSet<Sonido> Sonidos { get; set; }
    public virtual DbSet<Sala> Salas { get; set; }
    public virtual DbSet<Genero> Generos { get; set; }
    public virtual DbSet<Tipo> Tipos { get; set; }
    public virtual DbSet<Pelicula> Peliculas { get; set; }
    public virtual DbSet<Programar> Programaciones { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("name=conexion");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

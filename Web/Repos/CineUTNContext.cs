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

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("name=conexion");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Areas.Identity.Data;
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
    public virtual DbSet<ListaPrecio> ListaPrecios { get; set; }
    public virtual DbSet<Tarifa> Tarifas { get; set; }
    public virtual DbSet<Subtitulo> Subtitulos { get; set; }
    public virtual DbSet<Sonido> Sonidos { get; set; }
    public virtual DbSet<Sala> Salas { get; set; }
    public virtual DbSet<Genero> Generos { get; set; }
    public virtual DbSet<Tipo> Tipos { get; set; }
    public virtual DbSet<Pelicula> Peliculas { get; set; }
    public virtual DbSet<Programar> Programaciones { get; set; }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }

    public DbSet<Funcion> Funciones { get; set; }
    public DbSet<FuncionTarifa> FuncionTarifas { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("name=conexion");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Pedido>().Property(t => t.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<PedidoItem>().Property(t => t.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Funcion>().Property(t => t.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<FuncionTarifa>().Property(t => t.Id).ValueGeneratedOnAdd();


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}




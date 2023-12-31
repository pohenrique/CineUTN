﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Repos;

#nullable disable

namespace Web.Migrations
{
    [DbContext(typeof(CineUTNContext))]
    [Migration("20231030195203_func_rm")]
    partial class func_rm
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Web.Models.CondicionPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("CondicionPago");
                });

            modelBuilder.Entity("Web.Models.ListaPrecio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CondicionPagoRefId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaHasta")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CondicionPagoRefId");

                    b.ToTable("ListaPrecio");
                });

            modelBuilder.Entity("Web.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUrgent")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Web.Models.PedidoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PedidoId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("PedidoItens");
                });

            modelBuilder.Entity("Web.Models.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Clasificacion")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Duracion")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaEstreno")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("GeneroRefId")
                        .HasColumnType("int");

                    b.Property<string>("ImagemPelicula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubtituloRefId")
                        .HasColumnType("int");

                    b.Property<int?>("TipoRefId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GeneroRefId");

                    b.HasIndex("SubtituloRefId");

                    b.HasIndex("TipoRefId");

                    b.ToTable("Pelicula");
                });

            modelBuilder.Entity("Web.Models.Programar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("FechaHoraFuncion")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("PeliculaRefId")
                        .HasColumnType("int");

                    b.Property<int?>("SalaRefId")
                        .HasColumnType("int");

                    b.Property<int?>("Tarifa1RefId")
                        .HasColumnType("int");

                    b.Property<int?>("Tarifa2RefId")
                        .HasColumnType("int");

                    b.Property<int?>("Tarifa3RefId")
                        .HasColumnType("int");

                    b.Property<int?>("Tarifa4RefId")
                        .HasColumnType("int");

                    b.Property<int?>("Tarifa5RefId")
                        .HasColumnType("int");

                    b.Property<int?>("Tarifa6RefId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PeliculaRefId");

                    b.HasIndex("SalaRefId");

                    b.HasIndex("Tarifa1RefId");

                    b.HasIndex("Tarifa2RefId");

                    b.HasIndex("Tarifa3RefId");

                    b.HasIndex("Tarifa4RefId");

                    b.HasIndex("Tarifa5RefId");

                    b.HasIndex("Tarifa6RefId");

                    b.ToTable("Programaciones");
                });

            modelBuilder.Entity("Web.Models.Sala", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("SonidoRefId")
                        .HasColumnType("int");

                    b.Property<int?>("TipoRefId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SonidoRefId");

                    b.HasIndex("TipoRefId");

                    b.ToTable("Sala");
                });

            modelBuilder.Entity("Web.Models.Sonido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("Sonido");
                });

            modelBuilder.Entity("Web.Models.Subtitulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("Subtitulo");
                });

            modelBuilder.Entity("Web.Models.Tarifa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("ListaPrecioRefId")
                        .HasColumnType("int");

                    b.Property<int>("PorcentajeDescuento")
                        .HasColumnType("int");

                    b.Property<decimal?>("TarifaPrecio")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("ListaPrecioRefId");

                    b.ToTable("Tarifa");
                });

            modelBuilder.Entity("Web.Models.Tipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("Tipo");
                });

            modelBuilder.Entity("Web.Repos.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("Web.Models.ListaPrecio", b =>
                {
                    b.HasOne("Web.Models.CondicionPago", "CondicionPago")
                        .WithMany()
                        .HasForeignKey("CondicionPagoRefId");

                    b.Navigation("CondicionPago");
                });

            modelBuilder.Entity("Web.Models.PedidoItem", b =>
                {
                    b.HasOne("Web.Models.Pedido", null)
                        .WithMany("Items")
                        .HasForeignKey("PedidoId");
                });

            modelBuilder.Entity("Web.Models.Pelicula", b =>
                {
                    b.HasOne("Web.Repos.Models.Genero", "Genero")
                        .WithMany()
                        .HasForeignKey("GeneroRefId");

                    b.HasOne("Web.Models.Subtitulo", "Subtitulo")
                        .WithMany()
                        .HasForeignKey("SubtituloRefId");

                    b.HasOne("Web.Models.Tipo", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoRefId");

                    b.Navigation("Genero");

                    b.Navigation("Subtitulo");

                    b.Navigation("Tipo");
                });

            modelBuilder.Entity("Web.Models.Programar", b =>
                {
                    b.HasOne("Web.Models.Pelicula", "Pelicula")
                        .WithMany()
                        .HasForeignKey("PeliculaRefId");

                    b.HasOne("Web.Models.Sala", "Sala")
                        .WithMany()
                        .HasForeignKey("SalaRefId");

                    b.HasOne("Web.Models.Tarifa", "Tarifa1")
                        .WithMany()
                        .HasForeignKey("Tarifa1RefId");

                    b.HasOne("Web.Models.Tarifa", "Tarifa2")
                        .WithMany()
                        .HasForeignKey("Tarifa2RefId");

                    b.HasOne("Web.Models.Tarifa", "Tarifa3")
                        .WithMany()
                        .HasForeignKey("Tarifa3RefId");

                    b.HasOne("Web.Models.Tarifa", "Tarifa4")
                        .WithMany()
                        .HasForeignKey("Tarifa4RefId");

                    b.HasOne("Web.Models.Tarifa", "Tarifa5")
                        .WithMany()
                        .HasForeignKey("Tarifa5RefId");

                    b.HasOne("Web.Models.Tarifa", "Tarifa6")
                        .WithMany()
                        .HasForeignKey("Tarifa6RefId");

                    b.Navigation("Pelicula");

                    b.Navigation("Sala");

                    b.Navigation("Tarifa1");

                    b.Navigation("Tarifa2");

                    b.Navigation("Tarifa3");

                    b.Navigation("Tarifa4");

                    b.Navigation("Tarifa5");

                    b.Navigation("Tarifa6");
                });

            modelBuilder.Entity("Web.Models.Sala", b =>
                {
                    b.HasOne("Web.Models.Sonido", "Sonido")
                        .WithMany()
                        .HasForeignKey("SonidoRefId");

                    b.HasOne("Web.Models.Tipo", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoRefId");

                    b.Navigation("Sonido");

                    b.Navigation("Tipo");
                });

            modelBuilder.Entity("Web.Models.Tarifa", b =>
                {
                    b.HasOne("Web.Models.ListaPrecio", "ListaPrecio")
                        .WithMany()
                        .HasForeignKey("ListaPrecioRefId");

                    b.Navigation("ListaPrecio");
                });

            modelBuilder.Entity("Web.Models.Pedido", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}

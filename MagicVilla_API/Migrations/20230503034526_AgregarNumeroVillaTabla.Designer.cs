﻿// <auto-generated />
using System;
using MagicVilla_API.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230503034526_AgregarNumeroVillaTabla")]
    partial class AgregarNumeroVillaTabla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_API.Models.NumeroVilla", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<string>("DetalleEspecial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("NumeroVillas");
                });

            modelBuilder.Entity("MagicVilla_API.Models.cVilla", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Cost = 200.0,
                            CreationDate = new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6824),
                            Description = "Detalle de la villa.......",
                            ImageUrl = "",
                            MetrosCuadrados = 80,
                            Name = "Villa Real",
                            Ocupantes = 5,
                            UpdateDate = new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6836)
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "",
                            Cost = 250.0,
                            CreationDate = new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6840),
                            Description = "Detalle de la villa.......",
                            ImageUrl = "",
                            MetrosCuadrados = 50,
                            Name = "Premium Vista a la Piscina",
                            Ocupantes = 4,
                            UpdateDate = new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6840)
                        },
                        new
                        {
                            Id = 3,
                            Amenidad = "",
                            Cost = 100.0,
                            CreationDate = new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6843),
                            Description = "Detalle de la villa.......",
                            ImageUrl = "",
                            MetrosCuadrados = 20,
                            Name = "Villa Real La morena",
                            Ocupantes = 2,
                            UpdateDate = new DateTime(2023, 5, 2, 22, 45, 26, 202, DateTimeKind.Local).AddTicks(6844)
                        });
                });

            modelBuilder.Entity("MagicVilla_API.Models.NumeroVilla", b =>
                {
                    b.HasOne("MagicVilla_API.Models.cVilla", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
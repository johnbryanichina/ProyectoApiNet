﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Proyecto_API.Data;

#nullable disable

namespace ProyectoAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Proyecto_API.Modelos.instrumentos", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("cantidad")
                        .HasColumnType("integer");

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("fecha_actualizacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("fecha_creacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("imagen_url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("precio")
                        .HasColumnType("double precision");

                    b.HasKey("id");

                    b.ToTable("instrumentos");

                    b.HasData(
                        new
                        {
                            id = 1,
                            cantidad = 10,
                            descripcion = "Guitarra color café con cuerdas de metal",
                            fechaactualizacion = new DateTimeOffset(new DateTime(2024, 8, 9, 23, 12, 37, 65, DateTimeKind.Unspecified).AddTicks(6209), new TimeSpan(0, 0, 0, 0, 0)),
                            fechacreacion = new DateTimeOffset(new DateTime(2024, 8, 9, 23, 12, 37, 65, DateTimeKind.Unspecified).AddTicks(6204), new TimeSpan(0, 0, 0, 0, 0)),
                            imagenurl = "",
                            nombre = "Guitarra",
                            precio = 5.0
                        },
                        new
                        {
                            id = 2,
                            cantidad = 5,
                            descripcion = "Saxofon alto color negro Yamaha yas 23",
                            fechaactualizacion = new DateTimeOffset(new DateTime(2024, 8, 9, 23, 12, 37, 65, DateTimeKind.Unspecified).AddTicks(6211), new TimeSpan(0, 0, 0, 0, 0)),
                            fechacreacion = new DateTimeOffset(new DateTime(2024, 8, 9, 23, 12, 37, 65, DateTimeKind.Unspecified).AddTicks(6211), new TimeSpan(0, 0, 0, 0, 0)),
                            imagenurl = "",
                            nombre = "Saxofon",
                            precio = 10.5
                        });
                });

            modelBuilder.Entity("Proyecto_API.Modelos.numero_instrumentos", b =>
                {
                    b.Property<int>("instrumento_no")
                        .HasColumnType("integer");

                    b.Property<string>("descripcion_instrumento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("fecha_actualizacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("fecha_creacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("instrumento_id")
                        .HasColumnType("integer");

                    b.HasKey("instrumento_no");

                    b.HasIndex("instrumento_id");

                    b.ToTable("numero_instrumentos");
                });

            modelBuilder.Entity("Proyecto_API.Modelos.numero_instrumentos", b =>
                {
                    b.HasOne("Proyecto_API.Modelos.instrumentos", "instrumentos")
                        .WithMany()
                        .HasForeignKey("instrumento_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("instrumentos");
                });
#pragma warning restore 612, 618
        }
    }
}

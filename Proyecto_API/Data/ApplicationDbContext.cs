using Microsoft.EntityFrameworkCore;
using Proyecto_API.Modelos;
using System.Diagnostics.Metrics;

namespace Proyecto_API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<instrumentos> instrumentos { get; set; }
        public DbSet<numero_instrumentos> numero_instrumentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<instrumentos>().HasData(
                new instrumentos()
                {
                    id=1,
                    nombre="Guitarra",
                    descripcion= "Guitarra color café con cuerdas de metal",
                    precio=5,
                    cantidad=10,
                    imagen_url="",
                    fecha_creacion=DateTimeOffset.UtcNow,
                    fecha_actualizacion= DateTimeOffset.UtcNow,
                },
                 new instrumentos()
                 {
                     id = 2,
                     nombre = "Saxofon",
                     descripcion = "Saxofon alto color negro Yamaha yas 23",
                     precio = 10.50,
                     cantidad = 5,
                     imagen_url = "",
                     fecha_creacion = DateTimeOffset.UtcNow,
                     fecha_actualizacion = DateTimeOffset.UtcNow,
                 });
            
        }

    }
}

using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        { 
        }

        public DbSet<cVilla> Villas { get; set; }
        //Añadir datos al momento de crear
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cVilla>().HasData(
                new cVilla()
                {
                    Id = 1,
                    Name = "Villa Real",
                    Description = "Detalle de la villa.......",
                    ImageUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 80,
                    Cost = 200,
                    Amenidad = "",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,

                },
                new cVilla()
                {
                    Id = 2,
                    Name = "Premium Vista a la Piscina",
                    Description = "Detalle de la villa.......",
                    ImageUrl = "",
                    Ocupantes = 4,
                    MetrosCuadrados = 50,
                    Cost = 250,
                    Amenidad = "",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,

                },
                new cVilla()
                {
                    Id = 3,
                    Name = "Villa Real La morena",
                    Description = "Detalle de la villa.......",
                    ImageUrl = "",
                    Ocupantes = 2,
                    MetrosCuadrados = 20,
                    Cost = 100,
                    Amenidad = "",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,

                }
            );
        }
    }
}

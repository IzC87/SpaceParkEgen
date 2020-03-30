using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SpacePark
{
    public partial class SpacePark
    {
        public class MyContext : DbContext
        {
            public DbSet<ParkingSpace> ParkingSpaces { get; set; }
            public DbSet<Person> Persons { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddJsonFile($"appsettings.dev.json", optional: true);
                var config = builder.Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
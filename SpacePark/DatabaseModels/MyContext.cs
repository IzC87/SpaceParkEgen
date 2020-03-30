using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
                optionsBuilder.UseSqlServer("");
            }
        }
    }
}
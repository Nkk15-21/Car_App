using Car_App.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Car_App.Data
{
    public class Car_AppDbContext : DbContext
    {
        public Car_AppDbContext(DbContextOptions<Car_AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}

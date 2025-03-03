using DaveVentura.WebApiExtendedTemplate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DaveVentura.WebApiExtendedTemplate.Database {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Products { get; set; }
    }
}

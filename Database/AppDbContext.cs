using Microsoft.EntityFrameworkCore;
using WebApiExtendedTemplate.Domain.Models;

namespace WebApiExtendedTemplate.Database {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Products { get; set; }
    }
}

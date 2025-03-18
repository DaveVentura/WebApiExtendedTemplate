using Microsoft.EntityFrameworkCore;
using WebApiExtendedTemplate.Database;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class InMemoryRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Environment.GetEnvironmentVariable("SQL_DATABASE_NAME") ?? "database")
            );
        }
    }
}

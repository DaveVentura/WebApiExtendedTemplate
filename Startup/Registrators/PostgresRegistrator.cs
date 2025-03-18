using Microsoft.EntityFrameworkCore;
using WebApiExtendedTemplate.Database;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class PostgresRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")));
        }
    }
}

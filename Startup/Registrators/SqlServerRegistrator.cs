using DaveVentura.WebApiExtendedTemplate.Database;
using Microsoft.EntityFrameworkCore;

namespace DaveVentura.WebApiExtendedTemplate.Startup.Registrators {
    public class SqlServerRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")));
        }
    }
}

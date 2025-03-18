using Microsoft.EntityFrameworkCore;
using WebApiExtendedTemplate.Database;
using WebApiExtendedTemplate.Startup;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class MySqlRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            string? connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 41));
            builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(connectionString, serverVersion)
                );
        }
    }
}

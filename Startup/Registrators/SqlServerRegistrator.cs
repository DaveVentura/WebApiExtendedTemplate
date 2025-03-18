using Microsoft.EntityFrameworkCore;
using WebApiExtendedTemplate.Database;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class SqlServerRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")));
        }
    }
}

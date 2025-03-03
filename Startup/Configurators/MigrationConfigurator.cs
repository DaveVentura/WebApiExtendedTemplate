using DaveVentura.WebApiExtendedTemplate.Database;
using Microsoft.EntityFrameworkCore;

namespace DaveVentura.WebApiExtendedTemplate.Startup.Configurators
{
    public class MigrationConfigurator : IAppConfigurator
    {
        public int Order => 0;

        public void ConfigureApp(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }
        }
    }
}

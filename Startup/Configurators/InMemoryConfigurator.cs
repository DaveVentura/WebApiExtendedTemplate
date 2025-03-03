using DaveVentura.WebApiExtendedTemplate.Database;

namespace DaveVentura.WebApiExtendedTemplate.Startup.Configurators {
    public class InMemoryConfigurator : IAppConfigurator {
        public int Order => 0;

        public void ConfigureApp(WebApplication app) {
            using (var scope = app.Services.CreateScope()) {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            }
        }
    }
}

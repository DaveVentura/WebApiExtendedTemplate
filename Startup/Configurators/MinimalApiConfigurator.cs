using DaveVentura.WebApiExtendedTemplate.Domain.Models;
using DaveVentura.WebApiExtendedTemplate.Middlewares;
using DaveVentura.WebApiExtendedTemplate.Services;

namespace DaveVentura.WebApiExtendedTemplate.Startup.Configurators {
    public class MinimalApiConfigurator : IAppConfigurator {
        public int Order => 5000;

        public void ConfigureApp(WebApplication app) {
            app.UseValidationMiddleware();

            app.MapGet("/", () => "Hello World!");
            app.MapGet("/api", () => "OK");
        }
    }
}

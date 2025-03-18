using WebApiExtendedTemplate.Middlewares;

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class MinimalApiConfigurator : IAppConfigurator {
        public int Order => 5000;

        public void ConfigureApp(WebApplication app) {
            app.UseValidationMiddleware();

            app.MapGet("/", () => "Hello World!");
        }
    }
}

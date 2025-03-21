using System.Reflection;
using WebApiExtendedTemplate.Common;
using WebApiExtendedTemplate.Middlewares;

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class MinimalApiConfigurator : IAppConfigurator {
        public int Order => 5000;

        public void ConfigureApp(WebApplication app) {
            app.UseValidationMiddleware();

            app.MapGet(ApiRoutes.Info.ROUTE, () => {
                string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0";
                return Results.Ok(new { Version = version });
            });
        }
    }
}

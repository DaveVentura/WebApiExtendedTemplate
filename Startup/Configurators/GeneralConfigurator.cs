using WebApiExtendedTemplate.Extensions;

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class GeneralConfigurator : IAppConfigurator {
        public int Order => 1;

        public void ConfigureApp(WebApplication app) {
            app.UseErrorHandlingMiddleware();
            app.UseHttpsRedirection();
        }
    }
}

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class AuthConfigurator : IAppConfigurator {
        public int Order => 1000;

        public void ConfigureApp(WebApplication app) {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}

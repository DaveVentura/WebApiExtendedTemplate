namespace WebApiExtendedTemplate.Startup.Configurators {
    public class RunAppConfigurator : IAppConfigurator {
        public int Order => 10000;

        public void ConfigureApp(WebApplication app) => app.Run();
    }
}

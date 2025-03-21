namespace WebApiExtendedTemplate.Startup {
    public interface IAppConfigurator {
        int Order { get; }
        void ConfigureApp(WebApplication app);
    }
}

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class SwaggerConfigurator : IAppConfigurator {
        public int Order => 100;

        public void ConfigureApp(WebApplication app) {
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}

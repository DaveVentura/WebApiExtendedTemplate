using WebApiExtendedTemplate.Middlewares;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class ControllerRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder)
        => builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
    }
}

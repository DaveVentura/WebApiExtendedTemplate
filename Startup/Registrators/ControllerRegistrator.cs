using DaveVentura.WebApiExtendedTemplate.Middlewares;

namespace DaveVentura.WebApiExtendedTemplate.Startup.Registrators
{
    public class ControllerRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) 
        => builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
    }
}
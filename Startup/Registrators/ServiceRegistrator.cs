using DaveVentura.WebApiExtendedTemplate.Services;

namespace DaveVentura.WebApiExtendedTemplate.Startup.Registrators {
    public class ServiceRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            //#if(UseSql)
            builder.Services.AddScoped<PersonService>();
            //#endif
            //#if(useMongo)
            builder.Services.AddScoped<PostService>();
            //#endif
        }
    }
}

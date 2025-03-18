using WebApiExtendedTemplate.Services;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class ServiceRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            //#if(UseSql)
            builder.Services.AddScoped<PersonService>();
            //#endif
            //#if(useMongo)
            builder.Services.AddScoped<PostService>();
            //#endif
            //#if(useAzureTable)
            builder.Services.AddScoped<PublicationService>();
            //#endif

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton(provider => {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor?.HttpContext?.Request;
                string absoluteUri = string.Concat(request?.Scheme ?? "http", "://", request?.Host.ToUriComponent(), "/");
                return new UriService(absoluteUri);
            });
        }
    }
}

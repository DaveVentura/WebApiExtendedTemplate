using Microsoft.OpenApi.Models;

namespace DaveVentura.WebApiExtendedTemplate.Startup.Registrators {
    public class SwaggerRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.EnableAnnotations();
                //#if(UseAuth)
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme }
                    }, new List<string>()
                }});
                //#endif
            });
        }
    }
}

using WebApiExtendedTemplate.Mapping;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class MappingRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.AddAutoMapper(
                typeof(RequestToDomainProfile),
                typeof(DomainToResponseProfile)
            );
        }
    }
}

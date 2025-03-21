using Microsoft.AspNetCore.Mvc;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class MvcRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}

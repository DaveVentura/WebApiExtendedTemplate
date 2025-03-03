using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaveVentura.WebApiExtendedTemplate.Startup.Configurators
{
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
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class ControllerConfigurator : IAppConfigurator {
        public int Order => 5000;

        public void ConfigureApp(WebApplication app) {
            app.MapControllers();
        }
    }
}

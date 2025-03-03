using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaveVentura.WebApiExtendedTemplate.Startup
{
    public interface IAppConfigurator
    {
        int Order { get; }
        void ConfigureApp(WebApplication app);
    }
}
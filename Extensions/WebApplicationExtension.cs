using DaveVentura.WebApiExtendedTemplate.Startup;
using System.Reflection;

namespace DaveVentura.WebApiExtendedTemplate.Extensions {
    public static class WebApplicationExtension {
        public static void RegisterServicesInAssembly(this WebApplicationBuilder builder) {
            var registrators = GetAllClasses<IRegistator>();
            registrators.ForEach(registrator => registrator.RegisterServices(builder));

        }

        public static void ConfigureAppInAssembly(this WebApplication app) {
            var configurators = GetAllConfiguratorInOrder();
            configurators.ForEach(configurator => configurator.ConfigureApp(app));
        }

        private static List<T> GetAllClasses<T>() {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<T>().ToList();
        }

        private static List<IAppConfigurator> GetAllConfiguratorInOrder() {
            return [.. Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IAppConfigurator).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IAppConfigurator>()
                .OrderBy(configurator => configurator?.Order ?? 0)];
        }
    }
}

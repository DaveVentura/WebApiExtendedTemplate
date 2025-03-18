using Azure.Data.Tables;
using WebApiExtendedTemplate.Startup;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class AzureTableRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            string? connectionString = Environment.GetEnvironmentVariable("TABLE_STORAGE_CONNECTION_STRING");

            builder.Services.AddSingleton(_ =>
                new TableServiceClient(connectionString)
            );
        }
    }
}

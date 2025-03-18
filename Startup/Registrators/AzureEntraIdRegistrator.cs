using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class AzureEntraIdRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            string azureClientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID") ?? "";
            string azureTenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID") ?? "";

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidAudiences = [
                            azureClientId,
                            $"api://{azureClientId}"
                        ]
                    };
                    options.IncludeErrorDetails = true;
                },
                options => {
                    options.TenantId = azureTenantId;
                    options.ClientId = azureClientId;
                    options.Instance = "https://login.microsoftonline.com/";
                    options.DisableTelemetry = true;
                },
                "Bearer",
                true
            );
        }
    }
}

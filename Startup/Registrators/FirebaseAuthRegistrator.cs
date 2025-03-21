using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class FirebaseAuthRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            string firebaseAuthId = Environment.GetEnvironmentVariable("FIREBASE_AUTH_ID") ?? "";

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.Authority = $"https://securetoken.google.com/{firebaseAuthId}";
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidIssuer = $"https://securetoken.google.com/{firebaseAuthId}",
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = firebaseAuthId,
                        ValidateLifetime = true
                    };

                    options.IncludeErrorDetails = true;
                    options.Events = new JwtBearerEvents {
                        OnTokenValidated = async context => {
                            string? authorizationHeader = context.Request.Headers.Authorization.FirstOrDefault();
                            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ")) {
                                string firebaseToken = authorizationHeader["Bearer ".Length..].Trim();

                                var firebaseAuth = FirebaseAuth.DefaultInstance;
                                if (firebaseAuth == null) {
                                    FirebaseApp.Create(new AppOptions {
                                        Credential = GoogleCredential.FromFile("firebase-service-account.json")
                                    });
                                    firebaseAuth = FirebaseAuth.DefaultInstance;
                                }

                                try {
                                    var decodedToken = await firebaseAuth.VerifyIdTokenAsync(firebaseToken);
                                    var claimsIdentity = context?.Principal?.Identity as ClaimsIdentity;
                                    claimsIdentity?.AddClaim(new Claim(ClaimTypes.NameIdentifier, decodedToken.Uid));
                                } catch (Exception ex) {
                                    context.Fail($"Invalid Firebase Token: {ex.Message}");
                                }
                            } else {
                                context.Fail("No bearer auth header");
                            }
                        }
                    };
                });
        }
    }
}

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiExtendedTemplate.Startup.Registrators {
    public class MongoRegistrator : IRegistator {
        public void RegisterServices(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<IMongoClient>(_ => {
                var mongoSettings = MongoClientSettings.FromConnectionString(
                    Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING")
                );
                return new MongoClient(mongoSettings);
            });

            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IMongoClient>().GetDatabase(
                    Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME")
                )
            );
        }
    }
}

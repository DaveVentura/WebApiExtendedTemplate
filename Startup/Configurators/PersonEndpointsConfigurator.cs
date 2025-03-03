using DaveVentura.WebApiExtendedTemplate.Domain.Models;
using DaveVentura.WebApiExtendedTemplate.Services;
using DaveVentura.WebApiExtendedTemplate.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class PersonEndpointsConfigurator : IAppConfigurator {
        public int Order => 5001;

        public void ConfigureApp(WebApplication app) {
            app.MapGet("/api/persons", async (PersonService personService, CancellationToken cancellationToken) =>
                Results.Ok(await personService.GetAllPersonsAsync(true, cancellationToken)));

            app.MapGet("/api/persons/{id}", async (PersonService personService, long id, CancellationToken cancellationToken) => {
                var person = await personService.GetPersonByIdAsync(id, cancellationToken);
                return Results.Ok(person);
            });

            app.MapPost("/api/persons", async (PersonService personService, Person person, CancellationToken cancellationToken) => {
                await personService.CreatePersonAsync(person, true, cancellationToken);
                return Results.Created($"/api/persons/{person.Id}", person);
            });

            app.MapPut("/api/persons/{id}", async (PersonService personService, long id, Person person, CancellationToken cancellationToken) => {
                await personService.UpdatePersonAsync(id, person, true, cancellationToken);
                return Results.Ok(person);
            });

            app.MapDelete("/api/persons/{id}", async (PersonService personService, long id, CancellationToken cancellationToken) => {
                await personService.DeletePersonAsync(id, true, cancellationToken);
                return Results.NoContent();
            });
        }

    }
}

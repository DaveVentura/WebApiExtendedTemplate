using AutoMapper;
using WebApiExtendedTemplate.Common;
using WebApiExtendedTemplate.Contracts.Requests;
using WebApiExtendedTemplate.Contracts.Responses;
using WebApiExtendedTemplate.Domain.Models;
using WebApiExtendedTemplate.Services;

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class PersonEndpointsConfigurator : IAppConfigurator {
        public int Order => 5001;

        public void ConfigureApp(WebApplication app) {
            var create = app.MapPost(ApiRoutes.Persons.ROUTE,
                async (
                    PersonService personService,
                    UriService uriService,
                    IMapper mapper,
                    PersonRequest personRequest,
                    CancellationToken cancellationToken) => {
                        var person = mapper.Map<Person>(personRequest);
                        await personService.CreatePersonAsync(person, true, cancellationToken);
                        return Results.Created(
                            uriService.GetUri($"{ApiRoutes.Persons.ROUTE}/{person.Id}"),
                            mapper.Map<PersonResponse>(person)
                        );
                    });

            var getAll = app.MapGet(ApiRoutes.Persons.ROUTE,
                async (
                    PersonService personService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var persons = await personService.GetAllPersonsAsync(true, cancellationToken);
                        return Results.Ok(mapper.Map<IEnumerable<PersonResponse>>(persons));
                    });

            var getById = app.MapGet(ApiRoutes.Persons.ROUTE_BY_ID,
                async (
                    PersonService personService,
                    IMapper mapper,
                    long id,
                    CancellationToken cancellationToken) => {
                        var person = await personService.GetPersonByIdAsync(id, cancellationToken);
                        return Results.Ok(mapper.Map<PersonResponse>(person));
                    });


            var update = app.MapPut(ApiRoutes.Persons.ROUTE_BY_ID,
                async (
                    PersonService personService,
                    IMapper mapper,
                    long id,
                    PersonRequest personRequest,
                    CancellationToken cancellationToken) => {
                        var person = mapper.Map<Person>(personRequest);
                        await personService.UpdatePersonAsync(id, person, true, cancellationToken);
                        return Results.Ok(mapper.Map<PersonResponse>(person));
                    });

            var delete = app.MapDelete(ApiRoutes.Persons.ROUTE_BY_ID,
                async (
                    PersonService personService,
                    long id,
                    CancellationToken cancellationToken) => {
                        await personService.DeletePersonAsync(id, true, cancellationToken);
                        return Results.Ok();
                    });

            //#if(UseAuth)
            getAll.RequireAuthorization();
            getById.RequireAuthorization();
            create.RequireAuthorization();
            update.RequireAuthorization();
            delete.RequireAuthorization();
            //#endif
        }
    }
}

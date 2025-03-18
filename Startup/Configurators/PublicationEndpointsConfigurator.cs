using AutoMapper;
using WebApiExtendedTemplate.Common;
using WebApiExtendedTemplate.Contracts.Requests;
using WebApiExtendedTemplate.Contracts.Responses;
using WebApiExtendedTemplate.Domain.Entities;
using WebApiExtendedTemplate.Services;

namespace WebApiExtendedTemplate.Startup.Configurators {
    public class PublicationEndpointsConfigurator : IAppConfigurator {
        public int Order => 5002;

        public void ConfigureApp(WebApplication app) {
            var create = app.MapPost(ApiRoutes.Publications.ROUTE_BY_TYPE,
                async (
                    PublicationType publicationType,
                    PublicationRequest publicationRequest,
                    PublicationService publicationService,
                    UriService uriService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var publication = mapper.Map<Publication>(publicationRequest);
                        await publicationService.CreatePublicationAsync(publicationType.ToString(), publication, cancellationToken);
                        return Results.Created(
                            uriService.GetUri($"{ApiRoutes.Publications.ROUTE}/{publication.PartitionKey}/{publication.RowKey}"),
                            mapper.Map<PublicationResponse>(publication)
                        );
                    });

            var getAll = app.MapGet(ApiRoutes.Publications.ROUTE,
                async (
                    PublicationService publicationService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var publications = await publicationService.GetAllPublicationsAsync(cancellationToken);
                        return Results.Ok(mapper.Map<IEnumerable<PublicationResponse>>(publications));
                    });

            var getByType = app.MapGet(ApiRoutes.Publications.ROUTE_BY_TYPE,
                async (
                    PublicationType publicationType,
                    PublicationService publicationService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var publications = await publicationService.GetPublicationsByFilterAsync($"PartitionKey eq {publicationType}", cancellationToken);
                        return Results.Ok(mapper.Map<IEnumerable<PublicationResponse>>(publications));
                    });

            var getById = app.MapGet(ApiRoutes.Publications.ROUTE_BY_ID,
                async (
                    PublicationType publicationType,
                    string id,
                    PublicationService publicationService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var publication = await publicationService.GetPublicationByKeysAsync(publicationType.ToString(), id, cancellationToken);
                        return Results.Ok(mapper.Map<PublicationResponse>(publication));
                    });

            var update = app.MapPut(ApiRoutes.Publications.ROUTE_BY_ID,
                async (
                    PublicationType publicationType,
                    string id,
                    PublicationRequest publicationRequest,
                    PublicationService publicationService,
                    IMapper mapper,
                    CancellationToken cancellationToken) => {
                        var publication = mapper.Map<Publication>(publicationRequest);
                        await publicationService.UpdatePublicationAsync(publicationType.ToString(), id, publication, cancellationToken);
                        return Results.Ok(mapper.Map<PublicationResponse>(publication));
                    });

            var delete = app.MapDelete(ApiRoutes.Publications.ROUTE_BY_ID,
                async (
                    PublicationType publicationType,
                    string id,
                    PublicationService publicationService,
                    CancellationToken cancellationToken) => {
                        await publicationService.DeletePublicationAsync(publicationType.ToString(), id, cancellationToken);
                        return Results.Ok();
                    });

            //#if(UseAuth)
            create.RequireAuthorization();
            getAll.RequireAuthorization();
            getByType.RequireAuthorization();
            getById.RequireAuthorization();
            update.RequireAuthorization();
            delete.RequireAuthorization();
            //#endif
        }
    }
}

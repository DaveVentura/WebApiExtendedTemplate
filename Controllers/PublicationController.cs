using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExtendedTemplate.Common;
using WebApiExtendedTemplate.Contracts.Requests;
using WebApiExtendedTemplate.Contracts.Responses;
using WebApiExtendedTemplate.Domain.Entities;
using WebApiExtendedTemplate.Services;

namespace WebApiExtendedTemplate.Controllers {
    [ApiController]
    //#if(UseAuth)
    [Authorize]
    //#endif
    [Route(ApiRoutes.Publications.ROUTE)]
    public class PublicationController : CommonControllerBase {
        private readonly PublicationService _publicationService;

        public PublicationController(PublicationService publicationService, UriService uriService, IMapper mapper) : base(uriService, mapper) {
            _publicationService = publicationService;
        }

        [HttpPost(ApiRoutes.Publications.TYPE_ROUTE_PARAMETER)]
        public async Task<IActionResult> Create(
            PublicationType publicationType,
            [FromBody] PublicationRequest publicationRequest,
            CancellationToken cancellationToken) {
            var publication = Mapper.Map<Publication>(publicationRequest);
            await _publicationService.CreatePublicationAsync(publicationType.ToString(), publication, cancellationToken);
            return base.Created(
                UriService.GetUri($"{ApiRoutes.Publications.ROUTE}/{publication.PartitionKey}/{publication.RowKey}"),
                Mapper.Map<PublicationResponse>(publication)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) {
            var publications = await _publicationService.GetAllPublicationsAsync(cancellationToken);
            return base.Ok(Mapper.Map<IEnumerable<PublicationResponse>>(publications));
        }

        [HttpGet(ApiRoutes.Publications.TYPE_ROUTE_PARAMETER)]
        public async Task<IActionResult> GetByType(PublicationType publicationType, CancellationToken cancellationToken) {
            var publications = await _publicationService.GetPublicationsByFilterAsync($"PartitionKey eq {publicationType}", cancellationToken);
            return base.Ok(Mapper.Map<IEnumerable<PublicationResponse>>(publications));
        }

        [HttpGet(ApiRoutes.Publications.TYPE_AND_ID_ROUTE_PARAMETERS)]
        public async Task<IActionResult> GetPublication(PublicationType publicationType, string id, CancellationToken cancellationToken) {
            var publication = await _publicationService.GetPublicationByKeysAsync(publicationType.ToString(), id, cancellationToken);
            return base.Ok(Mapper.Map<PublicationResponse>(publication));
        }

        [HttpPut(ApiRoutes.Publications.TYPE_AND_ID_ROUTE_PARAMETERS)]
        public async Task<IActionResult> UpdatePublication(
            PublicationType publicationType,
            string id,
            [FromBody] PublicationRequest publicationRequest,
            CancellationToken cancellationToken) {
            var publication = Mapper.Map<Publication>(publicationRequest);
            await _publicationService.UpdatePublicationAsync(publicationType.ToString(), id, publication, cancellationToken);
            return base.Ok(Mapper.Map<PublicationResponse>(publication));
        }

        [HttpDelete(ApiRoutes.Publications.TYPE_AND_ID_ROUTE_PARAMETERS)]
        public async Task<IActionResult> DeletePublication(PublicationType publicationType, string id, CancellationToken cancellationToken) {
            await _publicationService.DeletePublicationAsync(publicationType.ToString(), id, cancellationToken);
            return base.Ok();
        }
    }
}

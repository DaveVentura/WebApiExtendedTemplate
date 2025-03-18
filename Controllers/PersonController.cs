using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiExtendedTemplate.Common;
using WebApiExtendedTemplate.Contracts.Requests;
using WebApiExtendedTemplate.Contracts.Responses;
using WebApiExtendedTemplate.Domain.Models;
using WebApiExtendedTemplate.Services;


namespace WebApiExtendedTemplate.Controllers {
    [ApiController]
    //#if(UseAuth)
    [Authorize]
    //#endif
    [Route(ApiRoutes.Persons.ROUTE)]
    public class PersonController : CommonControllerBase {
        private readonly PersonService _personService;

        public PersonController(PersonService personService, UriService uriService, IMapper mapper) : base(uriService, mapper) {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonRequest personRequest, CancellationToken cancellationToken) {
            var person = Mapper.Map<Person>(personRequest);
            await _personService.CreatePersonAsync(person, true, cancellationToken);
            return base.Created(
                UriService.GetUri($"{ApiRoutes.Persons.ROUTE}/{person.Id}"),
                Mapper.Map<PersonResponse>(person));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) {
            var persons = await _personService.GetAllPersonsAsync(true, cancellationToken);
            return base.Ok(Mapper.Map<IEnumerable<PersonResponse>>(persons));
        }

        [HttpGet(ApiRoutes.ID_ROUTE_PARAMETER)]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken) {

            var person = await _personService.GetPersonByIdAsync(id, cancellationToken);
            return base.Ok(Mapper.Map<PersonResponse>(person));
        }

        [HttpPut(ApiRoutes.ID_ROUTE_PARAMETER)]
        public async Task<IActionResult> Update(long id, [FromBody] PersonRequest personRequest, CancellationToken cancellationToken) {
            var person = Mapper.Map<Person>(personRequest);
            await _personService.UpdatePersonAsync(id, person, true, cancellationToken);
            return base.Ok(Mapper.Map<PersonResponse>(person));
        }

        [HttpDelete(ApiRoutes.ID_ROUTE_PARAMETER)]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken) {
            await _personService.DeletePersonAsync(id, true, cancellationToken);
            return base.Ok();
        }
    }
}

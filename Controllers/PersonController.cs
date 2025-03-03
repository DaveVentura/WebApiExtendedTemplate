using AutoMapper;
using DaveVentura.WebApiExtendedTemplate.Constants;
using DaveVentura.WebApiExtendedTemplate.Contracts.Requests;
using DaveVentura.WebApiExtendedTemplate.Contracts.Responses;
using DaveVentura.WebApiExtendedTemplate.Domain.Models;
using DaveVentura.WebApiExtendedTemplate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DaveVentura.WebApiExtendedTemplate.Controllers {
    [ApiController]
    //#if(UseAuth)
    [Authorize]
    //#endif
    [Route(ApiRoutes.Persons.ROUTE)]
    public class PersonController : CommonController {
        private readonly PersonService _personService;

        public PersonController(PersonService personService, IMapper mapper) : base(mapper) {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonRequest personRequest, CancellationToken cancellationToken) {
            var person = Mapper.Map<Person>(personRequest);
            await _personService.CreatePersonAsync(person, true, cancellationToken);
            return base.Created($"/api/persons/{person.Id}", Mapper.Map<PersonResponse>(person));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) {
            var persons = await _personService.GetAllPersonsAsync(true, cancellationToken);
            return base.Ok(Mapper.Map<IEnumerable<PersonResponse>>(persons));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken) {

            var person = await _personService.GetPersonByIdAsync(id, cancellationToken);
            return base.Ok(Mapper.Map<PersonResponse>(person));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] PersonRequest personRequest, CancellationToken cancellationToken) {
            var person = Mapper.Map<Person>(personRequest);
            await _personService.UpdatePersonAsync(id, person, true, cancellationToken);
            return base.Ok(Mapper.Map<PersonResponse>(person));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken) {
            await _personService.DeletePersonAsync(id, true, cancellationToken);
            return base.Ok();
        }
    }
}

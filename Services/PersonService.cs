using DaveVentura.WebApiExtendedTemplate.Database;
using DaveVentura.WebApiExtendedTemplate.Domain.Models;
using DaveVentura.WebApiExtendedTemplate.Services.Abstracts;
using System.Linq.Expressions;

namespace DaveVentura.WebApiExtendedTemplate.Services {
    public class PersonService : EfCoreDataProvider<Person, long> {
        public PersonService(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Person> GetPersonByIdAsync(long id, CancellationToken cancellationToken)
            => await base.GetByIdAsync(id, cancellationToken);

        public async Task<IEnumerable<Person>> GetPersonByPredicateAsync(
            Expression<Func<Person, bool>> predicate,
            bool isReadonly,
            CancellationToken cancellationToken
        ) => await base.GetByPredicateAsync(predicate, isReadonly, cancellationToken);

        public async Task<IEnumerable<Person>> GetAllPersonsAsync(
            bool isReadonly,
            CancellationToken cancellationToken
            ) => await base.GetAllAsync(isReadonly, cancellationToken);

        public async Task CreatePersonAsync(Person person, bool doSave, CancellationToken cancellationToken)
        => await base.CreateAsync(person, doSave, cancellationToken);

        public async Task UpdatePersonAsync(long id, Person person, bool doSave, CancellationToken cancellationToken)
        => await base.UpdateAsync(id, person, doSave, cancellationToken);

        public async Task DeletePersonAsync(long id, bool doSave, CancellationToken cancellationToken)
        => await base.DeleteAsync(id, doSave, cancellationToken);

        public async Task RemovePerson(Expression<Func<Person, bool>> predicate, bool doSave, CancellationToken cancellationToken)
        => await base.RemoveMany(predicate, doSave, cancellationToken);
    }
}

using DaveVentura.WebApiExtendedTemplate.Domain.Abstracts;
using DaveVentura.WebApiExtendedTemplate.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DaveVentura.WebApiExtendedTemplate.Services.Abstracts {
    public abstract class MongoDataProvider<TModel> where TModel : MongoDocumentBase {
        private protected readonly IMongoCollection<TModel> Collection;

        public MongoDataProvider(IMongoDatabase database, string collectionName) {
            Collection = database.GetCollection<TModel>(collectionName);
        }

        private protected virtual async Task<TModel> GetByIdAsync(string id, CancellationToken cancellationToken) {
            try {
                var filter = Builders<TModel>.Filter.Eq(x => x.Id, id);
                var model = await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

                return model ?? throw new DatabaseException(
                    $"{typeof(TModel).Name} with the id '{id}' was not found.",
                    StatusCodes.Status404NotFound
                );
            } catch (FormatException ex) {
                throw new DatabaseException(ex.Message, StatusCodes.Status412PreconditionFailed);
            }
        }

        private protected virtual async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken)
            => await Collection.Find(_ => true).ToListAsync(cancellationToken);

        private protected virtual async Task<IEnumerable<TModel>> GetByPredicateAsync(
            Expression<Func<TModel, bool>> predicate,
            CancellationToken cancellationToken)
            => await Collection.Find(predicate).ToListAsync(cancellationToken);

        private protected virtual async Task CreateAsync(
            TModel itemToCreate,
            CancellationToken cancellationToken) {
            itemToCreate.Id = ObjectId.GenerateNewId().ToString();
            await Collection.InsertOneAsync(itemToCreate, cancellationToken: cancellationToken);
        }

        private protected virtual async Task UpdateAsync(
            string id,
            TModel itemToUpdate,
            CancellationToken cancellationToken) {
            itemToUpdate.Id = id;
            var filter = Builders<TModel>.Filter.Eq(x => x.Id, id);
            var result = await Collection.ReplaceOneAsync(filter, itemToUpdate, cancellationToken: cancellationToken);

            if (result.MatchedCount == 0) {
                throw new DatabaseException(
                    $"{typeof(TModel).Name} with the id '{id}' was not found.",
                    StatusCodes.Status404NotFound
                );
            }
        }

        private protected virtual async Task DeleteAsync(
            string id,
            CancellationToken cancellationToken) {
            var filter = Builders<TModel>.Filter.Eq(x => x.Id, id);
            var result = await Collection.DeleteOneAsync(filter, cancellationToken);

            if (result.DeletedCount == 0) {
                throw new DatabaseException(
                    $"{typeof(TModel).Name} with the id '{id}' was not found.",
                    StatusCodes.Status404NotFound
                );
            }
        }

        private protected virtual async Task RemoveMany(
            Expression<Func<TModel, bool>> predicate,
            CancellationToken cancellationToken)
            => await Collection.DeleteManyAsync(predicate, cancellationToken);
    }
}

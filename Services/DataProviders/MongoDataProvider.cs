using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using WebApiExtendedTemplate.Domain.Abstracts;
using WebApiExtendedTemplate.Exceptions;

namespace WebApiExtendedTemplate.Services.DataProviders {
    public abstract class MongoDataProvider<TDocument> where TDocument : MongoDocumentBase {
        private protected readonly IMongoCollection<TDocument> Collection;

        public MongoDataProvider(IMongoDatabase database, string collectionName) {
            Collection = database.GetCollection<TDocument>(collectionName);
        }

        private protected virtual async Task<TDocument> GetByIdAsync(string id, CancellationToken cancellationToken) {
            try {
                var filter = Builders<TDocument>.Filter.Eq(x => x.Id, id);
                var model = await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

                return model ?? throw new DatabaseException(
                    $"{typeof(TDocument).Name} with the id '{id}' was not found.",
                    StatusCodes.Status404NotFound
                );
            } catch (FormatException ex) {
                throw new DatabaseException(ex.Message, StatusCodes.Status412PreconditionFailed);
            }
        }

        private protected virtual async Task<IEnumerable<TDocument>> GetAllAsync(CancellationToken cancellationToken)
            => await Collection.Find(_ => true).ToListAsync(cancellationToken);

        private protected virtual async Task<IEnumerable<TDocument>> GetByFilterAsync(
            FilterDefinition<TDocument> filter,
            CancellationToken cancellationToken)
            => await Collection.Find(filter).ToListAsync(cancellationToken);

        private protected virtual async Task CreateAsync(
            TDocument itemToCreate,
            CancellationToken cancellationToken) {
            itemToCreate.Id = ObjectId.GenerateNewId().ToString();
            await Collection.InsertOneAsync(itemToCreate, cancellationToken: cancellationToken);
        }

        private protected virtual async Task UpdateAsync(
            string id,
            TDocument itemToUpdate,
            CancellationToken cancellationToken) {
            itemToUpdate.Id = id;
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, id);
            var result = await Collection.ReplaceOneAsync(filter, itemToUpdate, cancellationToken: cancellationToken);

            if (result.MatchedCount == 0) {
                throw new DatabaseException(
                    $"{typeof(TDocument).Name} with the id '{id}' was not found.",
                    StatusCodes.Status404NotFound
                );
            }
        }

        private protected virtual async Task DeleteAsync(
            string id,
            CancellationToken cancellationToken) {
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, id);
            var result = await Collection.DeleteOneAsync(filter, cancellationToken);

            if (result.DeletedCount == 0) {
                throw new DatabaseException(
                    $"{typeof(TDocument).Name} with the id '{id}' was not found.",
                    StatusCodes.Status404NotFound
                );
            }
        }

        private protected virtual async Task RemoveMany(
            Expression<Func<TDocument, bool>> predicate,
            CancellationToken cancellationToken)
            => await Collection.DeleteManyAsync(predicate, cancellationToken);
    }
}

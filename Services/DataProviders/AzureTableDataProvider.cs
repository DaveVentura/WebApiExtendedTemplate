using Azure;
using Azure.Data.Tables;
using WebApiExtendedTemplate.Domain.Abstracts;
using WebApiExtendedTemplate.Exceptions;

namespace WebApiExtendedTemplate.Services.DataProviders {
    public abstract class AzureTableDataProvider<TEntity> where TEntity : TableEntityBase {
        private protected readonly TableClient TableClient;

        public AzureTableDataProvider(TableServiceClient serviceClient, string tableName) {
            TableClient = serviceClient.GetTableClient(tableName);
            TableClient.CreateIfNotExists();
        }

        private protected virtual async Task<TEntity> GetByKeysAsync(string partitionKey, string rowKey, CancellationToken cancellationToken) {
            var response = await TableClient.GetEntityAsync<TEntity>(partitionKey, rowKey, cancellationToken: cancellationToken);
            return response.Value ?? throw new DatabaseException(
                $"{typeof(TEntity).Name} with the keys '{partitionKey}', '{rowKey}' was not found.",
                StatusCodes.Status404NotFound
            );
        }

        private protected virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) {
            var results = new List<TEntity>();
            await foreach (var entity in TableClient.QueryAsync<TEntity>(cancellationToken: cancellationToken)) {
                results.Add(entity);
            }
            return results;
        }

        private protected virtual async Task<IEnumerable<TEntity>> GetByFilterAsync(string filter, CancellationToken cancellationToken) {
            var results = new List<TEntity>();
            await foreach (var entity in TableClient.QueryAsync<TEntity>(filter, cancellationToken: cancellationToken)) {
                results.Add(entity);
            }
            return results;
        }

        private protected virtual async Task CreateAsync(TEntity itemToCreate, CancellationToken cancellationToken)
        => await TableClient.AddEntityAsync(itemToCreate, cancellationToken);

        private protected virtual async Task UpdateAsync(TEntity itemToUpdate, CancellationToken cancellationToken) {
            var response = await TableClient.UpdateEntityAsync(itemToUpdate, ETag.All, TableUpdateMode.Merge, cancellationToken);
            if (response.IsError) {
                throw new DatabaseException(
                    $"{typeof(TEntity).Name} with keys '{itemToUpdate.PartitionKey}', '{itemToUpdate.RowKey}' was not found.",
                    StatusCodes.Status404NotFound
                );
            }
        }

        private protected virtual async Task DeleteAsync(string partitionKey, string rowKey, CancellationToken cancellationToken)
        => await TableClient.DeleteEntityAsync(partitionKey, rowKey, ETag.All, cancellationToken);
    }
}

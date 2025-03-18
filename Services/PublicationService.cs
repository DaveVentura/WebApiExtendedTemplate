using Azure.Data.Tables;
using WebApiExtendedTemplate.Domain.Entities;
using WebApiExtendedTemplate.Services.DataProviders;

namespace WebApiExtendedTemplate.Services {
    public class PublicationService : AzureTableDataProvider<Publication> {
        public PublicationService(TableServiceClient serviceClient) : base(serviceClient, "Publications") { }

        public async Task<Publication> GetPublicationByKeysAsync(string partitionKey, string rowKey, CancellationToken cancellationToken)
        => await base.GetByKeysAsync(partitionKey, rowKey, cancellationToken);

        public async Task<IEnumerable<Publication>> GetPublicationsByFilterAsync(string filter, CancellationToken cancellationToken)
        => await base.GetByFilterAsync(filter, cancellationToken);

        public async Task<IEnumerable<Publication>> GetAllPublicationsAsync(CancellationToken cancellationToken)
        => await base.GetAllAsync(cancellationToken);

        public async Task CreatePublicationAsync(string partitionKey, Publication publicationToCreate, CancellationToken cancellationToken) {
            publicationToCreate.PartitionKey = partitionKey;
            publicationToCreate.RowKey = Guid.NewGuid().ToString();
            await base.CreateAsync(publicationToCreate, cancellationToken);
        }

        public async Task UpdatePublicationAsync(string partitionKey, string rowKey, Publication publicationToUpdate, CancellationToken cancellationToken) {
            publicationToUpdate.PartitionKey = partitionKey;
            publicationToUpdate.RowKey = rowKey;
            await base.UpdateAsync(publicationToUpdate, cancellationToken);
        }

        public async Task DeletePublicationAsync(string partitionKey, string rowKey, CancellationToken cancellationToken)
        => await base.DeleteAsync(partitionKey, rowKey, cancellationToken);
    }
}

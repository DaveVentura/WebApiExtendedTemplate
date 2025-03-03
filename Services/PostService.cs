using DaveVentura.WebApiExtendedTemplate.Domain.Documents;
using DaveVentura.WebApiExtendedTemplate.Services.Abstracts;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DaveVentura.WebApiExtendedTemplate.Services {
    public class PostService : MongoDataProvider<Post> {
        public PostService(IMongoDatabase database) : base(database, "posts") { }

        public async Task<Post> GetPostByIdAsync(string id, CancellationToken cancellationToken)
            => await base.GetByIdAsync(id, cancellationToken);

        public async Task<IEnumerable<Post>> GetPostsByPredicate(Expression<Func<Post, bool>> predicate, CancellationToken cancellationToken)
        => await base.GetByPredicateAsync(predicate, cancellationToken);

        public async Task<IEnumerable<Post>> GetAllPostsAsync(CancellationToken cancellationToken)
            => await base.GetAllAsync(cancellationToken);

        public async Task CreatePostAsync(Post post, CancellationToken cancellationToken) {
            var now = DateTime.Now;
            post.CreatedAt = now;
            post.UpdatedAt = now;
            await base.CreateAsync(post, cancellationToken);
        }
        public async Task UpdatePostAsync(string id, Post post, CancellationToken cancellationToken) {
            post.UpdatedAt = DateTime.Now;
            await base.UpdateAsync(id, post, cancellationToken);
        }

        public async Task DeletePostAsync(string id, CancellationToken cancellationToken)
            => await base.DeleteAsync(id, cancellationToken);
    }
}

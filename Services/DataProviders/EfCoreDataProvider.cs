//#if(UseSqlServer)
using Microsoft.Data.SqlClient;
//#endif
using Microsoft.EntityFrameworkCore;
//#if(UsePostgres)
using Npgsql;
//#endif
using System.Linq.Expressions;
using WebApiExtendedTemplate.Database;
using WebApiExtendedTemplate.Domain.Abstracts;
using WebApiExtendedTemplate.Exceptions;

namespace WebApiExtendedTemplate.Services.DataProviders {
    public abstract class EfCoreDataProvider<TModel, TKey> where TModel : ModelBase<TKey> {
        private protected AppDbContext DbContext;
        private protected readonly DbSet<TModel> Set;

        public EfCoreDataProvider(
            AppDbContext dbContext
        ) {
            this.DbContext = dbContext;
            this.Set = dbContext.Set<TModel>();
        }

        private protected virtual async Task<TModel> GetByIdAsync(
            TKey id,
            CancellationToken cancellationToken
            ) {
            var model = await Set.FindAsync([id], cancellationToken);

            return model ?? throw new DatabaseException(
                $"{typeof(TModel).Name} with the id '{id}' was not found.",
                StatusCodes.Status404NotFound
            );
        }

        private protected virtual async Task<IEnumerable<TModel>> GetAllAsync(
            bool isReadonly,
            CancellationToken cancellationToken,
            params Expression<Func<TModel, object>>[] includeChildren
            ) {
            var query = isReadonly ? Set.AsNoTracking() : Set.AsQueryable();

            foreach (var include in includeChildren) {
                query = query.Include(include).AsSplitQuery();
            }

            return await query.ToListAsync(cancellationToken);
        }

        private protected virtual async Task<IEnumerable<TModel>> GetByPredicateAsync(
            Expression<Func<TModel, bool>> predicate,
            bool isReadonly,
            CancellationToken cancellationToken,
            params Expression<Func<TModel, object>>[] includeChildren
            ) {
            var query = isReadonly ? Set.AsNoTracking() : Set.AsQueryable();

            foreach (var include in includeChildren) {
                query = query.Include(include).AsSplitQuery();
            }

            return await query.Where(predicate).ToListAsync(cancellationToken);
        }
        private protected virtual async Task CreateAsync(
            TModel itemToCreate,
            bool doSave,
            CancellationToken cancellationToken) {
            await Set.AddAsync(itemToCreate, cancellationToken);
            if (doSave) {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }
        private protected virtual async Task UpdateAsync(
            TKey id,
            TModel itemToUpdate,
            bool doSave,
            CancellationToken cancellationToken) {
            var existingItem = await this.GetByIdAsync(id, cancellationToken);
            DbContext.Entry(existingItem).CurrentValues.SetValues(itemToUpdate);

            if (doSave) {
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }
        private protected virtual async Task DeleteAsync(
            TKey id,
            bool doSave,
            CancellationToken cancellationToken) {
            try {
                var itemToRemove = await this.GetByIdAsync(id, cancellationToken);
                Set.Remove(itemToRemove);
                if (doSave) {
                    await DbContext.SaveChangesAsync(cancellationToken);
                }
            } catch (DbUpdateException ex) {
                if (this.IsForeignKeyViolation(ex)) {
                    throw new DatabaseException(
                        $"{typeof(TModel).Name} cannot be deleted, because it has child data.",
                        StatusCodes.Status412PreconditionFailed
                    );
                }
            }
        }

        private protected virtual async Task RemoveMany(
            Expression<Func<TModel, bool>> predicate,
            bool doSave,
            CancellationToken cancellationToken
        ) {
            try {
                var itemsToRemove = await this.GetByPredicateAsync(predicate, false, cancellationToken);
                Set.RemoveRange(itemsToRemove);
                if (doSave) {
                    await this.DbContext.SaveChangesAsync(cancellationToken);
                }
            } catch (DbUpdateException ex) {
                if (this.IsForeignKeyViolation(ex)) {
                    throw new DatabaseException(
                        $"{typeof(TModel).Name} cannot be deleted, because it has child data.",
                        StatusCodes.Status412PreconditionFailed
                    );
                }
            }
        }

        private bool IsForeignKeyViolation(DbUpdateException ex) {
            var inner = ex.InnerException;

            if (inner == null) {
                return false;
            }
            //#if(UsePostgres)
            if (inner is PostgresException pex && pex.SqlState == "23503") {
                return true;
            }
            //#endif
            //#if(UseSqlServer)
            if (inner is SqlException sex && sex.Number == 547) {
                return true;
            }
            //#endif

            if (inner.Message.Contains("Cannot delete or update a parent row")) {
                return true;
            }

            return false;
        }
    }
}

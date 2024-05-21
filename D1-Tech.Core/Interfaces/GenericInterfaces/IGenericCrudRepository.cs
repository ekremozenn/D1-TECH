using D1_Tech.Core.Dtos.Constants;
using D1_Tech.Core.Models.CommonEntity;
using System.Linq.Expressions;

namespace D1_Tech.Core.Interfaces.GenericInterfaces
{
    public interface IGenericCrudRepository<TEntity> where TEntity : BaseEntity
    {

        Task InsertManyEntity(List<TEntity> entity);
        Expression<Func<TEntity, bool>> NonDeleted();
        Task UpdateMany(List<TEntity> entities, bool deleted = false, bool avoidToDeleteCreatedDateAndBy = false);
        Task DeleteManyEntity(List<long?> ids);
        Task<TEntity> GetByIdAsNoTracking(long id);
        IQueryable<TEntity> GetAll(int? skip = null, int? take = null, string orderedColumn = "Id", string orderType = EntityOrderType.OrderAsc, params Func<IQueryable<TEntity>, IQueryable<TEntity>>?[] whereQueries);
        IQueryable<TEntity> GetAllAsNoTracking(int? skip = null, int? take = null, string orderedColumn = "Id", string orderType = EntityOrderType.OrderAsc, params Func<IQueryable<TEntity>, IQueryable<TEntity>>?[] whereQueries);
        Task<int> GetTotalCount(params Func<IQueryable<TEntity>, IQueryable<TEntity>>?[] whereQueries);
        Task InsertManyWithoutTransaction(List<TEntity> entity);
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task DeleteManyWithoutTransaction(List<long?> ids);
        Task UpdateManyWithoutTransaction(List<TEntity> entities, bool deleted = false, bool avoidToDeleteCreatedDateAndBy = false);
        Task UpdateWithoutTransaction(TEntity entity, bool deleted = false);
        Task Delete(long id, bool deleted = false);
        Task<TEntity> Insert(TEntity entity);
        Task DeleteWithoutTransaction(long id);

    }
}

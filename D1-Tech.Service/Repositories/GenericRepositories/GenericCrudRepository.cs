using D1_Tech.Core.Models.CommonEntity;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using D1_Tech.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using D1_Tech.Core.Dtos.Constants;
using D1_Tech.Core.Interfaces.GenericInterfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace D1_Tech.Service.Repositories.GenericRepositories
{
    public class GenericCrudRepository<TEntity> : IGenericCrudRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly TechDB_Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericCrudRepository(TechDB_Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task DeleteManyEntity(List<long?> ids)
        {
            List<TEntity> entities = await this.GetByManyId(ids);

            foreach (var item in entities)
            {
                item.DeleteDate = DateTime.Now;
                item.Deleted = 1;
                item.DeletedBy = (int?)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");
            }
            await this.UpdateMany(entities, true);

        }

        public IQueryable<TEntity> GetAll(int? skip = null, int? take = null, string orderedColumn = "Id", string orderType = EntityOrderType.OrderAsc, params Func<IQueryable<TEntity>, IQueryable<TEntity>>?[] whereQueries)
        {
            var entitiesQuery = GetAllQuery(whereQueries).OrderBy((String.IsNullOrEmpty(orderedColumn) ? "Id" : orderedColumn) + " " + (String.IsNullOrEmpty(orderType) ? "ASC" : orderType));

            if (skip != null)
            {
                entitiesQuery = entitiesQuery.Skip((int)skip);
            }

            if (take != null)
            {
                entitiesQuery = entitiesQuery.Take((int)take);
            }
            return entitiesQuery;
        }

        public async Task<TEntity> GetByIdAsNoTracking(long id)
        {
            var entityQuery = _context.Set<TEntity>().Where(NonDeleted())
                                                                      .AsSplitQuery().AsNoTracking()
                                                                      .Where(x => x.Id == id);
            return await entityQuery.FirstUncommitedAsync();
        }
        public async Task InsertManyEntity(List<TEntity> entity)
        {
            foreach (var item in entity)
            {
                item.CreatedDate = DateTime.Now;
                item.CreatedBy = _httpContextAccessor.HttpContext == null ? null : Convert.ToInt64(_httpContextAccessor.HttpContext.Items["UserId"]);
            }

            await _context.Set<TEntity>().AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }
        public Expression<Func<TEntity, bool>> NonDeleted()
        {
            return tEntity => tEntity.Deleted != 1;
        }
        public async Task UpdateMany(List<TEntity> entities, bool deleted = false, bool avoidToDeleteCreatedDateAndBy = true)
        {
            if (!deleted)
            {
                //var createdBy = (long)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");
                foreach (var entity in entities)
                {
                    if (!avoidToDeleteCreatedDateAndBy)
                    {
                        TEntity _baseentity = await NoneAsyncGetByIdAsNoTracking(entity.Id);
                        entity.CreatedDate = _baseentity.CreatedDate;
                        entity.CreatedBy = _baseentity.CreatedBy;
                    }

                    entity.UpdatedDate = DateTime.Now;
                    //entity.UpdatedBy = createdBy;
                }
            }

            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
        private IQueryable<TEntity> GetAllQuery(params Func<IQueryable<TEntity>, IQueryable<TEntity>>?[] whereQueries)
        {
            var getAllQuery = _context.Set<TEntity>()
                .Where(NonDeleted());

            foreach (var query in whereQueries)
            {
                if (query != null)
                {
                    getAllQuery = query(getAllQuery);
                }
            }
            return getAllQuery;
        }
        public async Task<List<TEntity>> GetByManyId(List<long?> ids, int? skip = null, int? take = null,
            string orderedColumn = "Id", string orderType = EntityOrderType.OrderAsc)
        {
            return await PrivateGetByManyId(ids, skip, take, orderedColumn, orderType, false);
        }
        private async Task<List<TEntity>> PrivateGetByManyId(List<long?> ids, int? skip, int? take, string orderedColumn, string orderType, bool asNoTracking)
        {
            var tEntityQuery = _context.Set<TEntity>()
                .AsSplitQuery()
                .Where(x => ids.Contains((int)x.Id))
                .Where(NonDeleted())
                .OrderBy((String.IsNullOrEmpty(orderedColumn) ? "Id" : orderedColumn) + " " + (String.IsNullOrEmpty(orderType) ? "ASC" : orderType));

            if (asNoTracking)
            {
                tEntityQuery = tEntityQuery.AsNoTracking();
            }

            if (skip != null)
            {
                tEntityQuery = tEntityQuery.Skip((int)skip);
            }

            if (take != null)
            {
                tEntityQuery = tEntityQuery.Take((int)take);
            }

            List<TEntity> tEntity = await tEntityQuery.ToListUncommitedAsync();
            return tEntity;
        }
        private async Task<TEntity> NoneAsyncGetByIdAsNoTracking(long id) => await _context.Set<TEntity>()
           .Where(NonDeleted())
           .AsSplitQuery().AsNoTracking()
           .FirstOrDefaultUncommitedAsync(x => x.Id == id);
        public IQueryable<TEntity> GetAllAsNoTracking(int? skip = null, int? take = null, string orderedColumn = "Id", string orderType = EntityOrderType.OrderAsc, params Func<IQueryable<TEntity>, IQueryable<TEntity>>?[] whereQueries)
        {
            return this.GetAll(skip, take, orderedColumn, orderType, whereQueries).AsSplitQuery().AsNoTracking();
        }
        public async Task<int> GetTotalCount(params Func<IQueryable<TEntity>, IQueryable<TEntity>>?[] whereQueries)
        {
            return await GetAllQuery(whereQueries).AsSplitQuery().AsNoTracking().CountUncommitedAsync();
        }

        public async Task InsertManyWithoutTransaction(List<TEntity> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    item.CreatedDate = DateTime.Now;
                    //item.CreatedBy = (long)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");
                }

                await _context.AddRangeAsync(entity);

            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
            }

        }
        public async Task CommitTransactionAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.RollbackTransactionAsync();

            }
        }

        public async Task DeleteManyWithoutTransaction(List<long?> ids)
        {

            List<TEntity> entities = await this.GetByManyId(ids);

            foreach (var item in entities)
            {
                item.DeleteDate = DateTime.Now;
                item.Deleted = 1;
                //item.DeletedBy = (int?)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");
            }
            try
            {
                //ChangeTracker();
                await UpdateManyWithoutTransaction(entities, true);
                //_context.Set<TEntity>().RemoveRange(entities);
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
            }
        }

        public async Task UpdateManyWithoutTransaction(List<TEntity> entities, bool deleted = false, bool avoidToDeleteCreatedDateAndBy = true)
        {
            if (!deleted)
            {
                //var createdBy = (long)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");

                foreach (var entity in entities)
                {
                    if (entity.Id == 0)
                    {
                        entity.CreatedDate = DateTime.Now;
                        //entity.CreatedBy = createdBy;
                    }
                    else
                    {
                        if (!avoidToDeleteCreatedDateAndBy)
                        {
                            TEntity _baseentity = await NoneAsyncGetByIdAsNoTracking(entity.Id);
                            entity.CreatedDate = _baseentity.CreatedDate;
                            entity.CreatedBy = _baseentity.CreatedBy;
                        }

                        entity.UpdatedDate = DateTime.Now;
                        //entity.UpdatedBy = createdBy;
                    }
                }
            }
            try
            {
                _context.Set<TEntity>().UpdateRange(entities);
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
            }
        }

        public async Task UpdateWithoutTransaction(TEntity entity, bool deleted = false)
        {
            try
            {
                if (!deleted)
                {
                    entity.UpdatedDate = DateTime.Now;
                    //entity.UpdatedBy = (long)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");
                }
                _context.Set<TEntity>().Update(entity);
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
            }

        }
        public void ChangeTracker()
        {
            _context.ChangeTracker.Clear();
        }
        private async Task<TEntity> NoneAsyncGetById(long id) => await _context.Set<TEntity>()
    .Where(NonDeleted())
    .AsSplitQuery()
    .FirstOrDefaultUncommitedAsync(x => x.Id == id);
        public async Task Update(TEntity entity)
        {
            //   TransactionControl(_transaction);

            TEntity _baseentity = await NoneAsyncGetByIdAsNoTracking(entity.Id);

            entity.CreatedDate = _baseentity.CreatedDate;
            entity.CreatedBy = _baseentity.CreatedBy;
            entity.UpdatedDate = DateTime.Now;
            //entity.UpdatedBy = (long)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");


            // _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            //  await _transaction.CommitAsync();

        }
        public async Task Delete(long id, bool deleted = false)
        {
            //  TransactionControl(_transaction);
            TEntity entity = await NoneAsyncGetById(id);
            if (entity == null) return;

            entity.Deleted = 1;
            entity.DeleteDate = DateTime.Now;
            //entity.DeletedBy = (int?)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");

            if (deleted)
            {
                ChangeTracker();
            }

            await this.Update(entity);
            // _transaction.Commit();

        }
        public async Task<TEntity> Insert(TEntity entity)
        {
            // TransactionControl(_transaction);
            entity.CreatedDate = DateTime.Now;
            //entity.CreatedBy = (long)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");
            EntityEntry x = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            // await _transaction.CommitAsync();

            return (TEntity)x.Entity;
        }

        public async Task DeleteWithoutTransaction(long id)
        {
            try
            {
                TEntity entity = await NoneAsyncGetById(id);
                entity.Deleted = 1;
                entity.DeleteDate = DateTime.Now;
                //entity.DeletedBy = (int?)Convert.ToInt32(_httpContextAccessor.HttpContext.Items["UserId"].ToString() ?? "1");
                ChangeTracker();
                await this.UpdateWithoutTransaction(entity, true);
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
            }
        }
    }
}
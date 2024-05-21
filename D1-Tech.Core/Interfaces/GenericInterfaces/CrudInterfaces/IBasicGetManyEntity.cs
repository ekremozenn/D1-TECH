using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicGetManyEntity<TInputEntity, TResponseEntity, TEntity>
    {
        Task<GenericResponseDto<TResponseEntity>> GetManyEntity(GenericInputDto<TInputEntity> tEntity, Func<IQueryable<TEntity>, IQueryable<TEntity>>? query = null);
    }

}

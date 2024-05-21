using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicGetAllEntity<TInputEntity, TResponseEntity, TEntity>
    {
        Task<GenericResponseDto<TResponseEntity>> GetAllEntity(GenericInputDto<TInputEntity> tEntity, Func<IQueryable<TEntity>, IQueryable<TEntity>>? query = null);

    }
}

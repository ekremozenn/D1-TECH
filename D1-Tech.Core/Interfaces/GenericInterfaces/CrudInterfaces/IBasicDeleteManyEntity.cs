using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicDeleteManyEntity<TEntity, TResponseEntity>
    {
        Task<GenericResponseDto<TResponseEntity>> DeleteManyEntity(GenericInputDto<List<TEntity>>? tEntities);
    }
}

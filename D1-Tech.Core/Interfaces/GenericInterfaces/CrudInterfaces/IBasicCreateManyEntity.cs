using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicCreateManyEntity<TEntity, TResponseEntity>
    {
        Task<GenericResponseDto<TResponseEntity>> CreateManyEntity(GenericInputDto<List<TEntity>> tEntities);
    }
}

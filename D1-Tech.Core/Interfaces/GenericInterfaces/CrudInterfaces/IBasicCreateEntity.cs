using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicCreateEntity<TInputEntity,TResponseEntity>
    {
        Task<GenericResponseDto<TResponseEntity>> CreateEntity(GenericInputDto<TInputEntity> tEntity);

    }
}

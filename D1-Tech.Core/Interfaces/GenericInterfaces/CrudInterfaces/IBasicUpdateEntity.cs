using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicUpdateEntity<TInputEntity, TResponseEntity>
    {
        Task<GenericResponseDto<TResponseEntity>> UpdateEntity(GenericInputDto<TInputEntity> tEntity);

    }
}

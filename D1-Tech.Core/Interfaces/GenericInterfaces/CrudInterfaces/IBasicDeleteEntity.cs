using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicDeleteEntity<TInputEntity, TResponseEntity>
    {
        Task<GenericResponseDto<TResponseEntity>> DeleteEntity(GenericInputDto<TInputEntity>? genericInputDto);

    }
}

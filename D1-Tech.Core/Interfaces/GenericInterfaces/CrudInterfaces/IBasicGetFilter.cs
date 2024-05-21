using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicGetFilterEntity<TInputEntity, TResponseEntity>
    {
        Task<GenericResponseDto<List<TResponseEntity>>> GetFilterEntity(GenericInputDto<TInputEntity> entity);
    }
}

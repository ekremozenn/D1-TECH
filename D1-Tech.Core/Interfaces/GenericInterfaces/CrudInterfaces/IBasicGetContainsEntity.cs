using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicGetContainsEntity<TInputEntity, TResponseEntity>
    {
        Task<GenericResponseDto<List<TResponseEntity>>> GetContainsEntity(GenericInputDto<TInputEntity> tEntity);
    }
}

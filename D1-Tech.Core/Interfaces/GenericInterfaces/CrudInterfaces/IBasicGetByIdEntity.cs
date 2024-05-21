using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicGetByIdEntity<TInputEntity, TResponseEntity>
    {
        Task<GenericResponseDto<TResponseEntity>> GetByIdEntity(TInputEntity id);
    }
}

using D1_Tech.Core.Dtos.GenericDtos;

namespace Core.Interfaces.GenericInterfaces.CrudInterfaces
{
    public interface IBasicUpdateManyEntity<TInputEntity, TOutputEntity>
    {
        Task<GenericResponseDto<TOutputEntity>> UpdateManyEntity(GenericInputDto<List<TInputEntity>>? tEntities);
    }
}

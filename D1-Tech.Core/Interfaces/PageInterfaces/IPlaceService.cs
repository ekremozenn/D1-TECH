using Core.Interfaces.GenericInterfaces.CrudInterfaces;
using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.Place;
using D1_Tech.Core.Models.PageEntity;

namespace D1_Tech.Core.Interfaces.PageInterfaces
{
    public interface IPlaceService : IBasicCreateManyEntity<CreatePlaceDto, List<PlaceDto>>,
                                     IBasicDeleteManyEntity<DeletePlaceDto, NoContent>,
                                     IBasicUpdateManyEntity<UpdatePlaceDto, List<PlaceDto>>,
                                     IBasicGetByIdEntity<long, PlaceDto>,
                                     IBasicGetAllEntity<NoContent, List<PlaceDto>, Place>,
                                     IBasicGetContainsEntity<PlaceDto, PlaceDto>
    {
        Task<GenericResponseDto<NoContent>> DeletePlaceWithCrossTables(GenericInputDto<DeletePlaceWithCrossDto> tEntities);
        Task<GenericResponseDto<PlaceDto>> CreatePlaceWithCrossTables(GenericInputDto<CreatePlaceWithCrossDto> tEntities);
        Task<GenericResponseDto<PlaceDto>> UpdatePlaceWithCrossTables(GenericInputDto<UpdatePlaceWithCrossDto> tEntities);
    }
}

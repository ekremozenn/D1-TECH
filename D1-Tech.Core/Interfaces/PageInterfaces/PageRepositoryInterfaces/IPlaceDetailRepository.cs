using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.Place;
using D1_Tech.Core.Models.PageEntity;

namespace D1_Tech.Core.Interfaces.PageInterfaces.PageRepositoryInterfaces
{
    public interface IPlaceRepository
    {
        Task<GenericResponseDto<PlaceDto>> CreatePlaceWithCrossTable(Place place, CreatePlaceWithCrossDto createPlaceWithCrossDto);
        Task<GenericResponseDto<PlaceDto>> UpdatePlaceWithCrossTable(UpdatePlaceWithCrossDto updatePlaceWithCrossDto);
        Task<GenericResponseDto<NoContent>> DeletePlaceWithCrossTable(DeletePlaceWithCrossDto deletePlaceWithCrossDto);
    }
}
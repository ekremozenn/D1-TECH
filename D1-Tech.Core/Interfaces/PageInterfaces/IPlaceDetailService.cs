using Core.Interfaces.GenericInterfaces.CrudInterfaces;
using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.PlaceDetail;
using D1_Tech.Core.Models.PageEntity;

namespace D1_Tech.Core.Interfaces.PageInterfaces
{
    public interface IPlaceDetailService : IBasicCreateManyEntity<CreatePlaceDetailDto, List<PlaceDetailDto>>,
                                           IBasicDeleteManyEntity<DeletePlaceDetailDto, NoContent>,
                                           IBasicUpdateManyEntity<UpdatePlaceDetailDto, List<PlaceDetailDto>>,
                                           IBasicGetByIdEntity<long, PlaceDetailDto>,
                                           IBasicGetAllEntity<NoContent, List<PlaceDetailDto>, PlaceDetail>,
                                           IBasicGetContainsEntity<PlaceDetailDto, PlaceDetailDto>
    {
    }
}
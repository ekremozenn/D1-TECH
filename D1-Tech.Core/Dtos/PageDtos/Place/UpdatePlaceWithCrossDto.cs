using D1_Tech.Core.Dtos.PageDtos.PlaceDetail;

namespace D1_Tech.Core.Dtos.PageDtos.Place
{
    public class UpdatePlaceWithCrossDto
    {
        public UpdatePlaceDto UpdatePlaceDto { get; set; }
        public List<UpdatePlaceDetailDto> UpdatePlaceDetailDto { get; set; }
        public List<DeletePlaceDto> DeletePlaceDto { get; set; }
        public List<DeletePlaceDetailDto> DeletePlaceDetailDto { get; set; }
    }
}   
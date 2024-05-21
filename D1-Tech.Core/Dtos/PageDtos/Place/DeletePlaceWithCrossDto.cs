using D1_Tech.Core.Dtos.PageDtos.PlaceDetail;

namespace D1_Tech.Core.Dtos.PageDtos.Place
{
    public class DeletePlaceWithCrossDto
    {
        public DeletePlaceDto DeletePlaceDto { get; set; }
        public List<DeletePlaceDetailDto> DeletePlaceDetailDto { get; set; }

    }
}
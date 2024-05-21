using D1_Tech.Core.Dtos.PageDtos.PlaceDetail;

namespace D1_Tech.Core.Dtos.PageDtos.Place
{
    public class CreatePlaceWithCrossDto
    {
      public  CreatePlaceDto CreatePlaceDto { get; set; }
      public List <CreatePlaceDetailDto> CreatePlaceDetailDto { get; set; }
    }
}
using D1_Tech.Core.Dtos.CommonDtos;

namespace D1_Tech.Core.Dtos.PageDtos.PlaceDetail
{
    public class UpdatePlaceDetailDto:BaseIdDto
    {
        public string Address { get; set; }
        public long PlaceId { get; set; }
    }
}

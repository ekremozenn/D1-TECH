using D1_Tech.Core.Dtos.CommonDtos;

namespace D1_Tech.Core.Dtos.PageDtos.PlaceDetail
{
    public class PlaceDetailNavigationDto : BaseIdDto
    {
        public string Address { get; set; }
        public long PlaceId { get; set; }
    }
}
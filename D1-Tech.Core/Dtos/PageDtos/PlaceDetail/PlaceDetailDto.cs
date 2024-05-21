using D1_Tech.Core.Dtos.CommonDtos;
using D1_Tech.Core.Dtos.PageDtos.Place;

namespace D1_Tech.Core.Dtos.PageDtos.PlaceDetail
{
    public class PlaceDetailDto : BaseIdDto
    {
        public string Address { get; set; }
        public long PlaceId { get; set; }
        public PlaceNavigationDto Places { get; set; }
    }
}
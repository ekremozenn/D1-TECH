using D1_Tech.Core.Dtos.CommonDtos;

namespace D1_Tech.Core.Dtos.PageDtos.Place
{
    public class PlaceNavigationDto:BaseIdDto
    {
        public string Address { get; set; }
        public long PlaceId { get; set; }
    }
}

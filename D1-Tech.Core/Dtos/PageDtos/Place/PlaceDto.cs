using D1_Tech.Core.Dtos.CommonDtos;
using D1_Tech.Core.Dtos.PageDtos.PlaceDetail;

namespace D1_Tech.Core.Dtos.PageDtos.Place
{
    public class PlaceDto : BaseIdDto
    {
        public string? Name { get; set; }
        public ICollection<PlaceDetailNavigationDto> PlaceDetails { get; set; }
    }
}
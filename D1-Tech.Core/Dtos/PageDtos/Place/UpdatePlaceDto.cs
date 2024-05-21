using D1_Tech.Core.Dtos.CommonDtos;

namespace D1_Tech.Core.Dtos.PageDtos.Place
{
    public class UpdatePlaceDto:BaseIdDto
    {
        public UpdatePlaceDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

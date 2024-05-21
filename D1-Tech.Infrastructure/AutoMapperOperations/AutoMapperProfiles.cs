using AutoMapper;
using D1_Tech.Core.Dtos.PageDtos.Place;
using D1_Tech.Core.Dtos.PageDtos.PlaceDetail;
using D1_Tech.Core.Models.PageEntity;

namespace D1_Tech.Infrastructure.AutoMapperOperations
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Place
            CreateMap<Place, PlaceDto>().ReverseMap();
            CreateMap<Place, PlaceNavigationDto>().ReverseMap();
            CreateMap<Place, UpdatePlaceDto>().ReverseMap();
            CreateMap<Place, DeletePlaceDto>().ReverseMap();
            CreateMap<Place, CreatePlaceDto>().ReverseMap();
            #endregion
            #region PlaceDetail
            CreateMap<PlaceDetail, PlaceDetailDto>().ReverseMap();
            CreateMap<PlaceDetail, PlaceDetailNavigationDto>().ReverseMap();
            CreateMap<PlaceDetail, UpdatePlaceDetailDto>().ReverseMap();
            CreateMap<PlaceDetail, DeletePlaceDetailDto>().ReverseMap();
            CreateMap<PlaceDetail, CreatePlaceDetailDto>().ReverseMap();
            CreateMap<PlaceDetailDto, PlaceDetailNavigationDto>().ReverseMap();
            #endregion
        }
    }
}
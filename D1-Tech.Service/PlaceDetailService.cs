using AutoMapper;
using D1_Tech.Core.Dtos.Constants;
using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.PlaceDetail;
using D1_Tech.Core.Interfaces.GenericInterfaces;
using D1_Tech.Core.Interfaces.PageInterfaces;
using D1_Tech.Core.Models.PageEntity;
using D1_Tech.Service.Repositories.GenericRepositories;

namespace D1_Tech.Service
{
    public class PlaceDetailService : IPlaceDetailService
    {
        private readonly IGenericCrudRepository<PlaceDetail> _placeDetailCrudRepository;
        IMapper _mapper;

        public PlaceDetailService(IGenericCrudRepository<PlaceDetail> placeDetailCrudRepository, IMapper mapper)
        {
            _placeDetailCrudRepository = placeDetailCrudRepository;
            _mapper = mapper;
        }

        public async Task<GenericResponseDto<List<PlaceDetailDto>>> CreateManyEntity(GenericInputDto<List<CreatePlaceDetailDto>> tEntities)
        {
            List<PlaceDetail> mappedEntity = _mapper.Map<List<PlaceDetail>>(tEntities.Data);
            await _placeDetailCrudRepository.InsertManyEntity(mappedEntity);
            return GenericResponseDto<List<PlaceDetailDto>>.ResponseData(_mapper.Map<List<PlaceDetailDto>>(mappedEntity), (int)ErrorEnums.Success, null);
        }

        public async Task<GenericResponseDto<NoContent>> DeleteManyEntity(GenericInputDto<List<DeletePlaceDetailDto>>? tEntities)
        {
            await _placeDetailCrudRepository.DeleteManyEntity(tEntities.Data.Select(x => x.Id).ToList());
            return GenericResponseDto<NoContent>.ResponseData(new NoContent(), (int)ErrorEnums.Success, null);
        }

        public async Task<GenericResponseDto<List<PlaceDetailDto>>> GetAllEntity(GenericInputDto<NoContent> tEntity, Func<IQueryable<PlaceDetail>, IQueryable<PlaceDetail>>? query = null)
        {
            List<PlaceDetail> places = await _placeDetailCrudRepository.GetAllAsNoTracking(tEntity.Skip, tEntity.Take, tEntity.OrderedColumn, tEntity.OrderType, query)
                                        .ToListUncommitedAsync<PlaceDetail>();

            int totalCount = await _placeDetailCrudRepository.GetTotalCount(query);

            return GenericResponseDto<List<PlaceDetailDto>>.ResponseData(_mapper.Map<List<PlaceDetailDto>>(places), (int)ErrorEnums.Success, null, totalCount);
        }

        public async Task<GenericResponseDto<PlaceDetailDto>> GetByIdEntity(long id)
        {
            PlaceDetail place = await _placeDetailCrudRepository.GetByIdAsNoTracking(id);
            PlaceDetailDto mappedDto = _mapper.Map<PlaceDetailDto>(place);
            return GenericResponseDto<PlaceDetailDto>.ResponseData(mappedDto, (int)ErrorEnums.Success, null);
        }

        public async Task<GenericResponseDto<List<PlaceDetailDto>>> GetContainsEntity(GenericInputDto<PlaceDetailDto> tEntity)
        {
            GenericResponseDto<List<PlaceDetailDto>> listPlace = await GetAllEntity(GenericInputDto<NoContent>.ResponseData(new NoContent(), tEntity.Skip, tEntity.Take, tEntity.OrderedColumn, tEntity.OrderType), p =>
                                                           p.Where(x => (tEntity.Data.Id == null || x.Id == tEntity.Data.Id)
                                                           && (tEntity.Data.Address == null || x.Address.Contains(tEntity.Data.Address))));

            return GenericResponseDto<List<PlaceDetailDto>>.ResponseData(listPlace.Data, (int)ErrorEnums.Success, null, listPlace.TotalCount);
        }

        public async Task<GenericResponseDto<List<PlaceDetailDto>>> UpdateManyEntity(GenericInputDto<List<UpdatePlaceDetailDto>>? tEntities)
        {
            List<PlaceDetail> mappedEntity = _mapper.Map<List<PlaceDetail>>(tEntities.Data);
            await _placeDetailCrudRepository.UpdateMany(mappedEntity);
            return GenericResponseDto<List<PlaceDetailDto>>.ResponseData(_mapper.Map<List<PlaceDetailDto>>(mappedEntity), (int)ErrorEnums.Success, null);
        }
    }
}
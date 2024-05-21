using AutoMapper;
using D1_Tech.Core.Dtos.Constants;
using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.Place;
using D1_Tech.Core.Interfaces.GenericInterfaces;
using D1_Tech.Core.Interfaces.PageInterfaces;
using D1_Tech.Core.Interfaces.PageInterfaces.PageRepositoryInterfaces;
using D1_Tech.Core.Models.PageEntity;
using D1_Tech.Service.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace D1_Tech.Service
{
    public class PlaceService : IPlaceService
    {
        private readonly IGenericCrudRepository<Place> _placeCrudRepository;
        private readonly IMapper _mapper;
        private readonly IPlaceRepository _placeServiceRepository;

        public PlaceService(IGenericCrudRepository<Place> placeCrudRepository, IMapper mapper, IPlaceRepository placeServiceRepository)
        {
            _placeCrudRepository = placeCrudRepository;
            _mapper = mapper;
            _placeServiceRepository = placeServiceRepository;
        }

        public async Task<GenericResponseDto<List<PlaceDto>>> CreateManyEntity(GenericInputDto<List<CreatePlaceDto>> tEntities)
        {
            List<Place> mappedEntity = _mapper.Map<List<Place>>(tEntities.Data);
            await _placeCrudRepository.InsertManyEntity(mappedEntity);
            return GenericResponseDto<List<PlaceDto>>.ResponseData(_mapper.Map<List<PlaceDto>>(mappedEntity), (int)ErrorEnums.Success, null);
        }

        public async Task<GenericResponseDto<NoContent>> DeleteManyEntity(GenericInputDto<List<DeletePlaceDto>>? tEntities)
        {
            await _placeCrudRepository.DeleteManyEntity(tEntities.Data.Select(x => x.Id).ToList());
            return GenericResponseDto<NoContent>.ResponseData(new NoContent(), (int)ErrorEnums.Success, null);
        }

        public async Task<GenericResponseDto<List<PlaceDto>>> GetAllEntity(GenericInputDto<NoContent> tEntity, Func<IQueryable<Place>, IQueryable<Place>>? query = null)
        {
            List<Place> places = await _placeCrudRepository.GetAllAsNoTracking(tEntity.Skip, tEntity.Take, tEntity.OrderedColumn, tEntity.OrderType, query)
                             .Include(x => x.PlaceDetails)
                             .ToListUncommitedAsync<Place>();

            int totalCount = await _placeCrudRepository.GetTotalCount(query);

            return GenericResponseDto<List<PlaceDto>>.ResponseData(_mapper.Map<List<PlaceDto>>(places), (int)ErrorEnums.Success, null, totalCount);
        }

        public async Task<GenericResponseDto<PlaceDto>> GetByIdEntity(long id)
        {
            Place place = await _placeCrudRepository.GetByIdAsNoTracking(id);
            PlaceDto mappedDto = _mapper.Map<PlaceDto>(place);
            return GenericResponseDto<PlaceDto>.ResponseData(mappedDto, (int)ErrorEnums.Success, null);
        }

        public async Task<GenericResponseDto<List<PlaceDto>>> GetContainsEntity(GenericInputDto<PlaceDto> tEntity)
        {
            GenericResponseDto<List<PlaceDto>> listPlace = await GetAllEntity(GenericInputDto<NoContent>.ResponseData(new NoContent(), tEntity.Skip, tEntity.Take, tEntity.OrderedColumn, tEntity.OrderType), p =>
                                                  p.Where(x => (tEntity.Data.Id == null || x.Id == tEntity.Data.Id)
                                                  && (tEntity.Data.Name == null || x.Name.Contains(tEntity.Data.Name))));

            return GenericResponseDto<List<PlaceDto>>.ResponseData(listPlace.Data, (int)ErrorEnums.Success, null, listPlace.TotalCount);
        }

        public async Task<GenericResponseDto<List<PlaceDto>>> UpdateManyEntity(GenericInputDto<List<UpdatePlaceDto>>? tEntities)
        {
            List<Place> mappedEntity = _mapper.Map<List<Place>>(tEntities.Data);
            await _placeCrudRepository.UpdateMany(mappedEntity);
            return GenericResponseDto<List<PlaceDto>>.ResponseData(_mapper.Map<List<PlaceDto>>(mappedEntity), (int)ErrorEnums.Success, null);
        }

        public async Task<GenericResponseDto<PlaceDto>> CreatePlaceWithCrossTables(GenericInputDto<CreatePlaceWithCrossDto> tEntities)
        {
            Place createdEntity = await _placeCrudRepository.Insert(_mapper.Map<Place>(tEntities.Data.CreatePlaceDto));
            return await _placeServiceRepository.CreatePlaceWithCrossTable(createdEntity, tEntities.Data);
        }

        public async Task<GenericResponseDto<PlaceDto>> UpdatePlaceWithCrossTables(GenericInputDto<UpdatePlaceWithCrossDto> tEntities)
        {
            return await _placeServiceRepository.UpdatePlaceWithCrossTable(tEntities.Data);
        }

        public async Task<GenericResponseDto<NoContent>> DeletePlaceWithCrossTables(GenericInputDto<DeletePlaceWithCrossDto> tEntities)
        {
            await _placeCrudRepository.Delete((long)tEntities.Data.DeletePlaceDto.Id);

            return await _placeServiceRepository.DeletePlaceWithCrossTable(tEntities.Data);
        }
    }
}
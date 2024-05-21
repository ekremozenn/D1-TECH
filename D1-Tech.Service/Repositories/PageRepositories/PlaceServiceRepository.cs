using AutoMapper;
using D1_Tech.Core.Dtos.Constants;
using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.Place;
using D1_Tech.Core.Interfaces.GenericInterfaces;
using D1_Tech.Core.Interfaces.PageInterfaces.PageRepositoryInterfaces;
using D1_Tech.Core.Models.CommonEntity;
using D1_Tech.Core.Models.PageEntity;
using D1_Tech.Service.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace D1_Tech.Service.Repositories.PageRepositories
{
    public class PlaceServiceRepository : IPlaceRepository
    {
        readonly IMapper _mapper;
        private readonly IGenericCrudRepository<Place> _placeCrudRepository;
        private readonly IGenericCrudRepository<PlaceDetail> _placeDetailCrudRepository;

        public PlaceServiceRepository(IMapper mapper, IGenericCrudRepository<Place> placeCrudRepository, IGenericCrudRepository<PlaceDetail> placeDetailCrudRepository)
        {
            _mapper = mapper;
            _placeCrudRepository = placeCrudRepository;
            _placeDetailCrudRepository = placeDetailCrudRepository;
        }

        public async Task<GenericResponseDto<PlaceDto>> CreatePlaceWithCrossTable(Place place, CreatePlaceWithCrossDto createPlaceWithCrossDto)
        {
            try
            {
                if (createPlaceWithCrossDto.CreatePlaceDetailDto.Any())
                {
                    List<PlaceDetail> mappedPlaceDetailDto = _mapper.Map<List<PlaceDetail>>(createPlaceWithCrossDto.CreatePlaceDetailDto);
                    mappedPlaceDetailDto.ForEach(item => item.PlaceId = place.Id);

                    await _placeDetailCrudRepository.InsertManyWithoutTransaction(mappedPlaceDetailDto);

                }
                if (createPlaceWithCrossDto.CreatePlaceDetailDto != null) await _placeCrudRepository.CommitTransactionAsync();

            }
            catch (Exception e)
            {

                await _placeCrudRepository.Delete(place.Id, true);
                await _placeDetailCrudRepository.RollbackTransactionAsync();
                return GenericResponseDto<PlaceDto>.ResponseData(new PlaceDto(), (int)ErrorEnums.Fail, e.InnerException.Message);
            }
            Place result = _placeCrudRepository.GetAll()
                .Include(x => x.PlaceDetails)
                .Where(x => x.Id == place.Id).FirstOrDefault();
            return GenericResponseDto<PlaceDto>.ResponseData(_mapper.Map<PlaceDto>(result), (int)ErrorEnums.Success, null);
        }

        public async Task<GenericResponseDto<NoContent>> DeletePlaceWithCrossTable(DeletePlaceWithCrossDto deletePlaceWithCrossDto)
        {
            try
            {
                if (deletePlaceWithCrossDto.DeletePlaceDetailDto.Any())
                {
                    await _placeDetailCrudRepository.DeleteManyWithoutTransaction(deletePlaceWithCrossDto.DeletePlaceDetailDto.Select(x => x.Id).ToList());
                }
                if (deletePlaceWithCrossDto.DeletePlaceDetailDto.Any()) await _placeDetailCrudRepository.CommitTransactionAsync();

            }
            catch (Exception)
            {

                await _placeDetailCrudRepository.RollbackTransactionAsync();
                await _placeCrudRepository.RollbackTransactionAsync();
                return GenericResponseDto<NoContent>.ResponseData(new NoContent(), (int)ErrorEnums.Fail, ErrorMessages.DeleteError);
            }

            return GenericResponseDto<NoContent>.ResponseData(new NoContent(), (int)ErrorEnums.Success, null);
        }

        public async Task<GenericResponseDto<PlaceDto>> UpdatePlaceWithCrossTable(UpdatePlaceWithCrossDto updatePlaceWithCrossDto)
        {
            try
            {
                if (updatePlaceWithCrossDto.DeletePlaceDetailDto.Any())
                {
                    await _placeDetailCrudRepository.DeleteManyWithoutTransaction(updatePlaceWithCrossDto.DeletePlaceDetailDto.Select(x => x.Id).ToList());
                }
                if (updatePlaceWithCrossDto.UpdatePlaceDto != null)
                {
                    Place updatedDataGetById = await _placeCrudRepository.GetByIdAsNoTracking((int)updatePlaceWithCrossDto.UpdatePlaceDto.Id);
                    Place placeMapping = _mapper.Map<Place>(updatePlaceWithCrossDto.UpdatePlaceDto);
                    placeMapping.CreatedDate = updatedDataGetById.CreatedDate;
                    placeMapping.CreatedBy = updatedDataGetById.CreatedBy;
                    await _placeCrudRepository.UpdateWithoutTransaction(placeMapping);
                }

                if (updatePlaceWithCrossDto.UpdatePlaceDetailDto.Count() != 0)
                {
                    List<PlaceDetail> mappedEntity = _mapper.Map<List<PlaceDetail>>(updatePlaceWithCrossDto.UpdatePlaceDetailDto);
                    mappedEntity.ForEach(item => item.Id = (long)updatePlaceWithCrossDto.UpdatePlaceDto.Id);
                    await _placeDetailCrudRepository.UpdateManyWithoutTransaction(mappedEntity);
                }
                if (updatePlaceWithCrossDto.UpdatePlaceDto != null) await _placeCrudRepository.CommitTransactionAsync();
                if (updatePlaceWithCrossDto.UpdatePlaceDetailDto.Any() || updatePlaceWithCrossDto.DeletePlaceDetailDto.Any()) await _placeDetailCrudRepository.CommitTransactionAsync();

            }
            catch (Exception)
            {

                await _placeCrudRepository.RollbackTransactionAsync();
                await _placeDetailCrudRepository.RollbackTransactionAsync();
                return GenericResponseDto<PlaceDto>.ResponseData(new PlaceDto(), (int)ErrorEnums.Fail, ErrorMessages.UpdateError);
            }
            Place result = await _placeCrudRepository.GetAllAsNoTracking()
                .Include(x => x.PlaceDetails)
                .Where(x => x.Id == updatePlaceWithCrossDto.UpdatePlaceDto.Id).FirstOrDefaultUncommitedAsync();
            return GenericResponseDto<PlaceDto>.ResponseData(_mapper.Map<PlaceDto>(result), (int)ErrorEnums.Success, null);
        }
    }
}

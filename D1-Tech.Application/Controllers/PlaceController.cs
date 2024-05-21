using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.Place;
using D1_Tech.Core.Interfaces.PageInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace D1_Tech.Application.Controllers
{
    [Route("api/placeController", Order = 0)]
    [Authorize(Roles = "Admin")]
    public class PlaceController : Controller
    {
        readonly IPlaceService _placeService;
        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }
        [HttpPost]
        [Route("create-many-place")]
        public async Task<GenericResponseDto<List<PlaceDto>>> CreateManyEntity([FromBody] GenericInputDto<List<CreatePlaceDto>> tEntities)
        {
            return await _placeService.CreateManyEntity(tEntities);
        }
        [HttpDelete]
        [Route("delete-many-place")]
        public async Task<GenericResponseDto<NoContent>> DeleteManyEntity([FromBody] GenericInputDto<List<DeletePlaceDto>>? tEntities)
        {
            return await _placeService.DeleteManyEntity(tEntities);
        }
        [HttpGet]
        [Route("get-all-place")]
        public async Task<GenericResponseDto<List<PlaceDto>>> GetAllEntity(GenericInputDto<NoContent> tEntities)
        {
            return await _placeService.GetAllEntity(tEntities);
        }
        [HttpGet]
        [Route("get-by-id-place")]
        public async Task<GenericResponseDto<PlaceDto>> GetByIdEntity(long id)
        {
            return await _placeService.GetByIdEntity(id);
        }
        [HttpGet]
        [Route("get-contains-place")]
        public async Task<GenericResponseDto<List<PlaceDto>>> GetContainsEntity(GenericInputDto<PlaceDto> tEntities)
        {
            return await _placeService.GetContainsEntity(tEntities);
        }
        [HttpPut]
        [Route("update-many-place")]
        public async Task<GenericResponseDto<List<PlaceDto>>> UpdateManyEntity([FromBody] GenericInputDto<List<UpdatePlaceDto>>? tEntities)
        {
            return await _placeService.UpdateManyEntity(tEntities);
        }
        [HttpPost]
        [Route("create-cross-place")]
        public async Task<GenericResponseDto<PlaceDto>> CreatePlaceWithCrossTables([FromBody] GenericInputDto<CreatePlaceWithCrossDto> tEntities)
        {
            return await _placeService.CreatePlaceWithCrossTables(tEntities);

        }
        [HttpDelete]
        [Route("delete-cross-place")]
        public async Task<GenericResponseDto<NoContent>> DeletePlaceWithCrossTables([FromBody] GenericInputDto<DeletePlaceWithCrossDto> tEntities)
        {
            return await _placeService.DeletePlaceWithCrossTables(tEntities);

        }
        [HttpPut]
        [Route("update-cross-place")]
        public async Task<GenericResponseDto<PlaceDto>> UpdatePlaceWithCrossTables([FromBody] GenericInputDto<UpdatePlaceWithCrossDto> tEntities)
        {
            return await _placeService.UpdatePlaceWithCrossTables(tEntities);

        }
    }
}
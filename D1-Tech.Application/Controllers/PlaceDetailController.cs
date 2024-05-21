using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.PlaceDetail;
using D1_Tech.Core.Interfaces.PageInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace D1_Tech.Application.Controllers
{
    [Route("api/placeDetailController", Order = 0)]
    [Authorize(Roles= "Admin")]
    public class PlaceDetailController : Controller
    {
        private readonly IPlaceDetailService _placeDetailService;

        public PlaceDetailController(IPlaceDetailService placeDetailService)
        {
            _placeDetailService = placeDetailService;
        }
        [HttpPost]
        [Route("create-many-placeDetail")]
        public async Task<GenericResponseDto<List<PlaceDetailDto>>> CreateManyEntity([FromBody]GenericInputDto<List<CreatePlaceDetailDto>> tEntity)
        {
            return await _placeDetailService.CreateManyEntity(tEntity);
        }
        [HttpDelete]
        [Route("delete-many-placeDetail")]
        public async Task<GenericResponseDto<NoContent>> DeleteManyEntity([FromBody]GenericInputDto<List<DeletePlaceDetailDto>>? tEntity)
        {
            return await _placeDetailService.DeleteManyEntity(tEntity);
        }
        [HttpGet]
        [Route("get-all-placeDetail")]
        public async Task<GenericResponseDto<List<PlaceDetailDto>>> GetAllEntity(GenericInputDto<NoContent> tEntity)
        {
            return await _placeDetailService.GetAllEntity(tEntity);
        }
        [HttpGet]
        [Route("get-by-id-placeDetail")]
        public async Task<GenericResponseDto<PlaceDetailDto>> GetByIdEntity(long id)
        {
            return await _placeDetailService.GetByIdEntity(id);
        }
        [HttpGet]
        [Route("get-contains-placeDetail")]
        public async Task<GenericResponseDto<List<PlaceDetailDto>>> GetContainsEntity(GenericInputDto<PlaceDetailDto> tEntity)
        {
            return await _placeDetailService.GetContainsEntity(tEntity);
        }
        [HttpPut]
        [Route("update-many-placeDetail")]
        public async Task<GenericResponseDto<List<PlaceDetailDto>>> UpdateManyEntity([FromBody]GenericInputDto<List<UpdatePlaceDetailDto>>? tEntity)
        {
            return await _placeDetailService.UpdateManyEntity(tEntity);
        }
    }
}
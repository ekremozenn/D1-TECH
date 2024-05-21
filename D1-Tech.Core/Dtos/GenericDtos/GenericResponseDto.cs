using D1_Tech.Core.Interfaces.CommonInterfaces;

namespace D1_Tech.Core.Dtos.GenericDtos
{
    public class GenericResponseDto<TEntity> : IPaginationResult
    {

        public GenericResponseDto()
        {

        }

        public TEntity? Data { get; set; }
        public bool IsSuccessful { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
        public int? TotalCount { get; set; }
        public static GenericResponseDto<TEntity> ResponseData(TEntity? data, int statusCode, string? error, int? totalCount = 0)
        {
            return new GenericResponseDto<TEntity> { Data = data, StatusCode = statusCode, IsSuccessful = String.IsNullOrEmpty(error), Error = error, TotalCount = totalCount };
        }
    }
}
using D1_Tech.Core.Dtos.Constants;
using System.Text.Json.Serialization;

namespace D1_Tech.Core.Dtos.GenericDtos
{
    public class GenericInputDto<T>
    {
        public T? Data { get; set; }
        [JsonIgnore]
        public int? Skip { get; set; }
        [JsonIgnore]
        public int? Take { get; set; }
        [JsonIgnore]
        public string? OrderedColumn { get; set; }
        [JsonIgnore]
        public string? OrderType { get; set; }

        public GenericInputDto()
        {
            Skip = 0;
            Take = int.MaxValue;
        }

        public static GenericInputDto<T> ResponseData(T? data)
        {
            return new GenericInputDto<T>
            {
                Data = data
            };
        }
        public static GenericInputDto<T> ResponseData(T? data, int? skip = null, int? take = null, string orderedColumn = "Id", string orderType = EntityOrderType.OrderAsc)
        {
            return new GenericInputDto<T> { Data = data, Skip = skip, Take = take, OrderedColumn = orderedColumn, OrderType = orderType };
        }

    }
}

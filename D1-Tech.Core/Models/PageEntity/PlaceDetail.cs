using D1_Tech.Core.Models.CommonEntity;

namespace D1_Tech.Core.Models.PageEntity
{
    public class PlaceDetail:BaseEntity
    {
        public string Address { get; set; }
        public long PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}

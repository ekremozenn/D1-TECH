using D1_Tech.Core.Models.CommonEntity;

namespace D1_Tech.Core.Models.PageEntity
{
    public class Place:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<PlaceDetail> PlaceDetails { get; set; }
    }
}

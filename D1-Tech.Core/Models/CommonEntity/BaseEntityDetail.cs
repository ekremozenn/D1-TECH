namespace D1_Tech.Core.Models.CommonEntity
{
    public abstract class BaseEntityDetail
    {
        public DateTime CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public int? Deleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int? DeletedBy { get; set; }
    }
}

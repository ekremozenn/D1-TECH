using D1_Tech.Core.Models.CommonEntity;

namespace D1_Tech.Core.Models.PageEntity
{
    public class User : BaseEntity
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public long? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
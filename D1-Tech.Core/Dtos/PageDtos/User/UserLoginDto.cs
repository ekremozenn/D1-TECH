using D1_Tech.Core.Models.PageEntity;

namespace D1_Tech.Core.Dtos.PageDtos.User
{
    public class UserLoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public Role Role { get; set; }
    }
}
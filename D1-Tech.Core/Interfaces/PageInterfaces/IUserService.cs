using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.User;

namespace D1_Tech.Core.Interfaces.PageInterfaces
{
    public interface IUserService
    {
        Task<GenericResponseDto<UserLoginDto>> Login(UserLoginInputDto loginScreen);
    }
}

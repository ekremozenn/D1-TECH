using AutoMapper;
using D1_Tech.Core.Dtos.Constants;
using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.User;
using D1_Tech.Core.Interfaces.GenericInterfaces;
using D1_Tech.Core.Interfaces.PageInterfaces;
using D1_Tech.Core.Models.CommonEntity;
using D1_Tech.Core.Models.PageEntity;
using D1_Tech.Core.Models.PageEntity.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace D1_Tech.Service
{
    public class UserService : IUserService
    {
        private readonly IGenericCrudRepository<User> _userCrudRepository;
        readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IGenericCrudRepository<User> userCrudRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userCrudRepository = userCrudRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<GenericResponseDto<UserLoginDto>> Login(UserLoginInputDto loginScreen)
        {
            User? user = _userCrudRepository.GetAll().Include(x => x.Role).FirstOrDefault(u => u.Username == loginScreen.Username);

            if (user is null || user.Password != loginScreen.Password)
            {
                return GenericResponseDto<UserLoginDto>.ResponseData(null, (int)ErrorEnums.Fail, ErrorMessages.LoginError);
            }

            string? token = generateJwtToken(user);

            UserLoginDto userLoginDto = new UserLoginDto
            {
                Token = token,
                Username = loginScreen.Username,
                Role = user.Role
            };
            return GenericResponseDto<UserLoginDto>.ResponseData(userLoginDto, (int)ErrorEnums.Success, null);
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user?.Role?.Name?.ToString())
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

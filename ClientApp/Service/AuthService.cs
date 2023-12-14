using NiceToes.Web.Models;
using NiceToes.Web.Service.IService;
using NiceToes.Web.Utility;

namespace NiceToes.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = registrationRequestDto,
                Url = Sd.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = loginRequestDto,
                Url = Sd.AuthAPIBase + "/api/auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = registrationRequestDto,
                Url = Sd.AuthAPIBase + "/api/auth/register"
            }, withBearer: false);
        }
    }
}

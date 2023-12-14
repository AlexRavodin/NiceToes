using NiceToes.EmailAPI.Message;
using NiceToes.EmailAPI.Models.Dto;

namespace NiceToes.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
        Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}

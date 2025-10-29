using WebClient.Models.DTOs;

namespace WebClient.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(string email, string senha);
        Task<bool> LogoutAsync();
    }
}

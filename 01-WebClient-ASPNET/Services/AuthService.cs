using WebClient.Models.DTOs;

namespace WebClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiService _apiService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IApiService apiService, ILogger<AuthService> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        public async Task<LoginResponseDto?> LoginAsync(string email, string senha)
        {
            try
            {
                var loginData = new { Email = email, Senha = senha };
                var response = await _apiService.PostAsync<LoginResponseDto>("auth/login", loginData);

                if (response?.Sucesso == true && !string.IsNullOrEmpty(response.Token))
                {
                    _apiService.SetAuthToken(response.Token);
                    _logger.LogInformation($"Login bem-sucedido para {email}");
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao fazer login: {email}");
                return null;
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                _apiService.SetAuthToken(string.Empty);
                _logger.LogInformation("Logout realizado");
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer logout");
                return false;
            }
        }
    }
}

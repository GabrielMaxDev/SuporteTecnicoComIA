using WebClient.Models.DTOs;
using WebClient.Models.ViewModels;

namespace WebClient.Services
{
    public class ChamadoService : IChamadoService
    {
        private readonly IApiService _apiService;
        private readonly ILogger<ChamadoService> _logger;

        public ChamadoService(IApiService apiService, ILogger<ChamadoService> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        public async Task<List<ChamadoDto>> ObterMeusChamadosAsync()
        {
            try
            {
                var chamados = await _apiService.GetAsync<List<ChamadoDto>>("chamados/meus");
                return chamados ?? new List<ChamadoDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter chamados");
                return new List<ChamadoDto>();
            }
        }

        public async Task<ChamadoDto?> ObterDetalhesAsync(int id)
        {
            try
            {
                return await _apiService.GetAsync<ChamadoDto>($"chamados/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter detalhes do chamado {id}");
                return null;
            }
        }

        public async Task<ChamadoDto?> CriarChamadoAsync(ChamadoViewModel model)
        {
            try
            {
                var dto = new { model.Titulo, model.Descricao, model.Categoria, model.Prioridade };
                return await _apiService.PostAsync<ChamadoDto>("chamados", dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar chamado");
                return null;
            }
        }
    }
}

using WebClient.Models.DTOs;
using WebClient.Models.ViewModels;

namespace WebClient.Services
{
    public interface IChamadoService
    {
        Task<List<ChamadoDto>> ObterMeusChamadosAsync();
        Task<ChamadoDto?> ObterDetalhesAsync(int id);
        Task<ChamadoDto?> CriarChamadoAsync(ChamadoViewModel model);
    }
}

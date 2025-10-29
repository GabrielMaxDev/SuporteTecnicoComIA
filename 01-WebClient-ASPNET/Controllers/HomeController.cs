using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Services;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChamadoService _chamadoService;

        public HomeController(ILogger<HomeController> logger, IChamadoService chamadoService)
        {
            _logger = logger;
            _chamadoService = chamadoService;
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction(nameof(Dashboard));
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var chamados = await _chamadoService.ObterMeusChamadosAsync();

                ViewBag.TotalAbertos = chamados.Count(c => c.Status == "Aberto");
                ViewBag.TotalEmAndamento = chamados.Count(c => c.Status == "EmAndamento");
                ViewBag.TotalResolvidos = chamados.Count(c => c.Status == "Resolvido" || c.Status == "Fechado");

                var avaliacoes = chamados.Where(c => c.Avaliacao.HasValue).Select(c => c.Avaliacao!.Value);
                ViewBag.MediaAvaliacao = avaliacoes.Any() ? avaliacoes.Average() : 0;

                return View(chamados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar dashboard");
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}

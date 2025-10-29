using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Models.ViewModels;
using WebClient.Services;

namespace WebClient.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ChamadoController : Controller
    {
        private readonly IChamadoService _chamadoService;
        private readonly ILogger<ChamadoController> _logger;

        public ChamadoController(IChamadoService chamadoService, ILogger<ChamadoController> logger)
        {
            _chamadoService = chamadoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? status = null)
        {
            var chamados = await _chamadoService.ObterMeusChamadosAsync();
            if (!string.IsNullOrEmpty(status))
            {
                chamados = chamados.Where(c => c.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            ViewBag.StatusFiltro = status;
            return View(chamados);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View(new ChamadoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(ChamadoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var chamado = await _chamadoService.CriarChamadoAsync(model);
            if (chamado != null)
            {
                TempData["Sucesso"] = "Chamado criado com sucesso!";
                return RedirectToAction(nameof(Detalhes), new { id = chamado.Id });
            }

            ModelState.AddModelError(string.Empty, "Erro ao criar chamado.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            var chamado = await _chamadoService.ObterDetalhesAsync(id);
            if (chamado == null) return NotFound();
            return View(chamado);
        }
    }
}

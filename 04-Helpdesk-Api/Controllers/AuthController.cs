using Helpdesk.Api.DTOs;
using Helpdesk.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Helpdesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HelpdeskDBContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(HelpdeskDBContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            if (login == null)
            {
                _logger.LogWarning("O corpo da requisição de login estava nulo."); 
                return BadRequest();
            }
            
            _logger.LogInformation($"Tentativa de login para o usuário: '{login.Email}'");

            var usuarioDoBanco = await _context.TUsuarios
                                .Include(u => u.IdPerfilNavigation) 
                                .FirstOrDefaultAsync(u => u.DsUsername == login.Email); 

            if (usuarioDoBanco == null)
            {
                _logger.LogWarning($"FALHA NO LOGIN: Usuário '{login.Email}' não foi encontrado.");
                return Ok(new LoginResponse { Sucesso = false, Mensagem = "Usuário ou senha inválidos." });
            }

            _logger.LogInformation($"Usuário '{login.Email}' encontrado. Verificando senha...");

            // --- CÓDIGO FINAL ---
            // Agora vamos fazer a verificação REAL.
            bool senhaCorreta = false;
            try
            {
                // Usando .Trim() por segurança
                senhaCorreta = BCrypt.Net.BCrypt.Verify(login.Senha, usuarioDoBanco.DsSenhaHash.Trim());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no BCrypt.Verify."); 
                return Ok(new LoginResponse { Sucesso = false, Mensagem = "Erro ao verificar credenciais." });
            }
            // --- FIM DO CÓDIGO FINAL ---

            if (!senhaCorreta)
            {
                _logger.LogWarning($"FALHA NO LOGIN: Senha incorreta para o usuário '{login.Email}'.");
                return Ok(new LoginResponse { Sucesso = false, Mensagem = "Usuário ou senha inválidos." });
            }

            _logger.LogInformation($"Login para '{login.Email}' realizado com sucesso!");
            
            var usuarioDtoParaCliente = new UsuarioDto
            {
                Id = usuarioDoBanco.IdUsuario, 
                Nome = usuarioDoBanco.NmNome,
                Username = usuarioDoBanco.DsUsername, 
                Ativo = usuarioDoBanco.StAtivo,
                Perfil = usuarioDoBanco.IdPerfilNavigation?.NmNome ?? "Sem Perfil" 
            };
            
            return Ok(new LoginResponse
            {
                Sucesso = true,
                Usuario = usuarioDtoParaCliente,
                Mensagem = "Login realizado com sucesso!"
            });
        }
    }
}
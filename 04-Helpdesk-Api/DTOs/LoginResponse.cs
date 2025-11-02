using Helpdesk.Api.Models; // Mantenha este using

namespace Helpdesk.Api.DTOs
{
    public class LoginResponse
    {
        public bool Sucesso { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;

        public UsuarioDto? Usuario { get; set; }
    }
}
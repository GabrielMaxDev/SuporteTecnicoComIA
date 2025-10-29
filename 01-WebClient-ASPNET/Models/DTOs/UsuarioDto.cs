namespace WebClient.Models.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }

    public class LoginResponseDto
    {
        public bool Sucesso { get; set; }
        public string? Token { get; set; }
        public string? Mensagem { get; set; }
        public UsuarioDto? Usuario { get; set; }
    }
}

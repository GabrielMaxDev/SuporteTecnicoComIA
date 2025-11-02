using DesktopManager.Models;

namespace DesktopManager.Utils
{
    public class SessionManager
    {
        public Models.Usuario? Usuario { get; set; }
        public string? Token { get; set; } = string.Empty;

        public bool IsAutenticado()
        {
            return Usuario != null && !string.IsNullOrEmpty(Token);
        }

        public void Limpar()
        {
            Usuario = null;
            Token = string.Empty;
        }
    }
}

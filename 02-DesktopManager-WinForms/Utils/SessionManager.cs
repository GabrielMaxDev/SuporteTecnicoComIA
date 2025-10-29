namespace DesktopManager.Utils
{
    public static class SessionManager
    {
        public static Models.Usuario? Usuario { get; set; }
        public static string? Token { get; set; }

        public static bool IsAutenticado()
        {
            return Usuario != null && !string.IsNullOrEmpty(Token);
        }

        public static void Limpar()
        {
            Usuario = null;
            Token = null;
        }
    }
}

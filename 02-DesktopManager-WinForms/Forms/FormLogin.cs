using DesktopManager.Services;
using DesktopManager.Utils;
using DesktopManager.Models;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace DesktopManager.Forms
{
    public partial class FormLogin : Form
    {
        private Panel panelCentral = null!;
        private Label lblTitulo = null!;
        private Label lblSubtitulo = null!;
        private Label lblUsuario = null!;
        private TextBox txtUsuario = null!;
        private Label lblSenha = null!;
        private TextBox txtSenha = null!;
        private Button btnEntrar = null!;

        public FormLogin()
        {
            InitializeComponent();
            ConfigurarInterface();
            CentralizarConteudo();
            
            // Recentralizar quando redimensionar
            this.Resize += (s, e) => CentralizarConteudo();
        }

        private void ConfigurarInterface()
        {
            this.Text = "CATI - Central de Atendimento de Tecnologia da Informação";
            
            // Pegar tamanho da tela
            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
            
            // Definir tamanho como 80% da tela ou tamanho mínimo
            int width = Math.Max(800, (int)(screenSize.Width * 0.8));
            int height = Math.Max(600, (int)(screenSize.Height * 0.8));
            
            this.Size = new Size(width, height);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(800, 600);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.BackColor = Color.FromArgb(255, 221, 87); // Fundo amarelo
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Painel central amarelo - ocupa toda a janela
            panelCentral = new Panel();
            panelCentral.BackColor = Color.FromArgb(255, 221, 87);
            panelCentral.Dock = DockStyle.Fill; // Ocupa todo o espaço
            panelCentral.BorderStyle = BorderStyle.None;

            // Container interno para centralizar o conteúdo
            Panel containerConteudo = new Panel();
            containerConteudo.BackColor = Color.Transparent;
            containerConteudo.Size = new Size(400, 500);
            containerConteudo.Name = "containerConteudo";
            panelCentral.Controls.Add(containerConteudo);

            lblTitulo = new Label();
            lblTitulo.Text = "CATI";
            lblTitulo.Font = new Font("Segoe UI", 42, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(51, 51, 51);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(140, 50);
            containerConteudo.Controls.Add(lblTitulo);

            lblSubtitulo = new Label();
            lblSubtitulo.Text = "( Central de Atendimento\\nde Tecnologia da Informação)";
            lblSubtitulo.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lblSubtitulo.ForeColor = Color.FromArgb(51, 51, 51);
            lblSubtitulo.AutoSize = false;
            lblSubtitulo.Size = new Size(300, 50);
            lblSubtitulo.TextAlign = ContentAlignment.TopCenter;
            lblSubtitulo.Location = new Point(50, 120);
            containerConteudo.Controls.Add(lblSubtitulo);

            lblUsuario = new Label();
            lblUsuario.Text = "Usuário";
            lblUsuario.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblUsuario.ForeColor = Color.FromArgb(51, 51, 51);
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(50, 220);
            containerConteudo.Controls.Add(lblUsuario);

            txtUsuario = new TextBox();
            txtUsuario.Font = new Font("Segoe UI", 11);
            txtUsuario.Size = new Size(300, 30);
            txtUsuario.Location = new Point(50, 245);
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.BackColor = Color.White;
            txtUsuario.ForeColor = Color.Black;
            txtUsuario.TabIndex = 0;
            txtUsuario.KeyPress += TxtUsuario_KeyPress;
            containerConteudo.Controls.Add(txtUsuario);

            lblSenha = new Label();
            lblSenha.Text = "Senha";
            lblSenha.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblSenha.ForeColor = Color.FromArgb(51, 51, 51);
            lblSenha.AutoSize = true;
            lblSenha.Location = new Point(50, 290);
            containerConteudo.Controls.Add(lblSenha);

            txtSenha = new TextBox();
            txtSenha.Font = new Font("Segoe UI", 11);
            txtSenha.Size = new Size(300, 30);
            txtSenha.Location = new Point(50, 315);
            txtSenha.BorderStyle = BorderStyle.FixedSingle;
            txtSenha.BackColor = Color.White;
            txtSenha.ForeColor = Color.Black;
            txtSenha.PasswordChar = '•';
            txtSenha.UseSystemPasswordChar = false;
            txtSenha.TabIndex = 1;
            txtSenha.KeyPress += TxtSenha_KeyPress;
            containerConteudo.Controls.Add(txtSenha);

            btnEntrar = new Button();
            btnEntrar.Text = "Entrar";
            btnEntrar.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnEntrar.Size = new Size(140, 45);
            btnEntrar.Location = new Point(130, 380);
            btnEntrar.BackColor = Color.FromArgb(28, 28, 30);
            btnEntrar.ForeColor = Color.White;
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.FlatAppearance.BorderSize = 0;
            btnEntrar.Cursor = Cursors.Hand;
            btnEntrar.TabIndex = 2;
            btnEntrar.Click += btnLogin_Click;
            btnEntrar.MouseEnter += BtnEntrar_MouseEnter;
            btnEntrar.MouseLeave += BtnEntrar_MouseLeave;
            containerConteudo.Controls.Add(btnEntrar);

            this.Controls.Add(panelCentral);
            this.AcceptButton = btnEntrar;
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Centraliza o container de conteúdo dentro do painel amarelo
        /// </summary>
        private void CentralizarConteudo()
        {
            if (panelCentral != null)
            {
                // Encontrar o container de conteúdo
                var container = panelCentral.Controls.Find("containerConteudo", false);
                if (container.Length > 0 && container[0] is Panel containerPanel)
                {
                    // Calcular posição para centralizar
                    int x = (panelCentral.ClientSize.Width - containerPanel.Width) / 2;
                    int y = (panelCentral.ClientSize.Height - containerPanel.Height) / 2;
                    
                    // Garantir que não fique fora da tela
                    x = Math.Max(20, x);
                    y = Math.Max(20, y);
                    
                    containerPanel.Location = new Point(x, y);
                }
            }
        }

        private void TxtUsuario_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtSenha.Focus();
                e.Handled = true;
            }
        }

        private void TxtSenha_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnEntrar.PerformClick();
                e.Handled = true;
            }
        }

        private void BtnEntrar_MouseEnter(object? sender, EventArgs e)
        {
            btnEntrar.BackColor = Color.FromArgb(45, 45, 48);
        }

        private void BtnEntrar_MouseLeave(object? sender, EventArgs e)
        {
            btnEntrar.BackColor = Color.FromArgb(28, 28, 30);
        }

        private async void btnLogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Por favor, informe o usuário.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, informe a senha.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return;
            }

            btnEntrar.Enabled = false;
            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
            btnEntrar.Text = "Entrando...";

            try
            {
                var loginData = new
                {
                    Email = txtUsuario.Text.Trim(),
                    Senha = txtSenha.Text
                };

                var response = await ApiService.PostAsync<LoginResponse>("auth/login", loginData);

                if (response?.Sucesso == true && !string.IsNullOrEmpty(response.Token))
                {
                    ApiService.SetAuthToken(response.Token);
                    SessionManager.Usuario = response.Usuario;
                    SessionManager.Token = response.Token;

                    this.Hide();
                    var formPrincipal = new FormPrincipal();
                    formPrincipal.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        response?.Mensagem ?? "Usuário ou senha inválidos.\\n\\nVerifique suas credenciais e tente novamente.",
                        "Erro ao fazer login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    txtSenha.Clear();
                    txtSenha.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível conectar ao servidor.\\n\\nDetalhes: {ex.Message}\\n\\nVerifique se a API está rodando em http://localhost:5000",
                    "Erro de Conexão",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnEntrar.Enabled = true;
                txtUsuario.Enabled = true;
                txtSenha.Enabled = true;
                btnEntrar.Text = "Entrar";
            }
        }

        /// <summary>
        /// Recentraliza ao redimensionar
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CentralizarConteudo();
        }
    }

    // Classes para deserialização da resposta da API
    public class LoginResponse
    {
        public bool Sucesso { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public Usuario? Usuario { get; set; }
    }
}
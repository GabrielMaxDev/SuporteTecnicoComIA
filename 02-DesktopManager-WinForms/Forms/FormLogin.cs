// /Forms/FormLogin.cs

using DesktopManager.Models;
using DesktopManager.Services;
using DesktopManager.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

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

        // --- MELHORIA (BOA PRÁTICA): Injeção de Dependência ---
        // Vamos guardar as "instâncias" dos serviços
        private readonly ApiService _apiService;
        private readonly SessionManager _sessionManager;
        private Panel containerConteudo;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Construtor corrigido para receber os serviços (Injeção de Dependência).
        /// </summary>
        public FormLogin(ApiService apiService, SessionManager sessionManager, IServiceProvider serviceProvider)
        {
            // Atribui os serviços injetados
            _apiService = apiService;
            _sessionManager = sessionManager;
            _serviceProvider = serviceProvider;

            // O resto do seu código original
            InitializeComponent();
            ConfigurarInterface();
            CentralizarConteudo();

            // Recentralizar quando redimensionar
            this.Resize += (s, e) => CentralizarConteudo();
        }

        private void ConfigurarInterface()
        {
            this.Text = "CATI - Central de Atendimento de Tecnologia da Informação";

            Rectangle screenSize = Screen.PrimaryScreen.WorkingArea;
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
            panelCentral = new Panel();
            containerConteudo = new Panel();
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblSenha = new Label();
            txtSenha = new TextBox();
            btnEntrar = new Button();
            panelCentral.SuspendLayout();
            containerConteudo.SuspendLayout();
            SuspendLayout();
            // 
            // panelCentral
            // 
            panelCentral.BackColor = Color.FromArgb(255, 221, 87);
            panelCentral.Controls.Add(containerConteudo);
            panelCentral.Dock = DockStyle.Fill;
            panelCentral.Location = new Point(0, 0);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(606, 589);
            panelCentral.TabIndex = 0;
            // 
            // containerConteudo
            // 
            containerConteudo.BackColor = Color.Transparent;
            containerConteudo.Controls.Add(lblTitulo);
            containerConteudo.Controls.Add(lblSubtitulo);
            containerConteudo.Controls.Add(lblUsuario);
            containerConteudo.Controls.Add(txtUsuario);
            containerConteudo.Controls.Add(lblSenha);
            containerConteudo.Controls.Add(txtSenha);
            containerConteudo.Controls.Add(btnEntrar);
            containerConteudo.Location = new Point(0, 0);
            containerConteudo.Name = "containerConteudo";
            containerConteudo.Size = new Size(400, 500);
            containerConteudo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 42F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(51, 51, 51);
            lblTitulo.Location = new Point(140, 50);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(153, 74);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "CATI";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.Font = new Font("Segoe UI", 11F);
            lblSubtitulo.ForeColor = Color.FromArgb(51, 51, 51);
            lblSubtitulo.Location = new Point(50, 120);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(300, 50);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "( Central de Atendimento\nde Tecnologia da Informação)";
            lblSubtitulo.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 10F);
            lblUsuario.ForeColor = Color.FromArgb(51, 51, 51);
            lblUsuario.Location = new Point(50, 220);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(56, 19);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuário";
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.White;
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.ForeColor = Color.Black;
            txtUsuario.Location = new Point(50, 245);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(300, 27);
            txtUsuario.TabIndex = 0;
            txtUsuario.KeyPress += TxtUsuario_KeyPress;
            // 
            // lblSenha
            // 
            lblSenha.AutoSize = true;
            lblSenha.Font = new Font("Segoe UI", 10F);
            lblSenha.ForeColor = Color.FromArgb(51, 51, 51);
            lblSenha.Location = new Point(50, 290);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(46, 19);
            lblSenha.TabIndex = 3;
            lblSenha.Text = "Senha";
            // 
            // txtSenha
            // 
            txtSenha.BackColor = Color.White;
            txtSenha.BorderStyle = BorderStyle.FixedSingle;
            txtSenha.Font = new Font("Segoe UI", 11F);
            txtSenha.ForeColor = Color.Black;
            txtSenha.Location = new Point(50, 315);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '•';
            txtSenha.Size = new Size(300, 27);
            txtSenha.TabIndex = 1;
            txtSenha.KeyPress += TxtSenha_KeyPress;
            // 
            // btnEntrar
            // 
            btnEntrar.BackColor = Color.FromArgb(28, 28, 30);
            btnEntrar.Cursor = Cursors.Hand;
            btnEntrar.FlatAppearance.BorderSize = 0;
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnEntrar.ForeColor = Color.White;
            btnEntrar.Location = new Point(130, 380);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(140, 45);
            btnEntrar.TabIndex = 2;
            btnEntrar.Text = "Entrar";
            btnEntrar.UseVisualStyleBackColor = false;
            btnEntrar.Click += btnLogin_Click;
            btnEntrar.MouseEnter += BtnEntrar_MouseEnter;
            btnEntrar.MouseLeave += BtnEntrar_MouseLeave;
            // 
            // FormLogin
            // 
            AcceptButton = btnEntrar;
            ClientSize = new Size(606, 589);
            Controls.Add(panelCentral);
            Name = "FormLogin";
            panelCentral.ResumeLayout(false);
            containerConteudo.ResumeLayout(false);
            containerConteudo.PerformLayout();
            ResumeLayout(false);
        }

        private void CentralizarConteudo()
        {
            // Seu código de CentralizarConteudo() continua exatamente o mesmo...
            if (panelCentral != null)
            {
                var container = panelCentral.Controls.Find("containerConteudo", false);
                if (container.Length > 0 && container[0] is Panel containerPanel)
                {
                    int x = (panelCentral.ClientSize.Width - containerPanel.Width) / 2;
                    int y = (panelCentral.ClientSize.Height - containerPanel.Height) / 2;

                    x = Math.Max(20, x);
                    y = Math.Max(20, y);

                    containerPanel.Location = new Point(x, y);
                }
            }
        }

        // --- Seus métodos de UI continuam idênticos ---
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

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CentralizarConteudo();
        }

        /// <summary>
        /// Método de clique do botão, agora usando os serviços injetados.
        /// </summary>
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

                var response = await _apiService.PostAsync<LoginResponse>("auth/login", loginData);

                if (response?.Sucesso == true)
                {
                    // O login foi bem-sucedido (neste teste, forçado)
                    // Vamos garantir que recebemos o usuário
                    if (response.Usuario != null)
                    {
                        _sessionManager.Usuario = response.Usuario;
                        _sessionManager.Token = response.Token; // Token estará vazio, sem problemas

                        this.Hide();
                        using (var formPrincipal = _serviceProvider.GetRequiredService<FormPrincipal>())
                        {
                            formPrincipal.ShowDialog();
                        }
                        this.Close();
                    }
                    else
                    {
                        // Isso não deve acontecer, mas é uma boa defesa
                        MessageBox.Show("Login bem-sucedido, mas dados do usuário não foram recebidos.", "Erro de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // A API retornou Sucesso = false
                    MessageBox.Show(
                        response?.Mensagem ?? "Usuário ou senha inválidos.",
                        "Erro ao fazer login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show(
                    $"Erro de API: {httpEx.Message}\nStatus Code: {httpEx.StatusCode}\n\n" +
                    "Usuário ou senha podem estar inválidos, ou a API está com problemas.",
                    "Erro ao fazer login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível conectar ao servidor.\nDetalhes: {ex.Message}\nVerifique se a API está rodando.",
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
    }
}
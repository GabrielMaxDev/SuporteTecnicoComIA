using DesktopManager.Services;
using DesktopManager.Utils;
using System;
using System.Windows.Forms;
using System.Drawing;
using DesktopManager.Models;

namespace DesktopManager.Forms
{
    public partial class FormPrincipal : Form
    {
        private MenuStrip menuPrincipal = null!;
        private ToolStripMenuItem menuChamados = null!;
        private ToolStripMenuItem menuUsuarios = null!;
        private ToolStripMenuItem menuRelatorios = null!;
        private ToolStripMenuItem menuSair = null!;
        private StatusStrip statusBar = null!;
        private ToolStripStatusLabel lblStatusUsuario = null!;
        private ToolStripStatusLabel lblStatusHora = null!;
        private Panel panelCentral = null!;
        private Label lblBoasVindas = null!;
        private Label lblInfo = null!;
        private Timer timerRelogio = null!;
        private readonly SessionManager _sessionManager;

        public FormPrincipal(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;

            InitializeComponent();
            ConfigurarInterface();
            ExibirInformacoesUsuario();
            IniciarRelogioStatus();
        }

        private void ConfigurarInterface()
        {
            this.Text = "CATI - Desktop Manager";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            menuPrincipal = new MenuStrip();
            menuPrincipal.BackColor = Color.FromArgb(255, 221, 87);
            menuPrincipal.Font = new Font("Segoe UI", 11);
            menuPrincipal.Padding = new Padding(10, 5, 0, 5);

            menuChamados = new ToolStripMenuItem("📋 Chamados");
            menuChamados.DropDownItems.Add("Listar Todos", null, MenuChamadosListar_Click);
            menuChamados.DropDownItems.Add("Criar Novo", null, MenuChamadosNovo_Click);
            menuChamados.DropDownItems.Add(new ToolStripSeparator());
            menuChamados.DropDownItems.Add("Atribuir Técnico", null, MenuChamadosAtribuir_Click);
            menuPrincipal.Items.Add(menuChamados);

            menuUsuarios = new ToolStripMenuItem("👥 Usuários");
            menuUsuarios.DropDownItems.Add("Listar Usuários", null, MenuUsuariosListar_Click);
            menuUsuarios.DropDownItems.Add("Cadastrar Novo", null, MenuUsuariosNovo_Click);
            menuPrincipal.Items.Add(menuUsuarios);

            menuRelatorios = new ToolStripMenuItem("📊 Relatórios");
            menuRelatorios.DropDownItems.Add("Chamados por Status", null, MenuRelatoriosStatus_Click);
            menuRelatorios.DropDownItems.Add("Chamados por Técnico", null, MenuRelatoriosTecnico_Click);
            menuRelatorios.DropDownItems.Add("Satisfação Geral", null, MenuRelatoriosSatisfacao_Click);
            menuPrincipal.Items.Add(menuRelatorios);

            menuSair = new ToolStripMenuItem("🚪 Sair");
            menuSair.Click += MenuSair_Click;
            menuPrincipal.Items.Add(menuSair);

            this.Controls.Add(menuPrincipal);
            this.MainMenuStrip = menuPrincipal;

            panelCentral = new Panel();
            panelCentral.Dock = DockStyle.Fill;
            panelCentral.Padding = new Padding(30);
            panelCentral.BackColor = Color.White;

            lblBoasVindas = new Label();
            lblBoasVindas.Text = $"Bem-vindo(a), {_sessionManager.Usuario?.Nome ?? "Usuário"}!";
            lblBoasVindas.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblBoasVindas.ForeColor = Color.FromArgb(51, 51, 51);
            lblBoasVindas.AutoSize = true;
            lblBoasVindas.Location = new Point(30, 30);
            panelCentral.Controls.Add(lblBoasVindas);

            lblInfo = new Label();
            lblInfo.Text = "Selecione uma opção no menu acima para começar.\n\n" +
                             "📋 Chamados - Gerencie todos os chamados do sistema\n" +
                             "👥 Usuários - Administre usuários e técnicos\n" +
                             "📊 Relatórios - Visualize estatísticas e métricas\n\n" +
                             $"Perfil: {_sessionManager.Usuario?.Perfil?? "N/A"}";
            lblInfo.Font = new Font("Segoe UI", 12);
            lblInfo.ForeColor = Color.FromArgb(100, 100, 100);
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(30, 100);
            panelCentral.Controls.Add(lblInfo);

            this.Controls.Add(panelCentral);

            statusBar = new StatusStrip();
            statusBar.BackColor = Color.FromArgb(240, 240, 240);

            lblStatusUsuario = new ToolStripStatusLabel();
            lblStatusUsuario.Text = $"Usuário: {_sessionManager.Usuario?.Username ?? "N/A"}";
            lblStatusUsuario.Spring = true;
            lblStatusUsuario.TextAlign = ContentAlignment.MiddleLeft;
            statusBar.Items.Add(lblStatusUsuario);

            lblStatusHora = new ToolStripStatusLabel();
            lblStatusHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            statusBar.Items.Add(lblStatusHora);

            this.Controls.Add(statusBar);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ExibirInformacoesUsuario()
        {
            if (_sessionManager.Usuario != null)
            {
                this.Text = $"CATI - Desktop Manager - {_sessionManager.Usuario.Nome}";
            }
        }

        private void IniciarRelogioStatus()
        {
            timerRelogio = new Timer();
            timerRelogio.Interval = 1000;
            timerRelogio.Tick += (s, e) =>
            {
                lblStatusHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            timerRelogio.Start();
        }

        private void MenuChamadosListar_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade: Listar Chamados\\n\\n" +
                          "Aqui será exibida a tela com DataGridView de todos os chamados.\\n" +
                          "Implemente FormChamados.cs para esta funcionalidade.",
                          "Em Desenvolvimento",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void MenuChamadosNovo_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade: Criar Novo Chamado\\n\\n" +
                          "Formulário para criar um novo chamado manualmente.",
                          "Em Desenvolvimento",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void MenuChamadosAtribuir_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade: Atribuir Técnico\\n\\n" +
                          "Permite atribuir um chamado a um técnico específico.",
                          "Em Desenvolvimento",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void MenuUsuariosListar_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade: Listar Usuários\\n\\n" +
                          "Tela com DataGridView de todos os usuários do sistema.\\n" +
                          "Implemente FormUsuarios.cs para esta funcionalidade.",
                          "Em Desenvolvimento",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void MenuUsuariosNovo_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade: Cadastrar Novo Usuário\\n\\n" +
                          "Formulário para cadastrar clientes, técnicos e gestores.",
                          "Em Desenvolvimento",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void MenuRelatoriosStatus_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade: Relatório por Status\\n\\n" +
                          "Gráfico de pizza com chamados por status:\\n" +
                          "- Abertos\\n- Em Andamento\\n- Resolvidos\\n- Fechados",
                          "Em Desenvolvimento",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void MenuRelatoriosTecnico_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade: Relatório por Técnico\\n\\n" +
                          "Lista de técnicos com quantidade de chamados atribuídos.",
                          "Em Desenvolvimento",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void MenuRelatoriosSatisfacao_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade: Satisfação Geral\\n\\n" +
                          "Média de avaliações e gráfico de satisfação dos clientes.",
                          "Em Desenvolvimento",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void MenuSair_Click(object? sender, EventArgs e)
        {
            var resultado = MessageBox.Show(
                "Deseja realmente sair do sistema?",
                "Confirmar Saída",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                _sessionManager.Limpar();
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (timerRelogio != null)
            {
                timerRelogio.Stop();
                timerRelogio.Dispose();
            }
            _sessionManager.Limpar();
            base.OnFormClosing(e);
        }
    }
}
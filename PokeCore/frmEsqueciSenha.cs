using PokeCore.BLL;
using PokeCore.Utils;
using System;
using System.Windows.Forms;

namespace PokeCore.DesktopUI
{
    public partial class frmEsqueciSenha : Form
    {
        private readonly TreinadorServiceBLL _treinadorService;

        public frmEsqueciSenha()
        {
            InitializeComponent();
            _treinadorService = new TreinadorServiceBLL();

            pnlReset.Enabled = false;
        }

        private void btnLogarAdmin_Click(object sender, EventArgs e)
        {
            string adminUser = txtAdminUser.Text;
            string adminPass = txtAdminPass.Text;

            if (string.IsNullOrWhiteSpace(adminUser) || string.IsNullOrWhiteSpace(adminPass))
            {
                MessageBox.Show("Preencha os campos de admin.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var admin = _treinadorService.(adminUser, adminPass);

            if (admin != null && admin.IsAdmin)
            {
                MessageBox.Show("Admin autenticado. Prossiga com o reset.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                pnlReset.Enabled = true;
            }
            else
            {
                MessageBox.Show("Credenciais de admin inválidas ou usuário não é admin.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterarSenha_Click(object sender, EventArgs e)
        {
            string usuarioParaResetar = txtUsuarioReset.Text;
            string novaSenha = txtNovaSenha.Text;
            string confirmarSenha = txtConfirmarSenha.Text;

            if (string.IsNullOrWhiteSpace(usuarioParaResetar) || string.IsNullOrWhiteSpace(novaSenha))
            {
                MessageBox.Show("Preencha o nome do usuário e a nova senha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (novaSenha != confirmarSenha)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PasswordHelper.ValidatePasswordStrength(novaSenha);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Senha Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool sucesso = _treinadorService.ResetPasswordByAdmin(usuarioParaResetar, novaSenha);

                if (sucesso)
                {
                    MessageBox.Show($"Senha do usuário '{usuarioParaResetar}' foi alterada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Usuário '{usuarioParaResetar}' não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

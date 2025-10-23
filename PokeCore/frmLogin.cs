using PokeCore.BLL;
using PokeCore.DTO;

namespace PokeCore.DesktopUI
{
    public partial class frmLogin : Form
    {
        private TreinadorServiceBLL _bll;
        public frmLogin()
        {
            InitializeComponent();
            _bll = new TreinadorServiceBLL();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                string identifier = txtUsuario.Text.Trim();
                string senha = txtSenha.Text.Trim();

                if (string.IsNullOrEmpty(identifier) || string.IsNullOrEmpty(senha))
                {
                    MessageBox.Show("Usuário e senha são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TreinadorDTO treinadorLogado = _bll.Login(identifier, senha);

                frmMain mainForm = new frmMain(treinadorLogado);
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                mdAvisoFalhaLogin.Show("Erro de Login", ex.Message);
            }
        }

        private void lblCadastro_Click(object sender, EventArgs e)
        {
            using (frmCadastro formCadastro = new frmCadastro())
            {
                formCadastro.ShowDialog();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {

            this.MinimizeBox = true;

        }

        private void lblEsqueceuSenha_Click(object sender, EventArgs e)
        {
            using (frmEsqueciSenha frmReset = new frmEsqueciSenha())
            {
                frmReset.ShowDialog();
            }
        }
    }
}

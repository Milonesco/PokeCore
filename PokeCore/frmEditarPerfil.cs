using PokeCore.BLL;
using PokeCore.DTO;
using System.IO;

namespace PokeCore.DesktopUI
{
    public partial class frmEditarPerfil : Form
    {
        private int _idTreinadorLogado;
        private TreinadorDTO _treinadorAtual;
        private TreinadorServiceBLL treinadorService = new TreinadorServiceBLL();
        public frmEditarPerfil(int idTreinador)
        {
            InitializeComponent();
            _idTreinadorLogado = idTreinador;
        }
        private void frmEditarPerfil_Load(object sender, EventArgs e)
        {
            try
            {
                _treinadorAtual = treinadorService.GetTreinadorById(_idTreinadorLogado);

                if (_treinadorAtual == null)
                {
                    MessageBox.Show("Não foi possível carregar os dados do treinador.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                txtNome.Text = _treinadorAtual.Username;
                txtEmail.Text = _treinadorAtual.Email;
                txtDisplayName.Text = _treinadorAtual.DisplayName;

                if (!string.IsNullOrEmpty(_treinadorAtual.FotoPath) && File.Exists(_treinadorAtual.FotoPath))
                {
                    pbFoto.Image = Image.FromFile(_treinadorAtual.FotoPath);
                }
                else
                {
                    pbFoto.Image = Properties.Resources.poke_logo_colored;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar perfil: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSenha.Text))
                {
                    if (txtSenha.Text != txtConfirmarSenha.Text)
                    {
                        MessageBox.Show("As novas senhas não conferem!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _treinadorAtual.Password = txtSenha.Text;
                }

                _treinadorAtual.Username = txtNome.Text;
                _treinadorAtual.Email = txtEmail.Text;
                _treinadorAtual.DisplayName = txtDisplayName.Text;

                treinadorService.UpdateTreinador(_treinadorAtual);

                MessageBox.Show("Perfil atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar alterações: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

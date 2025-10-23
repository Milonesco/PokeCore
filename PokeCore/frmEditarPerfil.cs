using PokeCore.BLL;
using PokeCore.DTO;
using System.IO;

namespace PokeCore.DesktopUI
{
    public partial class frmEditarPerfil : Form
    {

        private TreinadorServiceBLL _bll;
        private TreinadorDTO _treinadorAtual;
        private string diretorioFotos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "fotos");
        private string caminhoFotoSelecionadaOriginal = null;
        private string caminhoFotoSalva = null;

        public frmEditarPerfil(TreinadorDTO treinador)
        {
            InitializeComponent();
            _bll = new TreinadorServiceBLL();
            _treinadorAtual = treinador;
            CarregarDados();
        }

        private void CarregarDados()
        {
            if (_treinadorAtual == null)
            {
                MessageBox.Show("Dados do treinador não fornecidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtNome.Text = _treinadorAtual.Username;
            txtEmail.Text = _treinadorAtual.Email;
            txtDisplayName.Text = _treinadorAtual.DisplayName;

            txtSenha.Text = string.Empty;
            txtConfirmarSenha.Text = string.Empty;

            CarregarFotoAtual();
        }

        private void CarregarFotoAtual()
        {
            string fotoPath = _treinadorAtual?.FotoPath;
            string defaultFotoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "poke_logo_colored.png");

            try
            {
                string caminhoParaCarregar = defaultFotoPath;
                if (!string.IsNullOrWhiteSpace(fotoPath) && File.Exists(fotoPath))
                {
                    caminhoParaCarregar = fotoPath;
                }
                else if (!File.Exists(defaultFotoPath))
                {
                    pbFoto.Image = null;
                    Console.WriteLine($"AVISO frmEditarPerfil: Imagem padrão não encontrada em '{defaultFotoPath}'.");
                    return;
                }

                using (var bmpTemp = new Bitmap(caminhoParaCarregar))
                {
                    pbFoto.Image = new Bitmap(bmpTemp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar foto '{fotoPath ?? defaultFotoPath}' no frmEditarPerfil: {ex.Message}");
                pbFoto.Image = null;
                try
                {
                    if (File.Exists(defaultFotoPath))
                    {
                        using (var bmpTemp = new Bitmap(defaultFotoPath)) { pbFoto.Image = new Bitmap(bmpTemp); }
                    }
                }
                catch { }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            if (_treinadorAtual == null)
            {
                MessageBox.Show("Erro interno: Dados do treinador não disponíveis.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool mudarSenha = !string.IsNullOrWhiteSpace(txtSenha.Text) || !string.IsNullOrWhiteSpace(txtConfirmarSenha.Text);
            if (mudarSenha)
            {
                if (txtSenha.Text != txtConfirmarSenha.Text)
                {
                    MessageBox.Show("As novas senhas não coincidem!", "Erro Senha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    string senhaAntiga = txtSenhaAntiga.Text;
                    _bll.ChangePassword(_treinadorAtual.Id, senhaAntiga, txtSenha.Text.Trim());
                    mudarSenha = false;
                }
                catch (Exception exSenha)
                {
                    MessageBox.Show("Erro ao tentar validar/mudar a senha: " + exSenha.Message, "Erro Senha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {

                string fotoPathFinal = _treinadorAtual.FotoPath;
                if (!string.IsNullOrEmpty(caminhoFotoSelecionadaOriginal) && pbFoto.Image != null)
                    _treinadorAtual.FotoPath = fotoPathFinal;


                _treinadorAtual.Username = txtNome.Text.Trim();
                _treinadorAtual.Email = txtEmail.Text.Trim();
                _treinadorAtual.DisplayName = txtDisplayName.Text.Trim();
                _treinadorAtual.Password = txtConfirmarSenha.Text.Trim();


                _bll.UpdateUserProfile(_treinadorAtual);

                MessageBox.Show("Perfil atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show("Erro de validação: " + argEx.Message, "Dados Inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar alterações: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var confirmacao = mdConfirma.Show("Você realmente deseja sair?");
            if (confirmacao == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.MinimizeBox = true;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMain telaMain = new(_treinadorAtual);
            telaMain.ShowDialog();
        }
    }
}

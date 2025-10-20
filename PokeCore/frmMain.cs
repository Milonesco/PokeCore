using PokeCore.DesktopUI;
using PokeCore.BLL;
using PokeCore.DTO;
using System.Drawing;
using System.IO;

namespace PokeCore
{
    public partial class frmMain : Form
    {
        private TreinadorDTO _treinadorLogado;
        public frmMain(TreinadorDTO treinadorLogado)
        {
            InitializeComponent();
            _treinadorLogado = treinadorLogado;

            AtualizarUsuarioLogado();
            AbrirUserControl(new ucHome(_treinadorLogado));
        }

        private void AbrirUserControl(UserControl uc)
        {
            panelConteudo.Controls.Clear();

            uc.Dock = DockStyle.Fill;

            panelConteudo.Controls.Add(uc);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AbrirUserControl(new ucHome(_treinadorLogado));
        }



        private void AtualizarUsuarioLogado()
        {
            if (_treinadorLogado == null)
            {
                lblDisplayName.Text = _treinadorLogado.DisplayName;
                lblDisplayName.TextAlignment = ContentAlignment.MiddleCenter;

                string fotoPath = _treinadorLogado.FotoPath;
                string fotoPadraoPath = "img/poke_logo_colored.png";

                try
                {
                    string caminhoParaCarregar = fotoPadraoPath;

                    if (!string.IsNullOrWhiteSpace(fotoPath) && File.Exists(fotoPath))
                    {
                        caminhoParaCarregar = fotoPath;
                    }
                    else if (!File.Exists(caminhoParaCarregar))
                    {
                        MessageBox.Show($"Aviso: A imagem padrão não foi encontrada em {fotoPadraoPath}");
                        pbUsuario.Image = null;
                        return;
                    }

                    using (var bmpTmp = new Bitmap(caminhoParaCarregar))
                    {
                        pbUsuario.Image = new Bitmap(bmpTmp);
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show($"Erro: Não foi possível encontrar o arquivo de imagem em '{fotoPath ?? fotoPadraoPath}'. Verifique o caminho.", "Erro ao Carregar Imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pbUsuario.Image = null;
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show($"Erro: O arquivo de imagem em '{fotoPath ?? fotoPadraoPath}' parece ser inválido ou corrompido.", "Erro ao Carregar Imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pbUsuario.Image = null;
                    if (!string.IsNullOrWhiteSpace(fotoPath) && File.Exists(fotoPadraoPath))
                    {
                        try
                        {
                            using (var bmpTemp = new Bitmap(fotoPadraoPath)) { pbUsuario.Image = new Bitmap(bmpTemp); }
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro inesperado ao carregar imagem: {ex.Message}", "Erro ao Carregar Imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pbUsuario.Image = null;
                    if (File.Exists(fotoPadraoPath))
                    {
                        try
                        {
                            using (var bmpTemp = new Bitmap(fotoPadraoPath)) { pbUsuario.Image = new Bitmap(bmpTemp); }
                        }
                        catch {}
                    }
                }
            }
        }
    }
}

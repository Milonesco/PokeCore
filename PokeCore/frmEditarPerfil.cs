using System.Drawing.Imaging;
using PokeCore.BLL;
using PokeCore.DTO;

namespace PokeCore.DesktopUI
{
    public partial class frmEditarPerfil : Form
    {

        private TreinadorServiceBLL _bll;
        private TreinadorDTO _treinadorAtual;
        private string diretorioFotos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "fotos");
        private string caminhoFotoSelecionadaOriginal = null;
        private string caminhoFotoSalva = null;
        private string caminhoFotoRelativo = null;

        public frmEditarPerfil(TreinadorDTO treinador)
        {
            InitializeComponent();
            _bll = new TreinadorServiceBLL();
            _treinadorAtual = treinador;
            diretorioFotos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "fotos");

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
                        using (var bmpTemp = new Bitmap(defaultFotoPath))
                        { pbFoto.Image = new Bitmap(bmpTemp); }
                    }
                }
                catch { }
            }
        }

        private void pbFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Filter = "Arquivos de Imagem (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Selecione uma nova foto de perfil";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        caminhoFotoSelecionadaOriginal = openFileDialog.FileName;

                        Image oldImage = pbFoto.Image;
                        using (var bmpTemp = new Bitmap(caminhoFotoSelecionadaOriginal))
                        {
                            pbFoto.Image = new Bitmap(bmpTemp);
                        }
                        oldImage?.Dispose();

                        caminhoFotoSalva = null;
                        caminhoFotoRelativo = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar a imagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        caminhoFotoSelecionadaOriginal = null;
                        CarregarFotoAtual();
                    }
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtDisplayName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Todos os campos (exceto senha) são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Lógica da Foto ---
            string fotoPathParaSalvar = _treinadorAtual.FotoPath;
            string caminhoAntigoAbsoluto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _treinadorAtual.FotoPath ?? "");
            bool fotoNovaSalvaComSucesso = false;


            if (!string.IsNullOrEmpty(caminhoFotoSelecionadaOriginal) && pbFoto.Image != null)
            {
                try
                {
                    if (!Directory.Exists(diretorioFotos))
                    {
                        Directory.CreateDirectory(diretorioFotos);
                    }

                    string extensao = Path.GetExtension(caminhoFotoSelecionadaOriginal);
                    string nomeArquivoUnico = $"{Guid.NewGuid()}{extensao}";

                    caminhoFotoRelativo = Path.Combine("data", "fotos", nomeArquivoUnico);

                    caminhoFotoSalva = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, caminhoFotoRelativo);

                    using (Bitmap bmpOriginal = new Bitmap(caminhoFotoSelecionadaOriginal))
                    {
                        ImageFormat formato = extensao.ToLower() == ".png" ? ImageFormat.Png : ImageFormat.Jpeg;
                        bmpOriginal.Save(caminhoFotoSalva, formato);
                    }

                    fotoPathParaSalvar = caminhoFotoRelativo;
                    fotoNovaSalvaComSucesso = true;

                }
                catch (Exception exSave)
                {
                    MessageBox.Show($"Erro ao salvar a nova imagem: {exSave.Message}", "Erro de Imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {

                var dadosAtualizados = new TreinadorDTO
                {
                    Id = _treinadorAtual.Id,
                    Username = txtNome.Text.Trim(),
                    DisplayName = txtDisplayName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    FotoPath = fotoPathParaSalvar,
                    IsAdmin = _treinadorAtual.IsAdmin,
                    Password = _treinadorAtual.Password
                };

                _bll.UpdateUserProfile(dadosAtualizados);

                if (fotoNovaSalvaComSucesso &&
                    !string.IsNullOrEmpty(_treinadorAtual.FotoPath) &&
                    _treinadorAtual.FotoPath != fotoPathParaSalvar &&
                    File.Exists(caminhoAntigoAbsoluto) &&
                    !caminhoAntigoAbsoluto.Contains("poke_logo_colored.png"))
                {
                    try
                    {
                        Image imgAntigaPb = pbFoto.Image;
                        pbFoto.Image = null;
                        imgAntigaPb?.Dispose();

                        File.Delete(caminhoAntigoAbsoluto);

                        if (!string.IsNullOrEmpty(caminhoFotoSalva) && File.Exists(caminhoFotoSalva))
                        {
                            using (FileStream fs = new FileStream(caminhoFotoSalva, FileMode.Open, FileAccess.Read))
                            {
                                pbFoto.Image = Image.FromStream(fs);
                            }
                        }

                    }
                    catch (IOException ioEx)
                    {
                        Console.WriteLine($"Aviso: Não foi possível deletar a foto antiga '{caminhoAntigoAbsoluto}'. Pode estar em uso. Erro: {ioEx.Message}");
                    }
                    catch (Exception exDel)
                    {
                        Console.WriteLine($"Erro ao deletar foto antiga: {exDel.Message}");
                    }
                }

                _treinadorAtual.Username = dadosAtualizados.Username;
                _treinadorAtual.DisplayName = dadosAtualizados.DisplayName;
                _treinadorAtual.Email = dadosAtualizados.Email;
                _treinadorAtual.FotoPath = dadosAtualizados.FotoPath;

                frmMain mainForm = Application.OpenForms.OfType<frmMain>().FirstOrDefault();
                mainForm?.AtualizarDadosUsuarioLogado(_treinadorAtual); // (Você precisaria criar este método no frmMain)

                MessageBox.Show("Perfil atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show("Erro de validação: " + argEx.Message, "Atualização Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ReverterSalvamentoFotoNova(fotoPathParaSalvar);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar perfil: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReverterSalvamentoFotoNova(fotoPathParaSalvar);
            }
        }

        private void ReverterSalvamentoFotoNova(string novoFotoPathRelativo)
        {
            string caminhoNovoAbsoluto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, novoFotoPathRelativo ?? "");
            string caminhoAntigoRelativo = _treinadorAtual.FotoPath;

            if (!string.IsNullOrEmpty(novoFotoPathRelativo) &&
                novoFotoPathRelativo != caminhoAntigoRelativo &&
                File.Exists(caminhoNovoAbsoluto))
            {
                try
                {
                    Image imgPb = pbFoto.Image;
                    pbFoto.Image = null;
                    imgPb?.Dispose();

                    File.Delete(caminhoNovoAbsoluto);
                    Console.WriteLine($"Nova foto '{caminhoNovoAbsoluto}' revertida devido a erro.");

                    CarregarFotoAtual();

                }
                catch (Exception exRev)
                {
                    Console.WriteLine($"Erro ao reverter salvamento da foto: {exRev.Message}");
                }
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
    }
}

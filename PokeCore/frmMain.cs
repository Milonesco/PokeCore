using PokeCore.BLL;
using PokeCore.DesktopUI;
using PokeCore.DTO;

namespace PokeCore
{
    public partial class frmMain : Form
    {
        private TreinadorServiceBLL _bll;
        private PokemonServiceBLL _pokemonBLL;
        private TreinadorDTO _treinadorLogado;
        private bool isTreinadorAdmin;
        private List<Guna.UI2.WinForms.Guna2CircleButton> menuButtons;
        public frmMain(TreinadorDTO treinadorLogado)
        {
            InitializeComponent();
            _treinadorLogado = treinadorLogado;
            _bll = new TreinadorServiceBLL();
            _pokemonBLL = new PokemonServiceBLL();
            this.txtPesquisa.TextChanged += new System.EventHandler(this.txtPesquisa_TextChanged);

            menuButtons = new List<Guna.UI2.WinForms.Guna2CircleButton>
            {
                btnHome, btnPcBox, btnEditarTime, btnTreinadores
            };

            AtualizarUsuarioLogado();
            AbrirUserControl(new ucHome(_treinadorLogado));
        }

        private void SetActiveButton(Guna.UI2.WinForms.Guna2CircleButton activeButton)
        {
            foreach (var button in menuButtons)
            {
                button.Checked = false;
            }
            activeButton.Checked = true;
        }

        private void AbrirUserControl(UserControl uc)
        {
            panelConteudo.Controls.Clear();

            uc.Dock = DockStyle.Fill;

            panelConteudo.Controls.Add(uc);

            if (uc is ucPcBox || uc is ucEditarTime || uc is ucTreinadores)
            {
                txtPesquisa.Visible = true;
                txtPesquisa.Clear();
                if (uc is ucTreinadores)
                {
                    txtPesquisa.PlaceholderText = "Pesquisar Treinador...";
                }
                else
                {
                    txtPesquisa.PlaceholderText = "Pesquisar Pokémon...";
                }
            }
            else
            {
                txtPesquisa.Visible = false;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnHome);
            AbrirUserControl(new ucHome(_treinadorLogado));
        }

        private void btnPcBox_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnPcBox);
            panelConteudo.Controls.Clear();
            AbrirUserControl(new ucPcBox(_treinadorLogado.Id));
        }

        private void btnEditarTime_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnEditarTime);
            panelConteudo.Controls.Clear();

            ucEditarTime ucTime = new ucEditarTime(_treinadorLogado);
            ucTime.Dock = DockStyle.Fill;
            ucTime.OnEditarPokemonRequisitado += UcTime_OnEditarPokemonRequisitado;
            panelConteudo.Controls.Add(ucTime);
        }

        private void UcTime_OnEditarPokemonRequisitado(PokemonDTO pokemonParaEditar)
        {
            panelConteudo.Controls.Clear();

            ucEditarPokemon ucEditPoke = new ucEditarPokemon(pokemonParaEditar, _treinadorLogado, _bll, _pokemonBLL);
            ucEditPoke.Dock = DockStyle.Fill;

            ucEditPoke.OnEdicaoConcluida += () =>
            {
                btnEditarTime_Click(this, EventArgs.Empty);
            };

            panelConteudo.Controls.Add(ucEditPoke);
        }

        private void btnTreinadores_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTreinadores);
            panelConteudo.Controls.Clear();
            AbrirUserControl(new ucTreinadores(_treinadorLogado));
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            var confirmacao = mdConfirma.Show("Você realmente deseja sair?");
            if (confirmacao == DialogResult.Yes)
            {
                FecharMain();
            }
            else
            {
                return;
            }
        }



        private void FecharMain()
        {
            Close();
            frmLogin telaLogin = new();
            telaLogin.ShowDialog();
        }


        private void AtualizarUsuarioLogado()
        {
            if (_treinadorLogado != null)
            {
                lblDisplayName.Text = _treinadorLogado.DisplayName;
                lblDisplayName.TextAlignment = ContentAlignment.MiddleCenter;

                string fotoPath = _treinadorLogado.FotoPath;
                string fotoPadraoPath = "img/poke_logo_colored.png";


                lblDisplayName.Left = pbUsuario.Left + (pbUsuario.Width - lblDisplayName.Width) / 2;

                lblDisplayName.Top = pbUsuario.Bottom + 4;

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
                            using (var bmpTemp = new Bitmap(fotoPadraoPath))
                            { pbUsuario.Image = new Bitmap(bmpTemp); }
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
                            using (var bmpTemp = new Bitmap(fotoPadraoPath))
                            { pbUsuario.Image = new Bitmap(bmpTemp); }
                        }
                        catch { }
                    }
                }

                btnGerenciarPokemon.Visible = _treinadorLogado.IsAdmin;
                lblGerenciarPokemon.Visible = _treinadorLogado.IsAdmin;
                btnTreinadores.Visible = _treinadorLogado.IsAdmin;
                lblTreinadores.Visible = _treinadorLogado.IsAdmin;
            }
        }

        private void pbConfig_Click(object sender, EventArgs e)
        {
            if (_treinadorLogado == null)
            {
                MessageBox.Show("Não há usuário logado para editar o perfil.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            using (frmEditarPerfil editarPerfil = new frmEditarPerfil(_treinadorLogado))
            {
                DialogResult result = editarPerfil.ShowDialog();

                if (result == DialogResult.OK)
                {
                    try
                    {
                        _treinadorLogado = _bll.GetTreinadorById(_treinadorLogado.Id);
                        AtualizarUsuarioLogado();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao recarregar dados do usuário após edição: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.MinimizeBox = true;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            string termoPesquisa = txtPesquisa.Text.Trim();

            if (panelConteudo.Controls.Count > 0)
            {
                Control activeControl = panelConteudo.Controls[0];

                if (activeControl is ucPcBox ucPc)
                {
                    ucPc.PesquisarPokemon(termoPesquisa);
                }
                else if (activeControl is ucEditarTime ucTime)
                {
                    ucTime.PesquisarPokemon(termoPesquisa);
                }
                else if (activeControl is ucGerenciarPokemons ucPokes)
                {
                    ucPokes.PesquisarPokemon(termoPesquisa);
                }
                else if (activeControl is ucTreinadores ucTrainers)
                {
                    ucTrainers.PesquisarTreinador(termoPesquisa);
                }
            }
        }

        public void AbrirGerenciadorTreinador(TreinadorDTO adminLogado, TreinadorDTO treinadorAlvo)
        {
            ucAdminGerenciarTreinador ucGerenciar = new ucAdminGerenciarTreinador(_bll, adminLogado, treinadorAlvo);

            AbrirUserControl(ucGerenciar);
        }

        public void AbrirUcTreinadores()
        {
            ucTreinadores uc = new ucTreinadores(_treinadorLogado);
            AbrirUserControl(uc);
        }

        public void AtualizarDadosUsuarioLogado(TreinadorDTO treinadorAtualizado)
        {
            _treinadorLogado = treinadorAtualizado;

            lblDisplayName.Text = _treinadorLogado.DisplayName ?? _treinadorLogado.Username;

            CarregarFotoPerfilPrincipal();
            CenterLabelUnderPictureBox(lblDisplayName, pbUsuario);
        }

        private void CarregarFotoPerfilPrincipal()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string fotoPathCompleto = Path.Combine(basePath, _treinadorLogado.FotoPath ?? "");


            Image oldImage = pbUsuario.Image;
            pbUsuario.Image = null;
            oldImage?.Dispose();

            if (!string.IsNullOrEmpty(_treinadorLogado.FotoPath) && File.Exists(fotoPathCompleto))
            {
                try
                {
                    using (FileStream fs = new FileStream(fotoPathCompleto, FileMode.Open, FileAccess.Read))
                    {
                        pbUsuario.Image = Image.FromStream(fs);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao carregar foto principal: {ex.Message}");
                    string fotoPadraoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img/poke_logo_colored.png");
                    if (File.Exists(fotoPadraoPath))
                    {
                        using (var bmpTemp = new Bitmap(fotoPadraoPath))
                        {
                            pbUsuario.Image = new Bitmap(bmpTemp);
                        }
                    }
                    else
                    {
                        pbUsuario.Image = null;
                    }
                }
            }
            else
            {
                string fotoPadraoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img/poke_logo_colored.png");
                if (File.Exists(fotoPadraoPath))
                {
                    using (var bmpTemp = new Bitmap(fotoPadraoPath))
                    {
                        pbUsuario.Image = new Bitmap(bmpTemp);
                    }
                }
                else
                {
                    pbUsuario.Image = null;
                }
            }
        }

        private void CenterLabelUnderPictureBox(Control label, Control pictureBox, int verticalSpacing = 4)
        {
            if (label == null || pictureBox == null || label.Parent == null)
                return;

            label.Parent.PerformLayout();

            label.Left = pictureBox.Left + (pictureBox.Width - label.Width) / 2;
            label.Top = pictureBox.Bottom + verticalSpacing;
        }

        private void btnGerenciarPokemon_Click(object sender, EventArgs e) // Ajuste o nome do botão
        {
            AbrirGerenciarPokemons();
        }

        public void AbrirEditarPokemonAdmin(PokemonDTO pokemon)
        {
            if (pokemon == null)
                return;
            ucEditarPokemon ucEditPoke = new ucEditarPokemon(pokemon, _treinadorLogado, _bll, _pokemonBLL);
            txtPesquisa.Visible = false;
        }

        public void AbrirGerenciarPokemons()
        {
            ucGerenciarPokemons uc = new ucGerenciarPokemons(_bll, _pokemonBLL, _treinadorLogado);
            AbrirUserControl(uc);
            txtPesquisa.Visible = true;
            txtPesquisa.PlaceholderText = "Pesquisar Pokémon por ID, Nome ou Apelido...";
            txtPesquisa.Clear();
        }
    }
}

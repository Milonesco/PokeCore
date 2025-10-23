using PokeCore.BLL;
using PokeCore.DTO;

namespace PokeCore.DesktopUI
{
    public partial class ucAdminGerenciarTreinador : UserControl
    {
        private TreinadorServiceBLL _bll;
        private TreinadorDTO _adminLogado;
        private TreinadorDTO _treinadorAlvo;

        private ucPokemonIcon _iconeSelecionado = null;

        public ucAdminGerenciarTreinador(TreinadorServiceBLL bll, TreinadorDTO adminLogado, TreinadorDTO treinadorAlvo)
        {
            InitializeComponent();
            _bll = bll;
            _adminLogado = adminLogado;
            _treinadorAlvo = treinadorAlvo;
        }

        private void ucAdminGerenciarTreinador_Load(object sender, EventArgs e)
        {
            CarregarDadosTreinador();
            CarregarPokemon();
        }

        private void CarregarDadosTreinador()
        {
            if (_treinadorAlvo == null) return;

            txtID.Text = _treinadorAlvo.Id.ToString();
            txtUsername.Text = _treinadorAlvo.Username;
            txtEmail.Text = _treinadorAlvo.Email;
            txtDisplayName.Text = _treinadorAlvo.DisplayName;
            txtDataCriacao.Text = _treinadorAlvo.CreatedAt.ToString("g");
            chkIsAdmin.Checked = _treinadorAlvo.IsAdmin;

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string fotoPath = Path.Combine(basePath, _treinadorAlvo.FotoPath ?? "");

            if (!string.IsNullOrEmpty(_treinadorAlvo.FotoPath) && File.Exists(fotoPath))
            {
                pbFotoPerfil.Image = Image.FromFile(fotoPath);
            }
            else
            {
                pbFotoPerfil.Image = Properties.Resources.poke_logo_colored;
            }
        }

        private void CarregarPokemon()
        {
            flpActiveTeam.Controls.Clear();
            flpPcBox.Controls.Clear();
            DesmarcarPokemon();

            try
            {
                List<PokemonDTO> timeAtivo = _bll.GetTreinadorActiveTeam(_treinadorAlvo.Id);
                List<PokemonDTO> pcBox = _bll.GetTreinadorPcBox(_treinadorAlvo.Id);

                foreach (var pokemon in timeAtivo)
                {
                    ucPokemonIcon icon = new ucPokemonIcon();
                    icon.SetPokemon(pokemon);
                    icon.Click += PokemonIcon_Click;
                    flpActiveTeam.Controls.Add(icon);
                }

                foreach (var pokemon in pcBox)
                {
                    ucPokemonIcon icon = new ucPokemonIcon();
                    icon.SetPokemon(pokemon);
                    icon.Click += PokemonIcon_Click;
                    flpPcBox.Controls.Add(icon);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar Pokémon: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PokemonIcon_Click(object sender, EventArgs e)
        {
            DesmarcarPokemon();

            _iconeSelecionado = sender as ucPokemonIcon;
            if (_iconeSelecionado != null)
            {
                _iconeSelecionado.BackColor = Color.FromArgb(221, 23, 58);
                btnReleasePokemon.Enabled = true;
            }
        }

        private void btnReleasePokemon_Click(object sender, EventArgs e)
        {
            if (_iconeSelecionado == null || _iconeSelecionado.PokemonData == null)
            {
                MessageBox.Show("Nenhum Pokémon selecionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nomePoke = _iconeSelecionado.PokemonData.Nickname ?? _iconeSelecionado.PokemonData.Nome;
            DialogResult confirm = MessageBox.Show(
                $"Você tem certeza que deseja liberar o Pokémon '{nomePoke}' (ID: {_iconeSelecionado.PokemonData.Id}) do treinador '{_treinadorAlvo.Username}'?\n\nEsta ação não pode ser desfeita.",
                "Confirmar Liberação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _bll.AdminReleasePokemon(_adminLogado.Id, _iconeSelecionado.PokemonData.Id);
                    MessageBox.Show("Pokémon liberado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarPokemon();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao liberar Pokémon: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DesmarcarPokemon()
        {
            if (_iconeSelecionado != null)
            {
                _iconeSelecionado.BackColor = Color.Transparent;
            }
            _iconeSelecionado = null;
            btnReleasePokemon.Enabled = false;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMain mainForm = this.ParentForm as frmMain;
            if (mainForm != null)
            {
                mainForm.AbrirUcTreinadores();
            }
        }
    }
}

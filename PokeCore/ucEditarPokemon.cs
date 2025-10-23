using PokeCore.BLL;
using PokeCore.DTO;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PokeCore.DesktopUI
{
    public partial class ucEditarPokemon : UserControl
    {
        private PokemonDTO _pokemon;
        private TreinadorServiceBLL _bll;
        private PokemonServiceBLL _pokemonBLL;
        private TreinadorDTO _treinadorLogado;

        public event Action OnEdicaoConcluida;

        private string imageBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "pokemon");
        private string defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "poke_logo_colored.png");


        public ucEditarPokemon()
        {
            InitializeComponent();
        }

        public ucEditarPokemon(PokemonDTO pokemon, TreinadorDTO treinadorLogado, TreinadorServiceBLL bll, PokemonServiceBLL pokemonBLL)
        {
            InitializeComponent();
            _pokemon = pokemon;
            _treinadorLogado = treinadorLogado;
            _bll = bll;
            _pokemonBLL = pokemonBLL;

            this.Load += UcEditarPokemon_Load;
            this.btnSalvar.Click += BtnSalvar_Click;
            this.btnVoltar.Click += BtnVoltar_Click;
        }

        private void UcEditarPokemon_Load(object sender, EventArgs e)
        {
            if (_pokemon == null)
            {
                MessageBox.Show("Erro fatal: Nenhum Pokémon foi carregado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnEdicaoConcluida?.Invoke();
                return;
            }

            CarregarImagemPokemon();

            lblNomePokemon.Text = _pokemon.Nome;
            chipNivel.Text = $"Nível: {_pokemon.Nivel}";
            chipTipo.Text = _pokemon.Tipo;
            lblPokemonID.Text = $"ID do Pokémon: {_pokemon.Id}";


            txtApelido.Text = _pokemon.Nickname;
            txtLocal.Text = _pokemon.LocalDeCaptura;
            txtDataCaptura.Text = _pokemon.CapturedAt.ToString("dd/MM/yyyy 'às' HH:mm");

            lblNomePokemon.Text = _pokemon.Nome;
            lblNomePokemon.Parent.PerformLayout();
            lblNomePokemon.Left = pnlPokemonImage.Left + (pnlPokemonImage.Width - lblNomePokemon.Width) / 2;
            lblNomePokemon.Top = pnlPokemonImage.Bottom + 4;

            lblPokemonID.Parent.PerformLayout();
            lblPokemonID.Left = pnlPokemonImage.Left + (pnlPokemonImage.Width - lblPokemonID.Width) / 2;
            lblPokemonID.Top = chipNivel.Bottom + 4;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                _pokemon.Nickname = txtApelido.Text.Trim();
                _pokemon.LocalDeCaptura = txtLocal.Text.Trim();

                _pokemonBLL.AdminUpdatePokemon(_treinadorLogado.Id, _pokemon);

                MessageBox.Show("Pokémon atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmMain mainForm = this.ParentForm as frmMain;
                mainForm?.AbrirGerenciarPokemons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar Pokémon: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            OnEdicaoConcluida?.Invoke();
        }

        private void CarregarImagemPokemon()
        {
            string baseName = _pokemon.Nome;

            string[] possibleExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".avif" };
            string foundPath = null;

            foreach (string ext in possibleExtensions)
            {
                string testPath = Path.Combine(imageBasePath, $"{baseName}{ext}");
                if (File.Exists(testPath))
                {
                    foundPath = testPath;
                    break;
                }
            }

            string pathToShow = defaultImagePath;
            if (foundPath != null)
            {
                pathToShow = foundPath;
            }
            else
            {
                Console.WriteLine($"AVISO: Arte do Pokémon não encontrada para {baseName} em '{imageBasePath}'. Usando padrão.");
            }

            try
            {
                using (var bmpTemp = new Bitmap(pathToShow))
                {
                    pbPokemonImage.Image = new Bitmap(bmpTemp);
                }
            }
            catch (Exception ex)
            {
                pbPokemonImage.Image = null;
                Console.WriteLine($"Erro EXCEÇÃO ao carregar imagem '{pathToShow}': {ex.Message}");
            }
        }
    }
}

using PokeCore.BLL;
using PokeCore.DTO;

namespace PokeCore.DesktopUI
{
    public partial class ucPcBox : UserControl
    {
        private TreinadorServiceBLL _bll;
        private int _treinadorId;
        private PokemonDTO _selectedPokemon = null;

        public ucPcBox(int treinadorId)
        {
            InitializeComponent();
            _bll = new TreinadorServiceBLL();
            _treinadorId = treinadorId;
            this.Load += ucPcBox_Load;
        }

        private void ucPcBox_Load(object sender, EventArgs e)
        {
            LoadPcBox();
            pnlPokemonDetails.Visible = false;
        }

        private void LoadPcBox()
        {
            try
            {
                flpPokemonGrid.Controls.Clear();

                List<PokemonDTO> pcPokemons = _bll.GetTreinadorPcBox(_treinadorId);

                foreach (var pokemon in pcPokemons)
                {
                    ucPokemonIcon iconControl = new ucPokemonIcon();
                    iconControl.SetPokemon(pokemon);

                    iconControl.Click += IconControl_Click;

                    flpPokemonGrid.Controls.Add(iconControl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar o PC Box: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IconControl_Click(object sender, EventArgs e)
        {
            if (sender is ucPokemonIcon clickedIcon)
            {
                _selectedPokemon = clickedIcon.PokemonData;

                if (_selectedPokemon != null)
                {
                    DisplayPokemonDetails(_selectedPokemon);
                }
            }
        }

        private void DisplayPokemonDetails(PokemonDTO pokemon)
        {
            string imageBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "pokemon");
            string defaultImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "pokeball_icon.png"); // Imagem padrão

            string imagePath = Path.Combine(imageBasePath, $"{pokemon.Nome}.png");
            try
            {
                string pathToShow = File.Exists(imagePath) ? imagePath : defaultImagePath;
                if (!File.Exists(pathToShow))
                {
                    pbDetailImage.Image = null;
                }
                else
                {
                    using (var bmpTemp = new Bitmap(pathToShow)) { pbDetailImage.Image = new Bitmap(bmpTemp); }
                }
            }
            catch { pbDetailImage.Image = null; }

            lblDetailName.Text = pokemon.Nome;
            lblDetailLevel.Text = "Nível: " + pokemon.Nivel.ToString();
            lblDetailType.Text = "Tipo: " + pokemon.Tipo;
            lblDetailNickname.Text = "Apelido: " + (string.IsNullOrWhiteSpace(pokemon.Nickname) ? "N/A" : pokemon.Nickname);
            lblDetailLocation.Text = "Local: " + (string.IsNullOrWhiteSpace(pokemon.LocalDeCaptura) ? "Desconhecido" : pokemon.LocalDeCaptura);
            lblDetailCapturedDate.Text = "Capturado em: " + pokemon.CapturedAt.ToString("dd/MM/yyyy HH:mm");

            pnlPokemonDetails.Visible = true;


            bool timeFull = _bll.GetTreinadorActiveTeam(_treinadorId).Count >= 6;
            btnMoveToTeam.Enabled = !timeFull;
        }

        private void btnMoveToTeam_Click(object sender, EventArgs e)
        {
            if (_selectedPokemon != null)
            {
                try
                {
                    _bll.MovePokemonToActiveTeam(_treinadorId, _selectedPokemon.Id);
                    MessageBox.Show($"{_selectedPokemon.Nome} movido para o time ativo!");
                    LoadPcBox();
                    pnlPokemonDetails.Visible = false;
                    _selectedPokemon = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (_selectedPokemon != null)
            {
                var result = MessageBox.Show($"Tem certeza que deseja soltar {_selectedPokemon.Nome}? Esta ação não pode ser desfeita.",
                                            "Soltar Pokémon", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _bll.TreinadorReleasePokemon(_treinadorId, _selectedPokemon.Id);
                        MessageBox.Show($"{_selectedPokemon.Nome} foi solto.");
                        LoadPcBox();
                        pnlPokemonDetails.Visible = false;
                        _selectedPokemon = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void btnEditNickname_Click(object sender, EventArgs e)
        {
            if (_selectedPokemon != null)
            {
                string novoApelido = Microsoft.VisualBasic.Interaction.InputBox("Digite o novo apelido:", "Editar Apelido", _selectedPokemon.Nickname ?? "");
                if (!string.IsNullOrEmpty(novoApelido))
                {
                    try
                    {
                        _bll.ChangePokemonNickname(_selectedPokemon.Id, novoApelido, _treinadorId);
                        MessageBox.Show("Apelido atualizado!");
                        _selectedPokemon.Nickname = novoApelido;
                        DisplayPokemonDetails(_selectedPokemon);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        public void PesquisarPokemon(string termo)
        {
            foreach (Control ctrl in flpPokemonGrid.Controls)
            {
                if (ctrl is ucPokemonIcon icon)
                {
                    bool corresponde = false;
                    if (icon.PokemonData != null)
                    {
                        corresponde = (icon.PokemonData.Nome?.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                      (icon.PokemonData.Nickname?.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    icon.Visible = corresponde;
                }
            }
        }
    }
}
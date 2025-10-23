using PokeCore.BLL;
using PokeCore.DTO;

namespace PokeCore.DesktopUI
{
    public partial class ucHome : UserControl
    {
        private TreinadorServiceBLL _bll;
        private TreinadorDTO _treinadorLogado;

        private string spriteBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "sprites");
        private string defaultSpritePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "poke_logo_colored.png");

        public ucHome(TreinadorDTO treinadorLogado)
        {
            InitializeComponent();
            _treinadorLogado = treinadorLogado;
            _bll = new TreinadorServiceBLL();

            this.Load += ucHome_Load;
        }

        private void ucHome_Load(object sender, EventArgs e)
        {
            CarregarDadosDashboard();
        }

        private void CarregarDadosDashboard()
        {
            try
            {
                lblBoasVindas.Text = $"Bem-vindo, {_treinadorLogado.DisplayName}!";

                lblDataCriacao.Text = "Treinador desde: " + _treinadorLogado.CreatedAt.ToShortDateString();

                int totalCapturados = _bll.GetTotalCapturedCount(_treinadorLogado.Id);
                lblPokemonCapturados.Text = $"{totalCapturados}";

                PokemonDTO ultimoPokemon = _bll.GetLastCapturedPokemon(_treinadorLogado.Id);
                if (ultimoPokemon != null)
                {
                    lblNomePokemon.Text = ultimoPokemon.Nome;
                    CarregarSpritePokemon(pbUltimoPokemon, ultimoPokemon);
                }
                else
                {
                    lblNomePokemon.Text = "Nenhum Pokemon Capturado";
                    pbUltimoPokemon.Image = null;
                }

                List<PokemonDTO> timeAtivo = _bll.GetTreinadorActiveTeam(_treinadorLogado.Id);

                var pictureBoxesTime = new List<Guna.UI2.WinForms.Guna2CirclePictureBox>
                {
                    pbTime1, pbTime2, pbTime3, pbTime4, pbTime5, pbTime6
                };

                foreach (var pb in pictureBoxesTime)
                {
                    pb.Visible = false;
                    pb.Image = null;
                }

                for (int i = 0; i < timeAtivo.Count; i++)
                {
                    if (i < pictureBoxesTime.Count)
                    {
                        PokemonDTO pokemon = timeAtivo[i];
                        pictureBoxesTime[i].Visible = true;
                        CarregarSpritePokemon(pictureBoxesTime[i], pokemon);
                    }
                }

                int pcCount = _bll.GetTreinadorPcBox(_treinadorLogado.Id).Count;
                lblPcBox.Text = $"{pcCount}";

                if (_treinadorLogado.IsAdmin)
                {
                    panelAdminStats.Visible = true;

                    lblAdminTotalTreinadores.Text = $"Total de Treinadores: {_bll.AdminGetTotalTrainerCount(_treinadorLogado.Id)}";
                    lblAdminTotalPokemon.Text = $"Total de Pokemons: {_bll.AdminGetTotalPokemonInSystem(_treinadorLogado.Id)}";
                    lblAdminNovosPokemons24h.Text = $"Novos Pokemons: {_bll.AdminGetNewPokemonLast24h(_treinadorLogado.Id)}";
                    lblAdminNovosTreinadores24h.Text = $"Novos Treinadores: {_bll.AdminGetNewTrainersLast24h(_treinadorLogado.Id)}";
                }
                else
                {
                    panelAdminStats.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do dashboard: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarSpritePokemon(Guna.UI2.WinForms.Guna2CirclePictureBox pictureBox, PokemonDTO pokemon)
        {
            if (pokemon == null)
            {
                pictureBox.Image = null;
                return;
            }

            string baseName = pokemon.Nome;
            string[] possibleExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".avif" };
            string foundPath = null;

            foreach (string ext in possibleExtensions)
            {
                string testPath = Path.Combine(spriteBasePath, $"{baseName}{ext}");
                if (File.Exists(testPath))
                {
                    foundPath = testPath;
                    break;
                }
            }

            string pathToShow = (foundPath != null) ? foundPath : defaultSpritePath;

            try
            {
                if (!File.Exists(pathToShow))
                {
                    pictureBox.Image = null;
                    Console.WriteLine($"ERRO: Imagem final não encontrada em '{pathToShow}' para {pokemon.Nome}.");
                    return;
                }

                using (var bmpTemp = new Bitmap(pathToShow))
                {
                    pictureBox.Image = new Bitmap(bmpTemp);
                }
            }
            catch (Exception ex)
            {
                pictureBox.Image = null;
                Console.WriteLine($"Erro EXCEÇÃO ao carregar imagem '{pathToShow}' para {pokemon.Nome}: {ex.Message}");
            }
        }

        private void lblAdminTotalTreinadores_Click(object sender, EventArgs e)
        {

        }
    }
}

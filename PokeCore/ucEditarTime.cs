using PokeCore.BLL;
using PokeCore.DTO;

namespace PokeCore.DesktopUI
{
    public partial class ucEditarTime : UserControl
    {
        private TreinadorDTO _treinadorLogado;
        private TreinadorServiceBLL _bll;
        private PokemonDTO _pokemonSelecionado = null;

        public event Action<PokemonDTO> OnEditarPokemonRequisitado;


        public ucEditarTime(TreinadorDTO treinadorLogado)
        {
            InitializeComponent();
            _treinadorLogado = treinadorLogado;
            _bll = new TreinadorServiceBLL();

            this.Load += ucEditarTime_Load;
            btnMoverParaTime.Click += BtnMoverParaTime_Click;
            btnMoverParaPc.Click += BtnMoverParaPC_Click;
            btnEditarPokemon.Click += BtnEditarPokemon_Click;
            btnLiberarPokemon.Click += BtnLiberarPokemon_Click;
        }

        private void ucEditarTime_Load(object sender, EventArgs e)
        {
            CarregarTimes();
            AtualizarPainelAcoes();
        }

        private void CarregarTimes()
        {
            flpActiveTeam.Controls.Clear();
            flpPcBox.Controls.Clear();

            try
            {
                List<PokemonDTO> timeAtivo = _bll.GetTreinadorActiveTeam(_treinadorLogado.Id);
                List<PokemonDTO> pcBox = _bll.GetTreinadorPcBox(_treinadorLogado.Id);

                foreach (var pokemon in timeAtivo)
                {
                    ucPokemonIcon icon = new ucPokemonIcon();
                    icon.SetPokemon(pokemon);
                    icon.ShowNameLabel = true;
                    icon.Click += PokemonIcon_Click;
                    flpActiveTeam.Controls.Add(icon);
                }

                foreach (var pokemon in pcBox)
                {
                    ucPokemonIcon icon = new ucPokemonIcon();
                    icon.SetPokemon(pokemon);
                    icon.ShowNameLabel = false;
                    icon.Click += PokemonIcon_Click;
                    flpPcBox.Controls.Add(icon);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Pokémon: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PokemonIcon_Click(object sender, EventArgs e)
        {
            if (sender is ucPokemonIcon clickedIcon)
            {
                _pokemonSelecionado = clickedIcon.PokemonData;

                AtualizarPainelAcoes();
            }
        }

        private void AtualizarPainelAcoes()
        {
            if (_pokemonSelecionado == null)
            {
                pnlAcoes.Enabled = false;
                lblNomeSelecionado.Text = "Nenhum Pokémon selecionado";
            }
            else
            {
                pnlAcoes.Enabled = true;

                lblNomeSelecionado.Text = !string.IsNullOrEmpty(_pokemonSelecionado.Nickname)
                   ? _pokemonSelecionado.Nickname
                   : _pokemonSelecionado.Nome;

                btnEditarPokemon.Enabled = true;
                btnLiberarPokemon.Enabled = true;

                if (_pokemonSelecionado.isInActiveTeam)
                {
                    btnMoverParaPc.Enabled = true;
                    btnMoverParaTime.Enabled = false;
                }
                else
                {
                    btnMoverParaPc.Enabled = false;
                    btnMoverParaTime.Enabled = true;
                }
            }
        }

        private void BtnMoverParaTime_Click(object sender, EventArgs e)
        {
            if (_pokemonSelecionado == null)
                return;

            try
            {
                _bll.MovePokemonToActiveTeam(_treinadorLogado.Id, _pokemonSelecionado.Id);
                LimparSelecaoEAtualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mover: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnMoverParaPC_Click(object sender, EventArgs e)
        {
            if (_pokemonSelecionado == null)
                return;

            try
            {
                _bll.MovePokemonToPcBox(_treinadorLogado.Id, _pokemonSelecionado.Id);
                LimparSelecaoEAtualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao mover: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnLiberarPokemon_Click(object sender, EventArgs e)
        {
            if (_pokemonSelecionado == null)
                return;

            string nome = !string.IsNullOrEmpty(_pokemonSelecionado.Nickname) ? _pokemonSelecionado.Nickname : _pokemonSelecionado.Nome;
            var confirm = MessageBox.Show($"Tem certeza que deseja liberar {nome}?\nEsta ação não pode ser desfeita.", "Confirmar Liberação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _bll.TreinadorReleasePokemon(_treinadorLogado.Id, _pokemonSelecionado.Id);
                    MessageBox.Show($"{nome} foi liberado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparSelecaoEAtualizar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao liberar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEditarPokemon_Click(object sender, EventArgs e)
        {
            if (_pokemonSelecionado == null)
                return;

            OnEditarPokemonRequisitado?.Invoke(_pokemonSelecionado);
        }

        private void LimparSelecaoEAtualizar()
        {
            _pokemonSelecionado = null;
            CarregarTimes();
            AtualizarPainelAcoes();
        }

        public void PesquisarPokemon(string termo)
        {
            foreach (Control ctrl in flpActiveTeam.Controls)
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

            foreach (Control ctrl in flpPcBox.Controls)
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

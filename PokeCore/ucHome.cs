using PokeCore.BLL;
using PokeCore.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PokeCore.DesktopUI
{
    public partial class ucHome : UserControl
    {
        private TreinadorServiceBLL _bll;
        private TreinadorDTO _treinadorLogado;

        public ucHome(TreinadorDTO treinadorLogado)
        {
            InitializeComponent();
            _treinadorLogado = treinadorLogado;
            _bll = new TreinadorServiceBLL();

            this.Load += ucHome_Load;
        }

        private void ucHome_Load(object sender, EventArgs e)
        {

        }

        private void CarregarDadosDashboard()
        {
            try
            {
                lblBoasVindas.Text = $"Bem-vindo, " + _treinadorLogado.DisplayName + "!";
                lblDataCriacao.Text = "Treinador desde: " + _treinadorLogado.CreatedAt.ToShortDateString;

                int totalCapturados = _bll.GetTotalCapturedCount(_treinadorLogado.Id);
                lblPokemonCapturados.Text = "Total de Pokemons Capturados: " + totalCapturados.ToString();

                PokemonDTO ultimoPokemon = _bll.GetLastCapturedPokemon(_treinadorLogado.Id);
                if (ultimoPokemon != null)
                {
                    lblNomePokemon.Text = ultimoPokemon.Nome;
                }
                else
                {
                    lblNomePokemon.Text = "Nenhum Pokemon Capturado";
                }

                List<PokemonDTO> timeAtivo = _bll.GetTreinadorActiveTeam(_treinadorLogado.Id);

                var pictureBoxesTime = new List<Guna.UI2.WinForms.Guna2CirclePictureBox>
            {
                pbTime1, pbTime2, pbTime3, pbTime4, pbTime5, pbTime6
            };

                for (int i = 0; i < pictureBoxesTime.Count; i++)
                {
                    if (i < timeAtivo.Count)
                    {
                        PokemonDTO pokemon = timeAtivo[i];

                        pictureBoxesTime[i].Visible = true;
                        pictureBoxesTime[i].FillColor = SystemColors.WindowFrame;
                    }
                }

                int pcCount = _bll.GetTreinadorPcBox(_treinadorLogado.Id).Count;
                lblPcBox.Text = $"Seu PC Box: {pcCount} Pokémon";

                if (_treinadorLogado.IsAdmin)
                {
                    panelAdminStats.Visible = true; // Mostra o painel de admin

                    // Preenche os stats de Admin (você precisará criar esses Labels no designer)
                    lblAdminTotalTreinadores.Text = _bll.AdminGetTotalTrainerCount(_treinadorLogado.Id).ToString();
                    lblAdminTotalPokemon.Text = _bll.AdminGetTotalPokemonInSystem(_treinadorLogado.Id).ToString();
                    lblAdminNovosPokemons24h.Text = _bll.AdminGetNewPokemonLast24h(_treinadorLogado.Id).ToString();
                    lblAdminNovosTreinadores24h.Text = _bll.AdminGetNewTrainersLast24h(_treinadorLogado.Id).ToString();
                }
                else
                {
                    panelAdminStats.Visible = false; // Esconde o painel de admin
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do dashboard: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

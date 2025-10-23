using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using PokeCore.BLL;
using PokeCore.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PokeCore.DesktopUI
{
    public partial class ucGerenciarPokemons : UserControl
    {
        private TreinadorServiceBLL _treinadorBLL;
        private PokemonServiceBLL _pokemonBLL;
        private TreinadorDTO _adminLogado;
        private List<TreinadorDTO> _listaTreinadores;
        public ucGerenciarPokemons(TreinadorServiceBLL treinadorBLL, PokemonServiceBLL pokemonBLL, TreinadorDTO adminLogado)
        {
            InitializeComponent();

            _treinadorBLL = treinadorBLL;
            _pokemonBLL = pokemonBLL;
            _adminLogado = adminLogado;
        }

        private void ucGerenciarPokemons_Load(object sender, EventArgs e)
        {
            CarregarPokemonsGrid();
            CarregarTreinadoresCombo();
            LimparCamposAdicao();
            LimparCamposExclusao();
        }

        private void CarregarPokemonsGrid()
        {
            try
            {
                dgvPokemons.DataSource = null;
                List<PokemonDTO> pokemons = _pokemonBLL.GetAllPokemon();
                _listaTreinadores = _treinadorBLL.GetAllTreinadores();

                var dataSource = pokemons.Select(p => new
                {
                    p.Id,
                    p.Nome,
                    p.Nickname,
                    p.Nivel,
                    p.OwnerId,
                    DonoNome = p.OwnerId.HasValue ? _listaTreinadores.FirstOrDefault(t => t.Id == p.OwnerId)?.Username : "(Livre)"
                }).ToList();

                dgvPokemons.DataSource = dataSource;

                dgvPokemons.Columns["Id"].HeaderText = "ID";
                dgvPokemons.Columns["Nome"].HeaderText = "Nome";
                dgvPokemons.Columns["Nickname"].HeaderText = "Apelido";
                dgvPokemons.Columns["Nivel"].HeaderText = "Nível";
                dgvPokemons.Columns["OwnerId"].HeaderText = "ID do Dono";
                dgvPokemons.Columns["DonoNome"].HeaderText = "Dono";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar Pokémon: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarTreinadoresCombo()
        {
            try
            {
                cmbTreinador.DataSource = null;
                if (_listaTreinadores == null)
                    _listaTreinadores = _treinadorBLL.GetAllTreinadores();

                var treinadoresComNenhum = new List<TreinadorDTO> { new TreinadorDTO { Id = 0, Username = "(Nenhum)" } };
                treinadoresComNenhum.AddRange(_listaTreinadores);

                cmbTreinador.DataSource = treinadoresComNenhum;
                cmbTreinador.DisplayMember = "Username";
                cmbTreinador.ValueMember = "Id";
                cmbTreinador.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar treinadores: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCamposAdicao();
        }

        private void LimparCamposAdicao()
        {
            txtNome.Clear();
            txtNivel.Clear();
            txtTipo.Clear();
            txtNickname.Clear();
            txtLocalDeCaptura.Clear();
            dtpDataDeCaptura.Value = DateTime.Now;
            cmbTreinador.SelectedIndex = 0;
        }

        private void LimparCamposExclusao()
        {
            txtIdExcluir.Clear();
            txtNomeExcluir.Clear();
            btnExcluir.Enabled = false;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("O nome do Pokémon é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(txtNivel.Text, out int nivel) || nivel <= 0)
                {
                    MessageBox.Show("Nível inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var novoPokemon = new PokemonDTO
                {
                    Nome = txtNome.Text.Trim(),
                    Nivel = nivel,
                    Tipo = txtTipo.Text.Trim(),
                    LocalDeCaptura = txtLocalDeCaptura.Text.Trim(),
                    CapturedAt = dtpDataDeCaptura.Value,
                    Nickname = txtNickname.Text.Trim(),
                    OwnerId = (int)cmbTreinador.SelectedValue == 0 ? (int?)null : (int)cmbTreinador.SelectedValue,
                    isInActiveTeam = false
                };

                _pokemonBLL.AdminAddPokemon(_adminLogado.Id, novoPokemon);

                MessageBox.Show("Pokémon adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarPokemonsGrid();
                LimparCamposAdicao();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar Pokémon: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPokemons_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPokemons.Rows[e.RowIndex];
                txtIdExcluir.Text = row.Cells["ID"].Value?.ToString() ?? "";
                txtNomeExcluir.Text = row.Cells["Nome"].Value?.ToString() ?? "";
                btnExcluir.Enabled = true;

                PokemonDTO pokeSelecionado = _pokemonBLL.GetPokemonById((int)row.Cells["Id"].Value);
            }
            else
            {
                LimparCamposExclusao();
            }
        }

        private void dgvPokemons_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    int pokemonId = (int)dgvPokemons.Rows[e.RowIndex].Cells["Id"].Value;
                    PokemonDTO pokemonParaEditar = _pokemonBLL.GetPokemonById(pokemonId);

                    frmMain mainForm = this.ParentForm as frmMain;
                    mainForm?.AbrirEditarPokemonAdmin(pokemonParaEditar);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao abrir edição: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtIdExcluir.Text, out int pokemonId))
            {
                MessageBox.Show("Nenhum Pokémon válido selecionado para exclusão.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nomePoke = txtNomeExcluir.Text;
            DialogResult confirm = MessageBox.Show(
                $"Você tem certeza que deseja excluir o Pokémon '{nomePoke}' (ID: {pokemonId}) PERMANENTEMENTE?\n\nEsta ação não pode ser desfeita.",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _pokemonBLL.AdminDeletePokemon(_adminLogado.Id, pokemonId);
                    MessageBox.Show("Pokémon excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarPokemonsGrid();
                    LimparCamposExclusao();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir Pokémon: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void PesquisarPokemon(string termo)
        {
            var pokemonsFiltrados = _pokemonBLL.GetAllPokemon()
                .Where(p => (p.Nome?.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (p.Nickname?.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (p.Id.ToString() == termo))
                .Select(p => new {})
                .ToList();
            dgvPokemons.DataSource = pokemonsFiltrados;
        }
    }
}

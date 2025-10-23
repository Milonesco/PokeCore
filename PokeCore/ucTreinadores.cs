using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PokeCore.BLL;
using PokeCore.DTO;

namespace PokeCore.DesktopUI
{
    public partial class ucTreinadores : UserControl
    {
        private TreinadorServiceBLL _bll;
        private TreinadorDTO _adminLogado;

        private int? treinadorSelecionadoId = null;
        private string diretorioFotos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "fotos");
        private string caminhoFotoSelecionadaOriginal = null;
        private string caminhoFotoSalva = null;

        public ucTreinadores(TreinadorDTO adminLogado)
        {
            InitializeComponent();
            dgvTreinadores.Columns.Clear();

            _bll = new TreinadorServiceBLL();
            _adminLogado = adminLogado;

            if (_adminLogado == null || !_adminLogado.IsAdmin)
            {
                MessageBox.Show("Acesso negado. Apenas administradores podem gerenciar treinadores.", "Erro de Permissão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnExcluir.Visible = false;
                btnExcluir.Enabled = false;
            }

            ConfigureDataGridView();

            this.Load += ucTreinadores_Load;
        }

        private void ConfigureDataGridView()
        {
            dgvTreinadores.AutoGenerateColumns = false;
            dgvTreinadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTreinadores.MultiSelect = false;
            dgvTreinadores.AllowUserToAddRows = false;
            dgvTreinadores.AllowUserToDeleteRows = false;
            dgvTreinadores.ReadOnly = true;
            dgvTreinadores.RowHeadersVisible = false;

            dgvTreinadores.Columns.Clear();

            dgvTreinadores.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50
            });
            dgvTreinadores.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUsername",
                HeaderText = "Nome",
                DataPropertyName = "Username",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvTreinadores.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDisplayName",
                HeaderText = "Nome Exibido",
                DataPropertyName = "DisplayName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvTreinadores.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEmail",
                HeaderText = "Email",
                DataPropertyName = "Email",
                Width = 200
            });
            dgvTreinadores.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colIsAdmin",
                HeaderText = "Admin",
                DataPropertyName = "IsAdmin",
                Width = 60
            });
            dgvTreinadores.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCreatedAt",
                HeaderText = "Data de Criação",
                DataPropertyName = "CreatedAt",
                Width = 60
            });

        }

        private void ucTreinadores_Load(object sender, EventArgs e)
        {
            if (_adminLogado != null && _adminLogado.IsAdmin)
            {
                CarregarTreinadores();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("As senhas não coincidem!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                caminhoFotoSalva = null;
                if (!string.IsNullOrEmpty(caminhoFotoSelecionadaOriginal) && pbFoto.Image != null)
                {
                    if (!Directory.Exists(diretorioFotos))
                    {
                        Directory.CreateDirectory(diretorioFotos);
                    }

                    string extensao = Path.GetExtension(caminhoFotoSelecionadaOriginal);
                    string nomeArquivoUnico = $"{Guid.NewGuid()}{extensao}";
                    caminhoFotoSalva = Path.Combine(diretorioFotos, nomeArquivoUnico);


                    pbFoto.Image.Save(caminhoFotoSalva, ImageFormat.Jpeg);
                }


                _bll.Register(
                    txtNome.Text,
                    txtSenha.Text,
                    txtEmail.Text,
                    txtDisplayName.Text,
                    caminhoFotoSalva
                );

                // --- Sucesso ---
                MessageBox.Show("Treinador registrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show("Erro de validação: " + argEx.Message, "Cadastro Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!string.IsNullOrEmpty(caminhoFotoSalva) && File.Exists(caminhoFotoSalva))
                {
                    try { File.Delete(caminhoFotoSalva); } catch { }
                }
            }
        }

        private void dgvTreinadores_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgvTreinadores.SelectedRows.Cast<DataGridViewRow>().FirstOrDefault();


            if (selectedRow != null && selectedRow.DataBoundItem is TreinadorDTO selected)
            {

                treinadorSelecionadoId = selected.Id;

                txtNome.Text = selected.Username;
                txtDisplayName.Text = selected.DisplayName;
                txtEmail.Text = selected.Email;
                chkIsAdmin.Checked = selected.IsAdmin;
                txtFotoCaminhoAtual.Text = selected.FotoPath;
                txtFotoCaminhoNovo.Text = string.Empty;
                caminhoFotoSelecionadaOriginal = null;
                caminhoFotoSalva = null;

                CarregarFotoPerfil(selected.FotoPath);

                bool canEditOrDelete = (_adminLogado != null && selected.Id != _adminLogado.Id);
                btnAtualizar.Enabled = canEditOrDelete;
                btnExcluir.Enabled = canEditOrDelete;
            }
            else
            {
                LimparCamposEdicao();
            }
        }

        private void CarregarTreinadores()
        {
            try
            {
                List<TreinadorDTO> todosTreinadores = _bll.GetAllTreinadores();

                dgvTreinadores.DataSource = todosTreinadores;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar lista de treinadores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvTreinadores.DataSource = null;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvTreinadores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um treinador para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TreinadorDTO treinadorSelecionado = dgvTreinadores.SelectedRows[0].DataBoundItem as TreinadorDTO;

            if (treinadorSelecionado == null)
            {
                MessageBox.Show("Não foi possível obter os dados do treinador selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int treinadorAlvoId = treinadorSelecionado.Id;

            var confirmResult = MessageBox.Show($"Tem certeza que deseja excluir o treinador '{treinadorSelecionado.Username}' (ID: {treinadorAlvoId})?\nEsta ação não pode ser desfeita.",
                                                 "Confirmar Exclusão",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    _bll.AdminDeleteTreinador(_adminLogado.Id, treinadorAlvoId);

                    MessageBox.Show("Treinador excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarTreinadores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir treinador: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEscolherFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Filter = "Arquivos de Imagem (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Selecione a nova foto";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        caminhoFotoSelecionadaOriginal = openFileDialog.FileName;
                        txtFotoCaminhoNovo.Text = caminhoFotoSelecionadaOriginal;
                        using (var bmpTemp = new Bitmap(caminhoFotoSelecionadaOriginal))
                        {
                            pbFoto.Image = new Bitmap(bmpTemp);
                        }
                        caminhoFotoSalva = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar a imagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        caminhoFotoSelecionadaOriginal = null;
                        txtFotoCaminhoNovo.Text = string.Empty;
                        CarregarFotoPerfil(txtFotoCaminhoAtual.Text);
                    }
                }
            }
        }

        private void CarregarFotoPerfil(string fotoPath)
        {
            string defaultFotoPath = "img/poke_logo_colored.png";
            string caminhoParaCarregar = defaultFotoPath;

            try
            {
                if (!string.IsNullOrWhiteSpace(fotoPath))
                {
                    string caminhoAbsoluto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fotoPath);

                    if (File.Exists(caminhoAbsoluto))
                    {
                        caminhoParaCarregar = caminhoAbsoluto;
                    }
                }

                using (var bmpTemp = new Bitmap(caminhoParaCarregar))
                {
                    pbFoto.Image = new Bitmap(bmpTemp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar foto '{fotoPath ?? defaultFotoPath}': {ex.Message}");
                try
                {
                    if (File.Exists(defaultFotoPath))
                    {
                        using (var bmpTemp = new Bitmap(defaultFotoPath)) { pbFoto.Image = new Bitmap(bmpTemp); }
                    }
                    else { pbFoto.Image = null; }
                }
                catch { pbFoto.Image = null; }
            }
        }

        private void LimparCamposEdicao()
        {
            treinadorSelecionadoId = null;
            txtNome.Clear();
            txtDisplayName.Clear();
            txtEmail.Clear();
            chkIsAdmin.Checked = false;
            pbFoto.Image = null;
            txtFotoCaminhoAtual.Clear();
            txtFotoCaminhoNovo.Clear();
            caminhoFotoSelecionadaOriginal = null;
            caminhoFotoSalva = null;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (treinadorSelecionadoId == null)
            {
                MessageBox.Show("Nenhum treinador selecionado para atualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string fotoPathFinal = txtFotoCaminhoAtual.Text;

                if (!string.IsNullOrEmpty(txtFotoCaminhoNovo.Text) && pbFoto.Image != null)
                {
                    if (!Directory.Exists(diretorioFotos))
                    {
                        Directory.CreateDirectory(diretorioFotos);
                    }

                    string extensao = Path.GetExtension(txtFotoCaminhoNovo.Text);
                    string nomeArquivoUnico = $"{Guid.NewGuid()}{extensao}";
                    string caminhoFotoRelativo = Path.Combine("data", "fotos", nomeArquivoUnico);
                    caminhoFotoSalva = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, caminhoFotoRelativo);

                    pbFoto.Image.Save(caminhoFotoSalva, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fotoPathFinal = caminhoFotoSalva;

                    if (!string.IsNullOrEmpty(txtFotoCaminhoAtual.Text) && File.Exists(txtFotoCaminhoAtual.Text) && txtFotoCaminhoAtual.Text != "img/poke_logo_colored.png")
                    {
                        try { File.Delete(txtFotoCaminhoAtual.Text); } catch { }
                    }
                }
                // --- Fim da Lógica da Foto ---


                var treinadorAtualizado = new TreinadorDTO
                {
                    Id = treinadorSelecionadoId.Value,
                    Username = txtNome.Text.Trim(),
                    DisplayName = txtDisplayName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    IsAdmin = chkIsAdmin.Checked,
                    FotoPath = fotoPathFinal,
                };


                _bll.AdminUpdateTreinadorProfile(_adminLogado.Id, treinadorAtualizado);

                MessageBox.Show($"Treinador '{treinadorAtualizado.Username}' atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparCamposEdicao();
                CarregarTreinadores();
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show("Erro de validação: " + argEx.Message, "Atualização Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar treinador: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (!string.IsNullOrEmpty(caminhoFotoSalva) && caminhoFotoSalva != txtFotoCaminhoAtual.Text && File.Exists(caminhoFotoSalva))
                {
                    try { File.Delete(caminhoFotoSalva); } catch { }
                }
            }
        }

        public void PesquisarTreinador(string termo)
        {
            CurrencyManager currencyManager = (CurrencyManager)BindingContext[dgvTreinadores.DataSource];
            currencyManager.SuspendBinding();

            foreach (DataGridViewRow row in dgvTreinadores.Rows)
            {
                bool visivel = false;
                if (row.DataBoundItem is TreinadorDTO treinador)
                {
                    visivel = (treinador.Username?.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0) ||
                              (treinador.DisplayName?.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0) ||
                              (treinador.Email?.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                row.Visible = visivel;
            }

            currencyManager.ResumeBinding();
        }
    }
}

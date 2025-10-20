using PokeCore.BLL;
using PokeCore.DTO;
using System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PokeCore.DesktopUI
{

    public partial class frmCadastro : Form
    {
        private TreinadorServiceBLL _bll;

        private string diretorioFotos;
        private string caminhoFotoSelecionadaOriginal = null;
        private string caminhoFotoSalva = null;

        public frmCadastro()
        {
            InitializeComponent();

            _bll = new TreinadorServiceBLL();
            diretorioFotos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "fotos");
        }

        private void pbFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Filter = "Arquivos de Imagem (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Selecione uma foto de perfil";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        caminhoFotoSelecionadaOriginal = openFileDialog.FileName;
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
                        pbFoto.Image = null;
                    }
                }
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


                    pbFoto.Image.Save(caminhoFotoSalva, ImageFormat.Jpeg); // Ou ImageFormat.Png, etc.

                    // Alternativa: Copiar o arquivo original (pode ser maior ou em formato diferente)
                    // File.Copy(caminhoFotoSelecionadaOriginal, caminhoFotoSalva, true); // true = sobrescrever
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
                this.DialogResult = DialogResult.OK; 
                this.Close(); 
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
                    try { File.Delete(caminhoFotoSalva); } catch {}
                }
            }
        }
    }
}


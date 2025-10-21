using PokeCore.DTO;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PokeCore.DesktopUI
{
    public partial class ucPokemonIcon : UserControl
    {

        public PokemonDTO PokemonData { get; private set; }


        private string spriteBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "sprites");
        private string defaultSpritePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "pokeball_icon.png");

        public ucPokemonIcon()
        {
            InitializeComponent();
            this.MouseEnter += (s, e) => this.BackColor = Color.LightGray;
            this.MouseLeave += (s, e) => this.BackColor = SystemColors.Control;
            pbPokemonSprite.MouseEnter += (s, e) => this.BackColor = Color.LightGray;
            pbPokemonSprite.MouseLeave += (s, e) => this.BackColor = SystemColors.Control;
        }

        public void SetPokemon(PokemonDTO pokemon)
        {
            PokemonData = pokemon;
            LoadPokemonSprite();
        }

        private void LoadPokemonSprite()
        {
            if (PokemonData == null)
            {
                pbPokemonSprite.Image = null;
                return;
            }

            string baseName = PokemonData.Nome; // Ex: "Pikachu"
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

            string pathToShow = defaultSpritePath;
            if (foundPath != null)
            {
                pathToShow = foundPath;
            }
            else
            {
                Console.WriteLine($"AVISO: Sprite não encontrado para {PokemonData.Nome} com extensões comuns em '{spriteBasePath}'. Usando padrão.");
            }

            try
            {
                if (!File.Exists(pathToShow))
                {
                    pbPokemonSprite.Image = null; 
                    Console.WriteLine($"ERRO: Imagem final (específica ou padrão) não encontrada em '{pathToShow}' para {PokemonData.Nome}.");
                    return;
                }

                using (var bmpTemp = new Bitmap(pathToShow))
                {
                    pbPokemonSprite.Image = new Bitmap(bmpTemp);
                }
            }
            catch (FileNotFoundException)
            {
                pbPokemonSprite.Image = null;
                Console.WriteLine($"ERRO (FileNotFound): Não foi possível carregar a imagem em '{pathToShow}' para {PokemonData.Nome}.");            }
            catch (OutOfMemoryException)
            {
                pbPokemonSprite.Image = null;
                Console.WriteLine($"ERRO (OutOfMemory): Arquivo de imagem inválido ou corrompido em '{pathToShow}' para {PokemonData.Nome}.");            }
            catch (Exception ex)
            {
                pbPokemonSprite.Image = null; // Limpa a imagem em caso de erro
                Console.WriteLine($"Erro EXCEÇÃO genérica ao carregar imagem '{pathToShow}' para {PokemonData.Nome}: {ex.Message}");
            }
        }

        private void pbPokemonSprite_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
    }
}
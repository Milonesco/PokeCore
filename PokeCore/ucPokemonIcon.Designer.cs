namespace PokeCore.DesktopUI
{
    partial class ucPokemonIcon
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pbPokemonSprite = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            ((System.ComponentModel.ISupportInitialize)pbPokemonSprite).BeginInit();
            SuspendLayout();
            // 
            // pbPokemonSprite
            // 
            pbPokemonSprite.ImageRotate = 0F;
            pbPokemonSprite.Location = new Point(5, 3);
            pbPokemonSprite.Name = "pbPokemonSprite";
            pbPokemonSprite.ShadowDecoration.CustomizableEdges = customizableEdges1;
            pbPokemonSprite.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            pbPokemonSprite.Size = new Size(49, 52);
            pbPokemonSprite.SizeMode = PictureBoxSizeMode.Zoom;
            pbPokemonSprite.TabIndex = 0;
            pbPokemonSprite.TabStop = false;
            pbPokemonSprite.Click += pbPokemonSprite_Click;
            // 
            // ucPokemonIcon
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbPokemonSprite);
            Name = "ucPokemonIcon";
            Size = new Size(60, 60);
            ((System.ComponentModel.ISupportInitialize)pbPokemonSprite).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox pbPokemonSprite;
    }
}

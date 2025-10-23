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
            lblNomeIcone = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)pbPokemonSprite).BeginInit();
            SuspendLayout();
            // 
            // pbPokemonSprite
            // 
            pbPokemonSprite.ImageRotate = 0F;
            pbPokemonSprite.Location = new Point(4, 5);
            pbPokemonSprite.Name = "pbPokemonSprite";
            pbPokemonSprite.ShadowDecoration.CustomizableEdges = customizableEdges1;
            pbPokemonSprite.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            pbPokemonSprite.Size = new Size(90, 90);
            pbPokemonSprite.SizeMode = PictureBoxSizeMode.Zoom;
            pbPokemonSprite.TabIndex = 0;
            pbPokemonSprite.TabStop = false;
            pbPokemonSprite.Click += pbPokemonSprite_Click;
            // 
            // lblNomeIcone
            // 
            lblNomeIcone.BackColor = Color.Transparent;
            lblNomeIcone.Location = new Point(31, 100);
            lblNomeIcone.Name = "lblNomeIcone";
            lblNomeIcone.Size = new Size(36, 17);
            lblNomeIcone.TabIndex = 1;
            lblNomeIcone.Text = "Nome";
            lblNomeIcone.TextAlignment = ContentAlignment.MiddleCenter;
            lblNomeIcone.Visible = false;
            // 
            // ucPokemonIcon
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblNomeIcone);
            Controls.Add(pbPokemonSprite);
            Name = "ucPokemonIcon";
            Size = new Size(100, 120);
            ((System.ComponentModel.ISupportInitialize)pbPokemonSprite).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox pbPokemonSprite;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNomeIcone;
    }
}

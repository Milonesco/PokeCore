namespace PokeCore.DesktopUI
{
    partial class ucEditarTime
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
            var customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            var customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            var customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            var customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            var customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            var customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            var customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            var customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            var customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            var customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            flpPcBox = new FlowLayoutPanel();
            lblTituloPcBox = new Guna.UI2.WinForms.Guna2HtmlLabel();
            flpActiveTeam = new FlowLayoutPanel();
            lblTituloTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlAcoes = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblNomeSelecionado = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnMoverParaTime = new Guna.UI2.WinForms.Guna2Button();
            btnMoverParaPc = new Guna.UI2.WinForms.Guna2Button();
            btnEditarPokemon = new Guna.UI2.WinForms.Guna2Button();
            btnLiberarPokemon = new Guna.UI2.WinForms.Guna2Button();
            pnlAcoes.SuspendLayout();
            SuspendLayout();
            // 
            // flpPcBox
            // 
            flpPcBox.Location = new Point(0, 207);
            flpPcBox.Name = "flpPcBox";
            flpPcBox.Size = new Size(883, 443);
            flpPcBox.TabIndex = 0;
            // 
            // lblTituloPcBox
            // 
            lblTituloPcBox.AutoSize = false;
            lblTituloPcBox.BackColor = Color.Transparent;
            lblTituloPcBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloPcBox.ForeColor = Color.FromArgb(58, 58, 58);
            lblTituloPcBox.Location = new Point(0, 178);
            lblTituloPcBox.Name = "lblTituloPcBox";
            lblTituloPcBox.Size = new Size(78, 27);
            lblTituloPcBox.TabIndex = 0;
            lblTituloPcBox.Text = "PC Box:";
            // 
            // flpActiveTeam
            // 
            flpActiveTeam.BackColor = Color.WhiteSmoke;
            flpActiveTeam.Location = new Point(0, 25);
            flpActiveTeam.Name = "flpActiveTeam";
            flpActiveTeam.Size = new Size(1148, 147);
            flpActiveTeam.TabIndex = 1;
            // 
            // lblTituloTime
            // 
            lblTituloTime.AutoSize = false;
            lblTituloTime.BackColor = Color.WhiteSmoke;
            lblTituloTime.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloTime.ForeColor = Color.FromArgb(58, 58, 58);
            lblTituloTime.Location = new Point(0, 0);
            lblTituloTime.Name = "lblTituloTime";
            lblTituloTime.Size = new Size(1148, 29);
            lblTituloTime.TabIndex = 0;
            lblTituloTime.Text = "Time Ativo:";
            // 
            // pnlAcoes
            // 
            pnlAcoes.BackColor = Color.WhiteSmoke;
            pnlAcoes.Controls.Add(guna2HtmlLabel3);
            pnlAcoes.Controls.Add(lblNomeSelecionado);
            pnlAcoes.Controls.Add(btnMoverParaTime);
            pnlAcoes.Controls.Add(btnMoverParaPc);
            pnlAcoes.Controls.Add(btnEditarPokemon);
            pnlAcoes.Controls.Add(btnLiberarPokemon);
            pnlAcoes.CustomizableEdges = customizableEdges9;
            pnlAcoes.Location = new Point(882, 170);
            pnlAcoes.Name = "pnlAcoes";
            pnlAcoes.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnlAcoes.Size = new Size(266, 480);
            pnlAcoes.TabIndex = 2;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Segoe UI", 12F);
            guna2HtmlLabel3.Location = new Point(7, 10);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(160, 23);
            guna2HtmlLabel3.TabIndex = 1;
            guna2HtmlLabel3.Text = "Pokemon Selecionado:";
            // 
            // lblNomeSelecionado
            // 
            lblNomeSelecionado.BackColor = Color.Transparent;
            lblNomeSelecionado.Font = new Font("Segoe UI", 12F);
            lblNomeSelecionado.Location = new Point(173, 10);
            lblNomeSelecionado.Name = "lblNomeSelecionado";
            lblNomeSelecionado.Size = new Size(43, 23);
            lblNomeSelecionado.TabIndex = 1;
            lblNomeSelecionado.Text = "nome";
            // 
            // btnMoverParaTime
            // 
            btnMoverParaTime.CustomizableEdges = customizableEdges1;
            btnMoverParaTime.DisabledState.BorderColor = Color.DarkGray;
            btnMoverParaTime.DisabledState.CustomBorderColor = Color.DarkGray;
            btnMoverParaTime.DisabledState.FillColor = Color.Gray;
            btnMoverParaTime.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnMoverParaTime.FillColor = Color.FromArgb(221, 23, 58);
            btnMoverParaTime.Font = new Font("Segoe UI", 9F);
            btnMoverParaTime.ForeColor = Color.White;
            btnMoverParaTime.Location = new Point(68, 182);
            btnMoverParaTime.Name = "btnMoverParaTime";
            btnMoverParaTime.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnMoverParaTime.Size = new Size(180, 45);
            btnMoverParaTime.TabIndex = 0;
            btnMoverParaTime.Text = "Mover para o time";
            // 
            // btnMoverParaPc
            // 
            btnMoverParaPc.CustomizableEdges = customizableEdges3;
            btnMoverParaPc.DisabledState.BorderColor = Color.DarkGray;
            btnMoverParaPc.DisabledState.CustomBorderColor = Color.DarkGray;
            btnMoverParaPc.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnMoverParaPc.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnMoverParaPc.FillColor = Color.FromArgb(58, 58, 58);
            btnMoverParaPc.Font = new Font("Segoe UI", 9F);
            btnMoverParaPc.ForeColor = Color.White;
            btnMoverParaPc.Location = new Point(68, 245);
            btnMoverParaPc.Name = "btnMoverParaPc";
            btnMoverParaPc.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnMoverParaPc.Size = new Size(180, 45);
            btnMoverParaPc.TabIndex = 0;
            btnMoverParaPc.Text = "Mover para o PC";
            // 
            // btnEditarPokemon
            // 
            btnEditarPokemon.CustomizableEdges = customizableEdges5;
            btnEditarPokemon.DisabledState.BorderColor = Color.DarkGray;
            btnEditarPokemon.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEditarPokemon.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEditarPokemon.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEditarPokemon.FillColor = Color.White;
            btnEditarPokemon.Font = new Font("Segoe UI", 9F);
            btnEditarPokemon.ForeColor = Color.FromArgb(58, 58, 58);
            btnEditarPokemon.HoverState.FillColor = Color.WhiteSmoke;
            btnEditarPokemon.Location = new Point(68, 311);
            btnEditarPokemon.Name = "btnEditarPokemon";
            btnEditarPokemon.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnEditarPokemon.Size = new Size(180, 45);
            btnEditarPokemon.TabIndex = 0;
            btnEditarPokemon.Text = "Editar Pokemon";
            // 
            // btnLiberarPokemon
            // 
            btnLiberarPokemon.BorderColor = Color.FromArgb(221, 23, 58);
            btnLiberarPokemon.BorderThickness = 2;
            btnLiberarPokemon.CustomizableEdges = customizableEdges7;
            btnLiberarPokemon.DisabledState.BorderColor = Color.DarkGray;
            btnLiberarPokemon.DisabledState.CustomBorderColor = Color.DarkGray;
            btnLiberarPokemon.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLiberarPokemon.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnLiberarPokemon.FillColor = Color.White;
            btnLiberarPokemon.Font = new Font("Segoe UI", 9F);
            btnLiberarPokemon.ForeColor = Color.FromArgb(221, 23, 58);
            btnLiberarPokemon.HoverState.FillColor = Color.FromArgb(255, 245, 245);
            btnLiberarPokemon.Location = new Point(68, 372);
            btnLiberarPokemon.Name = "btnLiberarPokemon";
            btnLiberarPokemon.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnLiberarPokemon.Size = new Size(180, 45);
            btnLiberarPokemon.TabIndex = 0;
            btnLiberarPokemon.Text = "Liberar Pokemon";
            // 
            // ucEditarTime
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(lblTituloPcBox);
            Controls.Add(lblTituloTime);
            Controls.Add(pnlAcoes);
            Controls.Add(flpPcBox);
            Controls.Add(flpActiveTeam);
            Name = "ucEditarTime";
            Size = new Size(1148, 650);
            pnlAcoes.ResumeLayout(false);
            pnlAcoes.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpPcBox;
        private FlowLayoutPanel flpActiveTeam;
        private Guna.UI2.WinForms.Guna2Panel pnlAcoes;
        private Guna.UI2.WinForms.Guna2Button btnMoverParaTime;
        private Guna.UI2.WinForms.Guna2Button btnMoverParaPc;
        private Guna.UI2.WinForms.Guna2Button btnEditarPokemon;
        private Guna.UI2.WinForms.Guna2Button btnLiberarPokemon;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNomeSelecionado;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTituloTime;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTituloPcBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
    }
}

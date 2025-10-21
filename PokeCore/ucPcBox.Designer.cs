namespace PokeCore.DesktopUI
{
    partial class ucPcBox
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            flpPokemonGrid = new FlowLayoutPanel();
            pnlPokemonDetails = new Guna.UI2.WinForms.Guna2Panel();
            btnRelease = new Guna.UI2.WinForms.Guna2Button();
            btnEditNickname = new Guna.UI2.WinForms.Guna2PictureBox();
            btnMoveToTeam = new Guna.UI2.WinForms.Guna2Button();
            lblDetailLocation = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDetailCapturedDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDetailOwnerName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDetailLevel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDetailType = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDetailNickname = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDetailName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pbDetailImage = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            pnlPokemonDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnEditNickname).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbDetailImage).BeginInit();
            SuspendLayout();
            // 
            // flpPokemonGrid
            // 
            flpPokemonGrid.AutoScroll = true;
            flpPokemonGrid.Location = new Point(0, 0);
            flpPokemonGrid.Name = "flpPokemonGrid";
            flpPokemonGrid.Size = new Size(740, 650);
            flpPokemonGrid.TabIndex = 0;
            // 
            // pnlPokemonDetails
            // 
            pnlPokemonDetails.Controls.Add(btnRelease);
            pnlPokemonDetails.Controls.Add(btnEditNickname);
            pnlPokemonDetails.Controls.Add(btnMoveToTeam);
            pnlPokemonDetails.Controls.Add(lblDetailLocation);
            pnlPokemonDetails.Controls.Add(lblDetailCapturedDate);
            pnlPokemonDetails.Controls.Add(lblDetailOwnerName);
            pnlPokemonDetails.Controls.Add(lblDetailLevel);
            pnlPokemonDetails.Controls.Add(lblDetailType);
            pnlPokemonDetails.Controls.Add(lblDetailNickname);
            pnlPokemonDetails.Controls.Add(lblDetailName);
            pnlPokemonDetails.Controls.Add(pbDetailImage);
            pnlPokemonDetails.CustomizableEdges = customizableEdges8;
            pnlPokemonDetails.Location = new Point(740, -1);
            pnlPokemonDetails.Name = "pnlPokemonDetails";
            pnlPokemonDetails.ShadowDecoration.CustomizableEdges = customizableEdges9;
            pnlPokemonDetails.Size = new Size(408, 651);
            pnlPokemonDetails.TabIndex = 1;
            pnlPokemonDetails.Visible = false;
            // 
            // btnRelease
            // 
            btnRelease.CustomizableEdges = customizableEdges1;
            btnRelease.DisabledState.BorderColor = Color.DarkGray;
            btnRelease.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRelease.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRelease.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRelease.FillColor = Color.FromArgb(255, 220, 37);
            btnRelease.Font = new Font("Segoe UI", 9F);
            btnRelease.ForeColor = Color.White;
            btnRelease.Image = Properties.Resources.alert_whole;
            btnRelease.ImageAlign = HorizontalAlignment.Left;
            btnRelease.Location = new Point(225, 552);
            btnRelease.Name = "btnRelease";
            btnRelease.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnRelease.Size = new Size(180, 45);
            btnRelease.TabIndex = 4;
            btnRelease.Text = "Liberar Pokemon";
            btnRelease.Click += btnRelease_Click;
            // 
            // btnEditNickname
            // 
            btnEditNickname.CustomizableEdges = customizableEdges3;
            btnEditNickname.Image = Properties.Resources.edit;
            btnEditNickname.ImageRotate = 0F;
            btnEditNickname.Location = new Point(6, 382);
            btnEditNickname.Name = "btnEditNickname";
            btnEditNickname.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnEditNickname.Size = new Size(26, 23);
            btnEditNickname.SizeMode = PictureBoxSizeMode.Zoom;
            btnEditNickname.TabIndex = 3;
            btnEditNickname.TabStop = false;
            btnEditNickname.Click += btnEditNickname_Click;
            // 
            // btnMoveToTeam
            // 
            btnMoveToTeam.CustomizableEdges = customizableEdges5;
            btnMoveToTeam.DisabledState.BorderColor = Color.DarkGray;
            btnMoveToTeam.DisabledState.CustomBorderColor = Color.DarkGray;
            btnMoveToTeam.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnMoveToTeam.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnMoveToTeam.FillColor = Color.FromArgb(221, 23, 58);
            btnMoveToTeam.Font = new Font("Segoe UI", 9F);
            btnMoveToTeam.ForeColor = Color.White;
            btnMoveToTeam.Image = Properties.Resources.back;
            btnMoveToTeam.ImageAlign = HorizontalAlignment.Left;
            btnMoveToTeam.Location = new Point(225, 603);
            btnMoveToTeam.Name = "btnMoveToTeam";
            btnMoveToTeam.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnMoveToTeam.Size = new Size(180, 45);
            btnMoveToTeam.TabIndex = 2;
            btnMoveToTeam.Text = "Mover para o time";
            btnMoveToTeam.Click += btnMoveToTeam_Click;
            // 
            // lblDetailLocation
            // 
            lblDetailLocation.BackColor = Color.Transparent;
            lblDetailLocation.Font = new Font("Segoe UI", 10F);
            lblDetailLocation.Location = new Point(6, 566);
            lblDetailLocation.Name = "lblDetailLocation";
            lblDetailLocation.Size = new Size(102, 19);
            lblDetailLocation.TabIndex = 1;
            lblDetailLocation.Text = "Capturado onde:";
            // 
            // lblDetailCapturedDate
            // 
            lblDetailCapturedDate.BackColor = Color.Transparent;
            lblDetailCapturedDate.Font = new Font("Segoe UI", 10F);
            lblDetailCapturedDate.Location = new Point(6, 629);
            lblDetailCapturedDate.Name = "lblDetailCapturedDate";
            lblDetailCapturedDate.Size = new Size(117, 19);
            lblDetailCapturedDate.TabIndex = 1;
            lblDetailCapturedDate.Text = "Capturado quando:";
            // 
            // lblDetailOwnerName
            // 
            lblDetailOwnerName.BackColor = Color.Transparent;
            lblDetailOwnerName.Font = new Font("Segoe UI", 10F);
            lblDetailOwnerName.Location = new Point(6, 502);
            lblDetailOwnerName.Name = "lblDetailOwnerName";
            lblDetailOwnerName.Size = new Size(97, 19);
            lblDetailOwnerName.TabIndex = 1;
            lblDetailOwnerName.Text = "Nome do dono:";
            // 
            // lblDetailLevel
            // 
            lblDetailLevel.BackColor = Color.Transparent;
            lblDetailLevel.Font = new Font("Segoe UI", 14F);
            lblDetailLevel.Location = new Point(310, 273);
            lblDetailLevel.Name = "lblDetailLevel";
            lblDetailLevel.Size = new Size(46, 27);
            lblDetailLevel.TabIndex = 1;
            lblDetailLevel.Text = "Nivel";
            // 
            // lblDetailType
            // 
            lblDetailType.BackColor = Color.Transparent;
            lblDetailType.Font = new Font("Segoe UI", 14F);
            lblDetailType.Location = new Point(6, 422);
            lblDetailType.Name = "lblDetailType";
            lblDetailType.Size = new Size(40, 27);
            lblDetailType.TabIndex = 1;
            lblDetailType.Text = "Tipo";
            // 
            // lblDetailNickname
            // 
            lblDetailNickname.BackColor = Color.Transparent;
            lblDetailNickname.Font = new Font("Segoe UI", 18F);
            lblDetailNickname.Location = new Point(6, 342);
            lblDetailNickname.Name = "lblDetailNickname";
            lblDetailNickname.Size = new Size(85, 34);
            lblDetailNickname.TabIndex = 1;
            lblDetailNickname.Text = "Apelido";
            // 
            // lblDetailName
            // 
            lblDetailName.BackColor = Color.Transparent;
            lblDetailName.Font = new Font("Segoe UI", 18F);
            lblDetailName.Location = new Point(6, 266);
            lblDetailName.Name = "lblDetailName";
            lblDetailName.Size = new Size(69, 34);
            lblDetailName.TabIndex = 1;
            lblDetailName.Text = "Nome";
            // 
            // pbDetailImage
            // 
            pbDetailImage.ImageRotate = 0F;
            pbDetailImage.Location = new Point(84, 4);
            pbDetailImage.Name = "pbDetailImage";
            pbDetailImage.ShadowDecoration.CustomizableEdges = customizableEdges7;
            pbDetailImage.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            pbDetailImage.Size = new Size(256, 256);
            pbDetailImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbDetailImage.TabIndex = 0;
            pbDetailImage.TabStop = false;
            pbDetailImage.Visible = false;
            // 
            // ucPcBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlPokemonDetails);
            Controls.Add(flpPokemonGrid);
            Name = "ucPcBox";
            Size = new Size(1148, 650);
            Load += ucPcBox_Load;
            pnlPokemonDetails.ResumeLayout(false);
            pnlPokemonDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnEditNickname).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbDetailImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpPokemonGrid;
        private Guna.UI2.WinForms.Guna2Panel pnlPokemonDetails;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDetailLevel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDetailType;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDetailNickname;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDetailName;
        private Guna.UI2.WinForms.Guna2CirclePictureBox pbDetailImage;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDetailCapturedDate;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDetailOwnerName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDetailLocation;
        private Guna.UI2.WinForms.Guna2Button btnMoveToTeam;
        private Guna.UI2.WinForms.Guna2PictureBox btnEditNickname;
        private Guna.UI2.WinForms.Guna2Button btnRelease;
    }
}

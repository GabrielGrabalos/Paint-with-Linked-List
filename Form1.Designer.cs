namespace InfinityPaint
{
    partial class frmGrafico
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGrafico));
            this.pbAreaDesenho = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.btnSalvar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDesfazer = new System.Windows.Forms.ToolStripButton();
            this.btnRefazer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPonto = new System.Windows.Forms.ToolStripButton();
            this.btnReta = new System.Windows.Forms.ToolStripButton();
            this.btnCirculo = new System.Windows.Forms.ToolStripButton();
            this.btnElipse = new System.Windows.Forms.ToolStripButton();
            this.btnRetangulo = new System.Windows.Forms.ToolStripButton();
            this.btnPolilinha = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.btnCor = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.displayDeCor = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAumentarEspessura = new System.Windows.Forms.ToolStripButton();
            this.tbEspessura = new System.Windows.Forms.ToolStripTextBox();
            this.btnDiminuirEspessura = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.btnSair = new System.Windows.Forms.ToolStripButton();
            this.stMensagem = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cdSeletorDeCor = new System.Windows.Forms.ColorDialog();
            this.dlgAbrir = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbAreaDesenho)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.stMensagem.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbAreaDesenho
            // 
            this.pbAreaDesenho.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAreaDesenho.BackColor = System.Drawing.Color.White;
            this.pbAreaDesenho.Location = new System.Drawing.Point(0, 47);
            this.pbAreaDesenho.Name = "pbAreaDesenho";
            this.pbAreaDesenho.Size = new System.Drawing.Size(800, 378);
            this.pbAreaDesenho.TabIndex = 0;
            this.pbAreaDesenho.TabStop = false;
            this.pbAreaDesenho.Paint += new System.Windows.Forms.PaintEventHandler(this.pbAreaDesenho_Paint);
            this.pbAreaDesenho.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbAreaDesenho_MouseClick);
            this.pbAreaDesenho.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbAreaDesenho_MouseDoubleClick);
            this.pbAreaDesenho.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbAreaDesenho_MouseDown);
            this.pbAreaDesenho.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbAreaDesenho_MouseMove);
            this.pbAreaDesenho.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbAreaDesenho_MouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAbrir,
            this.btnSalvar,
            this.toolStripSeparator1,
            this.btnDesfazer,
            this.btnRefazer,
            this.toolStripSeparator5,
            this.btnPonto,
            this.btnReta,
            this.btnCirculo,
            this.btnElipse,
            this.btnRetangulo,
            this.btnPolilinha,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.btnCor,
            this.toolStripButton1,
            this.displayDeCor,
            this.toolStripButton2,
            this.toolStripSeparator3,
            this.btnAumentarEspessura,
            this.tbEspessura,
            this.btnDiminuirEspessura,
            this.toolStripSeparator4,
            this.toolStripButton4,
            this.btnSair});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 44);
            this.toolStrip1.TabIndex = 1;
            // 
            // btnAbrir
            // 
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(37, 41);
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.Image")));
            this.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(42, 41);
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 44);
            // 
            // btnDesfazer
            // 
            this.btnDesfazer.Enabled = false;
            this.btnDesfazer.Image = ((System.Drawing.Image)(resources.GetObject("btnDesfazer.Image")));
            this.btnDesfazer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDesfazer.Name = "btnDesfazer";
            this.btnDesfazer.Size = new System.Drawing.Size(55, 41);
            this.btnDesfazer.Text = "Desfazer";
            this.btnDesfazer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDesfazer.Click += new System.EventHandler(this.btnDesfazer_Click);
            // 
            // btnRefazer
            // 
            this.btnRefazer.Enabled = false;
            this.btnRefazer.Image = ((System.Drawing.Image)(resources.GetObject("btnRefazer.Image")));
            this.btnRefazer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefazer.Name = "btnRefazer";
            this.btnRefazer.Size = new System.Drawing.Size(49, 41);
            this.btnRefazer.Text = "Refazer";
            this.btnRefazer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRefazer.Click += new System.EventHandler(this.btnRefazer_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 44);
            // 
            // btnPonto
            // 
            this.btnPonto.Image = ((System.Drawing.Image)(resources.GetObject("btnPonto.Image")));
            this.btnPonto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPonto.Name = "btnPonto";
            this.btnPonto.Size = new System.Drawing.Size(43, 41);
            this.btnPonto.Text = "Ponto";
            this.btnPonto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPonto.Click += new System.EventHandler(this.btnPonto_Click);
            // 
            // btnReta
            // 
            this.btnReta.Image = ((System.Drawing.Image)(resources.GetObject("btnReta.Image")));
            this.btnReta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReta.Name = "btnReta";
            this.btnReta.Size = new System.Drawing.Size(34, 41);
            this.btnReta.Text = "Reta";
            this.btnReta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReta.Click += new System.EventHandler(this.btnReta_Click);
            // 
            // btnCirculo
            // 
            this.btnCirculo.BackColor = System.Drawing.SystemColors.Control;
            this.btnCirculo.Image = ((System.Drawing.Image)(resources.GetObject("btnCirculo.Image")));
            this.btnCirculo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCirculo.Name = "btnCirculo";
            this.btnCirculo.Size = new System.Drawing.Size(49, 41);
            this.btnCirculo.Text = "Círculo";
            this.btnCirculo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCirculo.Click += new System.EventHandler(this.btnCirculo_Click);
            // 
            // btnElipse
            // 
            this.btnElipse.Image = ((System.Drawing.Image)(resources.GetObject("btnElipse.Image")));
            this.btnElipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnElipse.Name = "btnElipse";
            this.btnElipse.Size = new System.Drawing.Size(41, 41);
            this.btnElipse.Text = "Elipse";
            this.btnElipse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnElipse.Click += new System.EventHandler(this.btnElipse_Click);
            // 
            // btnRetangulo
            // 
            this.btnRetangulo.Image = ((System.Drawing.Image)(resources.GetObject("btnRetangulo.Image")));
            this.btnRetangulo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRetangulo.Name = "btnRetangulo";
            this.btnRetangulo.Size = new System.Drawing.Size(65, 41);
            this.btnRetangulo.Text = "Retângulo";
            this.btnRetangulo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRetangulo.Click += new System.EventHandler(this.btnRetangulo_Click);
            // 
            // btnPolilinha
            // 
            this.btnPolilinha.Image = ((System.Drawing.Image)(resources.GetObject("btnPolilinha.Image")));
            this.btnPolilinha.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPolilinha.Name = "btnPolilinha";
            this.btnPolilinha.Size = new System.Drawing.Size(60, 41);
            this.btnPolilinha.Text = "PoliLinha";
            this.btnPolilinha.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPolilinha.Click += new System.EventHandler(this.btnPolilinha_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 44);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.AutoSize = false;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripButton3.Enabled = false;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(12, 41);
            // 
            // btnCor
            // 
            this.btnCor.Image = ((System.Drawing.Image)(resources.GetObject("btnCor.Image")));
            this.btnCor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCor.Name = "btnCor";
            this.btnCor.Size = new System.Drawing.Size(30, 41);
            this.btnCor.Text = "Cor";
            this.btnCor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCor.ToolTipText = "Seletor de cor";
            this.btnCor.Click += new System.EventHandler(this.btnCor_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(12, 41);
            // 
            // displayDeCor
            // 
            this.displayDeCor.AutoSize = false;
            this.displayDeCor.BackColor = System.Drawing.Color.Black;
            this.displayDeCor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.displayDeCor.Enabled = false;
            this.displayDeCor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.displayDeCor.Name = "displayDeCor";
            this.displayDeCor.Size = new System.Drawing.Size(23, 23);
            this.displayDeCor.ToolTipText = "Cor selecionada";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(12, 41);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 44);
            // 
            // btnAumentarEspessura
            // 
            this.btnAumentarEspessura.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAumentarEspessura.Image = ((System.Drawing.Image)(resources.GetObject("btnAumentarEspessura.Image")));
            this.btnAumentarEspessura.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAumentarEspessura.Name = "btnAumentarEspessura";
            this.btnAumentarEspessura.Size = new System.Drawing.Size(26, 41);
            this.btnAumentarEspessura.ToolTipText = "Aumentar espessura da caneta";
            this.btnAumentarEspessura.Click += new System.EventHandler(this.btnAumentarEspessura_Click);
            // 
            // tbEspessura
            // 
            this.tbEspessura.AutoSize = false;
            this.tbEspessura.Enabled = false;
            this.tbEspessura.Name = "tbEspessura";
            this.tbEspessura.Size = new System.Drawing.Size(80, 44);
            this.tbEspessura.Text = "Espessura: 1";
            this.tbEspessura.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbEspessura.ToolTipText = "Espesura da caneta";
            // 
            // btnDiminuirEspessura
            // 
            this.btnDiminuirEspessura.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDiminuirEspessura.Enabled = false;
            this.btnDiminuirEspessura.Image = ((System.Drawing.Image)(resources.GetObject("btnDiminuirEspessura.Image")));
            this.btnDiminuirEspessura.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDiminuirEspessura.Name = "btnDiminuirEspessura";
            this.btnDiminuirEspessura.Size = new System.Drawing.Size(26, 41);
            this.btnDiminuirEspessura.ToolTipText = "Diminuir espessura da caneta";
            this.btnDiminuirEspessura.Click += new System.EventHandler(this.btnDiminuirEspessura_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 44);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.AutoSize = false;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripButton4.Enabled = false;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(8, 41);
            // 
            // btnSair
            // 
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(30, 41);
            this.btnSair.Text = "Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // stMensagem
            // 
            this.stMensagem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.stMensagem.Location = new System.Drawing.Point(0, 428);
            this.stMensagem.Name = "stMensagem";
            this.stMensagem.Size = new System.Drawing.Size(800, 22);
            this.stMensagem.TabIndex = 2;
            this.stMensagem.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLabel1.Text = "Mensagem:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(92, 17);
            this.toolStripStatusLabel2.Text = "Sem mensagem";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(81, 17);
            this.toolStripStatusLabel3.Text = "Coordenadas:";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel4.Text = "x, y";
            // 
            // cdSeletorDeCor
            // 
            this.cdSeletorDeCor.AnyColor = true;
            this.cdSeletorDeCor.FullOpen = true;
            this.cdSeletorDeCor.ShowHelp = true;
            // 
            // dlgAbrir
            // 
            this.dlgAbrir.FileName = "openFileDialog1";
            // 
            // frmGrafico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stMensagem);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pbAreaDesenho);
            this.Name = "frmGrafico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pbAreaDesenho)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.stMensagem.ResumeLayout(false);
            this.stMensagem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pbAreaDesenho;
        private ToolStrip toolStrip1;
        private ToolStripButton btnAbrir;
        private ToolStripButton btnSalvar;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnPonto;
        private ToolStripButton btnReta;
        private ToolStripButton btnCirculo;
        private ToolStripButton btnElipse;
        private ToolStripButton btnRetangulo;
        private ToolStripButton btnPolilinha;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnCor;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnSair;
        private StatusStrip stMensagem;
        private ColorDialog cdSeletorDeCor;
        private OpenFileDialog dlgAbrir;
        private ToolStripButton displayDeCor;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private ToolStripButton toolStripButton4;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnAumentarEspessura;
        private ToolStripTextBox tbEspessura;
        private ToolStripButton btnDiminuirEspessura;
        private ToolStripButton btnDesfazer;
        private ToolStripButton btnRefazer;
        private ToolStripSeparator toolStripSeparator5;
    }
}
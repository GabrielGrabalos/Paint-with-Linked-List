using System.Drawing;
using System.Drawing.Text;
using static System.Windows.Forms.LinkLabel;

namespace InfinityPaint
{
    public partial class frmGrafico : Form
    {
        // Variáveis de controle
        bool esperaPonto           = false;
        bool esperaInicioReta      = false;
        bool esperaFimReta         = false;
        bool esperaInicioCirculo   = false;
        bool esperaFimCirculo      = false;
        bool esperaInicioElipse    = false;
        bool esperaFimElipse       = false;
        bool esperaInicioRetangulo = false;
        bool esperaFimRetangulo    = false;
        bool polilinha             = false;
        bool drag                  = false;

        string editando = "";

        int espessura = 1;

        private ListaSimples<Ponto> figuras = new ListaSimples<Ponto>();
        private ListaSimples<Ponto> figurasDesfeitas = new ListaSimples<Ponto>();
        Color corAtual = Color.Black;

        // Pontos auxiliares:
        private static Ponto p1 = new Ponto(0, 0, Color.Black, 1);
        private static Ponto p2 = new Ponto(0, 0, Color.Black, 1); // Ponto temporário.

        ToolStripButton btnAtivado = null;
        CaixaDeEdicao caixaDeEdicao;// = new CaixaDeEdicao(10, 10, 100, 100, null);

        // Tratadores de evento:

        public frmGrafico()
        {
            InitializeComponent();
            pbAreaDesenho.KeyPress += new KeyPressEventHandler(pbAreaDesenho_KeyPress);
        }

        // Eventos click (em ordem):

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
                LerArquivo();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarArquivo();
        }

        private void btnDesfazer_Click(object sender, EventArgs e)
        {
            Desfazer();
        }

        private void btnRefazer_Click(object sender, EventArgs e)
        {
            Refazer();
        }

        private void btnPonto_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaPonto, btnPonto, "Clique no local do ponto desejado:");
        }

        private void btnReta_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaInicioReta, btnReta, "Clique no local do ponto inicial da reta:");
        }

        private void btnCirculo_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaInicioCirculo, btnCirculo, "Clique no local do ponto inicial do círculo:");
        }

        private void btnElipse_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaInicioElipse, btnElipse, "Clique no local do ponto inicial da elipse:");
        }

        private void btnRetangulo_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaInicioRetangulo, btnRetangulo, "Clique no local do ponto inicial do retângulo:");
        }

        private void btnPolilinha_Click(object sender, EventArgs e)
        {
            esperaInicioReta = false;
            BtnConfig(ref esperaInicioReta, btnPolilinha, "Clique no local do ponto inicial das retas:");
            polilinha = true;
        }

        private void btnCor_Click(object sender, EventArgs e)
        {
            if (cdSeletorDeCor.ShowDialog() == DialogResult.OK)
            {
                corAtual = cdSeletorDeCor.Color;
                displayDeCor.BackColor = corAtual;
            }
        }

        private void btnAumentarEspessura_Click(object sender, EventArgs e)
        {
            espessura++;

            if (espessura == 10)
            {
                btnAumentarEspessura.Enabled = false;
            }
            else if (!btnDiminuirEspessura.Enabled)
            {
                btnDiminuirEspessura.Enabled = true;
            }

            tbEspessura.Text = "Espessura: " + espessura;
        }

        private void btnDiminuirEspessura_Click(object sender, EventArgs e)
        {
            espessura--;

            if (espessura == 1)
            {
                btnDiminuirEspessura.Enabled = false;
            }
            else if (!btnAumentarEspessura.Enabled)
            {
                btnAumentarEspessura.Enabled = true;
            }

            tbEspessura.Text = "Espessura: " + espessura;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair?", "Sair",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Eventos mouse (pbAreaDesenhos):

        private void pbAreaDesenho_MouseMove(object sender, MouseEventArgs e)
        {
            if (caixaDeEdicao != null)
            {
                string estadoSeta = caixaDeEdicao.IsHovering(e.X, e.Y);

                if(editando != "")
                {
                    estadoSeta = editando;
                }

                if (estadoSeta != "")
                {
                    switch (estadoSeta)
                    {
                        case "N":
                            this.Cursor = Cursors.SizeNS;
                            break;
                        case "S":
                            this.Cursor = Cursors.SizeNS;
                            break;
                        case "E":
                            this.Cursor = Cursors.SizeWE;
                            break;
                        case "W":
                            this.Cursor = Cursors.SizeWE;
                            break;
                        case "NE":
                            this.Cursor = Cursors.SizeNESW;
                            break;
                        case "NW":
                            this.Cursor = Cursors.SizeNWSE;
                            break;
                        case "SE":
                            this.Cursor = Cursors.SizeNWSE;
                            break;
                        case "SW":
                            this.Cursor = Cursors.SizeNESW;
                            break;
                        case "C":
                            this.Cursor = Cursors.SizeAll;
                            break;
                    }
                }
                else if (this.Cursor != Cursors.Default && editando == "")
                {
                    this.Cursor = Cursors.Default;
                }


                if (drag)
                {
                    caixaDeEdicao.Move(editando, e.X, e.Y);
                    pbAreaDesenho.Invalidate();
                }
            }

            AtualizarP2(e.X, e.Y);

            stMensagem.Items[3].Text = "x: " + e.X + ", y: " + e.Y;
        }

        private void pbAreaDesenho_MouseClick(object sender, MouseEventArgs e)
        {
            if (esperaPonto)
            {
                EsperaPonto();
            }
            else if (esperaInicioReta)
            {
                EsperaInicioReta();
            }
            else if (esperaFimReta)
            {
                EsperaFimReta();
            }
            else if (esperaInicioCirculo)
            {
                EsperaInicioCirculo();
            }
            else if (esperaFimCirculo)
            {
                EsperaFimCirculo();
            }
            else if (esperaInicioElipse)
            {
                EsperaInicioElipse();
            }
            else if (esperaFimElipse)
            {
                EsperaFimElipse();
            }
            else if (esperaInicioRetangulo)
            {
                EsperaInicioRetangulo();
            }
            else if (esperaFimRetangulo)
            {
                EsperaFimRetangulo();
            }
            else if (caixaDeEdicao != null)
            {
                if (editando == "")
                {
                    ConfirmarCaixaDeEdicao();
                }
            }
        }

        private void ConfirmarCaixaDeEdicao()
        {
            figuras.InserirAposFim(new NoLista<Ponto>(caixaDeEdicao.FiguraInterna, null));
            caixaDeEdicao.FiguraInterna.desenhar(caixaDeEdicao.FiguraInterna.Cor, pbAreaDesenho.CreateGraphics());

            btnAtivado.BackColor = SystemColors.Control;

            if (!btnDesfazer.Enabled) btnDesfazer.Enabled = true;

            caixaDeEdicao = null;

            if (Cursor != Cursors.Default)
            {
                Cursor = Cursors.Default;
            }

            pbAreaDesenho.Invalidate();
        }

        private void pbAreaDesenho_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (polilinha)
            {
                polilinha     = false;
                esperaFimReta = false;
                btnPolilinha.BackColor = SystemColors.Control;
            }
        }

        // Evento KeyPress (pbAreaDesenhos):
        private void pbAreaDesenho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (polilinha && (Keys)e.KeyChar == Keys.Enter)
            {
                polilinha = false;
            }
        }

        // Evento Paint (pbAreaDesenhos):
        private void pbAreaDesenho_Paint(object sender, PaintEventArgs e)
        {
            DesenharFiguras(e.Graphics); // acessa contexto gráfico e desenha
        }

        // Funções:

        private void limparEsperas()
        {
            esperaPonto           = false;
            esperaInicioReta      = false;
            esperaFimReta         = false;
            esperaInicioCirculo   = false;
            esperaFimCirculo      = false;
            esperaInicioElipse    = false;
            esperaFimElipse       = false;
            esperaInicioRetangulo = false;
            esperaFimRetangulo    = false;
            polilinha             = false;
            drag                  = false;

            editando = "";
        }

        private void AtualizarP2(int mouseX, int mouseY)
        {
            if (esperaInicioReta || esperaInicioCirculo || esperaInicioElipse || esperaInicioRetangulo ||
                esperaFimReta    || esperaFimCirculo    || esperaFimElipse    || esperaFimRetangulo    || 
                esperaPonto )
            {
                p2.X = mouseX;
                p2.Y = mouseY;

                pbAreaDesenho.Invalidate();
            }
        }

        private void BtnConfig(ref bool boolEspera, ToolStripButton btnAtual, string mensagem)
        {
            if (!boolEspera)
            {
                limparEsperas();
                boolEspera = true;
                btnAtual.BackColor = SystemColors.ButtonShadow;
                stMensagem.Items[1].Text = "";

                if (btnAtivado != null)
                    btnAtivado.BackColor = SystemColors.Control;

                btnAtivado = btnAtual;
            }
            else
            {
                boolEspera = false;
                btnAtual.BackColor = SystemColors.Control;
                stMensagem.Items[1].Text = mensagem;
                btnAtivado = null;
            }

            if (caixaDeEdicao != null)
            {
                ConfirmarCaixaDeEdicao();
            }
        }

        private void EsperaPonto()
        {
            esperaPonto = false;

            Ponto novoPonto = new Ponto(p2.X, p2.Y, corAtual, espessura);

            figuras.InserirAposFim(new NoLista<Ponto>(novoPonto, null));
            novoPonto.desenhar(novoPonto.Cor, pbAreaDesenho.CreateGraphics());

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas();

            btnAtivado.BackColor = SystemColors.Control;

            if (!btnDesfazer.Enabled) btnDesfazer.Enabled = true;

            stMensagem.Items[1].Text = "";
        }

        private void EsperaInicioReta()
        {
            esperaInicioReta = false;
            esperaFimReta    = true;

            p1.X = p2.X;
            p1.Y = p2.Y;
            p1.Cor = corAtual;

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas();

            stMensagem.Items[1].Text = "Mensagem: clique o ponto final da reta";
        }

        private void EsperaFimReta()
        {
            esperaInicioReta = false;

            Reta novaLinha = new Reta(p1.X, p1.Y, p2.X, p2.Y, corAtual, espessura);

            if (polilinha)
            {
                p1.X = p2.X;
                p1.Y = p2.Y;
            }
            else
            {
                esperaFimReta = false;
            }

            figuras.InserirAposFim(new NoLista<Ponto>(novaLinha, null));
            novaLinha.desenhar(novaLinha.Cor, pbAreaDesenho.CreateGraphics());

            btnAtivado.BackColor = SystemColors.Control;

            if (!btnDesfazer.Enabled) btnDesfazer.Enabled = true;

            pbAreaDesenho.Invalidate();
        }

        private void EsperaInicioCirculo()
        {
            esperaInicioCirculo = false;
            esperaFimCirculo    = true;

            p1.X = p2.X;
            p1.Y = p2.Y;
            p1.Cor = corAtual;

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas();

            stMensagem.Items[1].Text = "Mensagem: clique o ponto final do círculo";
        }
        private void EsperaFimCirculo()
        {
            esperaInicioCirculo = false;
            esperaFimCirculo    = false;

            int raio = (int)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));

            Circulo novoCirculo = new Circulo(p1.X, p1.Y, raio, p1.Cor, espessura);

            caixaDeEdicao = new CaixaDeEdicao(novoCirculo.X - novoCirculo.Raio, novoCirculo.Y - novoCirculo.Raio, novoCirculo.Raio * 2, novoCirculo.Raio * 2, novoCirculo);
            caixaDeEdicao.desenhar(corAtual, pbAreaDesenho.CreateGraphics());
        }

        private void EsperaInicioElipse()
        {
            esperaInicioElipse = false;
            esperaFimElipse    = true;

            p1.X = p2.X;
            p1.Y = p2.Y;
            p1.Cor = corAtual;

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas();

            stMensagem.Items[1].Text = "Mensagem: clique o ponto final da elipse";
        }

        private void EsperaFimElipse()
        {
            esperaInicioElipse = false;
            esperaFimElipse    = false;

            int raio1 = Math.Abs(p2.X - p1.X);
            int raio2 = Math.Abs(p2.Y - p1.Y);

            Elipse novaElipse = new Elipse(p1.X, p1.Y, raio1, raio2, p1.Cor, espessura);

            caixaDeEdicao = new CaixaDeEdicao(novaElipse.X - novaElipse.Raio1, novaElipse.Y - novaElipse.Raio2, novaElipse.Raio1 * 2, novaElipse.Raio2 * 2, novaElipse);
            caixaDeEdicao.desenhar(corAtual, pbAreaDesenho.CreateGraphics());
        }

        private void EsperaInicioRetangulo()
        {
            esperaInicioRetangulo = false;
            esperaFimRetangulo    = true;

            p1.X = p2.X;
            p1.Y = p2.Y;
            p1.Cor = corAtual;

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas();

            stMensagem.Items[1].Text = "Mensagem: clique o ponto final do retângulo";
        }

        private void EsperaFimRetangulo()
        {
            esperaInicioRetangulo = false;
            esperaFimRetangulo    = false;

            int largura = Math.Abs(p2.X - p1.X);
            int altura  = Math.Abs(p2.Y - p1.Y);

            int x1, y1;

            if (p1.X < p2.X && p1.Y < p2.Y)
            {
                x1 = p1.X;
                y1 = p1.Y;
            }
            else if (p1.X > p2.X && p1.Y < p2.Y)
            {
                x1 = p2.X;
                y1 = p1.Y;
            }
            else if (p1.X < p2.X && p1.Y > p2.Y)
            {
                x1 = p1.X;
                y1 = p2.Y;
            }
            else
            {
                x1 = p2.X;
                y1 = p2.Y;
            }

            Retangulo novoRetangulo = new Retangulo(x1, y1, largura, altura, p1.Cor, espessura);

            caixaDeEdicao = new CaixaDeEdicao(novoRetangulo.X, novoRetangulo.Y, novoRetangulo.Largura, novoRetangulo.Altura, novoRetangulo);
            caixaDeEdicao.desenhar(corAtual, pbAreaDesenho.CreateGraphics());
        }

        private void DesenharFiguras(Graphics g)
        {
            if (caixaDeEdicao != null)
                caixaDeEdicao.desenhar(caixaDeEdicao.Cor, g);

            figuras.iniciarPercursoSequencial();

            while (figuras.podePercorrer())
            {
                Ponto figuraAtual = figuras.Atual.Info;
                figuraAtual.desenhar(figuraAtual.Cor, g);
            }

            if (esperaFimReta)
            {
                Pen pen = new Pen(p1.Cor, espessura);

                g.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
            }
            else if (esperaFimCirculo)
            {
                Pen pen = new Pen(p1.Cor, espessura);

                int raio = (int)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));

                g.DrawEllipse(pen, p1.X - raio, p1.Y - raio, // centro - raio
                              2 * raio, 2 * raio);
            }
            else if (esperaFimElipse)
            {
                Pen pen = new Pen(p1.Cor, espessura);

                int raio1 = Math.Abs(p1.X - p2.X);
                int raio2 = Math.Abs(p1.Y - p2.Y);

                g.DrawEllipse(pen, p1.X - raio1, p1.Y - raio2, // centro - raio
                              2 * raio1, 2 * raio2);
            }
            else if (esperaFimRetangulo)
            {
                Pen pen = new Pen(p1.Cor, espessura);

                int largura = Math.Abs(p2.X - p1.X);
                int altura = Math.Abs(p2.Y - p1.Y);

                int x1, y1;

                if (p1.X < p2.X && p1.Y < p2.Y)
                {
                    x1 = p1.X;
                    y1 = p1.Y;
                }
                else if (p1.X > p2.X && p1.Y < p2.Y)
                {
                    x1 = p2.X;
                    y1 = p1.Y;
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    x1 = p1.X;
                    y1 = p2.Y;
                }
                else
                {
                    x1 = p2.X;
                    y1 = p2.Y;
                }

                g.DrawRectangle(pen, x1, y1, largura, altura);
            }
        }

        private void SalvarArquivo()
        {
            SaveFileDialog salvar = new SaveFileDialog();

            salvar.Filter = "Arquivo de desenho (*.txt)|*.txt";
            salvar.Title = "Salvar desenho";

            salvar.ShowDialog();

            if (salvar.FileName != "")
            {
                FileStream arquivo = new FileStream(salvar.FileName, FileMode.Create);
                StreamWriter escritor = new StreamWriter(arquivo);

                int xSupDir = 0;
                int ySupDir = 0;
                int xInfEsq = this.Width;
                int yInfEsq = this.Height;

                Ponto aux = new Ponto(1, 1, Color.Black, 1);

                escritor.Write(aux.transformaString(xInfEsq, 5));
                escritor.Write(aux.transformaString(yInfEsq, 5));
                escritor.Write(aux.transformaString(xSupDir, 5));
                escritor.Write(aux.transformaString(ySupDir, 5));
                escritor.WriteLine();

                figuras.iniciarPercursoSequencial();

                while (figuras.podePercorrer())
                {
                    escritor.WriteLine(figuras.Atual.Info.ToString());
                }

                escritor.Close();
                arquivo.Close();
            }
        }

        private void LerArquivo()
        {
            try
            {
                StreamReader arqFiguras = new StreamReader(dlgAbrir.FileName);

                String linha = arqFiguras.ReadLine();

                Double xInfEsq = Convert.ToDouble(linha.Substring( 0, 5).Trim());
                Double yInfEsq = Convert.ToDouble(linha.Substring( 5, 5).Trim());
                Double xSupDir = Convert.ToDouble(linha.Substring(10, 5).Trim());
                Double ySupDir = Convert.ToDouble(linha.Substring(15, 5).Trim());

                while ((linha = arqFiguras.ReadLine()) != null)
                {
                    String tipo = linha.Substring(0, 5).Trim();

                    int xBase = Convert.ToInt32(linha.Substring( 5, 5).Trim());
                    int yBase = Convert.ToInt32(linha.Substring(10, 5).Trim());
                    int corR  = Convert.ToInt32(linha.Substring(15, 5).Trim());
                    int corG  = Convert.ToInt32(linha.Substring(20, 5).Trim());
                    int corB  = Convert.ToInt32(linha.Substring(25, 5).Trim());

                    int esp;

                    try
                    {
                        esp = Convert.ToInt32(linha.Substring(40, 5).Trim());
                    }
                    catch (Exception)
                    {
                        esp = 1;
                    }

                    Color cor = new Color();
                    cor = Color.FromArgb(255, corR, corG, corB);

                    switch (tipo[0])
                    {
                        // Ponto:
                        case 'p':
                            figuras.InserirAposFim(
                            new NoLista<Ponto>(new Ponto(xBase, yBase, cor, esp), null));
                            break;

                        // Linha (reta):
                        case 'l':
                            int xFinal = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            int yFinal = Convert.ToInt32(linha.Substring(35, 5).Trim());

                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Reta(xBase, yBase, xFinal, yFinal, cor, esp), null));

                            break;

                        // Círculo:
                        case 'c':
                            int raio = Convert.ToInt32(linha.Substring(30, 5).Trim());

                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Circulo(xBase, yBase, raio, cor, esp), null));

                            break;

                        // Elipse
                        case 'e':
                            int raio1 = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            int raio2 = Convert.ToInt32(linha.Substring(35, 5).Trim());

                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Elipse(xBase, yBase, raio1, raio2, cor, esp), null));

                            break;

                        // Retângulo:
                        case 'r': 
                            int largura = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            int altura  = Convert.ToInt32(linha.Substring(35, 5).Trim());

                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Retangulo(xBase, yBase, largura, altura, cor, esp), null));

                            break;
                    }
                }

                arqFiguras.Close();
                this.Text = dlgAbrir.FileName;
                pbAreaDesenho.Invalidate();
            }
            catch (IOException)
            {
                MessageBox.Show("Erro de leitura no arquivo", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Desfazer()
        {
            if (!figuras.EstaVazia)
            {
                figurasDesfeitas.InserirAposFim(figuras.RemoverUltimo()); // Remove a figura da lista figuras
                                                                          // e adiciona nas figuras desfeitas.
                pbAreaDesenho.Invalidate();

                if (figuras.EstaVazia) btnDesfazer.Enabled = false;

                if (!btnRefazer.Enabled) btnRefazer.Enabled = true;
            }
        }

        private void Refazer()
        {
            figuras.InserirAposFim(figurasDesfeitas.RemoverUltimo());

            pbAreaDesenho.Invalidate();

            if (figurasDesfeitas.EstaVazia) btnRefazer.Enabled = false;

            if (!btnDesfazer.Enabled) btnDesfazer.Enabled = true;
        }

        private void ResetarFigurasDesfeitas()
        {
            figurasDesfeitas.Limpar();
            btnRefazer.Enabled = false;
        }

        private void pbAreaDesenho_MouseDown(object sender, MouseEventArgs e)
        {
            if (caixaDeEdicao != null)
            {
                drag = true;

                editando = caixaDeEdicao.IsHovering(e.X, e.Y);
            }
        }

        private void pbAreaDesenho_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            editando = "";
        }
    }
}
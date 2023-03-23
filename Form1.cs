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

        int espessura = 1;

        private ListaSimples<Ponto> figuras = new ListaSimples<Ponto>();
        Color corAtual = Color.Black;

        // Pontos auxiliares:
        private static Ponto p1 = new Ponto(0, 0, Color.Black, 1);
        private static Ponto p2 = new Ponto(0, 0, Color.Black, 1); // Ponto temporário.

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

        private void btnPonto_Click(object sender, EventArgs e)
        {
            limparEsperas();

            esperaPonto = true;

            stMensagem.Items[1].Text = "Clique no local do ponto desejado:";
        }

        private void btnReta_Click(object sender, EventArgs e)
        {
            limparEsperas();

            esperaInicioReta = true;

            stMensagem.Items[1].Text = "Clique no local do ponto inicial da reta:";
        }

        private void btnCirculo_Click(object sender, EventArgs e)
        {
            limparEsperas();

            esperaInicioCirculo = true;

            stMensagem.Items[1].Text = "Clique no local do ponto inicial do círculo:";
        }

        private void btnElipse_Click(object sender, EventArgs e)
        {
            limparEsperas();

            esperaInicioElipse = true;

            stMensagem.Items[1].Text = "Clique no local do ponto inicial da elipse:";
        }

        private void btnRetangulo_Click(object sender, EventArgs e)
        {
            limparEsperas();

            esperaInicioRetangulo = true;

            stMensagem.Items[1].Text = "Clique no local do ponto inicial do retângulo:";
        }

        private void btnPolilinha_Click(object sender, EventArgs e)
        {
            limparEsperas();

            polilinha = true;
            esperaInicioReta = true;

            stMensagem.Items[1].Text = "Clique no local do ponto inicial das retas:";
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
        }

        private void pbAreaDesenho_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (polilinha)
            {
                polilinha     = false;
                esperaFimReta = false;
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
        }

        private void AtualizarP2(int mouseX, int mouseY)
        {
            if (esperaInicioReta || esperaInicioCirculo || esperaInicioElipse || esperaInicioRetangulo ||
                esperaFimReta    || esperaFimCirculo    || esperaFimElipse    || esperaFimRetangulo)
            {
                p2.X = mouseX;
                p2.Y = mouseY;

                pbAreaDesenho.Invalidate();
            }
        }

        private void EsperaPonto()
        {
            Ponto novoPonto = new Ponto(p2.X, p2.Y, corAtual, espessura);
            figuras.InserirAposFim(new NoLista<Ponto>(novoPonto, null));
            novoPonto.desenhar(novoPonto.Cor, pbAreaDesenho.CreateGraphics());
            esperaPonto = false;
            stMensagem.Items[1].Text = "";
        }

        private void EsperaInicioReta()
        {
            esperaInicioReta = false;
            esperaFimReta    = true;

            p1.X = p2.X;
            p1.Y = p2.Y;
            p1.Cor = corAtual;

            stMensagem.Items[1].Text = "Mensagem: clique o ponto final da reta";
        }

        private void EsperaFimReta()
        {
            esperaInicioReta = false;

            if (polilinha)
            {
                p1.X = p2.X;
                p1.Y = p2.Y;
            }
            else
            {
                esperaFimReta = false;
            }

            Reta novaLinha = new Reta(p1.X, p1.Y, p2.X, p2.Y, corAtual, espessura);

            figuras.InserirAposFim(new NoLista<Ponto>(novaLinha, null));
            novaLinha.desenhar(novaLinha.Cor, pbAreaDesenho.CreateGraphics());
        }

        private void EsperaInicioCirculo()
        {
            esperaInicioCirculo = false;
            esperaFimCirculo    = true;

            p1.X = p2.X;
            p1.Y = p2.Y;
            p1.Cor = corAtual;
            
            stMensagem.Items[1].Text = "Mensagem: clique o ponto final do círculo";
        }
        private void EsperaFimCirculo()
        {
            esperaInicioCirculo = false;
            esperaFimCirculo    = false;

            int raio = Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));

            Circulo novoCirculo = new Circulo(p1.X, p1.Y, raio, p1.Cor, espessura);

            figuras.InserirAposFim(new NoLista<Ponto>(novoCirculo, null));

            novoCirculo.desenhar(novoCirculo.Cor, pbAreaDesenho.CreateGraphics());
        }

        private void EsperaInicioElipse()
        {
            esperaInicioElipse = false;
            esperaFimElipse    = true;

            p1.X = p2.X;
            p1.Y = p2.Y;
            p1.Cor = corAtual;
            
            stMensagem.Items[1].Text = "Mensagem: clique o ponto final da elipse";
        }

        private void EsperaFimElipse()
        {
            esperaInicioElipse = false;
            esperaFimElipse    = false;

            int raio1 = Math.Abs(p2.X - p1.X);
            int raio2 = Math.Abs(p2.Y - p1.Y);

            Elipse novaElipse = new Elipse(p1.X, p1.Y, raio1, raio2, p1.Cor, espessura);

            figuras.InserirAposFim(new NoLista<Ponto>(novaElipse, null));

            novaElipse.desenhar(novaElipse.Cor, pbAreaDesenho.CreateGraphics());
        }

        private void EsperaInicioRetangulo()
        {
            esperaInicioRetangulo = false;
            esperaFimRetangulo    = true;

            p1.X = p2.X;
            p1.Y = p2.Y;
            p1.Cor = corAtual;

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

            figuras.InserirAposFim(new NoLista<Ponto>(novoRetangulo, null));

            novoRetangulo.desenhar(novoRetangulo.Cor, pbAreaDesenho.CreateGraphics());
        }

        private void DesenharFiguras(Graphics g)
        {
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

                int raio = Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));

                g.DrawEllipse(pen, p1.X - raio, p1.Y - raio, // centro - raio
                              2 * raio, 2 * raio);
            }
            else if (esperaFimElipse)
            {
                Pen pen = new Pen(p1.Cor);

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
    }
}
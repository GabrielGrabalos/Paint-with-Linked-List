using System.Drawing;

namespace InfinityPaint
{
    public partial class frmGrafico : Form
    {
        bool esperaPonto = false;
        bool esperaInicioReta = false;
        bool esperaFimReta = false;
        bool esperaInicioCirculo = false;
        bool esperaFimCirculo = false;
        bool esperaInicioElipse = false;
        bool esperaFimElipse = false;
        private ListaSimples<Ponto> figuras = new ListaSimples<Ponto>();
        Color corAtual = Color.Black;
        private static Ponto p1 = new Ponto(0, 0, Color.Black);
        private static Ponto p2 = new Ponto(0, 0, Color.Black); // Ponto temporário.

        private void limparEsperas()
        {
            esperaPonto = false;
            esperaInicioReta = false;
            esperaFimReta = false;
            esperaInicioCirculo = false;
            esperaFimCirculo = false;
            esperaInicioElipse = false;
            esperaFimElipse = false;
        }

        public frmGrafico()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja realmente sair?", "Sair", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnCor_Click(object sender, EventArgs e)
        {
            if(cdSeletorDeCor.ShowDialog() == DialogResult.OK)
            {
                corAtual = cdSeletorDeCor.Color;
                displayDeCor.BackColor = corAtual;
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
                LerArquivo();
        }

        private void LerArquivo()
        {
            try
            {
                StreamReader arqFiguras = new StreamReader(dlgAbrir.FileName);

                String linha = arqFiguras.ReadLine();

                Double xInfEsq = Convert.ToDouble(linha.Substring(0, 5).Trim());
                Double yInfEsq = Convert.ToDouble(linha.Substring(5, 5).Trim());
                Double xSupDir = Convert.ToDouble(linha.Substring(10, 5).Trim());
                Double ySupDir = Convert.ToDouble(linha.Substring(15, 5).Trim());

                while ((linha = arqFiguras.ReadLine()) != null)
                {
                    String tipo = linha.Substring(0, 5).Trim();

                    int xBase = Convert.ToInt32(linha.Substring(5, 5).Trim());
                    int yBase = Convert.ToInt32(linha.Substring(10, 5).Trim());
                    int corR = Convert.ToInt32(linha.Substring(15, 5).Trim());
                    int corG = Convert.ToInt32(linha.Substring(20, 5).Trim());
                    int corB = Convert.ToInt32(linha.Substring(25, 5).Trim());

                    Color cor = new Color();
                    cor = Color.FromArgb(255, corR, corG, corB);

                    switch (tipo[0])
                    {
                        case 'p': // figura é um ponto
                            figuras.InserirAposFim(
                            new NoLista<Ponto>(new Ponto(xBase, yBase, cor), null));
                            break;

                        case 'l': // figura é uma reta
                            int xFinal = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            int yFinal = Convert.ToInt32(linha.Substring(35, 5).Trim());
                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Reta(xBase, yBase, xFinal, yFinal, cor), null));
                            break;

                        case 'c': // figura é um círculo
                            int raio = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Circulo(xBase, yBase, raio, cor), null));
                            break;
                    }
                }

                arqFiguras.Close();
                this.Text = dlgAbrir.FileName;
                pbAreaDesenho.Invalidate();

            }
            catch (IOException)
            {
                Console.WriteLine("Erro de leitura no arquivo");
            }
        }

        private void pbAreaDesenho_MouseMove(object sender, MouseEventArgs e)
        {
            stMensagem.Items[3].Text = "x: " + e.X + ", y: " + e.Y;

            if (esperaFimReta || esperaFimCirculo)
            {
                p2.X = e.X;
                p2.Y = e.Y;

                pbAreaDesenho.Invalidate();
            }
        }

        private void btnPonto_Click(object sender, EventArgs e)
        {
            stMensagem.Items[1].Text = "Clique no local do ponto desejado:";
            limparEsperas();
            esperaPonto = true;
        }

        private void pbAreaDesenho_Paint(object sender, PaintEventArgs e)
        {
            DesenharFiguras(e.Graphics); // acessa contexto gráfico e desenha
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
                Pen pen = new Pen(p1.Cor);
                g.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
            }
            else if (esperaFimCirculo)
            {
                Pen pen = new Pen(p1.Cor);

                int raio = Math.Abs(p1.X - p2.X);

                g.DrawEllipse(pen, p1.X - raio, p1.Y - raio, // centro - raio
                              2 * raio, 2 * raio);
            }
        }

        private void pbAreaDesenho_MouseClick(object sender, MouseEventArgs e)
        {
            if (esperaPonto)
            {
                Ponto novoPonto = new Ponto(e.X, e.Y, corAtual);
                figuras.InserirAposFim(new NoLista<Ponto>(novoPonto, null));
                novoPonto.desenhar(novoPonto.Cor, pbAreaDesenho.CreateGraphics());
                esperaPonto = false;
                stMensagem.Items[1].Text = "";
            }
            else if (esperaInicioReta)
            {
                p1.Cor = corAtual;
                p1.X = e.X;
                p1.Y = e.Y;
                esperaInicioReta = false;
                esperaFimReta = true;
                stMensagem.Items[1].Text = "Mensagem: clique o ponto final da reta";
            }
            else if (esperaFimReta)
            {
                esperaInicioReta = false;
                esperaFimReta = false;
                Reta novaLinha = new Reta(p1.X, p1.Y, e.X, e.Y, corAtual);
                figuras.InserirAposFim(new NoLista<Ponto>(novaLinha, null));
                novaLinha.desenhar(novaLinha.Cor, pbAreaDesenho.CreateGraphics());
            }
            else if (esperaInicioCirculo)
            {
                p1.Cor = corAtual;
                p1.X = e.X;
                p1.Y = e.Y;
                esperaInicioCirculo = false;
                esperaFimCirculo = true;
                stMensagem.Items[1].Text = "Mensagem: clique o ponto final do círculo";
            }
            else if (esperaFimCirculo)
            {
                esperaInicioCirculo = false;
                esperaFimCirculo = false;
                Circulo novoCirculo = new Circulo(p1.X, p1.Y, Math.Abs(e.X - p1.X), p1.Cor);
                figuras.InserirAposFim(new NoLista<Ponto>(novoCirculo, null));
                novoCirculo.desenhar(novoCirculo.Cor, pbAreaDesenho.CreateGraphics());
            }
        }

        private void btnReta_Click(object sender, EventArgs e)
        {
            stMensagem.Items[1].Text = "Clique no local do ponto inicial da reta:";
            limparEsperas();
            esperaInicioReta = true;
        }

        private void btnCirculo_Click(object sender, EventArgs e)
        {
            stMensagem.Items[1].Text = "Clique no local do ponto inicial do círculo:";
            limparEsperas();
            esperaInicioCirculo = true;
        }

        private void btnElipse_Click(object sender, EventArgs e)
        {

        }
    }
}
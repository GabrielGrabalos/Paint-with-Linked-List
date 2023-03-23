using System.Drawing;
using static System.Windows.Forms.LinkLabel;

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
        bool esperaInicioRetangulo = false;
        bool esperaFimRetangulo = false;
        bool polilinha = false;

        int espessura = 1;

        private ListaSimples<Ponto> figuras = new ListaSimples<Ponto>();
        Color corAtual = Color.Black;
        private static Ponto p1 = new Ponto(0, 0, Color.Black, 1);
        private static Ponto p2 = new Ponto(0, 0, Color.Black, 1); // Ponto temporário.

        private void limparEsperas()
        {
            esperaPonto = false;
            esperaInicioReta = false;
            esperaFimReta = false;
            esperaInicioCirculo = false;
            esperaFimCirculo = false;
            esperaInicioElipse = false;
            esperaFimElipse = false;
            esperaInicioRetangulo = false;
            esperaFimRetangulo = false;
            polilinha = false;
        }

        public frmGrafico()
        {
            InitializeComponent();
            pbAreaDesenho.KeyPress += new KeyPressEventHandler(pbAreaDesenho_KeyPress);
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

                /*Double xInfEsq = Convert.ToDouble(linha.Substring(0, 5).Trim());
                Double yInfEsq = Convert.ToDouble(linha.Substring(5, 5).Trim());
                Double xSupDir = Convert.ToDouble(linha.Substring(10, 5).Trim());
                Double ySupDir = Convert.ToDouble(linha.Substring(15, 5).Trim());*/

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
                    catch(Exception ex)
                    {
                        esp = 1;
                    }

                    Color cor = new Color();
                    cor = Color.FromArgb(255, corR, corG, corB);

                    switch (tipo)
                    {
                        case "p": // figura é um ponto
                            figuras.InserirAposFim(
                            new NoLista<Ponto>(new Ponto(xBase, yBase, cor, esp), null));
                            break;

                        case "l": // figura é uma reta
                            int xFinal = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            int yFinal = Convert.ToInt32(linha.Substring(35, 5).Trim());
                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Reta(xBase, yBase, xFinal, yFinal, cor, esp), null));
                            break;

                        case "c": // figura é um círculo
                            int raio = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Circulo(xBase, yBase, raio, cor, esp), null));
                            break;
                        case "e":
                            int raio1 = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            int raio2 = Convert.ToInt32(linha.Substring(35, 5).Trim());
                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Elipse(xBase, yBase, raio1, raio2, cor, esp), null));
                            break;
                        case "ret":
                            int largura = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            int altura = Convert.ToInt32(linha.Substring(35, 5).Trim());
                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Retangulo(xBase, yBase, largura, altura, cor, esp), null));
                            break;
                        case "r":
                            int x2 = Convert.ToInt32(linha.Substring(30, 5).Trim());
                            int y2 = Convert.ToInt32(linha.Substring(35, 5).Trim());
                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Reta(xBase, yBase, x2, y2, cor, esp), null));
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

            if (esperaFimReta || esperaFimCirculo || esperaFimElipse || esperaFimRetangulo)
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
                Pen pen = new Pen(p1.Cor, espessura);
                g.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
            }
            else if (esperaFimCirculo)
            {
                Pen pen = new Pen(p1.Cor, espessura);

                int raio = Math.Abs(p1.X - p2.X);

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

                

                //g.DrawRectangle(pen, p1.X, p1.Y, largura, altura);
                if(p1.X < p2.X && p1.Y < p2.Y)
                    g.DrawRectangle(pen, p1.X, p1.Y, largura, altura);
                else if (p1.X > p2.X && p1.Y < p2.Y)
                    g.DrawRectangle(pen, p2.X, p1.Y, largura, altura);
                else if (p1.X < p2.X && p1.Y > p2.Y)
                    g.DrawRectangle(pen, p1.X, p2.Y, largura, altura);
                else if (p1.X > p2.X && p1.Y > p2.Y)
                    g.DrawRectangle(pen, p2.X, p2.Y, largura, altura);
            }
        }

        private void pbAreaDesenho_MouseClick(object sender, MouseEventArgs e)
        {
            if (esperaPonto)
            {
                Ponto novoPonto = new Ponto(e.X, e.Y, corAtual, espessura);
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
                Reta novaLinha = new Reta(p1.X, p1.Y, e.X, e.Y, corAtual, espessura);

                figuras.InserirAposFim(new NoLista<Ponto>(novaLinha, null));
                novaLinha.desenhar(novaLinha.Cor, pbAreaDesenho.CreateGraphics());

                esperaInicioReta = false;

                if (!polilinha)
                    esperaFimReta = false;
                else
                {
                    p1.X = e.X;
                    p1.Y = e.Y;
                }
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
                Circulo novoCirculo = new Circulo(p1.X, p1.Y, Math.Abs(e.X - p1.X), p1.Cor, espessura);
                figuras.InserirAposFim(new NoLista<Ponto>(novoCirculo, null));
                novoCirculo.desenhar(novoCirculo.Cor, pbAreaDesenho.CreateGraphics());
            }
            else if (esperaInicioElipse)
            {
                p1.Cor = corAtual;
                p1.X = e.X;
                p1.Y = e.Y;
                esperaInicioElipse = false;
                esperaFimElipse = true;
                stMensagem.Items[1].Text = "Mensagem: clique o ponto final da elipse";
            }
            else if (esperaFimElipse)
            {
                esperaInicioElipse = false;
                esperaFimElipse = false;
                Elipse novaElipse = new Elipse(p1.X, p1.Y, Math.Abs(e.X - p1.X), Math.Abs(e.Y - p1.Y), p1.Cor, espessura);
                figuras.InserirAposFim(new NoLista<Ponto>(novaElipse, null));
                novaElipse.desenhar(novaElipse.Cor, pbAreaDesenho.CreateGraphics());
            }
            else if (esperaInicioRetangulo)
            {
                p1.Cor = corAtual;
                p1.X = e.X;
                p1.Y = e.Y;
                esperaInicioRetangulo = false;
                esperaFimRetangulo = true;
                stMensagem.Items[1].Text = "Mensagem: clique o ponto final do retângulo";
            }
            else if (esperaFimRetangulo)
            {
                esperaInicioRetangulo = false;
                esperaFimRetangulo = false;

                Retangulo novoRetangulo;

                if (p1.X < e.X && p1.Y < e.Y)
                    novoRetangulo = new Retangulo(p1.X, p1.Y, Math.Abs(e.X - p1.X), Math.Abs(e.Y - p1.Y), p1.Cor, espessura);
                else if (p1.X > e.X && p1.Y < e.Y)
                    novoRetangulo = new Retangulo(e.X, p1.Y, Math.Abs(e.X - p1.X), Math.Abs(e.Y - p1.Y), p1.Cor, espessura);
                else if (p1.X < e.X && p1.Y > e.Y)
                    novoRetangulo = new Retangulo(p1.X, e.Y, Math.Abs(e.X - p1.X), Math.Abs(e.Y - p1.Y), p1.Cor, espessura);
                else
                    novoRetangulo = new Retangulo(e.X, e.Y, Math.Abs(e.X - p1.X), Math.Abs(e.Y - p1.Y), p1.Cor, espessura);

                figuras.InserirAposFim(new NoLista<Ponto>(novoRetangulo, null));
                novoRetangulo.desenhar(novoRetangulo.Cor, pbAreaDesenho.CreateGraphics());
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
            stMensagem.Items[1].Text = "Clique no local do ponto inicial da elipse:";
            limparEsperas();
            esperaInicioElipse = true;
        }

        private void btnRetangulo_Click(object sender, EventArgs e)
        {
            stMensagem.Items[1].Text = "Clique no local do ponto inicial do retângulo:";
            limparEsperas();
            esperaInicioRetangulo = true;
        }

        private void btnPolilinha_Click(object sender, EventArgs e)
        {
            stMensagem.Items[1].Text = "Clique no local do ponto inicial das retas:";
            limparEsperas();
            polilinha = true;
            esperaInicioReta = true;
        }

        private void pbAreaDesenho_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(polilinha)
            {
                polilinha = false;
                esperaFimReta = false;
            }
        }

        private void pbAreaDesenho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (polilinha && (Keys)e.KeyChar == Keys.Enter)
            {
                polilinha = false;
            }
        }

        private void btnAumentarEspessura_Click(object sender, EventArgs e)
        {
            espessura++;

            if(espessura == 10)
                btnAumentarEspessura.Enabled = false;
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
                btnDiminuirEspessura.Enabled = false;
            else if(!btnAumentarEspessura.Enabled)
            {
                btnAumentarEspessura.Enabled = true;
            }

            tbEspessura.Text = "Espessura: " + espessura;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja salvar o desenho?", "Salvar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveFileDialog salvar = new SaveFileDialog();
                salvar.Filter = "Arquivo de desenho (*.txt)|*.txt";
                salvar.Title = "Salvar desenho";
                salvar.ShowDialog();

                if (salvar.FileName != "")
                {
                    FileStream arquivo = new FileStream(salvar.FileName, FileMode.Create);
                    StreamWriter escritor = new StreamWriter(arquivo);

                    int xInfEsq = this.Width;
                    int yInfEsq = this.Height;
                    int xSupDir = 0;
                    int ySupDir = 0;

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
        }
    }
}
using System.Drawing;
using System.Drawing.Text;
using static System.Windows.Forms.LinkLabel;

namespace InfinityPaint
{
    public partial class frmGrafico : Form
    {
        // Variáveis de controle de desenho:
        bool esperaPonto           = false;
        bool esperaInicioReta      = false;
        bool esperaFimReta         = false;
        bool esperaInicioCirculo   = false;
        bool esperaFimCirculo      = false;
        bool esperaInicioElipse    = false;
        bool esperaFimElipse       = false;
        bool esperaInicioRetangulo = false;
        bool esperaFimRetangulo    = false;

        bool salvo                 = false; // Verifica se a versão atual do arquivo foi salva.

        // Variáveis de controle da caixa de edição:
        bool arrastando       = false;                      // Verifica se a caixa está sendo arrastada.
        string editando =    "";                      // Verifica qual área da caixa está sendo editada.


        // Listas de figuras:
        private ListaSimples<Ponto> figuras = new ListaSimples<Ponto>();
        private ListaSimples<Ponto> figurasDesfeitas = new ListaSimples<Ponto>();

        Color corAtual = Color.Black; // Cor atual do traço.
        int espessura = 2; // Espessura atual do traço.

        // Pontos auxiliares:
        private static Ponto p1 = new Ponto(0, 0, Color.Black, 1);
        private static Ponto p2 = new Ponto(0, 0, Color.Black, 1); // Ponto temporário para pré-visualização da figura a ser desenhada.

        // Classes auxiliares:
        Polilinha umaPolilinha = null;     // Classe que armazena a polilinha que está sendo desenhada no momento.

        ToolStripButton btnAtivado = null; // Botão que está ativado no momento, permitindo manusear os botões de forma fácil.

        CaixaDeEdicao caixaDeEdicao;       // Caixa de edição dos desenhos.


        // =================================== ↓ Tratadores de eventos ↓ ==================================== //

        public frmGrafico()
        {
            InitializeComponent();
        }

        // ------------------- ↓ Eventos click (em ordem de aparição) ↓ ------------------- //

        // Abrir:
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
                LerArquivo();
        }

        // Salvar:
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarArquivo();
        }

        // Desfazer:
        private void btnDesfazer_Click(object sender, EventArgs e)
        {
            Desfazer();
        }

        // Refazer:
        private void btnRefazer_Click(object sender, EventArgs e)
        {
            Refazer();
        }

        // Criar ponto:
        private void btnPonto_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaPonto,
                          btnPonto, 
                          "Clique no local " +
                          "do ponto desejado:");
        }

        // Criar reta:
        private void btnReta_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaInicioReta, 
                          btnReta, 
                          "Clique no local do " +
                          "ponto inicial da reta:");
        }

        // Criar círculo:
        private void btnCirculo_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaInicioCirculo, 
                          btnCirculo, 
                          "Clique no local do " +
                          "ponto inicial do círculo:");
        }

        // Criar elipse:
        private void btnElipse_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaInicioElipse, 
                          btnElipse, 
                          "Clique no local do " +
                          "ponto inicial da elipse:");
        }

        // Criar retângulo:
        private void btnRetangulo_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaInicioRetangulo, 
                          btnRetangulo, 
                          "Clique no local do ponto " +
                          "inicial do retângulo:");
        }

        // Criar polilinha:
        private void btnPolilinha_Click(object sender, EventArgs e)
        {
            BtnConfig(ref esperaInicioReta, // umaPolilinha == null
                          btnPolilinha, 
                          "Clique no local do ponto inicial " +
                          "das retas (clique duplo para finalizar):");


            // Se a polilinha já estiver sendo desenhada, finaliza-a:
            if (umaPolilinha == null)
            {
                umaPolilinha = new Polilinha(p1.X, p1.Y, corAtual, espessura);
                figuras.InserirAposFim(umaPolilinha);
            }
            // Se não, inicia uma nova:
            else
            {
                umaPolilinha = null;
            }
        }

        // Selecionar cor:
        private void btnCor_Click(object sender, EventArgs e)
        {
            if (cdSeletorDeCor.ShowDialog() == DialogResult.OK) // Se o usuário selecionar uma cor:
            {
                corAtual = cdSeletorDeCor.Color;   // Atribui a cor selecionada à cor atual.
                displayDeCor.BackColor = corAtual; // Atribui a cor atual ao display de cor.
            }
        }

        // Aumentar espessura:
        private void btnAumentarEspessura_Click(object sender, EventArgs e)
        {
            espessura++; // Aumenta a espessura.

            if (espessura == 10) // Se a espessura for igual a 10, desativa o
                                 // botão de aumentar, para que não seja possível
                                 // aumentar mais.
            {
                btnAumentarEspessura.Enabled = false;
            }
            else if (!btnDiminuirEspessura.Enabled)      // Se o botão de diminuir estiver desativado, ativa-o.
            {
                btnDiminuirEspessura.Enabled = true;
            }

            tbEspessura.Text = "Espessura: " + espessura; // Atualiza o texto da barra de ferramentas.
        }

        // Diminuir espessura:
        private void btnDiminuirEspessura_Click(object sender, EventArgs e)
        {
            espessura--; // Diminui a espessura.

            if (espessura == 1) // Se a espessura for igual a 1, desativa o
                                // botão de diminuir, para que não seja possível
                                // diminuir mais.
            {
                btnDiminuirEspessura.Enabled = false;
            }
            else if (!btnAumentarEspessura.Enabled)       // Se o botão de aumentar estiver desativado, ativa-o.
            {
                btnAumentarEspessura.Enabled = true;
            }

            tbEspessura.Text = "Espessura: " + espessura; // Atualiza o texto da barra de ferramentas.
        }

        // Sair:
        private void btnSair_Click(object sender, EventArgs e)
        {
            if (!salvo && MessageBox.Show("Deseja salvar antes de sair?", "Sair",                // Se o arquivo não estiver salvo,
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SalvarArquivo(); // Salva o arquivo.
            }
            else
            {
                if (MessageBox.Show("Deseja realmente sair?", "Sair",                            // Se o usuário confirmar a saída,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) 
                {
                    Application.Exit(); // Encerra o programa.
                }
            }
        }

        // ------------------- ↓ Eventos mouse (pbAreaDesenhos) ↓ ------------------- //

        // Mouse moveu:
        private void pbAreaDesenho_MouseMove(object sender, MouseEventArgs e) // Método utilizado para caixa de edição e para
                                                                              // pré-visualização da figura a ser desenhada.
        {
            if (caixaDeEdicao != null) // Se existir uma caixa de edição ainda não confirmada,
            {
                string estadoSeta;     // Verifica se o mouse está sobre algum canto.


                // Se o usuário já estiver fazendo uma modificação coma caixa
                // de edição, atribui o estado da seta baseado nessa edição:
                if(editando != "")
                {
                    estadoSeta = editando;
                }
                // Senão, faz a verificação:
                else
                {
                    estadoSeta = caixaDeEdicao.IsMousePorCima(e.X, e.Y);
                }

                // Por fim, se o mouse estiver por cima da caixa de edição, altera o cursor
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
                // Reseta o cursor:
                else if (this.Cursor != Cursors.Default && editando == "")
                {
                    this.Cursor = Cursors.Default;
                }

                // Se a pessoa estiver arrastando, faz as devidas alterações:
                if (arrastando)
                {
                    caixaDeEdicao.Mover(editando, e.X, e.Y);
                    pbAreaDesenho.Invalidate();
                }
            }

            AtualizarP2(e.X, e.Y); // Atualiza o ponto auxiliar p2.

            stMensagem.Items[3].Text = "x: " + e.X + ", y: " + e.Y; // Atualiza a mensagem.
        }

        // Mouse clicou:
        private void pbAreaDesenho_MouseClick(object sender, MouseEventArgs e)
        {
            salvo = false; // Assume que uma alteração foi feita, tornando o estado atual do arquivo com não salvo.

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
            else if (caixaDeEdicao != null) // Se houver uma caixa de edição não confirmada, confirma:
            {
                if (editando == "")         // Se já não estiver mais em estado de edição:
                {
                    ConfirmarCaixaDeEdicao();
                }
            }
            else
            {
                salvo = true; // Caso nenhuma das anteriores seja verdadeira, o documento não
                              // foi alterado, retornando o estado atual do desenho para salvo.
            }
        }

        
        // Mouse deu duplo clique:
        private void pbAreaDesenho_MouseDoubleClick(object sender, MouseEventArgs e) // Método utilizado para a confirmação da polilinha:
        {
            if (umaPolilinha != null) // Caso haja uma polilinha em processo de desenho,
            {
                umaPolilinha.adicionarPonto(new Ponto(e.X, e.Y, corAtual, espessura)); // Adiciona o último ponto,
                umaPolilinha  = null;                                                  // Reseta o ponteiro,
                esperaFimReta = false;                                                 // Para a pré-visualização e
                btnPolilinha.BackColor = SystemColors.Control;                         // Mostra o botão como desabilitado.

                if (!btnDesfazer.Enabled)       // Caso o botão de desfazer esteja desabilitado,
                                                // o habilita, para possível remoção da nova figura.
                    btnDesfazer.Enabled = true;
            }
        }

        private void pbAreaDesenho_MouseDown(object sender, MouseEventArgs e)
        {
            // Se houver uma caixa de edição:
            if (caixaDeEdicao != null)
            {
                arrastando = true;                                  // Avisa que o usuário está arrastando,
                editando = caixaDeEdicao.IsMousePorCima(e.X, e.Y);  // e atualiza qual edição está sendo feita.
            }
        }

        private void pbAreaDesenho_MouseUp(object sender, MouseEventArgs e)
        {
            // Reseta variáveis de controle da caixa de edição:
            arrastando = false;
            editando = "";
        }

        // ------------------- ↓ Evento Paint (pbAreaDesenhos) ↓ ------------------- //
        private void pbAreaDesenho_Paint(object sender, PaintEventArgs e)
        {
            DesenharFiguras(e.Graphics); // acessa contexto gráfico e desenha
        }



        // =================================== ↓ Funções ↓ ==================================== //

        // Reseta esperas e variáveis de controle similares:
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

            // Reseta variáveis da caixa de edição:
            arrastando = false;
            editando   =    "";

            umaPolilinha = null; // Reseta o ponteiro polilinha.
        }

        // Atualiza o ponto auxiliar p2, que auxilia na pré-visualização da figura a ser desenhada:
        private void AtualizarP2(int mouseX, int mouseY)
        {
            // Caso haja alguma espera:
            if (esperaInicioReta || esperaInicioCirculo || esperaInicioElipse || esperaInicioRetangulo ||
                esperaFimReta    || esperaFimCirculo    || esperaFimElipse    || esperaFimRetangulo    || 
                esperaPonto )
            {
                // Atualiza o ponto auxiliar p2:
                p2.X = mouseX;
                p2.Y = mouseY;

                pbAreaDesenho.Invalidate(); // Atualiza a tela.
            }
        }

        // Configura os botões de desenho:
        private void BtnConfig(ref bool boolEspera, ToolStripButton btnAtual, string mensagem) // Método utilizado para fazer verificações e configurar
                                                                                               // de forma que permita fácil manuseio os botões de desenho.
        {
            // Contexto:
                // boolEspera: Representa a variável de controle referente a figura a ser desenhada.
                // btnAtual:   Representa o botão a ser modificado.
                // mensagem:   Representa a mensagem que será mostrada ao usuário.

            // Se o programa não estiver esperando pela figura:
            if (!boolEspera)
            {
                limparEsperas();   // Limpa todas as outras esperas
                boolEspera = true; // e mantém a espera especificada.


                btnAtual.BackColor = SystemColors.ButtonShadow; // Exibe o botão como selecionado.

                stMensagem.Items[1].Text = "Sem mensagem";      // Reseta a mensagem.


                // Exibe o antigo botão selecionado com não selecionado:
                if (btnAtivado != null)
                    btnAtivado.BackColor = SystemColors.Control;


                btnAtivado = btnAtual; // Atualiza o novo botão selecionado.
            }
            // Se o programa estiver esperando, significa
            // que o usuário está desselecionando o botão:
            else
            {
                boolEspera = false; // Deixa a espera como falso.

                btnAtual.BackColor = SystemColors.Control; // Exibe o botão como não selecionado.

                stMensagem.Items[1].Text = mensagem;       // Reseta a mensagem
                btnAtivado = null;
            }

            // Caso haja uma mudança de botão enquanto
            // há uma caixa de edição, confirma a caixa:
            if (caixaDeEdicao != null)
            {
                ConfirmarCaixaDeEdicao();
            }
        }

        // ------------------- ↓ Esperas das figuras ↓ ------------------- //

        private void EsperaPonto()
        {
            esperaPonto = false; // Reseta a espera do ponto.

            Ponto novoPonto = new Ponto(p2.X, p2.Y, corAtual, espessura); // Instancia um novo ponto, o qual
                                                                          // será adicionado na lista de figuras.

            figuras.InserirAposFim(new NoLista<Ponto>(novoPonto, null));        // Insere o ponto na lista,
            novoPonto.desenhar(novoPonto.Cor, pbAreaDesenho.CreateGraphics());  // e o desenha na tela.

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas(); // Se há coisas que foram desefeitas na memória, as apaga,
                                                                        // visto que não serão mais utilizadas pelo usuário

            btnAtivado.BackColor = SystemColors.Control;                // Mostra o botão ativado como desselecionado.

            if (!btnDesfazer.Enabled) btnDesfazer.Enabled = true;       // Permite a remoção da figura, caso desejada.

            stMensagem.Items[1].Text = "Sem mensagem"; // Reseta a mensagem.
        }

        private void EsperaInicioReta()
        {
            esperaInicioReta = false; // Reseta a espera inicial da reta,
            esperaFimReta    = true;  // e inicia a espera final da mesma.

            // Atualiza o ponto auxiliar 1:
            p1.X = p2.X;
            p1.Y = p2.Y;

            // Caso uma polilinha esteja sendo feita:
            if(umaPolilinha != null)
            {
                umaPolilinha.adicionarPonto(new Ponto(p1.X, p1.Y, corAtual, espessura)); // Adiciona o ponto.
            }

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas(); // Se há coisas que foram desefeitas na memória, as apaga,
                                                                        // visto que não serão mais utilizadas pelo usuário

            stMensagem.Items[1].Text = "Mensagem: clique o ponto final da reta"; // Atualiza a mensagem.
        }

        private void EsperaFimReta()
        {
            // Caso uma polilinha esteja sendo feita:
            if (umaPolilinha != null)
            {
                // Se for o primeiro ponto da polilinha:
                if (!umaPolilinha.Pontos.EstaVazia)
                {
                    p1.X = p2.X;
                    p1.Y = p2.Y;
                }

                umaPolilinha.adicionarPonto(new Ponto(p1.X, p1.Y, corAtual, espessura)); // Adiciona o ponto.
            }
            // Se uma reta está sendo feita:
            else
            {
                esperaFimReta = false; // Reseta a espera final da reta.

                Reta novaLinha = new Reta(p1.X, p1.Y, p2.X, p2.Y, corAtual, espessura); // Instancia uma nova reta, a qual
                                                                                        // será adicionada a lista de figuras.

                caixaDeEdicao = new CaixaDeEdicao(Math.Min(novaLinha.X,  novaLinha.PontoFinal.X), // Faz uma nova caixa de edição,
                                                  Math.Min(novaLinha.Y,  novaLinha.PontoFinal.Y), // tomando em base a reta criada acima,
                                                  Math.Abs(novaLinha.X - novaLinha.PontoFinal.X), // permitindo que o usuário modifique
                                                  Math.Abs(novaLinha.Y - novaLinha.PontoFinal.Y), // a figura, dando-lhe mais mobilidade.
                                                  novaLinha);

                caixaDeEdicao.desenhar(corAtual, pbAreaDesenho.CreateGraphics()); // Desenha a caixa de edição.

                btnAtivado.BackColor = SystemColors.Control;                      // Mostra o botão ativado com desselecionado.

                if (!btnDesfazer.Enabled) btnDesfazer.Enabled = true;             // Permite a remoção da figura, caso desejada.
            }
        }

        private void EsperaInicioCirculo()
        {
            esperaInicioCirculo = false; // Reseta a espera inicial do círculo,
            esperaFimCirculo    = true;  // e inicia a espera final do mesmo.

            // Atualiza o ponto auxiliar 1:
            p1.X = p2.X;
            p1.Y = p2.Y;

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas(); // Se há coisas que foram desefeitas na memória, as apaga,
                                                                        // visto que não serão mais utilizadas pelo usuário

            stMensagem.Items[1].Text = "Mensagem: clique o ponto final do círculo"; // Atualiza a mensagem.
        }

        private void EsperaFimCirculo()
        {
            esperaFimCirculo = false; // Reseta a espera final do círculo.

            // Calcula a distância entre o centro do círculo e o cursor:
            int raio = (int)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));

            Circulo novoCirculo = new Circulo(p1.X, p1.Y, raio, corAtual, espessura); // Instancia um novo c´rculo, o qual
                                                                                      // será adicionado a lista de figuras.

            // Cria uma nova caixa de edição com o círculo criado acima:
            caixaDeEdicao = new CaixaDeEdicao(novoCirculo.X - novoCirculo.Raio, 
                                              novoCirculo.Y - novoCirculo.Raio, 
                                              novoCirculo.Raio * 2, 
                                              novoCirculo.Raio * 2, 
                                              novoCirculo);

            caixaDeEdicao.desenhar(corAtual, pbAreaDesenho.CreateGraphics()); // Desenha a caixa de edição.
        }

        private void EsperaInicioElipse()
        {
            esperaInicioElipse = false; // Reseta a espera inicial da elipse,
            esperaFimElipse    =  true; // e inicia a espera final da mesma.

            // Atualiza o ponto auxiliar p1:
            p1.X = p2.X;
            p1.Y = p2.Y;

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas(); // Se há coisas que foram desefeitas na memória, as apaga,
                                                                        // visto que não serão mais utilizadas pelo usuário

            stMensagem.Items[1].Text = "Mensagem: clique o ponto final da elipse"; // Atualiza a mensagem.
        }

        private void EsperaFimElipse()
        {
            esperaFimElipse = false; // Reseta a espera final da elipse.

            // Calcula os raios:
            int raio1 = Math.Abs(p2.X - p1.X);
            int raio2 = Math.Abs(p2.Y - p1.Y);

            Elipse novaElipse = new Elipse(p1.X, p1.Y, raio1, raio2, corAtual, espessura); // Instancia uma nova elipse, a qual
                                                                                           // será adicionada a lista de figuras.

            // Cria uma nova caixa de edição com a elipse criada acima:
            caixaDeEdicao = new CaixaDeEdicao(novaElipse.X - novaElipse.Raio1, 
                                              novaElipse.Y - novaElipse.Raio2, 
                                              novaElipse.Raio1 * 2, 
                                              novaElipse.Raio2 * 2, 
                                              novaElipse);

            caixaDeEdicao.desenhar(corAtual, pbAreaDesenho.CreateGraphics()); // Desenha a caixa de edição.
        }

        private void EsperaInicioRetangulo()
        {
            esperaInicioRetangulo = false; // Reseta a espera inicial do retângulo,
            esperaFimRetangulo    =  true; // e inicia a espera final do mesmo.

            // Atualiza o ponto auxiliar p1:
            p1.X = p2.X;
            p1.Y = p2.Y;

            if (!figurasDesfeitas.EstaVazia) ResetarFigurasDesfeitas(); // Se há coisas que foram desefeitas na memória, as apaga,
                                                                        // visto que não serão mais utilizadas pelo usuário

            stMensagem.Items[1].Text = "Mensagem: clique o ponto final do retângulo"; // Atualiza a mensagem.
        }

        private void EsperaFimRetangulo()
        {
            esperaFimRetangulo = false; // Reseta a espera final do retângulo.

            // Calcula a largura e altura:
            int largura = Math.Abs(p2.X - p1.X);
            int altura  = Math.Abs(p2.Y - p1.Y);

            // Corrige o retângulo, se necessário:
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

            Retangulo novoRetangulo = new Retangulo(x1, y1, largura, altura, corAtual, espessura); // Instancia um novo retângulo, o qual
                                                                                                   // será adicionado a lista de figuras.

            // Cria uma nova caixa de edição com o retângulo criado acima:
            caixaDeEdicao = new CaixaDeEdicao(novoRetangulo.X, 
                                              novoRetangulo.Y, 
                                              novoRetangulo.Largura, 
                                              novoRetangulo.Altura, 
                                              novoRetangulo);

            caixaDeEdicao.desenhar(corAtual, pbAreaDesenho.CreateGraphics()); // Desenha a caixa de edição.
        }

        // --------------------------------------------------------------- //

        // Desfaz a última figura desenhada pelo usuário:
        private void Desfazer()
        {
            if (!figuras.EstaVazia)
            {
                figurasDesfeitas.InserirAposFim(figuras.RemoverUltimo()); // Remove a figura da lista figuras
                                                                          // e adiciona nas figuras desfeitas.

                pbAreaDesenho.Invalidate(); // Atualiza a tela.

                // Atualiza o estado dos botões:
                if (figuras.EstaVazia)  btnDesfazer.Enabled = false;
                if (!btnRefazer.Enabled) btnRefazer.Enabled =  true;

                limparEsperas();

                // Mostra o botão ativado como desselecionado:
                if (btnAtivado != null)
                    btnAtivado.BackColor = SystemColors.Control;
            }
        }

        // Refaz a última figura desfeita pelo usuário:
        private void Refazer()
        {
            figuras.InserirAposFim(figurasDesfeitas.RemoverUltimo()); // Remove a figura da lista figuras desfeitas
                                                                      // e adiciona na lista de figuras.

            pbAreaDesenho.Invalidate(); // Atualiza a tela.

            // Atualiza o estado dos botões:
            if (figurasDesfeitas.EstaVazia) btnRefazer.Enabled = false;
            if (!btnDesfazer.Enabled)       btnDesfazer.Enabled = true;

            limparEsperas();

            // Mostra o botão ativado como desselecionado:
            if (btnAtivado != null)
                btnAtivado.BackColor = SystemColors.Control;
        }

        // Limpa a lista de figuras desfeitas:
        private void ResetarFigurasDesfeitas()
        {
            figurasDesfeitas.Limpar();
            btnRefazer.Enabled = false;
        }

        private void ConfirmarCaixaDeEdicao() // Método utilizado para adicionar à lista
                                              // de figuras a figura atualmente em edição.
        {
            figuras.InserirAposFim(new NoLista<Ponto>(caixaDeEdicao.FiguraInterna, null)); // Adiciona a figura na lista de figuras.

            caixaDeEdicao.FiguraInterna.desenhar(caixaDeEdicao.FiguraInterna.Cor, // Desenha a figura.
                                                 pbAreaDesenho.CreateGraphics());

            btnAtivado.BackColor = SystemColors.Control; // Mostra o botão ativado como desselecionado.

            btnAtivado = null;                           // Reseta o botão ativado.

            if (!btnDesfazer.Enabled) btnDesfazer.Enabled = true; // Permite a remoção da figura, caso desejada.

            caixaDeEdicao = null; // Reseta o ponteiro da caixa de edição.

            // Reseta o cursor:
            if (Cursor != Cursors.Default)
            {
                Cursor = Cursors.Default;
            }

            pbAreaDesenho.Invalidate(); // Atualiza a tela.
        }

        private void DesenharFiguras(Graphics g)
        {
            // Percorre a lista de figuras e desenha todas as figuras ali presentes:
            figuras.iniciarPercursoSequencial();

            while (figuras.podePercorrer())
            {
                Ponto figuraAtual = figuras.Atual.Info;
                figuraAtual.desenhar(figuraAtual.Cor, g);
            }

            // Desenha a caixa de edição, caso haja uma:
            caixaDeEdicao?.desenhar(caixaDeEdicao.Cor, g);

            // Desenha a pré visualizaçãodas figuras a serem desenhadas:

            Pen pen = new Pen(corAtual, espessura);

            if (esperaFimReta)
            {
                g.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
            }

            else if (esperaFimCirculo)
            {
                // Calcula a distância entre o centro do círculo e o mouse:
                int raio = (int)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));

                g.DrawEllipse(pen, p1.X - raio, p1.Y - raio, 2 * raio, 2 * raio);
            }

            else if (esperaFimElipse)
            {
                // Calcula os raios da elipse:
                int raio1 = Math.Abs(p1.X - p2.X);
                int raio2 = Math.Abs(p1.Y - p2.Y);

                g.DrawEllipse(pen, p1.X - raio1, p1.Y - raio2, 2 * raio1, 2 * raio2);
            }

            else if (esperaFimRetangulo)
            {
                // Calcula a altura e largura:
                int largura = Math.Abs(p2.X - p1.X);
                int altura  = Math.Abs(p2.Y - p1.Y);

                // Corrige o retânlo, caso necessário:
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

        private void LerArquivo()
        {
            // Caso o esboço atual não esteja salvo, pergunta se o usuário deseja continuar o desenho atual:
            if (!salvo && !MessageBox.Show(
                    "O desenho atual não foi salvo. Deseja continuar este desenho?", "Abrir desenho",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                return;

            try
            {
                figuras.Limpar(); // Limpa a lista de figuras.

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

                    // Tenta ler a espessura da figura. Caso não seja possível, assume 2.
                    int esp;

                    try
                    {
                        esp = Convert.ToInt32(linha.Substring(40, 5).Trim());
                    }
                    catch (Exception)
                    {
                        esp = 2;
                    }

                    // Cria a cor da figura:
                    Color cor = new Color();
                    cor = Color.FromArgb(255, corR, corG, corB);

                    // Verifica o tipo da figura:
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
                            int altura = Convert.ToInt32(linha.Substring(35, 5).Trim());

                            figuras.InserirAposFim(new NoLista<Ponto>(
                            new Retangulo(xBase, yBase, largura, altura, cor, esp), null));

                            break;

                        // Polilinha:
                        case 'i':
                            int numPontos = Convert.ToInt32(linha.Substring(30, 5).Trim());

                            Polilinha poli = new Polilinha(0, 0, cor, esp);
                            poli.adicionarPonto(new Ponto(xBase, yBase, cor, esp));

                            for (int i = 0; i < numPontos; i++)
                            {
                                if ((linha = arqFiguras.ReadLine()) == null) break;

                                int x = Convert.ToInt32(linha.Substring(5, 5).Trim());
                                int y = Convert.ToInt32(linha.Substring(10, 5).Trim());

                                int r = Convert.ToInt32(linha.Substring(15, 5).Trim());
                                int g = Convert.ToInt32(linha.Substring(20, 5).Trim());
                                int b = Convert.ToInt32(linha.Substring(25, 5).Trim());

                                esp = Convert.ToInt32(linha.Substring(40, 5).Trim());

                                cor = Color.FromArgb(255, r, g, b);

                                poli.adicionarPonto(new Ponto(x, y, cor, esp));
                            }

                            figuras.InserirAposFim(new NoLista<Ponto>(poli, null));

                            break;
                    }
                }

                salvo = true;

                arqFiguras.Close();
                this.Text = dlgAbrir.FileName;
                pbAreaDesenho.Invalidate();
            }
            catch (IOException)
            {
                MessageBox.Show("Erro de leitura no arquivo", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                salvo = true; // Deixa o estado atual do desenho como salvo.

                MessageBox.Show("Desenho salvo com sucesso!", "Salvar desenho",   // Avisa que o desenho foi salvo com sucesso.
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
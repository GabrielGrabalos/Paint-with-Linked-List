class Polilinha : Ponto
{
    ListaSimples<Ponto> pontos;

    Ponto supDir, supEsq, infEsq;

    int NumPontos { get => pontos.QuantosNos(); }

    public ListaSimples<Ponto> Pontos { get => pontos; }

    public Polilinha(int x, int y, Color cor, int espessura) : base(x, y, cor, espessura)
    {
        pontos = new ListaSimples<Ponto>();
        //adicionarPonto(new Ponto(x, y, cor, espessura));
    }

    public void adicionarPonto(Ponto ponto)
    {
        pontos.InserirAposFim(ponto);

        AtualizarPontosDeControle(ponto);
    }

    private void AtualizarPontosDeControle(Ponto ponto)
    {
        // Atualiza os pontos de controle
        if (pontos.QuantosNos() == 1)
        {
            supDir = new Ponto(ponto.X, ponto.Y, ponto.Cor, ponto.Espessura);
            supEsq = new Ponto(ponto.X, ponto.Y, ponto.Cor, ponto.Espessura);
            infEsq = new Ponto(ponto.X, ponto.Y, ponto.Cor, ponto.Espessura);
        }
        else
        {
            if (ponto.X > supDir.X)
                supDir.X = ponto.X;
            if (ponto.Y < supDir.Y)
                supDir.Y = ponto.Y;

            if (ponto.X < supEsq.X)
                supEsq.X = ponto.X;
            if (ponto.Y < supEsq.Y)
                supEsq.Y = ponto.Y;

            if (ponto.X < infEsq.X)
                infEsq.X = ponto.X;
            if (ponto.Y > infEsq.Y)
                infEsq.Y = ponto.Y;
        }
    }

    public override void desenhar(Color cor, Graphics g)
    {
        Pen pen = new Pen(cor, base.Espessura);

        var lista = pontos.Lista();

        // Desenha as linhas:
        for (int i = 1; i < lista.Count; i++)
        {
            pen.Width = lista[i].Espessura;
            pen.Color = lista[i].Cor;
            g.DrawLine(pen, lista[i - 1].X, lista[i - 1].Y, lista[i].X, lista[i].Y);
        }
    }

    public override string ToString()
    {
        if (pontos.EstaVazia)
            throw new Exception("Lista vazia!");

        var lista = pontos.Lista();

        string ret = "";

        for (int i = 0; i < lista.Count; i++)
        {
            var pontoAtual = lista[i];
            if(i == 0)
            {
                ret += transformaString("i", 5) +
                       transformaString(pontoAtual.X, 5) +
                       transformaString(pontoAtual.Y, 5) +
                       transformaString(pontoAtual.Cor.R, 5) +
                       transformaString(pontoAtual.Cor.G, 5) +
                       transformaString(pontoAtual.Cor.B, 5) +
                       transformaString(NumPontos, 5) +
                       "     " +
                       transformaString(pontoAtual.Espessura, 5) + "\n";
            }
            else
            {
                ret += transformaString("i", 5) +
                       transformaString(pontoAtual.X, 5) +
                       transformaString(pontoAtual.Y, 5) +
                       transformaString(pontoAtual.Cor.R, 5) +
                       transformaString(pontoAtual.Cor.G, 5) +
                       transformaString(pontoAtual.Cor.B, 5) +
                       "          " +
                       transformaString(pontoAtual.Espessura, 5);

                if (i != lista.Count - 1)
                    ret += "\n";
            }

            /*if (i == 0)
                ret += transformaString("i", 5) +
                       transformaString(X, 5) +
                       transformaString(Y, 5) +
                       transformaString(Cor.R, 5) +
                       transformaString(Cor.G, 5) +
                       transformaString(Cor.B, 5) +
                       transformaString(NumPontos, 5) +
                       "     " +
                       transformaString(Espessura, 5) + "\n";

            else
                ret += transformaString("i", 5) +
                   transformaString(X, 5) +
                   transformaString(Y, 5) +
                   transformaString(Cor.R, 5) +
                   transformaString(Cor.G, 5) +
                   transformaString(Cor.B, 5) +
                   transformaString(NumPontos, 5) +
                   "          " +
                   transformaString(Espessura, 5) + "\n";*/
        }

        return ret;
    }
}
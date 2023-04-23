class Polilinha : Ponto
{
    ListaSimples<Ponto> pontos; // Armazena os pontos da polilinha em sequência.

    int NumPontos { get => pontos.QuantosNos(); }

    public ListaSimples<Ponto> Pontos { get => pontos; }

    public Polilinha(int x, int y, Color cor, int espessura) : base(x, y, cor, espessura)
    {
        pontos = new ListaSimples<Ponto>();
    }

    public void adicionarPonto(Ponto ponto)
    {
        pontos.InserirAposFim(ponto);
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

            // Caso seja o primeiro ponto da lista, o salva junto à
            // quantidade de pontos que há na lista para futuras leituras:
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

                if (i != lista.Count - 1) // Impede que uma linha vazia seja escrita.
                    ret += "\n";
            }
        }

        return ret;
    }
}
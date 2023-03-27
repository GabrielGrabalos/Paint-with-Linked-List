using System;
using System.Drawing;

class Reta : Ponto
{
    private Ponto pontoFinal;

    public Ponto PontoFinal
    {
        get { return pontoFinal; } 
        set { pontoFinal = value; }
    }
    
    public Reta(int x1, int y1, int x2, int y2, Color novaCor, int espessura) : base(x1, y1, novaCor, espessura)
    {
        pontoFinal = new Ponto(x2, y2, novaCor, espessura);
    }

    public override void desenhar(Color corDesenho, Graphics g)
    {
        Pen pen = new Pen(corDesenho, base.Espessura);
        pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
        g.DrawLine(pen, base.X, base.Y, // ponto inicial
        pontoFinal.X, pontoFinal.Y);
    }

    public String transformaString(int valor, int quantasPosicoes)
    {
        String cadeia = valor + "";
        while (cadeia.Length < quantasPosicoes)
            cadeia = "0" + cadeia;
        return cadeia.Substring(0, quantasPosicoes); // corta, se necessário, para
                                                     // tamanho máximo
    }
    public String transformaString(String valor, int quantasPosicoes)
    {
        String cadeia = valor + "";
        while (cadeia.Length < quantasPosicoes)
            cadeia = cadeia + " ";
        return cadeia.Substring(0, quantasPosicoes); // corta, se necessário, para
                                                     // tamanho máximo
    }
    public override String ToString()
    {
        return transformaString("r", 5) +
        transformaString(X, 5) +
        transformaString(Y, 5) +
        transformaString(Cor.R, 5) +
        transformaString(Cor.G, 5) +
        transformaString(Cor.B, 5) +
        transformaString(pontoFinal.X, 5) +
        transformaString(pontoFinal.Y, 5) +
        transformaString(Espessura, 5);
    }
}
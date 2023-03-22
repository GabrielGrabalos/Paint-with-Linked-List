using System;
using System.Drawing;

class Ponto : IComparable<Ponto>
{
    private int x, y;
    private Color cor;

    public Ponto(int x, int y, Color cor)
    {
        this.X = x;
        this.Y = y;
        this.Cor = cor;
    }

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public Color Cor { get => cor; set => cor = value; }

    public virtual void desenhar(Color cor, Graphics g)
    {
        Pen pen = new Pen(cor);
        g.DrawLine(pen, x, y, x, y);
    }

    public int CompareTo(Ponto other)
    {
        int diferencaX = X - other.X;
        if (diferencaX == 0)
            return Y - other.Y;
        return diferencaX;
    }

    public String transformaString(int valor, int quantasPosicoes)
    {
        String cadeia = valor + "";
        //while (cadeia.Length < quantasPosicoes) cadeia = "0" + cadeia;
        cadeia = cadeia.PadLeft(quantasPosicoes, '0');

        return cadeia[..quantasPosicoes]; // corta, se necessário, para
                                                     // tamanho máximo
    }
    public String transformaString(String valor, int quantasPosicoes)
    {
        String cadeia = valor + "";
        //while (cadeia.Length < quantasPosicoes) cadeia += " ";
        cadeia = cadeia.PadRight(quantasPosicoes, ' ');

        return cadeia[..quantasPosicoes]; // corta, se necessário, para
                                                     // tamanho máximo
    }
    public override String ToString()
    {
        return transformaString("p", 5)   +
               transformaString(X, 5)     +
               transformaString(Y, 5)     +
               transformaString(Cor.R, 5) +
               transformaString(Cor.G, 5) +
               transformaString(Cor.B, 5);
    }
}
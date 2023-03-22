using System;
using System.Drawing;

class Reta : Ponto
{
    private Ponto pontoFinal;

    public Reta(int x1, int y1, int x2, int y2, Color novaCor) : base(x1, y1, novaCor)
    {
        pontoFinal = new Ponto(x2, y2, novaCor);
    }

    public override void desenhar(Color corDesenho, Graphics g)
    {
        Pen pen = new Pen(corDesenho);
        g.DrawLine(pen, base.X, base.Y, // ponto inicial
        pontoFinal.X, pontoFinal.Y);
    }
}
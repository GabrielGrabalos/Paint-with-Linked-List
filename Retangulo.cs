using System;

class Retangulo : Ponto
{
    private int largura;
    private int altura;

    public Retangulo(int x, int y, int largura, int altura, Color novaCor, int espessura) : base(x, y, novaCor, espessura)
    {
        this.largura = largura;
        this.altura = altura;
    }

    public int Largura
    {
        get { return largura; }
        set { largura = value; }
    }

    public int Altura
    {
        get { return altura; }
        set { altura = value; }
    }

    public override void desenhar(Color corDesenho, Graphics g)
    {
        Pen pen = new Pen(corDesenho, base.Espessura);
        // pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
        g.DrawRectangle(pen, base.X, base.Y, largura, altura);
    }

    public override String ToString()
    {
        return transformaString("ret", 5) +
        transformaString(X, 5) +
        transformaString(Y, 5) +
        transformaString(Cor.R, 5) +
        transformaString(Cor.G, 5) +
        transformaString(Cor.B, 5) +
        transformaString(Largura, 5) +
        transformaString(Altura, 5) +
        transformaString(Espessura, 5);
    }
}
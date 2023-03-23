using System;

class Retangulo : Ponto
{
    private int largura;
    private int altura;

    public Retangulo(int x, int y, int largura, int altura, Color novaCor) : base(x, y, novaCor)
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
        Pen pen = new Pen(corDesenho);
        g.DrawRectangle(pen, base.X, base.Y, largura, altura);
    }
}
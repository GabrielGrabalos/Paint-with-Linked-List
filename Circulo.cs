using System.Drawing;

class Circulo : Ponto
{
    // herda o ponto central (x, y) da classe Ponto
    int raio;

    public int Raio
    {
        get { return raio; }
        set { raio = value; }
    }

    public Circulo(int xCentro, int yCentro, int novoRaio, Color novaCor, int espessura) : base(xCentro, yCentro, novaCor, espessura) // construtor de Ponto(x,y)
    {
        raio = novoRaio;
    }

    public void setRaio(int novoRaio)
    {
        raio = novoRaio;
    }

    public override void desenhar(Color corDesenho, Graphics g)
    {
        Pen pen = new Pen(corDesenho, base.Espessura);
        g.DrawEllipse(pen, base.X - raio, base.Y - raio, // centro - raio
        2 * raio, 2 * raio); // centro + raio
    }

    public override String ToString()
    {
        return transformaString("c", 5) +
        transformaString(X, 5) +
        transformaString(Y, 5) +
        transformaString(Cor.R, 5) +
        transformaString(Cor.G, 5) +
        transformaString(Cor.B, 5) +
        transformaString(Raio, 5) +
        "     " +
        transformaString(Espessura, 5);
    }
}
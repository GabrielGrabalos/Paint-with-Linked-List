using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Elipse : Ponto
{
    // herda o ponto central (x, y) da classe Ponto
    int raio1, raio2;

    public int Raio1
    {
        get { return raio1; }
        set { raio1 = value; }
    }
    public int Raio2
    {
        get { return raio2; }
        set { raio2 = value; }
    }

    public Elipse(int xCentro, int yCentro, int novoRaio1, int novoRaio2, Color novaCor, int espessura) : base(xCentro, yCentro, novaCor, espessura) // construtor de Ponto(x,y)
    {
        raio1 = novoRaio1;
        raio2 = novoRaio2;
    }

    public override void desenhar(Color corDesenho, Graphics g)
    {
        Pen pen = new Pen(corDesenho, base.Espessura);
        g.DrawEllipse(pen, base.X - raio1, base.Y - raio2, // centro - raio
        2 * raio1, 2 * raio2); // centro + raio
    }

    public override String ToString()
    {
        return transformaString("e", 5) +
        transformaString(X, 5) +
        transformaString(Y, 5) +
        transformaString(Cor.R, 5) +
        transformaString(Cor.G, 5) +
        transformaString(Cor.B, 5) +
        transformaString(Raio1, 5) +
        transformaString(Raio2, 5) +
        transformaString(Espessura, 5);
    }
}
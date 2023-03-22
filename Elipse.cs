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

    public Elipse(int xCentro, int yCentro, int novoRaio1, int novoRaio2, Color novaCor) : base(xCentro, yCentro, novaCor) // construtor de Ponto(x,y)
    {
        raio1 = novoRaio1;
        raio2 = novoRaio2;
    }

    public override void desenhar(Color corDesenho, Graphics g)
    {
        Pen pen = new Pen(corDesenho);
        g.DrawEllipse(pen, base.X - raio1, base.Y - raio2, // centro - raio
        2 * raio1, 2 * raio2); // centro + raio
    }
}
﻿using System.Drawing;

class Circulo : Ponto
{
    // herda o ponto central (x, y) da classe Ponto
    int raio;

    public int Raio
    {
        get { return raio; }
        set { raio = value; }
    }

    public Circulo(int xCentro, int yCentro, int novoRaio, Color novaCor) : base(xCentro, yCentro, novaCor) // construtor de Ponto(x,y)
    {
        raio = novoRaio;
    }

    public void setRaio(int novoRaio)
    {
        raio = novoRaio;
    }

    public void desenhar(Color corDesenho, Graphics g)
    {
        Pen pen = new Pen(corDesenho);
        g.DrawEllipse(pen,base.X - raio, base.Y - raio, // centro - raio
        2 * raio, 2 * raio); // centro + raio
    }
}
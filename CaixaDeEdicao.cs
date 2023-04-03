﻿using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace InfinityPaint
{
    class CaixaDeEdicao : Retangulo
    {
        static Color cor = Color.Blue;

        Ponto figuraInterna;

        public Ponto FiguraInterna
        {
            get
            {
                return figuraInterna;
            }
            set
            {
                figuraInterna = value;
            }
        }

        public CaixaDeEdicao(int x, int y, int largura, int altura, Ponto figuraInterna) : base(x, y, largura, altura, cor, 1)
        {
            FiguraInterna = figuraInterna;
        }

        public override void desenhar(Color cor, Graphics g)
        {
            if (figuraInterna != null)
                figuraInterna.desenhar(figuraInterna.Cor, g);

            g.DrawRectangle(new Pen(Cor), X, Y, Largura, Altura);

            // Draws squares in the corners
            g.FillRectangle(new SolidBrush(Cor), X - 2, Y - 2, 5, 5);

            g.FillRectangle(new SolidBrush(Cor), X + Largura - 2, Y - 2, 5, 5);
            g.FillRectangle(new SolidBrush(Cor), X - 2, Y + Altura - 2, 5, 5);
            g.FillRectangle(new SolidBrush(Cor), X + Largura - 2, Y + Altura - 2, 5, 5);

            // Draws squares in the middle
            g.FillRectangle(new SolidBrush(Cor), X + Largura / 2 - 2, Y - 2, 5, 5);
            g.FillRectangle(new SolidBrush(Cor), X + Largura / 2 - 2, Y + Altura - 2, 5, 5);
            g.FillRectangle(new SolidBrush(Cor), X - 2, Y + Altura / 2 - 2, 5, 5);
            g.FillRectangle(new SolidBrush(Cor), X + Largura - 2, Y + Altura / 2 - 2, 5, 5);
        }

        public string IsHovering(int x, int y)
        {
            const int interval = 4;

            // Top left corner
            if (x >= X - interval && x <= X + interval && y >= Y - interval && y <= Y + interval)
                return "NW";
            // Top middle
            if (x >= X + Largura / 2 - interval && x <= X + Largura / 2 + interval && y >= Y - interval && y <= Y + interval)
                return "N";
            // Top right corner
            if (x >= X + Largura - interval && x <= X + Largura + interval && y >= Y - interval && y <= Y + interval)
                return "NE";
            // Middle left
            if (x >= X - interval && x <= X + interval && y >= Y + Altura / 2 - interval && y <= Y + Altura / 2 + interval)
                return "W";
            // Middle right
            if (x >= X + Largura - interval && x <= X + Largura + interval && y >= Y + Altura / 2 - interval && y <= Y + Altura / 2 + interval)
                return "E";
            // Bottom left corner
            if (x >= X - interval && x <= X + interval && y >= Y + Altura - interval && y <= Y + Altura + interval)
                return "SW";
            // Bottom middle
            if (x >= X + Largura / 2 - 2 && x <= X + Largura / 2 + 2 && y >= Y + Altura - 2 && y <= Y + Altura + 2)
                return "S";
            // Bottom right corner
            if (x >= X + Largura - 2 && x <= X + Largura + 2 && y >= Y + Altura - 2 && y <= Y + Altura + 2)
                return "SE";
            // Middle
            if (x >= X && x <= X + Largura && y >= Y && y <= Y + Altura)
                return "C";

            return "";
        }

        public void Move(string direction, int mouseX, int mouseY)
        {
            switch (direction)
            {
                case "NW":
                    Largura = X + Largura - mouseX;
                    Altura = Y + Altura - mouseY;
                    X = mouseX;
                    Y = mouseY;
                    break;
                case "N":
                    Altura = Y + Altura - mouseY;
                    Y = mouseY;
                    break;
                case "NE":
                    Largura = mouseX - X;
                    Altura = Y + Altura - mouseY;
                    Y = mouseY;
                    break;
                case "W":
                    Largura = X + Largura - mouseX;
                    X = mouseX;
                    break;
                case "E":
                    Largura = mouseX - X;
                    break;
                case "SW":
                    Largura = X + Largura - mouseX;
                    Altura = mouseY - Y;
                    X = mouseX;
                    break;
                case "S":
                    Altura = mouseY - Y;
                    break;
                case "SE":
                    Largura = mouseX - X;
                    Altura = mouseY - Y;
                    break;
                case "C":
                    X = mouseX;
                    Y = mouseY;
                    break;
            }

            if (figuraInterna != null)
                AtualizarFiguraInterna(direction);
        }

        private void AtualizarFiguraInterna(string direction)
        {
            var figuraType = figuraInterna.GetType();

            if (figuraType == typeof(Retangulo))
            {
                var retangulo = (Retangulo)figuraInterna;
                retangulo.Largura = Largura;
                retangulo.Altura = Altura;
                retangulo.X = X;
                retangulo.Y = Y;

                if (Altura < 0)
                {
                    retangulo.Altura *= -1;
                    retangulo.Y -= retangulo.Altura;
                }
                if (Largura < 0)
                {
                    retangulo.Largura *= -1;
                    retangulo.X -= retangulo.Largura;
                }
            }
            else if (figuraType == typeof(Circulo))
            {
                var circulo = (Circulo)figuraInterna;
                if (direction == "W" || direction == "E")
                    Altura = Largura;
                else
                    Largura = Altura;


                if (direction != "C")
                {
                    X = circulo.X - (Largura / 2);
                    Y = circulo.Y - (Altura / 2);
                }
                else
                {
                    circulo.X = X + (Largura / 2);
                    circulo.Y = Y + (Altura / 2);
                }

                circulo.Raio = Largura / 2;
            }
            else if (figuraType == typeof(Elipse))
            {
                var elipse = (Elipse)figuraInterna;
                elipse.Raio1 = Largura / 2;
                elipse.Raio2 = Altura / 2;
                elipse.X = X + elipse.Raio1;
                elipse.Y = Y + elipse.Raio2;
            }
        }
    }
}

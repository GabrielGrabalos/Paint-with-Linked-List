using Microsoft.VisualBasic;
using System.Drawing;

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
            figuraInterna.desenhar(cor, g);

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

        internal void Move(string direction, int deltaX, int deltaY)
        {
            if (direction == "N")
            {
                Y += deltaY;
                Altura -= deltaY;
            }
            else if (direction == "NW")
            {
                X += deltaX;
                Y += deltaY;
                Largura -= deltaX;
                Altura -= deltaY;
            }
            else if (direction == "NE")
            {
                Y += deltaY;
                Largura += deltaX;
                Altura -= deltaY;
            }
            else if (direction == "W")
            {
                X += deltaX;
                Largura -= deltaX;
            }
            else if (direction == "E")
            {
                Largura += deltaX;
            }
            else if (direction == "SW")
            {
                X += deltaX;
                Largura -= deltaX;
                Altura += deltaY;
            }
            else if (direction == "S")
            {
                Altura += deltaY;
            }
            else if (direction == "SE")
            {
                Largura += deltaX;
                Altura += deltaY;
            }
            else if (direction == "C")
            {
                X += deltaX;
                Y += deltaY;
            }  
        }
    }
}

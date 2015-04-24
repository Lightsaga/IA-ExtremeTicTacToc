using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA_ExtremeTicTacToc
{
    public class Tablero
    {
        public Celda[,] Matrix;
        
        public Tablero()
        {
            Matrix = new Celda[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Matrix[i, j] = new Celda();
                    Matrix[i, j].tamaño = new Size(60,60);
                    Matrix[i, j].position = new Point(i, j);
                    Matrix[i, j].state = 0;
                }
            }
        }

        public void DibujarTablero(PictureBox canvas,Point puntoInicial)
        {
            int escala = 1;
            Graphics g = canvas.CreateGraphics();
            //Size tam = new Size(escala*50,escala*50);
            Rectangle rect;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Matrix[i, j].Coordinates = new Point(puntoInicial.X + (escala * Matrix[i, j].tamaño.Width * i), puntoInicial.Y + (escala * Matrix[i, j].tamaño.Height * j));
                    rect = new Rectangle(Matrix[i, j].Coordinates, new Size(escala * Matrix[i, j].tamaño.Width, escala * Matrix[i, j].tamaño.Height));
                    g.DrawRectangle(new Pen(Color.Black,3),rect);
                    
                }
            }
            //g.DrawLine(new Pen(Color.Black, 20), 10, 10, 80, 80);
        }
    }
}

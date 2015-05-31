using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA_ExtremeTicTacToc
{
    public partial class TictacForm : Form
    {
        Tablero[,] Tableros = new Tablero[3, 3];
        bool turno = false;
        bool PrimerMov = false;
        int PerX = 0;
        int PerY = 0;

        public TictacForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            int calculo = 0;

            for (int i = 81; i > 0; i--)
            {
                calculo += i;
            }
            calculo = calculo * 81;
             */
            CargarTableros();
            label2.Text = "Turno de Jugador 1";



        }

        public void CargarTableros()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Tableros[i, j] = new Tablero();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pbCanvas.Refresh();
            int espacioX = 0;
            int espacioY = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Tableros[i, j].DibujarTablero(pbCanvas, 
    new Point(10 + (Tableros[i, j].Matrix[i, j].tamaño.Width * 3 * i)+espacioX, 10 + (Tableros[i, j].Matrix[i, j].tamaño.Height * 3 * j)+espacioY));
                    espacioY += 10; 
                }
                espacioX += 10;
                espacioY = 0;
            }
            //Tableros[0,0].DibujarTablero(pbCanvas, new Point(10,10));

        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = pbCanvas.CreateGraphics();
            label1.Text = "X: " + e.X + " Y: " + e.Y;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            if ((e.X >= Tableros[i, j].Matrix[k, l].Coordinates.X && e.X < Tableros[i, j].Matrix[k, l].Coordinates.X + 60) && (e.Y >= Tableros[i, j].Matrix[k, l].Coordinates.Y && e.Y < Tableros[i, j].Matrix[k, l].Coordinates.Y + 60) && PrimerMov == false)
                            {
                                g.DrawLine(new Pen(Color.Red, 10), Tableros[i, j].Matrix[k, l].Coordinates.X, Tableros[i, j].Matrix[k, l].Coordinates.Y, Tableros[i, j].Matrix[k, l].Coordinates.X + 60, Tableros[i, j].Matrix[k, l].Coordinates.Y + 60);
                                g.DrawLine(new Pen(Color.Red, 10), Tableros[i, j].Matrix[k, l].Coordinates.X + 60, Tableros[i, j].Matrix[k, l].Coordinates.Y, Tableros[i, j].Matrix[k, l].Coordinates.X, Tableros[i, j].Matrix[k, l].Coordinates.Y + 60);
                                turno = true;
                                PrimerMov = true;
                                Tableros[i, j].Matrix[k, l].state = 1;
                                PerX = k;
                                PerY = l;
                                label2.Text = "Turno de Jugador 2";
                            }
                            else if ((e.X >= Tableros[PerX, PerY].Matrix[k, l].Coordinates.X && e.X < Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60) && (e.Y >= Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y && e.Y < Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60) && PrimerMov == true)
                            {
                                if (turno == false && Tableros[PerX, PerY].Matrix[k, l].state == 0)
                                {
                                    g.DrawLine(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60);
                                    g.DrawLine(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60);
                                    turno = true;
                                    Tableros[PerX, PerY].Matrix[k, l].state = 1;
                                    PerX = k;
                                    PerY = l;
                                    label2.Text = "Turno de Jugador 2";
                                }
                                else if (turno == true && Tableros[PerX, PerY].Matrix[k, l].state == 0)
                                {
                                    g.DrawEllipse(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, 60, 60);
                                    turno = false;
                                    Tableros[PerX, PerY].Matrix[k, l].state = 1;
                                    PerX = k;
                                    PerY = l;
                                    label2.Text = "Turno de Jugador 1";
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

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
        Point tableroActual;

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
                                g.DrawEllipse(new Pen(Color.Red, 10), Tableros[i, j].Matrix[k, l].Coordinates.X, Tableros[i, j].Matrix[k, l].Coordinates.Y, 60, 60);
                                turno = true;
                                PrimerMov = true;
                                Tableros[i, j].Matrix[k, l].state = 1;
                                PerX = k;
                                PerY = l;
                                tableroActual.X = i;
                                tableroActual.Y = j;
                                label2.Text = "Turno de Jugador 2";
                            }
                            else if ((e.X >= Tableros[PerX, PerY].Matrix[k, l].Coordinates.X && e.X < Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60) && (e.Y >= Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y && e.Y < Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60) && PrimerMov == true)
                            {
                                if (turno == false && Tableros[PerX, PerY].Matrix[k, l].state == 0)
                                {
                                    g.DrawEllipse(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, 60, 60);
                                    turno = true;
                                    Tableros[PerX, PerY].Matrix[k, l].state = 1;
                                    tableroActual.X = PerX;
                                    tableroActual.Y = PerY;
                                    PerX = k;
                                    PerY = l;
                                    
                                    label2.Text = "Turno de Jugador 2";
                                }
                                else if (turno == true && Tableros[PerX, PerY].Matrix[k, l].state == 0)
                                {
                                    g.DrawLine(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60);
                                    g.DrawLine(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60);
                                    
                                    turno = false;
                                    Tableros[PerX, PerY].Matrix[k, l].state = 2;
                                    tableroActual.X = PerX;
                                    tableroActual.Y = PerY;
                                    PerX = k;
                                    PerY = l;
                                    label2.Text = "Turno de Jugador 1";
                                }
                            }
                        }
                    }
                }
            }
            RevisarEstadosTableroParcial();
        }

        public void RevisarEstadosTableroParcial()
        {
            Tablero T = Tableros[tableroActual.X, tableroActual.Y];
            Graphics g = pbCanvas.CreateGraphics();

            bool Ganar = false;
            //Horizontales
            for (int i = 0; i < 3; i++)
            {
                if (T.Matrix[0, i].state == 1 && T.Matrix[1, i].state == 1 && T.Matrix[2, i].state == 1)
                {
                    Ganar = true;
                    g.DrawLine(new Pen(Color.Red, 10), T.Matrix[0, i].Coordinates.X, T.Matrix[0, i].Coordinates.Y + 30, T.Matrix[2, i].Coordinates.X + 60, T.Matrix[2, i].Coordinates.Y + 30);
                }
                if (T.Matrix[0, i].state == 2 && T.Matrix[1, i].state == 2 && T.Matrix[2, i].state == 2)
                {
                    Ganar = true;
                    g.DrawLine(new Pen(Color.Red, 10), T.Matrix[0, i].Coordinates.X, T.Matrix[0, i].Coordinates.Y + 30, T.Matrix[2, i].Coordinates.X + 60, T.Matrix[2, i].Coordinates.Y + 30);
                }
            }
            //Verticales
            for (int i = 0; i < 3; i++)
            {
                if (T.Matrix[i, 0].state == 1 && T.Matrix[i, 1].state == 1 && T.Matrix[i, 2].state == 1)
                {
                    Ganar = true;
                    g.DrawLine(new Pen(Color.Red, 10), T.Matrix[i, 0].Coordinates.X + 30, T.Matrix[i, 0].Coordinates.Y, T.Matrix[i, 2].Coordinates.X + 30, T.Matrix[i, 2].Coordinates.Y + 60);
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            T.Matrix[j, k].state = 1;
                        }
                    }
                }
                if (T.Matrix[i, 0].state == 2 && T.Matrix[i, 1].state == 2 && T.Matrix[i, 2].state == 2)
                {
                    Ganar = true;
                    g.DrawLine(new Pen(Color.Red, 10), T.Matrix[i, 0].Coordinates.X + 30, T.Matrix[i, 0].Coordinates.Y, T.Matrix[i, 2].Coordinates.X + 30, T.Matrix[i, 2].Coordinates.Y + 60);
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            T.Matrix[j, k].state = 2;
                        }
                    }
                }
            }
            //Diagonales
            if (T.Matrix[0, 0].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[2, 2].state == 1)
            {
                Ganar = true;
                g.DrawLine(new Pen(Color.Red, 10), T.Matrix[0, 0].Coordinates.X, T.Matrix[0, 0].Coordinates.Y, T.Matrix[2, 2].Coordinates.X + 60, T.Matrix[2, 2].Coordinates.Y + 60);
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        T.Matrix[j, k].state = 1;
                    }
                }
            }
            if (T.Matrix[0, 0].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[2, 2].state == 2)
            {
                Ganar = true;
                g.DrawLine(new Pen(Color.Red, 10), T.Matrix[0, 0].Coordinates.X, T.Matrix[0, 0].Coordinates.Y, T.Matrix[2, 2].Coordinates.X + 60, T.Matrix[2, 2].Coordinates.Y + 60);
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        T.Matrix[j, k].state = 2;
                    }
                }
            }
            if (T.Matrix[0, 2].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[2, 0].state == 1)
            {
                Ganar = true;
                g.DrawLine(new Pen(Color.Red, 10), T.Matrix[0, 2].Coordinates.X, T.Matrix[0, 2].Coordinates.Y + 60, T.Matrix[2, 0].Coordinates.X + 60, T.Matrix[2, 0].Coordinates.Y);
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        T.Matrix[j, k].state = 1;
                    }
                }
            }
            if (T.Matrix[0, 2].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[2, 0].state == 2)
            {
                Ganar = true;
                g.DrawLine(new Pen(Color.Red, 10), T.Matrix[0, 2].Coordinates.X, T.Matrix[0, 2].Coordinates.Y + 60, T.Matrix[2, 0].Coordinates.X + 60, T.Matrix[2, 0].Coordinates.Y);
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        T.Matrix[j, k].state = 2;
                    }
                }
            }

            if (Ganar)
                FinJuego();
        }
        public void FinJuego()
        {
            MessageBox.Show("Gana " + (turno==false?"Computadora":"Jugador"));
        }

    }
}

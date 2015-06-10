using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
        Point p;
        

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
            cbxDificultad.Items.Add("Facil");
            cbxDificultad.Items.Add("Normal");
            cbxDificultad.Items.Add("Dificil");
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
            Graphics g = pictureBox1.CreateGraphics();
            Rectangle rect;
            int posX = 0, posY = 0;
            for (int i = 0; i < 3; i++)
            {
                posY = 0;
                for (int j = 0; j < 3; j++)
                {
                    rect = new Rectangle(posX, posY, 60, 60);
                    g.DrawRectangle(new Pen(Color.Black,3),rect);
                    posY += 60;
                }
                posX += 60;
            }
            

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
                            if ((e.X >= Tableros[i, j].Matrix[k, l].Coordinates.X && e.X < Tableros[i, j].Matrix[k, l].Coordinates.X + 60) && (e.Y >= Tableros[i, j].Matrix[k, l].Coordinates.Y && e.Y < Tableros[i, j].Matrix[k, l].Coordinates.Y + 60) && PrimerMov == false && Tableros[i, j].estado == 0)
                            {
                                if (Tableros[i, j].Matrix[k, l].state == 0 && Tableros[i, j].estado == 0)
                                {
                                    if (!turno)
                                        g.DrawEllipse(new Pen(Color.Red, 10), Tableros[i, j].Matrix[k, l].Coordinates.X, Tableros[i, j].Matrix[k, l].Coordinates.Y, 60, 60);
                                    else
                                    {
                                        g.DrawLine(new Pen(Color.Blue, 10), Tableros[i, j].Matrix[k, l].Coordinates.X, Tableros[i, j].Matrix[k, l].Coordinates.Y, Tableros[i, j].Matrix[k, l].Coordinates.X + 60, Tableros[i, j].Matrix[k, l].Coordinates.Y + 60);
                                        g.DrawLine(new Pen(Color.Blue, 10), Tableros[i, j].Matrix[k, l].Coordinates.X + 60, Tableros[i, j].Matrix[k, l].Coordinates.Y, Tableros[i, j].Matrix[k, l].Coordinates.X, Tableros[i, j].Matrix[k, l].Coordinates.Y + 60);
                                    }
                                    turno = !turno ? true : false;
                                    PrimerMov = true;
                                    Tableros[i, j].Matrix[k, l].state = turno ? 1 : 2;
                                    PerX = k;
                                    PerY = l;
                                    label4.Text = PerX + "," + PerY;
                                    tableroActual.X = i;
                                    tableroActual.Y = j;
                                    RevisarEstadosTableroParcial();
                                    if (Tableros[PerX, PerY].estado == 1 || Tableros[PerX, PerY].estado == 2 || Tableros[PerX, PerY].estado == 3)
                                    {
                                        PrimerMov = false;
                                    }
                                    label2.Text = !turno ? "Turno de Jugador 1" : "Turno de Jugador 2";
                                    if (turno)
                                        MovimientoComputadora();
                                }
                            }
                            else if ((e.X >= Tableros[PerX, PerY].Matrix[k, l].Coordinates.X && e.X < Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60) && (e.Y >= Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y && e.Y < Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60) && PrimerMov == true)
                            {
                                
                                if (/*turno == false &&*/ Tableros[PerX, PerY].Matrix[k, l].state == 0 && Tableros[PerX,PerY].estado == 0&&PrimerMov==true)
                                {
                                    if (!turno)
                                        g.DrawEllipse(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, 60, 60);
                                    else
                                    {
                                        g.DrawLine(new Pen(Color.Blue, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60);
                                        g.DrawLine(new Pen(Color.Blue, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60);
                                    }

                                    turno = !turno?true:false;
                                    Tableros[PerX, PerY].Matrix[k, l].state = turno ? 1 : 2;
                                    tableroActual.X = PerX;
                                    tableroActual.Y = PerY;
                                    PerX = k;
                                    PerY = l;
                                    label4.Text = PerX + "," + PerY;
                                    RevisarEstadosTableroParcial();
                                    if (Tableros[PerX, PerY].estado == 1 || Tableros[PerX, PerY].estado == 2 || Tableros[PerX, PerY].estado == 3)
                                    {
                                        PrimerMov = false;
                                    }
                                    label2.Text = !turno ? "Turno de Jugador 1" : "Turno de Jugador 2";
                                    if (turno)
                                        MovimientoComputadora();
                                    return;
                                    
                                }
                                /*else if (turno == true && Tableros[PerX, PerY].Matrix[k, l].state == 0 && Tableros[PerX, PerY].estado == 0 && PrimerMov == true)
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
                                }*/

                            }
                        }
                    }
                }
            }
            RevisarEstadosTableroTotal();

        }

        public void MovimientoComputadora()
        {
            Tablero T = Tableros[PerX, PerY];
            Graphics g = pbCanvas.CreateGraphics();

            if (cbxDificultad.SelectedItem == "Facil")
            {
                if(!PrimerMov)
                {
                    DificultadFacil(TableroAleatorio());
                }
                else
                {
                    DificultadFacil(T);
                }
            }
            if (cbxDificultad.SelectedItem == "Normal")
            {
                if (!PrimerMov)
                {
                    DificultadNormal(TableroAleatorio(),1);
                }
                else
                {
                    DificultadNormal(T,1);
                }
            }
            if (cbxDificultad.SelectedItem == "Dificil")
            {
                if (!PrimerMov)
                {
                    DificultadDificil(TableroConveniente(1));
                }
                else
                {
                    DificultadDificil(T);
                }
            }
            if (cbxDificultad.SelectedItem == "")
            {
               //MessageBox.Show("Elige dificultad");
            }
        }

        public void RevisarEstadosTableroParcial()
        {
            Tablero T = Tableros[tableroActual.X, tableroActual.Y];
            Graphics g = pbCanvas.CreateGraphics();
            Graphics g1 = pictureBox1.CreateGraphics();

            //Horizontales
            for (int i = 0; i < 3; i++)
            {
                if (T.Matrix[0, i].state == 1 && T.Matrix[1, i].state == 1 && T.Matrix[2, i].state == 1)
                {
                    g.DrawLine(new Pen(Color.Black, 10), T.Matrix[0, i].Coordinates.X, T.Matrix[0, i].Coordinates.Y + 30, T.Matrix[2, i].Coordinates.X + 60, T.Matrix[2, i].Coordinates.Y + 30);
                    T.estado = 1;
                }
                if (T.Matrix[0, i].state == 2 && T.Matrix[1, i].state == 2 && T.Matrix[2, i].state == 2)
                {
                    g.DrawLine(new Pen(Color.Black, 10), T.Matrix[0, i].Coordinates.X, T.Matrix[0, i].Coordinates.Y + 30, T.Matrix[2, i].Coordinates.X + 60, T.Matrix[2, i].Coordinates.Y + 30);
                    T.estado = 2;
                }
            }
            //Verticales
            for (int i = 0; i < 3; i++)
            {
                if (T.Matrix[i, 0].state == 1 && T.Matrix[i, 1].state == 1 && T.Matrix[i, 2].state == 1)
                {
                    g.DrawLine(new Pen(Color.Black, 10), T.Matrix[i, 0].Coordinates.X + 30, T.Matrix[i, 0].Coordinates.Y, T.Matrix[i, 2].Coordinates.X + 30, T.Matrix[i, 2].Coordinates.Y + 60);
                    T.estado = 1;
                }
                if (T.Matrix[i, 0].state == 2 && T.Matrix[i, 1].state == 2 && T.Matrix[i, 2].state == 2)
                {
                    g.DrawLine(new Pen(Color.Black, 10), T.Matrix[i, 0].Coordinates.X + 30, T.Matrix[i, 0].Coordinates.Y, T.Matrix[i, 2].Coordinates.X + 30, T.Matrix[i, 2].Coordinates.Y + 60);
                    T.estado = 2;
                }
            }
            //Diagonales
            if (T.Matrix[0, 0].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[2, 2].state == 1)
            {
                g.DrawLine(new Pen(Color.Black, 10), T.Matrix[0, 0].Coordinates.X, T.Matrix[0, 0].Coordinates.Y, T.Matrix[2, 2].Coordinates.X + 60, T.Matrix[2, 2].Coordinates.Y + 60);
                T.estado = 1;
            }
            if (T.Matrix[0, 0].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[2, 2].state == 2)
            {
                g.DrawLine(new Pen(Color.Black, 10), T.Matrix[0, 0].Coordinates.X, T.Matrix[0, 0].Coordinates.Y, T.Matrix[2, 2].Coordinates.X + 60, T.Matrix[2, 2].Coordinates.Y + 60);
                T.estado = 2;
            }
            if (T.Matrix[0, 2].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[2, 0].state == 1)
            {
                g.DrawLine(new Pen(Color.Black, 10), T.Matrix[0, 2].Coordinates.X, T.Matrix[0, 2].Coordinates.Y + 60, T.Matrix[2, 0].Coordinates.X + 60, T.Matrix[2, 0].Coordinates.Y);
                T.estado = 1;
            }
            if (T.Matrix[0, 2].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[2, 0].state == 2)
            {
                g.DrawLine(new Pen(Color.Black, 10), T.Matrix[0, 2].Coordinates.X, T.Matrix[0, 2].Coordinates.Y + 60, T.Matrix[2, 0].Coordinates.X + 60, T.Matrix[2, 0].Coordinates.Y);
                T.estado = 2;
            }
            int aux = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (T.Matrix[i, j].state == 1 || T.Matrix[i, j].state == 2)
                    {
                        aux++;
                    }
                    if (Tableros[i, j].estado == 1)
                    {
                        if (i == 0 && j == 0)
                        {
                            g1.DrawEllipse(new Pen(Color.Red, 10), 0, 0, 60, 60);
                        }
                        if (i == 0 && j == 1)
                        {
                            g1.DrawEllipse(new Pen(Color.Red, 10), 0, 60, 60, 60);
                        }
                        if (i == 0 && j == 2)
                        {
                            g1.DrawEllipse(new Pen(Color.Red, 10), 0, 120, 60, 60);
                        }
                        if (i == 1 && j == 0)
                        {
                            g1.DrawEllipse(new Pen(Color.Red, 10), 60, 0, 60, 60);
                        }
                        if (i == 1 && j == 1)
                        {
                            g1.DrawEllipse(new Pen(Color.Red, 10), 60, 60, 60, 60);
                        }
                        if (i == 1 && j ==2)
                        {
                            g1.DrawEllipse(new Pen(Color.Red, 10), 60, 120, 60, 60);
                        }
                        if (i == 2 && j == 0)
                        {
                            g1.DrawEllipse(new Pen(Color.Red, 10), 120, 0, 60, 60);
                        }
                        if (i == 2 && j == 1)
                        {
                            g1.DrawEllipse(new Pen(Color.Red, 10), 120, 60, 60, 60);
                        }
                        if (i == 2 && j == 2)
                        {
                            g1.DrawEllipse(new Pen(Color.Red, 10), 120, 120, 60, 60);
                        }
                    }
                    if (Tableros[i, j].estado == 2)
                    {
                        if (i == 0 && j == 0)
                        {
                            g1.DrawLine(new Pen(Color.Blue, 10), 0, 0, 60, 60);
                            g1.DrawLine(new Pen(Color.Blue, 10), 0, 60, 60, 0);
                        }
                        if (i == 0 && j == 1)
                        {
                            g1.DrawLine(new Pen(Color.Blue, 10), 0, 60, 60, 120);
                            g1.DrawLine(new Pen(Color.Blue, 10), 0, 120, 60, 60);
                        }
                        if (i == 0 && j == 2)
                        {
                            g1.DrawLine(new Pen(Color.Blue, 10), 0, 120, 60, 180);
                            g1.DrawLine(new Pen(Color.Blue, 10), 0, 180, 60, 120);
                        }
                        if (i == 1 && j == 0)
                        {
                            g1.DrawLine(new Pen(Color.Blue, 10), 60, 0, 120, 60);
                            g1.DrawLine(new Pen(Color.Blue, 10), 60, 60, 120, 0);
                        }
                        if (i == 1 && j == 1)
                        {
                            g1.DrawLine(new Pen(Color.Blue, 10), 60, 60, 120, 120);
                            g1.DrawLine(new Pen(Color.Blue, 10), 60, 120, 120, 60);
                        }
                        if (i == 1 && j == 2)
                        {
                            g1.DrawLine(new Pen(Color.Blue, 10), 60, 120, 120, 180);
                            g1.DrawLine(new Pen(Color.Blue, 10), 60, 180, 120, 60);
                        }
                        if (i == 2 && j == 0)
                        {
                            g1.DrawLine(new Pen(Color.Blue, 10), 120, 0, 180, 60);
                            g1.DrawLine(new Pen(Color.Blue, 10), 120, 60, 180, 0);
                        }
                        if (i == 2 && j == 1)
                        {
                            g1.DrawLine(new Pen(Color.Blue, 10), 120, 60, 180, 120);
                            g1.DrawLine(new Pen(Color.Blue, 10), 120, 120, 180, 60);
                        }
                        if (i == 2 && j == 2)
                        {
                            g1.DrawLine(new Pen(Color.Blue, 10), 120, 120, 180, 180);
                            g1.DrawLine(new Pen(Color.Blue, 10), 120, 180, 180, 120);
                        }
                    }
                }
            }
            if(aux==9)
            {
                T.estado = 3;
            }
        }

        public void RevisarEstadosTableroTotal()
        {
            Graphics g = pbCanvas.CreateGraphics();

            bool Ganar = false;
            //Horizontales
            for (int i = 0; i < 3; i++)
            {
                if (Tableros[0, i].estado == 1 && Tableros[1, i].estado == 1 && Tableros[2, i].estado == 1)
                {
                    Ganar = true;
                }
                if (Tableros[0, i].estado == 2 && Tableros[1, i].estado == 2 && Tableros[2, i].estado == 2)
                {
                    Ganar = true;
                }
            }
            //Verticales
            for (int i = 0; i < 3; i++)
            {
                if (Tableros[i, 0].estado == 1 && Tableros[i, 1].estado == 1 && Tableros[i, 2].estado == 1)
                {
                    Ganar = true;
                }
                if (Tableros[i, 0].estado == 2 && Tableros[i, 1].estado == 2 && Tableros[i, 2].estado == 2)
                {
                    Ganar = true;
                }
            }
            //Diagonales
            if (Tableros[0, 0].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[2, 2].estado == 1)
            {
                Ganar = true;
            }
            if (Tableros[0, 0].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[2, 2].estado == 2)
            {
                Ganar = true;
            }
            if (Tableros[0, 2].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[2, 0].estado == 1)
            {
                Ganar = true;
            }
            if (Tableros[0, 2].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[2, 0].estado == 2)
            {
                Ganar = true;
            }

            if (Ganar)
                FinJuego();
        }
        public Point RevisarSiguienteTurno(Tablero T,int i)
        {
            Point p = new Point(-1, -1);
            //Tablero T = Tableros[tableroActual.X, tableroActual.Y];

            if (i == 1)
            {
                //Ganar//
                //Esquinas
                if (T.Matrix[0, 0].state == 2 && T.Matrix[0, 2].state == 2 && T.Matrix[0, 1].state == 0)
                    p = new Point(0, 1);
                if (T.Matrix[0, 0].state == 2 && T.Matrix[2, 0].state == 2 && T.Matrix[1, 0].state == 0)
                    p = new Point(1, 0);
                if (T.Matrix[0, 0].state == 2 && T.Matrix[2, 2].state == 2 && T.Matrix[1, 1].state == 0)
                    p = new Point(1, 1);

                if (T.Matrix[2, 0].state == 2 && T.Matrix[2, 2].state == 2 && T.Matrix[2, 1].state == 0)
                    p = new Point(2, 1);
                if (T.Matrix[2, 0].state == 2 && T.Matrix[0, 2].state == 2 && T.Matrix[1, 1].state == 0)
                    p = new Point(1, 1);

                if (T.Matrix[2, 2].state == 2 && T.Matrix[0, 2].state == 2 && T.Matrix[1, 2].state == 0)
                    p = new Point(1, 2);

                //Medios
                if (T.Matrix[1, 0].state == 2 && T.Matrix[1, 2].state == 2 && T.Matrix[1, 1].state == 0)
                    p = new Point(1, 1);
                if (T.Matrix[0, 1].state == 2 && T.Matrix[2, 1].state == 2 && T.Matrix[1, 1].state == 0)
                    p = new Point(1, 1);

                if (T.Matrix[1, 0].state == 2 && T.Matrix[0, 0].state == 2 && T.Matrix[2, 0].state == 0)
                    p = new Point(2, 0);
                if (T.Matrix[1, 0].state == 2 && T.Matrix[2, 0].state == 2 && T.Matrix[0, 0].state == 0)
                    p = new Point(0, 0);

                if (T.Matrix[0, 1].state == 2 && T.Matrix[0, 0].state == 2 && T.Matrix[0, 2].state == 0)
                    p = new Point(0, 2);
                if (T.Matrix[0, 1].state == 2 && T.Matrix[0, 2].state == 2 && T.Matrix[0, 0].state == 0)
                    p = new Point(0, 0);
                if (T.Matrix[0, 1].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[2, 1].state == 0)
                    p = new Point(2, 1);

                if (T.Matrix[1, 2].state == 2 && T.Matrix[0, 2].state == 2 && T.Matrix[2, 2].state == 0)
                    p = new Point(2, 2);
                if (T.Matrix[1, 2].state == 2 && T.Matrix[2, 2].state == 2 && T.Matrix[0, 2].state == 0)
                    p = new Point(0, 2);

                if (T.Matrix[2, 1].state == 2 && T.Matrix[2, 0].state == 2 && T.Matrix[2, 2].state == 0)
                    p = new Point(2, 2);
                if (T.Matrix[2, 1].state == 2 && T.Matrix[2, 2].state == 2 && T.Matrix[2, 0].state == 0)
                    p = new Point(2, 0);
                if (T.Matrix[2, 1].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[0, 1].state == 0)
                    p = new Point(0, 1);

                //Centro
                if (T.Matrix[0, 0].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[2, 2].state == 0)
                    p = new Point(2, 2);
                if (T.Matrix[0, 1].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[2, 1].state == 0)
                    p = new Point(2, 1);
                if (T.Matrix[0, 2].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[2, 0].state == 0)
                    p = new Point(2, 0);
                if (T.Matrix[1, 0].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[1, 2].state == 0)
                    p = new Point(1, 2);
                if (T.Matrix[1, 2].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[1, 0].state == 0)
                    p = new Point(1, 0);
                if (T.Matrix[2, 0].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[0, 2].state == 0)
                    p = new Point(0, 2);
                if (T.Matrix[2, 1].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[0, 1].state == 0)
                    p = new Point(0, 1);
                if (T.Matrix[2, 2].state == 2 && T.Matrix[1, 1].state == 2 && T.Matrix[0, 0].state == 0)
                    p = new Point(0, 0);
            }
            else
            {
                //Bloqueos//
                //Esquinas
                if (T.Matrix[0, 0].state == 1 && T.Matrix[0, 2].state == 1 && T.Matrix[0, 1].state == 0)
                    p = new Point(0, 1);
                if (T.Matrix[0, 0].state == 1 && T.Matrix[2, 0].state == 1 && T.Matrix[1, 0].state == 0)
                    p = new Point(1, 0);
                if (T.Matrix[0, 0].state == 1 && T.Matrix[2, 2].state == 1 && T.Matrix[1, 1].state == 0)
                    p = new Point(1, 1);

                if (T.Matrix[2, 0].state == 1 && T.Matrix[2, 2].state == 1 && T.Matrix[2, 1].state == 0)
                    p = new Point(2, 1);
                if (T.Matrix[2, 0].state == 1 && T.Matrix[0, 2].state == 1 && T.Matrix[1, 1].state == 0)
                    p = new Point(1, 1);

                if (T.Matrix[2, 2].state == 1 && T.Matrix[0, 2].state == 1 && T.Matrix[1, 2].state == 0)
                    p = new Point(1, 2);

                //Medios
                if (T.Matrix[1, 0].state == 1 && T.Matrix[1, 2].state == 1 && T.Matrix[1, 1].state == 0)
                    p = new Point(1, 1);
                if (T.Matrix[0, 1].state == 1 && T.Matrix[2, 1].state == 1 && T.Matrix[1, 1].state == 0)
                    p = new Point(1, 1);

                if (T.Matrix[1, 0].state == 1 && T.Matrix[0, 0].state == 1 && T.Matrix[2, 0].state == 0)
                    p = new Point(2, 0);
                if (T.Matrix[1, 0].state == 1 && T.Matrix[2, 0].state == 1 && T.Matrix[0, 0].state == 0)
                    p = new Point(0, 0);

                if (T.Matrix[0, 1].state == 1 && T.Matrix[0, 0].state == 1 && T.Matrix[0, 2].state == 0)
                    p = new Point(0, 2);
                if (T.Matrix[0, 1].state == 1 && T.Matrix[0, 2].state == 1 && T.Matrix[0, 0].state == 0)
                    p = new Point(0, 0);
                if (T.Matrix[0, 1].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[2, 1].state == 0)
                    p = new Point(2, 1);

                if (T.Matrix[1, 2].state == 1 && T.Matrix[0, 2].state == 1 && T.Matrix[2, 2].state == 0)
                    p = new Point(2, 2);
                if (T.Matrix[1, 2].state == 1 && T.Matrix[2, 2].state == 1 && T.Matrix[0, 2].state == 0)
                    p = new Point(0, 2);

                if (T.Matrix[2, 1].state == 1 && T.Matrix[2, 0].state == 1 && T.Matrix[2, 2].state == 0)
                    p = new Point(2, 2);
                if (T.Matrix[2, 1].state == 1 && T.Matrix[2, 2].state == 1 && T.Matrix[2, 0].state == 0)
                    p = new Point(2, 0);
                if (T.Matrix[2, 1].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[0, 1].state == 0)
                    p = new Point(0, 1);

                //Centro
                if (T.Matrix[0, 0].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[2, 2].state == 0)
                    p = new Point(2, 2);
                if (T.Matrix[0, 1].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[2, 1].state == 0)
                    p = new Point(2, 1);
                if (T.Matrix[0, 2].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[2, 0].state == 0)
                    p = new Point(2, 0);
                if (T.Matrix[1, 0].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[1, 2].state == 0)
                    p = new Point(1, 2);
                if (T.Matrix[1, 2].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[1, 0].state == 0)
                    p = new Point(1, 0);
                if (T.Matrix[2, 0].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[0, 2].state == 0)
                    p = new Point(0, 2);
                if (T.Matrix[2, 1].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[0, 1].state == 0)
                    p = new Point(0, 1);
                if (T.Matrix[2, 2].state == 1 && T.Matrix[1, 1].state == 1 && T.Matrix[0, 0].state == 0)
                    p = new Point(0, 0);

            }
            return p;
        }
        public void FinJuego()
        {
            MessageBox.Show("Gana " + (turno==false?"Computadora":"Jugador"));
        }

        public void DificultadFacil(Tablero T)
        {  
            int X, Y;
            X = r.Next(0, 3);
            Y = r.Next(0, 3);
            Celda C = T.Matrix[X,Y];
            if (C.state == 1 || C.state == 2)
                DificultadFacil(T);
            DoMouseClick(C.Coordinates.X+50, C.Coordinates.Y+50);
        }
        public void DificultadNormal(Tablero T, int i)
        {
            Point p = RevisarSiguienteTurno(T,i);
            if (p.X != -1 && p.Y != -1)
            {
                if (T.Matrix[p.X, p.Y].state == 0)
                    DoMouseClick(T.Matrix[p.X, p.Y].Coordinates.X + 50, T.Matrix[p.X, p.Y].Coordinates.Y + 50);
                else if (i == 1)
                    DificultadNormal(T, i + 1);

                else
                    DificultadFacil(T);
            }
            else
            {
                if (i == 1)
                    DificultadNormal(T, i + 1);
                else
                    DificultadFacil(T);
            }
        }
        public void DificultadDificil(Tablero T)
        {
            Tablero aux = T;
            if(!PrimerMov)
            {
                //Si es turno libre...
                //En base al TableroConveniente
                aux = TableroConvenienteFinal();
            }
            Point P = TableroConvenienteFuturo(aux);
                if (P.X == -2 && P.Y == -2)
                    DificultadNormal(aux, 1);
                else
                    DoMouseClick(aux.Matrix[P.X, P.Y].Coordinates.X + 50, aux.Matrix[P.X, P.Y].Coordinates.Y + 50);
        }

        Random r = new Random();
        public Tablero TableroAleatorio()
        {
            int X, Y;
            while (true)
            {
                X = r.Next(0, 3);
                Y = r.Next(0, 3);
                if (Tableros[X, Y].estado == 0)
                    break;
                
            }
            
            
            /*if (Tableros[X, Y].estado == 1 || Tableros[X, Y].estado == 2)
                TableroAleatorio();*/

           return Tableros[X, Y];
            
        }

        //Dificil
        public Tablero TableroConveniente(int i)
        {
            Point p = new Point(-1, -1);
            //Tablero T = Tableros[tableroActual.X, tableroActual.Y];

            if (i == 1)
            {
                //Ganar//
                //Esquinas
                if (Tableros[0, 0].estado == 2 && Tableros[0, 2].estado == 2 && Tableros[0, 1].estado == 0)
                    p = new Point(0, 1);
                if (Tableros[0, 0].estado == 2 && Tableros[2, 0].estado == 2 && Tableros[1, 0].estado == 0)
                    p = new Point(1, 0);
                if (Tableros[0, 0].estado == 2 && Tableros[2, 2].estado == 2 && Tableros[1, 1].estado == 0)
                    p = new Point(1, 1);

                if (Tableros[2, 0].estado == 2 && Tableros[2, 2].estado == 2 && Tableros[2, 1].estado == 0)
                    p = new Point(2, 1);
                if (Tableros[2, 0].estado == 2 && Tableros[0, 2].estado == 2 && Tableros[1, 1].estado == 0)
                    p = new Point(1, 1);

                if (Tableros[2, 2].estado == 2 && Tableros[0, 2].estado == 2 && Tableros[1, 2].estado == 0)
                    p = new Point(1, 2);

                //Medios
                if (Tableros[1, 0].estado == 2 && Tableros[1, 2].estado == 2 && Tableros[1, 1].estado == 0)
                    p = new Point(1, 1);
                if (Tableros[0, 1].estado == 2 && Tableros[2, 1].estado == 2 && Tableros[1, 1].estado == 0)
                    p = new Point(1, 1);

                if (Tableros[1, 0].estado == 2 && Tableros[0, 0].estado == 2 && Tableros[2, 0].estado == 0)
                    p = new Point(2, 0);
                if (Tableros[1, 0].estado == 2 && Tableros[2, 0].estado == 2 && Tableros[0, 0].estado == 0)
                    p = new Point(0, 0);

                if (Tableros[0, 1].estado == 2 && Tableros[0, 0].estado == 2 && Tableros[0, 2].estado == 0)
                    p = new Point(0, 2);
                if (Tableros[0, 1].estado == 2 && Tableros[0, 2].estado == 2 && Tableros[0, 0].estado == 0)
                    p = new Point(0, 0);
                if (Tableros[0, 1].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[2, 1].estado == 0)
                    p = new Point(2, 1);

                if (Tableros[1, 2].estado == 2 && Tableros[0, 2].estado == 2 && Tableros[2, 2].estado == 0)
                    p = new Point(2, 2);
                if (Tableros[1, 2].estado == 2 && Tableros[2, 2].estado == 2 && Tableros[0, 2].estado == 0)
                    p = new Point(0, 2);

                if (Tableros[2, 1].estado == 2 && Tableros[2, 0].estado == 2 && Tableros[2, 2].estado == 0)
                    p = new Point(2, 2);
                if (Tableros[2, 1].estado == 2 && Tableros[2, 2].estado == 2 && Tableros[2, 0].estado == 0)
                    p = new Point(2, 0);
                if (Tableros[2, 1].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[0, 1].estado == 0)
                    p = new Point(0, 1);

                //Centro
                if (Tableros[0, 0].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[2, 2].estado == 0)
                    p = new Point(2, 2);
                if (Tableros[0, 1].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[2, 1].estado == 0)
                    p = new Point(2, 1);
                if (Tableros[0, 2].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[2, 0].estado == 0)
                    p = new Point(2, 0);
                if (Tableros[1, 0].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[1, 2].estado == 0)
                    p = new Point(1, 2);
                if (Tableros[1, 2].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[1, 0].estado == 0)
                    p = new Point(1, 0);
                if (Tableros[2, 0].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[0, 2].estado == 0)
                    p = new Point(0, 2);
                if (Tableros[2, 1].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[0, 1].estado == 0)
                    p = new Point(0, 1);
                if (Tableros[2, 2].estado == 2 && Tableros[1, 1].estado == 2 && Tableros[0, 0].estado == 0)
                    p = new Point(0, 0);
            }
            else
            {
                //Bloqueos//
                //Esquinas
                if (Tableros[0, 0].estado == 1 && Tableros[0, 2].estado == 1 && Tableros[0, 1].estado == 0)
                    p = new Point(0, 1);
                if (Tableros[0, 0].estado == 1 && Tableros[2, 0].estado == 1 && Tableros[1, 0].estado == 0)
                    p = new Point(1, 0);
                if (Tableros[0, 0].estado == 1 && Tableros[2, 2].estado == 1 && Tableros[1, 1].estado == 0)
                    p = new Point(1, 1);

                if (Tableros[2, 0].estado == 1 && Tableros[2, 2].estado == 1 && Tableros[2, 1].estado == 0)
                    p = new Point(2, 1);
                if (Tableros[2, 0].estado == 1 && Tableros[0, 2].estado == 1 && Tableros[1, 1].estado == 0)
                    p = new Point(1, 1);

                if (Tableros[2, 2].estado == 1 && Tableros[0, 2].estado == 1 && Tableros[1, 2].estado == 0)
                    p = new Point(1, 2);

                //Medios
                if (Tableros[1, 0].estado == 1 && Tableros[1, 2].estado == 1 && Tableros[1, 1].estado == 0)
                    p = new Point(1, 1);
                if (Tableros[0, 1].estado == 1 && Tableros[2, 1].estado == 1 && Tableros[1, 1].estado == 0)
                    p = new Point(1, 1);

                if (Tableros[1, 0].estado == 1 && Tableros[0, 0].estado == 1 && Tableros[2, 0].estado == 0)
                    p = new Point(2, 0);
                if (Tableros[1, 0].estado == 1 && Tableros[2, 0].estado == 1 && Tableros[0, 0].estado == 0)
                    p = new Point(0, 0);

                if (Tableros[0, 1].estado == 1 && Tableros[0, 0].estado == 1 && Tableros[0, 2].estado == 0)
                    p = new Point(0, 2);
                if (Tableros[0, 1].estado == 1 && Tableros[0, 2].estado == 1 && Tableros[0, 0].estado == 0)
                    p = new Point(0, 0);
                if (Tableros[0, 1].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[2, 1].estado == 0)
                    p = new Point(2, 1);

                if (Tableros[1, 2].estado == 1 && Tableros[0, 2].estado == 1 && Tableros[2, 2].estado == 0)
                    p = new Point(2, 2);
                if (Tableros[1, 2].estado == 1 && Tableros[2, 2].estado == 1 && Tableros[0, 2].estado == 0)
                    p = new Point(0, 2);

                if (Tableros[2, 1].estado == 1 && Tableros[2, 0].estado == 1 && Tableros[2, 2].estado == 0)
                    p = new Point(2, 2);
                if (Tableros[2, 1].estado == 1 && Tableros[2, 2].estado == 1 && Tableros[2, 0].estado == 0)
                    p = new Point(2, 0);
                if (Tableros[2, 1].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[0, 1].estado == 0)
                    p = new Point(0, 1);

                //Centro
                if (Tableros[0, 0].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[2, 2].estado == 0)
                    p = new Point(2, 2);
                if (Tableros[0, 1].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[2, 1].estado == 0)
                    p = new Point(2, 1);
                if (Tableros[0, 2].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[2, 0].estado == 0)
                    p = new Point(2, 0);
                if (Tableros[1, 0].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[1, 2].estado == 0)
                    p = new Point(1, 2);
                if (Tableros[1, 2].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[1, 0].estado == 0)
                    p = new Point(1, 0);
                if (Tableros[2, 0].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[0, 2].estado == 0)
                    p = new Point(0, 2);
                if (Tableros[2, 1].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[0, 1].estado == 0)
                    p = new Point(0, 1);
                if (Tableros[2, 2].estado == 1 && Tableros[1, 1].estado == 1 && Tableros[0, 0].estado == 0)
                    p = new Point(0, 0);

            }
            if (p.X == -1 && p.Y == -1)
                return TableroAleatorio();
            else
                return Tableros[p.X,p.Y];
        }

        public Tablero TableroConvenienteFinal()
        {
            List<Tablero> ListaConveniente = new List<Tablero>();
            Point p;
            Tablero aux = TableroAleatorio();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        p = RevisarSiguienteTurno(Tableros[i, j], 1);
                        if (p.X != -1 && p.Y != -1 && (Tableros[i, j].estado == 0))
                            ListaConveniente.Add(Tableros[i, j]);
                    }
                }
                if (ListaConveniente.Count != 0)
                    aux = ListaConveniente[r.Next(0, ListaConveniente.Count)];
                return aux;
        }

        public Point TableroConvenienteFuturo(Tablero T)
        {
            List<Point> ListaConveniente = new List<Point>();
            Point p;
            p = RevisarSiguienteTurno(T, 1);
            /*if (p.X == -1 && p.Y == -1)
                p = RevisarSiguienteTurno(T, 2);*/
            if (p.X == -1 && p.Y == -1)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        p = RevisarSiguienteTurno(Tableros[i, j], 2);
                        if (p.X == -1 && p.Y == -1 && (T.Matrix[i, j].state == 0))
                            ListaConveniente.Add(new Point(i, j));
                    }
                }
                if (ListaConveniente.Count != 0)
                    p = ListaConveniente[r.Next(0, ListaConveniente.Count)];
                else
                    p = new Point(-2, -2);
            }
            return p;
        }

        [DllImport("user32.dll")]
        public static extern void mouse_event
            (MouseEventType dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        public enum MouseEventType : int
        {
            LeftDown = 0x02,
            LeftUp = 0x04,
            RightDown = 0x08,
            RightUp = 0x10
        }

       public void DoMouseClick(int x, int y)
       {
           Cursor.Position = new Point(0, 0);
           //Codigo de Carlos
           p = new Point((this.Location.X + pbCanvas.Location.X+x),(this.Location.Y + pbCanvas.Location.Y+y));

           Cursor.Position = new Point(p.X,p.Y);
           mouse_event(MouseEventType.LeftDown, 0, 0, 0, 0);
           mouse_event(MouseEventType.LeftUp, 0, 0, 0, 0);
          
       }

    }
}

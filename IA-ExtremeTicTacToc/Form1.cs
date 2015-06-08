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
                            if ((e.X >= Tableros[i, j].Matrix[k, l].Coordinates.X && e.X < Tableros[i, j].Matrix[k, l].Coordinates.X + 60) && (e.Y >= Tableros[i, j].Matrix[k, l].Coordinates.Y && e.Y < Tableros[i, j].Matrix[k, l].Coordinates.Y + 60) && PrimerMov == false && Tableros[i, j].estado == 0)
                            {
                                if (!turno)
                                    g.DrawEllipse(new Pen(Color.Red, 10), Tableros[i, j].Matrix[k, l].Coordinates.X, Tableros[i, j].Matrix[k, l].Coordinates.Y, 60, 60);
                                else
                                {
                                    g.DrawLine(new Pen(Color.Red, 10), Tableros[i, j].Matrix[k, l].Coordinates.X, Tableros[i, j].Matrix[k, l].Coordinates.Y, Tableros[i, j].Matrix[k, l].Coordinates.X + 60, Tableros[i, j].Matrix[k, l].Coordinates.Y + 60);
                                    g.DrawLine(new Pen(Color.Red, 10), Tableros[i, j].Matrix[k, l].Coordinates.X + 60, Tableros[i, j].Matrix[k, l].Coordinates.Y, Tableros[i, j].Matrix[k, l].Coordinates.X, Tableros[i, j].Matrix[k, l].Coordinates.Y + 60);
                                }
                                turno = !turno ? true : false;
                                PrimerMov = true;
                                Tableros[i, j].Matrix[k, l].state =turno? 1:2;
                                PerX = k;
                                PerY = l;
                                tableroActual.X = i;
                                tableroActual.Y = j;
                                RevisarEstadosTableroParcial();
                                if (Tableros[PerX, PerY].estado == 1 || Tableros[PerX, PerY].estado == 2)
                                {
                                    PrimerMov = false;
                                }
                                label2.Text = !turno ? "Turno de Jugador 1" : "Turno de Jugador 2";
                            }
                            else if ((e.X >= Tableros[PerX, PerY].Matrix[k, l].Coordinates.X && e.X < Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60) && (e.Y >= Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y && e.Y < Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60) && PrimerMov == true)
                            {
                                
                                if (/*turno == false &&*/ Tableros[PerX, PerY].Matrix[k, l].state == 0 && Tableros[PerX,PerY].estado == 0&&PrimerMov==true)
                                {
                                    if (!turno)
                                        g.DrawEllipse(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, 60, 60);
                                    else
                                    {
                                        g.DrawLine(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60);
                                        g.DrawLine(new Pen(Color.Red, 10), Tableros[PerX, PerY].Matrix[k, l].Coordinates.X + 60, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y, Tableros[PerX, PerY].Matrix[k, l].Coordinates.X, Tableros[PerX, PerY].Matrix[k, l].Coordinates.Y + 60);
                                    }

                                    turno = !turno?true:false;
                                    Tableros[PerX, PerY].Matrix[k, l].state = turno ? 1 : 2;
                                    tableroActual.X = PerX;
                                    tableroActual.Y = PerY;
                                    PerX = k;
                                    PerY = l;
                                    RevisarEstadosTableroParcial();
                                    if (Tableros[PerX, PerY].estado == 1 || Tableros[PerX, PerY].estado == 2)
                                    {
                                        PrimerMov = false;
                                    }
                                    label2.Text = !turno ? "Turno de Jugador 1" : "Turno de Jugador 2";
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
            if (turno)
                MovimientoComputadora();
        }

        public void MovimientoComputadora()
        {
            Tablero T = Tableros[tableroActual.X, tableroActual.Y];
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
                    DificultadNormal(TableroAleatorio());
                }
                else
                {
                    DificultadNormal(T);
                }
            }
            if (cbxDificultad.SelectedItem == "Dificil")
            {
                if (!PrimerMov)
                {
                    DificultadDificil(TableroConveniente());
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

        public void FinJuego()
        {
            MessageBox.Show("Gana " + (turno==false?"Computadora":"Jugador"));
        }

        public void DificultadFacil(Tablero T)
        {
            Random r = new Random();
            int X, Y;
            X = r.Next(0, 3);
            Y = r.Next(0, 3);
            Celda C = T.Matrix[X,Y];
            DoMouseClick(C.Coordinates.X+10, C.Coordinates.Y+10);
        }
        public void DificultadNormal(Tablero T)
        {

        }
        public void DificultadDificil(Tablero T)
        {

        }

        public Tablero TableroAleatorio()
        {
            Random r = new Random();
            int X, Y;
            X = r.Next(0, 3);
            Y = r.Next(0, 3);
            return Tableros[X, Y];
        }

        //Dificil
        public Tablero TableroConveniente()
        {
            return new Tablero();
        }

        public Tablero TableroConvenienteFuturo()
        {
            return new Tablero();
        }

       [DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
       public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

       private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
       private const uint MOUSEEVENTF_LEFTUP = 0x04;
       private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
       private const uint MOUSEEVENTF_RIGHTUP = 0x10;

       public void DoMouseClick(int x, int y)
       {
          //Call the imported function with the cursor's current position
           int X = Convert.ToInt16(x);//set x position 
           int Y = Convert.ToInt16(y);//set y position 
           Cursor.Position = new Point(X, Y);
           mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//make left button down
           mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//make left button up
       }

    }
}

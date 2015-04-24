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
    }
}

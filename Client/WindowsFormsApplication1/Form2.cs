using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public int x = 100;
        public int y = 260;
        public int contador=60;
        public string username;
        bool temps_acabat;
        Socket server;
        List<string> LlistaJugadors = new List<string>();
        List<PictureBox> misPics = new List<PictureBox>();
        int numPics=0;
        int x_pic;
        int y_pic;
        string nom_pic;
        List<Label> misLabels = new List<Label>();
        int partidagen;

     
        public Form2(Socket server)
        {
            this.server = server;
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            this.contadorlbl.Text = Convert.ToInt32(contador).ToString();
            this.timer2.Enabled = true;
            
            
            
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        public bool Return_TempsAcabat()
        {
            return temps_acabat;
        }
        public void Set_X_Y(int x_pic, int y_pic, string nom_pic)
        {
            this.x_pic = x_pic;
            this.y_pic = y_pic;
            this.nom_pic = nom_pic;
            int i = 0;
            Pintar_pic(this.nom_pic, this.x_pic, this.y_pic);
            this.Invalidate();
        }
        public int Return_x()
        {
            return x;
        }
        public int Return_y()
        {
            return y;
        }


        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left)
            {
                    x = x - 10;
                if (x <= 40)
                {
                    x = 40;
                }
                else if (x<=435 && (y>270 && y<=490))
                {
                x = 435;
                }
                else if ((x<=400 && x > 360)&& (y >= 490 && y < 530))
                {
                    x = 400;
                }
                else if ((x >= 100 && x < 170) && (y >= 490 && y <= 615))
                {
                    x = 170;
                }



                else if ((x <= 270 && x > 200) && (y >= 150 && y < 250))
                {
                    x = 270;
                }
                else if ((x <= 170 && x > 130) && (y >= 120 && y < 210))
                {
                    x = 170;
                }
                else if ((x <= 75) && (y >= 120 && y < 210))
                {
                    x = 75;
                }

            }
            else if (e.KeyData == Keys.Right)
            {
                x = x + 10;
                if (x >= 707 - 50)
                {
                        x = 707 - 50;
                }
                else if (x>=525&& y<=300)
                {
                    x = 525;
                }
                else if (x >= 525 && (y > 400 && y <= 590))
                {
                    x = 525;
                }
                   
                else if ((x >= 100 && x <170) && (y >= 490 && y <= 615))
                {
                    x = 100;
                }
                else if ((x >= 360 && x < 400) && (y >= 490 && y < 530))
                {
                    x = 360;
                }
                else if ((x <= 270 && x >= 190) && (y >= 120 && y < 250))
                {
                    x = 190;
                }
                else if ((x <= 170 && x > 130) && (y >= 120 && y < 210))
                {
                    x = 130;
                }
            }
            else if (e.KeyData == Keys.Up)
            {
                y = y -10;
                if (y<=150 && (x>=270 && x < 535))
                {
                    y  = 150;
                }
                else if (y <= 120 && (x < 200 && x > 40))
                {
                    y = 120;
                }
                else if ((y <= 500 && y > 270) && (x >=40  && x < 435))
                {
                    y=500;
                }
                else if ((y <= 615 && y > 270) && (x > 100 && x < 170))
                {
                    y = 620;
                }
                else if ((y <= 590 && y > 400) && (x <=707-50 && x > 525))
                {
                    y = 590;
                }
                else if ((y <= 310 ) && (x <= 707 - 50 && x > 525))
                {
                    y = 310;
                }
                else if ((y <= 520 && y > 270) && (x < 400 && x >= 370))
                {
                    y = 530;
                }
                else if ((y <=250) && (x < 270 && x > 190))
                {
                    y = 250;
                }
                else if ((y < 210) && (x < 170 && x > 130))
                {
                    y = 210;
                }
                else if ((y < 210) && (x < 75 && x >= 40))
                {
                    y = 210;
                }
            }
            else if (e.KeyData == Keys.Down)
            {
                y = y + 10;
                if (y >= 740 - 95)
                {
                    y = 740-95;
                }
                else if ((y>=270 && y <500) && x<435)
                {
                    y = 270 ;
                }
                else if ((y >= 390) && (x < 707-40 && x>525))
                {
                    y = 390;
                }

            }

           
            string missatge_posicio = "11/" + x + "/" + y + "/" + partidagen.ToString();
            if (temps_acabat == false)
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge_posicio);
                server.Send(msg);
            }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
           
            e.Graphics.FillRectangle(Brushes.Red, x, y, 10, 10);
            
            
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            int i = 0;
            misPics = new List<PictureBox>();
            misLabels = new List<Label>();
            while (i < LlistaJugadors.Count())
            {
               
                
                PictureBox p = new PictureBox();
                p.Width = 10;
                p.Height = 10;
                p.ClientSize = new Size(10, 10);
                this.Controls.Add(p);
                misPics.Add(p);
                Label l = new Label();
                
                l.Font = new Font("Calibri", 10);
                l.AutoSize = true;

                misLabels.Add(l);
                
                i = i + 1;
            }
            Pintar(LlistaJugadors, x, y);
        }

        private void sortirDeLaPartidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            contador = contador - 1;
            this.contadorlbl.Text = contador.ToString();
            if (contador == 0)
            {
                this.timer2.Enabled = false;
                MessageBox.Show("S'ha acabat el temps!");
                this.temps_acabat = true;
                this.Close();
            }

        }
        public void SetLlistaJugadors(List<string> LlistaJugadors)
        {
            this.LlistaJugadors = LlistaJugadors;
        }
        public void SetUsername(string username)
        {
            this.username = username;
        }
        public void Set_Partida_Gen(int partida_gen)
        {
            this.partidagen = partida_gen;
        }
        private void Pintar_pic(string nom,int x, int y)
        {
            int i = 0;
            while (i < LlistaJugadors.Count())
            {
                if (LlistaJugadors[i] == this.nom_pic)
                {
                    misPics[i].Location = new Point(x , y);
                    misPics[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    Bitmap imatge = new Bitmap("circuloamarillo.jpg");
                    misPics[i].Image = (Image)imatge;

                    misLabels[i].Location = new Point(x , y - 15);
                    misLabels[i].Text = LlistaJugadors[i];
                    misLabels[i].BackColor = Color.Transparent;
                    this.Controls.Add(misLabels[i]);

                    this.Controls.Add(misPics[i]);
                    numPics++;
                }
                i = i + 1;
            }
        }
        private void Pintar(List<string> LlistaJugadors,int x, int y)
        {
            int i = 0;
            int k = 15;

            while (i < LlistaJugadors.Count())
            {
                if (LlistaJugadors[i] != username)
                {
                    misPics[i].Location = new Point(x+k , y);
                    misPics[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    Bitmap imatge = new Bitmap("circuloamarillo.jpg");
                    misPics[i].Image = (Image)imatge;
                    this.Controls.Add(misPics[i]);
                    numPics++;

                    misLabels[i].Location = new Point(x + k, y-15);
                    misLabels[i].Text = LlistaJugadors[i];
                    misLabels[i].BackColor = Color.Transparent;
                    this.Controls.Add(misLabels[i]);


                    
                    k = k + 15;
                }
                i = i + 1;
            }
        }

    }
}

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
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public int x = 100;
        public int y = 260;
        public int contador=500;
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
        delegate void DelegadoPer_DetectarPista(int x, int y);

        int partidagen;
        string nom_desconectat;

        int num_pistes;
        int num_MaxPistes;
        int final_x,final_y;
        PictureBox pistaOu;
        bool pistaOu_On;
        int temps_Ou = 500; //20s per agafar l'ou
        bool temps_Ou_Acabat = false;
        bool PartidaFinal;
        int temps_partida_final = 300; //30s per agafar els jugadors
        bool temps_final_acabat;
        bool impostor;
        int contador_pillats = 0;
        string guanyador;
        bool avisat;
        string llibre;
        string enigma;
        bool found1;
        bool found2;
        bool Agafat;
        bool Pista_Morse_2;
        int num_jugadors;
        Form formInv;
        List<string> LlistaPistes = new List<string>();
        string[] Nom_Posicio = new string[12];

        bool pistamorse = false;
        bool pistamorseutilitzada = false;
        FormMorse formMorse;
        FormMorseSol formMorseSol;

        SoundPlayer player;

        string infopartida;
     
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
        public void Tancar()
        {
            Close();
        }
        public bool Return_TempsAcabat()
        {
            return temps_acabat;
        }
        public bool Return_Agafat()
        {
            return Agafat;
        }

        public string Return_InfoPartida()
        {
            return infopartida;
        }
        //Rebem el nom d'algu que es desconecta i el borram de la llista de jugadors
        public void Set_NomDesconectat(string nom_desconectat)
        {
            this.nom_desconectat = nom_desconectat;
            BorrarPic(nom_desconectat);
            LlistaJugadors.Remove(nom_desconectat);
            this.Invalidate();

        }
        //Actualitzam el numero de pistes aconseguides
        public void Set_numPistes(int num_pistes, int totalPistes)
        {
            this.num_pistes = num_pistes;
            this.num_MaxPistes = totalPistes;
        }

        public void Set_impostor(bool impostor)
        {
            this.impostor = impostor;
        }
        //Pintam el ous i iniciam el contador 
        public void Set_pistaFinal(int final_x, int final_y)
        {
            pistaOu_On = true;
            this.final_x = final_x;
            this.final_y = final_y;
            pistaOu = new PictureBox();

            pistaOu.Location = new Point(final_x, final_y);
            pistaOu.SizeMode = PictureBoxSizeMode.StretchImage;

            Bitmap imatge = new Bitmap("goldenegg.png");
            pistaOu.Image = (Image)imatge;
            pistaOu.BackColor = Color.Transparent;
            pistaOu.Width = 10;
            pistaOu.Height = 20;
            pistaOu.ClientSize = new Size(10, 20);
            pistaOu.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pistaOu);

            timer3.Enabled = true;
            contador = contador + temps_Ou;

        }
        //Rebem quan un jugador ha estat pillat 
        public void Set_Agafat(bool agafat)
        {
            this.Agafat = agafat;
            //Si ho rebem i no hem estat pillats vol dir que som l'impostor i som els responsables de guardar la partida 
            if (agafat)
            {
                int duracio = 500 - Convert.ToInt32(contadorlbl.Text);
                infopartida = "/" + partidagen + "/"+LlistaJugadors[0]+"/" + DateTime.Now.ToString("dd-MM") + "/" + DateTime.Now.ToString("hh:mm:ss") + "/" + duracio;
            }
            MessageBox.Show("Agafat!, no has aconseguit sortir del castell :(");
            Close();
        }
        //Tots el jugadors han aconseguit l'ou daurat i comença la prova final
        public void Set_PartidaFinal(bool b)
        {
            this.PartidaFinal = b;
            timer4.Enabled = true;
            contador = contador + temps_partida_final;
            LabelNotificacio.Text = "Tots els jugadors han aconseguit l'ou daurat, però" + "\n" + " només un podrà sortir amb el seu";
            if (username != Nom_Posicio[0])
            {
                MessageBox.Show("Corr, que no t'agafin");
            }
        }
        //Actualitzam la posicío del jugador que s'ha mogut
        public void Set_X_Y(int x_pic, int y_pic, string nom_pic)
        {
            this.x_pic = x_pic;
            this.y_pic = y_pic;
            this.nom_pic = nom_pic;
            int i = 0;

            Pintar_pic(this.nom_pic, this.x_pic, this.y_pic);
            while (i < Nom_Posicio.Length)
            {
                if (this.nom_pic == Nom_Posicio[i] )
                {
                    Nom_Posicio[i + 1] = Convert.ToString(x_pic);
                    Nom_Posicio[i + 2] = Convert.ToString(y_pic);
                }
                i = i + 3;
            }
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
        //Cada cop que clickem una fletxa del teclat fem els moviments i elss enviem a tothom
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (Agafat == false)
            {
                if (e.KeyData == Keys.Left)
                {
                    x = x - 10;
                    if (x <= 40)
                    {
                        x = 40;
                    }
                    else if (x <= 435 && (y > 270 && y <= 490))
                    {
                        x = 435;
                    }
                    else if ((x <= 400 && x > 360) && (y >= 490 && y < 530))
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
                    else if (x >= 525 && y <= 300)
                    {
                        x = 525;
                    }
                    else if (x >= 525 && (y > 400 && y <= 590))
                    {
                        x = 525;
                    }

                    else if ((x >= 100 && x < 170) && (y >= 490 && y <= 615))
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
                    y = y - 10;
                    if (y <= 150 && (x >= 270 && x < 535))
                    {
                        y = 150;
                    }
                    else if (y <= 120 && (x < 200 && x > 40))
                    {
                        y = 120;
                    }
                    else if ((y <= 500 && y > 270) && (x >= 40 && x < 435))
                    {
                        y = 500;
                    }
                    else if ((y <= 615 && y > 270) && (x > 100 && x < 170))
                    {
                        y = 620;
                    }
                    else if ((y <= 590 && y > 400) && (x <= 707 - 50 && x > 525))
                    {
                        y = 590;
                    }
                    else if ((y <= 310) && (x <= 707 - 50 && x > 525))
                    {
                        y = 310;
                    }
                    else if ((y <= 520 && y > 270) && (x < 400 && x >= 370))
                    {
                        y = 530;
                    }
                    else if ((y <= 250) && (x < 270 && x > 190))
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
                        y = 740 - 95;
                    }
                    else if ((y >= 270 && y < 500) && x < 435)
                    {
                        y = 270;
                    }
                    else if ((y >= 390) && (x < 707 - 40 && x > 525))
                    {
                        y = 390;
                    }

                }

                DetectarObjecte(x, y); //detecta si les coordenades en les que estam poden contenir informació

                //Label que et diu la posicio en la que et trobes
                LabelXY.BackColor = Color.Transparent;
                LabelXY.AutoSize = true;
                LabelXY.Text = "x " + x.ToString() + "\n" + "y " + y.ToString();

                string missatge_posicio = "11/" + x + "/" + y + "/" + partidagen.ToString();
                if (temps_acabat == false)
                {
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge_posicio);
                    server.Send(msg);
                }
            }
            
        }

        //Pintem el nostre jugador
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
           
            e.Graphics.FillRectangle(Brushes.Red, x, y, 10, 10);
            
            
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            //pintam els picture box en la seva posició inicial
            int i = 0;
            misPics = new List<PictureBox>();
            misLabels = new List<Label>();
            while (i < LlistaJugadors.Count())
            {
               
                
                PictureBox p = new PictureBox();
                p.Width = 20;
                p.Height = 20;
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
            Afegir_Coord_inicials(Nom_Posicio, x, y);
        }
        //
        private void sortirDeLaPartidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No s'ha acabt el temps, però hem sortit de la partida");
            this.Close();
        }
        //Anam actualitzant les coordenades dins el vector per poder compara-les amb les de l'impostor
        private void Afegir_Coord_inicials(string[] nom_posicio, int x, int y)
        {
            int j = 0;
            int k = 15;
            while (j < nom_posicio.Length)
            {
                if (nom_posicio[j] == username)
                {
                    nom_posicio[j + 1] = Convert.ToString(x);
                    nom_posicio[j + 2] = Convert.ToString(y);
                }
                else if (nom_posicio[j] != null)
                {
                    nom_posicio[j + 1] = Convert.ToString(x + k);
                    nom_posicio[j + 2] = Convert.ToString(y);
                    k = k + 15;
                }
                j = j + 3;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            contador = contador - 1;
            this.contadorlbl.Text = contador.ToString();
            //Actualitzem el númerode pistes obtingudes per tots els jugadors
            if (pistaOu_On == false && PartidaFinal == false)
            {
                if (num_pistes == 0)
                    this.LabelNotificacio.Text = "Si vols ser el seleccionat per sortir amb l'ou daurat \n aprendre nous coneixements necessitaràs";
                else
                    LabelNotificacio.Text = "Pistes recol·lectades entre tots els jugadors " + num_pistes;
            }

            if (contador == 0)
            {
                this.timer2.Enabled = false;
                this.temps_acabat = true;
                Finalitzar_Partida(false);
            }

        }

        //Rebem la llista de jugadors des del form1
        public void SetLlistaJugadors(List<string> LlistaJugadors)
        {
            this.LlistaJugadors = LlistaJugadors;
            num_jugadors = LlistaJugadors.Count();
        }
        //Rebem la llista de noms amb posició
        public void Set_Nom_posicio(string[] nom_posicio)
        {
            this.Nom_Posicio = nom_posicio;
            
        }
        //Rebem el nostre usuari des del form1
        public void SetUsername(string username)
        {
            this.username = username;
        }
        //Rebem la id de la partida que jugam
        public void Set_Partida_Gen(int partida_gen)
        {
            this.partidagen = partida_gen;
        }
        //Actualitzam la posició del pics
        private void Pintar_pic(string nom,int x, int y)
        {
            int i = 0;
            while (i < LlistaJugadors.Count())
            {
                if (LlistaJugadors[i] == this.nom_pic)
                {
                    misPics[i].Location = new Point(x , y);
                    misPics[i].SizeMode = PictureBoxSizeMode.StretchImage;
                   
                    Bitmap imatge = new Bitmap("buffon.png");
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
        //PIntam el pics la primera vegada
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
                    Bitmap imatge = new Bitmap("buffon.png");
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
        //Funció que utilitzem per borrar un pic
        private void BorrarPic(string nom)
        {
            int i = 0;
            while (i < LlistaJugadors.Count())
            {
                if (LlistaJugadors[i] == nom)
                {

                    this.Controls.Remove(misPics[i]);
                    misPics.Remove(misPics[i]);
                    numPics--;


                    this.Controls.Remove(misLabels[i]);
                    misLabels.Remove(misLabels[i]);
                }
                i = i + 1;
            }
        }
        //Funció per borrar tots els jugadors
        private void BorrarJugador(string nom)
        {
            int i = 0;
            while (i < LlistaJugadors.Count())
            {
                if (LlistaJugadors[i] == nom)
                {

                    LlistaJugadors.Remove(LlistaJugadors[i]);              

                }
                i = i + 1;
            }
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        //Analitzam si la posicio en la que esteim conte informació
        private void DetectarObjecte(int x, int y)
        {
            bool found = false;
            if ((x > 490 && x < 510) && (y == 150) && (found1 == false))//Hem trobat la pista de la llibreria
            {
                llibre = "Per poder llegir s'ha de dormir bé";
                found1 = true;
                LlistaPistes.Add(llibre);
                found = true;
                num_pistes = num_pistes + 1;
            }
            if ((x > 120 && x < 140) && (y == 170) && (found2 == false) && (found1 == true))//Hem trobat la ista del llit
            {
                enigma = "Si l'ou daurat vols aconseguir morse hauràs de traduïr i "+"\n"+" per aconseguir-ho un llibre obert hauràs de llegir";
                found2 = true;
                LlistaPistes.Add(enigma);
                found = true;
                num_pistes = num_pistes + 1;
            }

            if ((y >= 520) && (y <= 540) && (x == 170))//Hem trobat la pista del morse
            {
                if (pistamorseutilitzada == true) //Si ja hem resolt, no podem tornar a resoldre
                    MessageBox.Show("Aquesta pista ja ha estat utilitzada!");
                else
                {
                    if (pistamorse == false)//Aquí dins entra el primer que agafi la pista, que li sortirá l'audio
                    {
                        pistamorse = true;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("16/" + partidagen + "/1/true");
                        server.Send(msg);
                        formMorse = new FormMorse();
                        formMorse.ShowDialog();
                        pistamorse = formMorse.GetPistaMorse();
                        if (pistamorse == true)
                        {
                            byte[] msg2 = System.Text.Encoding.ASCII.GetBytes("16/" + partidagen + "/1/true");
                            server.Send(msg2);
                        }
                        else
                        {
                            byte[] msg2 = System.Text.Encoding.ASCII.GetBytes("16/" + partidagen + "/1/false");
                            server.Send(msg2);
                        }

                    }
                    else if (Pista_Morse_2 == false) //Aquí entra el segon que agafi la pista, que li sortirà per escriure la pista
                    {
                        formMorseSol = new FormMorseSol();
                        Pista_Morse_2 = true;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("16/" + partidagen + "/2/true");
                        server.Send(msg);
                        formMorseSol.ShowDialog();
                        pistamorseutilitzada = formMorseSol.GetBooleanoMorse();
                        byte[] msg2 = System.Text.Encoding.ASCII.GetBytes("17/" + partidagen);
                        server.Send(msg2);
                        if (pistamorseutilitzada == true)
                        {
                            num_pistes = num_pistes + LlistaJugadors.Count;
                            found = true;
                        }

                    }
                    else //Els que agafin la pista després reben aquest missatge
                    {
                        MessageBox.Show("Aquesta pista ja ha estat oberta. Ajuda'l a resoldre l'enigma!");
                    }
                }

            }
            //si hem trobat una pista ho notificam als altres jugadors
            if (found == true)
            {
                
                Invalidate();
                //Si hem trobat totes les pistes comença la cerca de l'ou daurat
                if (num_pistes - (LlistaJugadors.Count-1) == num_MaxPistes)
                {
                    pistaOu_On = true;
                    LabelNotificacio.Text = "Per trobar la pista final necessitaras " + "\n" + "cooperar amb els teus companys";
                    Invalidate();
                    
                }
                string missatge = "14/"+ partidagen;//indicam que hem trobat una nova pista
                if (temps_acabat == false)
                {
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
                    server.Send(msg);
                }
            }
            //Rutina per agafar l'ou
            if( temps_Ou_Acabat == false && pistaOu_On == true)
            {
                if ((final_x - 10 <= x && final_x + 10 >= x) && (final_y - 10 <= y && final_y + 10 >= y))
                    {
                        pistaOu_On = false; //ja l'hem agafat i per tant la pista de l'ou deixa d'estar activa
                        string missatge = "18/" + partidagen;//ou agafat en el temps establert
                        timer3.Enabled = false;
                        pistaOu.Image = null;
                        
                        if (temps_acabat == false)
                        {
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
                            server.Send(msg);
                        }

                    }
               
            }
         
            //Actualitzam la posició dels jugadors durant el pillapilla
            int i = 0;
            while (i < Nom_Posicio.Length)
            {
                if (this.username == Nom_Posicio[i])
                {
                    Nom_Posicio[i + 1] = Convert.ToString(x);
                    Nom_Posicio[i + 2] = Convert.ToString(y);
                }
                i = i + 3;
            }
            //entram si som l'impostor i estam jugant el pillapilla
            if (temps_final_acabat == false && PartidaFinal == true && impostor == true)
            {
                int j = 3;
                if (avisat == false)//Avisar que ets l'impostor una vegada
                {
                    MessageBox.Show("Ets l'impostor!");
                    avisat = true;
                }
                while (j < Nom_Posicio.Length )
                {
                    //Compara les posicions de l'impostor amb els altres jugadors
                    if (((Convert.ToInt32(Nom_Posicio[j + 1])) <= x+5 && (Convert.ToInt32(Nom_Posicio[j + 1])) >= x-5) && ((Convert.ToInt32(Nom_Posicio[j + 2])) <= y+5 && (Convert.ToInt32(Nom_Posicio[j + 2]) ) >= y-5))
                    {
                        //L'impostor pilla un jugador
                        if (contador_pillats <= LlistaJugadors.Count() - 1)
                        {
                            contador_pillats = contador_pillats + 1;
                            BorrarPic(Nom_Posicio[j]);
                            BorrarJugador(Nom_Posicio[j]);
                            MatarJugador(j);
                            //notificam al pillat que ha estat pillat
                            string missatge = "19/" + partidagen + "/" + Nom_Posicio[j];
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(missatge);
                            server.Send(msg);
                            if (contador_pillats == 1)
                            {
                                //L'impostor ha agafat els jugadors i ha guanyat la partida
                                guanyador = Nom_Posicio[0];
                                //Notificam als altres jugadors que l'impostor ha guanyat i donam per acabada la partida
                                string missatge2 = "20/" + partidagen + "/" + guanyador;
                                byte[] msg2 = System.Text.Encoding.ASCII.GetBytes(missatge2);
                                server.Send(msg2);
                                timer4.Enabled = false;
                                MessageBox.Show("Enhorabona! Has aconseguit sortir amb l'ou daurat");//impostor guanya
                                int duracio = 500-Convert.ToInt32(contadorlbl.Text);
                                //Guardam la info de la partida per guardar-la a la BBDD
                                infopartida = "/" + partidagen + "/" + username + "/" + DateTime.Now.ToString("dd-MM") + "/" + DateTime.Now.ToString("hh:mm:ss") + "/" + duracio;
                            }

                        }
                    }
                 
                    j = j + 3;
                }
                
            }
        }
        //Quan matem un jugador, assignem aquestes coordenades
        private void MatarJugador(int j)
        {
            Nom_Posicio[j + 1] = "0";
            Nom_Posicio[j + 2] = "0";
        }

        private void Finalitzar_Partida (bool t)
        {
            if (t == false)
            {
                player = new SoundPlayer("Endthemewav.wav");
                player.Play();
                MessageBox.Show("L'objectiu no ha estat assolit, heu perdut");//quan s'acaba el temps
                player.Stop();
                Close();
            }
      
        }

        private void inventariToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formInv = new FormInventari(LlistaPistes);
            formInv.ShowDialog();
            LabelNotificacio.Text = "";
        }

        public void SetBooleanoMorse(bool booleano)
        {
            this.pistamorse = booleano;
        }

        public void SetBooleanoMorseUtilitzat(bool booleano)
        {
            this.pistamorseutilitzada = booleano;
        }
        public void SetBooleanoMorse2(bool booleano)
        {
            this.Pista_Morse_2= booleano;
        }
        //Timer per agafar els ous daurats
        private void timer3_Tick(object sender, EventArgs e)
        {
            LabelNotificacio.Text = "Temps per restant per agafar els ous:";
            Contador_proves.Text = temps_Ou.ToString();
            if (temps_Ou == 0)
            {
                timer3.Enabled = false;
                temps_Ou_Acabat = true;
                Finalitzar_Partida(false);
            }
            temps_Ou = temps_Ou - 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        //Timer per el pillapilla
        private void timer4_Tick(object sender, EventArgs e)
        {
            Contador_proves.Text = temps_partida_final.ToString();
            if (temps_partida_final == 0)
            {
                timer4.Enabled = false;
                temps_final_acabat = true;
                //el jugador no ha estat pillat i s'ha acabat el temps
                if (Agafat == false && impostor==false)
                {
                    MessageBox.Show("Enhorabona! Has guanyat la partida");
                    Close();
                }
                //l'impostor no ha aconseguit pillar a ningú i s'ha acabt el temps
                else
                {
                    player = new SoundPlayer("Endthemewav.wav");
                    player.Play();
                    MessageBox.Show("Has quedat tancat i la guardia t'ha agafat :(");//impostor perd
                    //player.Stop();
                    int duracio = 500 - Convert.ToInt32(contadorlbl.Text);
                    infopartida = "23/" + partidagen + "/poble/" + DateTime.Now.ToString("dd-MM") + "/" + DateTime.Now.ToString("hh:mm:ss") + "/" + duracio;
                    Close();
                }
            }
            temps_partida_final = temps_partida_final - 1;
        }
       


    }
}

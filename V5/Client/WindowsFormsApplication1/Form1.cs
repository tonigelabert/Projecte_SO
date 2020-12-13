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
    public partial class Form1 : Form
    {
        Socket server;
        Thread Atender;
        public bool LoggedIn = false;
        public bool Connectat = false;
        public string username;
        int port = 50072;//OPCIONS: 50072, 50073, 50074
        string ip = "147.83.117.22";//147.83.117.22,192.168.56.102
        bool tempsacabat = false;
        public int partidagen;
        public int x_anfitrio;
        public int y_anfitrio;
        public bool partida_iniciada = false;
        public bool desconnectat_partida;
        List<string> LlistaJugadors = new List<string>();
        string nom_anfitrio;
        Form2 form2;
        FormXat formxat;
        delegate void DelegadoPerLlistaConectats(string connectat);
        delegate void DelegadoPerLlistaConectats_Clear();
        delegate void DelegadoPerForm2_SetXY(int x, int y, string nom_mov);
        delegate void DelegadoPerForm2_Desconectat(string nom_desc);
        delegate void DelegadoPerFormXat_AfegirMissatge(string missatge);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje;

                switch (codigo)
                {
                    case 1: //respota menor duració
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show("L'ID de la partida de menor duració és:" + mensaje);
                        break;
                    case 2: //resposta retornar contrasenya
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show("La contrasenya és: " + mensaje);
                        break;
                    case 3: //resposta max participacions
                        mensaje = trozos[1].Split('\0')[0];
                        MessageBox.Show("El jugador amb més participacions és: " + mensaje);
                        break;
                    case 4: //resposta login
                        mensaje = trozos[1].Split('\0')[0];
                        if (mensaje == "1")
                        {
                            LoggedIn = true;
                            username = textBox_Nom.Text;
                            this.BackColor = Color.Green;
                            MessageBox.Show("LogIn correcte");
                        }
                        else
                            MessageBox.Show("Error al introduir el nom i la contrasenya");
                        break;
                    case 5: //registrar
                        mensaje = trozos[1].Split('\0')[0];
                        if (mensaje == "Existent")
                            MessageBox.Show("Usuari ja registrat");
                        else
                        {
                            MessageBox.Show("Usuari registrat correctament");
                            this.BackColor = Color.Blue;
                        }
                        break;
                    case 6: //llista connectats
                        //mensaje = trozos[1].Split('\0')[0];
                        //MessageBox.Show(mensaje);
                        //string[] llistatrossos = mensaje.Split('/');
                        //exemple 6/3/nom1/nom2/nom3
                        int numconnectats = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        DelegadoPerLlistaConectats_Clear delegadoClear = new DelegadoPerLlistaConectats_Clear(ClearLlista);
                        llistaconnectat.Invoke(delegadoClear, new object[] {});

                        int j = 2;
                        int i = 0;
                        while (i < numconnectats)
                        {
                            DelegadoPerLlistaConectats delegado =  new DelegadoPerLlistaConectats(PonLlista);
                            llistaconnectat.Invoke(delegado, new object[] { trozos[j].Split('\0')[0] });
                            j++;

                            i = i + 1;
                        }
                        break;
                    case 7://invitació per jugar // CONVIDAT
                        string anfitrio = trozos[1].Split('\0')[0];
                        nom_anfitrio = anfitrio;
                        int partida = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        partidagen = partida;
                        
                        DialogResult dialogResult = MessageBox.Show("Vols jugar amb " + anfitrio + "?", "Invitació a Partida", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes("8/" + anfitrio + "/" + partida.ToString() + "/SI");
                            server.Send(msg);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes("8/" + anfitrio +"/"+partida.ToString()+ "/NO");
                            server.Send(msg);
                        }
                        break;
                    case 8: //L'ANFITRIÓ rep la resposta del convidat
                        string convidat = trozos[1].Split('\0')[0];
                        string resposta = trozos[2].Split('\0')[0];
                        MessageBox.Show(resposta);
                        if (resposta == "SI")
                        {
                            LlistaJugadors.Add(convidat);//Afegim convidats
                            MessageBox.Show("Enhorabona, " + convidat + " vol jugar a la partida!");
                        }
                        else
                        {
                            MessageBox.Show("Ho sentim, " + convidat + " no vol jugar a la partida!");
                        }

                        break;
                    case 9: //CONVIDAT rep la llista de jugadors dins la partida i els afegeix dins la seva llista
                        if (trozos[1].Split('\0')[0] != username)
                        {
                            

                            if (trozos[1].Split('\0')[0] != "")
                            {
                                LlistaJugadors.Add(trozos[1].Split('\0')[0]);

                            }
                            if (trozos[2].Split('\0')[0] != "")
                            {
                                LlistaJugadors.Add(trozos[2].Split('\0')[0]);

                            }
                            if (trozos[3].Split('\0')[0] != "")
                            {
                                LlistaJugadors.Add(trozos[3].Split('\0')[0]);
                            }
                            if (trozos[4].Split('\0')[0] != "")
                            {
                                LlistaJugadors.Add(trozos[4].Split('\0')[0]);

                            }
                            ThreadStart ts = delegate { IniciarForm2(); };
                            ThreadStart ts2 = delegate { IniciarFormxat(); };
                            Thread t = new Thread(ts);
                            Thread t2 = new Thread(ts2);
                            t.Start();
                            t2.Start();
                        } 
                        break;

                    case 11://Els jugadors de la partida reben la notificació que algú ha actualitzat la seva posició 
                        int x = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        int y = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        string nom_moviment = trozos[3].Split('\0')[0];
                        DelegadoPerForm2_SetXY dele = new DelegadoPerForm2_SetXY(SetXY_form2);
                        this.form2.Invoke(dele, new object[] { x, y, nom_moviment });
                        break;

                    case 12:
                        string nom_desconectat = trozos[1].Split('\0')[0];
                        DelegadoPerForm2_Desconectat dele2 = new DelegadoPerForm2_Desconectat(Set_desconectat);
                        this.form2.Invoke(dele2, new object[] {nom_desconectat });

                        break;

                    case 13:
                        string nomemisor = trozos[1].Split('\0')[0];
                        string missatgeenviat = trozos[2].Split('\0')[0];
                        string missatge = nomemisor + ": " + missatgeenviat;
                        DelegadoPerFormXat_AfegirMissatge dele3 = new DelegadoPerFormXat_AfegirMissatge(AfegirMissatge);
                        this.formxat.Invoke(dele3, new object[] { missatge });
                        break;


                }
            }
        }
        public void PonLlista(string connectat)
        {
            llistaconnectat.Items.Add(connectat);
            
        }
        public void ClearLlista()
        {
            llistaconnectat.Items.Clear();
        }

        public void SetXY_form2(int x, int y, string nom_moviment)
        {
            form2.Set_X_Y(x, y, nom_moviment);
        }
        public void Set_desconectat(string nom_desc)
        {
            form2.Set_NomDesconectat(nom_desc);
        }
        public void AfegirMissatge(string missatge)
        {
            formxat.AfegirMissatge(missatge);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string nom = textBox_Nom.Text;
            string mensaje = "0/" + nom;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            //MessageBox.Show(mensaje);
            server.Send(msg);

            // Nos desconectamos
            Atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            Connectat = false;
            convidats.Clear();
            invitacio = null;
            invitacio = "7/";
            server.Close();
        }

        private void Button_Login_Click(object sender, EventArgs e)
        {
            if (!Connectat)
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse(ip);
                IPEndPoint ipep = new IPEndPoint(direc, port);


                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket
                    Connectat = true;

                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }


            }

            LoggedIn = false;
            try
            {
                if (textBox_Nom.Text == "" || textBox_Contrasenya.Text == "")
                {
                    MessageBox.Show("Introdueix el nom i la contrasenya");
                }
                string mensaje = "4/" + textBox_Nom.Text + "/" + textBox_Contrasenya.Text;
                // Enviam al servidor lel nom i la contrasenya per poder fer consultes
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                ThreadStart ts = delegate { AtenderServidor(); };
                Atender = new Thread(ts);
                Atender.Start();


            }
            catch (Exception error)
            {
                MessageBox.Show("Error a l'entrar");
            }


        }

        private void Button_Registrar_Click(object sender, EventArgs e)
        {
            if (!Connectat)
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse(ip);
                IPEndPoint ipep = new IPEndPoint(direc, port);


                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket
                    Connectat = true;

                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
                ThreadStart ts = delegate { AtenderServidor(); };
                Atender = new Thread(ts);
                Atender.Start();


            }
            LoggedIn = false;
            string nom = textBox_nom2.Text;
            string contrasenya = textBox_contrasenya2.Text;
            string paraulaSeguretat = textBox_ParaulaSeguretat2.Text;
            try
            {
                if (nom == "" || contrasenya == "" || paraulaSeguretat == "")
                {
                    MessageBox.Show("Introdueix el nom, la contrasenya  i la paraula de seguretat");
                }
                else
                {
                    string mensaje = "5/" + nom + "/" + contrasenya + "/" + paraulaSeguretat;
                    // Enviam al servidor el nom la contrasenya i la paraula de seguretat
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);


                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al registrar");
            }


        }

        private void Button_Enviar_Click(object sender, EventArgs e)
        {
            if (Partida_min.Checked && LoggedIn)
            {
                string mensaje = "1/";
                // Enviam al servidor la primera consulta
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);


            }
            else if (Retorna_Contrasenya.Checked && LoggedIn)
            {
                string ParaulaSeguretat = textBox_paraulaSeguretat.Text;
                if (ParaulaSeguretat == "")
                    MessageBox.Show("Introdueix la paraula de seguretat");
                else
                {
                    string mensaje = "2/" + ParaulaSeguretat + "/" + username;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);


                }

            }
            else if (Mes_particpacions.Checked && LoggedIn)
            {
                // Enviamos nombre y altura
                string mensaje = "3/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            else
            {
                MessageBox.Show("Registra't per fer una consulta");
            }
        }

        private void llistaconnectat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        List<string> convidats = new List<string>();
        
        string invitacio = "7/";
        private void llistaconnectat_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            int i = 0;
            if ((llistaconnectat.SelectedItem != null) && (llistaconnectat.SelectedItem.ToString() != username))
            {

                string Nom_Convidat = llistaconnectat.SelectedItem.ToString();
                convidats.Add(Nom_Convidat);
                while (i<convidats.Count())
                {
                    invitacio = invitacio + convidats[i] + "/";
                    i++;
                }
                MessageBox.Show(invitacio);

            }
            else if (llistaconnectat.SelectedItem.ToString() == username)
            {
                MessageBox.Show("No et pots convidar a tu mateix!");
            }

        }
        //Boto per convidar
        private void button2_Click(object sender, EventArgs e)
        {

            LlistaJugadors.Add(username);//Afegim L'anfitrió s ls llista
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(invitacio);
            server.Send(msg);
        }
        private void IniciarFormxat()
        {
            formxat = new FormXat(server);
            formxat.SetUsername(username);
            formxat.Set_Partida_Gen(partidagen);
            formxat.ShowDialog();
        }
        private void IniciarForm2()
        {
            form2 = new Form2(server);
            form2.SetLlistaJugadors(LlistaJugadors);
            nom_anfitrio = username;
            form2.SetUsername(username);
            form2.Set_Partida_Gen(partidagen);
            form2.ShowDialog();
            tempsacabat = form2.Return_TempsAcabat();
            

            if (tempsacabat == true)
            {
                byte[] msg2 = System.Text.Encoding.ASCII.GetBytes("10/" + partidagen + "/");
                server.Send(msg2);
            }
            else
            {
                //si sortim de la partida sense que s'acabi el temps ho notificam a tothom
                MessageBox.Show("No s'ha acabt el temps, però hem sortit de la partida");
                byte[] msg2 = System.Text.Encoding.ASCII.GetBytes("12/" + username+"/"+partidagen);
                server.Send(msg2);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes("9/");
            server.Send(msg);

            ThreadStart ts = delegate { IniciarForm2(); };
            ThreadStart ts2 = delegate { IniciarFormxat(); };
            Thread t = new Thread(ts);
            Thread t2 = new Thread(ts2);
            t.Start();
            t2.Start();

            //if (tempsacabat == true)
            //{
            //    byte[] msg2 = System.Text.Encoding.ASCII.GetBytes("10/" + partidagen + "/");
            //    server.Send(msg2);
            //}
            //else
            //{
            //    MessageBox.Show("No s'ha acabt el temps, però hem sortit de la partida");
            //}

        }

        private void Timer_Enviar_posicio_Tick(object sender, EventArgs e)
        {
            //if (tempsacabat == false && partida_iniciada == true)
            //{
            //    byte[] msg = System.Text.Encoding.ASCII.GetBytes("11/" + x_anfitrio.ToString() + "/" + y_anfitrio.ToString());
            //    server.Send(msg);
            //} 
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Mensaje de desconexión
            string nom = textBox_Nom.Text;
            string mensaje = "0/" + nom;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            //MessageBox.Show(mensaje);
            server.Send(msg);

            // Nos desconectamos
            Atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            Connectat = false;
            convidats.Clear();
            invitacio = null;
            invitacio = "7/";
            server.Close();
        }
    }
}

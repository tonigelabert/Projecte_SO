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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        public bool LoggedIn = false;
        public bool Connectat = false;
        public string username;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";
        
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            //MessageBox.Show(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();


        }

        private void Button_Login_Click(object sender, EventArgs e)
        {
            if (!Connectat)
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("192.168.56.102");
                IPEndPoint ipep = new IPEndPoint(direc, 9080);


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

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                if (mensaje == "1")
                {
                    LoggedIn = true;
                    username = textBox_Nom.Text;
                    this.BackColor = Color.Green;
                    MessageBox.Show("LogIn correcte");
                }
                else
                    MessageBox.Show("Error al introduir el nom i la contrasenya");
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
                IPAddress direc = IPAddress.Parse("192.168.56.102");
                IPEndPoint ipep = new IPEndPoint(direc, 9070);


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

                    //Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    if (mensaje == "Existent")
                        MessageBox.Show("Usuari ja registrat");
                    else
                    {
                        MessageBox.Show("Usuari registrat correctament");
                        this.BackColor = Color.Blue;
                    }
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

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("L'ID de la partida de menor duració és:" + mensaje);
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

                    //Recibimos la respuesta del servidor
                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    MessageBox.Show("La contrasenya és: " + mensaje);
                }

            }
            else if (Mes_particpacions.Checked && LoggedIn)
            {
                // Enviamos nombre y altura
                string mensaje = "3/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("El jugador amb més participacions és: " + mensaje);
            }
            else
            {
                MessageBox.Show("Registra't per fer una consulta");
            }
        }
    }
}

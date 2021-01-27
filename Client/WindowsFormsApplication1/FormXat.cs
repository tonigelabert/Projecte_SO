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
    public partial class FormXat : Form
    {
        Socket server;
        string username;
        int partidagen;
        public FormXat(Socket server)
        {
            this.server = server;
            InitializeComponent();
        }

        private void FormXat_Load(object sender, EventArgs e)
        {

        }
        public void SetUsername(string username)
        {
            this.username = username;
        }
        public void Set_Partida_Gen(int partida_gen)
        {
            this.partidagen = partida_gen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string missatge = textBoxXat.Text;
            string mensaje = "13/" + partidagen + "/" + username + "/" + missatge;
            //Enviem el nostre missatge a la resta
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            //MessageBox.Show(mensaje);
            server.Send(msg);
        }

        public void AfegirMissatge(string missatge)
        {
            //Rebem un missatge i l'afegim al nostre xat
            xat.Items.Add(missatge);
        }
    }
}

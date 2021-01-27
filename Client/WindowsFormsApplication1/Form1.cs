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
        int port = 50074;//OPCIONS: 50072, 50073, 50074
        string ip = "147.83.117.22";//147.83.117.22,192.168.56.102
        bool tempsacabat = false;
        bool EstatFinal;
        public int partidagen;
        public int x_anfitrio;
        public int y_anfitrio;
        public bool partida_iniciada = false;
        public bool desconnectat_partida;
        List<string> LlistaJugadors = new List<string>();
        List<string> convidats = new List<string>();
        string [] Nom_posicio = new string [12];
        bool impostor;
        bool agafat;
        string nom_anfitrio;
        int f = 0;
        Form2 form2;
        FormXat formxat;
        delegate void DelegadoPerLlistaConectats(string connectat);
        delegate void DelegadoPerLlistaConectats_Clear();
        delegate void DelegadoPerForm2_SetXY(int x, int y, string nom_mov);
        delegate void DelegadoSet_numPistes(int num_pistes, int enviat);
        delegate void DelegadoPerForm2_Desconectat(string nom_desc);
        delegate void DelegadoPerFormXat_AfegirMissatge(string missatge);
        delegate void DelegadoParaBooleano(bool booleano);
        delegate void DelegadoParaCerrarForm2();
        delegate void DelegadoParaCerrarFormXat(FormXat form);
        delegate void DelegadoParaDesconectar();
        System.Media.SoundPlayer player;

        string infopartida;
        bool foundatender = true;
        bool Connectar2 = true;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player = new System.Media.SoundPlayer();
            player.SoundLocation = "maintheme.wav";
            //player.Play();
            this.timer1.Enabled = true;
            textBox_Contrasenya.PasswordChar = '*';

        }

        private void AtenderServidor()
        {
            while (foundatender == true)
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
                        if (mensaje == "error")
                        {
                            MessageBox.Show("Paraula de seguretat no vàlida");
                        }
                        else
                        {
                            MessageBox.Show("La contrasenya és: " + mensaje);
                        }
                        break;
                    case 3: //resposta max participacions
                        //exemple 3/1/error/nom1/nom2/nom3
                        string error = trozos[2].Split('\0')[0];
                        if (error == "-1")
                            MessageBox.Show("Partida no vàlida");
                        else
                        {
                            string partidajugadors = "Partida: " + trozos[1].Split('\0')[0];
                            int jugadors = Convert.ToInt32(trozos[3].Split('\0')[0]);
                            DelegadoPerLlistaConectats delegado1 = new DelegadoPerLlistaConectats(PonJugadors);
                            jug_part_listbox.Invoke(delegado1, new object[] { partidajugadors });

                            int m = 4;
                            int n = 0;
                            while (n < jugadors)
                            {
                                DelegadoPerLlistaConectats delegado2 = new DelegadoPerLlistaConectats(PonJugadors);
                                jug_part_listbox.Invoke(delegado2, new object[] { trozos[m].Split('\0')[0] });
                                m++;
                                n++;
                            }
                        }
                        break;
                    case 4: //resposta login
                        mensaje = trozos[1].Split('\0')[0];
                        if (mensaje == "1")
                        {
                            LoggedIn = true;
                            username = textBox_Nom.Text;
                            MessageBox.Show("LogIn correcte");
                        }
                        else if (mensaje == "2")
                        {
                            MessageBox.Show("Usuari ja registrat");
                            //DelegadoParaDesconectar dele10 = new DelegadoParaDesconectar(Desconectar);
                            //this.Invoke(dele10, new object[] { });
                        }
                        else
                            MessageBox.Show("Error al introduir el nom i la contrasenya");
                            //DelegadoParaDesconectar dele11 = new DelegadoParaDesconectar(Desconectar);
                            //this.Invoke(dele11, new object[] {});
                        break;
                    case 5: //registrar
                        mensaje = trozos[1].Split('\0')[0];
                        if (mensaje == "Existent")
                            MessageBox.Show("Usuari ja registrat");
                        else
                        {
                            MessageBox.Show("Usuari registrat correctament");
                           
                        }
                        break;
                    case 6: //llista connectats
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
                        if (nom_anfitrio != username)
                        {

                            DialogResult dialogResult = MessageBox.Show("Vols jugar amb " + anfitrio + "?", "Invitació a Partida", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes("8/" + anfitrio + "/" + partida.ToString() + "/SI");
                                server.Send(msg);
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                byte[] msg = System.Text.Encoding.ASCII.GetBytes("8/" + anfitrio + "/" + partida.ToString() + "/NO");
                                server.Send(msg);
                            }
                        }
                        break;
                    case 8: //L'ANFITRIÓ rep la resposta del convidat
                        string convidat = trozos[1].Split('\0')[0];
                        string resposta = trozos[2].Split('\0')[0];
                        
                        if (resposta == "SI")
                        {
                            f = f + 3;
                            Nom_posicio[f] = convidat;
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
                                Nom_posicio[0] = trozos[1].Split('\0')[0];

                            }
                            if (trozos[2].Split('\0')[0] != "")
                            {
                                LlistaJugadors.Add(trozos[2].Split('\0')[0]);
                                Nom_posicio[3] = trozos[2].Split('\0')[0];

                            }
                            if (trozos[3].Split('\0')[0] != "")
                            {
                                LlistaJugadors.Add(trozos[3].Split('\0')[0]);
                                Nom_posicio[6] = trozos[3].Split('\0')[0];
                            }
                            if (trozos[4].Split('\0')[0] != "")
                            {
                                LlistaJugadors.Add(trozos[4].Split('\0')[0]);
                                Nom_posicio[9] = trozos[4].Split('\0')[0];

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
                        if (agafat == false)
                        {
                            int x = Convert.ToInt32(trozos[1].Split('\0')[0]);
                            int y = Convert.ToInt32(trozos[2].Split('\0')[0]);
                            string nom_moviment = trozos[3].Split('\0')[0];
                            DelegadoPerForm2_SetXY dele = new DelegadoPerForm2_SetXY(SetXY_form2);
                            this.form2.Invoke(dele, new object[] { x, y, nom_moviment });
                        }
                        break;

                    case 12:
                        //notificació que algú ha sortit de la partida i borrar elseu picture box
                        string nom_desconectat = trozos[1].Split('\0')[0];
                        DelegadoPerForm2_Desconectat dele2 = new DelegadoPerForm2_Desconectat(Set_desconectat);
                        this.form2.Invoke(dele2, new object[] {nom_desconectat });
                        break;

                    case 13://Rebem missatge del xat
                        string nomemisor = trozos[1].Split('\0')[0];
                        string missatgeenviat = trozos[2].Split('\0')[0];
                        string missatge = nomemisor + ": " + missatgeenviat;
                        DelegadoPerFormXat_AfegirMissatge dele3 = new DelegadoPerFormXat_AfegirMissatge(AfegirMissatge);
                        this.formxat.Invoke(dele3, new object[] { missatge });
                        break;

                    case 14://Augmentam el nombre de pistes
                        int num_pistes = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        int num_pistesMAX = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        DelegadoSet_numPistes dele4 = new DelegadoSet_numPistes(Set_numPistes);
                        this.form2.Invoke(dele4, new object[] { num_pistes, num_pistesMAX });
                        break;

                    case 15://Rebem la posició del ous daurats
                        int posicio_pistaFinal_x = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        int posicio_pistaFinal_y = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        if (posicio_pistaFinal_y > 1000)
                        {
                            posicio_pistaFinal_y = posicio_pistaFinal_y / 100;
                        }
                        DelegadoSet_numPistes dele5 = new DelegadoSet_numPistes(Set_posicio_pistaFinal);
                        this.form2.Invoke(dele5, new object[] { posicio_pistaFinal_x, posicio_pistaFinal_y});
                        break;
                    case 16://Notifiquem a tots que el form amb el audio del Morse s'ha obert
                        int opcio = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        if (opcio == 1)
                        {
                            DelegadoParaBooleano dele6 = new DelegadoParaBooleano(SetBooleanoMorse);
                            this.form2.Invoke(dele6, new object[] { Convert.ToBoolean(trozos[3]) });
                        }
                        else
                        {
                            DelegadoParaBooleano dele20 = new DelegadoParaBooleano(Set_Bool_Morse_2);
                            this.form2.Invoke(dele20, new object[] { true });
                        }
                         break;
                    case 17: //Notifiquem a tothom que la pista del Morse ha estat utilitzada
                        DelegadoParaBooleano dele7 = new DelegadoParaBooleano(SetBooleanoMorseUtilitzat);
                        this.form2.Invoke(dele7, new object[] { true });
                        break;
                    case 18://Toto els ous agafats, comença el pillapilla
                        DelegadoParaBooleano dele8 = new DelegadoParaBooleano(SetBool_PartidaFinal);
                        this.form2.Invoke(dele8, new object[] { true });
                        break;
                    case 19:
                        //Si rebem 19 hem estat agafats
                        DelegadoParaBooleano dele9 = new DelegadoParaBooleano(Set_Bool_Agafat);
                        this.form2.Invoke(dele9, new object[] { true });
                       
                        break;
                    case 20://rebem quan l'impostor pilla a un jugador
                        string guanyador = trozos[1].Split('\0')[0];
                        if (impostor == false)
                        {
                            MessageBox.Show("L'impostor "+guanyador + " ha guanyat la partida");
                            if (agafat == false)//Els altre jugadors que no han estat pillats també perden, se tanca el form2
                            {
                                agafat = true;
                                DelegadoParaCerrarForm2 dele10 = new DelegadoParaCerrarForm2(Tancar_Form);
                                form2.Invoke(dele10, new object[] { });
                            }
                        }
                        break;
                    case 21://Rebem info consulta partides que hem jugat
                        //exemple 21/partida/2/ID1/data1/hora1/ID2/data2/hora2
                        int numpartides = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        int p = 4;
                        int q = 0;
                        while (q < numpartides*3)
                        {
                            DelegadoPerLlistaConectats delegado2 =  new DelegadoPerLlistaConectats(PonPartides);
                            part_jug_listbox.Invoke(delegado2, new object[] { trozos[p].Split('\0')[0] });
                            p++;
                            q++;
                        }
                        break;
                    case 22://Ens desconectam
                        MessageBox.Show(trozos[1]);
                        foundatender = false;
                        

                        // Nos desconectamos
                        DelegadoParaDesconectar dele12 = new DelegadoParaDesconectar(Desconectar);
                        this.Invoke(dele12, new object[] {});
                        
                        break;

                }
            }
        }
        //Funció per tancar el form 2
        public void Tancar_Form()
        {

            form2.Tancar();
        }
        //Funció per notificar que ha estat agafat
        public void Set_Bool_Agafat(bool a)
        {
            form2.Set_Agafat(a);
            agafat = a;
        }
        
        //Funció per a posar la variable Morse_2
        public void Set_Bool_Morse_2(bool a)
        {
            form2.SetBooleanoMorse2(a);
            
        }
        public void PonLlista(string connectat)
        {
            llistaconnectat.Items.Add(connectat);
            
        }
        public void PonJugadors(string jugador)
        {
            jug_part_listbox.Items.Add(jugador);
        }
        public void PonPartides(string partida)
        {
            part_jug_listbox.Items.Add(partida);
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
        public void Set_numPistes(int num_pistes, int enviat) //enviem al form2 que hem recopilat les x pistes
        {
            form2.Set_numPistes(num_pistes, enviat);
        }
        public void Set_posicio_pistaFinal(int x, int y)
        {
            form2.Set_pistaFinal(x, y);
        }
        public void AfegirMissatge(string missatge)
        {
            formxat.AfegirMissatge(missatge);
        }
        public void SetBooleanoMorse(bool booleano)
        {
            form2.SetBooleanoMorse(booleano);
        }
        public void SetBooleanoMorseUtilitzat(bool booleano)
        {
            form2.SetBooleanoMorseUtilitzat(booleano);
        }
        //un cop tenim tots els ous podem passar a la prova final
        public void SetBool_PartidaFinal(bool b)
        {
            form2.Set_PartidaFinal(b);
        }

        public void Desconectar()
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
            //invitacio = null;
            //invitacio = "7/";
            server.Close();
            this.Close();


        }
        //Boto per desconectar
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
            //invitacio = null;
            //invitacio = "7/";
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
                    LoggedIn = true;

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

                if (Connectar2==true)
                {

                    ThreadStart ts = delegate { AtenderServidor(); };
                    Atender = new Thread(ts);
                    Atender.Start();
                }


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
                Connectar2 = false;


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
        //Boto per enviar les consultes
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
            else if (Jug_part.Checked && LoggedIn)
            {
                //Enviem la partida
                if (PartidaBox.Text == "")
                    MessageBox.Show("Introdueix una partida");
                else
                {
                    string partidaseleccionada = PartidaBox.Text;
                    string mensaje = "3/" + partidaseleccionada;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
            }
            else if (Part_jug.Checked && LoggedIn)
            {
                string mensaje = "21/" + partidagen + "/" + username;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Registra't per fer una consulta");
            }
        }

        
       
        
        string invitacio = "7/";
        private void llistaconnectat_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            bool convidat=false;
            int f = 0;
            //Comprovam que el jugador seleccionat no està convidat
            for (int j = 0; j < convidats.Count(); j++)
            {
                if (llistaconnectat.SelectedItem.ToString() == convidats[j])
                {

                    convidat = true;
                    f = j;
                }
            }
            //Mirem si l'item seleccionat no som nosaltres i llavors afegim el convidat a la llista
            if ((llistaconnectat.SelectedItem != null) && (llistaconnectat.SelectedItem.ToString() != username)&& (convidat==false))
            {

                string Nom_Convidat = llistaconnectat.SelectedItem.ToString();
                convidats.Add(Nom_Convidat);
                invitacio = invitacio + convidats.Last() + "/";
                //while (i<convidats.Count())
                //{
                //    invitacio = invitacio + convidats[i] + "/";
                //    i++;
                //}

            }
            else
            {

                if ((llistaconnectat.SelectedItem != null) && llistaconnectat.SelectedItem.ToString() == username)
                {
                    MessageBox.Show("No et pots convidar a tu mateix!");
                }
                if ((llistaconnectat.SelectedItem != null) && (convidat==true))//Miram si un jugador ja ha estat convidat
                {
                    MessageBox.Show(convidats[f] + " ja ha estat convidat");
                }
                 
            }

        }
        //Boto per convidar
        private void button2_Click(object sender, EventArgs e)
        {
            if (convidats.Count() == 0)
            {
                MessageBox.Show("Fes doble clic sobre els jugadors que vols convidar");
            }
            else
            {
                LlistaJugadors.Add(username);//Afegim L'anfitrió s ls llista
                Nom_posicio[0] = username;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(invitacio);
                server.Send(msg);
            }
        }
        private void IniciarFormxat()
        {
            formxat = new FormXat(server);
            formxat.SetUsername(username); //Enviem al form del xat el nostre nom
            formxat.Set_Partida_Gen(partidagen); //Enviem al form del xat la nostra partida
            formxat.ShowDialog();
        }
        private void IniciarForm2()
        {
            form2 = new Form2(server);
            form2.SetLlistaJugadors(LlistaJugadors);//Passam la llista de jugadors al form 2
            form2.Set_Nom_posicio(Nom_posicio);//Passam la llista de jugadors amb les seves respectives posicions al form 2
            nom_anfitrio = username;
            form2.SetUsername(username);
            form2.Set_Partida_Gen(partidagen);
            if (impostor == true)
                form2.Set_impostor(impostor);
            form2.ShowDialog();
            LlistaJugadors.Clear();//Tancam el form dos i buidam les llistes
            convidats.Clear();
            BorrarVector_Nom_Posició(Nom_posicio);
            DelegadoParaCerrarFormXat delegado2 = new DelegadoParaCerrarFormXat(TancarXat);
            formxat.Invoke(delegado2, new object[] { formxat });
            tempsacabat = form2.Return_TempsAcabat();//Rebem si s'ha acabat el temps
            EstatFinal = form2.Return_Agafat();//Rebem si hem estat agafat
            infopartida = form2.Return_InfoPartida();
            
            //si s'ha acabat el temps o hem estat agafats significa que la partida ha finalitzat i l'eliminam
            if (tempsacabat == true || EstatFinal == true)
            {
                if (username == nom_anfitrio)
                {
                    byte[] msg5 = System.Text.Encoding.ASCII.GetBytes("10/" + partidagen + "/1"+infopartida); //Enviem que l'anfitrio ha guanyat i la info de la partida per guardar-la
                    server.Send(msg5);
                }
                else
                {
                    byte[] msg5 = System.Text.Encoding.ASCII.GetBytes("10/" + partidagen + "/2");
                    server.Send(msg5);
                }
            }
            else
            {
                //si sortim de la partida sense que s'acabi el temps ho notificam a tothom
                byte[] msg2 = System.Text.Encoding.ASCII.GetBytes("12/" + username+"/"+partidagen);
                server.Send(msg2);
            }
        }
        //Boto per començar a jugar
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes("9/");
            server.Send(msg);
            impostor = true;
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
            //    server.Send(msg)
            //} 
        }
        //Pitjam la creu per tanacar el form i ens desconectam
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Connectat==true)
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
        //Boto per borrar el compte
        private void button4_Click(object sender, EventArgs e)
        {
            if (LoggedIn == true)
            {
                string mensaje = "22/" + username;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                //MessageBox.Show(mensaje);
                server.Send(msg);
            }
            else
                MessageBox.Show("Per eliminar un usuari has de registrar-te com a ell");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            horaLbl.Text = "Hora actual: \n" + DateTime.Now.ToString("hh:mm:ss");
            diaLbl.Text = "Data:\n" + DateTime.Now.ToString("dd-MM");
            //horaLbl.Text = DateTime.Now.ToString("hh:mm:ss");
            //diaLbl.Text = DateTime.Now.ToLongDateString();
        }
        public void TancarXat(FormXat form)
        {
            form.Close();
        }
        bool music_on = true;
        //Boto per aturar o reiniciar la musica
        private void button5_Click(object sender, EventArgs e)
        {
            if (music_on == true)
            {
                player.Stop();
                music_on = false;
            }
            else
                player.Play();
            
        }
        public void BorrarVector_Nom_Posició(string[] llista)
        {
            int i = 0;
            while (i < llista.Count())
            {
                llista[i] = null;
                i = i + 1;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox_Contrasenya.PasswordChar = '\0';
            }
            else if (checkBox1.Checked == false)
            {
                textBox_Contrasenya.PasswordChar = '*';
            }
        }



    }
}

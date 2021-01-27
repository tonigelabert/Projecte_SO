using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class FormMorse : Form
    {
        SoundPlayer Player;
        bool on = false;
        bool pistamorse = false;
        public FormMorse()
        {
            InitializeComponent();
        }

        private void FormMorse_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (on == false) //Si no està sonant, comença a sonar
            {
                on = true;
                Player = new SoundPlayer("Morsewav.wav");
                Player.Play();
            }
            else //Si està sonant, el parem
            {
                on = false;
                Player.Stop();
            }
        }

        public bool GetPistaMorse()
        {
            return this.pistamorse;
        }

        private void FormMorse_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.pistamorse = false;
        }
    }
}

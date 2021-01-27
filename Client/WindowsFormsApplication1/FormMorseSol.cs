using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormMorseSol : Form
    {
        bool trobada = false;
  
        public FormMorseSol()
        {
            InitializeComponent();
        }

        private void Enviar_Click(object sender, EventArgs e)
        {
            if ((benvingutBox.Text == "benvingut") && (alBox.Text == "al") && (castellBox.Text == "castell")) //Aquí mirem si la resposta és correcta
            {

                MessageBox.Show("Pista correcte!");
                trobada = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ho sentim, no és correcte. Torna a provar!");
                trobada = false;
            }
        }
        public bool GetBooleanoMorse()
        {
            return this.trobada;
        }

        private void FormMorseSol_Load(object sender, EventArgs e)
        {

        }

        private void FormMorseSol_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Si tanques el form no podràs tornar a obrir-lo");
        }
    }
}

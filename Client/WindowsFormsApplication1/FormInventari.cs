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
    public partial class FormInventari : Form
    {
        
        List<string> LlistaPistes = new List<string>();
        
        public FormInventari(List<string> llistapistes)
        {
            InitializeComponent();
            this.LlistaPistes = llistapistes;

        }

        private void FormInventari_Load(object sender, EventArgs e)
        {
            int i = 0;
            while (i < LlistaPistes.Count())
            {
                listBox1.Items.Add(LlistaPistes[i]);
                i++;
            }
        }

        private void FormInventari_FormClosing(object sender, FormClosingEventArgs e)
        {

            listBox1.Items.Clear();
        }

    }
}

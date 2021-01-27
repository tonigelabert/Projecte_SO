
namespace WindowsFormsApplication1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menúToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortirDeLaPartidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventariToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.contadorlbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LabelXY = new System.Windows.Forms.Label();
            this.LabelNotificacio = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.Contador_proves = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menúToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 36);
            this.menuStrip1.TabIndex = 0;
            // 
            // menúToolStripMenuItem
            // 
            this.menúToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.menúToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortirDeLaPartidaToolStripMenuItem,
            this.inventariToolStripMenuItem});
            this.menúToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menúToolStripMenuItem.ForeColor = System.Drawing.Color.Orange;
            this.menúToolStripMenuItem.Name = "menúToolStripMenuItem";
            this.menúToolStripMenuItem.Size = new System.Drawing.Size(78, 32);
            this.menúToolStripMenuItem.Text = "Menú";
            // 
            // sortirDeLaPartidaToolStripMenuItem
            // 
            this.sortirDeLaPartidaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sortirDeLaPartidaToolStripMenuItem.Name = "sortirDeLaPartidaToolStripMenuItem";
            this.sortirDeLaPartidaToolStripMenuItem.Size = new System.Drawing.Size(217, 28);
            this.sortirDeLaPartidaToolStripMenuItem.Text = "Sortir de la partida";
            this.sortirDeLaPartidaToolStripMenuItem.Click += new System.EventHandler(this.sortirDeLaPartidaToolStripMenuItem_Click);
            // 
            // inventariToolStripMenuItem
            // 
            this.inventariToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Light", 10.2F);
            this.inventariToolStripMenuItem.Name = "inventariToolStripMenuItem";
            this.inventariToolStripMenuItem.Size = new System.Drawing.Size(217, 28);
            this.inventariToolStripMenuItem.Text = "Inventari";
            this.inventariToolStripMenuItem.Click += new System.EventHandler(this.inventariToolStripMenuItem_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // contadorlbl
            // 
            this.contadorlbl.BackColor = System.Drawing.Color.Transparent;
            this.contadorlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contadorlbl.ForeColor = System.Drawing.Color.Orange;
            this.contadorlbl.Location = new System.Drawing.Point(568, 92);
            this.contadorlbl.Name = "contadorlbl";
            this.contadorlbl.Size = new System.Drawing.Size(68, 34);
            this.contadorlbl.TabIndex = 1;
            this.contadorlbl.Text = "+";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(547, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // LabelXY
            // 
            this.LabelXY.AutoSize = true;
            this.LabelXY.BackColor = System.Drawing.Color.Transparent;
            this.LabelXY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.LabelXY.Location = new System.Drawing.Point(570, 159);
            this.LabelXY.Name = "LabelXY";
            this.LabelXY.Size = new System.Drawing.Size(0, 17);
            this.LabelXY.TabIndex = 3;
            // 
            // LabelNotificacio
            // 
            this.LabelNotificacio.AutoSize = true;
            this.LabelNotificacio.BackColor = System.Drawing.Color.Transparent;
            this.LabelNotificacio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.LabelNotificacio.Location = new System.Drawing.Point(30, 328);
            this.LabelNotificacio.Name = "LabelNotificacio";
            this.LabelNotificacio.Size = new System.Drawing.Size(0, 17);
            this.LabelNotificacio.TabIndex = 4;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(138, 512);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(27, 24);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer4
            // 
            this.timer4.Interval = 1000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // Contador_proves
            // 
            this.Contador_proves.BackColor = System.Drawing.Color.Transparent;
            this.Contador_proves.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Contador_proves.ForeColor = System.Drawing.Color.Orange;
            this.Contador_proves.Location = new System.Drawing.Point(337, 328);
            this.Contador_proves.Name = "Contador_proves";
            this.Contador_proves.Size = new System.Drawing.Size(42, 34);
            this.Contador_proves.TabIndex = 6;
            // 
            // Form2
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(686, 710);
            this.Controls.Add(this.Contador_proves);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.LabelNotificacio);
            this.Controls.Add(this.LabelXY);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.contadorlbl);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load_1);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form2_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Panell;
        private System.Windows.Forms.Timer timer_move;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menúToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortirDeLaPartidaToolStripMenuItem;
        private System.Windows.Forms.Label contadorlbl;
        public System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LabelXY;
        private System.Windows.Forms.ToolStripMenuItem inventariToolStripMenuItem;
        private System.Windows.Forms.Label LabelNotificacio;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Label Contador_proves;
    }
}
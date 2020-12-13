namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Button_Enviar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_paraulaSeguretat = new System.Windows.Forms.TextBox();
            this.Partida_min = new System.Windows.Forms.RadioButton();
            this.Mes_particpacions = new System.Windows.Forms.RadioButton();
            this.Retorna_Contrasenya = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Button_Login = new System.Windows.Forms.Button();
            this.textBox_Contrasenya = new System.Windows.Forms.TextBox();
            this.textBox_Nom = new System.Windows.Forms.TextBox();
            this.LabelContrasenya = new System.Windows.Forms.Label();
            this.LabelNom = new System.Windows.Forms.Label();
            this.groupBoxRegister = new System.Windows.Forms.GroupBox();
            this.Label_ParaulaSeguretat = new System.Windows.Forms.Label();
            this.textBox_ParaulaSeguretat2 = new System.Windows.Forms.TextBox();
            this.Button_Registrar = new System.Windows.Forms.Button();
            this.textBox_contrasenya2 = new System.Windows.Forms.TextBox();
            this.textBox_nom2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.llistaconnectat = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Timer_Enviar_posicio = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 37);
            this.label2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.Button_Enviar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_paraulaSeguretat);
            this.groupBox1.Controls.Add(this.Partida_min);
            this.groupBox1.Controls.Add(this.Mes_particpacions);
            this.groupBox1.Controls.Add(this.Retorna_Contrasenya);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(61, 556);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(616, 311);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // Button_Enviar
            // 
            this.Button_Enviar.Location = new System.Drawing.Point(252, 196);
            this.Button_Enviar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button_Enviar.Name = "Button_Enviar";
            this.Button_Enviar.Size = new System.Drawing.Size(177, 30);
            this.Button_Enviar.TabIndex = 11;
            this.Button_Enviar.Text = "Enviar";
            this.Button_Enviar.UseVisualStyleBackColor = true;
            this.Button_Enviar.Click += new System.EventHandler(this.Button_Enviar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Paraula de seguretat";
            // 
            // textBox_paraulaSeguretat
            // 
            this.textBox_paraulaSeguretat.Location = new System.Drawing.Point(20, 196);
            this.textBox_paraulaSeguretat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_paraulaSeguretat.Name = "textBox_paraulaSeguretat";
            this.textBox_paraulaSeguretat.Size = new System.Drawing.Size(172, 26);
            this.textBox_paraulaSeguretat.TabIndex = 9;
            // 
            // Partida_min
            // 
            this.Partida_min.AutoSize = true;
            this.Partida_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Partida_min.Location = new System.Drawing.Point(20, 84);
            this.Partida_min.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Partida_min.Name = "Partida_min";
            this.Partida_min.Size = new System.Drawing.Size(302, 29);
            this.Partida_min.TabIndex = 7;
            this.Partida_min.TabStop = true;
            this.Partida_min.Text = "Partida de menor duració";
            this.Partida_min.UseVisualStyleBackColor = true;
            // 
            // Mes_particpacions
            // 
            this.Mes_particpacions.AutoSize = true;
            this.Mes_particpacions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mes_particpacions.Location = new System.Drawing.Point(20, 120);
            this.Mes_particpacions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Mes_particpacions.Name = "Mes_particpacions";
            this.Mes_particpacions.Size = new System.Drawing.Size(536, 29);
            this.Mes_particpacions.TabIndex = 7;
            this.Mes_particpacions.TabStop = true;
            this.Mes_particpacions.Text = "Jugador amb més participacions en una partida";
            this.Mes_particpacions.UseVisualStyleBackColor = true;
            // 
            // Retorna_Contrasenya
            // 
            this.Retorna_Contrasenya.AutoSize = true;
            this.Retorna_Contrasenya.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Retorna_Contrasenya.Location = new System.Drawing.Point(20, 48);
            this.Retorna_Contrasenya.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Retorna_Contrasenya.Name = "Retorna_Contrasenya";
            this.Retorna_Contrasenya.Size = new System.Drawing.Size(574, 29);
            this.Retorna_Contrasenya.TabIndex = 8;
            this.Retorna_Contrasenya.TabStop = true;
            this.Retorna_Contrasenya.Text = "Retornar contrasenya amb la paraula de serguretat";
            this.Retorna_Contrasenya.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(381, 225);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 40);
            this.button3.TabIndex = 10;
            this.button3.Text = "LogOut";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.Button_Login);
            this.groupBox2.Controls.Add(this.textBox_Contrasenya);
            this.groupBox2.Controls.Add(this.textBox_Nom);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.LabelContrasenya);
            this.groupBox2.Controls.Add(this.LabelNom);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox2.Location = new System.Drawing.Point(61, 219);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(544, 285);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // Button_Login
            // 
            this.Button_Login.Location = new System.Drawing.Point(381, 155);
            this.Button_Login.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button_Login.Name = "Button_Login";
            this.Button_Login.Size = new System.Drawing.Size(114, 41);
            this.Button_Login.TabIndex = 4;
            this.Button_Login.Text = "LogIn";
            this.Button_Login.UseVisualStyleBackColor = true;
            this.Button_Login.Click += new System.EventHandler(this.Button_Login_Click);
            // 
            // textBox_Contrasenya
            // 
            this.textBox_Contrasenya.Location = new System.Drawing.Point(38, 192);
            this.textBox_Contrasenya.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Contrasenya.Name = "textBox_Contrasenya";
            this.textBox_Contrasenya.Size = new System.Drawing.Size(230, 26);
            this.textBox_Contrasenya.TabIndex = 3;
            // 
            // textBox_Nom
            // 
            this.textBox_Nom.Location = new System.Drawing.Point(38, 104);
            this.textBox_Nom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Nom.Name = "textBox_Nom";
            this.textBox_Nom.Size = new System.Drawing.Size(230, 26);
            this.textBox_Nom.TabIndex = 2;
            // 
            // LabelContrasenya
            // 
            this.LabelContrasenya.AutoSize = true;
            this.LabelContrasenya.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelContrasenya.Location = new System.Drawing.Point(34, 155);
            this.LabelContrasenya.Name = "LabelContrasenya";
            this.LabelContrasenya.Size = new System.Drawing.Size(145, 25);
            this.LabelContrasenya.TabIndex = 1;
            this.LabelContrasenya.Text = "Contrasenya";
            // 
            // LabelNom
            // 
            this.LabelNom.AutoSize = true;
            this.LabelNom.BackColor = System.Drawing.Color.Transparent;
            this.LabelNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNom.Location = new System.Drawing.Point(34, 76);
            this.LabelNom.Name = "LabelNom";
            this.LabelNom.Size = new System.Drawing.Size(59, 25);
            this.LabelNom.TabIndex = 0;
            this.LabelNom.Text = "Nom";
            // 
            // groupBoxRegister
            // 
            this.groupBoxRegister.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxRegister.Controls.Add(this.Label_ParaulaSeguretat);
            this.groupBoxRegister.Controls.Add(this.textBox_ParaulaSeguretat2);
            this.groupBoxRegister.Controls.Add(this.Button_Registrar);
            this.groupBoxRegister.Controls.Add(this.textBox_contrasenya2);
            this.groupBoxRegister.Controls.Add(this.textBox_nom2);
            this.groupBoxRegister.Controls.Add(this.label3);
            this.groupBoxRegister.Controls.Add(this.label4);
            this.groupBoxRegister.Location = new System.Drawing.Point(732, 219);
            this.groupBoxRegister.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxRegister.Name = "groupBoxRegister";
            this.groupBoxRegister.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxRegister.Size = new System.Drawing.Size(567, 311);
            this.groupBoxRegister.TabIndex = 13;
            this.groupBoxRegister.TabStop = false;
            // 
            // Label_ParaulaSeguretat
            // 
            this.Label_ParaulaSeguretat.AutoSize = true;
            this.Label_ParaulaSeguretat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_ParaulaSeguretat.Location = new System.Drawing.Point(300, 155);
            this.Label_ParaulaSeguretat.Name = "Label_ParaulaSeguretat";
            this.Label_ParaulaSeguretat.Size = new System.Drawing.Size(232, 25);
            this.Label_ParaulaSeguretat.TabIndex = 6;
            this.Label_ParaulaSeguretat.Text = "Paraula de seguretat";
            // 
            // textBox_ParaulaSeguretat2
            // 
            this.textBox_ParaulaSeguretat2.Location = new System.Drawing.Point(304, 192);
            this.textBox_ParaulaSeguretat2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParaulaSeguretat2.Name = "textBox_ParaulaSeguretat2";
            this.textBox_ParaulaSeguretat2.Size = new System.Drawing.Size(230, 26);
            this.textBox_ParaulaSeguretat2.TabIndex = 5;
            // 
            // Button_Registrar
            // 
            this.Button_Registrar.Location = new System.Drawing.Point(38, 244);
            this.Button_Registrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button_Registrar.Name = "Button_Registrar";
            this.Button_Registrar.Size = new System.Drawing.Size(142, 41);
            this.Button_Registrar.TabIndex = 4;
            this.Button_Registrar.Text = "Registrar";
            this.Button_Registrar.UseVisualStyleBackColor = true;
            this.Button_Registrar.Click += new System.EventHandler(this.Button_Registrar_Click);
            // 
            // textBox_contrasenya2
            // 
            this.textBox_contrasenya2.Location = new System.Drawing.Point(38, 192);
            this.textBox_contrasenya2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_contrasenya2.Name = "textBox_contrasenya2";
            this.textBox_contrasenya2.Size = new System.Drawing.Size(230, 26);
            this.textBox_contrasenya2.TabIndex = 3;
            // 
            // textBox_nom2
            // 
            this.textBox_nom2.Location = new System.Drawing.Point(38, 104);
            this.textBox_nom2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_nom2.Name = "textBox_nom2";
            this.textBox_nom2.Size = new System.Drawing.Size(230, 26);
            this.textBox_nom2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Contrasenya";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nom";
            // 
            // llistaconnectat
            // 
            this.llistaconnectat.FormattingEnabled = true;
            this.llistaconnectat.ItemHeight = 20;
            this.llistaconnectat.Location = new System.Drawing.Point(732, 566);
            this.llistaconnectat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.llistaconnectat.Name = "llistaconnectat";
            this.llistaconnectat.Size = new System.Drawing.Size(179, 284);
            this.llistaconnectat.TabIndex = 14;
            this.llistaconnectat.SelectedIndexChanged += new System.EventHandler(this.llistaconnectat_SelectedIndexChanged);
            this.llistaconnectat.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.llistaconnectat_MouseDoubleClick_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1138, 752);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 81);
            this.button1.TabIndex = 15;
            this.button1.Text = "Entrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(61, 38);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(655, 121);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(732, 38);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(610, 121);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(81, 180);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(112, 62);
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(752, 180);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(179, 82);
            this.pictureBox4.TabIndex = 18;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.BackgroundImage")));
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(81, 510);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(112, 62);
            this.pictureBox5.TabIndex = 19;
            this.pictureBox5.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1141, 640);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 81);
            this.button2.TabIndex = 20;
            this.button2.Text = "Convidar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Timer_Enviar_posicio
            // 
            this.Timer_Enviar_posicio.Enabled = true;
            this.Timer_Enviar_posicio.Tick += new System.EventHandler(this.Timer_Enviar_posicio_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1407, 884);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.llistaconnectat);
            this.Controls.Add(this.groupBoxRegister);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Castle Break Menú Principal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxRegister.ResumeLayout(false);
            this.groupBoxRegister.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Partida_min;
        private System.Windows.Forms.RadioButton Retorna_Contrasenya;
        private System.Windows.Forms.RadioButton Mes_particpacions;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_paraulaSeguretat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_Nom;
        private System.Windows.Forms.Label LabelContrasenya;
        private System.Windows.Forms.Label LabelNom;
        private System.Windows.Forms.TextBox textBox_Contrasenya;
        private System.Windows.Forms.Button Button_Login;
        private System.Windows.Forms.GroupBox groupBoxRegister;
        private System.Windows.Forms.Label Label_ParaulaSeguretat;
        private System.Windows.Forms.TextBox textBox_ParaulaSeguretat2;
        private System.Windows.Forms.Button Button_Registrar;
        private System.Windows.Forms.TextBox textBox_contrasenya2;
        private System.Windows.Forms.TextBox textBox_nom2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Button_Enviar;
        private System.Windows.Forms.ListBox llistaconnectat;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer Timer_Enviar_posicio;
    }
}


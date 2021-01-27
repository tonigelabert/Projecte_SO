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
            this.PartidaBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Part_jug = new System.Windows.Forms.RadioButton();
            this.Button_Enviar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_paraulaSeguretat = new System.Windows.Forms.TextBox();
            this.Partida_min = new System.Windows.Forms.RadioButton();
            this.Jug_part = new System.Windows.Forms.RadioButton();
            this.Retorna_Contrasenya = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.Button_Login = new System.Windows.Forms.Button();
            this.textBox_Contrasenya = new System.Windows.Forms.TextBox();
            this.textBox_Nom = new System.Windows.Forms.TextBox();
            this.LabelContrasenya = new System.Windows.Forms.Label();
            this.LabelNom = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBoxRegister = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.jug_part_listbox = new System.Windows.Forms.ListBox();
            this.part_jug_listbox = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.horaLbl = new System.Windows.Forms.Label();
            this.diaLbl = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
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
            this.label2.Location = new System.Drawing.Point(26, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 31);
            this.label2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.PartidaBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Part_jug);
            this.groupBox1.Controls.Add(this.Button_Enviar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_paraulaSeguretat);
            this.groupBox1.Controls.Add(this.Partida_min);
            this.groupBox1.Controls.Add(this.Jug_part);
            this.groupBox1.Controls.Add(this.Retorna_Contrasenya);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(54, 445);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(548, 249);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // PartidaBox
            // 
            this.PartidaBox.Location = new System.Drawing.Point(18, 225);
            this.PartidaBox.MaxLength = 20;
            this.PartidaBox.Name = "PartidaBox";
            this.PartidaBox.Size = new System.Drawing.Size(153, 22);
            this.PartidaBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Partida";
            // 
            // Part_jug
            // 
            this.Part_jug.AutoSize = true;
            this.Part_jug.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Part_jug.Location = new System.Drawing.Point(18, 127);
            this.Part_jug.Name = "Part_jug";
            this.Part_jug.Size = new System.Drawing.Size(209, 24);
            this.Part_jug.TabIndex = 12;
            this.Part_jug.TabStop = true;
            this.Part_jug.Text = "Partides que he jugat";
            this.Part_jug.UseVisualStyleBackColor = true;
            // 
            // Button_Enviar
            // 
            this.Button_Enviar.Location = new System.Drawing.Point(339, 178);
            this.Button_Enviar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button_Enviar.Name = "Button_Enviar";
            this.Button_Enviar.Size = new System.Drawing.Size(157, 24);
            this.Button_Enviar.TabIndex = 11;
            this.Button_Enviar.Text = "Enviar";
            this.Button_Enviar.UseVisualStyleBackColor = true;
            this.Button_Enviar.Click += new System.EventHandler(this.Button_Enviar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Paraula de seguretat";
            // 
            // textBox_paraulaSeguretat
            // 
            this.textBox_paraulaSeguretat.Location = new System.Drawing.Point(18, 178);
            this.textBox_paraulaSeguretat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_paraulaSeguretat.MaxLength = 20;
            this.textBox_paraulaSeguretat.Name = "textBox_paraulaSeguretat";
            this.textBox_paraulaSeguretat.Size = new System.Drawing.Size(153, 22);
            this.textBox_paraulaSeguretat.TabIndex = 9;
            // 
            // Partida_min
            // 
            this.Partida_min.AutoSize = true;
            this.Partida_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Partida_min.Location = new System.Drawing.Point(18, 67);
            this.Partida_min.Margin = new System.Windows.Forms.Padding(4);
            this.Partida_min.Name = "Partida_min";
            this.Partida_min.Size = new System.Drawing.Size(242, 24);
            this.Partida_min.TabIndex = 7;
            this.Partida_min.TabStop = true;
            this.Partida_min.Text = "Partida de menor duració";
            this.Partida_min.UseVisualStyleBackColor = true;
            // 
            // Jug_part
            // 
            this.Jug_part.AutoSize = true;
            this.Jug_part.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Jug_part.Location = new System.Drawing.Point(18, 96);
            this.Jug_part.Margin = new System.Windows.Forms.Padding(4);
            this.Jug_part.Name = "Jug_part";
            this.Jug_part.Size = new System.Drawing.Size(334, 24);
            this.Jug_part.TabIndex = 7;
            this.Jug_part.TabStop = true;
            this.Jug_part.Text = "Jugadors de la partida seleccionada";
            this.Jug_part.UseVisualStyleBackColor = true;
            // 
            // Retorna_Contrasenya
            // 
            this.Retorna_Contrasenya.AutoSize = true;
            this.Retorna_Contrasenya.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Retorna_Contrasenya.Location = new System.Drawing.Point(18, 38);
            this.Retorna_Contrasenya.Margin = new System.Windows.Forms.Padding(4);
            this.Retorna_Contrasenya.Name = "Retorna_Contrasenya";
            this.Retorna_Contrasenya.Size = new System.Drawing.Size(459, 24);
            this.Retorna_Contrasenya.TabIndex = 8;
            this.Retorna_Contrasenya.TabStop = true;
            this.Retorna_Contrasenya.Text = "Retornar contrasenya amb la paraula de serguretat";
            this.Retorna_Contrasenya.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(339, 124);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 32);
            this.button3.TabIndex = 10;
            this.button3.Text = "LogOut";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.Button_Login);
            this.groupBox2.Controls.Add(this.textBox_Contrasenya);
            this.groupBox2.Controls.Add(this.textBox_Nom);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.LabelContrasenya);
            this.groupBox2.Controls.Add(this.LabelNom);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox2.Location = new System.Drawing.Point(54, 175);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(484, 228);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(339, 163);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 44);
            this.button4.TabIndex = 11;
            this.button4.Text = "Borrar compte";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Button_Login
            // 
            this.Button_Login.Location = new System.Drawing.Point(339, 83);
            this.Button_Login.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button_Login.Name = "Button_Login";
            this.Button_Login.Size = new System.Drawing.Size(101, 33);
            this.Button_Login.TabIndex = 4;
            this.Button_Login.Text = "LogIn";
            this.Button_Login.UseVisualStyleBackColor = true;
            this.Button_Login.Click += new System.EventHandler(this.Button_Login_Click);
            // 
            // textBox_Contrasenya
            // 
            this.textBox_Contrasenya.Location = new System.Drawing.Point(34, 154);
            this.textBox_Contrasenya.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Contrasenya.MaxLength = 20;
            this.textBox_Contrasenya.Name = "textBox_Contrasenya";
            this.textBox_Contrasenya.Size = new System.Drawing.Size(205, 22);
            this.textBox_Contrasenya.TabIndex = 3;
            // 
            // textBox_Nom
            // 
            this.textBox_Nom.Location = new System.Drawing.Point(34, 83);
            this.textBox_Nom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_Nom.MaxLength = 20;
            this.textBox_Nom.Name = "textBox_Nom";
            this.textBox_Nom.Size = new System.Drawing.Size(205, 22);
            this.textBox_Nom.TabIndex = 2;
            // 
            // LabelContrasenya
            // 
            this.LabelContrasenya.AutoSize = true;
            this.LabelContrasenya.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelContrasenya.Location = new System.Drawing.Point(30, 124);
            this.LabelContrasenya.Name = "LabelContrasenya";
            this.LabelContrasenya.Size = new System.Drawing.Size(114, 20);
            this.LabelContrasenya.TabIndex = 1;
            this.LabelContrasenya.Text = "Contrasenya";
            // 
            // LabelNom
            // 
            this.LabelNom.AutoSize = true;
            this.LabelNom.BackColor = System.Drawing.Color.Transparent;
            this.LabelNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNom.Location = new System.Drawing.Point(30, 61);
            this.LabelNom.Name = "LabelNom";
            this.LabelNom.Size = new System.Drawing.Size(47, 20);
            this.LabelNom.TabIndex = 0;
            this.LabelNom.Text = "Nom";
            // 
            // button5
            // 
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(8, 9);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(41, 32);
            this.button5.TabIndex = 24;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBoxRegister
            // 
            this.groupBoxRegister.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxRegister.Controls.Add(this.label7);
            this.groupBoxRegister.Controls.Add(this.Label_ParaulaSeguretat);
            this.groupBoxRegister.Controls.Add(this.textBox_ParaulaSeguretat2);
            this.groupBoxRegister.Controls.Add(this.Button_Registrar);
            this.groupBoxRegister.Controls.Add(this.textBox_contrasenya2);
            this.groupBoxRegister.Controls.Add(this.textBox_nom2);
            this.groupBoxRegister.Controls.Add(this.label3);
            this.groupBoxRegister.Controls.Add(this.label4);
            this.groupBoxRegister.Location = new System.Drawing.Point(651, 175);
            this.groupBoxRegister.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxRegister.Name = "groupBoxRegister";
            this.groupBoxRegister.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxRegister.Size = new System.Drawing.Size(504, 249);
            this.groupBoxRegister.TabIndex = 13;
            this.groupBoxRegister.TabStop = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(251, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 66);
            this.label7.TabIndex = 12;
            this.label7.Text = "Fes doble click sobre el nom del jugador que vulguis convidar";
            // 
            // Label_ParaulaSeguretat
            // 
            this.Label_ParaulaSeguretat.AutoSize = true;
            this.Label_ParaulaSeguretat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_ParaulaSeguretat.Location = new System.Drawing.Point(267, 124);
            this.Label_ParaulaSeguretat.Name = "Label_ParaulaSeguretat";
            this.Label_ParaulaSeguretat.Size = new System.Drawing.Size(184, 20);
            this.Label_ParaulaSeguretat.TabIndex = 6;
            this.Label_ParaulaSeguretat.Text = "Paraula de seguretat";
            // 
            // textBox_ParaulaSeguretat2
            // 
            this.textBox_ParaulaSeguretat2.Location = new System.Drawing.Point(270, 154);
            this.textBox_ParaulaSeguretat2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ParaulaSeguretat2.MaxLength = 20;
            this.textBox_ParaulaSeguretat2.Name = "textBox_ParaulaSeguretat2";
            this.textBox_ParaulaSeguretat2.Size = new System.Drawing.Size(205, 22);
            this.textBox_ParaulaSeguretat2.TabIndex = 5;
            // 
            // Button_Registrar
            // 
            this.Button_Registrar.Location = new System.Drawing.Point(34, 195);
            this.Button_Registrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button_Registrar.Name = "Button_Registrar";
            this.Button_Registrar.Size = new System.Drawing.Size(126, 33);
            this.Button_Registrar.TabIndex = 4;
            this.Button_Registrar.Text = "Registrar";
            this.Button_Registrar.UseVisualStyleBackColor = true;
            this.Button_Registrar.Click += new System.EventHandler(this.Button_Registrar_Click);
            // 
            // textBox_contrasenya2
            // 
            this.textBox_contrasenya2.Location = new System.Drawing.Point(34, 154);
            this.textBox_contrasenya2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_contrasenya2.MaxLength = 20;
            this.textBox_contrasenya2.Name = "textBox_contrasenya2";
            this.textBox_contrasenya2.Size = new System.Drawing.Size(205, 22);
            this.textBox_contrasenya2.TabIndex = 3;
            // 
            // textBox_nom2
            // 
            this.textBox_nom2.Location = new System.Drawing.Point(34, 83);
            this.textBox_nom2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_nom2.MaxLength = 20;
            this.textBox_nom2.Name = "textBox_nom2";
            this.textBox_nom2.Size = new System.Drawing.Size(205, 22);
            this.textBox_nom2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Contrasenya";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nom";
            // 
            // llistaconnectat
            // 
            this.llistaconnectat.FormattingEnabled = true;
            this.llistaconnectat.ItemHeight = 16;
            this.llistaconnectat.Location = new System.Drawing.Point(905, 439);
            this.llistaconnectat.Name = "llistaconnectat";
            this.llistaconnectat.Size = new System.Drawing.Size(160, 228);
            this.llistaconnectat.TabIndex = 14;
            this.llistaconnectat.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.llistaconnectat_MouseDoubleClick_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1080, 623);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 65);
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
            this.pictureBox1.Location = new System.Drawing.Point(54, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(582, 97);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(651, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(542, 97);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(72, 144);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 50);
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(668, 144);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(159, 66);
            this.pictureBox4.TabIndex = 18;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.BackgroundImage")));
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(72, 408);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(100, 50);
            this.pictureBox5.TabIndex = 19;
            this.pictureBox5.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1080, 439);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 65);
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
            // jug_part_listbox
            // 
            this.jug_part_listbox.FormattingEnabled = true;
            this.jug_part_listbox.ItemHeight = 16;
            this.jug_part_listbox.Location = new System.Drawing.Point(770, 439);
            this.jug_part_listbox.Name = "jug_part_listbox";
            this.jug_part_listbox.Size = new System.Drawing.Size(120, 228);
            this.jug_part_listbox.TabIndex = 21;
            // 
            // part_jug_listbox
            // 
            this.part_jug_listbox.FormattingEnabled = true;
            this.part_jug_listbox.ItemHeight = 16;
            this.part_jug_listbox.Location = new System.Drawing.Point(634, 439);
            this.part_jug_listbox.Name = "part_jug_listbox";
            this.part_jug_listbox.Size = new System.Drawing.Size(120, 228);
            this.part_jug_listbox.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(1076, 527);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 23;
            // 
            // horaLbl
            // 
            this.horaLbl.Font = new System.Drawing.Font("Miriam CLM", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.horaLbl.Location = new System.Drawing.Point(1080, 511);
            this.horaLbl.Name = "horaLbl";
            this.horaLbl.Size = new System.Drawing.Size(159, 45);
            this.horaLbl.TabIndex = 24;
            // 
            // diaLbl
            // 
            this.diaLbl.Font = new System.Drawing.Font("Miriam CLM", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.diaLbl.Location = new System.Drawing.Point(1080, 572);
            this.diaLbl.Name = "diaLbl";
            this.diaLbl.Size = new System.Drawing.Size(159, 45);
            this.diaLbl.TabIndex = 25;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(18, 185);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(160, 21);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "Mostrar contrasenya";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1251, 707);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.diaLbl);
            this.Controls.Add(this.horaLbl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.part_jug_listbox);
            this.Controls.Add(this.jug_part_listbox);
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
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Partida_min;
        private System.Windows.Forms.RadioButton Retorna_Contrasenya;
        private System.Windows.Forms.RadioButton Jug_part;
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
        private System.Windows.Forms.TextBox PartidaBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton Part_jug;
        private System.Windows.Forms.ListBox jug_part_listbox;
        private System.Windows.Forms.ListBox part_jug_listbox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label horaLbl;
        private System.Windows.Forms.Label diaLbl;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}


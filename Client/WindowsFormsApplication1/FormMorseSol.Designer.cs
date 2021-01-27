namespace WindowsFormsApplication1
{
    partial class FormMorseSol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMorseSol));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.benvingutBox = new System.Windows.Forms.TextBox();
            this.alBox = new System.Windows.Forms.TextBox();
            this.castellBox = new System.Windows.Forms.TextBox();
            this.Enviar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(366, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(237, 408);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // benvingutBox
            // 
            this.benvingutBox.Location = new System.Drawing.Point(133, 221);
            this.benvingutBox.Name = "benvingutBox";
            this.benvingutBox.Size = new System.Drawing.Size(171, 22);
            this.benvingutBox.TabIndex = 1;
            // 
            // alBox
            // 
            this.alBox.Location = new System.Drawing.Point(133, 249);
            this.alBox.Name = "alBox";
            this.alBox.Size = new System.Drawing.Size(171, 22);
            this.alBox.TabIndex = 2;
            // 
            // castellBox
            // 
            this.castellBox.Location = new System.Drawing.Point(133, 277);
            this.castellBox.Name = "castellBox";
            this.castellBox.Size = new System.Drawing.Size(171, 22);
            this.castellBox.TabIndex = 3;
            // 
            // Enviar
            // 
            this.Enviar.Location = new System.Drawing.Point(185, 305);
            this.Enviar.Name = "Enviar";
            this.Enviar.Size = new System.Drawing.Size(75, 23);
            this.Enviar.TabIndex = 4;
            this.Enviar.Text = "Enviar";
            this.Enviar.UseVisualStyleBackColor = true;
            this.Enviar.Click += new System.EventHandler(this.Enviar_Click);
            // 
            // FormMorseSol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(705, 518);
            this.Controls.Add(this.Enviar);
            this.Controls.Add(this.castellBox);
            this.Controls.Add(this.alBox);
            this.Controls.Add(this.benvingutBox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormMorseSol";
            this.Text = "FormMorseSol";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMorseSol_FormClosing);
            this.Load += new System.EventHandler(this.FormMorseSol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox benvingutBox;
        private System.Windows.Forms.TextBox alBox;
        private System.Windows.Forms.TextBox castellBox;
        private System.Windows.Forms.Button Enviar;
    }
}
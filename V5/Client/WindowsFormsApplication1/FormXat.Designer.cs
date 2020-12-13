namespace WindowsFormsApplication1
{
    partial class FormXat
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxXat = new System.Windows.Forms.TextBox();
            this.xat = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 408);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 22);
            this.button1.TabIndex = 1;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxXat
            // 
            this.textBoxXat.Location = new System.Drawing.Point(25, 408);
            this.textBoxXat.Name = "textBoxXat";
            this.textBoxXat.Size = new System.Drawing.Size(232, 22);
            this.textBoxXat.TabIndex = 2;
            // 
            // xat
            // 
            this.xat.FormattingEnabled = true;
            this.xat.ItemHeight = 16;
            this.xat.Location = new System.Drawing.Point(25, 23);
            this.xat.Name = "xat";
            this.xat.Size = new System.Drawing.Size(344, 372);
            this.xat.TabIndex = 3;
            // 
            // FormXat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 464);
            this.Controls.Add(this.xat);
            this.Controls.Add(this.textBoxXat);
            this.Controls.Add(this.button1);
            this.Name = "FormXat";
            this.Text = "FormXat";
            this.Load += new System.EventHandler(this.FormXat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxXat;
        private System.Windows.Forms.ListBox xat;
    }
}
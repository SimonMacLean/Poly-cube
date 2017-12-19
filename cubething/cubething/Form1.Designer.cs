namespace cubething
{
    partial class Form1
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
            this.rotate_x = new System.Windows.Forms.Button();
            this.rotate_y = new System.Windows.Forms.Button();
            this.rotate_z = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rotate_x
            // 
            this.rotate_x.Location = new System.Drawing.Point(12, 12);
            this.rotate_x.Name = "rotate_x";
            this.rotate_x.Size = new System.Drawing.Size(75, 23);
            this.rotate_x.TabIndex = 0;
            this.rotate_x.Text = "Rotate X";
            this.rotate_x.UseVisualStyleBackColor = true;
            this.rotate_x.Click += new System.EventHandler(this.RotateX_Click);
            // 
            // rotate_y
            // 
            this.rotate_y.Location = new System.Drawing.Point(12, 41);
            this.rotate_y.Name = "rotate_y";
            this.rotate_y.Size = new System.Drawing.Size(75, 23);
            this.rotate_y.TabIndex = 1;
            this.rotate_y.Text = "Rotate Y";
            this.rotate_y.UseVisualStyleBackColor = true;
            this.rotate_y.Click += new System.EventHandler(this.RotateY_Click);
            // 
            // rotate_z
            // 
            this.rotate_z.Location = new System.Drawing.Point(12, 70);
            this.rotate_z.Name = "rotate_z";
            this.rotate_z.Size = new System.Drawing.Size(75, 23);
            this.rotate_z.TabIndex = 2;
            this.rotate_z.Text = "Rotate Z";
            this.rotate_z.UseVisualStyleBackColor = true;
            this.rotate_z.Click += new System.EventHandler(this.RotateZ_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 575);
            this.Controls.Add(this.rotate_z);
            this.Controls.Add(this.rotate_y);
            this.Controls.Add(this.rotate_x);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button rotate_x;
        private System.Windows.Forms.Button rotate_y;
        private System.Windows.Forms.Button rotate_z;
    }
}


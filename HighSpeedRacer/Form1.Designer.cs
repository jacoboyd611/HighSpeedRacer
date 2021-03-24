namespace HighSpeedRacer
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
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.insturctionLabel = new System.Windows.Forms.Label();
            this.countDown = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // insturctionLabel
            // 
            this.insturctionLabel.AutoSize = true;
            this.insturctionLabel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.insturctionLabel.Font = new System.Drawing.Font("Segoe Print", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insturctionLabel.Location = new System.Drawing.Point(250, 131);
            this.insturctionLabel.Name = "insturctionLabel";
            this.insturctionLabel.Size = new System.Drawing.Size(378, 33);
            this.insturctionLabel.TabIndex = 0;
            this.insturctionLabel.Text = "Press space to start or escape to quit";
            // 
            // countDown
            // 
            this.countDown.Interval = 1000;
            this.countDown.Tick += new System.EventHandler(this.countDown_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(884, 548);
            this.Controls.Add(this.insturctionLabel);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "High Speed Racer";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label insturctionLabel;
        private System.Windows.Forms.Timer countDown;
    }
}


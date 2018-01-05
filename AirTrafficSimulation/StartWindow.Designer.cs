namespace AirTrafficSimulation
{
    partial class StartScreen
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
            this.StartSimulation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StartSimulation
            // 
            this.StartSimulation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartSimulation.Location = new System.Drawing.Point(210, 77);
            this.StartSimulation.Name = "StartSimulation";
            this.StartSimulation.Size = new System.Drawing.Size(116, 62);
            this.StartSimulation.TabIndex = 0;
            this.StartSimulation.Text = "Start simulation";
            this.StartSimulation.UseVisualStyleBackColor = true;
            this.StartSimulation.Click += new System.EventHandler(this.StartSimulation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bernard MT Condensed", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 44);
            this.label1.TabIndex = 1;
            this.label1.Text = "Air Traffic Simulation";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 487);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartSimulation);
            this.Name = "StartScreen";
            this.Text = "Air Traffic Simulation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartSimulation;
        private System.Windows.Forms.Label label1;
    }
}


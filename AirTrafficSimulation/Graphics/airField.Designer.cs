namespace AirTrafficSimulation
{
    partial class airField
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
            this.tmrMove = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrMove
            // 
            this.tmrMove.Enabled = true;
            this.tmrMove.Interval = 50;
            this.tmrMove.Tick += new System.EventHandler(this.tmrMove_Tick);
            // 
            // airField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(2480, 1224);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "airField";
            this.Text = "Airfield Sim";
            this.Load += new System.EventHandler(this.airField_Load_1);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.airField_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.airField_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrMove;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirTrafficSimulation
{
    public partial class airField : Form
    {
        private PictureBox picbox;

        private int x;
        private int y;
        private int size;
        private Position objPos;

        private int halfformHeight, halfformWidth;
        private int taxiWidth, taxiLength;
        private int runWayLength, runWayWidth, zebraWidth, zebraLength, linespace, runWayLength2;


        Bitmap plane = new Bitmap("airplane.png");
        

        enum Position
        {
            Left, Right, Up, Down
        }

        private void airfield_Load(object sender, System.EventArgs e)
        {
            // Dock the PictureBox to the form and set its background to white.
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            picbox.Dock = DockStyle.Fill;
            picbox.BackColor = Color.White;
            // Connect the Paint event of the PictureBox to the event handler method.
            picbox.Paint += new System.Windows.Forms.PaintEventHandler(this.airField_Paint);

            // Add the PictureBox control to the Form.
            this.Controls.Add(picbox);
        }


        public airField()
        {
            InitializeComponent();
            

            x = 50;
            y = 50;
            objPos = Position.Down;
            
            plane.RotateFlip(RotateFlipType.Rotate270FlipX);
        }

        private void runway_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void airField_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.FillEllipse(Brushes.DarkBlue, x, y, 100, 50);
            //e.Graphics.DrawImage(new Bitmap("airplane.png"), x, y, 32, 32);

            
            halfformHeight = (this.Height / 2);
            halfformWidth = (this.Width / 2);
            runWayLength = (this.Width * 79)/100;
            runWayWidth = (this.Height * 17) / 100;
            runWayLength2 = (this.Height - 60); //(this.Height * 94)/100;
            zebraWidth = runWayWidth / 12;
            zebraLength = runWayLength / 10;
            linespace = (runWayWidth - runWayWidth/12) / 7 ;
            
            size = (runWayWidth/10) * 5;

            taxiLength = runWayLength / 2;
            taxiWidth = runWayWidth / 2;



            //Taxiways
            e.Graphics.FillRectangle(Brushes.LightGray, 30, (this.Height - 30 - taxiWidth), runWayLength, taxiWidth);
            e.Graphics.FillRectangle(Brushes.LightGray, 30, (this.Height - runWayLength2-30), runWayLength, taxiWidth);
            e.Graphics.FillRectangle(Brushes.LightGray, runWayLength - taxiWidth +30 , 30, taxiWidth, runWayLength2);
            e.Graphics.FillRectangle(Brushes.LightGray, 30, 30, taxiWidth, runWayLength2);


            //Runways
            // North South
            e.Graphics.FillRectangle(Brushes.SlateGray, halfformWidth - (halfformWidth / 5), 30, runWayWidth, runWayLength2); 
            // East West
            e.Graphics.FillRectangle(Brushes.SlateGray, 30 , halfformHeight - (halfformHeight / 5 ), runWayLength, runWayWidth);

            // Hangar where planes go to die :D

            e.Graphics.FillRectangle(Brushes.DarkRed, 30 + runWayLength - (2*taxiWidth ) + taxiWidth/2, (this.Height - 30  - taxiWidth - taxiWidth/2), taxiWidth * 2, taxiWidth * 2);
            




            //e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight + (2 * linespace), zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight + linespace, zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight, zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight - linespace, zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight - (2 * linespace), zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight - (3 * linespace), zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight - (4 * linespace), zebraLength, zebraWidth);
            // e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight - (5 * linespace), zebraLength, zebraWidth);


            // e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);

            for (int i = 2; i<11; i++)
            {
                e.Graphics.FillRectangle(Brushes.White, i * 100, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
                if (i< 7 && i != 4) e.Graphics.FillRectangle(Brushes.White, halfformWidth - (halfformWidth / 9) , i * 100, halfformWidth / 100, halfformWidth / 12);

            }

            //e.Graphics.FillRectangle(Brushes.White, 200, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
            //e.Graphics.FillRectangle(Brushes.White, 300, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
            //e.Graphics.FillRectangle(Brushes.White, 400, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
            //e.Graphics.FillRectangle(Brushes.White, 500, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
            //e.Graphics.FillRectangle(Brushes.White, 600, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
            //e.Graphics.FillRectangle(Brushes.White, 700, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
            //e.Graphics.FillRectangle(Brushes.White, 800, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
            //e.Graphics.FillRectangle(Brushes.White, 900, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
            //e.Graphics.FillRectangle(Brushes.White, 1000, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);

            for (int i = 0; i<5; i++)
            {
                if (i < 3)
                {
                    //west-east
                    e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight + (i * linespace), zebraLength, zebraWidth);
                    e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight + (i * linespace), zebraLength, zebraWidth);
                }

                e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight - (i * linespace), zebraLength, zebraWidth);
                e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight - (i * linespace ), zebraLength, zebraWidth);
            }

            for (int i = 0; i<7; i++)
            {
                e.Graphics.FillRectangle(Brushes.White, halfformWidth - ((1 + i) * linespace + 10), 50, zebraWidth, zebraLength);
                e.Graphics.FillRectangle(Brushes.White, halfformWidth - ((1 + i) * linespace + 10), (this.Height - 50 - zebraLength), zebraWidth, zebraLength);
            }

            //e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight + (2 * linespace), zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight + linespace, zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight, zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight - linespace, zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight - (2 * linespace), zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight - (3 * linespace), zebraLength, zebraWidth);
            //e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight - (4 * linespace), zebraLength, zebraWidth);
            airPlane(e, x, y, size); 
            

            




        }

        private void airField_Load_1(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);
        }

        private void tmrMove_Tick(object sender, EventArgs e)
        {
            if (objPos == Position.Right)
            {
                x += halfformWidth / 15;
            }
            else if ( objPos == Position.Left)
            {
                x -= halfformWidth / 15; //take off and landing speed
            }
            else if (objPos == Position.Up)
            {
                y -= halfformHeight / 40; // taxi speed
            }
            else if (objPos == Position.Down)
            {
                y += halfformHeight / 40;
            }

            Invalidate();
        }

        private void airField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) objPos = Position.Left;
            else if (e.KeyCode == Keys.Right) objPos = Position.Right;
            else if (e.KeyCode == Keys.Up) objPos = Position.Up;
            else if (e.KeyCode == Keys.Down) objPos = Position.Down;
        }

        private void airPlane(PaintEventArgs e, int x, int y, int size)
        {
            
            e.Graphics.DrawImage(plane , x, y, size, size);
        }

        public bool isOnRunway(int x, int y)
        {
            return (((x > (halfformWidth - halfformWidth / 5) && (x < ((halfformWidth - halfformWidth / 5) + runWayWidth)) &&
            (y > 30 && y < (30 + runWayLength2)))) || ((x > 30 && x < 30 + runWayLength) &&
                (y > (halfformHeight - (halfformHeight / 5)) && y < (halfformHeight - (halfformHeight / 5)) + runWayWidth)));       
        }

        public bool isInHangar(int x, int y)
        {
            return ((x > (30 + runWayLength - (2 * taxiWidth) + taxiWidth / 2) && x < (30 + runWayLength - (2 * taxiWidth) + taxiWidth / 2) + taxiWidth * 2)
                && (y > this.Height - 30 - taxiWidth - taxiWidth / 2 && y < (this.Height - 30 - taxiWidth - taxiWidth / 2) + taxiWidth * 2));
        }

        public bool isOnTaxiWay(int x, int y)
        {
            return (x > 30 && x < 30+runWayLength && y > (this.Height - 30 - taxiWidth)+30 && y < (this.Height - 30 - taxiWidth)+taxiWidth) 
                || (x > 30 && x < 30+runWayLength && y > (this.Height - runWayLength2 - 30) && y < (this.Height - runWayLength2 - 30)+taxiWidth)
                || (x > runWayLength - taxiWidth + 30 && x < (runWayLength - taxiWidth + 30)+taxiWidth && y > 30 && y < 30+runWayLength2)
                || (x > 30 && x < 30+taxiWidth && y > 30 && y < 30+runWayLength2);
        }
    }
}

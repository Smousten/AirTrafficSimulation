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
    public partial class AirField : Form
    {
        private PictureBox picbox;

        private int x;
        private int y;
        private int size;
        private Position objPos;

        private int halfformHeight, halfformWidth;
        private int taxiWidth, taxiLength;
        private int runWayLength, runWayWidth, zebraWidth, zebraLength, linespace, runWayLength2;

        private int taxiPosXL, taxiPosXR, taxiPosYU, taxiPosYL;

        private int startX, startY;

        private int[] plainPos = new int[] { 0, 0 };


        private int speed, taxiSpeed, takeoffSpeed;
        private bool takingOff, taxiing, takingOffOnRW;
        private int rw;


        Bitmap plane = new Bitmap("airplane.png");


        enum Position
        {
            Left, Right, Up, Down
        }

        public AirField()
        {
            InitializeComponent();
            startX = 30 + (taxiWidth / 2);
            startY = startX;
            taxiSpeed = 10;
            takeoffSpeed = 15;
            takingOff = false;
            taxiing = true;
            takingOffOnRW = false;
            rw = 90;
            speed = taxiSpeed;


            plainPos[0] = plainPos[1] = startX;

            x = 50;
            y = 50;
            objPos = Position.Right;

            plane.RotateFlip(RotateFlipType.Rotate270FlipX);
        }

        private void airField_Paint(object sender, PaintEventArgs e)
        {



            halfformHeight = (this.Height / 2);
            halfformWidth = (this.Width / 2);
            runWayLength = (this.Width * 79) / 100;
            runWayWidth = (this.Height * 17) / 100;
            runWayLength2 = (this.Height - 60); //(this.Height * 94)/100;
            zebraWidth = runWayWidth / 12;
            zebraLength = runWayLength / 10;
            linespace = (runWayWidth - runWayWidth / 12) / 7;



            taxiLength = runWayLength / 2;
            taxiWidth = runWayWidth / 2;

            taxiPosXL = taxiPosYU = startX;
            taxiPosXR = 30 + runWayLength - (taxiWidth);
            taxiPosYL = (this.Height - 30 - taxiWidth);






            //Taxiways
            e.Graphics.FillRectangle(Brushes.LightGray, 30, (this.Height - 30 - taxiWidth), runWayLength, taxiWidth);
            e.Graphics.FillRectangle(Brushes.LightGray, 30, (this.Height - runWayLength2 - 30), runWayLength, taxiWidth);
            e.Graphics.FillRectangle(Brushes.LightGray, runWayLength - taxiWidth + 30, 30, taxiWidth, runWayLength2);
            e.Graphics.FillRectangle(Brushes.LightGray, 30, 30, taxiWidth, runWayLength2);

            //Runways
            // North South
            e.Graphics.FillRectangle(Brushes.SlateGray, halfformWidth - (halfformWidth / 5), 30, runWayWidth, runWayLength2);
            // East West
            e.Graphics.FillRectangle(Brushes.SlateGray, 30, halfformHeight - (halfformHeight / 5), runWayLength, runWayWidth);



            for (int i = 2; i < 11; i++)
            {
                e.Graphics.FillRectangle(Brushes.White, i * 100, halfformHeight - (halfformHeight / 5) + (halfformWidth / 10), halfformWidth / 12, halfformWidth / 100);
                if (i < 7 && i != 4) e.Graphics.FillRectangle(Brushes.White, halfformWidth - (halfformWidth / 9), i * 100, halfformWidth / 100, halfformWidth / 12);

            }

            for (int i = 0; i < 5; i++)
            {
                if (i < 3)
                {
                    //west-east
                    e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight + (i * linespace), zebraLength, zebraWidth);
                    e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight + (i * linespace), zebraLength, zebraWidth);
                }

                e.Graphics.FillRectangle(Brushes.White, 1100, halfformHeight - (i * linespace), zebraLength, zebraWidth);
                e.Graphics.FillRectangle(Brushes.White, 50, halfformHeight - (i * linespace), zebraLength, zebraWidth);
            }

            for (int i = 0; i < 7; i++)
            {
                e.Graphics.FillRectangle(Brushes.White, halfformWidth - ((1 + i) * linespace + 10), 50, zebraWidth, zebraLength);
                e.Graphics.FillRectangle(Brushes.White, halfformWidth - ((1 + i) * linespace + 10), (this.Height - 50 - zebraLength), zebraWidth, zebraLength);
            }

            if (taxiing) taxiPos();
            if (takingOff) takeOff(rw);

            if (takingOffOnRW)
            {
                if (rw == 90 && plainPos[0] > (30 + runWayLength / 2)) size += 10;
                if (rw == 27 && plainPos[0] < (30 + runWayLength / 2)) size += 10;
                if (rw == 18 && plainPos[1] > (30 + runWayLength2 / 2)) size += 10;
                if (rw == 0 && plainPos[1] < (30 + runWayLength2 / 2)) size += 10;
            }

            airPlane(e, plainPos[0], plainPos[1], size);

            // Hangar where planes go to die :D

            e.Graphics.FillRectangle(Brushes.DarkRed, 30 + runWayLength - (2 * taxiWidth) + taxiWidth / 2, (this.Height - 30 - taxiWidth - taxiWidth / 2), taxiWidth * 2, taxiWidth * 2);


        }

        private void takeOff(int rw)
        {
            switch (rw)
            {
                case (90):
                    if (plainPos[0] == 30 && plainPos[1] <= (halfformHeight - (halfformHeight / 5) + (runWayWidth / 4) + 10))
                    {
                        plainPos[1] = (halfformHeight - (halfformHeight / 5) + (runWayWidth / 4) + 10);
                        speed = takeoffSpeed;
                        objPos = Position.Right;
                        plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                        takingOffOnRW = true;

                    }
                    break;
                case (27):
                    if (plainPos[0] == taxiPosXR && plainPos[1] >= (halfformHeight - (halfformHeight / 5) + (runWayWidth / 4) + 10))
                    {
                        plainPos[1] = (halfformHeight - (halfformHeight / 5) + (runWayWidth / 4) + 10);
                        speed = takeoffSpeed;
                        objPos = Position.Left;
                        plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                        takingOffOnRW = true;

                    }
                    break;

                case (18):
                    if ((plainPos[0] >= (halfformWidth - (halfformWidth / 5)) + (runWayWidth / 4) + 10) && plainPos[1] == 30)
                    {
                        plainPos[0] = (halfformWidth - (halfformWidth / 5)) + (runWayWidth / 4) + 10;
                        speed = takeoffSpeed;
                        objPos = Position.Down;
                        plane.RotateFlip(RotateFlipType.Rotate90FlipX);
                        takingOffOnRW = true;

                    }
                    break;

                case (0):
                    if ((plainPos[0] <= (halfformWidth - (halfformWidth / 5)) + (runWayWidth / 4) + 10) && plainPos[1] == taxiPosYL)
                    {
                        plainPos[0] = (halfformWidth - (halfformWidth / 5)) + (runWayWidth / 4) + 10;
                        speed = takeoffSpeed;
                        objPos = Position.Up;
                        plane.RotateFlip(RotateFlipType.Rotate90FlipX);
                        takingOffOnRW = true;

                    }
                    break;

                default:
                    break;
            }
        }

        private void taxiPos()
        {
            size = (runWayWidth / 10) * 5;
            if ((objPos == Position.Right) && plainPos[0] >= taxiPosXR)
            {
                if (plainPos[1] >= taxiPosYU)
                {
                    objPos = Position.Down;
                    plane.RotateFlip(RotateFlipType.Rotate90FlipX);

                }
                else
                {
                    objPos = Position.Up;
                    plane.RotateFlip(RotateFlipType.Rotate270FlipX);

                }
                plainPos[0] = taxiPosXR;
                if (rw == 27)
                {
                    taxiing = false;
                    takingOff = true;
                }
            }

            else if ((objPos == Position.Left) && plainPos[0] <= taxiPosXL)
            {
                if (plainPos[1] <= taxiPosYU)
                {
                    objPos = Position.Down;
                    plane.RotateFlip(RotateFlipType.Rotate270FlipX);

                }
                else
                {
                    objPos = Position.Up;
                    plane.RotateFlip(RotateFlipType.Rotate90FlipX);
                }
                plainPos[0] = taxiPosXL;
                if (rw == 90)
                {
                    taxiing = false;
                    takingOff = true;
                }
            }

            else if ((objPos == Position.Down) && plainPos[1] >= taxiPosYL)
            {
                if (plainPos[0] == taxiPosXR)
                {
                    objPos = Position.Left;
                    plainPos[1] = taxiPosYL;
                    plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                }
                else
                {
                    objPos = Position.Right;
                    plainPos[1] = taxiPosYU;
                    plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                }
                plainPos[1] = taxiPosYL;
                if (rw == 0)
                {
                    taxiing = false;
                    takingOff = true;
                }
            }

            else if ((objPos == Position.Up) && plainPos[1] <= taxiPosYU)
            {
                if (plainPos[0] == taxiPosXR)
                {
                    objPos = Position.Left;
                    plainPos[0] = taxiPosXL;
                    plane.RotateFlip(RotateFlipType.Rotate90FlipX);
                }
                else
                {
                    objPos = Position.Right;
                    plainPos[1] = taxiPosXR;
                    plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                }
                plainPos[1] = taxiPosYU;
                if (rw == 18)
                {
                    taxiing = false;
                    takingOff = true;
                }
            }

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

            // div with 100 for taxi and div with 15 for takeoff
            if (objPos == Position.Right)
            {
                plainPos[0] += halfformWidth / speed;
            }
            else if (objPos == Position.Left)
            {
                plainPos[0] -= halfformWidth / speed; //take off and landing speed
            }
            else if (objPos == Position.Up)
            {
                plainPos[1] -= halfformHeight / speed; // taxi speed
            }
            else if (objPos == Position.Down)
            {
                plainPos[1] += halfformHeight / speed;
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

            e.Graphics.DrawImage(plane, x, y, size, size);
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
            return (x > 30 && x < 30 + runWayLength && y > (this.Height - 30 - taxiWidth) + 30 && y < (this.Height - 30 - taxiWidth) + taxiWidth)
                || (x > 30 && x < 30 + runWayLength && y > (this.Height - runWayLength2 - 30) && y < (this.Height - runWayLength2 - 30) + taxiWidth)
                || (x > runWayLength - taxiWidth + 30 && x < (runWayLength - taxiWidth + 30) + taxiWidth && y > 30 && y < 30 + runWayLength2)
                || (x > 30 && x < 30 + taxiWidth && y > 30 && y < 30 + runWayLength2);
        }


    }
}

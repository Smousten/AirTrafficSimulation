﻿using dotSpace.Interfaces.Space;
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

        private int x;
        private int y;
        private int size;
        private Position objPos;

        private static int halfformHeight, halfformWidth;
        private int taxiWidth, taxiLength;
        private int  zebraWidth, zebraLength, linespace;
        private static int runWayLength, runWayLength2, runWayWidth;

        private static int taxiPosXL, taxiPosXR, taxiPosYU, taxiPosYL;

        private int startX, startY;

        private int[] plainPos = new int[] { 0, 0 };


        private int speed, taxiSpeed, takeoffSpeed;
        private bool takingOff, taxiing, takingOffOnRW;
        private int rw;

        private static int[] boundaries;


        List<singleplain> plains = new List<singleplain>();


        //private Bitmap plane = new Bitmap("airplane.png");

        singleplain testplain;
        singleplain testplain2 , testplain3, testplain4;

       


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


            //plainPos[0] = plainPos[1] = startX;

            //x = 50;
            //y = 50;
            //objPos = Position.Right;

            //plane.RotateFlip(RotateFlipType.Rotate270FlipX);
            testplain = new singleplain("plain1", 0, 90, 2);
            testplain2 = new singleplain("plain2", 0, 18, 2);
            testplain3 = new singleplain("plain3", 0, 27, 2);
            testplain4 = new singleplain("plain4", 0, 0, 3);
            plains.Add(testplain);
            plains.Add(testplain2);
            plains.Add(testplain3);
            plains.Add(testplain4);
        }

        //public void run()
        //{
        //    InitializeComponent();
        //    startX = 30 + (taxiWidth / 2);
        //    startY = startX;
        //    taxiSpeed = 10;
        //    takeoffSpeed = 15;
        //    takingOff = false;
        //    taxiing = true;
        //    takingOffOnRW = false;
        //    rw = 90;
        //    speed = taxiSpeed;


        //    //plainPos[0] = plainPos[1] = startX;

        //    //x = 50;
        //    //y = 50;
        //    //objPos = Position.Right;

        //    //plane.RotateFlip(RotateFlipType.Rotate270FlipX);
        //    testplain = new singleplain("plain1", 0, 90, 2);
        //    testplain2 = new singleplain("plain2", 0, 18, 2);
        //    testplain3 = new singleplain("plain3", 0, 27, 2);
        //    testplain4 = new singleplain("plain4", 0, 0, 3);
        //    plains.Add(testplain);
        //    plains.Add(testplain2);
        //    plains.Add(testplain3);
        //    plains.Add(testplain4);
        //}

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
            boundaries = new int[] { taxiPosXL, taxiPosXR, taxiPosYL, taxiPosYU };





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


            //zebras
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

            
            airPlane(e);


            // Hangar where planes go to die :D

            e.Graphics.FillRectangle(Brushes.DarkRed, 30 + runWayLength - (2 * taxiWidth) + taxiWidth / 2, (this.Height - 30 - taxiWidth - taxiWidth / 2), taxiWidth * 2, taxiWidth * 2);



            

        }

        public static int getHalfformWidth()
        {
            return halfformWidth;
        }

        public static int getHalfformHeight()
        {
            return halfformHeight;
        }

        public static int getRunwayL(int w)
        {
            if (w == 1) return runWayLength;
            return runWayLength2;
        }

        public static int getRunwayW()
        {
            return runWayWidth;
        }

        public static int[] getBoundaries()
        {
            return boundaries;
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
            foreach (singleplain prop in plains)
            {
                prop.updatePosition();
                //// div with 100 for taxi and div with 15 for takeoff
                //if (prop.getObjPos() == 1)
                //{
                //    //plainPos[0] += halfformWidth / speed;
                //    prop.updatePosition(halfformWidth / prop.getSpeed(), 0);
                //}
                //else if (prop.getObjPos() == 2)
                //{
                //    //plainPos[0] -= halfformWidth / speed; //take off and landing speed
                //    prop.updatePosition(-(halfformWidth / prop.getSpeed()), 0);
                //}
                //else if (prop.getObjPos() == 3)
                //{
                //    //plainPos[1] -= halfformHeight / speed; // taxi speed
                //    prop.updatePosition(0, -(halfformHeight / prop.getSpeed()));
                //}
                //else if (prop.getObjPos() == 4)
                //{
                //    //plainPos[1] += halfformHeight / speed;
                //    prop.updatePosition(0, halfformHeight / prop.getSpeed());
                //}
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

        private void airPlane(PaintEventArgs e)
        {
            foreach (singleplain prop in plains)
            {
                e.Graphics.DrawImage(prop.getImg(), prop.getPos(0), prop.getPos(1), prop.getSize(), prop.getSize());
            }
           // e.Graphics.DrawImage(plane, testplain.getPos(0), testplain.getPos(1), testplain.getSize(), testplain.getSize());
        }

        public static int getHangarX()
        {
            return taxiPosXR;
        }

        public static int getHangarY()
        {
            return taxiPosYL;
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

    public class singleplain
    {
        private String name;
        private int size, x, y;
        private int runwayLength, runwayLength2, runwayWidth, rw, taxiSpeed, takeoffSpeed, speed;
        private int[] boundaries;
        

        private bool takingOff, taxiing, takingOffOnRW;

        private Bitmap plane = new Bitmap("airplane.png");

        

        private Position objPos;

        enum Position
        {
            Left, Right, Up, Down
        }

        public singleplain(String name, int size, int rw , int direction)
        {
            this.name = name;
            this.size = size;
            this.x = AirField.getHangarX();
            this.y = AirField.getHangarY();
            this.runwayLength = AirField.getRunwayL(1);
            this.runwayLength2 = AirField.getRunwayL(2);
            this.runwayWidth = AirField.getRunwayW();
            
            
            this.takingOff = false;
            this.taxiing = true;
            this.takingOffOnRW = false;
            this.rw = rw;
            this.taxiSpeed = 100/6;
            this.takeoffSpeed = 15;
            this.speed = taxiSpeed;

            

            if (direction == 1)
            {
                objPos = Position.Right;
                //plane.RotateFlip(RotateFlipType.Rotate270FlipX);
            }
            else if (direction == 2)
            {
                objPos = Position.Left;
                //plane.RotateFlip(RotateFlipType.Rotate90FlipX);
            }
            else if (direction == 3)
            {
                objPos = Position.Up;
                //plane.RotateFlip(RotateFlipType.Rotate270FlipX);
            }
            else if (direction == 4)
            {
                objPos = Position.Down;
                //plane.RotateFlip(RotateFlipType.Rotate90FlipX);
            }


            //plane.RotateFlip(RotateFlipType.Rotate270FlipX);
        }

        public void updatePosition()
        {
            //if (objPos == Position.Right) return 1;
            //if (objPos == Position.Left) return 2;
            //if (objPos == Position.Up) return 3;
            //if (objPos == Position.Down) return 4;
            // div with 100 for taxi and div with 15 for takeoff
            if (objPos == Position.Right)
            {
                //plainPos[0] += halfformWidth / speed;
                this.x += AirField.getHalfformWidth() / this.speed;
            }
            else if (objPos == Position.Left)
            {
                //plainPos[0] -= halfformWidth / speed; //take off and landing speed
                this.x -= AirField.getHalfformWidth() / this.speed;
            }
            else if (objPos == Position.Up)
            {
                //plainPos[1] -= halfformHeight / speed; // taxi speed
                this.y -= AirField.getHalfformHeight() / this.speed;
            }
            else if (objPos == Position.Down)
            {
                //plainPos[1] += halfformHeight / speed;
                this.y += AirField.getHalfformHeight() / this.speed;
            }


            //this.x += newx;
            //this.y += newy;
            if (taxiing) taxiPos();
            if (takingOff) takeOff(this.rw);

            if (takingOffOnRW)
            {
                if (rw == 90 && this.x > (30 + AirField.getRunwayL(1)/ 2)) size += 10;
                if (rw == 27 && this.x < (30 + AirField.getRunwayL(1) / 2)) size += 10;
                if (rw == 18 && this.y > (30 + AirField.getRunwayL(2) / 2)) size += 10;
                if (rw == 0 && this.y < (30 + AirField.getRunwayL(2) / 2)) size += 10;
            }
            
        }

        public Bitmap getImg()
        {
            Bitmap re = new Bitmap("airplane.png");
            if (this.objPos == Position.Right) re.RotateFlip(RotateFlipType.Rotate90FlipNone);
            else if (this.objPos == Position.Down) re.RotateFlip(RotateFlipType.Rotate180FlipNone);
            else if (this.objPos == Position.Left) re.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return re;
        }

        public int getPos(int what)
        {
            if (what == 0) return this.x;
            return this.y;
        }

        public int getObjPos()
        {
            if (objPos == Position.Right) return 1;
            if (objPos == Position.Left) return 2;
            if (objPos == Position.Up) return 3;
            if (objPos == Position.Down) return 4;
            return 0;
        }

        public int getSpeed()
        {
            return this.speed;
        }

        private void taxiPos()
        {
            
            this.boundaries = AirField.getBoundaries();
            this.size = (AirField.getRunwayW() / 10) * 5;
            this.speed = this.taxiSpeed;
            if ((this.objPos == Position.Right) && this.x >= this.boundaries[1])
            {
                if (this.y == boundaries[3])
                {
                    this.objPos = Position.Down;
                    //this.plane.RotateFlip(RotateFlipType.Rotate90FlipX);

                }
                else
                {
                    this.objPos = Position.Up;
                    //this.plane.RotateFlip(RotateFlipType.Rotate270FlipX);

                }
                this.x = this.boundaries[1];
                if (rw == 27)
                {
                    this.taxiing = false;
                    this.takingOff = true;
                }
            }

            else if ((this.objPos == Position.Left) && this.x <= this.boundaries[0])
            {
                if (this.y == this.boundaries[3])
                {
                    this.objPos = Position.Down;
                    
                    //this.plane.RotateFlip(RotateFlipType.Rotate270FlipX);

                }
                else
                {
                    this.objPos = Position.Up;
                    //this.plane.RotateFlip(RotateFlipType.Rotate90FlipX);
                }
                this.x = this.boundaries[0];
                if (rw == 90)
                {
                    this.taxiing = false;
                    this.takingOff = true;
                }
            }

            else if ((objPos == Position.Down) && this.y >= this.boundaries[2])
            {
                if (this.x == this.boundaries[1])
                {
                    this.objPos = Position.Left;
                    
                   // this.plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                }
                else
                {
                    this.objPos = Position.Right;
                    
                    //this.plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                }
                this.y = this.boundaries[2];
                if (rw == 0)
                {
                    this.taxiing = false;
                    this.takingOff = true;
                }
            }

            else if ((this.objPos == Position.Up) && this.y <= this.boundaries[3])
            {
                if (this.x == this.boundaries[1])
                {
                    this.objPos = Position.Left;
                    this.x = this.boundaries[1];
                    //this.plane.RotateFlip(RotateFlipType.Rotate90FlipX);
                }
                else
                {
                    this.objPos = Position.Right;
                    this.x = this.boundaries[0];
                   // this.plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                }
                this.y = this.boundaries[3];
                if (rw == 18)
                {
                    this.taxiing = false;
                    this.takingOff = true;
                }
            }

        }


        private void takeOff(int rw)
        {
            //if (this.x == 30 && this.y <= (AirField.getHalfformHeight() - (AirField.getHalfformHeight() / 5) + (AirField.getRunwayW() / 4) + 10))
            //{
            //    Console.WriteLine("lala");
            //}
            switch (rw)
            {                    
                case (90):
                    if (this.x == 30 && this.y <= (AirField.getHalfformHeight() - (AirField.getHalfformHeight() / 5) + (AirField.getRunwayW() / 4) + 10) 
                        && objPos == Position.Up)
                    {
                        this.y = (AirField.getHalfformHeight() - (AirField.getHalfformHeight() / 5) + (AirField.getRunwayW() / 4) + 10);
                        speed = takeoffSpeed;
                        objPos = Position.Right;
                        plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                        takingOffOnRW = true;

                    }
                    else if (this.x == 30 && this.y >= (AirField.getHalfformHeight() - (AirField.getHalfformHeight() / 5) + (AirField.getRunwayW() / 4) + 10)
                        && objPos == Position.Down)
                    {
                        this.y = (AirField.getHalfformHeight() - (AirField.getHalfformHeight() / 5) + (AirField.getRunwayW() / 4) + 10);
                        speed = takeoffSpeed;
                        objPos = Position.Right;
                        plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                        takingOffOnRW = true;

                    }
                    break;
                case (27):
                    if (this.x == boundaries[1] && (this.y >= (AirField.getHalfformHeight() - (AirField.getHalfformHeight() / 5) + (AirField.getRunwayW() / 4) + 10))
                        && objPos == Position.Down)
                    {
                        this.y = (AirField.getHalfformHeight() - (AirField.getHalfformHeight() / 5) + (AirField.getRunwayW() / 4) + 10);
                        speed = takeoffSpeed;
                        objPos = Position.Left;
                        plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                        takingOffOnRW = true;

                    }
                    else if (this.x == boundaries[1] && (this.y <= (AirField.getHalfformHeight() - (AirField.getHalfformHeight() / 5) + (AirField.getRunwayW() / 4) + 10))
                        && objPos == Position.Up)
                    {
                        this.y = (AirField.getHalfformHeight() - (AirField.getHalfformHeight() / 5) + (AirField.getRunwayW() / 4) + 10);
                        speed = takeoffSpeed;
                        objPos = Position.Left;
                        plane.RotateFlip(RotateFlipType.Rotate270FlipX);
                        takingOffOnRW = true;
                    }
                    break;

                case (18):
                    if ((this.x >= (AirField.getHalfformWidth() - (AirField.getHalfformWidth() / 5)) + (AirField.getRunwayW() / 4) + 10) && this.y == 30
                        && objPos == Position.Right)
                    {
                        this.x = (AirField.getHalfformWidth() - (AirField.getHalfformWidth() / 5)) + (AirField.getRunwayW() / 4) + 10;
                        speed = takeoffSpeed;
                        objPos = Position.Down;
                        plane.RotateFlip(RotateFlipType.Rotate90FlipX);
                        takingOffOnRW = true;

                    }
                    else if ((this.x <= (AirField.getHalfformWidth() - (AirField.getHalfformWidth() / 5)) + (AirField.getRunwayW() / 4) + 10) && this.y == 30
                        && objPos == Position.Left)
                    {
                        this.x = (AirField.getHalfformWidth() - (AirField.getHalfformWidth() / 5)) + (AirField.getRunwayW() / 4) + 10;
                        speed = takeoffSpeed;
                        objPos = Position.Down;
                        plane.RotateFlip(RotateFlipType.Rotate90FlipX);
                        takingOffOnRW = true;

                    }
                    break;

                case (0):
                    if ((this.x <= (AirField.getHalfformWidth() - (AirField.getHalfformWidth() / 5)) + (AirField.getRunwayW() / 4) + 10) && this.y == boundaries[2]
                        && objPos == Position.Left)
                    {
                        this.x = (AirField.getHalfformWidth() - (AirField.getHalfformWidth() / 5)) + (AirField.getRunwayW() / 4) + 10;
                        speed = takeoffSpeed;
                        objPos = Position.Up;
                        plane.RotateFlip(RotateFlipType.Rotate90FlipX);
                        takingOffOnRW = true;

                    }
                    else if ((this.x >= (AirField.getHalfformWidth() - (AirField.getHalfformWidth() / 5)) + (AirField.getRunwayW() / 4) + 10) && this.y == boundaries[2]
                        && objPos == Position.Right)
                    {
                        this.x = (AirField.getHalfformWidth() - (AirField.getHalfformWidth() / 5)) + (AirField.getRunwayW() / 4) + 10;
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

        public int getSize()
        {
            return this.size;
        }
    }


}

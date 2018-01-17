using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirTrafficSimulation.Graphics
{
    class DrawAirfield
    {

        private int halfformHeight, halfformWidth;
        private int taxiWidth, taxiLength;
        private int runWayLength, runWayWidth, zebraWidth, zebraLength, linespace, runWayLength2;
        private int taxiPosXL, taxiPosXR, taxiPosYU, taxiPosYL;


        
        private int startX, startY;
        private int[] plainPos = new int[] { 0, 0 };
        private int speed, taxiSpeed, takeoffSpeed;
        private int size;

        private bool takingOff, taxiing, takingOffOnRW, landingOnRW;



        private int rw;


        public DrawAirfield (PaintEventArgs e, int height, int width)
        {

            startX = 30 + (taxiWidth / 2);
            startY = startX;
            taxiSpeed = 100;
            takeoffSpeed = 25;
            takingOff = false;
            taxiing = true;
            takingOffOnRW = false;
            landingOnRW = false;
            rw = 90;
            speed = taxiSpeed;


            plainPos[0] = plainPos[1] = startX;


            halfformHeight = (height / 2);
            halfformWidth = (width / 2);
            runWayLength = (width * 79) / 100;
            runWayWidth = (height * 17) / 100;
            runWayLength2 = (height - 60); //(this.Height * 94)/100;
            zebraWidth = runWayWidth / 12;
            zebraLength = runWayLength / 10;
            linespace = (runWayWidth - runWayWidth / 12) / 7;



            taxiLength = runWayLength / 2;
            taxiWidth = runWayWidth / 2;

            taxiPosXL = taxiPosYU = startX;
            taxiPosXR = 30 + runWayLength - (taxiWidth);
            taxiPosYL = (height - 30 - taxiWidth);

            //Taxiways
            e.Graphics.FillRectangle(Brushes.LightGray, 30, (height - 30 - taxiWidth), runWayLength, taxiWidth);
            e.Graphics.FillRectangle(Brushes.LightGray, 30, (height - runWayLength2 - 30), runWayLength, taxiWidth);
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
                e.Graphics.FillRectangle(Brushes.White, halfformWidth - ((1 + i) * linespace + 10), (height - 50 - zebraLength), zebraWidth, zebraLength);
            }

            e.Graphics.FillRectangle(Brushes.DarkRed, 30 + runWayLength - (2 * taxiWidth) + taxiWidth / 2, (height - 30 - taxiWidth - taxiWidth / 2), taxiWidth * 2, taxiWidth * 2);


        }
    }
}

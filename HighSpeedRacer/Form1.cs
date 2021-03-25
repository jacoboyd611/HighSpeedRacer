using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace HighSpeedRacer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Variables   
        float score = 0.0F;
        float highScore = 0.0F;

        int speed = 70;

        int dashSpace = 0;
        
        int dashesWidth = 10;
        int dashesHeight = 30;

        int carX = 360;
        int carSpeed = 20;

        int centerLane = 360;
        int leftLane = 150;
        int rightLane = 575;

        int closestLane = 0;

        int counter = 0;
        bool running = false;

        bool rightDown = false;
        bool leftDown = false;

        List<int> rightDashesY = new List<int>();
        List<int> leftDashesY = new List<int>();

        Pen blackPen = new Pen(Color.Black, 50);
        Pen whitePen = new Pen(Color.White, 10);


        Font drawFont = new Font("Seoge Print", 40, FontStyle.Bold | FontStyle.Italic);
        Font smallFont = new Font("Seoge Print", 15, FontStyle.Bold | FontStyle.Italic);

        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush purpleBrush = new SolidBrush(Color.Purple);
        SolidBrush orangeBrush = new SolidBrush(Color.OrangeRed);
        SolidBrush greenBrush = new SolidBrush(Color.Green);

        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blueBrush = new SolidBrush(Color.Indigo);

        #endregion

        private void Timer1_Tick(object sender, EventArgs e)
        {
            #region Move dashes
            if (rightDashesY.Count == 0 || dashSpace == 8)
            {
                rightDashesY.Add(150);
                leftDashesY.Add(0);
                dashSpace = 0;
            }

            for (int i = 0; i < rightDashesY.Count; i++)
            {
                rightDashesY[i] += 20;
                if (rightDashesY[i] > this.Height + 100) { rightDashesY.RemoveAt(i); }
            }

            for (int i = 0; i < leftDashesY.Count; i++)
            {
                leftDashesY[i] += 20;
                if (leftDashesY[i] > this.Height + 100) { leftDashesY.RemoveAt(i); }
            }
            #endregion

            #region Move player
            if (running)
            {
                if (rightDown)
                {
                    carX += carSpeed;
                    if (carX >= closestLane)
                    {
                        rightDown = false;
                    }
                }

                if (leftDown)
                {
                    carX -= carSpeed;
                    if (carX <= closestLane)
                    {
                        leftDown = false;
                    }
                }
            }

            #endregion

            dashSpace++;
            Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (running)
                    {
                        //decide what lane to end in
                        if (carX < centerLane) { closestLane = centerLane; }
                        else { closestLane = rightLane; }
                        rightDown = true;

                        //prevents from moving outside the lanes
                        if (carX > rightLane) { rightDown = false; }
                    }
                    break;
                    

                case Keys.Left:
                    if (running)
                    {
                        //decide what lane to end in
                        if (carX > centerLane) { closestLane = centerLane; }
                        else { closestLane = leftLane; }
                        leftDown = true;

                        //prevents from moving outside the lanes
                        if (carX < leftLane) { leftDown = false; }
                    }
                    break;

                case Keys.Space:
                    //running = true;
                    if (running == false)
                    {
                        countDown.Enabled = true;
                        insturctionLabel.Visible = false;
                        introLabel.Visible = false;
                    }
                    break;

                case Keys.Escape:
                    this.Close();
                    break;
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            #region Road
            e.Graphics.RotateTransform(20);
            e.Graphics.FillRectangle(blackBrush, 300, 0, 50, this.Height);
            e.Graphics.FillRectangle(blackBrush, 350, 0, 50, this.Height);
            e.Graphics.FillRectangle(blackBrush, 400, 0, 50, this.Height);

            e.Graphics.RotateTransform(-40);
            e.Graphics.FillRectangle(blackBrush, 475, 200, 50, this.Height);
            e.Graphics.FillRectangle(blackBrush, 425, 200, 50, this.Height);
            e.Graphics.FillRectangle(blackBrush, 375, 200, 50, this.Height);

            e.Graphics.RotateTransform(20);
            e.Graphics.FillRectangle(blackBrush, 250, 200, 350, this.Height);

            e.Graphics.RotateTransform(-10);

            for (int i = 0; i < rightDashesY.Count; i++)
            {
                e.Graphics.FillRectangle(whiteBrush, 450, rightDashesY[i], dashesWidth, dashesHeight);
            }
            e.Graphics.RotateTransform(+20);
            for (int i = 0; i < leftDashesY.Count; i++)
            {
                e.Graphics.FillRectangle(whiteBrush, 400, leftDashesY[i], dashesWidth, dashesHeight);
            }
            e.Graphics.RotateTransform(-10);
            #endregion

            //sky
            e.Graphics.FillRectangle(blueBrush, 0, 0, this.Width, 200);

            //background

            //car
            e.Graphics.DrawImage(Properties.Resources.car, carX, 460, 150, 75);

            if (running)
            {
                //draw obstacles


                //draw score
                if (score > 1) { e.Graphics.DrawString($"{score.ToString("0.00")}km", drawFont, whiteBrush, 325, 60); }
                else { e.Graphics.DrawString($"{score.ToString("0.000")}km", drawFont, whiteBrush, 325, 60); }

                if (highScore > 1) { e.Graphics.DrawString($"High Score = {highScore.ToString("0.00")}km", smallFont, yellowBrush, 10, 10); }
                else { e.Graphics.DrawString($"High Score = {highScore.ToString("0.000")}km", smallFont, yellowBrush, 10, 10); }

            }
            else
            {
                #region Title
                int startingPoint = 275;
                e.Graphics.DrawString("S", drawFont, purpleBrush, startingPoint, 60);
                e.Graphics.DrawString("P", drawFont, orangeBrush, startingPoint + 25, 60);
                e.Graphics.DrawString("E", drawFont, yellowBrush, startingPoint + 50, 60);
                e.Graphics.DrawString("E", drawFont, yellowBrush, startingPoint + 75, 60);
                e.Graphics.DrawString("D", drawFont, purpleBrush, startingPoint + 100, 60);

                e.Graphics.DrawString("R", drawFont, orangeBrush, startingPoint + 150, 60);
                e.Graphics.DrawString("A", drawFont, yellowBrush, startingPoint + 175, 60);
                e.Graphics.DrawString("C", drawFont, purpleBrush, startingPoint + 200, 60);
                e.Graphics.DrawString("E", drawFont, yellowBrush, startingPoint + 230, 60);
                e.Graphics.DrawString("R", drawFont, orangeBrush, startingPoint + 255, 60);
                e.Graphics.DrawString("S", drawFont, purpleBrush, startingPoint + 280, 60);               
                #endregion
            }

            #region 3 2 1 count down
            if (counter == 1) { e.Graphics.DrawString("3", drawFont, purpleBrush, 400, 300); }
            else if (counter == 2) { e.Graphics.DrawString("2", drawFont, orangeBrush, 400, 300); }
            else if (counter == 3) { e.Graphics.DrawString("1", drawFont, yellowBrush, 400, 300); }
            else if (counter == 4) 
            { 
                e.Graphics.DrawString("GO", drawFont, greenBrush, 375, 300);
                running = true;
            }
            else if (counter == 5)
            {
                countDown.Enabled = false;
                counter = 0;
                scoreTimer.Enabled = true;
            }
            #endregion
        }

        private void countDown_Tick(object sender, EventArgs e)
        {
            counter++;
        }

        private void scoreTimer_Tick(object sender, EventArgs e)
        {
            score += 0.001F;
            if (score > highScore) { highScore = score; }
        }
    }
}

///Jacob Boyd
///Mr. T
///ICS3U
///April 6th
///8bit style subway surfers/temple run game were the player must dodge traffic coming towards them in 3 different lanes


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
using System.IO;

namespace HighSpeedRacer
{
    public partial class Form1 : Form
    {
        System.Windows.Media.MediaPlayer raceStart = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer engine = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer backMedia = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer skid = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer crash = new System.Windows.Media.MediaPlayer();

        public Form1()
        {
            InitializeComponent();
            raceStart.Open(new Uri(Application.StartupPath + "/Resources/raceStart.wav"));
            engine.Open(new Uri(Application.StartupPath + "/Resources/engine.wav"));

            backMedia.Open(new Uri(Application.StartupPath + "/Resources/music.wav"));
            backMedia.MediaEnded += new EventHandler(backMedia_MediaEnded);

            crash.Open(new Uri(Application.StartupPath + "/Resources/crash.wav"));
            skid.Open(new Uri(Application.StartupPath + "/Resources/skid.wav"));
        }

        #region Variables   
        float score = 0.0F;
        float highScore = 0.0F;
        float lastScore = 0.0F;

        int speed = 70;

        int dashSpace = 0;
        int cloudSpace = 0;
        int lampPostSpace = 0;

        int dashesWidth = 10;
        int dashesHeight = 30;

        int carX = 360;
        int carSpeed = 20;

        int leftObstacleX = 260;
        int leftObstacleY = 150;
        int left = 0;

        int centerObstacleX = 395;
        int centerObstacleY = 150;
        int centerOb = 1;

        int rightObstacleX = 485;
        int rightObstacleY = 150;
        int right = 2;

        int centerLane = 360;
        int leftLane = 150;
        int rightLane = 575;

        int closestLane = 0;

        int counter = 0;
        int obstacleCounter = 0;
        int obstacleFrenq = 50;

        bool running = false;
        bool endScreen = false;
        bool dashes = true;

        bool rightDown = false;
        bool leftDown = false;

        bool leftObstacle = false;
        bool rightObstacle = false;
        bool centerObstacle = false;

        bool music = false;

        List<int> rightDashesY = new List<int>();
        List<int> leftDashesY = new List<int>();

        List<int> cloudX = new List<int>();
        List<int> cloudY = new List<int>();

        List<int> lampPostY = new List<int>();
        List<int> lampPostX = new List<int>();
        List<int> lampPostHeight = new List<int>();
        List<int> lampPostWidth = new List<int>();

        int[] obstacleHeight = { 50, 50, 50 };
        int[] obstacleWidth = { 100, 70, 100 };
        Random randGen = new Random();

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
        SolidBrush blueBrush = new SolidBrush(Color.DarkBlue);

        //SoundPlayer startRace = new SoundPlayer(Properties.Resources.raceStart);
        //SoundPlayer engine = new SoundPlayer(Properties.Resources.engine);

        #endregion

        private void backMedia_MediaEnded(object sender, EventArgs e)
        {
            backMedia.Stop();
            backMedia.Play();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (music == false)
            {
                //background music
                backMedia.Play();
                music = true;
            }

            #region Move dashes and lamp post
            if (dashes)
            {
                //lamp posts
                if (lampPostX.Count == 0 || lampPostSpace == 40)
                {
                    lampPostX.Add(225);
                    lampPostY.Add(100);
                    lampPostHeight.Add(100);
                    lampPostWidth.Add(25);
                    lampPostSpace = 0;
                }

                for (int i = 0; i < lampPostX.Count; i++)
                {
                    lampPostX[i] -= 3;
                    lampPostY[i] += 3;
                    lampPostWidth[i] += 1;
                    lampPostHeight[i] += 3;

                    if (lampPostHeight[i] > this.Height)
                    {
                        lampPostX.RemoveAt(i);
                        lampPostY.RemoveAt(i);
                        lampPostWidth.RemoveAt(i);
                        lampPostHeight.RemoveAt(i);
                    }
                }

                //dashes
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
            }
            #endregion

            #region Clouds
            if (cloudX.Count == 0 || cloudSpace == 100)
            {
                cloudY.Add(randGen.Next(10, 100));
                cloudX.Add(-200);
                cloudSpace = 0;
            }
            for (int i = 0; i < cloudX.Count; i++)
            {
                cloudX[i] += 5;
                if (cloudX[i] > this.Width)
                {
                    cloudX.RemoveAt(i);
                    cloudY.RemoveAt(i);
                }
            }
            #endregion

            if (running)
            {

                #region Move player
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
                #endregion

                #region Deicde lane and frenquency
                if (obstacleCounter >= obstacleFrenq)
                {
                    int i = randGen.Next(1, 4);
                    if(i == 1)
                    {
                        leftObstacle = true;
                        obstacleCounter = 0;
                    }
                    if (i == 2)
                    {
                        rightObstacle = true;
                        obstacleCounter = 0;
                    }
                    if (i == 3)
                    {
                        centerObstacle = true;
                        obstacleCounter = 0;
                    }
                }
                else { obstacleCounter++; }
                #endregion

                #region Obstacle movement 
                if (leftObstacle)
                {
                    leftObstacleX -= 2;
                    leftObstacleY += 5;
                    obstacleHeight[left] += 1;
                    obstacleWidth[left] += 1;
                }

                if (centerObstacle)
                {
                    centerObstacleY += 6;
                    centerObstacleX -= 1; 
                    obstacleHeight[centerOb] += 1;
                    obstacleWidth[centerOb] += 2;
                }

                if (rightObstacle)
                {
                    rightObstacleX += 2;
                    rightObstacleY += 5;
                    obstacleHeight[right] += 1;
                    obstacleWidth[right] += 1;
                }

                //reset after they leave screen
                if (leftObstacle && leftObstacleY > this.Height)
                {
                    leftObstacle = false;
                    leftObstacleX = 260;
                    leftObstacleY = 150;
                    obstacleHeight[left] = 50;
                    obstacleWidth[left] = 100;
                }

                if (centerObstacle && centerObstacleY > this.Height)
                {
                    centerObstacle = false;
                    centerObstacleX = 395;
                    centerObstacleY = 150;
                    obstacleHeight[centerOb] = 50;
                    obstacleWidth[centerOb] = 70;
                }

                if (rightObstacle && rightObstacleY > this.Height)
                {
                    rightObstacle = false;
                    rightObstacleX = 485;
                    rightObstacleY = 150;
                    obstacleHeight[right] = 50;
                    obstacleWidth[right] = 100;
                }


                #endregion

                #region Intersections and clear for end screen
                Rectangle playerRec = new Rectangle(carX, 460 + 30, 150, 40);
                Rectangle leftRec = new Rectangle(leftObstacleX, leftObstacleY + 50, obstacleWidth[left], 40);
                Rectangle centerRec = new Rectangle(centerObstacleX, centerObstacleY + 50, obstacleWidth[centerOb], 40);
                Rectangle rightRec = new Rectangle(rightObstacleX, rightObstacleY + 50, obstacleWidth[right], 40);

                if (playerRec.IntersectsWith(leftRec) || playerRec.IntersectsWith(centerRec) || playerRec.IntersectsWith(rightRec))
                {
                    skid.Stop();
                    skid.Play();

                    crash.Stop();
                    crash.Play();

                    running = false;
                    endScreen = true;
                    scoreTimer.Enabled = false;
                    dashes = false;

                    leftObstacle = false;
                    leftObstacleX = 260;
                    leftObstacleY = 150;
                    obstacleHeight[left] = 50;
                    obstacleWidth[left] = 100;

                    centerObstacle = false;
                    centerObstacleX = 395;
                    centerObstacleY = 150;
                    obstacleHeight[centerOb] = 50;
                    obstacleWidth[centerOb] = 70;

                    rightObstacle = false;
                    rightObstacleX = 485;
                    rightObstacleY = 150;
                    obstacleHeight[right] = 50;
                    obstacleWidth[right] = 100;
                    lastScore = 0;
                    obstacleFrenq = 50;
                }
                #endregion

                //increase car frequncy 
                if (score > lastScore + 0.2)
                {
                    obstacleFrenq -= 5;
                    speed = speed + 5;
                    lastScore = score;
                }
            }
            dashSpace++;
            cloudSpace++;
            lampPostSpace++;
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
                    if (endScreen)
                    {
                        //changes from endscreen back to main menu
                        dashes = true;
                        endScreen = false;
                        insturctionLabel.Visible = true;
                        introLabel.Visible = true;
                        carX = 360;
                        score = 0;
                    }
                    else if (running == false)
                    {
                        //start game
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

            //moon
            e.Graphics.DrawImage(Properties.Resources.moon, 750, 20, 75, 75);

            //clouds
            for (int i = 0; i < cloudX.Count; i++)
            {
                e.Graphics.DrawImage(Properties.Resources.cloud, cloudX[i], cloudY[i], 150, 25);
            }

            //lamp posts
            for (int i = lampPostX.Count - 1; i > -1; i--)
            {
                e.Graphics.DrawImage(Properties.Resources.lampost, lampPostX[i], lampPostY[i], lampPostWidth[i], lampPostHeight[i]);
            }

            //car
            e.Graphics.DrawImage(Properties.Resources.car2, carX, 460, 150, 75);

            if (running)
            {
                #region Draw obstacles
                if (leftObstacle)
                {
                    e.Graphics.DrawImage(Properties.Resources.car, leftObstacleX, leftObstacleY, obstacleWidth[left], obstacleHeight[left]);
                }

                if (centerObstacle)
                {
                    e.Graphics.DrawImage(Properties.Resources.car, centerObstacleX, centerObstacleY, obstacleWidth[centerOb], obstacleHeight[centerOb]);
                }

                if (rightObstacle)
                {
                    e.Graphics.DrawImage(Properties.Resources.car, rightObstacleX, rightObstacleY, obstacleWidth[right], obstacleHeight[right]);
                }
                #endregion

                //draw score
                if (score > 1) { e.Graphics.DrawString($"{score.ToString("0.00")}km", drawFont, whiteBrush, 325, 60); }
                else { e.Graphics.DrawString($"{score.ToString("0.000")}km", drawFont, whiteBrush, 325, 60); }

                if (highScore > 1) { e.Graphics.DrawString($"High Score = {highScore.ToString("0.00")}km", smallFont, yellowBrush, 10, 10); }
                else { e.Graphics.DrawString($"High Score = {highScore.ToString("0.000")}km", smallFont, yellowBrush, 10, 10); }

            }
            else
            {
                #region draw title
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
                if (endScreen)
                {
                    //Output final 
                    if (highScore > 1) { e.Graphics.DrawString($"High Score = {highScore.ToString("0.00")}km", smallFont, yellowBrush, 330, 125); }
                    else { e.Graphics.DrawString($"High Score = {highScore.ToString("0.000")}km", smallFont, yellowBrush, 330, 125); }

                    if (highScore > 1) { e.Graphics.DrawString($"Your score = {score.ToString("0.00")}km", smallFont, whiteBrush, 330, 150); }
                    else { e.Graphics.DrawString($"Your score = {score.ToString("0.000")}km", smallFont, whiteBrush, 330, 150); }
                    e.Graphics.DrawString($"Press space to continue", smallFont, whiteBrush, 330, 250);
                }
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
            //start countdown timer
            counter++;
            if (counter < 5)
            {                
                raceStart.Stop();
                raceStart.Play();
                if (counter == 4)
                {
                    engine.Stop();
                    engine.Play();
                }
            }
        }

        private void scoreTimer_Tick(object sender, EventArgs e)
        {
            //score counter
            score += 0.002F;
            if (score > highScore) { highScore = score; }
        }
    }
}

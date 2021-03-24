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
        int dashSpace = 0;

        int dashesWidth = 10;
        int dashesHeight = 30;

        List<int> rightDashesY = new List<int>();
        List<int> leftDashesY = new List<int>();

        Pen blackPen = new Pen(Color.Black, 50);
        Pen whitePen = new Pen(Color.White, 10);

        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blueBrush = new SolidBrush(Color.LightSkyBlue);

        #endregion

        private void Timer1_Tick(object sender, EventArgs e)
        {

            if (rightDashesY.Count == 0 || dashSpace == 20)
            {
                rightDashesY.Add(150);
                leftDashesY.Add(0);
                dashSpace = 0;
            }

            for (int i = 0; i < rightDashesY.Count; i++)
            {
                rightDashesY[i] += 5;
                if (rightDashesY[i] > this.Height + 100) { rightDashesY.RemoveAt(i); }
            }

            for (int i = 0; i < leftDashesY.Count; i++)
            {
                leftDashesY[i] += 5;
                if (leftDashesY[i] > this.Height + 100) { leftDashesY.RemoveAt(i); }
            }
            dashSpace++;
            Refresh();
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
            #endregion

            #region
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

            //car
            e.Graphics.DrawImage(Properties.Resources.car, 50, 20, 200, 400);
        }
    }
}

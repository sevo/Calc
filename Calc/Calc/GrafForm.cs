using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPlot;

namespace Calc
{
    public partial class GrafForm : Form
    {
        private Boolean grafOpened;

        public GrafForm(string funkcia, ref Boolean grafOpened)
        {   
            InitializeComponent();
            AddFunkcia(funkcia);
            this.grafOpened = grafOpened;
            DrawGraf();
        }
        ~GrafForm()
        {
            grafOpened = false;
        }
        public GrafForm( ref bool grafOpened)
        {
            InitializeComponent();
            this.grafOpened = grafOpened;
            DrawGraf();
        }

        public void AddFunkcia(string funkcia)
        {
            if (funkcia[0] == 'y' || funkcia[0] == 'Y')
            {
                funkcia=funkcia.Remove(0, 1);
                funkcia=funkcia.Insert(0, "f(x)");
            }
            checkedListBox1.Items.Add(funkcia, true);
        }

        private void yMaxTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                decimal diffY = YMaxNumericUpDown.Value - YMinNumericUpDown.Value;
                decimal diffX = XMaxNumericUpDown.Value - XMinNumericUpDown.Value;
                decimal diffZ = ZMaxNumericUpDown.Value - ZMinNumericUpDown.Value;
             
                YMaxNumericUpDown.Value = YMaxNumericUpDown.Value + diffY / 2;
                YMinNumericUpDown.Value = YMinNumericUpDown.Value - diffY / 2;                
                XMaxNumericUpDown.Value = XMaxNumericUpDown.Value + diffX / 2;
                XMinNumericUpDown.Value = XMinNumericUpDown.Value - diffX / 2;
                ZMaxNumericUpDown.Value = ZMaxNumericUpDown.Value + diffZ / 2;
                ZMinNumericUpDown.Value = ZMinNumericUpDown.Value - diffZ / 2;
            }
            catch (Exception err) { }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void DrawGraf()
        {
            /*GrafSurface2D.XAxis1 = new Axis();
            GrafSurface2D.YAxis1 = new Axis();
            GrafSurface2D.Add(new HorizontalLine(0.0, Color.LightGray));*/
            //GrafSurface2D.Add(new VerticalLine(0.0, Color.LightGray));
            string[] lines = {
                "Circular Example. Demonstrates - ",
                "  * PiAxis, Horizontal and Vertical Lines.",
                "  * Placement of legend" };

            //infoBox.Lines = lines;

            this.GrafSurface2D.Clear();
            GrafSurface2D.Add(new HorizontalLine(0.0, Color.LightGray));
            GrafSurface2D.Add(new VerticalLine(0.0, Color.LightGray));

            const int N = 400;
            /*const double start = -Math.PI * 7.0;
            const double end = Math.PI * 7.0;

            double[] xs = new double[N];
            double[] ys = new double[N];

            for (int i = 0; i < N; ++i)
            {
                double t = ((double)i * (end - start) / (double)N + start);
                xs[i] = 0.5 * (t - 2.0 * Math.Sin(t));
                ys[i] = 2.0 * (1.0 - 2.0 * Math.Cos(t));
            }*/
            double start = (double)XMinNumericUpDown.Value;
            double end = (double)XMaxNumericUpDown.Value;

            double[] xs = new double[N];
            double[] ys = new double[N];
            double[] ys2 = new double[N];
            
            double x = start;
            for (int i = 0; i < N; ++i)
            { 
                double t = ((double)i * (end - start) / (double)N + start);
                xs[i] = x;
                ys[i] = Math.Cos(x);
                ys2[i] = Math.Sin(x);
                x += ((end - start) / N);
            }

            LinePlot lp = new LinePlot(ys, xs);
            lp.Pen = new Pen(Color.DarkBlue, 2.0f);
            lp.Label = "g(x)=cos(x)"; // no legend, but still useful for copy data to clipboard.
            GrafSurface2D.Add(lp);

            LinePlot lp2 = new LinePlot(ys2, xs);
            lp2.Pen = new Pen(Color.Red, 2.0f);
            lp2.Label = "f(x)=sin(x)"; // no legend, but still useful for copy data to clipboard.
            GrafSurface2D.Add(lp2);

            VerticalLine line = new VerticalLine((double)XnumericUpDown.Value, new Pen(Color.Black, 2.0f));
            GrafSurface2D.Add(line);

            //GrafSurface2D.XAxis1 = new PiAxis(GrafSurface2D.XAxis1);
            GrafSurface2D.XAxis1 = new LinearAxis(GrafSurface2D.XAxis1);
               
            GrafSurface2D.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            GrafSurface2D.Legend = new Legend();
            GrafSurface2D.Legend.AttachTo(PlotSurface2D.XAxisPosition.Bottom, PlotSurface2D.YAxisPosition.Right);
            GrafSurface2D.Legend.HorizontalEdgePlacement = Legend.Placement.Inside;
            GrafSurface2D.Legend.VerticalEdgePlacement = Legend.Placement.Inside;
            GrafSurface2D.Legend.XOffset = -10;
            GrafSurface2D.Legend.YOffset = -10;
            GrafSurface2D.Legend.NeverShiftAxes = true;

            GrafSurface2D.Inner.XAxis1.WorldMax = (double)XMaxNumericUpDown.Value;
            GrafSurface2D.Inner.XAxis1.WorldMin = (double)XMinNumericUpDown.Value;
            GrafSurface2D.Inner.YAxis1.WorldMax = (double)YMaxNumericUpDown.Value;
            GrafSurface2D.Inner.YAxis1.WorldMin = (double)YMinNumericUpDown.Value;
            
            GrafSurface2D.Refresh();
            //Point bod = new Point(-10, -2);
            //Point bod1 = new Point((int)GrafSurface2D.Inner.XAxis1.WorldMin, (int)GrafSurface2D.Inner.YAxis1.WorldMin);
            //Point bod2 = new Point((int)GrafSurface2D.Inner.XAxis1.WorldMax, (int)GrafSurface2D.Inner.YAxis1.WorldMax);
            //functionDeclaration.Text = GrafSurface2D.RectangleToClient(GrafSurface2D.DisplayRectangle).Left.ToString() + "," + GrafSurface2D.RectangleToClient(GrafSurface2D.DisplayRectangle).Right.ToString() + ";" + GrafSurface2D.RectangleToScreen(GrafSurface2D.DisplayRectangle).Top.ToString() + "," + GrafSurface2D.RectangleToScreen(GrafSurface2D.DisplayRectangle).Bottom.ToString();
            //functionDeclaration.Text = GrafSurface2D.In;
        
            //functionDeclaration.Text = GrafSurface2D.PointToScreen(bod).X.ToString() +","+ GrafSurface2D.PointToScreen(bod).Y.ToString();
            //functionDeclaration.Text = GrafSurface2D.PointToClient(bod1).X.ToString() + "," + GrafSurface2D.PointToClient(bod1).Y.ToString() + ";" + GrafSurface2D.PointToClient(bod2).X.ToString() + "," + GrafSurface2D.PointToClient(bod2).Y.ToString();

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Add(functionDeclaration.Text);
        }

        private void YMinNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DrawGraf();
        }

        private void YMaxNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DrawGraf();
        }

        private void XMinNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DrawGraf();
        }

        private void ZMinNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DrawGraf();
        }

        private void XMaxNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DrawGraf();
        }

        private void ZMaxNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DrawGraf();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            DrawGraf();
            if (XnumericUpDown.Value > XMaxNumericUpDown.Value) XnumericUpDown.Value = (decimal)XMaxNumericUpDown.Value;
            if (XnumericUpDown.Value < XMinNumericUpDown.Value) XnumericUpDown.Value = (decimal)XMinNumericUpDown.Value;
            
            xTrackBar.Value = (int)(((double)XnumericUpDown.Value - (double)XMinNumericUpDown.Value) * 400 / ((double)XMaxNumericUpDown.Value - (double)XMinNumericUpDown.Value));

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DrawGraf();
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                decimal diffY = YMaxNumericUpDown.Value - YMinNumericUpDown.Value;
                decimal diffX = XMaxNumericUpDown.Value - XMinNumericUpDown.Value;
                decimal diffZ = ZMaxNumericUpDown.Value - ZMinNumericUpDown.Value;

                YMaxNumericUpDown.Value = YMaxNumericUpDown.Value - diffY / 4;
                YMinNumericUpDown.Value = YMinNumericUpDown.Value + diffY / 4;
                XMaxNumericUpDown.Value = XMaxNumericUpDown.Value - diffX / 4;
                XMinNumericUpDown.Value = XMinNumericUpDown.Value + diffX / 4;
                ZMaxNumericUpDown.Value = ZMaxNumericUpDown.Value - diffZ / 4;
                ZMinNumericUpDown.Value = ZMinNumericUpDown.Value + diffZ / 4;
            }
            catch(Exception err) { }
        }

        private void GrafSurface2D_Click(object sender, EventArgs e)
        {

        }

        private void xTrackBar_Scroll(object sender, EventArgs e)
        {
            XnumericUpDown.Value = (decimal)((double)XMinNumericUpDown.Value + (((double)xTrackBar.Value / 400) * ((double)XMaxNumericUpDown.Value - (double)XMinNumericUpDown.Value)));           
        }

    }

}

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
        private bool grafOpened;
        public GrafForm(string funkcia, ref bool grafOpened)
        {   
            InitializeComponent();
            AddFunkcia(funkcia);
            this.grafOpened = grafOpened;
            DrawGraf();
        }
        public GrafForm( ref bool grafOpened)
        {
            InitializeComponent();
            this.grafOpened = grafOpened;
            DrawGraf();
        }
        ~GrafForm()
        {
            grafOpened = false;
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
            const double start = -10.0;
            const double end = 10.0;

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
            
            GrafSurface2D.Refresh();
            
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Add(functionDeclaration.Text);
        }

    }

}

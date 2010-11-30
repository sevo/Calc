using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPlot;
using System.Threading;

namespace Calc
{
    public partial class GrafForm : Form
    {
        private static Mutex functionsMutex = new Mutex(false, "functionsMutex");
        private int N = 100; 
        private Boolean grafOpened;
        private Form1 mainWindow;
        private ArrayList functions = new ArrayList();
        private String drawFunction="";

        public GrafForm(string funkcia, ref Boolean grafOpened, Form1 main_window)
        {   
            InitializeComponent();
            mainWindow = main_window;

            functions = mainWindow.functions;
            if (functions.Count != 0)
            { 
                for (int i = 0; i < functions.Count; i++)
                {
                    checkedListBox.Items.Add(((Function)functions[i]).label, true);
                }
            }

            if (functions.Count != 0)
            {
                Thread t = new Thread(resetValuesThread);
                System.Threading.Timer timer = new System.Threading.Timer(mainWindow.abortGettingResult, t, checkedListBox.Items.Count * 1000, Timeout.Infinite);
                t.Start();
                t.Join();
            }

            AddFunkcia(funkcia);

            this.grafOpened = grafOpened;
            DrawGraf();
        }
        ~GrafForm()
        {
            mainWindow.functions = functions;

            grafOpened = false;
        }
        public GrafForm(ref bool grafOpened, Form1 main_window)
        {
            InitializeComponent();
            mainWindow = main_window;

            functions = mainWindow.functions;
            if (functions.Count != 0)
            {
                for (int i = 0; i < functions.Count; i++)
                {
                    checkedListBox.Items.Add(((Function)functions[i]).label, true);
                }
            }
            if (functions.Count != 0)
            {
                Thread t = new Thread(resetValuesThread);
                System.Threading.Timer timer = new System.Threading.Timer(mainWindow.abortGettingResult, t, checkedListBox.Items.Count * 1000, Timeout.Infinite);
                t.Start();
                t.Join();
            }

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
            //checkedListBox.Items.Add(funkcia, true);
            functionDeclaration.Text = funkcia;

            Thread t = new Thread(DefineFunctionThread);
            System.Threading.Timer timer = new System.Threading.Timer(mainWindow.abortGettingResult, t, 100, Timeout.Infinite);
            t.Start();
            t.Join();

            if (!functionDeclaration.BackColor.Equals(Color.Red))
            {
                String function = functionDeclaration.Text;
                String body = function.Substring(function.IndexOf("=") + 1, function.Length - function.IndexOf("=") - 1);
                drawFunction = body;
                Thread t1 = new Thread(getValuesThread);
                System.Threading.Timer timer1 = new System.Threading.Timer(mainWindow.abortGettingResult, t1, 1000, Timeout.Infinite);
                t1.Start();
                t1.Join();
            }
            DrawGraf();
            updateListBox();


        }

        public int DefineFunction(String function)
        {
            String body = "";
            body = function.Substring(function.IndexOf("=") + 1, function.Length - function.IndexOf("=") - 1);
            if (body == "")//return -1 if function is wrong
            {
                return -1;
            }
            mainWindow.bcIn.WriteLine("define run(x) {return " + body + "};3");
            mainWindow.bcOut.ReadLine();//if bc returns something, function was accepted, so we can retur success
            return 1;
        }

        public void DefineFunctionThread()
        {
            try
            {
                if (DefineFunction(functionDeclaration.Text) != 1)
                    functionDeclaration.BackColor = Color.Red;
            }
            catch (ThreadAbortException err)//if the function has incorrect syntax, exeption is raised
            {
                functionDeclaration.BackColor = Color.Red;
            }
        }

        double[] getValues(string function)
        {
            double[] values = new double[N+1];

            String run = "define run(x) {return " + function + "};3";
            mainWindow.bcIn.WriteLine(run);
            mainWindow.bcOut.ReadLine();//if bc returns something, function was accepted, so we can retur success
            String frun = "frun(" + XMinNumericUpDown.Value.ToString() + "," + (((double)XMaxNumericUpDown.Value - (double)XMinNumericUpDown.Value) / (double)(N)).ToString().Replace(",", ".") + "," + (N+1).ToString() + ")";
            mainWindow.bcIn.WriteLine(frun);

            for (int i = 0; i < N+1; i++)
            {
                try
                {
                    values[i] = float.Parse(mainWindow.bcOut.ReadLine().Replace(".", ","));//the last line won't be in result
                }
                catch (FormatException err) { values[i] = 0; }
            }
            mainWindow.bcOut.ReadLine();
            return values;
           
        }

        void resetValuesThread() //tread to run gc to get values of function
        {
            try
            {
                functionsMutex.WaitOne();
                for (int i = 0; i < checkedListBox.Items.Count; i++)
                {
                    Function funkcia = (Function)functions[i];
                    String body = funkcia.label.Substring(funkcia.label.IndexOf("=") + 1, funkcia.label.Length - funkcia.label.IndexOf("=") - 1);          
                    funkcia.data = getValues(body);
                    functions.RemoveAt(i); 
                    functions.Insert(i,funkcia);
                }
                functionsMutex.ReleaseMutex();
            }
            catch (ThreadAbortException err)
            {
                functionDeclaration.BackColor = Color.Red;
                functionsMutex.ReleaseMutex();
                return;
            }
            
        }

        void getValuesThread() //tread to run gc to get values of function
        {
            try
            {
                functionsMutex.WaitOne();
                Random random = new Random();
                Function funkcia = new Function(N+1);
                funkcia.label = functionDeclaration.Text;
                funkcia.colorR = (random.Next(230) + random.Next(230)) / 2;
                funkcia.colorG = (random.Next(230) + random.Next(230)) / 2;
                funkcia.colorB = (random.Next(230) + random.Next(230)) / 2;
                funkcia.data = getValues(drawFunction);
                functions.Add(funkcia);
                functionsMutex.ReleaseMutex();
            }
            catch (ThreadAbortException err)
            {
                functionDeclaration.BackColor = Color.Red;
                functionsMutex.ReleaseMutex();
                return;
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
                Thread t = new Thread(DefineFunctionThread);
                System.Threading.Timer timer = new System.Threading.Timer(mainWindow.abortGettingResult, t, 100, Timeout.Infinite);
                t.Start();
                t.Join();


                if (!functionDeclaration.BackColor.Equals(Color.Red))
                {
                    String function = functionDeclaration.Text;
                    String body = function.Substring(function.IndexOf("=") + 1, function.Length - function.IndexOf("=") - 1);
                    drawFunction = body;
                    Thread t1 = new Thread(getValuesThread);
                    System.Threading.Timer timer1 = new System.Threading.Timer(mainWindow.abortGettingResult, t1, 1000, Timeout.Infinite);
                    t1.Start();
                    t1.Join();
                    if (!functionDeclaration.BackColor.Equals(Color.Red))
                    {
                        checkedListBox.Items.Add(functionDeclaration.Text, true);
                    }
                }
                DrawGraf();
                updateListBox();
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
            lock (typeof(GrafForm))
            {
                string[] lines = {
                "Circular Example. Demonstrates - ",
                "  * PiAxis, Horizontal and Vertical Lines.",
                "  * Placement of legend" };

                this.GrafSurface2D.Clear();
                GrafSurface2D.Add(new HorizontalLine(0.0, Color.LightGray));
                GrafSurface2D.Add(new VerticalLine(0.0, Color.LightGray));

                double start = (double)XMinNumericUpDown.Value;
                double end = (double)XMaxNumericUpDown.Value;
                Random random = new Random();
                double[] xs = new double[N+1];

                double x = start;
                for (int i = 0; i < N+1; i++)
                {

                    xs[i] = x;
                    x += ((end - start) / (N));//mozno tu treba N+1
                }
                functionsMutex.WaitOne();
                for (int i = 0; i < functions.Count; i++)
                {
                    if (checkedListBox.GetItemChecked(i))
                    {
                        LinePlot lp = new LinePlot(((Function)functions[i]).data, xs);
                        lp.Pen = new Pen(Color.FromArgb(((Function)functions[i]).colorR, ((Function)functions[i]).colorG, ((Function)functions[i]).colorB), 2.0f);
                        lp.Label = ((Function)functions[i]).label; // no legend, but still useful for copy data to clipboard.
                        GrafSurface2D.Add(lp);
                    }
                }
                functionsMutex.ReleaseMutex();

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
               }
        }       

        private void YMinNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (YMinNumericUpDown.Value >= YMaxNumericUpDown.Value) { YMinNumericUpDown.Value = YMaxNumericUpDown.Value - 1; return; }
            DrawGraf();
        }

        private void YMaxNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (YMaxNumericUpDown.Value <= YMinNumericUpDown.Value) { YMaxNumericUpDown.Value = YMinNumericUpDown.Value + 1; return; }
            if (checkedListBox.Items.Count == 0) return;
            Thread t = new Thread(resetValuesThread);
            System.Threading.Timer timer = new System.Threading.Timer(mainWindow.abortGettingResult, t, checkedListBox.Items.Count*1000, Timeout.Infinite);
            t.Start();
            t.Join();
            DrawGraf();
        }

        private void XMinNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (XMinNumericUpDown.Value >= XMaxNumericUpDown.Value) { XMinNumericUpDown.Value = XMaxNumericUpDown.Value - 1; return; }
            if (checkedListBox.Items.Count == 0) { DrawGraf(); return; }
            Thread t = new Thread(resetValuesThread);
            System.Threading.Timer timer = new System.Threading.Timer(mainWindow.abortGettingResult, t, checkedListBox.Items.Count * 1000, Timeout.Infinite);
                t.Start();
                t.Join();
                DrawGraf();
        }

        private void ZMinNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DrawGraf();
        }

        private void XMaxNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (XMaxNumericUpDown.Value <= XMinNumericUpDown.Value) { XMaxNumericUpDown.Value = XMinNumericUpDown.Value + 1; return; }
            if (checkedListBox.Items.Count == 0) { DrawGraf(); return; }
            Thread t = new Thread(resetValuesThread);
            System.Threading.Timer timer = new System.Threading.Timer(mainWindow.abortGettingResult, t, checkedListBox.Items.Count * 1000, Timeout.Infinite);
            t.Start();
            t.Join();
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
            
            xTrackBar.Value = (int)(((double)XnumericUpDown.Value - (double)XMinNumericUpDown.Value) * (N) / ((double)XMaxNumericUpDown.Value - (double)XMinNumericUpDown.Value));

            updateListBox();
            
            

        }

        void updateListBox() 
        {
            listBox.Items.Clear();
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (checkedListBox.GetItemChecked(i))
                {
                    String text = ((Function)functions[i]).label.Substring(0, ((Function)functions[i]).label.IndexOf("(") + 1);
                    text += XnumericUpDown.Value.ToString() + ")=" + 
                        ((Function)functions[i]).data[xTrackBar.Value].ToString();
                    listBox.Items.Add(text);
                }
            }
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
                if (diffX >= 6 && diffY >=6)
                {
                    YMaxNumericUpDown.Value = YMaxNumericUpDown.Value - diffY / 4;
                    YMinNumericUpDown.Value = YMinNumericUpDown.Value + diffY / 4;
                    XMaxNumericUpDown.Value = XMaxNumericUpDown.Value - diffX / 4;
                    XMinNumericUpDown.Value = XMinNumericUpDown.Value + diffX / 4;
                    ZMaxNumericUpDown.Value = ZMaxNumericUpDown.Value - diffZ / 4;
                    ZMinNumericUpDown.Value = ZMinNumericUpDown.Value + diffZ / 4;
                }
            }
            catch(Exception err) { }
        }

        private void GrafSurface2D_Click(object sender, EventArgs e)
        {

        }

        private void xTrackBar_Scroll(object sender, EventArgs e)
        {
            XnumericUpDown.Value = (decimal)((double)XMinNumericUpDown.Value + (((double)(xTrackBar.Value) / N) * ((double)XMaxNumericUpDown.Value - (double)XMinNumericUpDown.Value)));           
        }

        private void buttonInverseSellection_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                CheckState changeTo;
                if(checkedListBox.GetItemChecked(i))changeTo = CheckState.Unchecked;
                else changeTo = CheckState.Checked;
                checkedListBox.SetItemCheckState(i, changeTo);
            }
            DrawGraf();
            updateListBox();
        }

        private void functionDeclaration_TextChanged(object sender, EventArgs e)
        {
            functionDeclaration.BackColor = Color.White;
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (int i=0;i<checkedListBox.Items.Count;i++)            
            { 
            checkedListBox.SetItemCheckState(i,CheckState.Checked);
            }
            DrawGraf();
            updateListBox();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = checkedListBox.Items.Count - 1; i >= 0; i--)
            {
                if (checkedListBox.GetItemChecked(i))
                {
                    functionsMutex.WaitOne();
                    checkedListBox.Items.RemoveAt(i);
                    functions.RemoveAt(i);
                    functionsMutex.ReleaseMutex();
                }
            }
            DrawGraf();
            updateListBox();

        }

        private void checkedListBox_MouseUp(object sender, MouseEventArgs e)
        {
            DrawGraf();
            updateListBox();
        }

        private void functionDeclaration_MouseClick(object sender, MouseEventArgs e)
        {
            functionDeclaration.BackColor = Color.White;
        }

        private void functionDeclaration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) buttonCreate_Click(sender, e);
        }
    }

    public class Function
    {
        public Function(int N)
        {
            this.N = N;
            data = new double[N];
        }

        int N;
        public int colorR;
        public int colorG;
        public int colorB;

        public double[] data;

        public String label;
    }
}

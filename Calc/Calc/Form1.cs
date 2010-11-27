using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;
using DMSoft;




namespace Calc
{
    public partial class Form1 : Form
    {
        public ArrayList functions = new ArrayList();//sem sa budu odkladat funkcie ak sa zavrie okno s grafom
        public System.Diagnostics.Process bc;
        public StreamReader bcOut;
        public StreamWriter bcIn;
        float memory = 0.0f;
        protected Boolean graphOpened;
        protected GrafForm grafoveOkno;
        private bool isShiftDown=false;
        int history_index;
        ArrayList history = new ArrayList();
        public Decimal MinX;
        public Decimal MaxX;
        public Decimal MinY;
        public Decimal MaxY;
        public Decimal X;

        public Form1()
        {
            InitializeComponent();
            startBC();
            graphOpened = false;
            expressionTextBox.Select();
            grafoveOkno = null;
            InitializeTooltips();
            
        }

        ~Form1()
        {
            bc.StandardInput.WriteLine("quit");//ukoncenie programu bc
        }
        /// <summary>
        /// spustenie programu bc a nasypanie funkcii do neho
        /// </summary>
        private void startBC() 
        {
            
            bc = new System.Diagnostics.Process();
            try
            {
         
                String strCommand = "..\\..\\..\\..\\bc\\bc.exe";//cesta k programu

                bc.StartInfo.FileName = strCommand;

                bc.StartInfo.UseShellExecute = false;

                bc.StartInfo.RedirectStandardOutput = true;//presmerovanie standardneho vstupu a vystupu programu
                bc.StartInfo.RedirectStandardInput = true;
                bc.StartInfo.CreateNoWindow = true;//aby sa nezobrazovalo to cierne okno konzoly ked sa spusti program
                bc.StartInfo.Arguments = "-l";
                bc.EnableRaisingEvents = true;                
                bc.Exited += new EventHandler(bcExited);//event handler na ukoncenie programu bc
                bc.StartInfo.RedirectStandardError = true;



                bc.Start();//spustenie programu bc                
            }
            catch (Exception e)
            {
                resultTextBox.Text = "Error: unable to run bc.exe";                
            }
            bcOut = bc.StandardOutput;
            bcIn = bc.StandardInput;

            TextReader functions = new StreamReader("..\\..\\..\\..\\bc\\funkcie.bc");
            String line = "";
            //resultTextBox.Text = line;
            while ((line = functions.ReadLine()) != null)
            {
                bcIn.WriteLine(line);
            }
        }
        /// <summary>
        /// funkcia pre handler ukone=cenia programu bc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bcExited(object sender, EventArgs e)
        {
            startBC();//ak sa bc ukonci tak sa znova spusti
        }

        /// <summary>
        /// funkcia ktoru vykonava vlakno na ziskanie vysledku z programu bc
        /// </summary>
        private void getResult()
        {
            resultTextBox.Text = "";
            String text = "";
            for (int i = 0; i < expressionTextBox.Lines.Length; i++) text += expressionTextBox.Lines[i];
            try
            {
                bcIn.WriteLine(text);//vlozenie vyrazu na vstup programu                        
                string line = "";
                while ((line = bcOut.ReadLine()) != null)
                {
                    resultTextBox.Text += line;
                }
            }
            catch (ThreadAbortException e) 
            {
                if (resultTextBox.Text == "") resultTextBox.Text = "Unable to find result."; 
            }           
        }

        /// <summary>
        /// funkcia pre timer na ukoncenie vlakna kde sa ziskava vysledok a aj programu bc
        /// </summary>
        /// <param name="data"></param>
        public void abortGettingResult(object data)
        {
            if (((Thread)data).IsAlive)
            {
                ((Thread)data).Abort();
                bc.Kill();
            }
        }

        /// <summary>
        /// Funkcia ktora sluzi ako event na spracovanie stlaceny zakladnych tlacidiel (hodnot)
        /// </summary>
        /// <param name="sender">No fucking idee</param>
        /// <param name="e">same as sender</param>
        private void buttonNum_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            String s = b.Text;
            if (s.Length > 1)
            {
                s = s.ToLower();
            }
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void buttonFun_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string s = b.Text.ToLower();
            s = s.Insert(s.Length, "()");
            s=s.Replace(" ", string.Empty);
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length - 1;
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            String text = "";
            if (expressionTextBox.Text == "") return;
            String expression = expressionTextBox.Lines[0];
            for (int i = 1; i < expressionTextBox.Lines.Length; i++) expression = expression + "\n" + expressionTextBox.Lines[i];
            history.Add(expression);
            history_index = history.Count - 1;

            Regex regex = new Regex(@"^\w+\((x|x,y)?\)=");

            for (int i = 0; i < expressionTextBox.Lines.Length; i++) text += expressionTextBox.Lines[i];   

            if(regex.Match(text).Success)
            {
                if (grafoveOkno==null || grafoveOkno.IsDisposed)   //grafove okno este neni otvorene
                {
                    grafoveOkno = new GrafForm(text,ref graphOpened,this);
                    grafoveOkno.Visible = true;
                    graphOpened = true;        
                }
                else                //grafove okno uz je otvorene
                {
                    grafoveOkno.AddFunkcia(text);
                    grafoveOkno.Show();
                    grafoveOkno.TopMost = true;
                    grafoveOkno.Focus();
                    grafoveOkno.BringToFront();
                    grafoveOkno.TopMost = false;
                }
            }
            else
            {               
                Thread t = new Thread(getResult);
                System.Threading.Timer timer = new System.Threading.Timer(abortGettingResult, t, 1000, Timeout.Infinite);
                t.Start();
            }
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            String text = "";
            Regex regex = new Regex(@"^\w+\((x|x,y)?\)=");

            for (int i = 0; i < expressionTextBox.Lines.Length; i++) text += expressionTextBox.Lines[i];

            if (regex.Match(text).Success)
            {
                if (grafoveOkno == null || grafoveOkno.IsDisposed)   //grafove okno este neni otvorene
                {
                    grafoveOkno = new GrafForm(text, ref graphOpened,this);
                    grafoveOkno.Visible = true;
                    graphOpened = true;
                }
                else                //grafove okno uz je otvorene
                {
                    grafoveOkno.AddFunkcia(text);
                    grafoveOkno.Show();
                    grafoveOkno.TopMost = true;
                    grafoveOkno.Focus();
                    grafoveOkno.BringToFront();
                    grafoveOkno.TopMost = false;
                }
            }
            else if (text.CompareTo("") == 0)
            {
                if (grafoveOkno == null || grafoveOkno.IsDisposed)   //grafove okno este neni otvorene
                {
                    grafoveOkno = new GrafForm(ref graphOpened,this);
                    grafoveOkno.Visible = true;
                    graphOpened = true;
                }
                else                //grafove okno uz je otvorene
                {
                    //grafoveOkno.AddFunkcia(text);
                    grafoveOkno.Show();
                    grafoveOkno.TopMost = true;
                    grafoveOkno.Focus();
                    grafoveOkno.BringToFront();
                    grafoveOkno.TopMost = false;
                }
            }
            else
            {
                resultTextBox.Text = "Not a function.";
            }

        }

        private void expressionTextBox_KeyDown(object sender, KeyEventArgs e)//odstranil som lebo chceme aby sa dal ten vyraz nejako formatovat
        {
            if (e.KeyCode == Keys.ShiftKey)isShiftDown = true;
            if (e.KeyCode == Keys.Enter && isShiftDown)buttonEquals_Click(sender, e);
            if (e.KeyCode == Keys.PageUp) 
            {
                if (history_index>0) history_index--;
                expressionTextBox.Text = history[history_index].ToString();

            }
            if (e.KeyCode == Keys.PageDown)
            {
                if (history_index < history.Count - 1)
                {
                    history_index++;
                    expressionTextBox.Text = history[history_index].ToString();
                }
                else 
                {
                    history_index = history.Count;
                    expressionTextBox.Text = "";
                }
            }   
        }


        private void expressionTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isShiftDown)//a zmaz enter ktory sa zapisal pri odpaleni vypoctu
            {
                int cursorPosition = expressionTextBox.SelectionStart;
                expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition - 1) + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
                expressionTextBox.SelectionStart = cursorPosition - 1;
            }
            if (e.KeyCode == Keys.ShiftKey) isShiftDown = false;
        }  

        private void buttonC_Click(object sender, EventArgs e)
        {
            expressionTextBox.Text = "";
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if(expressionTextBox.Text=="") {
                return;
            }
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition-1) + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition - 1;
        }

        private void buttonMS_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(getResult);
            System.Threading.Timer timer = new System.Threading.Timer(abortGettingResult, t, 1000, Timeout.Infinite);
            t.Start();
            t.Join();
            try
            {
                memory = float.Parse(resultTextBox.Text);
            }
            catch (FormatException err) { }
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + memory.ToString() + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + memory.ToString().Length;
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            memory = 0.0f;
        }

        private void buttonMplus_Click(object sender, EventArgs e)
        {
            expressionTextBox.Text = memory.ToString() + "+(" + expressionTextBox.Text + ")";
            Thread t = new Thread(getResult);
            System.Threading.Timer timer = new System.Threading.Timer(abortGettingResult, t, 1000, Timeout.Infinite);
            t.Start();
            t.Join();
            try
            {              
                    memory = float.Parse(resultTextBox.Text);
            }
            catch (FormatException err) { }

        }

        private void buttonMminus_Click(object sender, EventArgs e)
        {
            expressionTextBox.Text = memory.ToString() + "-(" + expressionTextBox.Text + ")";
            Thread t = new Thread(getResult);
            System.Threading.Timer timer = new System.Threading.Timer(abortGettingResult, t, 1000, Timeout.Infinite);
            t.Start();
            t.Join();
            try
            {
                    memory = float.Parse(resultTextBox.Text);
            }
            catch (FormatException err) { }
        }

        private void buttonAns_Click(object sender, EventArgs e)
        {
            String ans;
            try
            {
                ans = float.Parse(resultTextBox.Text).ToString();
            }
            catch (FormatException err) { ans="0";}
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + ans + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + ans.Length;
        }

        private void HexRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            buttonA.Enabled = true;
            buttonB.Enabled = true;
            buttonC.Enabled = true;
            buttonD.Enabled = true;
            buttonE.Enabled = true;
            buttonF.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
        }

        private void DecRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            buttonA.Enabled = false;
            buttonB.Enabled = false;
            buttonC.Enabled = false;
            buttonD.Enabled = false;
            buttonE.Enabled = false;
            buttonF.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;

        }

        private void OctRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            buttonA.Enabled = false;
            buttonB.Enabled = false;
            buttonC.Enabled = false;
            buttonD.Enabled = false;
            buttonE.Enabled = false;
            buttonF.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = false;
            button9.Enabled = false;
        }

        private void BinRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            buttonA.Enabled = false;
            buttonB.Enabled = false;
            buttonC.Enabled = false;
            buttonD.Enabled = false;
            buttonE.Enabled = false;
            buttonF.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            }

        private void expressionTextBox_TextChanged(object sender, EventArgs e)
        {
            int length = expressionTextBox.Text.Length;
            int cursor_position = expressionTextBox.SelectionStart;

            kontrolaZatvoriek(expressionTextBox.Text);

            if (DecRadioButton.Checked == true)
            {
                String[] num = {"A","B","C","D","E","F"};
                for (int i = 0; i < num.Length; i++)
                {
                    expressionTextBox.Text = expressionTextBox.Text.Replace(num[i], "");
                }
            }
            if (OctRadioButton.Checked == true)
            {
                String[] num = { "A", "B", "C", "D", "E", "F" , "9","8"};
                for (int i = 0; i < num.Length; i++)
                {
                    expressionTextBox.Text = expressionTextBox.Text.Replace(num[i], "");
                }
            }
            if (BinRadioButton.Checked == true)
            {
                String[] num = { "A", "B", "C", "D", "E", "F" ,"2","3","4","5","6","7","8","9"};
                for (int i = 0; i < num.Length; i++)
                {
                    expressionTextBox.Text = expressionTextBox.Text.Replace(num[i], "");
                }                
            }
            expressionTextBox.SelectionStart = cursor_position - (expressionTextBox.Text.Length - length);
            //ak sa nejaky znak zmazal tak sa posunie nastavenie kurzora
        }

        private void kontrolaZatvoriek(string text)
        {
            Stack<char> zatv = new Stack<char>();
            foreach (char p in text)
            {
                if (p == '(')
                {
                    zatv.Push('(');
                }
                if (p == ')')
                {
                    if (zatv.Count == 0)
                    {
                        syntaxTextBox.Text = "Missing \"(\"";
                        return;
                    }
                    zatv.Pop();
                }
            }
            if (zatv.Count > 0)
                syntaxTextBox.Text = "Missing \")\"";
            else
                syntaxTextBox.Text = "OK";
        }
    }
}

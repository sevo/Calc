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
        protected string[] pamet;
        List<Button> favButtons;
        Button[] delButtons;

        public Form1()
        {
            InitializeComponent();
            startBC();
            graphOpened = false;
            expressionTextBox.Select();
            grafoveOkno = null;
            InitializeTooltips();
            pamet = new string[10];
            delButtons=new Button[7];
            for (int i = 0; i < 10; i++)
                pamet[i] = "Memory " + (1 + i).ToString();
            memoryComboBox.SelectedIndex = 0;
            delButtons[0] = Del0; ;
            delButtons[1] = Del1; ;
            delButtons[2] = Del2; ;
            delButtons[3] = Del3; ;
            delButtons[4] = Del4; ;
            delButtons[5] = Del5; ;
            delButtons[6] = Del6; ;
            Del0.Hide();
            Del1.Hide();
            Del2.Hide();
            Del3.Hide();
            Del4.Hide();
            Del5.Hide();
            Del6.Hide();

            favButtons = new List<Button>();
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

        private void buttonFav_Click(object sender, EventArgs e)
        {
            Button but = sender as Button;
            foreach (Control.ControlCollection c in new Control.ControlCollection[] { GeneralTab.Controls, ProgrammerTab.Controls, TrigonometricTab.Controls, powerButton.Controls, StatisticalTab.Controls, ConversionTab.Controls, groupBox1.Controls, groupBox2.Controls })
            {
                foreach (Control cc in c)
                {
                    Button b = cc as Button;
                    if (b.Text == but.Text)
                    {
                        favoritesTab.Controls.Add(b);
                        b.PerformClick();
                        favoritesTab.Controls.Remove(b);
                        c.Add(b);
                        return;
                    }
                }
            }
        }

        private void buttonFun2_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string s = b.Text.ToLower();
            s = s.Insert(s.Length, "(,)");
            s = s.Replace(" ", string.Empty);
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length - 2;
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
                System.Threading.Timer timer = new System.Threading.Timer(abortGettingResult, t, 1250, Timeout.Infinite);
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
            /*Thread t = new Thread(getResult);
            System.Threading.Timer timer = new System.Threading.Timer(abortGettingResult, t, 1000, Timeout.Infinite);
            t.Start();
            t.Join();*/
            int index = memoryComboBox.SelectedIndex;
            pamet[index] = expressionTextBox.Text;
            refreshMemory();
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            int cursorPosition = expressionTextBox.SelectionStart;
            int index = memoryComboBox.SelectedIndex;
            if (pamet[index].StartsWith("Memory")) return;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + pamet[index] + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + pamet[index].Length;
            refreshMemory();
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            int index = memoryComboBox.SelectedIndex;
            pamet[index] = "Memory " + (index + 1).ToString();
            refreshMemory();
        }

        private void buttonMplus_Click(object sender, EventArgs e)
        {
            int index = memoryComboBox.SelectedIndex;
            if (pamet[index].StartsWith("Memory")) return;
            string what = pamet[index].ToString() + "+(" + expressionTextBox.Text + ")";
            pamet[index] = what;
            refreshMemory();
            /*Thread t = new Thread(getResult);
            System.Threading.Timer timer = new System.Threading.Timer(abortGettingResult, t, 1000, Timeout.Infinite);
            t.Start();
            t.Join();*/
            

        }

        private void buttonMminus_Click(object sender, EventArgs e)
        {
            int index = memoryComboBox.SelectedIndex;
            if (pamet[index].StartsWith("Memory")) return;
            string what = pamet[index].ToString() + "-(" + expressionTextBox.Text + ")";
            /*Thread t = new Thread(getResult);
            System.Threading.Timer timer = new System.Threading.Timer(abortGettingResult, t, 1000, Timeout.Infinite);
            t.Start();
            t.Join();*/
            pamet[index] = what;
            refreshMemory();
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

        private void piButton_Click(object sender, EventArgs e)
        {
            string s = "pi";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void psiButton_Click(object sender, EventArgs e)
        {
            string s = "psi";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void eButton_Click(object sender, EventArgs e)
        {
            string s = "e";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void goldenButton_Click(object sender, EventArgs e)
        {
            string s = "1.61803399";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void sqbutton_Click(object sender, EventArgs e)
        {
            string s = "1.41421356237309504880";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void cButton_Click(object sender, EventArgs e)
        {
            string s = "299792458";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void mebutton_Click(object sender, EventArgs e)
        {
            string s = "9.10938188*power(10, -31)";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void mpButton_Click(object sender, EventArgs e)
        {
            string s = "1.67262158*power(10, -27)";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void hButton_Click(object sender, EventArgs e)
        {
            string s = "6.626068*power(10, -34)";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void eeeButton_Click(object sender, EventArgs e)
        {
            string s = "1.60217646*power(10, -19)";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void ggButton_Click(object sender, EventArgs e)
        {
            string s = "9.8066";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void GButton_Click(object sender, EventArgs e)
        {
            string s = "9.8";
            int cursorPosition = expressionTextBox.SelectionStart;
            expressionTextBox.Text = expressionTextBox.Text.Substring(0, cursorPosition) + s + expressionTextBox.Text.Substring(cursorPosition, expressionTextBox.Text.Length - cursorPosition);
            expressionTextBox.SelectionStart = cursorPosition + s.Length;
        }

        private void refreshMemory()
        {
            for (int i = 0; i < 10; i++)
            {
                memoryComboBox.Items[i] = pamet[i];
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            switch (cb.SelectedItem.ToString())
            {
                case "General":
                    changeOtherCombobox(GeneralTab);
                    break;
                case "Programmer":
                    changeOtherCombobox(ProgrammerTab);
                    break;
                case "Trigonometric":
                    changeOtherCombobox(TrigonometricTab);
                    break;
                case "Power":
                    changeOtherCombobox(PowerTab);
                    break;
                case "Statistical":
                    changeOtherCombobox(StatisticalTab);
                    break;
                case "Conversion":
                    changeOtherCombobox(ConversionTab);
                    break;
                case "Constants":
                    changeOtherCombobox(ConstantsTab.Controls[0]);
                    changeOtherCombobox(ConstantsTab.Controls[1]);
                    break;
                default: throw new InvalidDataException("Selected index in favorites");
            }
        }

        private void changeOtherCombobox(Control c)
        {
            Fav2ComboBox.Items.Clear();
            foreach (object o in c.Controls)
            {
                if (o is Button)
                {
                    Button b = o as Button;
                    Fav2ComboBox.Items.Add(b.Text);
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            
            Control.ControlCollection where=null;
            if (favButtons.Count >= 7)
            {
                MessageBox.Show("Too much buttons, delete some first");
                return;
            }
            switch (Fav1ComboBox.SelectedItem.ToString())
            {
                case "General":
                    where = GeneralTab.Controls;
                    break;
                case "Programmer":
                    where = ProgrammerTab.Controls;
                    break;
                case "Trigonometric":
                    where = TrigonometricTab.Controls;
                    break;
                case "Power":
                    where = PowerTab.Controls;
                    break;
                case "Statistical":
                    where = StatisticalTab.Controls;
                    break;
                case "Conversion":
                    where = ConversionTab.Controls;
                    break;
                case "Constants":
                    where = groupBox1.Controls;
                    break;
                default: throw new InvalidDataException("Selected index in favorites");
            }

            foreach (Button b in where)
            {
                if (Fav2ComboBox.SelectedItem.ToString() == b.Text)
                {
                    Button novy = new Button();
                    novy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                    novy.UseVisualStyleBackColor = true;
                    novy.Size = b.Size;
                    novy.Text = b.Text;
                    novy.Click += new EventHandler(buttonFav_Click);
                    
                    favButtons.Add(novy);
                    favoritesTab.Controls.Add(novy);
                    reorganizeFav();
                    return;
                }
            }
        }

        private void reorganizeFav()
        {
            Del0.Show();
            for (int i = 0; i <  favButtons.Count; i++)
            {
                favButtons[i].Location = new System.Drawing.Point(80, 8 + (i * 50));
                delButtons[i].Show();
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            delButtons[favButtons.Count-1].Hide();
            Button b = sender as Button;
            int i = Int32.Parse(b.Name.Substring(b.Name.Length - 1));
            favoritesTab.Controls.Remove(favButtons[i]);
            favButtons.RemoveAt(i);
            reorganizeFav();
        }

        private void histUpButton_Click(object sender, EventArgs e)
        {
            if (history_index > 0) history_index--;
            expressionTextBox.Text = history[history_index].ToString();
        }

        private void histDownButton_Click(object sender, EventArgs e)
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
}

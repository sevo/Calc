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


namespace Calc
{
    public partial class Form1 : Form
    {
        System.Diagnostics.Process bc;
        StreamReader bcOut;
        StreamWriter bcIn;

        //Parser parser;
        public Form1()
        {
            InitializeComponent();
            //parser = new Parser();
            startBC();
            
        }

        ~Form1()
        {
            bc.StandardInput.WriteLine("quit");//ukoncenie programu bc
        }

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

                bc.Start();//spustenie programu bc                
            }
            catch (Exception e)
            {
                richTextBox2.Text = "Error: unable to run bc.exe";
            }
            bcOut = bc.StandardOutput;
            bcIn = bc.StandardInput;

            TextReader functions = new StreamReader("..\\..\\..\\..\\bc\\funkcie.bc");
            String line = "";
            //richTextBox1.Text = line;
            while ((line = functions.ReadLine()) != null)
            {
                bcIn.WriteLine(line);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "8";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "8" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "1";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "1" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "2";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "2" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "3";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "3" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "0";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "0" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += ".";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "." + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "+";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "+" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "-";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "-" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "*";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "*" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "/";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "/" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "4";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "4" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "5";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "5" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "6";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "6" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "7";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "7" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition + 1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text += "9";
            int cursorPosition = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Substring(0, cursorPosition) + "9" + richTextBox1.Text.Substring(cursorPosition, richTextBox1.Text.Length - cursorPosition);
            richTextBox1.SelectionStart = cursorPosition +1;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(getResult);
            System.Threading.Timer timer = new System.Threading.Timer(abortGettingResult, t, 1000, Timeout.Infinite);
            t.Start();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button19_Click(sender, e);
        }

        private void getResult()
        {
            try
            {
                bcIn.WriteLine(richTextBox1.Text);//vlozenie vyrazu na vstup programu                        
                string line = "";
                if ((line = bcOut.ReadLine()) != null)
                {
                    richTextBox2.Text = line;
                }
            }
            catch (ThreadAbortException e) { richTextBox2.Text = "Calculation timeout."; }            
        }

        private void abortGettingResult(object data)
        {
            if (((Thread)data).IsAlive)
            {
                ((Thread)data).Abort();
                //richTextBox2.Text = "thread aborded";
                bc.Kill();
                startBC();
            }
        }        
    }
}

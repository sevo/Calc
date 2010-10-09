using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


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
            bc = new System.Diagnostics.Process();

            String strCommand = "D:\\skola\\stu\\3.rocnik\\zima\\ICP\\projekt\\bc\\bc.exe";//cesta k programu
            bc.StartInfo.FileName = strCommand;

            bc.StartInfo.UseShellExecute = false;

            bc.StartInfo.RedirectStandardOutput = true;//presmerovanie standardneho vstupu a vystupu programu
            bc.StartInfo.RedirectStandardInput = true;
            bc.StartInfo.CreateNoWindow = true;//aby sa nezobrazovalo to cierne okno konzoly ked sa spusti program
            bc.StartInfo.Arguments = "-l";

            bc.Start();//spustenie programu bc
            bcOut = bc.StandardOutput;
            bcIn = bc.StandardInput;

            TextReader functions = new StreamReader("D:\\skola\\stu\\3.rocnik\\zima\\ICP\\projekt\\bc\\funkcie.bc");
            String line = "";
            //richTextBox1.Text = line;
            while ((line = functions.ReadLine()) != null)
            {
               bcIn.WriteLine(line);
            }
        }

        ~Form1()
        {
            bc.StandardInput.WriteLine("quit");//ukoncenie programu bc
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "8";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "3";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "0";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += ",";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "+";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "-";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "*";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "/";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "7";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "9";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            bcIn.WriteLine(richTextBox1.Text);//vlozenie vyrazu na vstup programu                        
            string line = "";
            if ((line = bcOut.ReadLine()) != null)
            {
                richTextBox2.Text = line;
            }
        }
    }
}

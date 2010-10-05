using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calc
{
    public partial class Form1 : Form
    {
        Parser parser;
        public Form1()
        {
            InitializeComponent();
            parser = new Parser();
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
            richTextBox2.Text = parser.Parse(richTextBox1.Text);
            if (!parser.IsValid(richTextBox1.Text)) richTextBox1.ForeColor = Color.Red;
        }
    }
}

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
    public partial class GrafForm : Form
    {
        private bool grafOpened;
        public GrafForm(string funkcia, ref bool grafOpened)
        {   
            InitializeComponent();
            AddFunkcia(funkcia);
            this.grafOpened = grafOpened;
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

    }

}

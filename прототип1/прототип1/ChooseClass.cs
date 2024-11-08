using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace прототип1
{
    public partial class ChooseClass : Form
    {
        public ChooseClass()
        {
            InitializeComponent();
            C = 1;

            button1.Enabled = false;
        }

        bool CanClose = false;
        public static int C { get; private set; }
        public static string login { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        { 
            if (radioButton1.Checked)
                C = 1;
            if (radioButton2.Checked)
                C = 2;
            if (radioButton3.Checked)
                C = 3;

            login = textBox1.Text;

            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
                CanClose = true;
            }
            else
            {
                button1.Enabled = false;
                CanClose = false;
            }
        }

        private void ChooseClass_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && CanClose == false)
                e.Cancel = true;
            else
                e.Cancel = false;
        }
        
    }
}

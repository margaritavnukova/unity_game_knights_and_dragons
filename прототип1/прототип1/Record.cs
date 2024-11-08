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
    public partial class Record : Form
    {
        public Record()
        {
            InitializeComponent();

            Login.RecordToTable(filename, dataGridView1);
        }

        string filename = Game.filename;
    }
}

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
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        //начало игры
        private void StartButton_Click(object sender, EventArgs e)
        {
            //открытие формы выбора класса в диалоговом режиме
            if (Application.OpenForms.Count < 3)
            {
                changeClassForm.Owner = this;
                changeClassForm.ShowDialog();

                //геттер свойства С - выбранный класс игрока
                int C = ChooseClass.C;

                EventCl = null;
            }

            Close();
            if (EventVis != null)
                EventVis();
        }

        public ChooseClass changeClassForm = new ChooseClass();

        //создание делегата для скрытия/отображения стартового окна
        public delegate void del();
        public event del EventVis; 
        public event del EventCl; 

        //открытие формы с рекордами
        private void SeeRecordButton_Click(object sender, EventArgs e)
        {
            Record recordForm = new Record();

            recordForm.Owner = this;
            recordForm.ShowDialog();
        }

        private void StartMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (EventCl != null)
                EventCl();
        }
    }
}

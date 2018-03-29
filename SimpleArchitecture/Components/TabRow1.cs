using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UserControls
{
    public partial class TabContentOfRowForEdit : UserControls.TabContentOfRow
    {
        public event EventHandler<RowEventArgs> EndEditEvent;

        public TabContentOfRowForEdit()
        {
            InitializeComponent();
            this.SaveButton.Click += SaveButtonClicked;
            //this.OkButtonClicked += OkButtonClicked;
        }

        private void SaveButtonClicked(object sender, EventArgs e)
        {
            //MessageBox.Show("ok");
            employee.Name = textBox1.Text;
            employee.Salary = System.Convert.ToDecimal(textBox2.Text);
            if (EndEditEvent != null)   //  Перед вызовом мы проверяем, закреплены ли за этими событиями обработчики 
            { 
                RowEventArgs ee = new RowEventArgs();
                ee.inc = employee.Id;
                ee.linkPage = null;
                EndEditEvent(this, ee); // вызываем эти события
            };
        }
        private void OkButtonClicked(object sender, EventArgs e)
        {
            CloseTabPage();

        }


    }
}


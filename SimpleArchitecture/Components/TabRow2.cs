using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UserControls
{
    public partial class TabContentOfRowForCalculate : UserControls.TabContentOfRow
    {
        //public event EventHandler<RowEventArgs> EndCalculateEvent;

        public TabContentOfRowForCalculate()
        {
            InitializeComponent();
            this.calcButton.Click += CalcButtonClicked;
        }

        private void CalcButtonClicked(object sender, EventArgs e)
        {
            //MessageBox.Show("Calc");
            textBox3.Text = employee.GetMonthlyPayment().ToString();
        }
        private void OkButtonClicked(object sender, EventArgs e)
        {
            CloseTabPage();

        }

        private void printButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print " + textBox3.Text);

        }



    }
}


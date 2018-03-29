using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UserControls
{
    public partial class TabContentForOperation : UserControls.TabContent
    {
        // Объявляем делегат
        //В среде .NET Framework предоставляется встроенный обобщенный делегат под названием 
        //EventHandler<TEventArgs>. В данном случае тип TEventArgs обозначает тип аргумента, передаваемого параметру EventArgs события.
        public event EventHandler<RowEventArgs> SelectedEventForCalculate;


        public TabContentForOperation()
        {
            InitializeComponent();
        }


        private void OperationButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("edit"); 
            int inc = dataGridView1.CurrentRow.Index;
            if (this.SelectedEventForCalculate != null)   //  Перед вызовом мы проверяем, закреплены ли за этими событиями обработчики 
            { 
                RowEventArgs ee = new RowEventArgs();
                ee.inc = inc;
                ee.linkPage = tabControl.SelectedTab; //or tabControl.SelectedIndex
                this.SelectedEventForCalculate(this, ee); // вызываем эти события
            }

        }

     }

}


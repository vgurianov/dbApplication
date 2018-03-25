using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TabPagesSet; // потом убрать

namespace UserControls
{
    public partial class TabContentForEdit : UserControls.TabContent
    {
        // Объявляем делегат
        //В среде .NET Framework предоставляется встроенный обобщенный делегат под названием 
        //EventHandler<TEventArgs>. В данном случае тип TEventArgs обозначает тип аргумента, передаваемого параметру EventArgs события.
        public event EventHandler<TableEventArgs> SelectedEvent;
        public event EventHandler<TableEventArgs> ChangeViewEvent;


        public TabContentForEdit()
        {
            InitializeComponent();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            int inc = dataGridView1.CurrentRow.Index;
            if (this.SelectedEvent != null)   //  Перед вызовом мы проверяем, закреплены ли за этими событиями обработчики 
            {
                TableEventArgs ee = new TableEventArgs();
                ee.inc = -1; // marker new row
                ee.linkPage = tabControl.SelectedTab;  //or tabControl.SelectedIndex
                this.SelectedEvent(this, ee); // вызываем эти события
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            int inc = dataGridView1.CurrentRow.Index;
            if (this.SelectedEvent != null)   //  Перед вызовом мы проверяем, закреплены ли за этими событиями обработчики 
            {//SelectedEvent("Edit " + inc.ToString()); 
                TableEventArgs ee = new TableEventArgs();
                ee.inc = inc;
                ee.linkPage = tabControl.SelectedTab;  //or tabControl.SelectedIndex
                this.SelectedEvent(this, ee); // вызываем эти события
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("delete");
                int inc = dataGridView1.CurrentRow.Index;
                ds.Tables[0].Rows[inc].Delete();
                dataGridView1.Update();
                if (ChangeViewEvent != null)  // Перед вызовом мы проверяем, закреплены ли за этими событиями обработчики
                {
                    TableEventArgs ee = new TableEventArgs();
                    ee.inc = inc;
                    ee.linkPage = tabControl.SelectedTab;  //or tabControl.SelectedIndex
                    ChangeViewEvent(this, ee); // вызов objConnect.UpdateDatabase(ds);
                }


        }
    }

}


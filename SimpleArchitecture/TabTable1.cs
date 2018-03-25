using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TabPagesSet; // ����� ������

namespace UserControls
{
    public partial class TabContentForEdit : UserControls.TabContent
    {
        // ��������� �������
        //� ����� .NET Framework ��������������� ���������� ���������� ������� ��� ��������� 
        //EventHandler<TEventArgs>. � ������ ������ ��� TEventArgs ���������� ��� ���������, ������������� ��������� EventArgs �������.
        public event EventHandler<TableEventArgs> SelectedEvent;
        public event EventHandler<TableEventArgs> ChangeViewEvent;


        public TabContentForEdit()
        {
            InitializeComponent();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            int inc = dataGridView1.CurrentRow.Index;
            if (this.SelectedEvent != null)   //  ����� ������� �� ���������, ���������� �� �� ����� ��������� ����������� 
            {
                TableEventArgs ee = new TableEventArgs();
                ee.inc = -1; // marker new row
                ee.linkPage = tabControl.SelectedTab;  //or tabControl.SelectedIndex
                this.SelectedEvent(this, ee); // �������� ��� �������
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            int inc = dataGridView1.CurrentRow.Index;
            if (this.SelectedEvent != null)   //  ����� ������� �� ���������, ���������� �� �� ����� ��������� ����������� 
            {//SelectedEvent("Edit " + inc.ToString()); 
                TableEventArgs ee = new TableEventArgs();
                ee.inc = inc;
                ee.linkPage = tabControl.SelectedTab;  //or tabControl.SelectedIndex
                this.SelectedEvent(this, ee); // �������� ��� �������
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("delete");
                int inc = dataGridView1.CurrentRow.Index;
                ds.Tables[0].Rows[inc].Delete();
                dataGridView1.Update();
                if (ChangeViewEvent != null)  // ����� ������� �� ���������, ���������� �� �� ����� ��������� �����������
                {
                    TableEventArgs ee = new TableEventArgs();
                    ee.inc = inc;
                    ee.linkPage = tabControl.SelectedTab;  //or tabControl.SelectedIndex
                    ChangeViewEvent(this, ee); // ����� objConnect.UpdateDatabase(ds);
                }


        }
    }

}


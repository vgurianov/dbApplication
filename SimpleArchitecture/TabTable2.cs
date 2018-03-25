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
        // ��������� �������
        //� ����� .NET Framework ��������������� ���������� ���������� ������� ��� ��������� 
        //EventHandler<TEventArgs>. � ������ ������ ��� TEventArgs ���������� ��� ���������, ������������� ��������� EventArgs �������.
        public event EventHandler<RowEventArgs> SelectedEventForCalculate;


        public TabContentForOperation()
        {
            InitializeComponent();
        }


        private void OperationButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("edit"); 
            int inc = dataGridView1.CurrentRow.Index;
            if (this.SelectedEventForCalculate != null)   //  ����� ������� �� ���������, ���������� �� �� ����� ��������� ����������� 
            { 
                RowEventArgs ee = new RowEventArgs();
                ee.inc = inc;
                ee.linkPage = tabControl.SelectedTab; //or tabControl.SelectedIndex
                this.SelectedEventForCalculate(this, ee); // �������� ��� �������
            }

        }

     }

}


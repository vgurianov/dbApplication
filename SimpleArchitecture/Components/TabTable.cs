using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UserControls
{
    // Using UserControl class for contents of TabPage
    // https://msdn.microsoft.com/en-us/library/system.windows.forms.usercontrol.aspx
    public partial class TabContent : UserControl
    {
        public DataSet ds;
        public TabControl tabControl;

        
        public TabContent()
        {
            InitializeComponent();
            ds = null;
            tabControl = null;
 
        }
        // Initialize the control elements.

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (ds != null)
            {
                // The data mapping to grid
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["Id"].ReadOnly = true;
            }
            else MessageBox.Show("DataSet is null");

        }

        private void CloseTabPage()
        {
            // Delete this tab
            //tabControl.TabPages.Remove(this); //or 
            tabControl.TabPages.Remove(tabControl.SelectedTab);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("cancel");
            CloseTabPage();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            CloseTabPage();
        }

    }

    /*
     *  Class AppSelectedEventArgs
     *  EventArgs for event of selected in the table 
     */
    public class TableEventArgs : EventArgs
    {
        public int inc;
        public TabPage linkPage; // link to call object
    }

}

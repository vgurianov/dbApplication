using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using BusinessLogic;

namespace UserControls
{
    // Using UserControl class for contents of TabPage
    // https://msdn.microsoft.com/en-us/library/system.windows.forms.usercontrol.aspx
    public partial class TabContentOfRow : UserControl
    {
        public DataSet ds;
        public TabControl tabControl;
        public Employee employee;

        public TabContentOfRow()
        {
            InitializeComponent();
            ds = null;
            tabControl = null;
 
        }
        // Initialize the control elements.

        // Delete this tab
        protected void CloseTabPage()
        {
            tabControl.TabPages.Remove(tabControl.SelectedTab);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("cancel");
            CloseTabPage();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Ok");
            CloseTabPage();
        }

        private void OkButton_Click_1(object sender, EventArgs e)
        {
            CloseTabPage();
        }

 
    }
    public class RowEventArgs : EventArgs
    {
        public int inc;
        public TabPage linkPage; // link to call object
    }


}

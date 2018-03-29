using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

using Database;
using BusinessLogic;

namespace TabPagesSet
{
   
    // -------------------------------------------------------------
    // Using:
    // http://www.codenet.ru/progr/cpp/dot-net-events.php

    
     
// ------------------------------------------------------------
        public class TabPageOfReport : TabPage
        {
            public WebBrowser wb;

            public TabPageOfReport()
            {
                wb = new WebBrowser();
                this.Controls.Add(wb);
                wb.Dock = DockStyle.Fill;
            }

        }

// -----------------------------------------------------------

}

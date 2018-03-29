using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;


// see licence!
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Database;
using TabPagesSet;
using BusinessLogic;
using UserControls;

namespace GraphicalUserInterface
{
    public partial class MainForm : Form
    {
        // Base variables ----------------------------------------
        DatabaseConnection objConnect;
        static string connectionString;   //= @"Data Source=.\SQLEXPRESS;Initial Catalog=appdb;Integrated Security=True";
        DataSet ds = null;
        Employee employee;
        // -------------------------------------------------------

        public MainForm()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection;
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection validation is successful: " + connectionString);
            }

        }


        // ---------------------------------------------------------------------------

        // Catalog/Employees (main menu item) 
        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {// view Employees

            try
            {
                objConnect = new DatabaseConnection();
                objConnect.connection_string = connectionString;
                objConnect.Sql = "SELECT * FROM Employees";
                ds = objConnect.GetConnection;

                TabPage page = new TabPage("Employees");
                TabContentForEdit content = new TabContentForEdit();
                content.Dock = DockStyle.Fill;
                page.Controls.Add(content);
                page.Controls.SetChildIndex(content, 0); // по совету из http://forum.vingrad.ru/forum/topic-215354.html
                content.ds = ds;
                content.tabControl = tabControl1;
                // create delegate
                content.SelectedEvent += new EventHandler<TableEventArgs>(GetEmployee);
                content.ChangeViewEvent += new EventHandler<TableEventArgs>(ChangeTable);

                tabControl1.TabPages.Add(page);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        // handler of SelectedEvent event 
        private void GetEmployee(object sender, TableEventArgs e)
        { 
            MessageBox.Show("Begin Edit emp = " + e.inc.ToString());
            TabContentOfRowForEdit content = new TabContentOfRowForEdit();
            TabPage page;
            int inc = e.inc;
            if (inc != -1)
            {
                DataRow row = ds.Tables[0].Rows[inc];
                // business object
                employee = new OrdinaryEmployee(inc, row[1].ToString(), System.Convert.ToDecimal(row[2]));
                // View
                page = new TabPage("Edit"); ;
            }
            else
            {// marker new row is inc == -1
                employee = new OrdinaryEmployee(inc, "no name", 0);
                // View
                page = new TabPage("New"); ;

            }
            content.textBox1.Text = employee.Name;
            content.textBox2.Text = employee.Salary.ToString(); // = System.Convert.ToDecimal(textBox2.Text);
            content.employee = employee;
            //page.linkPage = e.linkPage;
            content.tabControl = tabControl1;
            content.Dock = DockStyle.Fill;
            content.EndEditEvent += new EventHandler<RowEventArgs>(UpdateEmployeeTable);
            page.Controls.Add(content);
            page.Controls.SetChildIndex(content, 0); // from http://forum.vingrad.ru/forum/topic-215354.html
            tabControl1.TabPages.Add(page);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;

        }
        
        // Handler EndEditEvent event
        private void UpdateEmployeeTable(object sender, RowEventArgs e)
        { 
            MessageBox.Show("End Edit emp = " + employee.Name);
            if (employee.Id != -1)
            {
                DataRow row = ds.Tables[0].Rows[employee.Id];
                row[1] = employee.Name;
                row[2] = employee.Salary;
            }
            else
            {
                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();
                newRow["Name"] = employee.Name;
                newRow["Salary"] = employee.Salary; ;
                dt.Rows.Add(newRow);
            }
            try
            {
                objConnect.UpdateDatabase(ds);
                MessageBox.Show("Database updated");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        
        // Handler ChangeView event
        private void ChangeTable(object sender, TableEventArgs e)
        { 
            try
            {
                objConnect.UpdateDatabase(ds);
                MessageBox.Show("Database updated");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        // Catalog/Departments (main menu item)
        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                objConnect = new DatabaseConnection();
                objConnect.connection_string = connectionString;
                objConnect.Sql = "SELECT * FROM Departments";
                ds = objConnect.GetConnection;

                TabPage page = new TabPage("Departments");
                TabContentForEdit content = new TabContentForEdit();
                content.Dock = DockStyle.Fill;
                page.Controls.Add(content);
                page.Controls.SetChildIndex(content, 0); // по совету из http://forum.vingrad.ru/forum/topic-215354.html
                content.ds = ds;
                content.tabControl = tabControl1;
                // create delegate
                //content.SelectedEventForCalculate += new EventHandler<RowEventArgs>(GetDepartment);

                tabControl1.TabPages.Add(page);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }



        }



// -------------------------------------------------------------- 

        // Operation/Calculation (item of main menu)
        private void calculationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                objConnect = new DatabaseConnection();
                objConnect.connection_string = connectionString;
                objConnect.Sql = "SELECT * FROM Employees";
                ds = objConnect.GetConnection;

                TabPage page = new TabPage("Employees");
                TabContentForOperation content = new TabContentForOperation();
                content.Dock = DockStyle.Fill;
                page.Controls.Add(content);
                page.Controls.SetChildIndex(content, 0); // from http://forum.vingrad.ru/forum/topic-215354.html
                content.ds = ds;
                content.tabControl = tabControl1;
                // создаем делегата
                content.SelectedEventForCalculate += new EventHandler<RowEventArgs>(GetCalculateEmployee);

                tabControl1.TabPages.Add(page);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        // Handler SelectedEventForCalculate event
        private void GetCalculateEmployee(object sender, RowEventArgs e)
        {
            MessageBox.Show("Begin Calculate emp = " + e.inc.ToString());
            TabContentOfRowForCalculate content = new TabContentOfRowForCalculate();
            TabPage page;
            int inc = e.inc;
            DataRow row = ds.Tables[0].Rows[inc];
            // business object
            employee = new OrdinaryEmployee(inc, row[1].ToString(), System.Convert.ToDecimal(row[2]));
            // view
            page = new TabPage("Calculate"); ;

            content.textBox1.Text = employee.Name;
            content.textBox2.Text = employee.Salary.ToString(); // = System.Convert.ToDecimal(textBox2.Text);
            content.employee = employee;
            content.tabControl = tabControl1;
            content.Dock = DockStyle.Fill;
            //content.EndEditEvent += new EventHandler<RowEventArgs>(UpdateEmployeeTable);
            page.Controls.Add(content);
            page.Controls.SetChildIndex(content, 0); // from http://forum.vingrad.ru/forum/topic-215354.html
            tabControl1.TabPages.Add(page);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
        }

        // --------------------------------------------------------
        // Report to pdf (main menu item)
        // https://metanit.com/sharp/articles/25.php
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Define DataSet
            DatabaseConnection objConnect;
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=appdb;Integrated Security=True";
            DataSet ds; //DataRow dRow; 

            try
            {
                objConnect = new DatabaseConnection();
                objConnect.connection_string = connectionString;
                objConnect.Sql = "SELECT * FROM Employees";
                ds = objConnect.GetConnection;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                ds = null;
            }
            // ---------------------------------------------------------
            if (ds != null)
            {
                //pdf-document object
                iTextSharp.text.Document doc = new iTextSharp.text.Document();

                //better:
                string filename = @"C:\pdfTables.pdf";//Application.StartupPath;
                //filename = Path.GetFullPath(
                //    Path.Combine(filename, ".\\pdfTables.pdf"));

                //wbrPdf

                string reportDir = @"C:\pdfTables.pdf";
                PdfWriter.GetInstance(doc, new FileStream(reportDir, FileMode.Create));

                doc.Open();

                //The definition of the font is necessary to save the Cyrillic text
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                //Traversal of all the tables in the dataset 
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    //Create a table object and pass in it the number of columns of the table from our dataset
                    PdfPTable table = new PdfPTable(ds.Tables[i].Columns.Count);
                    //Add a common header to the table
                    PdfPCell cell = new PdfPCell(new Phrase("БД " + ", таблица №" + (i + 1), font));

                    cell.Colspan = ds.Tables[i].Columns.Count;
                    cell.HorizontalAlignment = 1;
                    //We remove the boundary of the first cell, so that we have both a header
                    cell.Border = 0;
                    table.AddCell(cell);

                    //First we add table headers
                    for (int j = 0; j < ds.Tables[i].Columns.Count; j++)
                    {
                        cell = new PdfPCell(new Phrase(new Phrase(ds.Tables[i].Columns[j].ColumnName, font)));
                        //Background color (optional, just do it nicer)
                        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                        table.AddCell(cell);
                    }

                    //Add all other cells
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        for (int k = 0; k < ds.Tables[i].Columns.Count; k++)
                        {
                            table.AddCell(new Phrase(ds.Tables[i].Rows[j][k].ToString(), font));
                        }
                    }
                    //Adding a table to the document
                    doc.Add(table);
                }
                doc.Close();

                MessageBox.Show("The PDF document is saved, " + reportDir);


                // --------------------------------------------------------- 
                TabPageOfReport page = new TabPageOfReport();
                page.Text = "Report"; page.wb.Navigate(filename);
                tabControl1.TabPages.Add(page);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;


            }; // if(ds == null)

        }

        // Setting (main menu item) 
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        { // interaction of forms

            ConnectForm newForm = new ConnectForm(this);
            //newForm.Controls[2].Text = connectionString;
            (newForm.Controls["textBox1"] as TextBox).Text = connectionString;


            //newForm.Show();
            newForm.ShowDialog();
            //string cs = newForm.Controls["textBox1"].Text;
            string cs = (newForm.Controls["textBox1"] as TextBox).Text;
            MessageBox.Show("NEW: " + cs);
            connectionString = cs;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //connection.State.ToString();
            //objConnect.
            this.Close();

        }

        // -------------------------------------------------------------- 

        #region TabPage context menu


        int bufferOfContextMenu;
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Point ee = new Point(e.Location.X - panel1.Left, e.Location.Y - panel1.Top);
                Point ee = new Point(e.Location.X, e.Location.Y);
                bufferOfContextMenu = -1;
                for (int i = 0; i < this.tabControl1.TabCount; i++)
                {
                    System.Drawing.Rectangle r = this.tabControl1.GetTabRect(i);
                    if (r.Contains(ee))
                    {
                        if (this.tabControl1.SelectedIndex == i)
                        {
                            this.contextMenu.Show(PointToScreen(ee));
                            //this.contextMenuTabs.Show(this.tabControl1, e.Location);
                            this.tabControl1.SelectedIndex = i;
                            bufferOfContextMenu = i;
                            //MessageBox.Show("tabControl1_MouseClick "+i.ToString());

                        }
                        else
                        {
                            //if a non seelcted page was clicked we detected it here!!
                        }

                        break;
                    }
                }
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("close_Click^" + sender.ToString());
            if (bufferOfContextMenu >= 0)
            {
                this.tabControl1.TabPages.RemoveAt(bufferOfContextMenu);
            }

        }
        #endregion

        
 

// ------------------------------------------------------


    
    }

}
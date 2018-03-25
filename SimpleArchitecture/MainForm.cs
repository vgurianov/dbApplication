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

namespace WindowsApplication
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
        // 

        // Catalog/Employees (пункт меню) 
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
                // создаем делегата
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

        // обработчик события SelectedEvent
        private void GetEmployee(object sender, TableEventArgs e)
        { 
            MessageBox.Show("Begin Edit emp = " + e.inc.ToString());
            TabContentOfRowForEdit content = new TabContentOfRowForEdit();
            TabPage page;
            int inc = e.inc;
            if (inc != -1)
            {
                DataRow row = ds.Tables[0].Rows[inc];
                // бизнес-объект
                employee = new OrdinaryEmployee(inc, row[1].ToString(), System.Convert.ToDecimal(row[2]));
                // представление
                page = new TabPage("Edit"); ;
            }
            else
            {// marker new row is inc == -1
                employee = new OrdinaryEmployee(inc, "no name", 0);
                // представление
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
            page.Controls.SetChildIndex(content, 0); // по совету из http://forum.vingrad.ru/forum/topic-215354.html
            tabControl1.TabPages.Add(page);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;

        }
        
        // обработчик события EndEditEvent
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
                // добавим новую строку
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
        
        // обработчик события ChangeView
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
        
        // Catalog/Departments (main menu)
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
                // создаем делегата
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
                page.Controls.SetChildIndex(content, 0); // по совету из http://forum.vingrad.ru/forum/topic-215354.html
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

        // обработчик события SelectedEventForCalculate
        private void GetCalculateEmployee(object sender, RowEventArgs e)
        {
            MessageBox.Show("Begin Calculate emp = " + e.inc.ToString());
            TabContentOfRowForCalculate content = new TabContentOfRowForCalculate();
            TabPage page;
            int inc = e.inc;
            DataRow row = ds.Tables[0].Rows[inc];
            // бизнес-объект
            employee = new OrdinaryEmployee(inc, row[1].ToString(), System.Convert.ToDecimal(row[2]));
            // представление
            page = new TabPage("Calculate"); ;

            content.textBox1.Text = employee.Name;
            content.textBox2.Text = employee.Salary.ToString(); // = System.Convert.ToDecimal(textBox2.Text);
            content.employee = employee;
            content.tabControl = tabControl1;
            content.Dock = DockStyle.Fill;
            // not end of action
            //content.EndEditEvent += new EventHandler<RowEventArgs>(UpdateEmployeeTable);
            page.Controls.Add(content);
            page.Controls.SetChildIndex(content, 0); // по совету из http://forum.vingrad.ru/forum/topic-215354.html
            tabControl1.TabPages.Add(page);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
        }

        // --------------------------------------------------------
        // Report to pdf (пункт меню)
        // https://metanit.com/sharp/articles/25.php
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Определяем объект DataSet
            DatabaseConnection objConnect;
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=appdb;Integrated Security=True";
            DataSet ds; //DataRow dRow; 

            try
            {
                objConnect = new DatabaseConnection();
                objConnect.connection_string = connectionString;
                objConnect.Sql = "SELECT * FROM Employees";
                ds = objConnect.GetConnection;
                // The data mapping to grid
                //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //dataGridView1.AllowUserToAddRows = false;
                //dataGridView1.DataSource = ds.Tables[0];
                //dataGridView1.Columns["Id"].ReadOnly = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                ds = null;
            }
            // ---------------------------------------------------------
            if (ds != null)
            {
                //Объект документа пдф
                iTextSharp.text.Document doc = new iTextSharp.text.Document();

                //Создаем объект записи пдф-документа в файл
                string reportDir = @"G:\_patterns\dbApplication\pdfTables.pdf";
                PdfWriter.GetInstance(doc, new FileStream(reportDir, FileMode.Create));

                //Открываем документ
                doc.Open();

                //Определение шрифта необходимо для сохранения кириллического текста
                //Иначе мы не увидим кириллический текст
                //Если мы работаем только с англоязычными текстами, то шрифт можно не указывать
                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                //Обход по всем таблицам датасета (хотя в данном случае мы можем опустить
                //Так как в нашей бд только одна таблица)
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    //Создаем объект таблицы и передаем в нее число столбцов таблицы из нашего датасета
                    PdfPTable table = new PdfPTable(ds.Tables[i].Columns.Count);
                    //Добавим в таблицу общий заголовок
                    PdfPCell cell = new PdfPCell(new Phrase("БД " + ", таблица №" + (i + 1), font));

                    cell.Colspan = ds.Tables[i].Columns.Count;
                    cell.HorizontalAlignment = 1;
                    //Убираем границу первой ячейки, чтобы были как заголовок
                    cell.Border = 0;
                    table.AddCell(cell);

                    //Сначала добавляем заголовки таблицы
                    for (int j = 0; j < ds.Tables[i].Columns.Count; j++)
                    {
                        cell = new PdfPCell(new Phrase(new Phrase(ds.Tables[i].Columns[j].ColumnName, font)));
                        //Фоновый цвет (необязательно, просто сделаем по красивее)
                        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                        table.AddCell(cell);
                    }

                    //Добавляем все остальные ячейки
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        for (int k = 0; k < ds.Tables[i].Columns.Count; k++)
                        {
                            table.AddCell(new Phrase(ds.Tables[i].Rows[j][k].ToString(), font));
                        }
                    }
                    //Добавляем таблицу в документ
                    doc.Add(table);
                }
                //Закрываем документ
                doc.Close();

                MessageBox.Show("Pdf-документ сохранен, см. " + reportDir);


                // --------------------------------------------------------- 
                //tabControl1.SelectedIndex = 2;
                string filename = @"G:\_patterns\dbApplication\pdfTables.pdf";//Application.StartupPath;

                //filename = Path.GetFullPath(

                //    Path.Combine(filename, ".\\Test.pdf"));

                //wbrPdf
                TabPageOfReport page = new TabPageOfReport();
                page.Text = "Report"; page.wb.Navigate(filename);
                tabControl1.TabPages.Add(page);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;


            }; // if(ds == null)

        }

        // Setting (пункт меню) 
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        { // взаимодействие форм

            ConnectForm newForm = new ConnectForm(this);
            // textBox1  в коллекции индекс 2
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
 
        #region Управление TabPage


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

        
 
// -------- Presenter -------------------------------------------------
        public Presenter presenter;
        public MvpModel model;
        public int inc2;
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Load
            try
            {
                objConnect = new DatabaseConnection();
                objConnect.connection_string = connectionString;
                objConnect.Sql = "SELECT * FROM Employee";
                ds = objConnect.GetConnection;
                inc2 = 3;
                DataRow row = ds.Tables[0].Rows[inc2];
                model = new MvpModel(inc2, row[1].ToString(), System.Convert.ToDecimal(row[2]));
                MessageBox.Show(model.Name);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            TabPageOfView page = new TabPageOfView();
            page.Text = "Presenter";
            presenter = new Presenter(page, model);
            tabControl1.TabPages.Add(page);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;

        }


        // ------------------------------------------------------


    
    }

}
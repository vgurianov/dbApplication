using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.Configuration;

namespace WindowsApplication
{
    public partial class ConnectForm : Form
    {
        static string connectionString;

        public ConnectForm()
        {
            InitializeComponent();
        }
        public ConnectForm(MainForm form1)
        {
            InitializeComponent();
            //System.Configuration.dll // https://metanit.com/sharp/adonet/2.2.php
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            label3.Text = connectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // SQL-server: RR\SQLEXPRESS    / RR\vg
            string cString = textBox1.Text;
            SqlConnection connection;
            connection = new SqlConnection(cString);

            try
            {
                // Открываем подключение
                connection.Open();
                //Console.WriteLine("Подключение открыто");
                textBox2.AppendText("Подключение открыто" + Environment.NewLine);
             // Вывод информации о подключении
                //Console.WriteLine("Свойства подключения:");
                textBox2.AppendText("Свойства подключения:" + Environment.NewLine);
                //Console.WriteLine("\tСтрока подключения: {0}", connection.ConnectionString);
                textBox2.AppendText("\tСтрока подключения: " + connection.ConnectionString + Environment.NewLine);
                //Console.WriteLine("\tБаза данных: {0}", connection.Database);
                textBox2.AppendText("\tБаза данных: " + connection.Database + Environment.NewLine);
                //Console.WriteLine("\tСервер: {0}", connection.DataSource);
                textBox2.AppendText("\tСервер: " + connection.DataSource + Environment.NewLine);
                //Console.WriteLine("\tВерсия сервера: {0}", connection.ServerVersion);
                textBox2.AppendText("\tВерсия сервера: " + connection.ServerVersion + Environment.NewLine);
                //Console.WriteLine("\tСостояние: {0}", connection.State);
                textBox2.AppendText("\tСостояние: " + connection.State + Environment.NewLine);
                //Console.WriteLine("\tWorkstationld: {0}", connection.WorkstationId);
                textBox2.AppendText("\tWorkstationld: " + connection.WorkstationId + Environment.NewLine);

            }
            catch (SqlException ex)
            {
                // Unhandled Exception: System.ArgumentException: Keyword not supported...
                // неправильного указания параметров строки подключения
                // Cannot open database "название базы данных" requested by the login. The login failed.Login failed for user 'название_пользователя'
                // убедиться, что на сервере есть база данных с таким названием, а если есть, то проверить, есть ли доступ для данного пользователя к этой бд
                // A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible..
                // SQL Server не запущен. И его надо запустить или перезапустить, через панель служб.
                Console.WriteLine(ex.Message);
                textBox2.AppendText("Error: " + ex.Message + Environment.NewLine);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
                textBox2.AppendText("Подключение закрыто... " + Environment.NewLine);
                textBox2.AppendText("Проверка подключения выполнена успешно " + Environment.NewLine);
                //Console.WriteLine("Подключение закрыто...");
            }

            //Console.Read();

        }
    }
}
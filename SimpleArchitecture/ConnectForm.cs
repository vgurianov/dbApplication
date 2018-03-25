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
                // ��������� �����������
                connection.Open();
                //Console.WriteLine("����������� �������");
                textBox2.AppendText("����������� �������" + Environment.NewLine);
             // ����� ���������� � �����������
                //Console.WriteLine("�������� �����������:");
                textBox2.AppendText("�������� �����������:" + Environment.NewLine);
                //Console.WriteLine("\t������ �����������: {0}", connection.ConnectionString);
                textBox2.AppendText("\t������ �����������: " + connection.ConnectionString + Environment.NewLine);
                //Console.WriteLine("\t���� ������: {0}", connection.Database);
                textBox2.AppendText("\t���� ������: " + connection.Database + Environment.NewLine);
                //Console.WriteLine("\t������: {0}", connection.DataSource);
                textBox2.AppendText("\t������: " + connection.DataSource + Environment.NewLine);
                //Console.WriteLine("\t������ �������: {0}", connection.ServerVersion);
                textBox2.AppendText("\t������ �������: " + connection.ServerVersion + Environment.NewLine);
                //Console.WriteLine("\t���������: {0}", connection.State);
                textBox2.AppendText("\t���������: " + connection.State + Environment.NewLine);
                //Console.WriteLine("\tWorkstationld: {0}", connection.WorkstationId);
                textBox2.AppendText("\tWorkstationld: " + connection.WorkstationId + Environment.NewLine);

            }
            catch (SqlException ex)
            {
                // Unhandled Exception: System.ArgumentException: Keyword not supported...
                // ������������� �������� ���������� ������ �����������
                // Cannot open database "�������� ���� ������" requested by the login. The login failed.Login failed for user '��������_������������'
                // ���������, ��� �� ������� ���� ���� ������ � ����� ���������, � ���� ����, �� ���������, ���� �� ������ ��� ������� ������������ � ���� ��
                // A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible..
                // SQL Server �� �������. � ��� ���� ��������� ��� �������������, ����� ������ �����.
                Console.WriteLine(ex.Message);
                textBox2.AppendText("Error: " + ex.Message + Environment.NewLine);
            }
            finally
            {
                // ��������� �����������
                connection.Close();
                textBox2.AppendText("����������� �������... " + Environment.NewLine);
                textBox2.AppendText("�������� ����������� ��������� ������� " + Environment.NewLine);
                //Console.WriteLine("����������� �������...");
            }

            //Console.Read();

        }
    }
}
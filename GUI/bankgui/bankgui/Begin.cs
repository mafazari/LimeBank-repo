using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using bankgui;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace bankgui
{
    public partial class Begin : Form
    {
        public SerialPort Arduino = new SerialPort();
        public delegate void DataToUI(String myString);
        public DataToUI updateFromHandler;
        MySqlConnection cn;
        //MySqlDataAdapter da;
        //DataSet ds;

        public Begin()
        {
            Cursor.Hide();
            InitializeComponent();
            //Hash hash = new Hash();
            //textBox1.Text = hash.makeHash("F049897C", "1234");

            //cn = new MySqlConnection("server=127.           0.0.1;uid=root;pwd=Antonio01!;database=lime-bank");
            //da = new MySqlDataAdapter("SELECT Naam FROM `lime-bank`.klant where KlantID = 1;", cn);
            //DataSet ds = new DataSet();
            //cn.Open();
            //MySqlCommand cmd = new MySqlCommand("SELECT * FROM `lime-bank`.klant where KlantID = 1;", cn);
            //cmd.CommandText = "SELECT Naam FROM `lime-bank`.klant where KlantID = 1;";
            //MySqlDataReader reader = cmd.ExecuteReader();
            //cmd.ExecuteNonQuery();

            //cn.Close();
            //string info;
            /*try
            {
                cn.Open();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error" + error);
                this.Close();
            }*/

            /*while (reader.Read())
            {
                Object obj = new Object();
                obj = reader.GetString(3);
                string a = (string)obj;
                textBox1.Text = a;
                cn.Close();
            }*/
            //cn.Close();
            /*string info1;
            //da.GetFillParam
            //info = info1.ToString();
            da.Fill(ds , "klant");
            foreach (DataTable myTable in ds.Tables)
            {
                foreach (DataRow myRow in myTable.Rows)
                {
                    foreach (DataColumn myColumn in myTable.Columns)
                    {
                        Console.Write(myRow[myColumn] + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                string info;
                info = Console.();
                textBox1.Text = info;
            }
            
            //info = ds.ToString();
            //info = 
            
            dataGridView1.DataSource = ds.Tables["klant"];*/
        }

        private void Begin_Load(object sender, EventArgs e)
        {
            Arduino.BaudRate = 9600;
            Arduino.PortName = "COM4";
            Arduino.DataReceived += new SerialDataReceivedEventHandler(DataRecHandler);
            Arduino.Open();

            updateFromHandler = new DataToUI(CheckNUID);
        }

        private void DataRecHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();
            string outdata = indata.Remove(indata.Length - 1);
            //string outdata = indata.Substring(indata.Length - 7);
            //string outdata = indata.First().ToString();
            //textBox1.AppendText(indata);
            BeginInvoke(this.updateFromHandler, new object[] { outdata });
        }

        public void CheckNUID(String mystring)
        {
            if (mystring.Contains("NUID"))
            {
                Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                Arduino.Close();
                new Login(mystring).Show();
                this.Refresh();
                Thread.Sleep(1);
                this.Hide();
            }
        }

        /*public void ChangeTextBox(String mystring)
        {
            /*         if(isAlphaNumeric(mystring))
                     {
                         MessageBox.Show("Its a number");
                     }
                     else
                     {
                         MessageBox.Show(mystring);
                     }   


            if (isAlphaNumeric(mystring))
            {
                string appendNumber = mystring.First().ToString();
                //textBox1.AppendText(appendNumber);
            }
            //string textboxtext = textBox1.Text;
            if (mystring.Equals("*") && textboxtext == "1234")
            {
                Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                //var MainMenu = new MainScherm();
                //MainMenu.Show();
                this.Hide();
                Arduino.Close();
            }
            if (mystring.Equals("#"))
            {
                //textBox1.ResetText();
            }
        }
        public bool isAlphaNumeric(string input)
        {
            Regex r = new Regex("[0-9]");
            if (r.IsMatch(input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //new Login().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

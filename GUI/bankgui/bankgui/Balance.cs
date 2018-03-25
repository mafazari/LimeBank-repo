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
using bankgui;
using System.IO.Ports;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace bankgui
{
    public partial class Balance : Form
    {
        public SerialPort Arduino = new SerialPort();
        public delegate void DataToUI(String myString);
        public DataToUI updateFromHandler;
        string SelectedAcc;
        MySqlConnection cn;

        public Balance(SerialPort s, string sa)
        {
            SelectedAcc = sa;
            //Cursor.Hide();
            this.Arduino = s;
            InitializeComponent();
            Arduino.DataReceived += new SerialDataReceivedEventHandler(DataRecHandler);
            updateFromHandler = new DataToUI(CheckInput);
            cn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=Antonio01!;database=lime-bank");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT Balans FROM `lime-bank`.rekening where RekeningType = " + SelectedAcc + " AND PasID = 'F049897C';", cn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Object obj = new Object();
                obj = reader.GetString(0);
                string a = (string)obj;
                label5.Text = "€ " + a;
                //cn.Close();
            }
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

        public void CheckInput(String mystring)
        {
            if (!mystring.Contains("NUID"))
            {
                if (mystring.Equals("*"))
                {
                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new Withdraw(Arduino, SelectedAcc).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
                if (mystring.Equals("A"))
                {
                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new Menu(Arduino, SelectedAcc).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
                if (mystring.Equals("D"))
                {
                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new EndScreen(Arduino).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
            }
        }

        private void Balance_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //new EndScreen().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new EndScreen().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

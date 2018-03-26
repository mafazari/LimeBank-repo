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
    public partial class Menu : Form
    {
        private SerialPort Arduino;
        public delegate void DataToUI(String myString);
        public DataToUI updateFromHandler;
        string SelectedAcc;
        MySqlConnection cn;
        private MySqlDataReader reader;
        String nuid;

        public Menu(SerialPort s, string sa, String n)
        {
            SelectedAcc = sa;
            nuid = n;
            Cursor.Hide();
            this.Arduino = s;
            InitializeComponent();
            Arduino.DataReceived += new SerialDataReceivedEventHandler(DataRecHandler);
            updateFromHandler = new DataToUI(CheckInput);
            cn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=Antonio01!;database=lime-bank");
            cn.Open();
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
            if (!mystring.Contains("NUID") || !mystring.Equals("A") || !mystring.Equals("B"))
            {
                if (mystring.Equals("1")) //quick 70
                {
                    MySqlCommand cmd = new MySqlCommand("Update `lime-bank`.rekening set Balans = Balans - " + 70 + " where PasID = '" + nuid + "' and RekeningType = " + SelectedAcc + ";", cn);
                    reader = cmd.ExecuteReader();
                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new EndScreen(Arduino).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
                if (mystring.Equals("2")) //withdraw
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT RekeningID FROM `lime-bank`.rekening where PasID = '" + nuid + "' and RekeningType = " + SelectedAcc + ";", cn);
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    Object obj = new Object();
                    obj = reader.GetString(0);
                    string a = (string)obj;
                    int rekid = Int32.Parse(a);

                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new Withdraw(Arduino, SelectedAcc, nuid, rekid).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
                if (mystring.Equals("3")) //check balance
                {
                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new Balance(Arduino, SelectedAcc, nuid).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
                if (mystring.Equals("D")) //logout
                {
                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new EndScreen(Arduino).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
                if (mystring.Equals("A")) //Account Select
                {
                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new AccountSelect(Arduino, nuid).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            //new EndScreen().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //new Withdraw().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //new Balance().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }
    }
}

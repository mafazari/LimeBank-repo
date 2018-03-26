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

namespace bankgui
{
    public partial class AccountSelect : Form
    {
        private SerialPort Arduino;
        public delegate void DataToUI(String myString);
        public DataToUI updateFromHandler;
        String nuid;

        public AccountSelect(SerialPort s, String n)
        {
            nuid = n;
            Cursor.Hide();
            this.Arduino = s;
            InitializeComponent();
            Arduino.DataReceived += new SerialDataReceivedEventHandler(DataRecHandler);
            updateFromHandler = new DataToUI(CheckInput);
        }

        private void AccountSelect_Load(object sender, EventArgs e)
        {
            //Arduino.DataReceived += new SerialDataReceivedEventHandler(DataRecHandler);
            //updateFromHandler = new DataToUI(CheckInput);
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
                if (mystring.Equals("1"))
                {
                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new Menu(Arduino, "1", nuid).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
                if (mystring.Equals("3"))
                {
                    Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                    new Menu(Arduino, "2", nuid).Show();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //new Menu().Show();
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
    }
}

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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace bankgui
{
    public partial class Withdraw : Form
    {
        private SerialPort Arduino;
        public delegate void DataToUI(String myString);
        public DataToUI updateFromHandler;
        string SelectedAcc;
        MySqlConnection cn;
        private MySqlDataReader reader1;
        String nuid;
        int rekid;

        public Withdraw(SerialPort s, string sa, String n, int r)
        {
            rekid = r;
            nuid = n;
            SelectedAcc = sa;
            Cursor.Hide();
            this.Arduino = s;
            InitializeComponent();
            Arduino.DataReceived += new SerialDataReceivedEventHandler(DataRecHandler);
            updateFromHandler = new DataToUI(ChangeTextBox);
            cn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=Antonio01!;database=lime-bank");
            cn.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT Balans FROM `lime-bank`.rekening where RekeningType = " + SelectedAcc + " AND PasID = '" + nuid + "';", cn);
            reader1 = cmd1.ExecuteReader();
        }

        private void DataRecHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();
            //string outdata = indata.Remove(indata.Length - 1);
            //string outdata = indata.Substring(indata.Length - 7);
            if (!indata.Contains("NUID"))
            {
                string outdata = indata.First().ToString();
                BeginInvoke(this.updateFromHandler, new object[] { outdata });
            }
            //textBox1.AppendText(indata);
            
        }

        public void ChangeTextBox(String mystring)
        {

            /*         if(isAlphaNumeric(mystring))
                     {
                         MessageBox.Show("Its a number");
                     }
                     else
                     {
                         MessageBox.Show(mystring);
                     }   */


            //if (isAlphaNumeric(mystring))
            //{
            //    string appendNumber = mystring.First().ToString();
            //  textBox1.AppendText(appendNumber);
            //}
            string textboxtext = textBox1.Text;
            //if (textboxtext == "1234")
            {
              //  Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);

                //new AccountSelect(Arduino).Show(); //IF USER HAS >1 ACCOUNT
                //this.Refresh();
                //Thread.Sleep(1);
                //this.Hide();

                //Arduino.Close();

                //Thread.Sleep(1); 
            }
            if (!mystring.Contains("NUID"))
            {
                if (mystring.Equals("C"))
                {
                    textBox1.ResetText();
                    label2.Text = "Enter withdrawal amount";
                    label2.ForeColor = System.Drawing.Color.DarkGreen;
                    label2.Left = (this.ClientSize.Width - label2.Size.Width) / 2;
                }
                if (mystring.Equals("D"))
                {
                    //logout
                    new EndScreen(Arduino).Show();
                    this.Refresh();
                    Thread.Sleep(1);
                    this.Hide();
                }
                if (!mystring.Equals("A") || !mystring.Equals("B") || !mystring.Equals("#"))
                {
                    //textBox1.ResetText();
                    if (isAlphaNumeric(mystring))
                    {
                        if (!mystring.Contains("NUID"))
                        {
                            string appendNumber = mystring.First().ToString();
                            textBox1.AppendText(appendNumber);
                        }
                    }
                }
                if (mystring.Equals("*"))
                {
                    while (true)
                    {
                        reader1.Read();
                        Object obj = new Object();
                        obj = reader1.GetString(0);
                        string a = (string)obj;
                        int balans = Int32.Parse(a);
                        int textboxint = Int32.Parse(textboxtext);
                        if (balans > textboxint)
                        {
                            reader1.Close();
                            //reader.Close();
                            MySqlCommand cmd = new MySqlCommand("Update `lime-bank`.rekening set Balans = Balans - " + textboxtext + " where PasID = '" + nuid + "' and RekeningType = " + SelectedAcc + ";", cn);
                            MySqlCommand cmd2 = new MySqlCommand("insert into `lime-bank`.transaction(RekeningID, Balans, AtmID, Time, Date) values("+ rekid + ", " + textboxtext + " ,'Lime Bank', current_time(), current_date())", cn);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            reader.Close();
                            MySqlDataReader reader2 = cmd2.ExecuteReader();
                            new EndScreen(Arduino).Show();
                            this.Refresh();
                            Thread.Sleep(1);
                            this.Hide();
                        }
                        else
                        {
                            label2.Text = "Insufficient funds";
                            label2.ForeColor = System.Drawing.Color.Red;
                            label2.Left = (this.ClientSize.Width - label2.Size.Width) / 2;
                        }
                        return;
                        //cn.Close();
                    }
                }
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
        }


        private void Withdraw_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new EndScreen(Arduino).Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new EndScreen(Arduino).Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class Login : Form
    {
        public SerialPort Arduino = new SerialPort();
        public delegate void DataToUI(String myString);
        public DataToUI updateFromHandler;
        public String nuid;
        MySqlConnection cn;
        int IncorrectAttemptCounter;
        //bool loop = true;

        public Login(String mystring)
        {
            Cursor.Hide();
            InitializeComponent();
            nuid = mystring.Remove(mystring.Length - 4);
            cn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=Antonio01!;database=lime-bank");
            cn.Open();

            //textBox2.Text = nuid;    
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Arduino.BaudRate = 9600;
            Arduino.PortName = "COM4";
            Arduino.DataReceived += new SerialDataReceivedEventHandler(DataRecHandler);
            Arduino.Open();
            updateFromHandler = new DataToUI(ChangeTextBox);

            
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

            //textBox2.Text = nuid;
            //Hash hash = new Hash();
            if (isAlphaNumeric(mystring))
            {
                    string appendNumber = mystring.First().ToString();
                    textBox1.AppendText(appendNumber);
            }
            string textboxtext = textBox1.Text;

            if (IncorrectAttemptCounter != 3)
            {
                if (textboxtext.Length > 3 && mystring.Equals("*") && IncorrectAttemptCounter < 3)
                {
                    //string hashed = hash.makeHash(nuid, textboxtext);
                    //textBox2.Text = hashed;
                    //MySqlCommand cmd = new MySqlCommand("SELECT Hash FROM `lime-bank`.rekening where PasID = '" + nuid + "';", cn);
                    //MySqlDataReader reader = cmd.ExecuteReader();


                    /*while (reader.Read())
                    {
                        Object obj = new Object();
                        obj = reader.GetString(0);
                        string hashCheck = (string)obj;
                        textBox2.Text = hashCheck;
                        if (hashed == hashCheck)
                        {*/
                    if (textboxtext == "1234")
                    {
                        Arduino.DataReceived -= new SerialDataReceivedEventHandler(DataRecHandler);
                        new AccountSelect(Arduino).Show(); //IF USER HAS >1 ACCOUNT
                        cn.Close();
                        //this.Refresh();
                        Thread.Sleep(1);
                        this.Hide();
                    }
                    else
                    {
                        IncorrectAttemptCounter++;
                        label2.Text = "Password incorrect, attempt " + "(" + IncorrectAttemptCounter + ")!";
                        label2.ForeColor = System.Drawing.Color.Red;
                        label2.Left = (this.ClientSize.Width - label2.Size.Width) / 2;
                        if (IncorrectAttemptCounter > 2)
                        {
                            label2.Text = "Your card has been blocked, please contact support";
                            label2.Left = (this.ClientSize.Width - label2.Size.Width) / 2;
                        }
                    }
                    /*if (IncorrectAttemptCounter == 3)
                    {
                        label2.Text = "Your card is blocked, please contact support";
                        label2.Left = (this.ClientSize.Width - label2.Size.Width) / 2;
                    }*/
                    return;
                    //}
                    //Arduino.Close();
                    //Thread.Sleep(1); 
                }
                /*else if (IncorrectAttemptCounter == 3)
                {
                    label2.Text = "This card has been disactivated";
                    label2.Left = (this.ClientSize.Width - label2.Size.Width) / 2;
                }*/
                if (mystring.Equals("C"))
                {
                    label2.Text = "Please, log in";
                    label2.ForeColor = System.Drawing.Color.DarkGreen;
                    label2.Left = 617;
                    textBox1.ResetText();
                }
            }
            else
            {
                textBox1.ResetText();
                label2.Text = "This card has been disactivated and cannot be used, please contact support.";
                label2.ForeColor = System.Drawing.Color.Red;
                label2.Left = (this.ClientSize.Width - label2.Size.Width) / 2;
                timer1.Start();
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

        private void DataRecHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();
            if (!indata.Contains("NUID"))
            {
                //string outdata = indata.Remove(indata.Length - 1);
                //string outdata = indata.Substring(indata.Length - 7);
                string outdata = indata.First().ToString();
                textBox1.Invoke(this.updateFromHandler, new object[] { outdata });
            }
            //textBox1.AppendText(indata);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //new AccountSelect().Show(); //IF USER HAS >1 ACCOUNT
            this.Refresh();
            Thread.Sleep(1);
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Arduino.Close();
            new Begin().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Close();
        }
    }
}

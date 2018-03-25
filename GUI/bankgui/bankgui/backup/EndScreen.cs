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

namespace bankgui
{
    public partial class EndScreen : Form
    {
        public EndScreen()
        {
            InitializeComponent();
        }

        private void EndScreen_Load(object sender, EventArgs e)
        {
            timer1.Interval = 3 * 1000; // 3 seconds
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            new Begin().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Close();
        }
    }
}

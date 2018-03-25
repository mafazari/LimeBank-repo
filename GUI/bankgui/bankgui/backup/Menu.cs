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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
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
            new EndScreen().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Withdraw().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Balance().Show();
            this.Refresh();
            Thread.Sleep(1);
            this.Hide();
        }
    }
}

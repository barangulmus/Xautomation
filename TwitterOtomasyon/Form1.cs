using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitterOtomasyon
{
    public partial class Form1 : Form
    {   
        public loginpage loginInfo;
        public Form1()
        {
            InitializeComponent();
            loginInfo = new loginpage();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.FromArgb(40,0,0,0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginInfo.username = textBox1.Text;
            loginInfo.password = textBox2.Text;

            // Check if both username and password are not null or empty
            if (!string.IsNullOrEmpty(loginInfo.username) && !string.IsNullOrEmpty(loginInfo.password))
            {
                Form ff = new Form2(this);
                ff.ShowDialog();
            }
            else
            {
                // Show an error message or handle the case where either username or password is null
                MessageBox.Show("Please enter both username and password.");
            }
        }

    }
}

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
            groupBox1.BackColor = Color.FromArgb(100,0,0,0);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginInfo.username = textBox1.Text;
            loginInfo.password = textBox2.Text;

            Form ff = new Form2(this);
            ff.ShowDialog();
        }
    }
}

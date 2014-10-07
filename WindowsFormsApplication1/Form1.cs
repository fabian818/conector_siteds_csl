using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Conector_CSL_Siteds : Form
    {
        public Conector_CSL_Siteds()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 5000;
            timer1.Enabled = true;
        }


        
        private void timer1_Tick(object sender, EventArgs e)
        {
            ConecctionAccess.TimerInsertMysql();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConecctionAccess.InsertMysql();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

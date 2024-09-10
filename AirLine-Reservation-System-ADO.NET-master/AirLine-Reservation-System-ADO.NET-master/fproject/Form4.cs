using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace fproject
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
            this.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();  
            form7.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Implement functionality for button3 if needed
            Form6 form6 = new Form6();
            form6.ShowDialog();
            this.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Implement functionality for button4 if needed
            Form8 form8 = new Form8();  
            form8.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           Form3 form3 = new Form3();
            form3.ShowDialog();
            this.Close();
        }
    }
}

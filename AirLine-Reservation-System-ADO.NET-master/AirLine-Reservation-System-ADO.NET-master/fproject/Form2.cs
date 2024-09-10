using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fproject
{
    public partial class Form2 : Form
    {
        public Dictionary<string, string> validCredentials = new Dictionary<string, string>
        {
            { "Rehan", "147" },
            { "Chandu", "69" },
            { "Manohar", "40" },
            { "Gopi", "186" },
            { "Ashik", "49" },
            { "Admin", "Admin" }
        };

        public Form2()
        {
            InitializeComponent();
            StyleSubmitButton(button1); // Apply styling to button1
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredName = textBox1.Text;
            string enteredPassword = textBox2.Text;

            if (validCredentials.ContainsKey(enteredName) && validCredentials[enteredName] == enteredPassword)
            {
                MessageBox.Show("Validation successful!");
                Form3 form3 = new Form3();
                form3.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid name or password.");
            }
        }

        private void StyleSubmitButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent; // Set background color to black
            button.ForeColor = Color.White; // Set font color to white
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Your code here (if needed)
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Your code here (if needed)
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Your code here (if needed)
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
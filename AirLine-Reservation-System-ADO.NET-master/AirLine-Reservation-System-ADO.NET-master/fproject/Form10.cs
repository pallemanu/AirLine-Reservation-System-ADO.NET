using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace fproject
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            // Set the form to full screen
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Optional: Verify the table existence
                    string checkTableQuery = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'IFlights'";
                    using (SqlCommand checkCmd = new SqlCommand(checkTableQuery, conn))
                    {
                        var result = checkCmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Table 'IFlights' does not exist.");
                            return;
                        }
                    }

                    string query = "INSERT INTO IFlights (FID, Arlname, Dairport, Aairport, Dcountry, Acountry, Dcity, Acity, Ddate, Adate, Dtime, Atime, Tprice) " +
                                   "VALUES (@FID, @Arlname, @Dairport, @Aairport, @Dcountry, @Acountry, @Dcity, @Acity, @Ddate, @Adate, @Dtime, @Atime, @Tprice)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FID", int.Parse(textBox1.Text));
                        cmd.Parameters.AddWithValue("@Arlname", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Dairport", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Aairport", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Dcity", textBox5.Text);
                        cmd.Parameters.AddWithValue("@Acity", textBox6.Text);
                        cmd.Parameters.AddWithValue("@Dcountry", textBox13.Text);
                        cmd.Parameters.AddWithValue("@Acountry", textBox12.Text);
                        cmd.Parameters.AddWithValue("@Ddate", DateTime.Parse(textBox8.Text));
                        cmd.Parameters.AddWithValue("@Adate", DateTime.Parse(textBox7.Text));
                        cmd.Parameters.AddWithValue("@Dtime", TimeSpan.Parse(textBox10.Text));
                        cmd.Parameters.AddWithValue("@Atime", TimeSpan.Parse(textBox9.Text));
                        cmd.Parameters.AddWithValue("@Tprice", decimal.Parse(textBox11.Text));

                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data Inserted Successfully .... :)");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            // Any additional load logic if needed
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Clear the text boxes
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox12.Text = string.Empty;
            textBox13.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.ShowDialog();
            this.Close();
        }

        // Additional methods for text box and label events if needed
        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void textBox8_TextChanged(object sender, EventArgs e) { }
        private void textBox9_TextChanged(object sender, EventArgs e) { }
        private void textBox10_TextChanged(object sender, EventArgs e) { }
        private void textBox11_TextChanged(object sender, EventArgs e) { }
        private void textBox12_TextChanged(object sender, EventArgs e) { }
        private void textBox13_TextChanged(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e)
        {
            // Your code here
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}

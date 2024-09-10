using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace fproject
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Optional: Any code for label1 click event
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Optional: Any code for textBox1 text changed event
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Delete flight details from database
            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHAIK MOHAMMAD REHAN\\Downloads\\Flightdata\\rehan.mdf\";Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Dflights WHERE FID = @FID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FID", int.Parse(textBox1.Text));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Flight details Deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No flight found with the given flight number.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Navigate back to Form4
            Form4 form4 = new Form4();
            form4.ShowDialog();
            this.Close();
        }
    }
}

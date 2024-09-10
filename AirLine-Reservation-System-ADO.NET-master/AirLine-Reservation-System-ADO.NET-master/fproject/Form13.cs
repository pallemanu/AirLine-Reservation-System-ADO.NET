using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace fproject
{
    public partial class Form13 : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHAIK MOHAMMAD REHAN\\source\\repos\\fproject\\fproject\\rehan.mdf\";Integrated Security=True;Connect Timeout=30;";

        public Form13()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Handle text change if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int flightNumber))
            {
                // Call method to delete flight record
                DeleteFlightByNumber(flightNumber);
            }
            else
            {
                MessageBox.Show("Please enter a valid flight number.");
            }
        }

        private void DeleteFlightByNumber(int flightNumber)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM IFlights WHERE FID = @FID";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@FID", flightNumber);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Flight record deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No record found with the specified flight number.");
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
            Form9 form9 = new Form9();
            form9.ShowDialog();
            this.Close();
        }
    }
}

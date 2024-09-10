using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace fproject
{
    public partial class Form11 : Form
    {
        // Connection string for the database
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHAIK MOHAMMAD REHAN\\source\\repos\\fproject\\fproject\\rehan.mdf\";Integrated Security=True;Connect Timeout=30;";

        public Form11()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Form11_Load);
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            // Load all data initially when the form loads
            LoadAllData();
        }

        private void LoadAllData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM IFlights";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = null; // Clear previous data
                        dataGridView1.DataSource = dataTable; // Set new data
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadDataByFlightNumber(int flightNumber)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM IFlights WHERE FID = @FID";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@FID", flightNumber);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = null; // Clear previous data
                        dataGridView1.DataSource = dataTable; // Set new data
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // If the textbox is empty, load all data
                LoadAllData();
            }
            else if (int.TryParse(textBox1.Text, out int flightNumber))
            {
                // If a valid flight number is provided, load specific data
                LoadDataByFlightNumber(flightNumber);
            }
            else
            {
                // Show an error message for invalid input
                MessageBox.Show("Please enter a valid flight number.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell content click if needed
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Handle text change if needed
        }
    }
}

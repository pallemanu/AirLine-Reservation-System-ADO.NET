using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace fproject
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // Load all data initially
            LoadAllData();
        }

        private void LoadAllData()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHAIK MOHAMMAD REHAN\\Downloads\\Flightdata\\rehan.mdf\";Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Dflights";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
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
            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\SHAIK MOHAMMAD REHAN\\Downloads\\Flightdata\\rehan.mdf\";Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Dflights WHERE FID = @FID";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@FID", flightNumber);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
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
            if (int.TryParse(textBox1.Text, out int flightNumber))
            {
                LoadDataByFlightNumber(flightNumber);
            }
            else
            {
                MessageBox.Show("Please enter a valid flight number.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell content click if needed
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Handle text change if needed
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace fproject
{
    public partial class Form17 : Form
    {
        public Form17()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form17_Load);
            button1.Click += new EventHandler(button1_Click);
        }

        // User session class to hold user data across forms
        public static class UserSession
        {
            public static string Gmail { get; set; }
        }

        private void Form17_Load(object sender, EventArgs e)
        {
            LoadUserData();
            LoadFlightData();
        }

        private void LoadUserData()
        {
            string gmail = UserSession.Gmail; // Get the Gmail from the session
            if (string.IsNullOrEmpty(gmail))
            {
                MessageBox.Show("No user logged in.");
                this.Close();
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf';Integrated Security=True;Connect Timeout=30";
            string query = "SELECT * FROM DFlightuser WHERE Gmail = @Gmail";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Gmail", gmail);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        textBox1.Text = reader["Name"].ToString();
                        textBox2.Text = reader["Pno"].ToString();
                        textBox3.Text = reader["Gmail"].ToString();
                        textBox4.Text = reader["Adharno"].ToString();
                        textBox5.Text = reader["Flightno"].ToString();
                        textBox6.Text = reader["State"].ToString();
                        textBox7.Text = reader["City"].ToString();
                        textBox8.Text = reader["Area"].ToString();
                        textBox9.Text = reader["Pincode"].ToString();
                        textBox10.Text = reader["Tickets"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadFlightData()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf';Integrated Security=True;Connect Timeout=30";
            string query = "SELECT * FROM Dflights";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
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
            if (ValidateInputs(out string adharno, out string name, out string pno, out string gmail, out string state, out string city, out string area, out string pincode, out int flightno, out int tickets))
            {
                StoreUserData(adharno, name, pno, gmail, state, city, area, pincode, flightno, tickets);
            }
        }

        private bool ValidateInputs(out string adharno, out string name, out string pno, out string gmail, out string state, out string city, out string area, out string pincode, out int flightno, out int tickets)
        {
            adharno = pno = pincode = string.Empty;
            name = gmail = state = city = area = string.Empty;
            flightno = tickets = 0;

            if (textBox4.Text.Length != 12 || !long.TryParse(textBox4.Text, out _))
            {
                MessageBox.Show("Please enter a valid 12-digit Aadhaar number.");
                return false;
            }
            adharno = textBox4.Text;
            name = textBox1.Text;
            pno = textBox2.Text;
            gmail = textBox3.Text;
            state = textBox6.Text;
            city = textBox7.Text;
            area = textBox8.Text;
            pincode = textBox9.Text;

            if (!int.TryParse(textBox5.Text, out flightno) || flightno <= 0)
            {
                MessageBox.Show("Please enter a valid flight number.");
                return false;
            }

            if (!int.TryParse(textBox10.Text, out tickets) || tickets <= 0)
            {
                MessageBox.Show("Please enter a valid number of tickets.");
                return false;
            }

            return true;
        }

        private void StoreUserData(string adharno, string name, string pno, string gmail, string state, string city, string area, string pincode, int flightno, int tickets)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf';Integrated Security=True;Connect Timeout=30";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string insertQuery = @"
                        INSERT INTO DFlightuser (Adharno, Name, Pno, Gmail, State, City, Area, Pincode, Flightno, Tickets)
                        VALUES (@Adharno, @Name, @Pno, @Gmail, @State, @City, @Area, @Pincode, @Flightno, @Tickets)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Adharno", adharno);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Pno", pno);
                        cmd.Parameters.AddWithValue("@Gmail", gmail);
                        cmd.Parameters.AddWithValue("@State", state);
                        cmd.Parameters.AddWithValue("@City", city);
                        cmd.Parameters.AddWithValue("@Area", area);
                        cmd.Parameters.AddWithValue("@Pincode", pincode);
                        cmd.Parameters.AddWithValue("@Flightno", flightno);
                        cmd.Parameters.AddWithValue("@Tickets", tickets);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Booking successful!");
                    Form18 form18 = new Form18();
                    form18.ShowDialog();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }

        // Event handlers for textboxes
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
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form16 form16 = new Form16();
            form16.ShowDialog();
            this.Close();
        }
    }
}
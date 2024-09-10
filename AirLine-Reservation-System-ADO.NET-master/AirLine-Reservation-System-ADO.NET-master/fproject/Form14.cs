using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static fproject.Form17;

namespace fproject
{
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Signup();
        }

        private void Login()
        {
            string gmail = textBox1.Text;
            string password = textBox2.Text;

            if (!IsValidGmail(gmail))
            {
                MessageBox.Show("Please enter a valid Gmail address ending with '@gmail.com'.");
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf';Integrated Security=True;Connect Timeout=30;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM UserD WHERE Gmail=@Gmail AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Gmail", gmail);
                cmd.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    UserSession.Gmail = gmail; // Store the logged-in user's email
                    MessageBox.Show("Login successful!");
                    Form16 form16 = new Form16();
                    form16.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Gmail or Password");
                }
            }
        }

        private void Signup()
        {
            string gmail = textBox1.Text;
            string password = textBox2.Text;

            if (!IsValidGmail(gmail))
            {
                MessageBox.Show("Please enter a valid Gmail address ending with '@gmail.com'.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.");
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf';Integrated Security=True;Connect Timeout=30;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO UserD (Gmail, Password) VALUES (@Gmail, @Password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Gmail", gmail);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Signup successful!");

                    // Clear the textboxes after successful signup
                    textBox1.Clear();
                    textBox2.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private bool IsValidGmail(string email)
        {
            return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
        }

        // Dummy methods to prevent errors
        private void button4_Click(object sender, EventArgs e) 
        {
        Form15 form15 = new Form15();
        form15.ShowDialog();
         this.Close();  

        }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}

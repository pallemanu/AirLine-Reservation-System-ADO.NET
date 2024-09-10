using System;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;

namespace fproject
{
    public partial class Form15 : Form
    {
        private string generatedOtp;

        public Form15()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Email input
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // OTP input
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate OTP button
            string email = textBox1.Text;
            string enteredOtp = textBox2.Text;

            if (ValidateEmailOtp(email, enteredOtp))
            {
                MessageBox.Show("OTP validated successfully!");
                Form21 form21 = new Form21();
                form21.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Open Form14 button
            Form14 form14 = new Form14();
            form14.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Send OTP button
            string email = textBox1.Text;

            if (!string.IsNullOrWhiteSpace(email))
            {
                generatedOtp = GenerateOtp();
                StoreEmailOtpInDatabase(email, generatedOtp);
                SendEmailOtp(email, generatedOtp);
            }
            else
            {
                MessageBox.Show("Please enter a valid email address.");
            }
        }

        private string GenerateOtp()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        private void StoreEmailOtpInDatabase(string email, string otp)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf';Integrated Security=True;Connect Timeout=30;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(1) FROM EmailOTP WHERE Email=@Email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Email", email);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                string query;
                if (count == 1)
                {
                    query = "UPDATE EmailOTP SET OTP=@OTP WHERE Email=@Email";
                }
                else
                {
                    query = "INSERT INTO EmailOTP (Email, OTP) VALUES (@Email, @OTP)";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@OTP", otp);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error storing OTP: " + ex.Message);
                }
            }
        }

        private bool ValidateEmailOtp(string email, string enteredOtp)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\SHAIK MOHAMMAD REHAN\source\repos\fproject\fproject\rehan.mdf';Integrated Security=True;Connect Timeout=30;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string selectQuery = "SELECT OTP FROM EmailOTP WHERE Email=@Email";
                SqlCommand cmd = new SqlCommand(selectQuery, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                string storedOtp = cmd.ExecuteScalar()?.ToString();

                return enteredOtp == storedOtp;
            }
        }

        private void SendEmailOtp(string email, string otp)
        {
            string fromEmail = "mdrehansk143@gmail.com";
            string subject = "Your OTP Code";
            string body = $"Your OTP code is: {otp}";

            try
            {
                MailMessage mail = new MailMessage(fromEmail, email, subject, body);
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new System.Net.NetworkCredential("mdrehansk143@gmail.com", "your-email-password"), // Use an app-specific password
                    EnableSsl = true
                };

                client.Send(mail);
                MessageBox.Show("OTP sent successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending OTP: " + ex.Message);
            }
        }
    }
}

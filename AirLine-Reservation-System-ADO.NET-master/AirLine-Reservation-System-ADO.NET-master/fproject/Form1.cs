using System.Drawing;
using System.Windows.Forms;
using System;

namespace fproject
{
    public partial class Form1 : Form
    {
        private Timer fadeInTimer;
        private double opacityStep = 0.05; // Change this value to adjust the fade-in speed

        public Form1()
        {
            InitializeComponent();
            MakeButtonTransparent(button1);
            MakeButtonTransparent(button2);

            InitializeFadeInTimer();
            this.Load += Form1_Load; // Add the Form1_Load event handler
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = 0; // Start with form invisible
            fadeInTimer.Start(); // Start the fade-in effect

            // Set the form to full screen
            this.FormBorderStyle = FormBorderStyle.None; // Remove the border
            this.WindowState = FormWindowState.Maximized; // Maximize the form to full screen
        }

        private void InitializeFadeInTimer()
        {
            fadeInTimer = new Timer();
            fadeInTimer.Interval = 50; // Adjust this interval to make the animation smoother or faster
            fadeInTimer.Tick += FadeInTimer_Tick;
        }

        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += opacityStep;
                if (this.Opacity >= 1)
                {
                    this.Opacity = 1; // Ensure it reaches full opacity
                    fadeInTimer.Stop(); // Stop the timer when done
                }
            }
        }

        private void MakeButtonTransparent(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.Black; // Set font color to white
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14();   
            form14.ShowDialog();
            this.Close();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // This seems to be an unused event handler. Consider removing it if not needed.
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

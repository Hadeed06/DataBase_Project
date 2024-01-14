using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase_Project
{
    public partial class Suoervisor_interface : Form
    {
        public Suoervisor_interface()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Form1.ShowDialog(); // Show the sign-up form as a dialog
            Form1.Show(); // This will open the sign-up form as a separate window

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thesis_view_by_supervisor Thesis_view_by_supervisor = new Thesis_view_by_supervisor(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Thesis_view_by_supervisor.ShowDialog(); // Show the sign-up form as a dialog
            Thesis_view_by_supervisor.Show(); // This will open the sign-up form as a separate window

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Suopervisor_review Suopervisor_review = new Suopervisor_review(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Suopervisor_review.ShowDialog(); // Show the sign-up form as a dialog
            Suopervisor_review.Show(); // This will open the sign-up form as a separate window

        }

        private void Suoervisor_interface_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Student_data_for_supervisor Student_data_for_supervisor = new Student_data_for_supervisor(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Student_data_for_supervisor.ShowDialog(); // Show the sign-up form as a dialog
            Student_data_for_supervisor.Show();
        }
    }
}

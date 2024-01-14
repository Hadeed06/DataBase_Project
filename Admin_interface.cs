using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataBase_Project
{
    public partial class Admin_interface : Form
    {
        public Admin_interface()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Form1.ShowDialog(); // Show the sign-up form as a dialog
            Form1.Show(); // This will open the sign-up form as a separate window
        }

        // view data function
        private void button1_Click(object sender, EventArgs e)
        {
            View_data_Admin View_data_Admin = new View_data_Admin(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            View_data_Admin.ShowDialog(); // Show the sign-up form as a dialog
            View_data_Admin.Show(); // This will open the sign-up form as a separate window
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Deletetion_by_admin Deletetion_by_admin = new Deletetion_by_admin(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Deletetion_by_admin.ShowDialog(); // Show the sign-up form as a dialog
            Deletetion_by_admin.Show(); // This will open the sign-up form as a separate window
        }

        private void button4_Click(object sender, EventArgs e)
        {

            View_Reviews_and_Panel_Members_for_Each_Thesis_Proposal View_Reviews_and_Panel_Members_for_Each_Thesis_Proposal = new View_Reviews_and_Panel_Members_for_Each_Thesis_Proposal(); // Assuming your sign-up form class is named SignUpForm

            this.Hide(); // Optional: Hide the login form
            View_Reviews_and_Panel_Members_for_Each_Thesis_Proposal.ShowDialog(); // Show the sign-up form as a dialog
            View_Reviews_and_Panel_Members_for_Each_Thesis_Proposal.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Student_Details_For_Admin Student_Details_For_Admin = new Student_Details_For_Admin(); // Assuming your sign-up form class is named SignUpForm

            this.Hide(); // Optional: Hide the login form
            Student_Details_For_Admin.ShowDialog(); // Show the sign-up form as a dialog
            Student_Details_For_Admin.Show();
        }
    }
}

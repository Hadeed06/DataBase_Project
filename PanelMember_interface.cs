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
    public partial class PanelMember_interface : Form
    {
        public PanelMember_interface()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PanelM_view_thesis PanelM_view_thesis = new PanelM_view_thesis(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            PanelM_view_thesis.ShowDialog(); // Show the sign-up form as a dialog
            PanelM_view_thesis.Show(); // This will open the sign-up form as a separate window

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PanelM_Review PanelM_Review = new PanelM_Review(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            PanelM_Review.ShowDialog(); // Show the sign-up form as a dialog
            PanelM_Review.Show(); // This will open the sign-up form as a separate window


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Form1.ShowDialog(); // Show the sign-up form as a dialog
            Form1.Show();
        }
    }
}

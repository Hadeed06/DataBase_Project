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
    public partial class PanelM_Review : Form
    {
        public PanelM_Review()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int studentID;
            if (!int.TryParse(studentIDTextBox.Text, out studentID))
            {
                MessageBox.Show("Please enter a valid Student ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Approve_Thesis_PM Approve_Thesis_PM = new Approve_Thesis_PM(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Approve_Thesis_PM.info(studentID);
            Approve_Thesis_PM.ShowDialog(); // Show the sign-up form as a dialog
            Approve_Thesis_PM.Show(); // This will open the sign-up form as a separate window

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int studentID;
            if (!int.TryParse(studentIDTextBox.Text, out studentID))
            {
                MessageBox.Show("Please enter a valid Student ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Reject_thesis_PM Reject_thesis_PM = new Reject_thesis_PM(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Reject_thesis_PM.info(studentID);
            Reject_thesis_PM.ShowDialog(); // Show the sign-up form as a dialog
            Reject_thesis_PM.Show(); // This will open the sign-up form as a separate window
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Form1.ShowDialog(); // Show the sign-up form as a dialog
            Form1.Show(); // This will ope
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PanelMember_interface PanelMember_interface = new PanelMember_interface(); // Assuming your sign-up form class is named SignUpForm

            this.Hide(); // Optional: Hide the login form
            PanelMember_interface.ShowDialog(); // Show the sign-up form as a dialog
            PanelMember_interface.Show();
        }
    }
}

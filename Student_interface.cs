using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataBase_Project
{
    public partial class Student_interface : Form
    {
        private const string ConnectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"; // Replace with your actual connection string

        public Student_interface()
        {
            InitializeComponent();
        }

        String Username;
        public void info(string uname)
        {
            Username=uname;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // submission button
            submit_proposal submit_proposal = new submit_proposal(); // Assuming your sign-up form class is named SignUpForm
            submit_proposal.info(Username);

            this.Hide(); // Optional: Hide the login form
            submit_proposal.ShowDialog(); // Show the sign-up form as a dialog
            submit_proposal.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //update button
            Update_thesis Update_thesis = new Update_thesis(); // Assuming your sign-up form class is named SignUpForm
            Update_thesis.info(Username);

            this.Hide(); // Optional: Hide the login form
            Update_thesis.ShowDialog(); // Show the sign-up form as a dialog
            Update_thesis.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // delete button
            int studentID = GetStudentID(Username);

            int proposalID = GetProposalIDByStudentID(studentID);
            if (proposalID != -1)
            {
                if (IsProposalCreatedByStudent(proposalID, studentID))
                {
                    WithdrawProposal(proposalID);
                    MessageBox.Show("Proposal Deleted Successfully!");
                }
            }
            else
            {
                MessageBox.Show("Unable to find a matching proposal!");

            }
        }

        private int GetStudentID(string username)
        {
            // Use the provided username to find the corresponding StudentID from the UserRole table
            if (string.IsNullOrEmpty(username))
            {
                // Handle the case where the username is null or empty
                return -1; // Or another appropriate value
            }

            // Implement the SQL query to get the StudentID
            string query = "SELECT StudentID FROM Student WHERE Name = @Username";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();
                    if (result == DBNull.Value)
                    {
                        // Handle the case where the username is not found
                        // You may want to display an error message or log the issue
                        return -1;
                    }

                    return Convert.ToInt32(result);
                }
            }
        }


        private int GetProposalIDByStudentID(int studentID)
        {
            // Retrieve the corresponding ProposalID from the ThesisProposal table based on the found StudentID

            // Implement the SQL query to get the ProposalID
            string query = "SELECT ThesisProposalID FROM ThesisProposal WHERE StudentID = @StudentID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID);

                    object result = command.ExecuteScalar();
                    if (result == DBNull.Value)
                    {
                        // Handle the case where there is no matching ProposalID
                        // You may want to display an error message or log the issue
                        return -1;
                    }

                    return Convert.ToInt32(result);
                }
            }
        }
        private bool IsProposalCreatedByStudent(int thesisProposalID, int studentID)
        {
            // Check if the Proposal is made by the same student

            // Implement the SQL query to check if the Proposal is made by the same student
            string query = "SELECT COUNT(*) FROM ThesisProposal WHERE ThesisProposalID = @ThesisProposalID AND StudentID = @StudentID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ThesisProposalID", thesisProposalID);
                    command.Parameters.AddWithValue("@StudentID", studentID);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        // withdrawn function|

        private void WithdrawProposal(int thesisProposalID)
        {
            string query = "DELETE FROM ThesisProposal WHERE ThesisProposalID = @ThesisProposalID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ThesisProposalID", thesisProposalID);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(); // Assuming your sign-up form class is named SignUpForm

            this.Hide(); // Optional: Hide the login form
            Form1.ShowDialog(); // Show the sign-up form as a dialog
            Form1.Show(); // This will open the sign-up form as a separate window
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //update button
            Comments_student Comments_student = new Comments_student(); // Assuming your sign-up form class is named SignUpForm
            Comments_student.info(Username);

            this.Hide(); // Optional: Hide the login form
            Comments_student.ShowDialog(); // Show the sign-up form as a dialog
            Comments_student.Show();
        }

        // button to vew the reviews
        private void button6_Click(object sender, EventArgs e)
        {
            //update button
            student_view_reviews student_view_reviews = new student_view_reviews(); // Assuming your sign-up form class is named SignUpForm
            student_view_reviews.info(Username);

            this.Hide(); // Optional: Hide the login form
            student_view_reviews.ShowDialog(); // Show the sign-up form as a dialog
            student_view_reviews.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

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

namespace DataBase_Project
{
    public partial class Update_thesis : Form
    {
        private const string ConnectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"; // Replace with your actual connection string

        public Update_thesis()
        {
            InitializeComponent();
        }
        string username;
        public void info(string uname)
        { 
           username = uname;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int studentID = GetStudentID(username);
            int proposalID = GetProposalIDByStudentID(studentID);
            if (proposalID != -1)
            {
                UpdateProposal(proposalID);
                MessageBox.Show("Proposal Updated");

               
            }


           // going back to menu
            Student_interface Student_interface = new Student_interface(); // Assuming your sign-up form class is named SignUpForm

            this.Hide(); // Optional: Hide the login form
            Student_interface.ShowDialog(); // Show the sign-up form as a dialog
            Student_interface.Show(); // This will open the sign-up form as a separate window
            this.Close();

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

        private void UpdateProposal(int thesisProposalID)
        {
            // Update the existing proposal in the ThesisProposal table using the gathered information

            // Implement the SQL query to update the proposal
            string query = "UPDATE ThesisProposal SET Title = @Title WHERE ThesisProposalID = @ThesisProposalID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set parameters for the query using the values from your form controls
                    command.Parameters.AddWithValue("@ThesisProposalID", thesisProposalID);
                    command.Parameters.AddWithValue("@Title", txttitle.Text);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }

        private void txttitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Student_interface Student_interface = new Student_interface(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Student_interface.ShowDialog(); // Show the sign-up form as a dialog
            Student_interface.Show();
        }
    }
}

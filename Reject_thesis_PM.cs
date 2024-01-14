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
    public partial class Reject_thesis_PM : Form
    {
        public Reject_thesis_PM()
        {
            InitializeComponent();
        }
        int studentID;
        public void info(int id)
        {
            studentID = id;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Get user input from text boxes


            string comments = commentsTextBox.Text;
            string evaluation = evaluationTextBox.Text;

            // Call the function to submit the review
            SubmitReview(studentID, comments, evaluation);

        }


        private void SubmitReview(int studentID, string comments, string evaluation)
        {
            // Get the corresponding ThesisProposalID from the ThesisProposal table
            int thesisProposalID = GetThesisProposalID(studentID);

            if (thesisProposalID == -1)
            {
                // Handle the case where the student ID is not found
                MessageBox.Show("Student ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Connect to the database (replace "YourConnectionString" with the actual connection string)
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Insert data into the Review table
                    string insertQuery = "INSERT INTO Review (Comments, Evaluation, Approval, ThesisProposalID) " +
                                         "VALUES (@Comments, @Evaluation, 0,@ThesisProposalID)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        // Set parameters for the query using the values from your form controls
                        insertCommand.Parameters.AddWithValue("@Comments", comments);
                        insertCommand.Parameters.AddWithValue("@Evaluation", evaluation);
                        insertCommand.Parameters.AddWithValue("@ThesisProposalID", thesisProposalID);

                        // Execute the query
                        insertCommand.ExecuteNonQuery();

                        // Update the Approved column in the ThesisProposal table
                        string updateQuery = "UPDATE ThesisProposal SET Approved = 0 WHERE ThesisProposalID = @ThesisProposalID";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@ThesisProposalID", thesisProposalID);
                            updateCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Review submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Handle database connection or query execution errors
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Function to get ThesisProposalID based on StudentID
        private int GetThesisProposalID(int studentID)
        {
            // Implement the SQL query to get the ThesisProposalID
            string query = "SELECT ThesisProposalID FROM ThesisProposal WHERE StudentID = @StudentID";

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID);

                    object result = command.ExecuteScalar();
                    if (result == DBNull.Value)
                    {
                        // Handle the case where the student ID is not found
                        return -1;
                    }

                    return Convert.ToInt32(result);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PanelM_Review PanelM_Review = new PanelM_Review(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            PanelM_Review.ShowDialog(); // Show the sign-up form as a dialog
            PanelM_Review.Show(); // This will open the sign-up form as a separate window

        }
    }
}

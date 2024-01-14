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
    public partial class student_view_reviews : Form
    {

        private const string ConnectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"; // Replace with your actual connection string

        public student_view_reviews()
        {
            InitializeComponent();
        }
        string Username;
        public void info(string un)
        { 
          Username = un;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Get the student ID from the user input
            int studentID = GetStudentID(Username);

            if (studentID == -1)
            {
                MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Connect to the database (replace "YourConnectionString" with the actual connection string)
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Check if the student has reviews in the Review table
                    string reviewCheckQuery = "SELECT COUNT(*) FROM Review WHERE ThesisProposalID IN (SELECT ThesisProposalID FROM ThesisProposal WHERE StudentID = @StudentID)";
                    using (SqlCommand reviewCheckCommand = new SqlCommand(reviewCheckQuery, connection))
                    {
                        reviewCheckCommand.Parameters.AddWithValue("@StudentID", studentID);

                        int reviewCount = (int)reviewCheckCommand.ExecuteScalar();

                        if (reviewCount > 0)
                        {
                            // Student has reviews, fetch and display them in the grid
                            string reviewQuery = "SELECT Comments, Evaluation, Approval FROM Review WHERE ThesisProposalID IN (SELECT ThesisProposalID FROM ThesisProposal WHERE StudentID = @StudentID)";
                            using (SqlDataAdapter adapter = new SqlDataAdapter(reviewQuery, connection))
                            {
                                adapter.SelectCommand.Parameters.AddWithValue("@StudentID", studentID);

                                DataTable reviewDataTable = new DataTable();
                                adapter.Fill(reviewDataTable);

                                // Assuming you have a DataGridView named dataGridViewReviews
                                dataGridView1.DataSource = reviewDataTable;
                            }
                        }
                        else
                        {
                            // No reviews found
                            MessageBox.Show("No reviews found for the specified student ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle database connection or query execution errors
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int GetStudentID(string username)
        {
            // Use the provided username to find the corresponding StudentID from the Student table
            if (string.IsNullOrEmpty(username))
            {
                // Handle the case where the username is null or empty
                return -1; // Or another appropriate value
            }

            username = username.Trim();

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

        private void button2_Click(object sender, EventArgs e)
        {
            Student_interface Student_interface = new Student_interface(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Student_interface.ShowDialog(); // Show the sign-up form as a dialog
            Student_interface.Show();
        }
    }
}

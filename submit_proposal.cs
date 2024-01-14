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
    public partial class submit_proposal : Form
    {
        private const string ConnectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"; // Replace with your actual connection string
        public submit_proposal()
        {
            InitializeComponent();
        }
        string Username;
        public void info(string uname)
        {
            Username = uname;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int Student_id = GetStudentID(Username);
           int Supervisor_id= GetSupervisorID(Username);

            InsertProposal(Student_id, Supervisor_id);
            MessageBox.Show("Proposal submitted!");

            //going back to menu
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


        private int GetSupervisorID(string username)
        {
            // Use the provided username to find the corresponding StudentID from the UserRole table

            // Implement the SQL query to get the StudentID
            string query = "SELECT SupervisorID FROM Student WHERE Name = @Username";

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



        private void InsertProposal(int studentID, int supervisorID)
        {
            // Insert the new proposal into the ThesisProposal table using the gathered information

            // Implement the SQL query to insert the new proposal
            string query = "INSERT INTO ThesisProposal (Title, Approved, UnderReview, Submitted, StudentID, SupervisorID) " +
                           "VALUES (@Title, 0, 0, 1, @StudentID, @SupervisorID)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set parameters for the query using the values from your form controls
                    command.Parameters.AddWithValue("@Title", txttitle.Text);
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    command.Parameters.AddWithValue("@SupervisorID", supervisorID);

                    // Execute the query
                    command.ExecuteNonQuery();
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

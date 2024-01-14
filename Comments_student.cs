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
    public partial class Comments_student : Form
    {

        private const string ConnectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"; // Replace with your actual connection string

        public Comments_student()
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
            int studentID= GetStudentID(Username);
           

            // Connect to the database (replace "YourConnectionString" with the actual connection string)
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Check if there are notifications with comments for the specified student ID
                    string query = "SELECT Comment FROM Notifications WHERE RecipientID = @StudentID AND Comment IS NOT NULL";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentID);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            // Notifications with comments found, display them in a text box
                            StringBuilder commentsBuilder = new StringBuilder();
                            while (reader.Read())
                            {
                                string comment = reader.GetString(0);
                                commentsBuilder.AppendLine(comment);
                            }

                            commentsTextBox.Text = commentsBuilder.ToString();
                        }
                        else
                        {
                            // No notifications with comments found
                            MessageBox.Show("No comments found for the specified student ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Use the provided username to find the corresponding StudentID from the UserRole table
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

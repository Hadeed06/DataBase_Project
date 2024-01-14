using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataBase_Project
{
    public partial class signUp1 : Form
    {
        public signUp1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void signUp1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Get user input
            string role = comboBoxRole.Text; // Assuming role is selected from a combo box
            string userName = textBoxUserName.Text;
            string password = textBoxPassword.Text;

            // Validate that username and password are not empty
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Connect to the database (replace "YourConnectionString" with the actual connection string)
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Check if the username already exists in the database
                    string checkDuplicateQuery = "SELECT COUNT(*) FROM UserRole WHERE UserName = @UserName";
                    using (SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, connection))
                    {
                        checkDuplicateCommand.Parameters.AddWithValue("@UserName", userName);

                        int existingUserCount = (int)checkDuplicateCommand.ExecuteScalar();

                        if (existingUserCount > 0)
                        {
                            // User with the same username already exists
                            MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Insert the new user into the database
                    string insertQuery = "INSERT INTO UserRole (RoleName, Password, UserName) VALUES (@Role, @Password, @UserName)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Role", role);
                        insertCommand.Parameters.AddWithValue("@UserName", userName);
                        insertCommand.Parameters.AddWithValue("@Password", password);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Registration successful
                            MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Add further logic here if needed
                        }
                        else
                        {
                            // Registration failed
                            MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Retrieve the last entered role from the UserRole table
            string userRole = GetLastEnteredRole();

            // Navigate to the corresponding page based on the user's role
            switch (userRole.ToLower())
            {
                case "student":
                    // Open student page
                    OpenStudentPage();
                    break;

                case "supervisor":
                    // Open supervisor page
                    OpenSupervisorPage();
                    break;

                case "panelmember":
                    // Open panel member page
                    OpenPanelMemberPage();
                    break;

                case "administrator":
                    // Open administrator page
                    OpenAdministratorPage();
                    break;

                default:
                    // Handle unknown role or any other logic
                    MessageBox.Show("Unknown role or error in retrieving user role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        // Method to retrieve the last entered role from the UserRole table
        // Method to retrieve the last entered role from the UserRole table
        private string GetLastEnteredRole()
        {
            string lastEnteredRole = ""; // Initialize with a default value or handle accordingly

            // Connect to the database and retrieve the last entered role
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Assuming the UserRole table has an auto-incremented ID column
                    string query = "SELECT TOP 1 RoleName FROM UserRole ORDER BY ID DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and retrieve the last entered role
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            lastEnteredRole = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle database connection or query execution errors
                    MessageBox.Show($"An error occurred while retrieving the last entered role: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return lastEnteredRole;
        }


        // Methods to open corresponding pages (unchanged)
        private void OpenStudentPage()
        {
            MessageBox.Show("Open Student Page", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // code to open student form
            Student_signup Student_signup = new Student_signup(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Student_signup.ShowDialog(); // Show the sign-up form as a dialog
            Student_signup.Show(); // This will open the sign-up form as a separate window
        }

        private void OpenSupervisorPage()
        {
            MessageBox.Show("Open Supervisor Page", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // code to open supervisor form
            Supervisor_signup Supervisor_signup = new Supervisor_signup(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Supervisor_signup.ShowDialog(); // Show the sign-up form as a dialog
            Supervisor_signup.Show(); // This will open the sign-up form as a separate window
        }

        private void OpenPanelMemberPage()
        {
            MessageBox.Show("Open Panel Member Page", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // code to open panel form
            PanelMember_signup PanelMember_signup = new PanelMember_signup(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            PanelMember_signup.ShowDialog(); // Show the sign-up form as a dialog
            PanelMember_signup.Show(); // This will open the sign-up form as a separate window
        }

        private void OpenAdministratorPage()
        {
            MessageBox.Show("Open Administrator Page", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // code to open supervisor form

            Administrator_signup Administrator_signup = new Administrator_signup(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Administrator_signup.ShowDialog(); // Show the sign-up form as a dialog
            Administrator_signup.Show(); // This will open the sign-up form as a separate window
        }

    }
}

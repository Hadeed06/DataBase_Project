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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get user input
            string role = role_box.Text; // Assuming role is selected from a combo box
            string userName = nameBox.Text;
            string password = password_box.Text;

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

                    // Check if the user with the given credentials exists in the database
                    string query = "SELECT COUNT(*) FROM UserRole WHERE RoleName = @RoleName AND UserName = @UserName AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleName", role);
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@Password", password);

                        int userCount = (int)command.ExecuteScalar();

                        if (userCount > 0)
                        {
                            // Successful login
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Add further logic here, such as navigating to another form

                            // Retrieve the user's role
                            string roleQuery = "SELECT RoleName FROM UserRole WHERE UserName = @UserName";
                            using (SqlCommand roleCommand = new SqlCommand(roleQuery, connection))
                            {
                                roleCommand.Parameters.AddWithValue("@UserName", userName);
                                string userRole = roleCommand.ExecuteScalar().ToString();

                                // Display additional message based on the user's role
                                switch (userRole.ToLower())
                                {
                                    case "student":
                                        MessageBox.Show("Welcome, Student!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                       // Add logic for student role

                                        //opening the student page

                                       Student_interface Student_interface = new Student_interface(); // Assuming your sign-up form class is named SignUpForm
                                        Student_interface.info(userName);

                                        this.Hide(); // Optional: Hide the login form
                                        Student_interface.ShowDialog(); // Show the sign-up form as a dialog
                                        Student_interface.Show(); // This will open the sign-up form as a separate window
                                        break;

                                    case "administrator":
                                        MessageBox.Show("Welcome, Administrator!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Admin_interface Admin_interface = new Admin_interface(); // Assuming your sign-up form class is named SignUpForm
                                        this.Hide(); // Optional: Hide the login form
                                        Admin_interface.ShowDialog(); // Show the sign-up form as a dialog
                                        Admin_interface.Show(); // This will open the sign-up form as a separate window


                                        // Add logic for administrator role
                                        break;

                                    case "panelmember":
                                        MessageBox.Show("Welcome, Panel Member!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        // Add logic for panel member role
                                        PanelMember_interface PanelMember_interface = new PanelMember_interface(); // Assuming your sign-up form class is named SignUpForm
                                        this.Hide(); // Optional: Hide the login form
                                        PanelMember_interface.ShowDialog(); // Show the sign-up form as a dialog
                                        PanelMember_interface.Show(); // This will open the sign-up form as a separate window




                                        break;

                                    case "supervisor":
                                        MessageBox.Show("Welcome, Supervisor!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Suoervisor_interface suoervisorInterface = new Suoervisor_interface(); // Corrected class name
                                        this.Hide();
                                        suoervisorInterface.ShowDialog();
                                        suoervisorInterface.Show();
                                        // Add logic for supervisor role
                                        break;

                                    // Add cases for other roles as needed

                                    default:
                                        // Handle unknown role or any other logic
                                        break;
                                }
                            }


                        }
                        else
                        {
                            // Invalid credentials
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        // function to goto signup page #1
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            signUp1 signUp1 = new signUp1(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            signUp1.ShowDialog(); // Show the sign-up form as a dialog
            signUp1.Show(); // This will open the sign-up form as a separate window
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class Supervisor_signup : Form
    {
        public Supervisor_signup()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                // Get user input
                string name = textBoxName.Text;
                string password = textBoxPassword.Text;
                string email = textBoxEmail.Text;
                string contactNumber = textBoxContactNumber.Text;

                // Validate that name, password, email, and contact number are not empty
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contactNumber))
                {
                    MessageBox.Show("Please enter all required information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Connect to the database (replace "YourConnectionString" with the actual connection string)
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"))
                {
                    try
                    {
                        connection.Open();

                        // Check if the name already exists in the database
                        string checkDuplicateQuery = "SELECT COUNT(*) FROM Supervisor WHERE Name = @Name";
                        using (SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, connection))
                        {
                            checkDuplicateCommand.Parameters.AddWithValue("@Name", name);

                            int existingNameCount = (int)checkDuplicateCommand.ExecuteScalar();

                            if (existingNameCount > 0)
                            {
                                // Panel member with the same name already exists
                                MessageBox.Show("Name already exists. Please choose a different name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Check if the password already exists in the database
                        string checkPasswordQuery = "SELECT COUNT(*) FROM Supervisor WHERE Password = @Password";
                        using (SqlCommand checkPasswordCommand = new SqlCommand(checkPasswordQuery, connection))
                        {
                            checkPasswordCommand.Parameters.AddWithValue("@Password", password);

                            int existingPasswordCount = (int)checkPasswordCommand.ExecuteScalar();

                            if (existingPasswordCount > 0)
                            {
                                // Panel member with the same password already exists
                                MessageBox.Show("Password already exists. Please choose a different password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Insert the new panel member into the database
                        string insertQuery = "INSERT INTO Supervisor (Name, Password, Email, ContactNumber) VALUES (@Name, @Password, @Email, @ContactNumber)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Name", name);
                            insertCommand.Parameters.AddWithValue("@Password", password);
                            insertCommand.Parameters.AddWithValue("@Email", email);
                            insertCommand.Parameters.AddWithValue("@ContactNumber", contactNumber);

                            int rowsAffected = insertCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Registration successful
                                MessageBox.Show("Supervisor registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Add further logic here if needed
                            }
                            else
                            {
                                // Registration failed
                                MessageBox.Show("Supervisor registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Form1.ShowDialog(); // Show the sign-up form as a dialog
            Form1.Show();
        }
    }
}

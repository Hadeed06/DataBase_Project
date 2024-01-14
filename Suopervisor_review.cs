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
    public partial class Suopervisor_review : Form
    {
        public Suopervisor_review()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get user input from text boxes
            string recipientIDText = recipientIDTextBox.Text;
            if (string.IsNullOrWhiteSpace(recipientIDText) || !int.TryParse(recipientIDText, out int recipientID))
            {
                MessageBox.Show("Please enter a valid recipient ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string comment = commentTextBox.Text;

            // Connect to the database (replace "YourConnectionString" with the actual connection string)
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Insert the new notification into the Notifications table
                    string insertQuery = "INSERT INTO Notifications (Read_, RecipientID, Comment) VALUES (1, @RecipientID, @Comment)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@RecipientID", recipientID);
                        insertCommand.Parameters.AddWithValue("@Comment", comment);

                        // Execute the query
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Notification insertion successful
                            MessageBox.Show("Notification added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Add further logic here if needed
                        }
                        else
                        {
                            // Notification insertion failed
                            MessageBox.Show("Notification addition failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Suoervisor_interface Suoervisor_interface = new Suoervisor_interface(); // Assuming your sign-up form class is named SignUpForm

            this.Hide(); // Optional: Hide the login form
            Suoervisor_interface.ShowDialog(); // Show the sign-up form as a dialog
            Suoervisor_interface.Show();
        }
    }
}

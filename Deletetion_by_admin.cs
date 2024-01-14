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
    public partial class Deletetion_by_admin : Form
    {
        public Deletetion_by_admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the table name entered by the user
            string tableName = textBoxTableName.Text.Trim();

            // Get the row identifier entered by the user
            string rowIdentifier = textBoxRowIdentifier.Text.Trim();

            // Validate if a table name and row identifier are entered
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(rowIdentifier))
            {
                MessageBox.Show("Please enter both the table name and the row identifier.");
                return;
            }

            // Get the primary key column of the specified table
            string primaryKeyColumn = GetPrimaryKeyColumn(tableName);

            // Validate if a primary key column is found
            if (string.IsNullOrEmpty(primaryKeyColumn))
            {
                MessageBox.Show($"Primary key column not found for table '{tableName}'.");
                return;
            }



            // Delete the specified row from the database
            bool success = DeleteRowFromTable(tableName, primaryKeyColumn, rowIdentifier);

            // Display a message based on the deletion result
            if (success)
            {
                MessageBox.Show("Row deleted successfully.");
            }
            else
            {
                MessageBox.Show("Failed to delete the specified row. Please check your input.");
            }
        }

        // Sample method to delete a row from a table in a SQL Server database
        private bool DeleteRowFromTable(string tableName,string PrimaryKeyColumn, string rowIdentifier)
        {
            // Replace the connection string with your own
            string connectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct a query to delete the specified row
                string query = $"DELETE FROM {tableName} WHERE PrimaryKeyColumn = @RowIdentifier";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Replace 'YourPrimaryKeyColumn' with the actual primary key column of the table
                    command.Parameters.AddWithValue("@RowIdentifier", rowIdentifier);

                    try
                    {
                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Return true if at least one row was deleted
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception (e.g., log it, show an error message)
                        MessageBox.Show($"Error: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        // Sample method to get the primary key column of a table
        private string GetPrimaryKeyColumn(string tableName)
        {
            // Replace the connection string with your own
            string connectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to get the primary key column of the specified table
                string query = @"
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
            WHERE OBJECT_NAME(OBJECT_ID) = @TableName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableName", tableName);

                    // Execute the query to get the primary key column
                    object result = command.ExecuteScalar();

                    // Return null if no primary key column is found
                    return result?.ToString();
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin_interface Admin_interface = new Admin_interface(); // Assuming your sign-up form class is named SignUpForm
            this.Hide(); // Optional: Hide the login form
            Admin_interface.ShowDialog(); // Show the sign-up form as a dialog
            Admin_interface.Show();
        }
    }
}

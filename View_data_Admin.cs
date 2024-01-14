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
    public partial class View_data_Admin : Form
    {
        public View_data_Admin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the table name entered by the user
            string tableName = textBoxTableName.Text.Trim();

            // Validate if a table name is entered
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Please enter a table name.");
                return;
            }

            // Fetch data from the database based on the entered table name
            DataTable dataTable = GetDataFromTable(tableName);

            // Display the data in a DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private DataTable GetDataFromTable(string tableName)
        {
            // Replace the connection string with your own
            string connectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct a query based on the entered table name
                string query = $"SELECT * FROM {tableName}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
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

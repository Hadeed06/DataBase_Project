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
    public partial class Thesis_view_by_supervisor : Form
    {
        private const string ConnectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"; // Replace with your actual connection string

        public Thesis_view_by_supervisor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a DataTable to store the data
            DataTable dataTable = new DataTable();

            // Implement the SQL query to select all rows from the ThesisProposal table
            string query = "SELECT * FROM ThesisProposal";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // Fill the DataTable with the data from the ThesisProposal table
                        adapter.Fill(dataTable);
                    }
                }
            }

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;
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

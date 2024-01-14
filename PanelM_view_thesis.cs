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
    public partial class PanelM_view_thesis : Form
    {
        public PanelM_view_thesis()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Connect to the database (replace "YourConnectionString" with the actual connection string)
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Retrieve data from the ThesisProposal table
                    string query = "SELECT * FROM ThesisProposal";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the data to the DataGridView
                        dataGridViewThesis.DataSource = dataTable;
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

            PanelMember_interface PanelMember_interface = new PanelMember_interface(); // Assuming your sign-up form class is named SignUpForm

            this.Hide(); // Optional: Hide the login form
            PanelMember_interface.ShowDialog(); // Show the sign-up form as a dialog
            PanelMember_interface.Show();
        }
    }
}

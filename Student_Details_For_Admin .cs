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
    public partial class Student_Details_For_Admin : Form
    {
        public Student_Details_For_Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int thesisProposalID = int.Parse(textBox1.Text);

                string connectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT *
                             FROM Student s
                             JOIN Supervisor sup ON s.SupervisorID = sup.SupervisorID
                             JOIN ThesisProposal tp ON s.StudentID = tp.StudentID
                             JOIN Review r ON tp.ThesisProposalID = r.ThesisProposalID
                             WHERE tp.ThesisProposalID = @ThesisProposalID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ThesisProposalID", thesisProposalID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
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

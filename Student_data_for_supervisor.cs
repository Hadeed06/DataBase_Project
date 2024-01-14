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
    public partial class Student_data_for_supervisor : Form
    {


        private const string ConnectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"; // Replace with your actual connection string


        public Student_data_for_supervisor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if StudentID is provided
            if (string.IsNullOrEmpty(txtStudentID.Text))
            {
                MessageBox.Show("Please enter a Student ID.");
                return;
            }

            int studentID;
            if (!int.TryParse(txtStudentID.Text, out studentID))
            {
                MessageBox.Show("Invalid Student ID. Please enter a valid integer.");
                return;
            }

            try
            {
                // Execute the SQL query
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT
                            s.StudentID,
                            s.Name AS StudentName,
                            t.Title AS ThesisTitle,
                            r.Comments AS ReviewComments
                        FROM
                            Student s
                        JOIN
                            ThesisProposal t ON s.StudentID = t.StudentID
                        LEFT JOIN
                            Review r ON t.ThesisProposalID = r.ThesisProposalID
                        JOIN
                            Supervisor sup ON s.SupervisorID = sup.SupervisorID
                        WHERE
                            s.StudentID = @StudentID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Display the results in the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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
    

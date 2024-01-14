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
    public partial class View_Reviews_and_Panel_Members_for_Each_Thesis_Proposal : Form
    {
        public View_Reviews_and_Panel_Members_for_Each_Thesis_Proposal()
        {
            InitializeComponent();
        }
        private const string ConnectionString = "Data Source=DESKTOP-3TMHGIH\\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True"; 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the Thesis Proposal ID from the TextBox
                if (int.TryParse(textBox1.Text, out int thesisProposalID))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Your SQL query
                        string query = @"
                            SELECT r.ReviewID, r.Comments, r.Evaluation, r.Approval,
                           pm.PanelMemberID, pm.Name AS PanelMemberName,
                           tp.ThesisProposalID, tp.Title, tp.Approved, tp.UnderReview, tp.Submitted
                            FROM Review r
                            LEFT JOIN PanelMember pm ON r.PanelMemberID = pm.PanelMemberID
                            LEFT JOIN ThesisProposal tp ON r.ThesisProposalID = tp.ThesisProposalID
                            WHERE r.ThesisProposalID = @ThesisProposalID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters to the query
                            command.Parameters.AddWithValue("@ThesisProposalID", thesisProposalID);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid Thesis Proposal ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

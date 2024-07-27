using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Coursework
{
    public partial class Teacher_DIS : Form
    {
        public Teacher_DIS()
        {
            InitializeComponent();
        }

        private void teacherBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.teacherBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Subject' table. You can move, or remove it, as needed.
            this.subjectTableAdapter.Fill(this.database1DataSet.Subject);
            // TODO: This line of code loads data into the 'database1DataSet.Teacher' table. You can move, or remove it, as needed.
            this.teacherTableAdapter.Fill(this.database1DataSet.Teacher);

        }
        // Reload the subject list
        private void btn_reload_Click(object sender, EventArgs e)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Subject";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    subjectDataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        // Show the teacher list with the subject they teach
        private void btn_teacher_teach_Click(object sender, EventArgs e)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT t.TeacherID, t.Name, t.Salary, su.Name AS Teach " +
                                   "FROM Teacher t " +
                                   "INNER JOIN Subject su ON t.Teach = su.SubjectID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    Customized_List.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        // Load out the student list
        private void btn_load_student_Click(object sender, EventArgs e)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Student";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    Customized_List.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        // Add new subject to the database
        private void btn_add_Click(object sender, EventArgs e)
        {
            string name = tb_subject_name.Text;
            string status = selectedStatus();

            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a subject name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Subject newSubject = new Subject(name, status);

            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";
            
            try
            {
                newSubject.AddSubject(connectionString);
                MessageBox.Show("Subject added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            // Refresh DataGridView to reflect changes
            btn_reload_Click(sender, e);
        }
        private string selectedStatus()
        {
            if (rb_status_in_progress.Checked)
            {
                return "In Progress";
            }
            else if (rb_status_done.Checked)
            {
                return "Done";
            }
            else
            {
                MessageBox.Show("Please select a status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        // Remove subject from database
        private void btn_remove_Click(object sender, EventArgs e)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";
            
            string name = tb_subject_name.Text;
            string status = selectedStatus();

            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a subject name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Subject subjectToRemove = new Subject(name, status);

            try
            {
                subjectToRemove.RemoveSubject(connectionString);
                MessageBox.Show("Subject removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btn_reload_Click(sender, e);
        }
        // Edit subject's details
        private void btn_modify_Click(object sender, EventArgs e)
        {
            string name = tb_subject_name.Text;
            string status = selectedStatus();

            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a subject name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Subject modifySubject = new Subject(name, status);

            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";
            
            try
            {
                modifySubject.ModifySubject(connectionString);
                MessageBox.Show("Subject changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            // Refresh DataGridView to reflect changes
            btn_reload_Click(sender, e);
        }
        // Back to login window
        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Coursework
{
    public partial class Admin_DIS : Form
    {
        public Admin_DIS()
        {
            InitializeComponent();

            // hide password by default
            tb_password.UseSystemPasswordChar = true;

            // hide worktime calculation result
            work_time_display_group.Visible = false;

            // hide all panel
            panel_teacher.Visible = false;
            panel_student.Visible = false;
            panel_subject.Visible = false;
        }

        private void teacherBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.teacherBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Admin_DIS_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Subject' table. You can move, or remove it, as needed.
            this.subjectTableAdapter.Fill(this.database1DataSet.Subject);
            // TODO: This line of code loads data into the 'database1DataSet.Teacher' table. You can move, or remove it, as needed.
            this.teacherTableAdapter.Fill(this.database1DataSet.Teacher);

        }

        // ADMIN TAB //
        private void checkbox_show_password_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle between showing and hiding the password characters
            tb_password.UseSystemPasswordChar = !checkbox_show_password.Checked;
        }

        // Admin working time calculation:
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            work_time_display_group.Visible = true;

            DateTime startDate = dateTimePicker.Value;
            bool isFullTime = rb_fulltime.Checked; // Determine if full-time is selected
            TimeSpan workingHours = CalculateWorkingHours(startDate, isFullTime);
            work_time_display.Text = $"{workingHours.TotalHours:F2} hours";
        }
        private TimeSpan CalculateWorkingHours(DateTime startDate, bool isFullTime)
        {
            DateTime endDate = DateTime.Now; // Current time
            int hoursPerDay = isFullTime ? 8 : 4; // Determine the number of hours per day based on the selected option
            TimeSpan totalWorkingHours = endDate - startDate;
            int totalDays = (int)Math.Ceiling(totalWorkingHours.TotalDays); // Round up to the nearest whole day
            TimeSpan workingHours = TimeSpan.FromHours(totalDays * hoursPerDay);
            return workingHours;
        }
        private void rb_fulltime_CheckedChanged(object sender, EventArgs e)
        {
            // Calculate working hours when Full-time radio button is checked
            if (rb_fulltime.Checked)
            {
                UpdateWorkingHours();
            }
        }
        private void rb_parttime_CheckedChanged(object sender, EventArgs e)
        {
            // Calculate working hours when Part-time radio button is checked
            if (rb_parttime.Checked)
            {
                UpdateWorkingHours();
            }
        }
        private void UpdateWorkingHours()
        {
            DateTime startDate = dateTimePicker.Value;
            bool isFullTime = rb_fulltime.Checked;

            TimeSpan workingHours = CalculateWorkingHours(startDate, isFullTime);

            work_time_display.Text = $"{workingHours.TotalHours:F2} hours";
        }

        // SYSTEM MANAGEMENT TAB //
        // 3 load buttons
        private void btn_load_teacher_Click(object sender, EventArgs e)
        {
            panel_teacher.Visible = true;
            panel_student.Visible = false;
            panel_subject.Visible = false;

            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Teacher";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    customized_list.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void btn_load_student_Click(object sender, EventArgs e)
        {
            panel_student.Visible = true;
            panel_teacher.Visible = false;
            panel_subject.Visible = false;

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
                    customized_list.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void btn_load_subject_Click(object sender, EventArgs e)
        {
            panel_subject.Visible = true;
            panel_student.Visible = false;
            panel_teacher.Visible = false;

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
                    customized_list.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        //Modify teacher panel buttons
        private void btn_add_teacher_Click(object sender, EventArgs e)
        {
            string name = tb_teacher_name.Text;
            string telephone = tb_teacher_telephone.Text;
            string email = tb_teacher_email.Text;
            string salaryStr = tb_teacher_salary.Text;
            string teach = cb_teacher_teach.Text;

            // Validate and parse salary
            int salary;
            if (!int.TryParse(salaryStr, out salary))
            {
                MessageBox.Show("Please enter a valid salary.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a teacher name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Teacher newTeacher = new Teacher(name, telephone, email, salary, teach);

            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";

            try
            {
                newTeacher.AddTeacher(connectionString);
                MessageBox.Show("Teacher added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            btn_load_teacher_Click(sender, e);
        }

        private void btn_modify_teacher_Click(object sender, EventArgs e)
        {
            string name = tb_teacher_name.Text;
            string telephone = tb_teacher_telephone.Text;
            string email = tb_teacher_email.Text;
            string salaryStr = tb_teacher_salary.Text;
            string teach = cb_teacher_teach.SelectedValue.ToString();

            // Validate and parse salary
            int salary;
            if (!int.TryParse(salaryStr, out salary))
            {
                MessageBox.Show("Please enter a valid salary.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a teacher name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Teacher modifyTeacher = new Teacher(name, telephone, email, salary, teach);

            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";

            try
            {
                modifyTeacher.ModifyTeacher(connectionString);
                MessageBox.Show("Teacher changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            btn_load_teacher_Click(sender, e);
        }

        private void btn_remove_teacher_Click(object sender, EventArgs e)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";

            string name = tb_teacher_name.Text;

            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a teacher name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Teacher TeacherToRemove = new Teacher(name, "", "", 0, "");

            try
            {
                TeacherToRemove.RemoveTeacher(connectionString);
                MessageBox.Show("Teacher removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            btn_load_teacher_Click(sender, e);
        }

        //Modify student panel buttons
        private void btn_add_student_Click(object sender, EventArgs e)
        {
            string name = tb_student_name.Text;
            string telephone = tb_student_telephone.Text;
            string email = tb_student_email.Text;

            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a student name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Student newStudent = new Student(name, telephone, email);

            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";

            try
            {
                newStudent.AddStudent(connectionString);
                MessageBox.Show("Student added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            btn_load_student_Click(sender, e);
        }
        private void btn_modify_student_Click(object sender, EventArgs e)
        {
            string name = tb_student_name.Text;
            string telephone = tb_student_telephone.Text;
            string email = tb_student_email.Text;

            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a student name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Student modifyStudent = new Student(name, telephone, email);

            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";

            try
            {
                modifyStudent.ModifyStudent(connectionString);
                MessageBox.Show("Student changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            btn_load_student_Click(sender, e);
        }
        private void btn_remove_student_Click(object sender, EventArgs e)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";

            string name = tb_student_name.Text;

            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a student name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Student StudentToRemove = new Student(name, "", "");

            try
            {
                StudentToRemove.RemoveStudent(connectionString);
                MessageBox.Show("Student removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            btn_load_student_Click(sender, e);
        }

        //Modify subject buttons
        private void btn_add_subject_Click(object sender, EventArgs e)
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
            btn_load_subject_Click(sender, e);
        }
        private string selectedStatus()
        {
            if (rb_inprogress.Checked)
            {
                return "In Progress";
            }
            else if (rb_done.Checked)
            {
                return "Done";
            }
            else
            {
                MessageBox.Show("Please select a status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private void btn_modify_subject_Click(object sender, EventArgs e)
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
            btn_load_subject_Click(sender, e);
        }

        private void btn_remove_subject_Click(object sender, EventArgs e)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Construct the connection string
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\Database1.mdf;Integrated Security=True";

            string name = tb_subject_name.Text;

            // Validate input
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a subject name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Subject subjectToRemove = new Subject(name, null);

            try
            {
                subjectToRemove.RemoveSubject(connectionString);
                MessageBox.Show("Subject removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btn_load_subject_Click(sender, e);
        }
        // back to login window
        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class Student_DIS : Form
    {
        public Student_DIS()
        {
            InitializeComponent();
        }

        private void studentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.studentBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Student_DIS_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Subject' table. You can move, or remove it, as needed.
            this.subjectTableAdapter.Fill(this.database1DataSet.Subject);
            // TODO: This line of code loads data into the 'database1DataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.database1DataSet.Student);

        }
        // Searching in the student list
        private void btn_search_student_Click(object sender, EventArgs e)
        {
            string StudentSearchTerm = tb_search_student.Text;
            // Search by different elements
            if (cb_search_student.Text == "Name")
            {
                var query = database1DataSet.Student.Where(stu => stu.Name.Contains(StudentSearchTerm)); // search in dataset
                studentBindingSource.DataSource = query.ToList();   // show the search result to the list
            }
            else if (cb_search_student.Text == "Telephone")
            {
                var query = database1DataSet.Student.Where(stu => stu.Telephone.Contains(StudentSearchTerm));
                studentBindingSource.DataSource = query.ToList();
            }
            else if (cb_search_student.Text == "Email")
            {
                var query = database1DataSet.Student.Where(stu => stu.Email.Contains(StudentSearchTerm));
                studentBindingSource.DataSource = query.ToList();
            }
            else
            {
                MessageBox.Show("Something wrong, please double check the provided info!");
            }
        }
        // Search for subject in the list
        private void btn_search_subject_Click(object sender, EventArgs e)
        {
            string SubjectSearchTerm = tb_search_subject.Text;
            // Search by different elements
            if (cb_search_subject.Text == "Name")
            {
                var query = database1DataSet.Subject.Where(sub => sub.Name.Contains(SubjectSearchTerm));    // finding in dataset
                subjectBindingSource.DataSource = query.ToList();   // show the search result to the list
            }
            else if (cb_search_subject.Text == "Status")
            {
                var query = database1DataSet.Subject.Where(sub => sub.Status.Contains(SubjectSearchTerm));
                subjectBindingSource.DataSource = query.ToList();
            }
            else
            {
                MessageBox.Show("Something wrong, please double check the provided info!");
            }
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

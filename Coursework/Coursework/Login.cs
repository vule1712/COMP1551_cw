using System;
using System.Windows.Forms;

namespace Coursework
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_access_Click(object sender, EventArgs e)
        {
            string role = cb_role.Text;

            if (role == "Student")
            {
                this.Hide();
                Student_DIS student_dis = new Student_DIS();
                student_dis.ShowDialog();
                this.Close();
            }
            else if (role == "Teacher")
            {
                this.Hide();
                Teacher_DIS teacher_DIS = new Teacher_DIS();
                teacher_DIS.ShowDialog();
                this.Close();
            }
            else
            {
                this.Hide();
                Login_Admin admin_access = new Login_Admin();
                admin_access.ShowDialog();
                this.Close();
            }
        }
    }
}

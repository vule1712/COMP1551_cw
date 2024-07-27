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
    public partial class Login_Admin : Form
    {
        public Login_Admin()
        {
            InitializeComponent();
        }

        // login if username = admin and password = 12345
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            string username = tb_name.Text;
            string password = tb_password.Text;

            if (username == "admin" && password == "12345")
            {
                this.Hide();
                Admin_DIS admin_dis = new Admin_DIS();
                admin_dis.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid admin info");
            }
        }
        // open back the login window, hide the current window
        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }
    }
}

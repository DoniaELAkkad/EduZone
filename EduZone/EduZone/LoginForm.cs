using BusinessModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EduZone
{
    public partial class LoginForm : Form
    {
        LoginManager loginManager = new LoginManager();
        public static DomainModel.BusinessObject.User user;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String username = tbUsername.Text;
            String password = tbPassword.Text;
            user = loginManager.validateUser(username, password);
            if (user!=null)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                lbError.Visible = true;
            }
        }
    }
}

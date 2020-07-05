using CryptoCore.Classes;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace CryptoFront
{
    public partial class LoginForm : XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();

            btnLogin.Focus();
        }

        bool IsLogin = true;

        private void linkSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (btnLogin.Text == "Login")
            {
                // change everthing to sign up
                btnLogin.Text = "Sign up";
                groupLogin.Text = "Sign up";
                linkSignup.Text = "Go back to Login";

                IsLogin = false;

            }
            else
            {
                btnLogin.Text = "Login";
                groupLogin.Text = "Login";
                linkSignup.Text = "No account? Click here to sign up";

                IsLogin = true;
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        { 
            if (IsLogin)
            {
                Login();
            }
            else
            {
                SignUp();
            }
            
        }


        private void Login()
        {
            var html = Globals.HTTPGet(Globals.API_ENDPOINT + string.Format("api/user?action=login&username={0}&password={1}", txtUsername.Text, txtPassword.Text)).Trim();

            if (html == "error")
            {

                XtraMessageBox.Show("Invalid login");
                return;
            }

            if (html == "")
            {
                XtraMessageBox.Show("Couldn't connect to API: " + Globals.API_ENDPOINT);
                return;
            }

            Globals.CurrentUser = JsonConvert.DeserializeObject<UserInfo>(html);

            if (Globals.CurrentUser == null)
            {
                XtraMessageBox.Show("Invalid Login");
                return;
            }

            //Globals.CurrentUser = Globals.LoginUser(txtUsername.Text, txtPassword.Text);


            this.Hide();
            var f = new MainForm();

            f.ShowDialog();

            this.Close();
        }

        private void SignUp()
        {
            var html = Globals.HTTPGet(Globals.API_ENDPOINT + string.Format("api/user?action=signup&username={0}&password={1}", txtUsername.Text, txtPassword.Text)).Trim();


            if (html == "success")
            {
                XtraMessageBox.Show("User created! Please login!");
                linkSignup_LinkClicked(null, null);
            }
            else
            {
                XtraMessageBox.Show("Please try a different username as that one already exists.");
            }
        }

        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(null, null);
            }
        }
    }
}

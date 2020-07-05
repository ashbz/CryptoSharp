using CryptoCore.Classes;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoFront.Forms
{
    public partial class UserSettingsForm : XtraForm
    {
        public UserSettingsForm()
        {
            InitializeComponent();

            if (Globals.CurrentUser == null)
            {
                MessageBox.Show("Invalid user");
                this.Close();
            }

            txtBinanceKey.Text = Globals.CurrentUser.BinanceKey;
            txtBinanceSecret.Text = Globals.CurrentUser.BinanceSecret;
            txtEmail.Text = Globals.CurrentUser.Email;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Globals.CurrentUser.BinanceKey = txtBinanceKey.Text;
            Globals.CurrentUser.BinanceSecret = txtBinanceSecret.Text;
            Globals.CurrentUser.Email = txtEmail.Text;


            Globals.UpdateUser(Globals.CurrentUser);

            this.Close();
        }

    }
}

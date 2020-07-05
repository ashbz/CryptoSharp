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
    public partial class StrategyOptionsForm : XtraForm
    {
        public StrategyOptionsForm()
        {
            InitializeComponent();
            var so = Globals.ActiveStrategyOptions;
            if (so!=null)
            {
                txtBalance.EditValue = so.BacktestStartingBalance;
                txtMinsInbetweenOrders.EditValue = so.MinutesInBetweenOrders;
                txtMaxOpenOrders.EditValue = so.MaxOpenOrders;
                txtFees.EditValue = so.Fees;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var so = new StrategyOptions();
            so.BacktestStartingBalance = Convert.ToDouble(txtBalance.EditValue);
            so.MinutesInBetweenOrders = Convert.ToInt32(txtMinsInbetweenOrders.EditValue);
            so.MaxOpenOrders = Convert.ToInt32(txtMaxOpenOrders.EditValue);
            so.Fees = Convert.ToDouble(txtFees.EditValue);

            Globals.ActiveStrategyOptions = so;

            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptoCore.Classes;

namespace CryptoFront.Forms
{
    public partial class OrderControl : UserControl
    {
        public OrderControl()
        {
            InitializeComponent();
        }


        public void SetOrders(List<OrderInfo> orders)
        {
            treeOrders.ClearNodes();


        }

        private void TreeOrders_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node.Tag == null) return;

            if (e.Column != treeListColumn1) return;

            var txt = e.Node.GetDisplayText(treeListColumn1).Replace("$", "").Replace(" ", "").Trim();
            if (txt == "" || txt == "0") return;


            var num = float.Parse(txt);

            var n = float.Parse(e.Node.GetDisplayText(treeListColumn2));

            if (num > n)
            {
                e.Appearance.ForeColor = Color.LimeGreen;
            }
            else if (num < n)
            {
                e.Appearance.ForeColor = Color.Orange;

            }
        }
    }
}

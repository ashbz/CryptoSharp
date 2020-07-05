using CryptoFront.Classes;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
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
    public partial class IndicatorsForm : XtraForm
    {
        public IndicatorsForm()
        {
            InitializeComponent();

            if (GlobalHelper.ChartIndicators != null)
            {
                foreach (var indicator in GlobalHelper.ChartIndicators)
                {
                    var n = treeIndicators.Nodes.Add(new object[] { indicator.Name + " (" + indicator.Period + ")" });
                    n.Tag = indicator;
                }
            }
        }

        private void BtnAddIndicator_Click(object sender, EventArgs e)
        {
            var f = new IndicatorEditForm(null);
            f.ShowDialog();

            if (f.DialogResult == DialogResult.OK)
            {
                IndicatorInfo indicator = f.CurrentIndicator;

                var n = treeIndicators.Nodes.Add(new object[] { indicator.Name + " (" + indicator.Period + ")" });
                n.Tag = indicator;
            }
        }

        private void TreeIndicators_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null) return;


            var indicator = (IndicatorInfo)e.Node.Tag;

            var rect = new Rectangle();

            var blockSize = e.Bounds.Height / 2;

            rect.X = e.Bounds.Width - 14;
            rect.Y = e.Bounds.Y + blockSize/2;
            rect.Width = blockSize;
            rect.Height = blockSize;

            e.Graphics.FillRectangle(new SolidBrush(indicator.LineColor),rect);
        }

        private void TreeIndicators_DoubleClick(object sender, EventArgs e)
        {
            TreeListHitInfo hi = treeIndicators.CalcHitInfo(treeIndicators.PointToClient(Control.MousePosition));
            if (hi.Node != null)
            {
                IndicatorInfo indicator = (IndicatorInfo)hi.Node.Tag;

                var f = new IndicatorEditForm(indicator);
                f.ShowDialog();

                if (f.DialogResult == DialogResult.OK)
                {
                    indicator = f.CurrentIndicator;

                    hi.Node.SetValue(treeListColumn1, indicator.Name + " (" + indicator.Period + ")");
                    hi.Node.Tag = indicator;

                    treeIndicators.Refresh();
                }
            }
        
        }

        private void BtnRemoveIndicator_Click(object sender, EventArgs e)
        {
            if (treeIndicators.FocusedNode != null)
            {
                treeIndicators.Nodes.Remove(treeIndicators.FocusedNode);
            }
        }

        private void IndicatorsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalHelper.ChartIndicators = new List<IndicatorInfo>();

            treeIndicators.NodesIterator.DoOperation((n) => {
                GlobalHelper.ChartIndicators.Add((IndicatorInfo)n.Tag);
            });
        }
    }
}

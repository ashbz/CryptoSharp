using CryptoCore.Classes;
using CryptoFront.Classes;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid.Rows;
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
    public partial class IndicatorEditForm : XtraForm
    {
        public IndicatorInfo CurrentIndicator = null;


        public IndicatorEditForm(IndicatorInfo indicator)
        {
            InitializeComponent();


            CurrentIndicator = indicator;

            foreach (var item in Globals.GetTechnicalIndicatorList())
            {
                comboIndicator.Properties.Items.Add(item);
            }

            if (indicator != null)
            {
                // means we are editing the indicator
                comboIndicator.Text = indicator.Name;
                comboInputValue.Text = indicator.InputValue;
                chkVisible.Checked = indicator.Visible;

                comboIndicator.Enabled = false;
            }
            else
            {
                comboIndicator.Text = Globals.GetTechnicalIndicatorList()[0];
                comboInputValue.Text = "Close";
                chkVisible.Checked = true;
            }
        }

        private string X_PERIOD = "Period";
        private string X_STDEV = "StDev";
        private string X_LINE_COLOR = "Line Color";
        private string X_FAST_PERIOD = "Fast Period";
        private string X_SLOW_PERIOD = "Slow Period";
        private string X_FAST_COLOR = "Fast Color";
        private string X_SLOW_COLOR = "Slow Color";


        private void InitPropertyList(IndicatorInfo indicator)
        {
            var hasPeriod = false;
            var hasStDev = false;
            var hasLineColor = false;

            var hasFastPeriod = false;
            var hasSlowPeriod = false;

            var hasFastColor = false;
            var hasSlowColor = false;

            var indName = (indicator != null) ? indicator.Name : comboIndicator.Text;

            if (indName == Globals.IND_ParabolicSAR ||
                indName == Globals.IND_CCI ||
                indName == Globals.IND_ATR ||
                indName == Globals.IND_ADX || 
                indName == Globals.IND_WilliamsR)
            {
                comboInputValue.Visible = false;
                lblInputValue.Visible = false;
            }
            else
            {
                comboInputValue.Visible = true;
                lblInputValue.Visible = true;
            }

            if (indName == Globals.IND_SimpleMovingAverage)
            {
                hasPeriod = true;
                hasLineColor = true;
            }
            else if(indName == Globals.IND_ExponentialMovingAverage)
            {
                hasPeriod = true;
                hasLineColor = true;
            }
            else if (indName == Globals.IND_ParabolicSAR)
            {
                hasLineColor = true;
            }
            else if (indName == Globals.IND_BollingerBands)
            {
                hasPeriod = true;
                hasStDev = true;
                hasLineColor = true;
            }
            else if (indName == Globals.IND_MACD)
            {
                hasFastPeriod = true;
                hasSlowPeriod = true;

                hasFastColor = true;
                hasSlowColor = true;
            }
            else if (indName == Globals.IND_RSI)
            {
                hasLineColor = true;
                hasPeriod = true;
            }else if (indName == Globals.IND_CCI || indName == Globals.IND_ATR || indName == Globals.IND_ADX || indName == Globals.IND_WilliamsR)
            {
                hasPeriod = true;
                hasLineColor = true;
            }else if (indName== Globals.IND_SD)
            {
                hasPeriod = true;
                hasStDev = true;
                hasLineColor = true;
            }

            treeProperties.ClearNodes();

            if (indicator == null)
            {
                indicator = new IndicatorInfo();
            }

            if (hasPeriod)
            {
                var pValue = (indicator != null) ? indicator.Period : 14;

                var n = treeProperties.Nodes.Add(new object[] { X_PERIOD, pValue });
            }

            if (hasStDev)
            {
                var pValue = (indicator != null) ? indicator.StdDev : 2.0f;

                var n = treeProperties.Nodes.Add(new object[] { X_STDEV, pValue });
            }

            if (hasLineColor)
            {
                var pValue = (indicator != null) ? indicator.LineColor : Color.LawnGreen;

                var n = treeProperties.Nodes.Add(new object[] { X_LINE_COLOR, pValue });
            }

            if (hasFastPeriod)
            {
                var pValue = (indicator != null) ? indicator.FastPeriod : 12;

                var n = treeProperties.Nodes.Add(new object[] { X_FAST_PERIOD, pValue });
            }

            if (hasSlowPeriod)
            {
                var pValue = (indicator != null) ? indicator.SlowPeriod : 24;

                var n = treeProperties.Nodes.Add(new object[] { X_SLOW_PERIOD, pValue });
            }

            if (hasFastColor)
            {
                var pValue = (indicator != null) ? indicator.FastColor : Color.LawnGreen;

                var n = treeProperties.Nodes.Add(new object[] { X_FAST_COLOR, pValue });
            }

            if (hasSlowColor)
            {
                var pValue = (indicator != null) ? indicator.SlowColor : Color.LawnGreen;

                var n = treeProperties.Nodes.Add(new object[] { X_SLOW_COLOR, pValue });
            }
        }


        private IndicatorInfo GetCurrentIndicator()
        {
            var ind = new IndicatorInfo();
            ind.Name = comboIndicator.Text;
            ind.Visible = chkVisible.Checked;
            ind.InputValue = comboInputValue.Text;


            foreach (TreeListNode n in treeProperties.Nodes)
            {
                var vValue = n.GetValue(treeListColumn2);
                var vName = n.GetDisplayText(treeListColumn1);
                if (vName == X_PERIOD)
                {
                    ind.Period = Convert.ToInt32(vValue);
                }else if (vName == X_STDEV)
                {
                    ind.StdDev = (float)Convert.ToDouble(vValue);
                }else if (vName == X_LINE_COLOR)
                {
                    ind.LineColor = (Color)vValue;
                }
                else if (vName == X_FAST_PERIOD)
                {
                    ind.FastPeriod = Convert.ToInt32(vValue);
                }
                else if (vName == X_SLOW_PERIOD)
                {
                    ind.SlowPeriod = Convert.ToInt32(vValue);
                }
                else if (vName == X_FAST_COLOR)
                {
                    ind.FastColor = (Color)vValue;
                }
                else if (vName == X_SLOW_COLOR)
                {
                    ind.SlowColor = (Color)vValue;
                }
                else
                {

                }

            }

            return ind;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            CurrentIndicator = GetCurrentIndicator();

            this.Close();
        }

        private void ComboIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitPropertyList(CurrentIndicator);
        }

        private void TreeProperties_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column != treeListColumn2) return;

            var vName = e.Node.GetDisplayText(treeListColumn1);

            if (vName == X_PERIOD || vName == X_FAST_PERIOD || vName == X_SLOW_PERIOD)
            {
                e.RepositoryItem = repoInt;
            } else if (vName == X_STDEV)
            {
                e.RepositoryItem = repoFloat;
            } else if (vName == X_LINE_COLOR || vName == X_FAST_COLOR || vName == X_SLOW_COLOR)
            {
                e.RepositoryItem = repoColor;
            }
            else
            {

            }

        }
    }
}

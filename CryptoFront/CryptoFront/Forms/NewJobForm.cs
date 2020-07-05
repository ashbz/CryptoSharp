using CryptoCore.Classes;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
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
    public partial class NewJobForm : XtraForm
    {
        public NewJobForm()
        {
            InitializeComponent();

        }

        private void NewJobForm_Load(object sender, EventArgs e)
        {
            var html = Globals.HTTPGet(Globals.API_ENDPOINT + "api/markets?refresh=false");
            if (Globals.IsValidJson(html))
            {
                var l = JsonConvert.DeserializeObject<List<MarketInfo>>(html);

                foreach (var item in l)
                {
                    comboMarket.Properties.Items.Add(item.Symbol);
                }

            }
            else
            {
                XtraMessageBox.Show("Couldn't get markets! Check API server!");
            }
        }
    }
}

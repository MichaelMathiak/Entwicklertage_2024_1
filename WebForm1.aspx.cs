using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Entwicklertage_2024_1.Models;

namespace Entwicklertage_2024_1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Dictionary<string, string> verbindungsDict { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            DBase dBase = new DBase();
            verbindungsDict = dBase.Haltestellen();
            var distinctDict = verbindungsDict.Values.Distinct();
            if (!IsPostBack)
            {
                DropDownList1.Items.Clear();
                DropDownList2.Items.Clear();
                foreach (var element in distinctDict)
                {
                    DropDownList1.Items.Add(element);
                    DropDownList2.Items.Add(element);
                }
            }
        }

        protected void BtnSuchen_Click(object sender, EventArgs e)
        {
            var dBase = new DBase();
            int z;
            var selectedStart = verbindungsDict.Where(x => x.Value.Equals(DropDownList1.SelectedItem.Text));
            var selectedEnd = verbindungsDict.Where(x => x.Value.Equals(DropDownList2.SelectedItem.Text));
            
            dBase.HandleStartZiel(selectedStart, selectedEnd);

            GridView1.DataSource = DBase.AnzeigeDatan_Transfers;
            GridView1.Rows.clear();
            z = GridView1.Rows.add;
        }
    }
}
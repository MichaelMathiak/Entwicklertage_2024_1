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
            
            if (!IsPostBack)
            {
                verbindungsDict = dBase.Haltestellen();
                DropDownList1.Items.Clear();
                DropDownList2.Items.Clear();
                foreach (var element in verbindungsDict)
                {
                    DropDownList1.Items.Add(element.Value);
                    DropDownList2.Items.Add(element.Value);
                }
            }
        }

        protected void BtnSuchen_Click(object sender, EventArgs e)
        {
            var dBase = new DBase();
            
            
        }
    }
}
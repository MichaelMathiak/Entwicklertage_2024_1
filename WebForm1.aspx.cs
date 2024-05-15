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
        protected void Page_Load(object sender, EventArgs e)
        {
            DBase dBase = new DBase();
            List<string> LHaltestellen = new List<string>();
            if (!IsPostBack)
            {
                LHaltestellen = dBase.Haltestellen();
                DropDownList1.Items.Clear();
                DropDownList2.Items.Clear();
                for (int i = 0; i < LHaltestellen.Count; i++)
                {
                    DropDownList1.Items.Add(LHaltestellen[i]);
                    DropDownList2.Items.Add(LHaltestellen[i]);
                }
                

            }
        }

        protected void BtnSuchen_Click(object sender, EventArgs e)
        {
            if (!(DropDownList1.Text == DropDownList2.Text))
                    {
                //Verbindung suchen und ausgeben
                TextBox1.Text = "Verbindungen!";
            }
            
        }
    }
}
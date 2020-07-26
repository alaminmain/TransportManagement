using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TransportManagerUI.UI
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (Session["RoleId"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }
            else
            {
                string RoleId = (string)Session["RoleId"];
                if (RoleId == "1")
                {
                    divadmin.Visible = true;
                    Admin.Visible = true;
                }
                else
                {
                    divadmin.Visible = false;
                    Admin.Visible = false;
                }
                litMessage.Text = "Welcome back <b>Mr. " + Convert.ToString(Session["userName"]+"</b>");
            }
            
        }

        protected void mnuLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/login.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;
using System.Data;

namespace TransportManagerUI
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try

            {
                UserGateway ug=new UserGateway();
                DataTable dt = new DataTable();
                string userid = txtUsername.Text.Trim();
                string password=txtPassword.Text.Trim();
                if (String.IsNullOrEmpty(userid) || String.IsNullOrEmpty(password))
                {
                    lblMessage.Text = "Please Provied User Name and Password";
                }
                else
                {
                    if (ug.validateUser(userid, password))
                    {
                        dt = ug.GetUserInfo(1, userid);
                        Session["userInfo"] = dt;
                        Session["userName"] = dt.Rows[0][1].ToString();
                        Session["RoleId"] = dt.Rows[0]["Role_Id"].ToString();
                        Session["RoleMenu"] = ug.GetRolesMenuAll(1, Convert.ToInt16(dt.Rows[0]["Role_Id"]));
                        Response.Redirect("~/UI/Default.aspx");
                    }
                    else
                        lblMessage.Text = "Incorrect UserID or Password";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            
        }
    }
}
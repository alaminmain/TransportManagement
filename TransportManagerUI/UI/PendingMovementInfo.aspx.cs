using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagerLibrary.DAL;

namespace TransportManagerUI.UI
{
    public partial class PendingMovementInfo : System.Web.UI.Page
    {
        #region

        private DataTable loadAllMovement(string searchKey)
        {
            try
            {
                DataTable dt;


                using (TransportMasterGateway gatwayObj = new TransportMasterGateway())
                {
                    
                    dt = gatwayObj.GetAllMovementAll(1);
                    if (String.IsNullOrEmpty(searchKey))
                    {

                    }
                    else
                    {
                        var filtered = dt.AsEnumerable()
    .Where(r => r.Field<String>("MovementNo").Contains(searchKey) || r.Field<String>("TripNo").ToUpper().Contains(searchKey.ToUpper()));
                        dt = filtered.CopyToDataTable();

                    }
                    return dt;

                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex.ToString(), new object[0]);
                return null;
            }
        }

        private void isAuthorizeToPage()
        {
            string url = Path.GetFileName(Request.Path);
            DataTable dt = new DataTable();
            if (Session["RoleMenu"] != null)
            {
                dt = (DataTable)(Session["RoleMenu"]);
                var filtered = dt.AsEnumerable().Where(r => r.Field<String>("menuUrl").Contains(url)).ToList();
                if (filtered.Count <= 0)
                {
                    Response.Redirect("~/UI/404.aspx");
                }

            }

        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
            }
            isAuthorizeToPage();

            if ((!IsPostBack) && (!IsCallback))
            {

                DataTable dt = new DataTable();
                dt = loadAllMovement(txtSearch.Text);


                gvlistofBasicData.DataSource = dt;
                gvlistofBasicData.DataBind();
                upListofbasicData.Update();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvlistofBasicData.ClientID + "', 410,1070, 30 ,true); </script>", false);
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllMovement(txtSearch.Text);


            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
        }

        protected void btnMovement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/MovmentInfo.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Default.aspx");
        }
        protected void gvlistofBasicData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllMovement(txtSearch.Text);

            gvlistofBasicData.PageIndex = e.NewPageIndex;
            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = loadAllMovement(txtSearch.Text);


            gvlistofBasicData.DataSource = dt;
            gvlistofBasicData.DataBind();
            upListofbasicData.Update();
        }
    }
}
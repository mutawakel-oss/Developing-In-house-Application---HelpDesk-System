using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class redirect_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            HttpContext.Current.Response.Redirect("frmRequestDetails.aspx?id=" + Request.QueryString["id"].ToString());
                
        }

        if (Request.QueryString["INVPRODUCT"] != null)
        {
            HttpContext.Current.Response.Redirect("frmInvProductDetails.aspx?id=" + Request.QueryString["INVPRODUCT"].ToString());

        }

        if (Request.QueryString["QUICKREQUEST"] != null)
        {
            HttpContext.Current.Response.Redirect("Login.aspx");
        }

        if (Request.QueryString["NEWDEPARTMENT"] != null)
        {
            HttpContext.Current.Response.Redirect("frmAssignDeptSecretary.aspx");
        }
        if (Request.QueryString["NEWUSERREQUESTID"] != null)
        {
            HttpContext.Current.Response.Redirect("frmNewUserAccount.aspx?NEWUSERREQUESTID=" + Request.QueryString["NEWUSERREQUESTID"].ToString() + "&FN=" + Request.QueryString["FN"].ToString());
        }

    }
}

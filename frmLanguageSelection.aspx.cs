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

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
        
       // Response.Write(Request.QueryString["error"].ToString());
        LoggedAs();
    }

    protected void LoggedAs()
    {
        /// <summary>
        /// 	Description: show the user group of the logged in user
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

        //if (null != Session["UserFullName"] && Session["UserFullName"].ToString() != "")
        //    lblLogUser.Text = Session["UserFullName"].ToString();
        //if (null != Session["Badge"] && Session["Badge"].ToString() != "")
        //    lblBadgeNo.Text = Session["Badge"].ToString();

        Label LB = (Label)this.Master.Page.Controls[0].Controls[3].Controls[9].Controls[1];
        HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].Controls[5];

       
    }
  
   
    protected void setCmbLanguage(object sender, EventArgs e)
    {
        try
        {
            Session["Language"] = ddSelLanguage.SelectedValue.ToString();
            Response.Redirect("./Login.aspx");
        }
        catch (Exception exp)
        {
        }
    }
}

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
using System.Net.Mail;
using System.DirectoryServices;
using System.Data.Odbc;
using System.Text;
using System.Drawing;

using System.Net;
using System.IO;
using System.Threading;



public partial class frmNewUserAccount : System.Web.UI.Page
{
    OdbcDataReader reader;
    bool bEmail = false;
    bool bMcurriculum = false;
    bool bMMeducation = false;
    bool bLibray = false;
    string strBadgeNo = "";
    string strDfileName = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Label LB = (Label)this.Master.Page.Controls[0].Controls[3].Controls[9].Controls[1];
            HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].Controls[5];
            Hlk.NavigateUrl = "frmUserRequestList.aspx";
            LB.Text = "User";
            if (Request["message"] != null)
            {
                txtMessage.Text = Request["message"].ToString();
            }
        }
        catch (Exception exp)
        {
        }
        
    }
   

    protected void InitializeCulture()
    {
        //Response.Write(Request.QueryString["Language"].ToString());
        //string culture = Request.QueryString["Language"].ToString();
        string culture = "en-US";
        if (string.IsNullOrEmpty(culture))
            culture = "Auto";
        UICulture = culture;
        this.Culture = culture;
        if (culture != "Auto")
        {
            System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
            System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;

            base.InitializeCulture();
        }

    }
    
}

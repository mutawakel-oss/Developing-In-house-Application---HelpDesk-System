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
    protected void mSubmitComplain(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to submit the user complain
        /// Author: mutawakelm
        /// Date :3/15/2009 1:06:21 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            GeneralClass.Program.Add("user_id", Session["UserID"].ToString(), "S");
            GeneralClass.Program.Add("subject", txtSubject.Text, "S");
            GeneralClass.Program.Add("description", txtDescription.Text, "S");
            if(rdCategory.SelectedItem.Value=="1")
            GeneralClass.Program.Add("category","Comment" , "S");
            else
                if(rdCategory.SelectedItem.Value=="2")
                    GeneralClass.Program.Add("category", "Suggestion", "S");
                else
                    if (rdCategory.SelectedItem.Value == "3")
                        GeneralClass.Program.Add("category", "Complain", "S");
            int returnId=GeneralClass.Program.InsertRecordStatement("t_user_complain");
            if (returnId != 0)
                Response.Redirect("~/frmMessage.aspx?message=Your complain has been sent successfully");

        }
        catch (Exception exp)
        {
        }
    }
}

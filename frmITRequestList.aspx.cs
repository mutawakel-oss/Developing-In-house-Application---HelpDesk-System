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

public partial class frmUserRequestList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HyperLink LB1 = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired");
        }

        LB1.Text = "Log Out";
        if (Session["Language"] != null)
            this.languageLabel.Text = Session["Language"].ToString();
        DisplayRequestList();
        DisplayOptions();
        LoggedAs();
    }

    protected void DisplayRequestList()
    {
        /// <summary>
        /// 	Description: Populate requests from t_requests into gridview  
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: Requestlist in the gridview
        /// 	Example:  DisplayRequestList();
        /// </summary>
        try
        {
            string userid = "";
            if (null != Session["UserID"])           
                userid = Session["UserID"].ToString();
           

            DataSet ds;
            string strRequestsQuery ="SELECT vw_user_requests.*,'assigned_to' = CASE WHEN vw_assignment.assigned_to IS NULL then 'None' ELSE vw_assignment.full_name END FROM vw_user_requests LEFT OUTER JOIN vw_assignment ON vw_user_requests.id = vw_assignment.request_id WHERE created_by ='" + userid + "' ORDER BY vw_user_requests.id DESC";
            ds = GeneralClass.Program.gDataSetGridView(strRequestsQuery, "t_requests" );

            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            gdvRequestList.DataSource = ds;
            gdvRequestList.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }

    protected void gdvRequestList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        /// <summary>
        /// 	Description: handles paging event gridview navigate into next page
        ///	
        ///
        /// 	Date:15/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: navigation of userlist in the gridview
        /// 	Example:
        /// </summary>

        try
        {
            string userid = "";
           if(null!=Session["UserID"] && Session["UserID"].ToString()!="")
             userid = Session["UserID"].ToString();

            DataSet ds;
            ds = GeneralClass.Program.gDataSetGridView("SELECT vw_user_requests.*,'assigned_to' = CASE WHEN vw_assignment.assigned_to IS NULL then 'None' ELSE vw_assignment.full_name END FROM vw_user_requests LEFT OUTER JOIN vw_assignment ON vw_user_requests.id = vw_assignment.request_id WHERE created_by ='" + userid + "' ORDER BY vw_user_requests.id DESC", "t_requests");
            gdvRequestList.DataSource = ds;
            gdvRequestList.PageIndex = e.NewPageIndex;
            gdvRequestList.DataBind();

        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }


    protected void DisplayOptions()
    {
        /// <summary>
        /// 	Description: Display controls based on user rights
        ///	
        ///
        /// 	Date:22/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>

        if ((null != Session["Admin"] && Session["Admin"].ToString() == "true") || (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString() == "true"))
        {
            //if (Session["Admin"].ToString() == "true")
            //{

            hlkUserList.Enabled = true;
            hlkUserList.Visible = true;

                hlkAdminRequestList.Enabled = true;
                hlkAdminRequestList.Visible = true;

            //}
        }
        else
        {
            if (null != Session["Technician"])
            {
                if (Session["Technician"].ToString() == "true")
                {

                }
            }
            else
            { 
            
            }

        }

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

        if (null != Session["UserFullName"] && Session["UserFullName"].ToString() != "")
            lblLogUser.Text = Session["UserFullName"].ToString();

        if (null != Session["Badge"] && Session["Badge"].ToString() != "")
            lblBadgeNo.Text = Session["Badge"].ToString();

        if (null != Session["Title"] && Session["Title"].ToString() != "")
            lblTitle.Text = Session["Title"].ToString();
        if (null != Session["Department"] && Session["Department"].ToString() != "")
            lblDepartment.Text = Session["Department"].ToString(); 

        Label LB = (Label)this.Master.Page.Controls[0].Controls[3].Controls[9].Controls[1];
        HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].Controls[5];

        if (null != Session["Admin"] && Session["Admin"].ToString() == "true")
        {
            LB.Text = "Administrator";
            Hlk.NavigateUrl = "frmAdminDefault.aspx";
        }
        else
        {
            if (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString() == "true")
            {
                LB.Text = "Local Admin";
                Hlk.NavigateUrl = "frmAdminDefault.aspx";
            }
            else
                if (null != Session["Technician"] && Session["Technician"].ToString() == "true")
                {
                    LB.Text = "PC Technician";
                    //Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
                    Hlk.NavigateUrl = "frmTechnicianDefault.aspx";
                }
                else
                {
                    LB.Text = "User";
                    Hlk.NavigateUrl = "frmUserRequestList.aspx";
                }
        }
    }

    protected void EditLogUserInfo(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Navigate to the Editor page of user info edit
        ///	
        ///
        /// 	Date:28/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>    


        if (null != Session["UserID"] && Session["UserID"].ToString() != "")
        {
            Session["Language"] = this.languageLabel.Text.ToString();
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmUserRequestList.aspx");
        }
    }
    protected void InitializeCulture()
    {
        
        if (Session["Language"] != null)
        {
            
            string culture = Session["Language"].ToString();
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
    protected void NewRequest(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to assign the language of the session of new request page
        /// Author: mutawakelm
        /// Date :02/05/2008 09:29:35
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            //Response.Write(this.languageLabel.Text.ToString());
            Session["Language"] = this.languageLabel.Text.ToString();
            Response.Redirect("./frmNewRequest.aspx");


        }
        catch (Exception NewRequest_Exp)
        {

        }
    }
    protected void DisplayTaskList(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :9/22/2007 7:46:19 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (null != Session["UserID"])

                HttpContext.Current.Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
        }
        catch (Exception DisplayTaskList_Exp)
        {

        }

    }
    protected void DisplayOwnRequest(object sender, EventArgs e)
    {
        try
        {
            Session["Language"] = "en-US";
            Response.Redirect("frmITRequestList.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void DisplayHomePage(object sender, EventArgs e)
    {
        try
        {
            Session["Language"] = "en-US";
            Response.Redirect("./frmItDefault.aspx");
        }
        catch (Exception exp)
        {
        }
    }
}

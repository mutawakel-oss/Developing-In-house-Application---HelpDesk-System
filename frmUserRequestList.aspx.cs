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
using System.DirectoryServices;

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
        if(!IsPostBack)
            mCheckProfile();
        DisplayRequestList();
        DisplayOptions();
        LoggedAs();
        
    }
    protected void mCheckProfile()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to check the profile update for the logged user
        /// Author: mutawakelm
        /// Date :3/8/2009 10:38:21 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (null != Session["UserID"] && Session["UserID"].ToString() != "")

                DisplayUserInfo(Session["UserID"].ToString(), Session["UserFullName"].ToString(), GeneralClass.Program.SKODE);
        }
        catch (Exception exp)
        {
        }
    }
    protected void DisplayUserInfo(string logID, string username, string pwd)
    {
        try
        {
            DirectoryEntry entry = new DirectoryEntry(GeneralClass.Program.strLOGINUSERPATH, logID, pwd);
            Object obj = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + logID + ")";
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();
            if (null == result)
            {
                // return string.Empty;
            }
            else
            {
                if ((null == result.GetDirectoryEntry().Properties["telephonenumber"].Value) && (null == result.GetDirectoryEntry().Properties["pager"].Value))
                    updatingProfileExtender.Show();
            }
        }
        catch (Exception exp)
        {
            // return string.Empty;
        }
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
            string strRequestsQuery = "SELECT vw_end_user_requests.*,'assigned_to' = CASE WHEN vw_assignment.assigned_to IS NULL then 'None' ELSE vw_assignment.full_name END FROM vw_end_user_requests LEFT OUTER JOIN vw_assignment ON vw_end_user_requests.id = vw_assignment.request_id WHERE created_by ='" + userid + "' ORDER BY vw_end_user_requests.id DESC";
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
                    if (null != Session["ITMember"] && Session["ITMember"].ToString() == "true")
                    {
                        LB.Text = "IT Member";
                        //Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
                        Hlk.NavigateUrl = "frmItDefault.aspx";
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
    protected void btnUpdateProfileClicekd(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to update the profile of the user
        /// Author: mutawakelm
        /// Date :3/8/2009 10:46:55 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            UpdateLdapUserDetails(Session["UserID"].ToString(), Session["UserID"].ToString(), GeneralClass.Program.SKODE);    
        }
        catch (Exception exp)
        {
        }
    }
    protected void UpdateLdapUserDetails(string loginID, string AccountUser, string pwd)
    {
        /// <summary>
        /// 	Description: Update the Ldap user details  
        ///	
        ///
        /// 	Date:26/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example: 
        /// </summary>

        try
        {
            string strPager = " ";//This variable will hold the employee pager
            string strExtensionNo = " ";//This variable will hold the employee extension number
            DirectoryEntry de = GetUser(loginID, AccountUser, pwd);
            if (de != null)
            {
                if (de.Properties["displayName"] != null && de.Properties["displayName"].Value != null)
                {

                    if(txtPhone.Text !="")
                        de.Properties["telephonenumber"].Value = txtPhone.Text;
                    else
                        de.Properties["telephonenumber"].Value = null;
                    if (txtMobile.Text != "")
                        de.Properties["pager"].Value = txtMobile.Text;
                    else
                        de.Properties["pager"].Value = null;
                    de.CommitChanges();
                    


                }
            }
            
        }
        catch (Exception ex)
        {
            // Response.Redirect("error.aspx?error=" + ex.Message);
            Response.Redirect("login.aspx");
        }
    }
    private DirectoryEntry GetUser(string UserName, string accountName, string pwd)
    {
        DirectoryEntry de = GetDirectoryObject(accountName, pwd);
        DirectorySearcher deSearch = new DirectorySearcher();
        deSearch.SearchRoot = de;

        deSearch.Filter = "(&(objectClass=user)(SAMAccountName=" + UserName + "))";
        deSearch.SearchScope = SearchScope.Subtree;
        SearchResult results = deSearch.FindOne();

        if (!(results == null))
        {
            de = new DirectoryEntry(results.Path, accountName, pwd, AuthenticationTypes.Secure);
            return de;
        }
        else
        {
            return null;
        }
    }
    private DirectoryEntry GetDirectoryObject(string accountName, string pwd)
    {
        DirectoryEntry oDE;
        //oDE = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", accountName, pwd, AuthenticationTypes.Secure);
        oDE = new DirectoryEntry(GeneralClass.Program.strLOGINUSERPATH, accountName, pwd, AuthenticationTypes.Secure);

        return oDE;
    }
    protected void mOpenComplainPage(object sender, EventArgs e)
    {
        try
        {
           
            Response.Redirect("~/frmUserComplain.aspx");
        }
        catch (Exception exp)
        {
        }
    }
}

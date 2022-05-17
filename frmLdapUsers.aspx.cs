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

public partial class frmLdapUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HyperLink LB1 = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired");
        }
        LB1.Text = "Log Out";
        if (!IsPostBack)
        {
            pullusers("", "wstaff", "Ldap@KSAU!23");
        }

        DisplayUserList();
        LoggedAs();
    }
    
    private void pullusers(string domain, string username, string pwd)
    {
        /// <summary>
        /// 	Description: Import users from the LDAP
        ///	
        ///
        /// 	Date:26/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  pullusers("", txtusername.Text, txtpwd.Text)
        /// </summary>

        try
        {
            GeneralClass.Program.gRetrieveRecord("delete from t_ldapUsers");
            
            ///DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis

            DirectoryEntry entry1 = new DirectoryEntry("LDAP://DC=med,DC=ksuhs,DC=edu,DC=sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis

            DirectorySearcher mySearcher = new DirectorySearcher(entry1);
            SearchResultCollection results;
            results = mySearcher.FindAll();

            string strFullName;
            string strEMail;
            string strBadgeNo;
            string strTitle;
            string strDepartment;
            string strPager;
            string strTele;
            string strMobile;
            string strLoginName;


            DirectorySearcher dSearch = new DirectorySearcher(entry1);
            dSearch.Filter = "(&(objectCategory=user)(cn=*))";
            

            foreach (SearchResult sResultSet in dSearch.FindAll())
            {
                    strFullName = GetProperty(sResultSet, "Name");
                    strBadgeNo = GetProperty(sResultSet, "employeeid");
                    strEMail = GetProperty(sResultSet, "mail");
                    strTele = GetProperty(sResultSet, "telephonenumber");
                    strDepartment = GetProperty(sResultSet, "department");
                    strTitle = GetProperty(sResultSet, "title");
                    strLoginName = GetProperty(sResultSet, "sAMAccountName");
                  
                    if (GetProperty(sResultSet, "sAMAccountName").Trim() == "wtest")
                    {
                        string str_qwe = "";
                    }

                    strPager = GetProperty(sResultSet, "pager");

                    if ("" == strBadgeNo)
                        strBadgeNo = "0";

                    if ("" == strTele)
                        strTele = "0";

                    strMobile = GetProperty(sResultSet, "mobile");

                    if ("" == strMobile)
                        strMobile = "0";

                if(!Convert.ToBoolean(Convert.ToInt32(GetProperty(sResultSet,"userAccountControl"))&0x0002))   // CHECKING FOR THE ENABLED USER
                if ("" != strLoginName.Trim())
                        if (strFullName != string.Empty)
                        {
                            GeneralClass.Program.Add("full_name", strFullName.Trim(), "S");
                            GeneralClass.Program.Add("badge_number", strBadgeNo, "I");
                            GeneralClass.Program.Add("email_address", strEMail, "S");
                            GeneralClass.Program.Add("phone_ext", strTele, "I");
                            GeneralClass.Program.Add("mobile", strMobile, "S");
                            GeneralClass.Program.Add("department_name", strDepartment, "S");
                            GeneralClass.Program.Add("job_title", strTitle, "S");
                            GeneralClass.Program.Add("login_name", strLoginName, "S");

                            int intReturnID = GeneralClass.Program.InsertRecordStatement("t_LdapUsers");
                        }
                }
          

        }
        catch (Exception ex)
        {

        }
    }

    public static string GetProperty(SearchResult searchResult, string PropertyName)
    {
        /// <summary>
        /// 	Description: Returns property for the LDAP data fetching
        ///	
        ///
        /// 	Date:26/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  GetProperty(sResultSet, "sAMAccountName");
        /// </summary>
        //HttpContext.Current.Response.Write(searchResult.Path);
        try
        {
            if (searchResult.Properties.Contains(PropertyName))
                return searchResult.Properties[PropertyName][0].ToString();
            else
                return string.Empty;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    protected void DisplayUserList()
    {
        /// <summary>
        /// 	Description: Populate data from t_users into gridview  
        ///	
        ///
        /// 	Date:26/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: userlist in the gridview
        /// 	Example:  DisplayUserList();
        /// </summary>
        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSet("t_LdapUsers", "*", " ORDER BY full_name ASC");
            gdvUserList.DataSource = ds;
            gdvUserList.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }

    protected void gdvUserList_PageIndexChanging(object sender,GridViewPageEventArgs  e)
    {
        /// <summary>
        /// 	Description: Populate data from t_users into gridview  
        ///	
        ///
        /// 	Date:26/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: userlist in the gridview
        /// 	Example:  DisplayUserList();
        /// </summary>
        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSet("t_LdapUsers", "*", " ORDER BY full_name ASC");
            gdvUserList.DataSource = ds;
            gdvUserList.PageIndex = e.NewPageIndex;
            gdvUserList.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmLdapUsers.aspx");
    }
    protected void NewRecepient(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Navigate to Newrecepient page 
        ///	
        ///
        /// 	Date:12th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        HttpContext.Current.Response.Redirect("frmInvRecepient.aspx?called=frmLdapUsers.aspx");
    }
    protected void AddNewSecretary(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Redirect to new AddSecretary page
        ///	
        ///
        /// 	Date:11th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>                       

        Response.Redirect("frmAssignDeptSecretary.aspx?secretary=frmLdapUsers.aspx");
    }

    protected void AddNewPCTech(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Redirect to new AddSecretary page
        ///	
        ///
        /// 	Date:11th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>                       

        Response.Redirect("frmNewPcTech.aspx?called=frmLdapUsers.aspx");
    }
}

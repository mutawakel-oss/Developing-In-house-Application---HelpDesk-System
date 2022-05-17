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

public partial class frmEditLogUserInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired");
        }
        
        
        string strid = Request.QueryString["logid"].ToString();
        string strback = Request.QueryString[1].ToString();

        if (!IsPostBack)
        {
            if (null != Session["UserID"] && Session["UserID"].ToString() != "")

                DisplayUserInfo(Session["UserID"].ToString(), Session["UserFullName"].ToString(), GeneralClass.Program.SKODE);
        }
        LoggedAs();    

    }

    protected void BackToHome(object sender, EventArgs e)
    {
        // <summary>
        /// 	Description: navigate to the called page
        ///	
        ///
        /// 	Date:28/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        Response.Redirect(Request.QueryString[1].ToString());    
    }

    protected void SaveDetails(object sender, EventArgs e)
    {
        // <summary>
        /// 	Description: Save edited user details
        ///	
        ///
        /// 	Date:28/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        if (null == Session["LocalAdmin"])

            UpdateLdapUserDetails(Session["UserID"].ToString(), Session["UserID"].ToString(), GeneralClass.Program.SKODE);    
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
            DirectoryEntry de = GetUser(loginID, AccountUser, pwd);
            if (de != null)
            {
                if (de.Properties["displayName"] != null && de.Properties["displayName"].Value != null)
                {

                    //de.Properties["employeeid"].Value = txtEmployeeID.Text;
                    if(txtPhone.Text !="")
                    de.Properties["telephonenumber"].Value = txtPhone.Text;
                    else
                    de.Properties["telephonenumber"].Value = null;
                    if (txtMobile.Text != "")
                    de.Properties["pager"].Value = txtMobile.Text;
                    else
                    de.Properties["pager"].Value = null;
                    de.CommitChanges();
                    Session["Badge"] = txtEmployeeID.Text;
                    Session["Extn"] = txtPhone.Text;
                    Session["Mobile"] = txtMobile.Text;


                }
            }
            Response.Redirect("login.aspx");
        }
        catch (Exception ex)
        {
           // Response.Redirect("error.aspx?error=" + ex.Message);
            Response.Redirect("login.aspx");
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
                // Update the new path to the user in the directory
                //_path = result.Path;
                //_filterAttribute = (string)result.Properties["cn"][0];
                
              txtFullname.Text= result.GetDirectoryEntry().Properties["Name"].Value.ToString();
              txtEmail.Text = result.GetDirectoryEntry().Properties["mail"].Value.ToString();
              if(null!=result.GetDirectoryEntry().Properties["employeeid"].Value)
                  txtEmployeeID.Text = result.GetDirectoryEntry().Properties["employeeid"].Value.ToString();                            
              if(null!=result.GetDirectoryEntry().Properties["telephonenumber"].Value)
                txtPhone.Text = result.GetDirectoryEntry().Properties["telephonenumber"].Value.ToString();
              if(null!=result.GetDirectoryEntry().Properties["pager"].Value)
              txtMobile.Text = result.GetDirectoryEntry().Properties["pager"].Value.ToString();
          if (null != result.GetDirectoryEntry().Properties["Description"].Value)
             txtTitle.Text = result.GetDirectoryEntry().Properties["Description"].Value.ToString();
         if (null != result.GetDirectoryEntry().Properties["Department"].Value)
            txtDepartment.Text = result.GetDirectoryEntry().Properties["Department"].Value.ToString();     

          txtLogin.Text = logID;  
                //return (string)result.Properties["cn"][0].ToString();
            }
        }
        catch (Exception exp)
        {
           // return string.Empty;
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

}

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
using System.Data.Odbc;
using GeneralClass;
using System.DirectoryServices;


public partial class frmUserDetails : System.Web.UI.Page
{
    OdbcDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired");
        }
        if (!IsPostBack)
        {
            LoadGroupItems();
            LoadUserlist();
            if (Request.QueryString["logid"].ToString() != "")
                IsInvRecepient(Request.QueryString["logid"].ToString());
        }
  
        LoggedAs();

    }

    protected void SaveUserDetails(object sender, EventArgs e)
    {
        
        /// <summary>
        /// 	Description: Save user details into t_users table
        ///	
        ///
        /// 	Date:15/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:user details specified in the textbox
        ///		output:
        /// 	Example:  SaveUserDetails()
        /// </summary>

        int intReturnID=0;
        
        //try
        //{
            //GeneralClass.Program.Add("full_name", txtFullname.Text.Trim(), "S");
            //GeneralClass.Program.Add("phone_ext", txtPhone.Text, "I");
            //GeneralClass.Program.Add("mobile", txtMobile.Text.Trim(), "I");
            //GeneralClass.Program.Add("department_name", txtDepartment.Text.Trim(), "S");
            //GeneralClass.Program.Add("login_name", txtLogin.Text.Trim(), "S");
            //GeneralClass.Program.Add("user_group", GroupString(), "S");

            //int intReturnID = GeneralClass.Program.UpdateRecordStatement("t_users", "id", Request.QueryString["id"].ToString());

        // HAVE TO PUT CODE UPDATE USER DETAILS IN LDAP  **************

       // UpdateLdapUserDetails("wtest","wstaff","test123");


        if (!UserExists(txtLogin.Text.Trim()))
        {
            GeneralClass.Program.Add("id", txtLogin.Text.Trim(), "S");
            GeneralClass.Program.Add("full_name", txtFullname.Text.Trim(), "S");
            GeneralClass.Program.Add("user_group", GroupString(), "S");

            // intReturnID = GeneralClass.Program.InsertRecordStatement("t_userRights");
            intReturnID = GeneralClass.Program.InsertRecordStatement("t_users");
        }
        else
        {
         
            GeneralClass.Program.Add("user_group", GroupString(), "S");
            intReturnID = GeneralClass.Program.UpdateRecordStatement("t_users", "id", "'" + txtLogin.Text.Trim() + "'");
           
            
        }

        UpdtInvRecepient(txtLogin.Text.Trim());  // to save inventory recepient
        
        
        if(intReturnID > 0)
                Response.Redirect("frmLdapUsers.aspx");
            else
                Response.Redirect("error.aspx?error=There is ");



        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}
    }
    
    protected bool UserExists(string loginID)
    {
        /// <summary>
        /// 	Description: Checking for the existence of the user
        ///	
        ///
        /// 	Date:26/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        bool blnExits = false;

        try
        {
            //reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(id) FROM t_userrights WHERE id='" + loginID + "'");
            reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(id) FROM t_users WHERE id='" + loginID + "'");

            while (reader.Read())
            {
                if (Convert.ToInt32(reader[0]) != 0)
                    blnExits = true;
            }
            reader.Close();
            return blnExits;

        }
        catch (Exception ex)
        {

            return blnExits;
        }
    }

    protected void LoadUserlist()
    {
        /// <summary>
        /// 	Description: Populate data from the t_users table 
        ///	
        ///
        /// 	Date:15/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: userdetails will be assigned to textboxes
        /// 	Example:  LoadUserlist();
        /// </summary>
        try
        {
            //reader = GeneralClass.Program.gRetrieveRecord("SELECT * FROM t_users WHERE id=" + Request.QueryString["id"].ToString());
            reader = GeneralClass.Program.gRetrieveRecord("SELECT * FROM t_ldapusers WHERE login_name='" + Request.QueryString["logid"].ToString()+ "'");
           
            while (reader.Read())
            {
                lblLoginName.Text = reader["login_name"].ToString();
                txtFullname.Text = reader["full_name"].ToString();
                txtEmployeeID.Text = reader["badge_number"].ToString();
                txtEmail.Text = reader["email_address"].ToString();
                txtPhone.Text = reader["phone_ext"].ToString();
                txtMobile.Text = reader["mobile"].ToString();
                txtDepartment.Text = reader["department_name"].ToString();
                txtTitle.Text = reader["job_title"].ToString();
                txtLogin.Text = reader["login_name"].ToString();
                //GroupDetails(reader["user_group"].ToString());               
            }
            reader.Close();
            GroupDetails();
        }
        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
        }

    }


    protected void GroupDetails()
    {
        /// <summary>
        /// 	Description: Check mark  Group details check box list based input string 
        ///	
        ///
        /// 	Date:15/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: comma seperated string
        ///		output: appropriate check box will be selected based on the group string
        /// 	Example:  GroupDetails("1,2,3");
        ///     Modified on: 26/Aug/2007
        /// </summary>           

        string groups = "";

        try
        {
            //reader = GeneralClass.Program.gRetrieveRecord("SELECT user_group FROM t_userrights WHERE id='" + Request.QueryString["logid"].ToString() + "'");
            reader = GeneralClass.Program.gRetrieveRecord("SELECT user_group FROM t_users WHERE id='" + Request.QueryString["logid"].ToString() + "'");
           
            while(reader.Read())
            {
                groups = reader["user_group"].ToString();
            }
            reader.Close();

            string[] grps = groups.Split(',');

            for (int i = 0; i <= grps.Length - 1; i++)
            {
                if (grps[i] == "1")
                {
                    cblGroups.Items[0].Selected = true;
                }

                if (grps[i] == "2")
                {
                    cblGroups.Items[1].Selected = true;
                }

                //if (grps[i] == "3")
                //{
                //    cblGroups.Items[2].Selected = true;
                //}
            }
        }
        catch (Exception ex)
        {
            reader.Close();
        }
    }

    protected void LoadGroupItems()
    {
        /// <summary>
        /// 	Description: Load items from the usergroup table 
        ///	
        ///
        /// 	Date:15/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: comma seperated string
        /// 	Example:  LoadGroupItems();
        /// </summary>

        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSet("usergroup", "*", "");

            cblGroups.DataTextField = "GroupName";
            cblGroups.DataValueField = "GroupID";
            cblGroups.DataSource = ds;
            cblGroups.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }


    protected string GroupString()
    {
        /// <summary>
        /// 	Description: forms a comma seperated string base on the group items selected 
        ///	
        ///
        /// 	Date:15/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:checkboxlist selected item
        ///		output: comma seperated string
        /// 	Example:  GroupString();
        /// </summary>

        string grps = "";
        for (int i = 0; i <= cblGroups.Items.Count - 1; i++)
        {
            if (cblGroups.Items[i].Selected)
            {
                if (grps != "")
                {
                    grps += ",";
                }

                grps += cblGroups.Items[i].Value.ToString();
            }
        }
        return grps;
    }

    protected void SaveNewPassword(object sender, EventArgs e)
    {

        //pnl_reset_password.
        //    modal.Dispose();

    }


    protected void BackToList(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save user details into t_users table
        ///	
        ///
        /// 	Date:15/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:user details specified in the textbox
        ///		output:
        /// 	Example:  SaveUserDetails()
        /// </summary>
       
            Response.Redirect("frmLdapUsers.aspx");               

    }

    //protected string UpdateLdapUserDetails(string usrID, string username, string pwd)
    //{
    //    /// <summary>
    //    /// 	Description: Update the Ldap user details  
    //    ///	
    //    ///
    //    /// 	Date:26/Aug/2007
    //    /// 	Author:Oliver
    //    /// 	Parameter:
    //    ///		input: 
    //    ///		output: 
    //    /// 	Example: 
    //    /// </summary>

    //    try
    //    {
    //        string domainAndUsername = username.Trim();
    //        DirectoryEntry entry = new DirectoryEntry("LDAP://med.ksuhs.edu.sa","wtest", "test123");
    //        entry.AuthenticationType = AuthenticationTypes.Secure;
            
    //        Object obj = entry.NativeObject;
    //        DirectorySearcher search = new DirectorySearcher(entry);
    //        search.Filter = "(SAMAccountName=" + usrID + ")";


    //        string test= (string)entry.Properties["phone_ext"].Value;
            
    //        search.PropertiesToLoad.Add("cn");
    //        SearchResult result = search.FindOne();
    //        if (null == result)
    //        {
    //            return string.Empty;
    //        }
    //        else
    //        {
    //            // Update the new path to the user in the directory
    //            //_path = result.Path;
    //            //_filterAttribute = (string)result.Properties["cn"][0];

    //            //myDirectoryEntry.Properties["mail"].Value = sa[3];
    //            //myDirectoryEntry.CommitChanges(); 
    //            //result.Properties["mobile"].
    //            entry.Properties["mobile"].Value = "61128";
    //            entry.CommitChanges();
                
    //           return (string)result.Properties["cn"][0].ToString();
    //        }
    //    }
    //    catch (Exception exp)
    //    {
    //        return string.Empty;
    //    }

    //}

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
        oDE = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", accountName, pwd, AuthenticationTypes.Secure);
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


        DirectoryEntry de = GetUser(loginID,AccountUser,pwd);
        if (de != null)
        {
            if (de.Properties["displayName"] != null && de.Properties["displayName"].Value != null)
            {

                de.Properties["employeeid"].Value = txtEmployeeID.Text;
                de.Properties["telephonenumber"].Value= txtPhone.Text;
                de.Properties["mobile"].Value=txtMobile.Text ;
                
                
               // de.Properties["title"].Value=txtTitle.Text ;   ///  Check this property string
              // de.Properties["department"].Value=txtDepartment.Text;  // CHECK THIS PROPERTY NAME 
                

                de.CommitChanges();
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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmUserDetails.aspx");
    }

    protected bool IsInvRecepient(string loginID)
    {
        /// <summary>
        /// 	Description: Checking for current user is inventory recepient or not.
        ///	
        ///
        /// 	Date:05/Dec/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        bool blnExits = false;

        try
        {
            //reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(id) FROM t_userrights WHERE id='" + loginID + "'");
            reader = GeneralClass.Program.gRetrieveRecord("SELECT * FROM t_invrecepient WHERE id='" + loginID + "'");

            while (reader.Read())
            {
                if (reader[0].ToString() != "" & reader[0] != null)
                {
                    blnExits = true;
                    chbInvRecepient.Checked = true;
                }
            }
            reader.Close();
            return blnExits;

        }
        catch (Exception ex)
        {

            return blnExits;
        }
    }

    protected void UpdtInvRecepient(string userID)
    {
        // <summary>
        /// 	Description: updating t_invrecepient table based on the selected inv check box.
        ///	
        ///
        /// 	Date:05/Dec/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        try
        {

            if (chbInvRecepient.Checked)
            {
                if (!IsInvRecepient(userID))
                {
                    GeneralClass.Program.Add("id", txtLogin.Text.Trim(), "S");
                    GeneralClass.Program.Add("full_name", txtFullname.Text.Trim(), "S");
                    int intReturnID = GeneralClass.Program.InsertRecordStatement("t_invrecepient");           
                }            
            }
            if (!chbInvRecepient.Checked)
            { 
                if (IsInvRecepient(userID))
                    GeneralClass.Program.gRetrieveRecord("DELETE FROM t_invrecepient WHERE id='" + userID + "'");
             }
        }
        catch (Exception ex)
        {
                        
        }
    }

    
}

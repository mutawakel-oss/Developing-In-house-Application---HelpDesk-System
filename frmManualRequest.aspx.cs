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
using GeneralClass;
using System.DirectoryServices;
using System.IO;
using System.Text;
public partial class frmNewRequest : System.Web.UI.Page
{
  System.Data.Odbc.OdbcDataReader reader;
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
            //LoadAllocatedItems(); // load allocated items to the logged user      

            LoadRequestForUsers();
            mFillTechnicianDDL();
            LoadCategory();
              

        }

        DisplayOptions();
        //LdapUserInfo("wtest", "wstaff", "test123");

        LoggedAs();

    }
    protected void SaveNewRequest(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save new request
        /// 1-check pc technician availability.
        /// 2-check token.
        /// 3-if pc technician available then assign the task.
        /// 4-if not then insert the request into the waiting list table "t_request_queue".
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:mutawakel
        /// 	Parameter:
        ///		input:user details specified in the textbox
        ///		output:
        /// 	Example:  SaveUserDetails()
        /// </summary>

        try{
            //The simple code
            GeneralClass.Program.Add("priority", ddlPriority.SelectedItem.Text, "S");
            GeneralClass.Program.Add("Status_id", "5", "I");
            GeneralClass.Program.Add("Description", txtDescription.Text.Trim(), "S");
            GeneralClass.Program.Add("Department", txtDepartment.Text.Trim(), "S");
            GeneralClass.Program.Add("Location", txtLocation.Text.Trim(), "S");
            GeneralClass.Program.Add("created_by",txtFullname.SelectedItem.Value, "S");
            GeneralClass.Program.Add("requester_mail", txtFullname.SelectedItem.Value + "@ksau-hs.edu.sa", "S");
            GeneralClass.Program.Add("requested_for", ddlRequestedFor.SelectedValue.ToString(), "S");
            GeneralClass.Program.Add("category_id", ddlCategory.SelectedItem.Value, "I");
            int intReturnID = GeneralClass.Program.InsertRecordStatement("t_requests");
            if (intReturnID!=0)
            {

                GeneralClass.Program.Add("request_id", intReturnID.ToString(), "I");
                GeneralClass.Program.Add("assigned_to", ddlRequestedFor.SelectedValue.ToString(), "S");
                GeneralClass.Program.Add("assigned_date",txtAssignDate.Text+" "+txtAssignHours.Text+":"+txtAssignMinutes.Text+":"+"00"+" "+ddlAssignGlo.SelectedItem.Text, "D");
                int intReturnID2 = GeneralClass.Program.InsertRecordStatement("t_assignment");
                if (intReturnID2!=0)
                {
                    
                    GeneralClass.Program.Add("request_id", intReturnID.ToString(), "I");
                    GeneralClass.Program.Add("status", "Completed", "S");
                    GeneralClass.Program.Add("status_id", "5", "I");
                    GeneralClass.Program.Add("serviced_by", ddlRequestedFor.SelectedValue.ToString(), "S");
                    GeneralClass.Program.Add("modified_date", txtCompleteDate.Text + " " + txtCompleteHours.Text + ":" + txtCompleteMinutes.Text + ":" + "00" + " " + ddlCompleteGlo.SelectedItem.Text, "D");
                    int intReturnID3 = GeneralClass.Program.InsertRecordStatement("t_requestServicehistory");
                    if (intReturnID3 != 0)
                        Response.Redirect("~/frmAdminDefault.aspx");

                }
            }
     
      
        }
        catch (Exception ex)
        {
           
        }  
       

  
    }
    protected void LoadCategory()
    {
        /// <summary>
        /// 	Description: Load categorie into drop list  
        ///	
        ///
        /// 	Date:2nd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadCategory();
        /// </summary>

        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSet("t_category", "*", "");
            ddlCategory.DataValueField = "id";
            ddlCategory.DataTextField = "category";
            ddlCategory.DataSource = ds;
            ddlCategory.DataBind();

        }
        catch (Exception ex)
        {

            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }
    protected string GetMobileNo(string userid)
    {
        /// <summary>
        /// 	Description: Retrieve mobile no from the t_user table
        ///	
        /// 
        /// 	Date:4th/Dec/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        string mobile = "";

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT mobile FROM t_users WHERE id='" + userid + "'");
            while (reader.Read())
            {
                if (DBNull.Value != reader["mobile"] && reader["mobile"].ToString() != "")
                    mobile = reader["mobile"].ToString();
            }
            reader.Close();
            return mobile;
        }
        catch (Exception ex)
        {
            return mobile;
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }
    protected string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
        {
            return builder.ToString().ToLower();
            return builder.ToString();
        }
        else return "nothing";
    }
    protected void BacktoList(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: back to user request list  
        ///	
        ///
        /// 	Date:22/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        Response.Redirect("frmUserRequestlist.aspx");

    }
    protected void DisplayOptions()
    {
        /// <summary>
        /// 	Description: Display controls based on user rights
        ///	
        ///
        /// 	Date:21/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>

        if ((null != Session["Admin"] && Session["Admin"].ToString() == "true") || (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString()=="true"))
        {
            //if (Session["Admin"].ToString() == "true")
            //{
            
                hlkUserlist.Enabled = true;
                hlkUserlist.Visible = true;
                
                hlkAdminRequestList.Enabled = true;
                hlkAdminRequestList.Visible = true;                         

               
            //}
        }
        else
        {
            //hlkUserRequestList.Enabled = true;
            //hlkUserRequestList.Visible = true;



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
    public void LdapUserInfo(string logID, string username, string pwd)
    {
        /// <summary>
        /// 	Description: fetch userdetails from the LDAP
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>


        try
        {
            string domainAndUsername = username.Trim();
            DirectoryEntry entry = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", domainAndUsername, pwd);
            Object obj = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();
            if (null == result)
            {
             //   return string.Empty;
            }
            else
            {
                // Update the new path to the user in the directory
                //_path = result.Path;
                //_filterAttribute = (string)result.Properties["cn"][0];
                string fullName = result.GetDirectoryEntry().Properties["Name"].Value.ToString();
               // return (string)result.Properties["cn"][0].ToString();


            }
        }
        catch (Exception exp)
        {
            //return string.Empty;
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
                   // Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
                    Hlk.NavigateUrl = "frmTechnicianDefault.aspx";
                }
                else
                    if (null != Session["ITMember"] && Session["ITMember"].ToString() == "true")
                    {
                        LB.Text = "IT Member";
                        //Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
                        Hlk.NavigateUrl = "frmAdminDefault.aspx";
                    }
                    else
                {
                    LB.Text = "User";
                    Hlk.NavigateUrl = "frmUserRequestList.aspx";
                }
        }
    }
    protected void AttachFile(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: attaching file 
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

       

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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmNewRequest.aspx");
    }
   

    protected void chbRequestedFor_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chbRequestedFor.Checked)
        {
            
            LoadRequestForUsers();
        }
        if (!chbRequestedFor.Checked)
        {
            ddlRequestedFor.SelectedIndex = 0;
            //LoadAllocatedItems();         
        }    
    }
    protected void LoadRequestForUsers()
    {
        /// <summary>
        /// 	Description: Import userid and fullname  from the LDAP
        ///	
        ///
        /// 	Date:4th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        /// 
        ListItem l1 = new ListItem();
        l1.Value = "0";
        l1.Text = "-- Select Owner name -- ";
        if(this.Label7.Text.ToString()=="«ÿ·» „‰")
            l1.Text = "-- Õœœ «”„ «·„Œ ’ -- ";
        ddlRequestedFor.Items.Add(l1);

        try
        {

            DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", "wstaff", "Ldap@KSAU!23");//OU=staff,OU=collegeusers,OU=mis                                


            //  DirectoryEntry entry1 = new DirectoryEntry("LDAP://OU=staff,OU=collegeusers,DC=med,DC=ksuhs,DC=edu,DC=sa", "wstaff", "test123");

            DirectorySearcher mySearcher = new DirectorySearcher(entry1);
            SearchResultCollection results;
            results = mySearcher.FindAll();
            
            string strFullName;
            string strLoginName;

            DirectorySearcher dSearch = new DirectorySearcher(entry1);

            dSearch.Filter = "(&(objectCategory=user)(cn=*))";

            foreach (SearchResult sResultSet in dSearch.FindAll())
            {

                strFullName = GetProperty(sResultSet, "Name");
                strLoginName = GetProperty(sResultSet, "sAMAccountName");
                if ("" != strLoginName.Trim())
                    if (strFullName != string.Empty)
                    {
                        ListItem li = new ListItem();
                        li.Value = strLoginName;
                        li.Text = strFullName;
                        
                        txtFullname.Items.Add(li);
                    }
            }

        }

        catch (Exception ex)
        {
            //if(null!= reader)
            //    reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
    protected string GetProperty(SearchResult searchResult, string PropertyName)
    {
        if (searchResult.Properties.Contains(PropertyName))
            return searchResult.Properties[PropertyName][0].ToString();
        else
            return string.Empty;
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
    protected void mOwnRequestClick(object sender, EventArgs e)
    {
        try
        {
            if (null != Session["ITMember"])
            {
                if (Session["ITMember"].ToString() == "true")
                    Response.Redirect("./frmITRequestList.aspx");
            }
            else
                Response.Redirect("./frmUserRequestList.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillTechnicianDDL()
    {
        try
        {
            int counter = 1;
            string strTechQuery = "SELECT * FROM t_users WHERE user_group='2'";
            reader = GeneralClass.Program.gRetrieveRecord(strTechQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddlRequestedFor.Items.Add(reader["full_name"].ToString());
                    ddlRequestedFor.Items[counter].Value = reader["id"].ToString();
                    counter++;

                }
                reader.Close();
            }
            else reader.Close();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSearchUser(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < txtFullname.Items.Count; i++)
            {
                if (txtFullname.Items[i].Value == txtUserNameSearch.Text)
                    txtFullname.SelectedIndex = i;
            }
        }
        catch (Exception exp)
        {
        }
    }
   
}

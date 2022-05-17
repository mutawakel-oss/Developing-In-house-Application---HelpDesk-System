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
using System.Data.Odbc;

public partial class frmInvRecepient : System.Web.UI.Page
{
    OdbcDataReader reader;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadLdapUsers();
            LoadRecepientUsers();
        }
        LoggedAs();
    }

   protected void LoadLdapUsers()
    {
        /// <summary>
        /// 	Description: Import userid and fullname  from the LDAP
        ///	
        ///
        /// 	Date:3rd/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        try
        {
            
            //DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", "wstaff", "test123");//OU=staff,OU=collegeusers,OU=mis                                


            DirectoryEntry entry1 = new DirectoryEntry("LDAP://DC=med,DC=ksuhs,DC=edu,DC=sa", "wstaff", "Ldap@KSAU!23");

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
                
                if(!Convert.ToBoolean(Convert.ToInt32(GetProperty(sResultSet,"userAccountControl"))&0x0002)) // to filter only active(enabled) users
                if ("" != strLoginName.Trim())
                {                     
                    if (strFullName != string.Empty)
                    {
                        ListItem li = new ListItem();
                        li.Value = strLoginName;
                        li.Text = strFullName;
                        lstAllUsers.Items.Add(li);
                    }
                }
            }

            SortListBox();
          } 
        
        catch (Exception ex)
        {
            //if(null!= reader)
            //    reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }


    protected void SortListBox()
    {
        /// <summary>
        /// 	Description: sort values in the list box
        ///	
        ///
        /// 	Date: 22nd/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>


        try
        {
            ArrayList array1 = new ArrayList();

            for (int i = 0; i < lstAllUsers.Items.Count; i++)
            {
                array1.Add(lstAllUsers.Items[i].Text + "~" + lstAllUsers.Items[i].Value);
            }
            array1.Sort();
            lstAllUsers.Items.Clear();
            for (int i = 0; i < array1.Count; i++)
            {

                string item = array1[i].ToString();

                string[] items = item.Split('~');

                ListItem li = new ListItem();
                li.Value = (string)items[1];
                li.Text = (string)items[0];

                lstAllUsers.Items.Add(li);
            }
        }
        catch (Exception ex)
        { 
        Response.Write("error.aspx?error=?"+ex.Message);
        }
    }


    protected string GetProperty(SearchResult searchResult, string PropertyName)
    {
        if (searchResult.Properties.Contains(PropertyName))
            return searchResult.Properties[PropertyName][0].ToString();
        else
            return string.Empty;
    }

    protected void SelectSingleUSer(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Adds single user from the userslist box to selected user list box
        /// 	
        ///
        /// 	Date:3rd/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        try
        {
            if (lstAllUsers.SelectedIndex > -1)
            {
                string strText = lstAllUsers.SelectedItem.Text;
                string strValue = lstAllUsers.SelectedItem.Value;
                ListItem li = new ListItem();
                li.Text = strText;
                li.Value = strValue;
                lstSelectedUsers.Items.Add(li);
                lstAllUsers.Items.Remove(li);
            }        
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }  
    
    }

    protected void DeSelectSingleUSer(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Adds single user from the userslist box to selected user list box
        /// 	
        ///
        /// 	Date:3rd/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        try
        {
            if (lstSelectedUsers.SelectedIndex > -1)
            {
                string strText = lstSelectedUsers.SelectedItem.Text;
                string strValue = lstSelectedUsers.SelectedItem.Value;
                ListItem li = new ListItem();
                li.Text = strText;
                li.Value = strValue;
                lstAllUsers.Items.Add(li);
                lstSelectedUsers.Items.Remove(li);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void SelectMultipleUSer(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Adds single user from the userslist box to selected user list box
        /// 	
        ///
        /// 	Date:3rd/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        
        try
        {
            //lstSelectedUsers.Items.Clear();
            for(int i=0; i< lstAllUsers.Items.Count; i++)                                    
             lstSelectedUsers.Items.Add(lstAllUsers.Items[i]);
         lstAllUsers.Items.Clear();
                                      
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void DeSelectMultipleUSer(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Adds single user from the userslist box to selected user list box
        /// 	
        ///
        /// 	Date:3rd/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        try
        {
           //lstAllUsers.Items.Clear();
            for (int i = 0; i < lstSelectedUsers.Items.Count; i++)
                lstAllUsers.Items.Add(lstSelectedUsers.Items[i]);
                lstSelectedUsers.Items.Clear();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }

    protected void SaveRecepient(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: save recepient user from selected user list box  to t_invrecepient table
        /// 	
        ///
        /// 	Date:3rd/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        //try
        //{
            GeneralClass.Program.gRetrieveRecord("delete from t_invrecepient");

            for(int i= 0; i<lstSelectedUsers.Items.Count; i++)
            {
                GeneralClass.Program.Add("id", lstSelectedUsers.Items[i].Value.ToString(), "S");
                GeneralClass.Program.Add("full_name", lstSelectedUsers.Items[i].Text.ToString(), "S");
                GeneralClass.Program.InsertRecordStatement("t_invrecepient");
                AddUser(lstSelectedUsers.Items[i].Value.ToString(), lstSelectedUsers.Items[i].Text.ToString()); // insert into t_user table, later this user may be deleted from t_invrecepient table, so old received records may not be available
            }
            //Response.Redirect("frminvproductlist.aspx");
            if (Request.QueryString["called"] != null)
            {
                HttpContext.Current.Response.Redirect(Request.QueryString["called"].ToString());

            }
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error=" + ex.Message);               
        //}
    }

    protected void LoadRecepientUsers()
    {
        /// <summary>
        /// 	Description: load recepient users from t_invrecepient table to selected user list box
        /// 	
        ///
        /// 	Date:3rd/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        try
        {
            //DataSet ds;
            //ds = GeneralClass.Program.gDataSet("t_invrecepient", "*", "");

            //lstSelectedUsers.DataSource = ds;
            //lstSelectedUsers.DataValueField = "id";
            //lstSelectedUsers.DataTextField = "full_name";
            //lstSelectedUsers.DataBind();

            reader = GeneralClass.Program.gRetrieveRecord("SELECT * FROM t_invrecepient ORDER BY full_name");

            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value= reader["id"].ToString();
                li.Text = reader["full_name"].ToString();
                lstSelectedUsers.Items.Add(li);
                lstAllUsers.Items.Remove(li);            
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void AddUser(string logID, string fullName)
    {
        // <summary>
        /// 	Description: Create intial entry for local admin  
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        //GeneralClass.Program.gRetrieveRecord("delete from t_users");
        //GeneralClass.Program.Add("id", "123456", "S");
        //GeneralClass.Program.Add("full_name", "Local Admin", "S");

        if (!UserExists(logID))
        {
            GeneralClass.Program.Add("id", logID, "S");
            GeneralClass.Program.Add("full_name", fullName, "S");
            int intReturnID = GeneralClass.Program.InsertRecordStatement("t_users");
        }
    }

    protected bool UserExists(string logID)
    {
        // <summary>
        /// 	Description: Check for the exitstance of user in the temp table  
        ///	
        ///
        /// 	Date:1st/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        bool userExists = false;

        reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(id) FROM t_users WHERE id='" + logID + "'");
        while (reader.Read())
        {
            if (Convert.ToInt32(reader[0]) > 0)
                userExists = true;
        }
        reader.Close();
        return userExists;
    }

    protected void GoBack(object sender, EventArgs e)
    {
        // <summary>
        /// 	Description: Back to called page
        ///	
        ///
        /// 	Date:12th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        if (Request.QueryString["called"] != null)
        {
            HttpContext.Current.Response.Redirect(Request.QueryString["called"].ToString());
        }    
       // HttpContext.Current.Response.Redirect("frmLdapUsers.aspx");

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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmInvRecepient.aspx");
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

        Response.Redirect("frmAssignDeptSecretary.aspx?secretary=frmInvRecepient.aspx");
    }
    protected void AddNewTechnician(object sender, EventArgs e)
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

        Response.Redirect("frmNewPcTech.aspx?called=frmInvRecepient.aspx");
    }
}

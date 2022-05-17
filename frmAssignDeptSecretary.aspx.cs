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
using System.DirectoryServices;

public partial class frmAssignDeptSecretary : System.Web.UI.Page
{
    OdbcDataReader reader;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadLdapUsers();
            LoadDepartment();
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


            DirectoryEntry entry1 = new DirectoryEntry("LDAP://OU=mis,DC=med,DC=ksuhs,DC=edu,DC=sa", "wstaff", "Ldap@KSAU!23");

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
        
                if(!(Convert.ToBoolean(Convert.ToInt32(GetProperty(sResultSet,"userAccountControl"))&0x0002)))  //filtering for the active(enabled) users

                if ("" != strLoginName.Trim())
                    if (strFullName != string.Empty)
                    {
                        ListItem li = new ListItem();
                        li.Value = strLoginName;
                        li.Text = strFullName;
                        lstAllUsers.Items.Add(li);
                    }
            }
            SortListBox();// for sorting the list
        
        }

        catch (Exception ex)
        {
            //if(null!= reader)
            //    reader.Close();
            Response.Redirect("error.aspx?error=unable to find server");
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
            for (int i = 0; i < lstAllUsers.Items.Count; i++)
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
        

        GeneralClass.Program.Add("secretary", SecretaryUsers(), "S");

        GeneralClass.Program.UpdateRecordStatement("t_department_master", "id", ddlDepartments.SelectedValue.ToString());

        for (int i = 0; i < lstSelectedUsers.Items.Count; i++)
        {
            GeneralClass.Program.AddUser(lstSelectedUsers.Items[i].Value.ToString(), lstSelectedUsers.Items[i].Text.ToString());         

        }


        //if (null != Request.QueryString["secretary"])
        //    Response.Redirect(Request.QueryString["secretary"]);

        HttpContext.Current.Response.Redirect("frmLdapUsers.aspx");
              

       // }
      //  Response.Redirect("frminvproductlist.aspx");
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error=" + ex.Message);               
        //}
    }

    protected string SecretaryUsers()
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
    
        //for (int i = 0; i <= cblGroups.Items.Count - 1; i++)
        for (int i = 0; i < lstSelectedUsers.Items.Count; i++)
        {
            
                if (grps != "")
                {
                    grps += ",";
                }

                grps += lstSelectedUsers.Items[i].Value.ToString();                 
            
        }
        return grps;
    }




    

    protected void ddlDepartments_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Load the Department secretary from the Department_master table
        ///	
        ///
        /// 	Date:10th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>           

        string strSecretaries = "";
        lstSelectedUsers.Items.Clear();

        try
        {

            reader = GeneralClass.Program.gRetrieveRecord("SELECT secretary FROM t_department_master WHERE id =" + ddlDepartments.SelectedValue);

            while (reader.Read())
            {
                strSecretaries = reader["secretary"].ToString();
            }
            reader.Close();

            string[] strSecratary = strSecretaries.Split(',');

            for (int i = 0; i <= strSecratary.Length - 1; i++)
            {
                AddSecretary(strSecratary[i].ToString());
            }
        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();

            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void AddSecretary(string logID)
    {
        /// <summary>
        /// 	Description: Load the Department secretary from names from t_users table
        ///	
        ///
        /// 	Date:10th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>    
        

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,full_name FROM t_users WHERE id='" + logID + "'");

            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value = reader["id"].ToString();
                li.Text = reader["full_name"].ToString();
                lstSelectedUsers.Items.Add(li);
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

    protected void LoadDepartment()
    {
        // <summary>
        /// 	Description: Load the departments from t_department_master  
        ///	
        ///
        /// 	Date:10th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        ListItem l1 = new ListItem();
        l1.Value = "0";
        l1.Text="-- Select Department -- ";
        ddlDepartments.Items.Add(l1);
        
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,department_name FROM t_department_master");
            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value = reader["id"].ToString();
                li.Text=reader["department_name"].ToString();
                ddlDepartments.Items.Add(li);
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

    protected void BackToHome(object sender, EventArgs e)
    {
        // <summary>
        /// 	Description: navigate to the called page  
        ///	
        ///
        /// 	Date:11th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>
        if (null != Request.QueryString["secretary"])
            Response.Redirect(Request.QueryString["secretary"]);    

      //  HttpContext.Current.Response.Redirect("frmLdapUsers.aspx");
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

        Response.Redirect("frmNewPcTech.aspx?called=frmAssignDeptSecretary.aspx");
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

        HttpContext.Current.Response.Redirect("frmInvRecepient.aspx?called=frmAssignDeptSecretary.aspx");
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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmAssigndeptSecretary.aspx");
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
            Response.Write("error.aspx?error=?" + ex.Message);
        }
    }

    protected void SaveNewDepartment(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save a new Department into t_department_master 
        ///	
        ///
        /// 	Date:27th/Oct/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

        //try
        //{
        GeneralClass.Program.Add("department_name", txtNewDepartment.Text, "S");
        GeneralClass.Program.InsertRecordStatement("t_department_master");
        LoadDepartment();

       // if (null != Request.QueryString["id"])
            HttpContext.Current.Response.Redirect("redirect_1.aspx?NEWDEPARTMENT=true");
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error=" + ex.Message);
        //}

    }

}

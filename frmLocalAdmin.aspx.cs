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

public partial class frmLocalAdmin : System.Web.UI.Page
{
    OdbcDataReader reader;
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    
    protected void  ImportUsers(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Import user from the LDAP  
        ///	
        ///
        /// 	Date:21/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  ImportUsers();
        /// </summary>

        try
        {

            string bln = IsAuthenticated("", txtUserName.Text, txtPassword.Text);
            if (bln != null && bln != "")
            {
                pullusers("", txtUserName.Text, txtPassword.Text);

                lblResult.Text = "Users are imported successfully";
                Response.Redirect("frmUserList.aspx");

            }
            else
                lblResult.Text = "You are not a authenticated user";
        }
        catch (Exception ex)
        {
          
            //  Response.Redirect("error.aspx?error="+ex.Message);
        }
    }


    public static string GetProperty(SearchResult searchResult, string PropertyName)
    {
        /// <summary>
        /// 	Description: Returns property for the LDAP data fetching
        ///	
        ///
        /// 	Date:21/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  GetProperty(sResultSet, "sAMAccountName");
        /// </summary>

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

    
    public string IsAuthenticated(string domain, string username, string pwd)
    {
        /// <summary>
        /// 	Description: Check  for authentication of user in the LDAP
        ///	
        ///
        /// 	Date:21/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  IsAuthenticated("", txtusername.Text, txtpwd.Text)
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
                return string.Empty;
            }
            else
            {
                // Update the new path to the user in the directory
                //_path = result.Path;
                //_filterAttribute = (string)result.Properties["cn"][0];
                return (string)result.Properties["cn"][0].ToString();
            }
        }
        catch (Exception exp)
        {
            return string.Empty;
        }
    }


    private void pullusers(string domain, string username, string pwd)
    {
        /// <summary>
        /// 	Description: Import users from the LDAP
        ///	
        ///
        /// 	Date:21/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  pullusers("", txtusername.Text, txtpwd.Text)
        /// </summary>
        
        try
        {
            DirectoryEntry entry1 = new DirectoryEntry("LDAP://OU=staff,OU=collegeusers,DC=med,DC=ksuhs,DC=edu,DC=sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis
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
            //string strString = "";
            foreach (SearchResult sResultSet in dSearch.FindAll())
            {
                strLoginName = GetProperty(sResultSet, "sAMAccountName");
                strFullName = GetProperty(sResultSet, "Name");
                strEMail = GetProperty(sResultSet, "mail");
                strBadgeNo = GetProperty(sResultSet, "employeeid");
                strDepartment = GetProperty(sResultSet, "department");
                strPager = GetProperty(sResultSet, "pager");
                strTitle = GetProperty(sResultSet, "title");
                strTele = GetProperty(sResultSet, "telephonenumber");


                if ("" == strBadgeNo)
                    strBadgeNo = "0";


                if ("" == strTele)
                    strTele = "0";

                strMobile = GetProperty(sResultSet, "mobile");

                if ("" == strMobile)
                    strMobile = "0";
                if("" != strLoginName.Trim())
                if (strFullName != string.Empty )
                {

                    

                    GeneralClass.Program.Add("full_name", strFullName, "S");
                    GeneralClass.Program.Add("badge_number", strBadgeNo, "I");
                    GeneralClass.Program.Add("email_address", strEMail, "S");
                    GeneralClass.Program.Add("phone_ext", strTele, "I");
                    GeneralClass.Program.Add("mobile", strMobile, "I");
                    GeneralClass.Program.Add("department_name", strDepartment, "S");
                    GeneralClass.Program.Add("job_title", strTitle, "S");
                    //GeneralClass.Program.Add("login_name", strLoginName, "S");

                    if (UserExists(strLoginName))
                    {
                        int intReturnID = GeneralClass.Program.UpdateRecordStatement("t_users", "login_name", strLoginName);
                                          
                    }
                    else
                    {
                        GeneralClass.Program.Add("login_name", strLoginName, "S");
                        int intReturnID = GeneralClass.Program.InsertRecordStatement("t_users");
                    }
                                      

                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected bool UserExists(string loginID)
    { 
     /// <summary>
        /// 	Description: Checking for the existence of the user
        ///	
        ///
        /// 	Date:21/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        bool blnExits=false;

        try
        {
             reader=GeneralClass.Program.gRetrieveRecord("select count(full_name) from t_users where login_name='" + loginID + "'");

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

}

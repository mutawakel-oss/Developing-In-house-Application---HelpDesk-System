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
using System.Data.SqlClient;
using System.Data.Odbc;
using GeneralClass;

public partial class Default3 : System.Web.UI.Page
{
       
    protected void Page_Load(object sender, EventArgs e)
    {
        DisplayUserList();
    }

    protected void DisplayUserList()
    {
        /// <summary>
        /// 	Description: Populate data from t_users into gridview  
        ///	
        ///
        /// 	Date:15/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: userlist in the gridview
        /// 	Example:  DisplayUserList();
        /// </summary>
        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSet("t_users", "*", " ORDER BY full_name ASC");
            gdvUserList.DataSource = ds;
            gdvUserList.DataBind();
        }
        catch (Exception ex)
        {

            Response.Redirect("error.aspx?error=" + ex.Message);
            
            //Response.Write(ex.Message);
        }
    
    }
    
    
    protected void gdvUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            DataSet ds;
            ds = GeneralClass.Program.gDataSet("t_users", "*", " ORDER BY full_name ASC");
            
            gdvUserList.DataSource = ds;
            gdvUserList.PageIndex = e.NewPageIndex;
            gdvUserList.DataBind();
           
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
}

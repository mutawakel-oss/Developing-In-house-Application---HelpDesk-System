using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Programs1
/// </summary>
public class Programs1
{
    public String ConnectionString = System.Configuration.ConfigurationManager.AppSettings.Get("DbConnection").ToString(); 
    
    public Programs1()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SqlConnection Connect()
    {
        //string SqlConnectionString = "Server=mis9\\SQLEXPRESS; Database=HelpDesk;Integrated Security=SSPI";
        //SqlConnection conn = new SqlConnection(SqlConnectionString);
        SqlConnection conn = new SqlConnection(ConnectionString);
        conn.Open();
        return conn;
    }
}

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
using System.Net.Mail;
using System.DirectoryServices;
using System.Data.Odbc;
using System.Text;
using System.Drawing;

using System.Net;
using System.IO;
using System.Threading;



public partial class frmNewUserAccount : System.Web.UI.Page
{
    OdbcDataReader reader;
    bool bEmail = false;
    bool bMcurriculum = false;
    bool bMMeducation = false;
    bool bLibray = false;
    string strBadgeNo = "";
    string strDfileName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HyperLink LB1 = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
            Label LB = (Label)this.Master.Page.Controls[0].Controls[3].Controls[9].Controls[1];
            HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].Controls[5];
            //Hlk.NavigateUrl = "frmUserRequestList.aspx";
            //LB.Text = "User";
            mFillComplainsGrid();
            LB1.Text = "Log Out";
            Hlk.NavigateUrl = "frmAdminDefault.aspx";
        }
        catch (Exception exp)
        {
        }
    }
   

    protected void InitializeCulture()
    {
        //Response.Write(Request.QueryString["Language"].ToString());
        //string culture = Request.QueryString["Language"].ToString();
        string culture = "en-US";
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
    protected void mFillComplainsGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the complains grid
        /// Author: mutawakelm
        /// Date :3/15/2009 2:30:02 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            
            DataTable tblUsersComplains = new DataTable();
            tblUsersComplains.Columns.Add("no");
            tblUsersComplains.Columns.Add("category");
            tblUsersComplains.Columns.Add("userName");
            tblUsersComplains.Columns.Add("subject");
            tblUsersComplains.Columns.Add("description");
            tblUsersComplains.Columns.Add("date");
            string strComplainsQuery = "SELECT comp.id,comp.subject,comp.description,comp.category,comp.Complain_date,us.full_name FROM t_user_complain as comp,t_users as us WHERE comp.user_id=us.id ORDER BY comp.id DESC";
            reader = GeneralClass.Program.gRetrieveRecord(strComplainsQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblUsersComplains.Rows.Add(reader["id"].ToString(), reader["category"].ToString(), reader["full_name"].ToString(), reader["subject"].ToString(), reader["description"].ToString(), reader["Complain_date"].ToString());
                }
                reader.Close();
                userComplainsGrid.DataSource = tblUsersComplains;
                userComplainsGrid.DataBind();
            }
            else reader.Close();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mGetNextPage(object sender, DataGridPageChangedEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to get the next page
        /// Author: mutawakelm
        /// Date :3/15/2009 2:45:01 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {

            userComplainsGrid.CurrentPageIndex = e.NewPageIndex;
            userComplainsGrid.DataBind();
        }
        catch (Exception exp)
        {
        }
    }
 
}

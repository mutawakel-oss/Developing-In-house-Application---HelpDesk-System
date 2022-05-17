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

public partial class frmUserRequestList : System.Web.UI.Page
{
    OdbcDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string selectQuery = "SELECT t_emailServiceRequest.* from t_emailServiceRequest";
            DataSet ds = GeneralClass.Program.gDataSetGridView(selectQuery, "t_emailServiceRequest");
           if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            gdvRequestList.DataSource = ds;
            gdvRequestList.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
            
    }
  

    protected void btnCreate_Click(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to create new accounts for 
        /// the selected users in the page "frmNewUserRecords"
        /// Author: mutawakelm
        /// Date :02/12/2008 09:01:38
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        
        try
        {
            //DataTable d = new DataTable();
            //DataRow r ;
            Response.Write(gdvRequestList.Rows[0].Cells[8].Text.ToString());
           // Response.Write(gdvRequestList.Columns.Count.ToString());
            for (int i = 0; i < gdvRequestList.Rows.Count; i++)
            {
             // Response.Write( gdvRequestList.Rows[i].Cells[2].Text.ToString());
               
                /*Response.Redirect("has checked");
                if (gdvRequestList.Columns[8].ToString() == "True")
                    Response.Redirect("has checked");*/
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
}

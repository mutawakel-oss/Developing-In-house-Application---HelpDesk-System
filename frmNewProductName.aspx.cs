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

public partial class frmNewProductName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SaveNewProductName(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save a new product name into t_product_master 
        ///	
        ///
        /// 	Date:11th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

        try
        {
            Program.Add("product_name", txtProductName.Text, "S");
            Program.InsertRecordStatement("t_product_master");

            HttpContext.Current.Response.Redirect("");
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }

}

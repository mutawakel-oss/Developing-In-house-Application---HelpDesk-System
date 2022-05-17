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

public partial class frmArabicSystem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //The following code will be used to call the arabic login page
        //Session["Language"] = "ar-EG";
        Session["Language"] = "ar-EG";
        Response.Redirect("./Login.aspx?lang=ar-EG");
    }
}

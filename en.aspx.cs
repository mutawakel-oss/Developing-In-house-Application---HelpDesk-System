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

public partial class help_desk_en : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
            //Session["Language"] = "en-US";
       Session["Language"] = "en-US";
        Response.Redirect("./Login.aspx?lang=en-US");
   
     
      
    }
}

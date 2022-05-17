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

public partial class frmCallEvaluationPopup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string popupScript = "<script language='javascript'>" +
  "window.open('frmServiceEvaluation.aspx?id=" + Request.QueryString["id"].ToString() + "', 'CustomPopUp', " +
  "'width=445, height=380, menubar=yes, resizable=no')" +
  "</script>";

            Page.RegisterStartupScript("PopupScript", popupScript);

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
}

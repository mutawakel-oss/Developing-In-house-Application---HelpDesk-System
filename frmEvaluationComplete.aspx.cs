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

public partial class frmEvaluationComplete2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void mGoBack(object sender, EventArgs e)
    {
        try
        {
            if ((null != Session["Admin"] && Session["Admin"].ToString() == "true") || (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString() == "true"))
            {
                // if (Session["Admin"].ToString() == "true")
                Response.Redirect("frmAdminRequestList.aspx");
            }
            else
            {
                //if (null != Session["LocalAdmin"])
                //    if (Session["LocalAdmin"].ToString() == "true")
                //        Response.Redirect("frmAdminRequestList.aspx");
                //    else
                //    {
                if (null != Session["Technician"])
                {
                    if (Session["Technician"].ToString() == "true")
                        Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
                }
                else
                    Response.Redirect("frmUserRequestList.aspx");
                //}

            }
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
}

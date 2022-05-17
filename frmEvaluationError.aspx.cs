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

public partial class frmServiceEvaluation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mSelectChanged(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSubmitRequesterStatus(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will insert the submission of the requester for the request 
        /// the submission will be one of two cases 
        /// 1- solved .
        /// 2- did not solved.
        /// Author: mutawakelm
        /// Date :10/05/2008 11:40:48 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
           

        }
        catch (Exception mSubmitRequestStatus_Exp)
        {
            Response.Write(mSubmitRequestStatus_Exp.Message.ToString());

        }
    }
}

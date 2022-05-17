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

public partial class frmServiceEvaluation : System.Web.UI.Page
{
    OdbcDataReader reader = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                string strEvaluationQuery = "Select requester_rate from t_requests where id=" + Request.QueryString["id"].ToString();
                reader = GeneralClass.Program.gRetrieveRecord(strEvaluationQuery);
                if (reader.HasRows)
                {
                    reader.Read();
                    if ((reader["requester_rate"] != null) && (reader["requester_rate"] != ""))
                    {
                        if (reader["requester_rate"].ToString() != "0")
                            Response.Redirect("frmEvaluationError.aspx");
                    }
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    Response.Redirect("Error.aspx?Error=Failed.");
                    
                }



            }
            else
                Response.Redirect("Error.aspx?Error=Failed.");
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
            this.btnSubmitStatus.Enabled = true;
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
            int intQueryResult = 0;
            if (rdioRequestStatus.Items[0].Selected == true)
            {
                GeneralClass.Program.Add("requester_submit", "Solved", "S");
                GeneralClass.Program.Add("requester_rate", rating.CurrentRating.ToString(), "I");
                if(!string.IsNullOrEmpty(txtComments.Text.ToString()))
                    GeneralClass.Program.Add("requester_comment",txtComments.Text.ToString(), "S");
                intQueryResult = GeneralClass.Program.UpdateRecordStatement("t_requests", "id", Request.QueryString["id"].ToString());
            }
            else
            {
                GeneralClass.Program.Add("requester_submit", "NotSolved", "S");
                GeneralClass.Program.Add("requester_rate", rating.CurrentRating.ToString(), "I");
                if (!string.IsNullOrEmpty(txtComments.Text.ToString()))
                    GeneralClass.Program.Add("requester_comment", txtComments.Text.ToString(), "S");
                intQueryResult = GeneralClass.Program.UpdateRecordStatement("t_requests", "id", Request.QueryString["id"].ToString());
            }
            Response.Redirect("./frmEvaluationSubmittedMessage.aspx");

        }
        catch (Exception mSubmitRequestStatus_Exp)
        {
            Response.Write(mSubmitRequestStatus_Exp.Message.ToString());

        }
    }
}

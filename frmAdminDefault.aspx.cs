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

public partial class frmAdminDefault : System.Web.UI.Page
{
    OdbcDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        HyperLink LB1 = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired");
        }
        LB1.Text = "Log Out";
        LoggedAs();
        NumberOfUnAssignedRequests();
        NumberOfPendingRequests();
        NumberOfDueTodayRequests();
        mCheckAdminAccess();
        
    }

    protected void LoggedAs()
    {
        /// <summary>
        /// 	Description: show the user group of the logged in user
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>
        /// 

        if(null !=Session["UserFullName"] && Session["UserFullName"].ToString()!="")
            lblLogUser.Text = Session["UserFullName"].ToString();
        if (null != Session["Badge"] && Session["Badge"].ToString() != "")
            lblBadgeNo.Text = Session["Badge"].ToString();
        if (null != Session["Title"] && Session["Title"].ToString() != "")
            lblTitle.Text = Session["Title"].ToString();
        if (null != Session["Department"] && Session["Department"].ToString() != "")
            lblDepartment.Text = Session["Department"].ToString(); 
        Label LB = (Label)this.Master.Page.Controls[0].Controls[3].Controls[9].Controls[1];
        if (null != Session["Admin"] && Session["Admin"].ToString() == "true")
            LB.Text = "Administrator";
        else
        {
            if (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString() == "true")
                LB.Text = "Local Admin";
            else
                if (null != Session["Technician"] && Session["Technician"].ToString() == "true")
                    LB.Text = "PC Technician";
                else
                    LB.Text = "User";
        }    
        //The following code will be used to assign the home link
        HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].Controls[5];
        if (null != Session["Admin"] && Session["Admin"].ToString() == "true")
        {
            LB.Text = "Administrator";
            Hlk.NavigateUrl = "frmAdminDefault.aspx";
        }
        else
        {
            if (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString() == "true")
            {
                LB.Text = "Local Admin";
                Hlk.NavigateUrl = "frmAdminDefault.aspx";
            }
            else
                if (null != Session["Technician"] && Session["Technician"].ToString() == "true")
                {
                    LB.Text = "PC Technician";
                    //Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
                    Hlk.NavigateUrl = "frmTechnicianDefault.aspx";
                }
                else
                    if (null != Session["ITMember"] && Session["ITMember"].ToString() == "true")
                    {
                        LB.Text = "IT Member";
                        //Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
                        Hlk.NavigateUrl = "frmItDefault.aspx";

                    }
                    else
                    {
                        LB.Text = "User";
                        Hlk.NavigateUrl = "frmUserRequestList.aspx";
                    }
        }
    }

    protected void EditLogUserInfo(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Navigate to the Editor page of user info edit
        ///	
        ///
        /// 	Date:28/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>    
        
        
        if(null!=Session["UserID"]&& Session["UserID"].ToString() !="")
        Response.Redirect("frmEditLogUserInfo.aspx?logid="+Session["UserID"].ToString()+"& backto=frmAdminDefault.aspx");
    }

    

    protected void NumberOfUnAssignedRequests()
    {

        //=====================================================//
        /// <summary>
        /// Description: Assign no of unassinged requests
        /// Author: Olivery
        /// Date :9/19/2007 12:07:38 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        int unAssignedRequest=0;

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(id) FROM t_requests WHERE id not in (SELECT request_id FROM t_assignment)");
            while (reader.Read())
            {
                unAssignedRequest = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            lbtnUnAssignedRequestCount.Text = unAssignedRequest.ToString();

        }
        catch (Exception NumberOfUnAssignedRequests_Exp)
        {
            Response.Redirect("error.aspx?error="+NumberOfUnAssignedRequests_Exp.Message);
        }

    }

    protected void NumberOfPendingRequests()
    {

        //=====================================================//
        /// <summary>
        /// Description: Assign no of unassinged requests
        /// Author: Olivery
        /// Date :9/19/2007 12:07:38 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        int PendingRequest = 0;

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(id) FROM t_requests WHERE status_id=4");
            while (reader.Read())
            {
                PendingRequest = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            lbtnPendignRequestCount.Text = PendingRequest.ToString();

        }
        catch (Exception NumberOfUnAssignedRequests_Exp)
        {
            Response.Redirect("error.aspx?error=" + NumberOfUnAssignedRequests_Exp.Message);
        }
    }

    protected void NumberOfDueTodayRequests()
    {

        //=====================================================//
        /// <summary>
        /// Description: Assign no of unassinged requests
        /// Author: Olivery
        /// Date :9/19/2007 12:07:38 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        int DueTodayRequest = 0;

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(id) FROM t_requests WHERE status_id<>5 AND (datediff(day,getdate(),created_date))<-2");
            while (reader.Read())
            {
                DueTodayRequest = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            lbtnRequestOverDueCount.Text = DueTodayRequest.ToString();

        }
        catch (Exception NumberOfUnAssignedRequests_Exp)
        {
            Response.Redirect("error.aspx?error=" + NumberOfUnAssignedRequests_Exp.Message);
        }

    }
    protected void NewRequest(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to assign the language of the session of new request page
        /// Author: mutawakelm
        /// Date :02/05/2008 09:29:35
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            //Response.Write(this.languageLabel.Text.ToString());
            Session["Language"] = "en-US";
            Response.Redirect("./frmNewRequest.aspx");


        }
        catch (Exception NewRequest_Exp)
        {

        }
    }
    protected void mCheckAdminAccess()
    {
        try
        {
            if ((Session["UserID"].ToString() == "hadwera") || (Session["UserID"].ToString() == "mutawakelm") || (Session["UserID"].ToString() == "meguida"))
                lnkReport.Visible = true;
        }
        catch (Exception exp)
        {
        }
    }

   

}

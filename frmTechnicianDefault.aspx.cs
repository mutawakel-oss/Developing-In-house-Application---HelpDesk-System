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

public partial class frmTechnicianDefault : System.Web.UI.Page
{
    OdbcDataReader reader;
    string strLoginID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        HyperLink LB1 = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired");
        }
        LB1.Text = "Log Out";
        if (null != Session["UserID"] && Session["UserID"].ToString()!="")
            strLoginID = Session["UserID"].ToString();
        
        AssignedTaskCount(); // To Display no of assigned tasks
        TaskOverDueCount();// to display no of overdue task
        
        LoggedAs();
        mCheckAdminAccess();
    }


    protected void DisplayTaskList(object sender, EventArgs e)
    {
        
        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :9/22/2007 7:46:19 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if(null!=Session["UserID"])

            HttpContext.Current.Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
        }
        catch (Exception DisplayTaskList_Exp)
        {

        }    
    
    }

    protected void DisplayOverDueTaskList(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :9/22/2007 7:46:19 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (null != Session["UserID"])

                HttpContext.Current.Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString()+"&duetoday=true");
        }
        catch (Exception DisplayTaskList_Exp)
        {

        }

    }
    
    
    
    protected void AssignedTaskCount()
    {    

            //=====================================================//
            /// <summary>
            /// Description:
            /// Author: Olivery
            /// Date :9/22/2007 8:15:28 AM
            /// Parameter:
            /// input:
            /// output:
            /// Example:
            /// <summary>
            //=====================================================//
            int TaskCount = 0;
            
            try
            {
                reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(r.id) FROM t_requests r,t_category ct,t_status st,t_users tu,t_assignment a WHERE r.category_id = ct.id AND r.status_id = st.id AND r.created_by=tu.id and r.id=a.request_id and a.assigned_to='" + strLoginID + "'and r.status_id<>5 and r.status_id!=6");
                while (reader.Read())
                {
                    TaskCount = Convert.ToInt32(reader[0]);                 
                }
                reader.Close();
                lbtnAssignedTaskCount.Text = TaskCount.ToString();

            }
            catch (Exception AssignedTaskCount_Exp)
            {
                Response.Redirect("error.aspx?error=" + AssignedTaskCount_Exp.Message);
            }      
    }

    protected void TaskOverDueCount()
    {

        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :9/22/2007 8:15:28 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        int TaskCount = 0;

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(r.id) FROM t_requests r,t_category ct,t_status st,t_users tu,t_assignment a WHERE r.category_id = ct.id AND r.status_id = st.id AND r.created_by=tu.id and r.id=a.request_id and a.assigned_to='" + strLoginID + "'and r.status_id<>5 and (datediff(day,getdate(),r.created_date))<-2");
            while (reader.Read())
            {
                TaskCount = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            lbtnRequestOverDueCount.Text = TaskCount.ToString();

        }
        catch (Exception AssignedTaskCount_Exp)
        {
            Response.Redirect("error.aspx?error=" + AssignedTaskCount_Exp.Message);
        }
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

        if (null != Session["UserFullName"] && Session["UserFullName"].ToString() != "")
            lblLogUser.Text = Session["UserFullName"].ToString();
        if (null != Session["Badge"] && Session["Badge"].ToString() != "")
            lblBadgeNo.Text = Session["Badge"].ToString();
        if (null != Session["Title"] && Session["Title"].ToString() != "")
            lblTitle.Text = Session["Title"].ToString();
        if (null != Session["Department"] && Session["Department"].ToString() != "")
            lblDepartment.Text = Session["Department"].ToString(); 

        Label LB = (Label)this.Master.Page.Controls[0].Controls[3].Controls[9].Controls[1];
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

        if (null != Session["UserID"] && Session["UserID"].ToString() != "")
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmTechnicianDefault.aspx");
    }
    protected void mInventoryImageClicked(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/frmInvProductList.aspx");
        }
        catch (Exception exp)
        {
        }
    }
    protected void mCheckAdminAccess()
    {
        try
        {
            //meguida
            if (Session["UserID"].ToString() == "meguida")
                lnkAdminPage.Visible = true;
        }
        catch (Exception exp)
        {
        }
    }
    protected void mAdminPageClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used when the admin page link clicked
        /// to access the admin pages account.
        /// Author: mutawakelm
        /// Date :1/21/2009 9:17:17 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            Session["ITMember"] = "false";
            Session["ITMember"] = "true";
            Response.Redirect("frmAdminDefault.aspx");


        }
        catch (Exception exp)
        {
        }
    }
    
}

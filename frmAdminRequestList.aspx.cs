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

public partial class Default2 : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            LoadUnAssignFilter();
            if ((null != Request.QueryString["pending"]) && (Request.QueryString["pending"].ToString() == "true"))
            {
                ddlFilterUnassign.SelectedValue = "4";
                PendingRequests();
            }
            else
            {
                if ((null != Request.QueryString["dueToday"]) && (Request.QueryString["dueToday"].ToString() == "true"))
                    //UnAssignedDueTodayRequest();
                    RequestsDueList();
                else
                {
                    Session["Unassigned"] = "true";
                    DisplayUnAssignedRequest();
                }
            }
        }
        
        LoggedAs();

     
       
    }
    
    protected void DisplayUnAssignedRequest()
    {
        /// <summary>
        /// 	Description: Populate unassigned requests from t_requests into gridview  
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: Requestlist in the gridview
        /// 	Example:  DisplayUnAssignedRequest()
        /// </summary>
        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSetGridView("SELECT t.id,ct.category,tu.full_name,t.created_date,t.priority FROM t_requests t,t_category ct,t_status st,t_users tu WHERE t.category_id = ct.id AND t.status_id = st.id AND t.created_by=tu.id AND t.id not in (select request_id from t_assignment) ORDER BY t.id DESC", "t_requests");
            //ds = GeneralClass.Program.gDataSetGridView("SELECT t.id,ct.category,tu.full_name,t.created_date,t.priority FROM t_requests t,t_category ct,t_status st,t_users tu WHERE t.category_id = ct.id AND t.status_id = st.id AND t.created_by=tu.id AND t.status_id= 1 ORDER BY id DESC", "t_requests");
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            
            gdvUnAssignedRequests.DataSource = ds;
            gdvUnAssignedRequests.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }           

    protected void LoadUnAssignFilter()
    {
        /// <summary>
        /// 	Description: Load status for filterig into dropdown list  
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadFilterBy();
        /// </summary>

        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSet("t_Status", "*", "");

            ddlFilterUnassign.DataValueField = "id";
            ddlFilterUnassign.DataTextField = "status";

            ddlFilterUnassign.DataSource = ds;
            ddlFilterUnassign.DataBind();
            ddlFilterUnassign.SelectedIndex = 1;

            ListItem li = new ListItem();
            li.Value = "99";
            li.Text = "Overdue Requests";
            ddlFilterUnassign.Items.Add(li);

        }
        catch (Exception ex)
        {
            
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }


    protected void LoadAssignFilter()
    {
        /// <summary>
        /// 	Description: Load status for filterig into dropdown list  
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadFilterBy();
        /// </summary>

        try
        {
            //DataSet ds;
            //ds = GeneralClass.Program.gDataSet("t_Status", "*", "");

            //ddlFilterAssign.DataValueField = "id";
            //ddlFilterAssign.DataTextField = "status";

            //ddlFilterAssign.DataSource = ds;
            //ddlFilterAssign.DataBind();

        }
        catch (Exception ex)
        {
            
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void ddlFilterUnassign_SelectedIndexChanged(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Load status for filterig into dropdown list  
        ///	
        ///
        /// 	Date:19/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadFilterBy();
        /// </summary>

        try
        {
            int filter = Convert.ToInt32(ddlFilterUnassign.SelectedValue);
            
            if (filter == 1)
            {
                Session["Unassigned"] = "true";
                gdvAssignedRequests.Enabled= false;
                gdvAssignedRequests.Visible = false;                

                DataSet ds;
                ds = GeneralClass.Program.gDataSetGridView("SELECT t.id,ct.category,tu.full_name,t.created_date,t.priority FROM t_requests t,t_category ct,t_status st,t_users tu WHERE t.category_id = ct.id AND t.status_id = st.id AND t.created_by=tu.id AND t.status_id=" + ddlFilterUnassign.SelectedValue + " ORDER BY id DESC", "t_requests");
                gdvUnAssignedRequests.DataSource = ds;
                if (ds.Tables[0].Rows.Count == 0)
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gdvUnAssignedRequests.DataBind();
                gdvUnAssignedRequests.Enabled = true;
                gdvUnAssignedRequests.Visible = true;
            }
            else
            {
                if (filter == 99)
                    RequestsDueList();
                else
                {
                    Session["Unassigned"] = "false";
                    gdvUnAssignedRequests.Enabled = false;
                    gdvUnAssignedRequests.Visible = false;

                    DataSet ds;
                    //  ds = GeneralClass.Program.gDataSetGridView("SELECT r.id,ct.category,u.full_name assignedto,s.status, u1.full_name,r.created_date FROM t_requests r,t_assignment a, t_users u, t_users u1,t_category ct,t_status s WHERE r.id=a.request_id and a.assigned_to = u.id and r.category_id = ct.id and r.created_by = u1.id and r.status_id=s.id and r.status_id=" + ddlFilterUnassign.SelectedValue + " ORDER BY r.id DESC", "t_requests");

                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_admin_requestlist.*,'assignedto' = CASE WHEN vw_assignment.assigned_to IS NULL then 'None' ELSE vw_assignment.full_name END FROM vw_admin_requestlist LEFT OUTER JOIN vw_assignment ON vw_admin_requestlist.id = vw_assignment.request_id WHERE vw_admin_requestlist.status_id=" + ddlFilterUnassign.SelectedValue + " ORDER BY vw_admin_requestlist.id DESC", "t_requests");
                    gdvAssignedRequests.DataSource = ds;
                    if (ds.Tables[0].Rows.Count == 0)
                        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gdvAssignedRequests.DataBind();
                    gdvAssignedRequests.Enabled = true;
                    gdvAssignedRequests.Visible = true;
                }
            }        
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void gdvUnAssignedRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        /// <summary>
        /// 	Description: handles page index changing of the Gridview  
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSetGridView("SELECT t.id,ct.category,tu.full_name,t.created_date,t.priority FROM t_requests t,t_category ct,t_status st,t_users tu WHERE t.category_id = ct.id AND t.status_id = st.id AND t.created_by=tu.id AND t.status_id=" + ddlFilterUnassign.SelectedValue + " ORDER BY id DESC", "t_requests");
            gdvUnAssignedRequests.DataSource = ds;
            gdvUnAssignedRequests.PageIndex = e.NewPageIndex;
            gdvUnAssignedRequests.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    
    }
    protected void gdvAssignedRequests_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        /// <summary>
        /// 	Description: handles page index changing of the Gridview  
        ///	
        ///
        /// 	Date:9th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        DataSet ds;
        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_admin_requestlist.*,'assignedto' = CASE WHEN vw_assignment.assigned_to IS NULL then 'None' ELSE vw_assignment.full_name END FROM vw_admin_requestlist LEFT OUTER JOIN vw_assignment ON vw_admin_requestlist.id = vw_assignment.request_id WHERE vw_admin_requestlist.status_id=" + ddlFilterUnassign.SelectedValue + " ORDER BY vw_admin_requestlist.id DESC", "t_requests");
        gdvAssignedRequests.DataSource = ds;
        if (ds.Tables[0].Rows.Count == 0)
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        gdvAssignedRequests.PageIndex = e.NewPageIndex;
        gdvAssignedRequests.DataBind();
        gdvAssignedRequests.Enabled = true;
        gdvAssignedRequests.Visible = true;
    
    
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
            { LB.Text = "Local Admin";
            Hlk.NavigateUrl = "frmAdminDefault.aspx";
            }
            else
                if (null != Session["Technician"] && Session["Technician"].ToString() == "true")
                { LB.Text = "PC Technician";
                //Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
                Hlk.NavigateUrl = "frmTechnicianDefault.aspx";
                }
                else
                    if (null != Session["ITMember"] && Session["ITMember"].ToString() == "true")
                    {
                        LB.Text = "User";
                        Hlk.NavigateUrl = "frmAdminDefault.aspx";
                    }
                else
                { LB.Text = "User";
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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmAdminRequestList.aspx");
    }

    protected void PendingRequests()
    {
        /// <summary>
        /// 	Description: list of pending requests exclusively used when navigaed from admin home by clicking pending requests link  
        ///	
        ///
        /// 	Date:19/sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadFilterBy();
        /// </summary>

        try
        {
            int filter = Convert.ToInt32(ddlFilterUnassign.SelectedValue);
                        
                Session["Unassigned"] = "false";
                gdvUnAssignedRequests.Enabled = false;
                gdvUnAssignedRequests.Visible = false;

                DataSet ds;
                //  ds = GeneralClass.Program.gDataSetGridView("SELECT r.id,ct.category,u.full_name assignedto,s.status, u1.full_name,r.created_date FROM t_requests r,t_assignment a, t_users u, t_users u1,t_category ct,t_status s WHERE r.id=a.request_id and a.assigned_to = u.id and r.category_id = ct.id and r.created_by = u1.id and r.status_id=s.id and r.status_id=" + ddlFilterUnassign.SelectedValue + " ORDER BY r.id DESC", "t_requests");

                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_admin_requestlist.*,'assignedto' = CASE WHEN vw_assignment.assigned_to IS NULL then 'None' ELSE vw_assignment.full_name END FROM vw_admin_requestlist LEFT OUTER JOIN vw_assignment ON vw_admin_requestlist.id = vw_assignment.request_id WHERE vw_admin_requestlist.status_id= 4 ORDER BY vw_admin_requestlist.id DESC", "t_requests");
                gdvAssignedRequests.DataSource = ds;
                if (ds.Tables[0].Rows.Count == 0)
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gdvAssignedRequests.DataBind();
                gdvAssignedRequests.Enabled = true;
                gdvAssignedRequests.Visible = true;
           }
        
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void UnAssignedDueTodayRequest()
    {
        /// <summary>
        /// 	Description: list of unassinged and created_date is older than 2 days requests
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: Requestlist in the gridview
        /// 	Example:  DisplayUnAssignedRequest()
        /// </summary>
        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSetGridView("SELECT t.id,ct.category,tu.full_name,t.created_date,t.priority FROM t_requests t,t_category ct,t_status st,t_users tu WHERE t.category_id = ct.id AND t.status_id = st.id AND t.created_by=tu.id AND t.id not in (select request_id from t_assignment) and (datediff(day,getdate(),created_date))<-2 ORDER BY t.id DESC", "t_requests");
            //ds = GeneralClass.Program.gDataSetGridView("SELECT t.id,ct.category,tu.full_name,t.created_date,t.priority FROM t_requests t,t_category ct,t_status st,t_users tu WHERE t.category_id = ct.id AND t.status_id = st.id AND t.created_by=tu.id AND t.status_id= 1 ORDER BY id DESC", "t_requests");
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            gdvUnAssignedRequests.DataSource = ds;
            gdvUnAssignedRequests.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void RequestsDueList()
    {
        /// <summary>
        /// 	Description: list of pending requests exclusively used when navigaed from admin home by clicking pending requests link  
        ///	
        ///
        /// 	Date:19/sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadFilterBy();
        /// </summary>

        try
        {
           // int filter = Convert.ToInt32(ddlFilterUnassign.SelectedValue);

            Session["Unassigned"] = "false";
            gdvUnAssignedRequests.Enabled = false;
            gdvUnAssignedRequests.Visible = false;
            ddlFilterUnassign.SelectedValue = "99";

            DataSet ds;
            //  ds = GeneralClass.Program.gDataSetGridView("SELECT r.id,ct.category,u.full_name assignedto,s.status, u1.full_name,r.created_date FROM t_requests r,t_assignment a, t_users u, t_users u1,t_category ct,t_status s WHERE r.id=a.request_id and a.assigned_to = u.id and r.category_id = ct.id and r.created_by = u1.id and r.status_id=s.id and r.status_id=" + ddlFilterUnassign.SelectedValue + " ORDER BY r.id DESC", "t_requests");

            ds = GeneralClass.Program.gDataSetGridView("SELECT vw_admin_requestlist.*,'assignedto' = CASE WHEN vw_assignment.assigned_to IS NULL then 'None' ELSE vw_assignment.full_name END FROM vw_admin_requestlist LEFT OUTER JOIN vw_assignment ON vw_admin_requestlist.id = vw_assignment.request_id WHERE vw_admin_requestlist.status_id<> 5 and (datediff(day,getdate(),vw_admin_requestlist.created_date))<-2 ORDER BY vw_admin_requestlist.id DESC", "t_requests");
            gdvAssignedRequests.DataSource = ds;
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gdvAssignedRequests.DataBind();
            gdvAssignedRequests.Enabled = true;
            gdvAssignedRequests.Visible = true;
        }

        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
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

    

}

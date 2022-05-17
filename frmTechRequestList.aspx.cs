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

public partial class frmTechRequestList : System.Web.UI.Page
{
    string Userid;
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
            LoadStatusFilter();
            Userid = Request.QueryString["id"].ToString();

            if (null != Request.QueryString["duetoday"] && Request.QueryString["duetoday"].ToString() == "true")
                overDueTaskList(); // list all the overdue tasks
            else            
                DisplayAssignedRequest();           
        }
        LoggedAs();
    }


    protected void DisplayAssignedRequest()
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
            int totalTasksNo = 0;
            ds = GeneralClass.Program.gDataSetGridView("SELECT r.id,tu.full_name,ct.category,r.priority,r.description,r.created_date,a.assigned_date FROM t_requests r,t_category ct,t_status st,t_users tu,t_assignment a WHERE r.category_id = ct.id AND r.status_id = st.id AND r.created_by=tu.id and r.id=a.request_id and a.assigned_to='" + Request.QueryString["id"] + "' and r.status_id=" + ddlStatusFilter.SelectedValue + "ORDER BY id DESC", "t_requests");
            if (ds.Tables[0].Rows.Count == 0)
            {
                totalTasksNo = ds.Tables[0].Rows.Count;
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            else
                totalTasksNo = ds.Tables[0].Rows.Count;
            
            gdvTechRequests.DataSource = ds;
            gdvTechRequests.DataBind();
            lblTaskNo.Text = totalTasksNo.ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }


    protected void LoadStatusFilter()
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

            ddlStatusFilter.DataValueField = "id";
            ddlStatusFilter.DataTextField = "status";

            ddlStatusFilter.DataSource = ds;
            ddlStatusFilter.DataBind();

            ddlStatusFilter.SelectedValue = "2";

            ListItem li = new ListItem();
            li.Value = "99";
            li.Text = "Overdue Tasks";
            ddlStatusFilter.Items.Add(li);

        }
        catch (Exception ex)
        {
           
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }

    protected void btnSearchClicked(object sender, EventArgs e)
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
            DateTime endingDate = new DateTime();
            if (ddlStatusFilter.SelectedValue == "99") // for listing of overdue tasks
            {
                overDueTaskList();
                
            }
            else
            {
                DataSet ds;
                int totalRowsNo = 0;
                if ((txtStartDate.Text == "") && (txtEndDate.Text == ""))
                {
                    string strSqlQuery = "SELECT r.id,tu.full_name,ct.category,r.priority,r.description,r.created_date,a.assigned_date FROM t_requests r,t_category ct,t_status st,t_users tu,t_assignment a WHERE r.category_id = ct.id AND r.status_id = st.id AND r.created_by=tu.id and r.id=a.request_id and a.assigned_to='" + Request.QueryString["id"] + "' and r.status_id=" + ddlStatusFilter.SelectedValue + " ORDER BY id DESC";
                    ds = GeneralClass.Program.gDataSetGridView(strSqlQuery, "t_requests");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        totalRowsNo = ds.Tables[0].Rows.Count;
                        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    }
                    else
                        totalRowsNo = ds.Tables[0].Rows.Count;
                    gdvTechRequests.DataSource = ds;
                    gdvTechRequests.DataBind();
                    lblTaskNo.Text = totalRowsNo.ToString();
                }
                else
                    if ((txtStartDate.Text != "") && (txtEndDate.Text != ""))
                    {
                        string[] endingDateSplitter = txtEndDate.Text.ToString().Split('-');

                        endingDate = new DateTime(int.Parse(endingDateSplitter[0]), int.Parse(endingDateSplitter[1]), int.Parse(endingDateSplitter[2]));
                        endingDate = endingDate.AddDays(1);
                        string strSqlQuery = "SELECT r.id,tu.full_name,ct.category,r.priority,r.description,r.created_date,a.assigned_date FROM t_requests r,t_category ct,t_status st,t_users tu,t_assignment a WHERE r.category_id = ct.id AND r.status_id = st.id AND r.created_by=tu.id and r.id=a.request_id and a.assigned_to='" + Request.QueryString["id"] + "' and r.status_id=" + ddlStatusFilter.SelectedValue + "AND a.assigned_date>='" + txtStartDate.Text + "' AND a.assigned_date<='" + endingDate.Year.ToString() + "-" + endingDate.Month.ToString() + "-" + endingDate.Day.ToString() + "' ORDER BY id DESC";
                        ds = GeneralClass.Program.gDataSetGridView(strSqlQuery, "t_requests");
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            totalRowsNo = ds.Tables[0].Rows.Count;
                            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                        }
                        else
                            totalRowsNo = ds.Tables[0].Rows.Count;
                        gdvTechRequests.DataSource = ds;
                        gdvTechRequests.DataBind();
                        lblTaskNo.Text = totalRowsNo.ToString();
                    }
                    else
                    {
                        gdvTechRequests.DataSource = null;
                        gdvTechRequests.DataBind();
                        lblTaskNo.Text = "0";
                    }
            }
            
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    
    }

    protected void gdvTechRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        /// <summary>
        /// 	Description: Populate unassigned requests from t_requests into gridview  
        ///	
        ///
        /// 	Date:20/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: Requestlist in the gridview
        /// 	Example:  DisplayUnAssignedRequest()
        /// </summary>
        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSetGridView("SELECT r.id,tu.full_name,ct.category,r.priority,r.description,r.created_date,a.assigned_date FROM t_requests r,t_category ct,t_status st,t_users tu,t_assignment a WHERE r.category_id = ct.id AND r.status_id = st.id AND r.created_by=tu.id and r.id=a.request_id and a.assigned_to='" + Request.QueryString["id"] + "' and r.status_id=" + ddlStatusFilter.SelectedValue + "ORDER BY id DESC", "t_requests");
            gdvTechRequests.DataSource = ds;
            gdvTechRequests.PageIndex = e.NewPageIndex;
            gdvTechRequests.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    
    }

    protected void overDueTaskList()
    {
        /// <summary>
        /// 	Description: Populate unassigned requests from t_requests into gridview  
        ///	
        ///
        /// 	Date:20/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: Requestlist in the gridview
        /// 	Example:  DisplayUnAssignedRequest()
        /// </summary>

        ddlStatusFilter.SelectedValue = "99";


        try
        {
            DataSet ds;
            int totalTasksNo = 0;
            ds = GeneralClass.Program.gDataSetGridView("SELECT r.id,tu.full_name,ct.category,r.priority,r.description,r.created_date,a.assigned_date FROM t_requests r,t_category ct,t_status st,t_users tu,t_assignment a WHERE r.category_id = ct.id AND r.status_id = st.id AND r.created_by=tu.id and r.id=a.request_id and a.assigned_to='" + Request.QueryString["id"] + "' and r.status_id<>5 AND DATEDIFF(DAY,r.created_date,GETDATE()) > 2 ORDER BY id DESC", "t_requests");
            if (ds.Tables[0].Rows.Count == 0)
            {
                totalTasksNo = ds.Tables[0].Rows.Count;
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            else
                totalTasksNo = ds.Tables[0].Rows.Count;
            gdvTechRequests.DataSource = ds;
            gdvTechRequests.DataBind();
            lblTaskNo.Text = totalTasksNo.ToString();
            //gdvTechRequests.HeaderStyle.Height = Unit.Pixel(1);
            //gdvTechRequests.RowStyle.Height = Unit.Pixel(1);
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
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


        if (null != Session["UserID"] && Session["UserID"].ToString() != "")
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
                                                                                    
                                                         
    }
    protected void mClearDates(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to clear the dates text boxes
        /// Author: mutawakelm
        /// Date :3/8/2009 8:48:49 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
        }
        catch (Exception exp)
        {
        }
    }

}

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
            
           
        }
        
        LoggedAs();

     
       
    }
   
    protected void LoggedAs()
    {
        /// <summary>
        /// 	Description: show the user group of the logged in user
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Muta
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
                        LB.Text = "IT Member";
                        //Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
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

    protected void mSearchLog(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: This function will be used to fill the datagrid of the log according to the selected date.
        ///	
        ///
        /// 	Date:19/sept/2007
        /// 	Author:Muta
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadFilterBy();
        /// </summary>

        try
        {
                
                //gdvUnAssignedRequests.Enabled = false;
                //gdvUnAssignedRequests.Visible = false;

                DataSet ds;
                //  ds = GeneralClass.Program.gDataSetGridView("SELECT r.id,ct.category,u.full_name assignedto,s.status, u1.full_name,r.created_date FROM t_requests r,t_assignment a, t_users u, t_users u1,t_category ct,t_status s WHERE r.id=a.request_id and a.assigned_to = u.id and r.category_id = ct.id and r.created_by = u1.id and r.status_id=s.id and r.status_id=" + ddlFilterUnassign.SelectedValue + " ORDER BY r.id DESC", "t_requests");
                
                string strQuery = "SELECT id,done_by,affected_by,previous_status,new_status,date FROM t_technician_status_log WHERE date>='" +txtDate.Text+ " 00:00:00' and date<='" + txtDate.Text+ " 23:59:59'";
                ds = GeneralClass.Program.gDataSetGridView(strQuery, "t_technician_status_log");
                logGrid.DataSource = ds;
                if (ds.Tables[0].Rows.Count == 0)
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                logGrid.DataBind();
                //gdvAssignedRequests.Enabled = true;
                //gdvAssignedRequests.Visible = true;
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

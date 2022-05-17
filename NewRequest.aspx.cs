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
using GeneralClass;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Session["UserFullName"])
            {
                txtFullname.Text = Session["UserFullName"].ToString();
            }
            LoadCategory();
            LoadFilterBy();
            DisplayRequestList();
        }
    }

    protected void LoadCategory()
    {
        /// <summary>
        /// 	Description: Load categorie into drop list  
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadCategory();
        /// </summary>
        
       try{
            DataSet ds;
            ds = GeneralClass.Program.gDataSet("t_category", "*","");

            ddlCategory.DataValueField = "id";
            ddlCategory.DataTextField = "category";
            ddlCategory.DataSource = ds;
            ddlCategory.DataBind();
        
    }
        catch(Exception ex)
       {
          
            Response.Redirect("error.aspx?error="+ ex.Message );            
      }
    
    }

    protected void LoadFilterBy()
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
            //ds = GeneralClass.Program.gDataSet("t_Status", "*", "");

            // ddlFilter.DataValueField = "id";
            // ddlFilter.DataTextField = "status";

            // ddlFilter.DataSource = ds;
            // ddlFilter.DataBind();

        }
        catch (Exception ex)
        {
           
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }

    protected void SaveNewRequest(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save new request into t_requests table
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:user details specified in the textbox
        ///		output:
        /// 	Example:  SaveUserDetails()
        /// </summary>

        HttpContext.Current.Response.Redirect("~/Login.aspx");
        try
        {
            //string createby = Session["UserID"].ToString();
            
            //GeneralClass.Program.Add("category_id",ddlCategory.SelectedValue,"I");
            //GeneralClass.Program.Add("priority",ddlPriority.SelectedItem.Text,"S");
            //GeneralClass.Program.Add("Status_id", "1", "I");
            //GeneralClass.Program.Add("Description",txtDescription.Text,"S");
            //GeneralClass.Program.Add("created_by", Session["UserID"].ToString(), "S");
            
            //int intReturnID = GeneralClass.Program.InsertRecordStatement("t_requests");

            //if (intReturnID > 0)
            //    HttpContext.Current.Response.Redirect("Logn.aspx");
            //else
            //    Response.Redirect("Error.aspx?error=There is ");
        }
        catch (Exception ex)
        {
            Response.Redirect("Error.aspx?error="+ex.Message.ToString());
        }    
    
    }

    protected void DisplayRequestList()
    {
        /// <summary>
        /// 	Description: Populate requests from t_requests into gridview  
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: Requestlist in the gridview
        /// 	Example:  DisplayRequestList();
        /// </summary>
        try
        {
            int userid = Convert.ToInt32(Session["UserID"]);
                    
            DataSet ds;
            ds = GeneralClass.Program.gDataSetGridView("select t.id,ct.category,st.status,t.created_date from t_requests t,t_category ct,t_status st where t.category_id = ct.id and t.status_id = st.id and created_by=" + userid + " order by id desc", "t_requests");
            //gdvRequestList.DataSource = ds;
            //gdvRequestList.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmAdminDefault.aspx");
    }
}

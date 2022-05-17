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
public partial class frmInvProductList : System.Web.UI.Page
{
    OdbcDataReader reader;   

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)

                ViewState["sortDirection"] = SortDirection.Descending;

            return (SortDirection)ViewState["sortDirection"];
        }

        set { ViewState["sortDirection"] = value; }
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired"); 
            
        }
        LoggedAs();
       
        if (!IsPostBack)
        {
            ListItem l1 = new ListItem();
            l1.Value = "0";
            l1.Text = " All ";
            ddlProduct.Items.Add(l1);
            LoadProductList();
          
            LoadSubCategory();
            LoadAllocatedUsers();
            
        }
    }
    
    protected void LoadProductList()
    {
        /// <summary>
        /// 	Description: Populate data from t_invProductdetails into gridview  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example: 
        /// </summary>
        try
        {
            DataSet ds;
            //ds = GeneralClass.Program.gDataSet("t_invproductdetails", "*", " ORDER BY vendor_serial_no");

            //ds = GeneralClass.Program.gDataSet("vw_productdetails", "*", " ORDER BY vendor_serial_no");

           // ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
           ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");

           
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            
            gdvInvProductList.DataSource = ds;
            gdvInvProductList.DataBind();

            GeneralClass.Program.sortDS = ds; // used for sorting
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }       
    }

    protected void ddlAllocated_SelectdIndexChanged(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Populate data from t_invProductdetails into gridview based on the filering 
        ///	
        ///
        /// 	Date:4t/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

       txtMissAssetNo.Text = "";
       ddlAllocatedUser.SelectedIndex = 0;
        try
        {
             DataSet ds;
           //  if (ddlCatgory.SelectedIndex == 0)
           //  {
           //      if (ddlAllocated.SelectedValue == "2")   // 2--> All
           //          //ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_productdetails");
           //          ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
           //      else
           //          ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=" + ddlAllocated.SelectedValue, "vw_productdetails");

           //  }
           // else

           //ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
             //if (ddlProduct.Text == "")
             //    ddlProduct.Items.Add("ALL");

             if (ddlCatgory.SelectedIndex != 0)
             {
                 if (ddlAllocated.SelectedValue == "2")
                     ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                 else
                     ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
             }
             else if (ddlProduct.SelectedIndex != 0)
             {
                 if (ddlAllocated.SelectedValue == "2")
                     ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                 else
                     ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
             }
             else if (ddlAllocated.SelectedValue == "2") // ALL
                 ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
             else
                   ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=" + ddlAllocated.SelectedValue, "vw_productdetails");
           
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            if (ds.Tables[0].Rows.Count != 0 && (ddlCatgory.SelectedIndex != 0 || ddlProduct.SelectedIndex != 0 || txtMissAssetNo.Text != "" || ddlAllocatedUser.SelectedIndex != 0))
            {
                lblAvailable.Visible = true;
                lblItem.Visible = true;
                lblAvailable.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblAvailable.Visible = false;
                lblItem.Visible = false;
            }
                    gdvInvProductList.DataSource = ds;
            gdvInvProductList.DataBind();
            GeneralClass.Program.sortDS = ds; // used for sorting
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }    
    }

    protected void gdvInvProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        /// <summary>
        /// 	Description: Populate data from t_invProductdetails into gridview based on the filering 
        ///	
        ///
        /// 	Date:4t/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        try
        {
            DataSet ds;
          //  if (ddlCatgory.SelectedIndex == 0)
          //   {
          //  if (ddlAllocated.SelectedValue == "2")
          //      //ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_productdetails");
          //      ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
          //  else
          //      ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=" + ddlAllocated.SelectedValue, "vw_productdetails");
          //  }
          //else
            
          //      ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
            if (ddlCatgory.SelectedIndex != 0)
            {
                if (ddlAllocatedUser.SelectedIndex != 0)
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "' AND category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                else
                {
                    if (ddlAllocated.SelectedValue == "2")
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                    else
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                }
            }
            else if (ddlProduct.SelectedIndex != 0)
            {
                if (ddlAllocatedUser.SelectedIndex != 0)
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "' AND Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                else
                {
                    if (ddlAllocated.SelectedValue == "2")
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                    else
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                }
            }
            else if (txtMissAssetNo.Text != "")
                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE asset_number LIKE'" + txtMissAssetNo.Text.Trim() + "'", "vw_ProductDetails");
            
            else if (ddlCatgory.SelectedIndex == 0 && ddlProduct.SelectedIndex == 0 && ddlAllocatedUser.SelectedIndex!=0)
                     ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "'", "vw_ProductDetails");
            else if (ddlAllocated.SelectedValue == "2") // ALL
                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
            else
                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=" + ddlAllocated.SelectedValue, "vw_productdetails");
 
            
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            gdvInvProductList.PageIndex = e.NewPageIndex;
            if (ds.Tables[0].Rows.Count != 0 && (ddlCatgory.SelectedIndex != 0 || ddlProduct.SelectedIndex != 0 || txtMissAssetNo.Text != "" || ddlAllocatedUser.SelectedIndex != 0))
            {
                lblAvailable.Visible = true;
                lblItem.Visible = true;
                lblAvailable.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblItem.Visible = false;
                lblAvailable.Visible = false;
            }

            if (null != Session["SORTED"] && Session["SORTED"].ToString() == "TRUE")
            {
                string sortExpress = "", sortDirect = "";
                if (null != Session["SORTEXPRESSION"] && Session["SORTEXPRESSION"].ToString() != "")
                    sortExpress = Session["SORTEXPRESSION"].ToString();
                if (null != Session["SORTDIRECTION"] && Session["SORTDIRECTION"].ToString() != "")
                    sortDirect = Session["SORTDIRECTION"].ToString();

                if (sortDirect != "" && sortExpress != "")
                    SortGridView(sortExpress, sortDirect);
                else
                {
                    gdvInvProductList.DataSource = ds;
                    gdvInvProductList.DataBind();
                }
            }
            else
            {
                gdvInvProductList.DataSource = ds;
                gdvInvProductList.DataBind();
            }
            GeneralClass.Program.sortDS = ds; // used for sorting
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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmInvProductList.aspx");
    }


    protected void SearchInProductList(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Populate data from t_invProductdetails into gridview  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example: 
        /// </summary>

        ddlAllocatedUser.SelectedIndex = 0;
        try
        {
            DataSet ds;
           

         
            if (txtMissAssetNo.Text != "")
            {
                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE asset_number LIKE'" + txtMissAssetNo.Text.Trim() + "'", "vw_ProductDetails");
            }
            else
            {
                if (ddlCatgory.SelectedIndex != 0)
                {
                    if (ddlAllocated.SelectedValue == "2")
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                    else
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                }
                else if (ddlProduct.SelectedIndex != 0)
                {
                    if (ddlAllocated.SelectedValue == "2")
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                    else
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                }

                else if (ddlAllocated.SelectedValue == "2") // ALL
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
                else
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=" + ddlAllocated.SelectedValue, "vw_productdetails");
            }
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            if (ds.Tables[0].Rows.Count != 0 && (ddlCatgory.SelectedIndex != 0 || ddlProduct.SelectedIndex != 0 || txtMissAssetNo.Text != "" || ddlAllocatedUser.SelectedIndex != 0))
            {
                lblAvailable.Visible = true;
                lblItem.Visible = true;
                lblAvailable.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblItem.Visible = false;
                lblAvailable.Visible = false;
            }

            gdvInvProductList.DataSource = ds;
            gdvInvProductList.DataBind();
            GeneralClass.Program.sortDS = ds; // used for sorting
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
    protected void mSearch(object sender, EventArgs e)
    {
        try
        {

            if (txtSerialNo.Text != "")
            {
                DataSet ds;//This data set will hold the result of serch by using serial number
                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE vendor_serial_no LIKE'" + txtSerialNo.Text.Trim() + "'", "vw_ProductDetails");
                gdvInvProductList.DataSource = ds;
                gdvInvProductList.DataBind();
                GeneralClass.Program.sortDS = ds; // used for sorting

            }
            else
            {
                if (txtMissAssetNo.Text != "")
                {
                    DataSet ds;



                    if (txtMissAssetNo.Text != "")
                    {
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE asset_number LIKE'" + txtMissAssetNo.Text.Trim() + "'", "vw_ProductDetails");
                    }
                    else
                    {
                        if (ddlCatgory.SelectedIndex != 0)
                        {
                            if (ddlAllocated.SelectedValue == "2")
                                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                            else
                                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                        }
                        else if (ddlProduct.SelectedIndex != 0)
                        {
                            if (ddlAllocated.SelectedValue == "2")
                                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                            else
                                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                        }

                        else if (ddlAllocated.SelectedValue == "2") // ALL
                            ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
                        else
                            ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=" + ddlAllocated.SelectedValue, "vw_productdetails");
                    }
                    if (ds.Tables[0].Rows.Count == 0)
                        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

                    if (ds.Tables[0].Rows.Count != 0 && (ddlCatgory.SelectedIndex != 0 || ddlProduct.SelectedIndex != 0 || txtMissAssetNo.Text != "" || ddlAllocatedUser.SelectedIndex != 0))
                    {
                        lblAvailable.Visible = true;
                        lblItem.Visible = true;
                        lblAvailable.Text = ds.Tables[0].Rows.Count.ToString();
                    }
                    else
                    {
                        lblItem.Visible = false;
                        lblAvailable.Visible = false;
                    }

                    gdvInvProductList.DataSource = ds;
                    gdvInvProductList.DataBind();
                    GeneralClass.Program.sortDS = ds; // used for sorting

                }
            }
          
           

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }

    protected void LoadSubCategory()
    {
        /// <summary>
        /// 	Description: Load Inventory categorie into drop list  
        ///	
        ///
        /// 	Date:13/Jan/2008
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        ddlCatgory.Items.Clear();

        try
        {


            ListItem l1 = new ListItem();
            l1.Value = "0";
            l1.Text = " -- All -- ";
            ddlCatgory.Items.Add(l1);

            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,category FROM t_category WHERE parent_category <>0");
            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value = reader["id"].ToString();
                li.Text = reader["category"].ToString();
                ddlCatgory.Items.Add(li);
            }
            reader.Close();

        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
    protected void LoadProduct()
    {
        /// <summary>
        /// 	Description: Load Inventory categorie into drop list  
        ///	
        ///
        /// 	Date:23/6/2008
        /// 	Author:Mutawakelm
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        ddlProduct.Items.Clear();

        try
        {

            ListItem l1 = new ListItem();
            l1.Value = "0";
            l1.Text = " All ";
            ddlProduct.Items.Clear();
            ddlProduct.Items.Add(l1);
            string strProductQuery = "SELECT DISTINCT(product_name) FROM t_InvProductDetails WHERE sub_category=" + ddlCatgory.SelectedValue.ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Text = reader["product_name"].ToString().Trim();
                ddlProduct.Items.Add(li);
            }
            reader.Close();

        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void OnCategoryChanged(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :14/01/2008 09:24:21 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            ddlProduct.SelectedValue = "0";
            txtMissAssetNo.Text = "";
            DataSet ds;
         

            if (ddlCatgory.SelectedIndex != 0)
            {
                if (ddlAllocatedUser.SelectedIndex != 0)
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "' AND category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                else
                {

                    if (ddlAllocated.SelectedValue == "2")
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                    else
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                }
              }
            else if (ddlProduct.SelectedIndex != 0)
            {
                if (ddlAllocatedUser.SelectedIndex != 0)
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "' AND Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                else
                {

                    if (ddlAllocated.SelectedValue == "2")
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                    else
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                }
              }

              else if (ddlCatgory.SelectedIndex == 0 && ddlAllocatedUser.SelectedIndex != 0)
                {
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "'", "vw_ProductDetails");
                }
              else if (ddlAllocated.SelectedValue == "2") // ALL
                  ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
              else
                  ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=" + ddlAllocated.SelectedValue, "vw_productdetails");

            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            if (ds.Tables[0].Rows.Count != 0 && (ddlCatgory.SelectedIndex != 0 || ddlProduct.SelectedIndex != 0 || txtMissAssetNo.Text != "" || ddlAllocatedUser.SelectedIndex != 0))
            {
                lblAvailable.Visible = true;
                lblItem.Visible = true;
                lblAvailable.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblItem.Visible = false;
                lblAvailable.Visible = false;
            }
            
            gdvInvProductList.DataSource = ds;
            gdvInvProductList.DataBind();
            GeneralClass.Program.sortDS = ds; // used for sorting
            //The following code will be used to fill the product drop down list
            LoadProduct();

        }
        catch (Exception OnCategoryChanged_Exp)
        {

        }
    }


    protected void onProductChanged(object sender, EventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :14/01/2008 09:25:29 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            ddlCatgory.SelectedValue = "0";
            txtMissAssetNo.Text = "";
            DataSet ds;


            if (ddlCatgory.SelectedIndex != 0)
            {
                if (ddlAllocated.SelectedValue == "2")
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                else
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
            }
            else if (ddlProduct.SelectedIndex != 0)
            {
                if (ddlAllocatedUser.SelectedIndex != 0)
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "' AND Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                else
                {
                    if (ddlAllocated.SelectedValue == "2")
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                    else
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                }
            }

            else if (ddlProduct.SelectedIndex == 0 && ddlAllocatedUser.SelectedIndex != 0)
            {
                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "'", "vw_ProductDetails");
            }

            else if (ddlAllocated.SelectedValue == "2") // ALL
                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
            else
                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=" + ddlAllocated.SelectedValue, "vw_productdetails");

            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            if (ds.Tables[0].Rows.Count != 0 && (ddlCatgory.SelectedIndex != 0 || ddlProduct.SelectedIndex != 0 || txtMissAssetNo.Text != "" || ddlAllocatedUser.SelectedIndex != 0))
            {
                lblAvailable.Visible = true;
                lblItem.Visible = true;
                lblAvailable.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblItem.Visible = false;
                lblAvailable.Visible = false;
            }


            gdvInvProductList.DataSource = ds;
            gdvInvProductList.DataBind();
            GeneralClass.Program.sortDS = ds; // used for sorting

        }
        catch (Exception onProductChanged_Exp)
        {

        }
    
    }

    protected void LoadAllocatedUsers()
    {
        /// <summary>
        /// 	Description: Load allocated users into drop list  
        ///	
        ///
        /// 	Date:28th/Jan/2008
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        ddlAllocatedUser.Items.Clear();

        try
        {

            ListItem l1 = new ListItem();
            l1.Value = "0";
            l1.Text = " All ";

            ddlAllocatedUser.Items.Add(l1);

            reader = GeneralClass.Program.gRetrieveRecord("SELECT distinct allocated_to,allocated_user from vw_assetallocation ORDER BY allocated_user");
            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value = reader["allocated_to"].ToString();
                li.Text = reader["allocated_user"].ToString().Trim();
                ddlAllocatedUser.Items.Add(li);
            }
            reader.Close();

        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
    protected void onAllocatedUserChange(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Populate data from t_invProductdetails into gridview  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example: 
        /// </summary>
        try
        {
            DataSet ds;

            if (ddlCatgory.SelectedIndex != 0)
            {
                if (ddlAllocatedUser.SelectedIndex != 0)
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "' AND category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                else
                {
                    if (ddlAllocated.SelectedValue == "2")
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                    else
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE category= '" + ddlCatgory.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");                
                }
            }
            else if (ddlProduct.SelectedIndex != 0)
            {
                if (ddlAllocatedUser.SelectedIndex != 0)
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "' AND Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                else
                {
                    if (ddlAllocated.SelectedValue == "2")
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "'", "vw_ProductDetails");
                    else
                        ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE Product_name= '" + ddlProduct.SelectedItem.Text.Trim() + "' AND assigned_yn=" + ddlAllocated.SelectedValue, "vw_ProductDetails");
                }
            }
            else if (ddlCatgory.SelectedIndex == 0 && ddlProduct.SelectedIndex == 0 && ddlAllocatedUser.SelectedIndex == 0)
            {
                if (ddlAllocated.SelectedValue == "2") // ALL
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id", "vw_ProductDetails");
                else
                    ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=" + ddlAllocated.SelectedValue, "vw_productdetails");
            
            }
            else
                ds = GeneralClass.Program.gDataSetGridView("SELECT vw_ProductDetails.*,vw_assetallocation.allocated_to,'allocated_user'=CASE WHEN vw_assetallocation.allocated_to IS NULL THEN 'Not Allocated' ElSE  vw_assetallocation.allocated_user END,'Location'= CASE WHEN allocated_bldg IS NULL THEN ' ' ELSE vw_assetallocation.allocated_bldg END FROM vw_ProductDetails LEFT OUTER JOIN vw_assetallocation ON vw_ProductDetails.id = vw_assetallocation.product_id WHERE assigned_yn=1 and allocated_to='" + ddlAllocatedUser.SelectedValue + "'", "vw_ProductDetails");
            
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            gdvInvProductList.DataSource = ds;
            gdvInvProductList.DataBind();


            if (ds.Tables[0].Rows.Count != 0 && (ddlCatgory.SelectedIndex != 0 || ddlProduct.SelectedIndex != 0 || txtMissAssetNo.Text != "" || ddlAllocatedUser.SelectedIndex != 0))
            {
                lblAvailable.Visible = true;
                lblItem.Visible = true;
                lblAvailable.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lblItem.Visible = false;
                lblAvailable.Visible = false;
            }

            GeneralClass.Program.sortDS = ds; // used for sorting
          

        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :10/03/2008 11:20:39 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string sortExpression = e.SortExpression;
            Session["SORTED"] = "TRUE";
            Session["SORTEXPRESSION"] = sortExpression;


            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, "DESC");
                Session["SORTDIRECTION"] ="DESC";
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, "ASC");
                Session["SORTDIRECTION"] = "ASC";
            } 

        }
        catch (Exception GridView_Sorting_Exp)
        {

        }
          
    }


    private void te()
    {
        //Response.Write("<script language='javascript'> alert('Session Expired');</script>");
        //Response.Redirect("redirect_2.aspx");  
        Response.Redirect("error.aspx?error=Session Expired");     
    }

    private void SortGridView(string sortExpression, string direction)
    {
        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :10/03/2008 11:19:20 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataView dv = new DataView(GeneralClass.Program.sortDS.Tables[0]);
            dv.Sort = sortExpression + " "+direction;

            gdvInvProductList.DataSource = dv;
            gdvInvProductList.DataBind();    

        }
        catch (Exception SortGridView_Exp)
        {

        }
    }

}

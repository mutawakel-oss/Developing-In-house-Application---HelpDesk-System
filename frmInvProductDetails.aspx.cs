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
using AjaxControlToolkit;
using System.Data.Odbc;
using System.DirectoryServices;
public partial class frmInvProductDetails : System.Web.UI.Page
{
    OdbcDataReader reader,readerVendor;

    //string strSerialNo,strAssetNo;
    int ProdID = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired");
        }
        
        //if (null != Request.QueryString["sno"])
        //    strSerialNo = Request.QueryString["sno"].ToString();
        //if (null != Request.QueryString["ano"])
        //    strAssetNo = Request.QueryString["ano"].ToString();
        if (null != Request.QueryString["id"])
            ProdID = Convert.ToInt32(Request.QueryString["id"]);

        if (!IsPostBack)
        {               
            LoadCategory();
            LoadSubCategoryInial();
            LoadParentCategory();          
            LoadVendor();           
            LoadReceivedBy();
            LoadProduct();
            ProductDetials();
            AllocationDisplayOpt();
            
        }
        LoggedAs();      
    }

    protected void LoadCategory()
    {
        /// <summary>
        /// 	Description: Load Inventory categorie into drop list  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        ddlCategory.Items.Clear();

        ListItem l1 = new ListItem();
        l1.Value = "0";
        l1.Text = " -- Select Category -- ";
        ddlCategory.Items.Add(l1);

        try
        {
           
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,category FROM t_category WHERE parent_category = 0");
            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value= reader["id"].ToString();
                li.Text=reader["category"].ToString();
                ddlCategory.Items.Add(li);            
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
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        ddlProductName.Items.Clear();

        try
        {
                    
                ListItem l1 = new ListItem();
                l1.Value = "0";
                l1.Text = " -- Select Product -- ";
                ddlProductName.Items.Add(l1);

                reader = GeneralClass.Program.gRetrieveRecord("SELECT id,product_name FROM t_product_master ORDER BY product_name");
                while (reader.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = reader["id"].ToString();
                    li.Text = reader["product_name"].ToString().Trim();
                    ddlProductName.Items.Add(li);
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

    protected void LoadVendor()
    {
        /// <summary>
        /// 	Description: Load Inventory categorie into drop list  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        ListItem l1 = new ListItem();
        l1.Value = "0";
        l1.Text = "-- Select Vendor--";
        ddlVendor.Items.Add(l1);
       try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,Vendor_name FROM t_invVendorMaster ORDER BY vendor_name");
            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value=reader["id"].ToString();
                li.Text=reader["vendor_name"].ToString();
                ddlVendor.Items.Add(li);
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
      
    
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Load Inventory categorie into drop list  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        ddlSubCategory.Items.Clear();
       

        try
        {
            if (Convert.ToInt32(ddlCategory.SelectedValue) != 0)
            {
                ListItem l1 = new ListItem();
                l1.Value = "0";
                l1.Text = " -- Select Subcategory -- ";
                ddlSubCategory.Items.Add(l1);            
                
                reader = GeneralClass.Program.gRetrieveRecord("SELECT id,category FROM t_category WHERE parent_category =" + ddlCategory.SelectedValue);

                while (reader.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = reader["id"].ToString();
                    li.Text = reader["category"].ToString();
                    ddlSubCategory.Items.Add(li);
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }    
    }

    protected void ddlSubCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: To intialize mis asset no text box with available prefix based on selection  
        ///	
        ///
        /// 	Date:3rd/Dec/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
       
        try
        {
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) != 0)
            {

                reader = GeneralClass.Program.gRetrieveRecord("SELECT category_prefix FROM t_category WHERE id=" + ddlSubCategory.SelectedValue);

                while (reader.Read())
                {
                    txtMisAssetNo.Text = reader["category_prefix"].ToString();
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }


    protected void LoadSubCategory()
    {
        /// <summary>
        /// 	Description: Load Inventory categorie into drop list  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        ddlSubCategory.Items.Clear();
        
        try
        {

            if (Convert.ToInt32(ddlCategory.SelectedValue) != 0)
            {
                ListItem l1 = new ListItem();
                l1.Value = "0";
                l1.Text = " -- Select Subcategory -- ";
                ddlSubCategory.Items.Add(l1);         
                
                reader = GeneralClass.Program.gRetrieveRecord("SELECT id,category FROM t_category WHERE parent_category =" + ddlCategory.SelectedValue);
                while (reader.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = reader["id"].ToString();
                    li.Text = reader["category"].ToString();
                    ddlSubCategory.Items.Add(li);
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
    protected void LoadSubCategoryInial()
    {
        /// <summary>
        /// 	Description: Load Inventory categorie into drop list  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        ddlSubCategory.Items.Clear();

        try
        {

           
                ListItem l1 = new ListItem();
                l1.Value = "0";
                l1.Text = " -- Select Subcategory -- ";
                ddlSubCategory.Items.Add(l1);

                reader = GeneralClass.Program.gRetrieveRecord("SELECT id,category FROM t_category WHERE parent_category <>0 order by category");
                while (reader.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = reader["id"].ToString();
                    li.Text = reader["category"].ToString();
                    ddlSubCategory.Items.Add(li);
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
    protected void LoadReceivedBy()
    {
        /// <summary>
        /// 	Description: Load Inventory categorie into drop list  
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
         
            ListItem li = new ListItem();
                li.Value="0";
                li.Text = "-- Select Recepient --";
                ddlReceivedBy.Items.Add(li);

                reader = GeneralClass.Program.gRetrieveRecord("SELECT id,full_name FROM t_invrecepient ORDER BY full_name");
            
            while(reader.Read())
            {
                if (reader["id"].ToString() != "123456")
                {
                    ListItem l1 = new ListItem();
                    l1.Value = reader["id"].ToString();
                    l1.Text = reader["full_name"].ToString();
                    ddlReceivedBy.Items.Add(l1);
                }
            }
            reader.Close();

        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
    
    protected void SaveProduct(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: save product details into t_invproudctdetails table  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        
        //try
        //{

        int productID = ProductExists();

        

            DateTime dtExpiryDate, dtDeliveryDate, dtTagDate;

            dtExpiryDate =Convert.ToDateTime(txtExpiryDate.Text);
            dtDeliveryDate=Convert.ToDateTime(txtDeliveryDate.Text);
            dtTagDate = Convert.ToDateTime(txtTagDate.Text);

            GeneralClass.Program.Add("vendor_serial_no", txtSerialNo.Text, "S");
            GeneralClass.Program.Add("asset_number", txtMisAssetNo.Text, "S");
            GeneralClass.Program.Add("sub_category", ddlSubCategory.SelectedValue, "I");
            GeneralClass.Program.Add("VendorId", ddlVendor.SelectedValue, "I");
           // GeneralClass.Program.Add("product_name", txtProductName.Text, "S");
            GeneralClass.Program.Add("product_name", ddlProductName.SelectedItem.Text, "S");

            
        GeneralClass.Program.Add("warranty_expire_on",string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(dtExpiryDate)).ToString(), "D");
        
        GeneralClass.Program.Add("spr_number", txtSprNo.Text, "S");
            
            GeneralClass.Program.Add("delivery_date", string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(dtDeliveryDate)).ToString(), "D");
           
            GeneralClass.Program.Add("received_by", ddlReceivedBy.SelectedValue, "S");
            GeneralClass.Program.Add("tagged_date", string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(dtTagDate)).ToString(), "S");
            GeneralClass.Program.Add("approved", rbtnApproved.SelectedItem.Value.ToString(), "I");

            int intreturnID=0;
            if(productID>0)
            {
                intreturnID=GeneralClass.Program.UpdateRecordStatement("t_invproductdetails", "id", productID.ToString());
            }
            else
            {
                intreturnID=GeneralClass.Program.InsertRecordStatement("t_invproductdetails");
            }
           
        if(intreturnID==0)
            {
                Response.Redirect("error.aspx?error=" + "Product details is not updated");
            }
            else
                Response.Redirect("frmInvProductList.aspx");

    
    }

    protected void ProductDetials()
    {
        /// <summary>
        /// 	Description: save product details into t_invproudctdetails table  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        int intSubCategory = 0;
        Session["NewProduct"] = "true"; //use to dipaly Asset Allocation Panel or not
        
        try
        {
            //reader = GeneralClass.Program.gRetrieveRecord("SELECT * FROM t_invproductdetails WHERE vendor_serial_no='" + strSerialNo + "' AND asset_number='" + strAssetNo + "'");
            reader = GeneralClass.Program.gRetrieveRecord("SELECT * FROM t_invproductdetails WHERE id=" + ProdID);
            
            while (reader.Read())
            {
                Session["NewProduct"] = "false";
                    
                txtSerialNo.Text = reader["vendor_serial_no"].ToString();
                txtMisAssetNo.Text = reader["asset_number"].ToString();
                ddlSubCategory.SelectedValue = reader["sub_category"].ToString();
                intSubCategory = Convert.ToInt32(reader["sub_category"]);
                                
                ddlVendor.SelectedValue = reader["vendorId"].ToString();
                
                //txtProductName.Text = reader["product_name"].ToString();
                ddlProductName.Items.FindByText(reader["product_name"].ToString().Trim()).Selected = true;
                
                txtExpiryDate.Text = reader["warranty_expire_on"].ToString();
                txtExpiryDate.Text = string.Format("{0:dd/MMM/yyyy}", Convert.ToDateTime(txtExpiryDate.Text));
                txtSprNo.Text = reader["spr_number"].ToString();
                txtDeliveryDate.Text = reader["delivery_date"].ToString();
                txtDeliveryDate.Text=string.Format("{0:dd/MMM/yyyy}", Convert.ToDateTime(txtDeliveryDate.Text));
                ddlReceivedBy.SelectedValue = reader["received_by"].ToString();
                txtTagDate.Text = reader["tagged_date"].ToString();
                txtTagDate.Text = string.Format("{0:dd/MMM/yyyy}",Convert.ToDateTime(txtTagDate.Text));
                
               
                if (DBNull.Value != reader["approved"])
                    rbtnApproved.SelectedValue = (Convert.ToByte(reader["approved"])).ToString();                
            }
            reader.Close();
            ddlCategory.SelectedValue = ParentCategory(intSubCategory).ToString();
            VendorDetails(ddlVendor.SelectedValue);// to display vendor details
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }      
    }
    
    protected int ProductExists()
    {
        /// <summary>
        /// 	Description: Check for existence of product details in t_invproudctdetails table  
        ///	
        ///
        /// 	Date:3rd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        int intProductID = 0;

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id FROM t_invproductdetails WHERE id=" + ProdID );

            while (reader.Read())
            {
                intProductID = Convert.ToInt32(reader["id"]);

            }
            reader.Close();
            return intProductID;
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
            return intProductID;
        }
    }
    protected void LoadAllocateToUsers()
    {
        /// <summary>
        /// 	Description: Import userid and fullname  from the LDAP
        ///	
        ///
        /// 	Date:4th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        /// 
        ListItem l1 = new ListItem();
        l1.Value = "0";
        l1.Text = "-- Select Owner name -- ";
        ddlOwnerName.Items.Clear();
        ddlOwnerName.Items.Add(l1);
        ArrayList array1 = new ArrayList();
        try
        {

            DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", "wstaff", "Ldap@KSAU!23");//OU=staff,OU=collegeusers,OU=mis                                


            //  DirectoryEntry entry1 = new DirectoryEntry("LDAP://OU=staff,OU=collegeusers,DC=med,DC=ksuhs,DC=edu,DC=sa", "wstaff", "test123");

            DirectorySearcher mySearcher = new DirectorySearcher(entry1);
            SearchResultCollection results;
            results = mySearcher.FindAll();

            string strFullName;
            string strLoginName;

            DirectorySearcher dSearch = new DirectorySearcher(entry1);

            dSearch.Filter = "(&(objectCategory=user)(cn=*))";

            foreach (SearchResult sResultSet in dSearch.FindAll())
            {

                strFullName = GetProperty(sResultSet, "Name");
                strLoginName = GetProperty(sResultSet, "sAMAccountName");
                //if ("" != strLoginName.Trim())
                //    if (strFullName != string.Empty)
                //    {
                //        ListItem li = new ListItem();
                //        li.Value = strLoginName;
                //        li.Text = strFullName;
                //        ddlOwnerName.Items.Add(li);
                //    }

                if (strLoginName != "" && strFullName != "")
                    array1.Add(strFullName.Trim() + "~" + strLoginName.Trim());              
            
            }

            array1.Sort();
          
            for (int i = 0; i < array1.Count; i++)
            {

                string item = array1[i].ToString();

                string[] items = item.Split('~');

                ListItem li = new ListItem();
                li.Value = (string)items[1];
                li.Text = (string)items[0];

                ddlOwnerName.Items.Add(li);
            }
            
        }

        catch (Exception ex)
        {
            //if(null!= reader)
            //    reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
    protected string GetProperty(SearchResult searchResult, string PropertyName)
    {
        if (searchResult.Properties.Contains(PropertyName))
            return searchResult.Properties[PropertyName][0].ToString();
        else
            return string.Empty;
    }


    protected void AllocationDisplayOpt()
    {
        /// <summary>
        /// 	Description: Choose to dipaly Asset Allocation Panel or not
        ///	
        ///
        /// 	Date:4th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        /// 

        if (null != Session["NewProduct"] && Session["NewProduct"].ToString() == "false")
        {
            pnlAssetAllocation.Enabled = true;
            pnlAssetAllocation.Visible = true;
            btnAllocate.Enabled = true;
            btnAllocate.Visible = true;
            
            if(Isallocated())
                if(SelectedAllocatedToType()=="1")
                {
                LoadAllocateToUsers();
                SelectAllocatedUser();
                }
                else
                {
                    LoadAllocatedToDepartment();
                    SelectAllocatedDeparment();
                    SelectDeparmentSecretary();
                }
        }
        else
        {
            pnlAssetAllocation.Enabled = false;
            pnlAssetAllocation.Visible = false;
            btnAllocate.Enabled = false;
            btnAllocate.Visible = false;        
        }
    
    }

    protected void SelectAllocatedUser()
    {
        /// <summary>
        /// 	Description: Select allocation details for the user
        ///	
        ///
        /// 	Date:4th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        /// 
        
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT allocated_to,allocated_bldg,allocated_room,allocated_date FROM t_assetallocation WHERE product_id =" + ProdID);
            while (reader.Read())
            {
                ddlOwnerName.SelectedValue = reader["allocated_to"].ToString();
                txtBuilding.Text = reader["allocated_bldg"].ToString();
                txtRoom.Text = reader["allocated_room"].ToString();
                txtAllocationDate.Text = reader["allocated_date"].ToString();
                txtAllocationDate.Text = string.Format("{0:dd/MMM/yyy}", Convert.ToDateTime(txtAllocationDate.Text));
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

    protected void SelectAllocatedDeparment()
    {
        /// <summary>
        /// 	Description: Select allocation details for the user
        ///	
        ///
        /// 	Date:4th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        /// 

        int deptID=0;
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT allocated_to,allocated_bldg,allocated_room,allocated_date,departmentID FROM t_assetallocation WHERE product_id =" + ProdID);
            while (reader.Read())
            {
                ddlOwnerName.SelectedValue = reader["departmentID"].ToString();
                deptID=Convert.ToInt32( reader["departmentID"]);
                txtBuilding.Text = reader["allocated_bldg"].ToString();
                txtRoom.Text = reader["allocated_room"].ToString();
                txtAllocationDate.Text = reader["allocated_date"].ToString();
                txtAllocationDate.Text = string.Format("{0:dd/MMM/yyy}", Convert.ToDateTime(txtAllocationDate.Text));
            }
            reader.Close();
            
            LoadDeptSecretary(deptID);

        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void SelectDeparmentSecretary()
    {
        /// <summary>
        /// 	Description: Select Secretary for the allocated department
        ///	
        ///
        /// 	Date:4th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        /// 

        int deptID = 0;
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT allocated_to,allocated_bldg,allocated_room,allocated_date,departmentID FROM t_assetallocation WHERE product_id =" + ProdID);
            while (reader.Read())
            {
                lblsecretary.Enabled = true;
                lblsecretary.Visible = true;
                ddlDeptSecretary.Enabled = true;
                ddlDeptSecretary.Visible = true;
                //lbtnNewSecretary.Enabled = true;
                //lbtnNewSecretary.Visible = true;
                                
                ddlDeptSecretary.SelectedValue = reader["allocated_to"].ToString();
                
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
    
    
    protected void AssetAllocation(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Allocate asset to the users
        ///	
        ///
        /// 	Date:4th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        int intReturnID;
        int assetAllocationID = AssetAllocated();

      // GeneralClass.Program.Add("product_id", Request.QueryString["id"].ToString(), "I");

        if (ddlAllocateTo.SelectedValue == "1") //Test for whether selected item is user or department
            GeneralClass.Program.Add("allocated_to", ddlOwnerName.SelectedValue.ToString(), "S");
        else
        {
            GeneralClass.Program.Add("allocated_to", ddlDeptSecretary.SelectedValue.ToString(), "S");
            GeneralClass.Program.Add("departmentID", ddlOwnerName.SelectedValue.ToString(), "I"); 
        }
        
        GeneralClass.Program.Add("allocated_bldg", txtBuilding.Text, "S");
        GeneralClass.Program.Add("allocated_room", txtRoom.Text, "S");
        if (txtAllocationDate.Text!="")
            GeneralClass.Program.Add("allocated_date", string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(txtAllocationDate.Text)).ToString(), "S");

        if (assetAllocationID >0)
            intReturnID = GeneralClass.Program.UpdateRecordStatement("t_assetallocation", "id", assetAllocationID.ToString());
        else
        {
            GeneralClass.Program.Add("product_id", Request.QueryString["id"].ToString(), "I");
            intReturnID = GeneralClass.Program.InsertRecordStatement("t_assetallocation");
        }
        
        
        GeneralClass.Program.Add("assigned_yn", "1", "I");

        int intUpdtID = GeneralClass.Program.UpdateRecordStatement("t_invproductdetails", "id", Request.QueryString["id"].ToString());

        AddUser(ddlOwnerName.SelectedValue.ToString(), ddlOwnerName.SelectedItem.Text);

        

        if (intReturnID > 0)
            Response.Redirect("frminvProductList.aspx");
        else
            Response.Redirect("error.aspx?error=" + "Record is not saved"); 
    
    }

    protected void AddUser(string logID, string fullName)
    {
        // <summary>
        /// 	Description: Create intial entry for local admin  
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        //GeneralClass.Program.gRetrieveRecord("delete from t_users");
        //GeneralClass.Program.Add("id", "123456", "S");
        //GeneralClass.Program.Add("full_name", "Local Admin", "S");

        if (!UserExists(logID))
        {
            GeneralClass.Program.Add("id", logID, "S");
            GeneralClass.Program.Add("full_name", fullName, "S");
            int intReturnID = GeneralClass.Program.InsertRecordStatement("t_users");
        }
    }

    protected bool UserExists(string logID)
    {
        // <summary>
        /// 	Description: Check for the exitstance of user in the temp table  
        ///	
        ///
        /// 	Date:1st/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        bool userExists = false;

        reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(id) FROM t_users WHERE id='" + logID + "'");
        while (reader.Read())
        {
            if (Convert.ToInt32(reader[0]) > 0)
                userExists = true;
        }
        reader.Close();
        return userExists;
    }

    protected void ddlAllocateTo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        // <summary>
        /// 	Description: based on allocate_to option owners will be loaded from Ldap users or t_department_master           
        ///
        /// 	Date:9th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>
        ddlOwnerName.Items.Clear();
        if (ddlAllocateTo.SelectedItem.Value == "1")
        {
            ddlDeptSecretary.Enabled = false;
            ddlDeptSecretary.Visible = false;
            lblsecretary.Enabled = false;
            lblsecretary.Visible = false;
            //lbtnNewSecretary.Enabled = false;
            //lbtnNewSecretary.Visible = false;
            LoadAllocateToUsers();
        }
        else
            LoadAllocatedToDepartment();
    }
        
    protected void LoadAllocatedToDepartment()
    {
        // <summary>
        /// 	Description: Load ddlOwner with t_department names
        ///	
        /// 	Date:9th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        ListItem l1 = new ListItem();
        l1.Value = "0";
        l1.Text = "-- Select Department name --";
        ddlOwnerName.Items.Clear();
        ddlOwnerName.Items.Add(l1);
        
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,department_name FROM t_department_master");
                
            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value = reader["id"].ToString();
                li.Text = reader["department_name"].ToString();
                ddlOwnerName.Items.Add(li);
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

    protected void ddlOwnerName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Load the Department secretary based on the department selection
        ///	
        ///
        /// 	Date:10th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>           

       
        try
        {
            if (ddlAllocateTo.SelectedItem.Value != "1")
            {
                ddlDeptSecretary.Enabled = true;
                ddlDeptSecretary.Visible = true;
                lblsecretary.Enabled = true;
                lblsecretary.Visible = true;
                //lbtnNewSecretary.Enabled = true;
                //lbtnNewSecretary.Visible = true;

                LoadDeptSecretary(Convert.ToInt32(ddlOwnerName.SelectedItem.Value));
            }
            else
            {
                ddlDeptSecretary.Enabled = false;
                ddlDeptSecretary.Visible = false;
                lblsecretary.Enabled = false;
                lblsecretary.Visible = false;
                //lbtnNewSecretary.Enabled = false;
                //lbtnNewSecretary.Visible = false;
            }

        }
        catch (Exception ex)
        {
           
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    
    }

    protected void LoadDeptSecretary(int deptID)
    {
        /// <summary>
        /// 	Description: Load the Department secretary from the Department_master table
        ///	
        ///
        /// 	Date:10th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>           

        string strSecretaries = "";

        try
        {

           reader = GeneralClass.Program.gRetrieveRecord("SELECT secretary FROM t_department_master WHERE id =" + deptID);

            while (reader.Read())
            {
                strSecretaries = reader["secretary"].ToString();
            }
            reader.Close();

            string[] strSecratary = strSecretaries.Split(',');

            for (int i = 0; i <= strSecratary.Length - 1; i++)
            {
                AddSecretary(strSecratary[i].ToString());
            }
        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();

            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }


    protected void AddSecretary(string logID)
    {
        /// <summary>
        /// 	Description: Load the Department secretary from names from t_users table
        ///	
        ///
        /// 	Date:10th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>                   
        ddlDeptSecretary.Items.Clear();

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,full_name FROM t_users WHERE id='"+ logID +"'");

            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value=reader["id"].ToString();
                li.Text=reader["full_name"].ToString();
                ddlDeptSecretary.Items.Add(li);
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

    protected string SelectedAllocatedToType()
    {
        /// <summary>
        /// 	Description: select the allocated_to user type based on the allocation whether it's a user or a department
        ///	
        ///
        /// 	Date:10th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>                   
        string AllocatedType = "";
        
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(departmentID) FROM t_assetallocation WHERE LTRIM(RTRIM(Departmentid)) <>'' and DepartmentID is not null and product_id="+Convert.ToInt32(Request.QueryString["id"]));

            while (reader.Read())
            {
                if (Convert.ToInt32(reader[0]) > 0)
                {
                    ddlAllocateTo.SelectedValue = "2";
                    AllocatedType = "2";  // for department
                }
                else
                {
                    ddlAllocateTo.SelectedValue = "1";
                    AllocatedType = "1";
                }
            }
            reader.Close();
            return AllocatedType;

        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
            return AllocatedType;
        }
    }

    protected bool Isallocated()
    {
        /// <summary>
        /// 	Description: Check for the product whether it's allocated or not.
        ///	
        ///
        /// 	Date:10th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>                   

        bool exists = false;
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(ID) FROM t_assetallocation WHERE product_id=" + Convert.ToInt32(Request.QueryString["id"]));

            while (reader.Read())
            {
                if (Convert.ToInt32(reader[0]) > 0)
                    exists = true;
            }
            reader.Close();
            return exists;
        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
            return exists;
        }
    }

    protected void AddNewSecretary(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Redirect to new AddSecretary page
        ///	
        ///
        /// 	Date:11th/sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        ///     
        /// </summary>                       

       if(null!=Request.QueryString["id"])
        Response.Redirect("frmAssignDeptSecretary.aspx?secretary=frminvproductdetails.aspx?id=" + Request.QueryString["id"]);
    
    }

    protected void SaveNewProductName(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save a new product name into t_product_master 
        ///	
        ///
        /// 	Date:11th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

        //try
        //{
            GeneralClass.Program.Add("product_name", txtNewProductName.Text, "S");
           GeneralClass.Program.InsertRecordStatement("t_product_master");
           if (null != Request.QueryString["id"])
               HttpContext.Current.Response.Redirect("redirect_1.aspx?INVPRODUCT=" + Request.QueryString["id"].ToString());
           else
               LoadProduct();
        
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error=" + ex.Message);
        //}

    }

    protected void SaveNewCategory(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save a new category into t_category 
        ///	
        ///
        /// 	Date:11th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

        //try
        //{
        GeneralClass.Program.Add("parent_category", "0", "I");
        GeneralClass.Program.Add("category", txtNewCategory.Text, "S");
        GeneralClass.Program.InsertRecordStatement("t_category");
        LoadCategory();

        if (null != Request.QueryString["id"])
        HttpContext.Current.Response.Redirect("redirect_1.aspx?INVPRODUCT=" + Request.QueryString["id"].ToString());
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error=" + ex.Message);
        //}

    }

    protected void LoadParentCategory()
    {
        /// <summary>
        /// 	Description: Load Inventory categorie into drop list for adding adding new sub category panel  
        ///	
        ///
        /// 	Date:11th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        /// 
        ListItem l1 = new ListItem();
        l1.Value = "0";
        l1.Text = " -- Select Category --";
        ddlParentCategory.Items.Add(l1);

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,category FROM t_category WHERE parent_category = 0");

            while (reader.Read())
            {
                ListItem li = new ListItem();
                li.Value=reader["id"].ToString();
                li.Text=reader["category"].ToString();
                ddlParentCategory.Items.Add(li);
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

    protected void SaveNewSubCategory(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save a new sub category into t_category 
        ///	
        ///
        /// 	Date:11th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

        //try
        //{
        GeneralClass.Program.Add("parent_category", ddlParentCategory.SelectedValue.ToString(), "I");
        GeneralClass.Program.Add("category", txtNewSubCategory.Text, "S");
        GeneralClass.Program.InsertRecordStatement("t_category");
        LoadSubCategory();
         
        if (null != Request.QueryString["id"])
        HttpContext.Current.Response.Redirect("redirect_1.aspx?INVPRODUCT=" + Request.QueryString["id"].ToString());
       
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error=" + ex.Message);
        //}

    }

    protected void SaveNewVendor(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save a new product name into t_invVendorMaster 
        ///	
        ///
        /// 	Date:11th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

        //try
        //{
        GeneralClass.Program.Add("vendor_name", txtNewVendorName.Text, "S");
        GeneralClass.Program.Add("description", txtNewVendorDescription.Text  , "S");
        GeneralClass.Program.Add("contact_person",txtNewVendorContactPerson.Text, "S");

        GeneralClass.Program.InsertRecordStatement("t_invvendormaster");

        LoadVendor();

       if(null!=Request.QueryString["id"])
        HttpContext.Current.Response.Redirect("redirect_1.aspx?INVPRODUCT=" + Request.QueryString["id"].ToString());
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error=" + ex.Message);
        //}

    }

    protected void NewRecepient(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save a new product name into t_invVendorMaster 
        ///	
        ///
        /// 	Date:11th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

        //try
        //{
        
        if (null != Request.QueryString["id"])
        HttpContext.Current.Response.Redirect("frmInvRecepient.aspx?called=frmInvProductDetails.aspx?id=" + Request.QueryString["id"].ToString());
           else
        HttpContext.Current.Response.Redirect("frmInvRecepient.aspx?called=frmInvProductDetails.aspx");
  
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error=" + ex.Message);
        //}

}

#region commented

//protected void LoadLdapUsers()
    //{
    //    /// <summary>
    //    /// 	Description: Import userid and fullname  from the LDAP
    //    ///	
    //    ///
    //    /// 	Date:3rd/Sept/2007
    //    /// 	Author:Oliver
    //    /// 	Parameter:
    //    ///		input: 
    //    ///		output: 
    //    /// 	Example:  
    //    /// Modified on: 11th/Sept/2007
    //    /// </summary>

    //    try
    //    {

    //        DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", "wstaff", "test123");//OU=staff,OU=collegeusers,OU=mis                                


    //        //  DirectoryEntry entry1 = new DirectoryEntry("LDAP://OU=staff,OU=collegeusers,DC=med,DC=ksuhs,DC=edu,DC=sa", "wstaff", "test123");

    //        DirectorySearcher mySearcher = new DirectorySearcher(entry1);
    //        SearchResultCollection results;
    //        results = mySearcher.FindAll();

    //        string strFullName;
    //        string strLoginName;

    //        DirectorySearcher dSearch = new DirectorySearcher(entry1);

    //        dSearch.Filter = "(&(objectCategory=user)(cn=*))";

    //        foreach (SearchResult sResultSet in dSearch.FindAll())
    //        {

    //            strFullName = GetProperty(sResultSet, "Name");
    //            strLoginName = GetProperty(sResultSet, "sAMAccountName");
    //            if ("" != strLoginName.Trim())
    //                if (strFullName != string.Empty)
    //                {
    //                    ListItem li = new ListItem();
    //                    li.Value = strLoginName;
    //                    li.Text = strFullName;
    //                    lstAllUsers.Items.Add(li);
    //                }
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        //if(null!= reader)
    //        //    reader.Close();
    //        Response.Redirect("error.aspx?error=" + ex.Message);
    //    }
    //}
    //protected void SelectSingleUSer(object sender, EventArgs e)
    //{
    //    /// <summary>
    //    /// 	Description: Adds single user from the userslist box to selected user list box
    //    /// 	
    //    ///
    //    /// 	Date:3rd/Sept/2007
    //    /// 	Author:Oliver
    //    /// 	Parameter:
    //    ///		input: 
    //    ///		output: 
    //    /// 	Example: 
    //    /// Modified on:11th/Sept/2007
    //    /// </summary>

    //    try
    //    {
    //        if (lstAllUsers.SelectedIndex > -1)
    //        {
    //            string strText = lstAllUsers.SelectedItem.Text;
    //            string strValue = lstAllUsers.SelectedItem.Value;
    //            ListItem li = new ListItem();
    //            li.Text = strText;
    //            li.Value = strValue;
    //            lstSelectedUsers.Items.Add(li);
    //            lstAllUsers.Items.Remove(li);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Redirect("error.aspx?error=" + ex.Message);
    //    }

    //}
    //protected void DeSelectSingleUSer(object sender, EventArgs e)
    //{
    //    /// <summary>
    //    /// 	Description: Adds single user from the userslist box to selected user list box
    //    /// 	
    //    ///
    //    /// 	Date:3rd/Sept/2007
    //    /// 	Author:Oliver
    //    /// 	Parameter:
    //    ///		input: 
    //    ///		output: 
    //    /// 	Example:  
    //    /// Modified on:11th/Sept/2007
    //    /// </summary>

    //    try
    //    {
    //        if (lstSelectedUsers.SelectedIndex > -1)
    //        {
    //            string strText = lstSelectedUsers.SelectedItem.Text;
    //            string strValue = lstSelectedUsers.SelectedItem.Value;
    //            ListItem li = new ListItem();
    //            li.Text = strText;
    //            li.Value = strValue;
    //            lstAllUsers.Items.Add(li);
    //            lstSelectedUsers.Items.Remove(li);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Redirect("error.aspx?error=" + ex.Message);
    //    }
    //}

    //protected void SelectMultipleUSer(object sender, EventArgs e)
    //{
    //    /// <summary>
    //    /// 	Description: Adds single user from the userslist box to selected user list box
    //    /// 	
    //    ///
    //    /// 	Date:3rd/Sept/2007
    //    /// 	Author:Oliver
    //    /// 	Parameter:
    //    ///		input: 
    //    ///		output: 
    //    /// 	Example: 
    //    /// Modified on:11th/Sept/2007
    //    /// </summary>

    //    try
    //    {
    //        //lstSelectedUsers.Items.Clear();
    //        for (int i = 0; i < lstAllUsers.Items.Count; i++)
    //            lstSelectedUsers.Items.Add(lstAllUsers.Items[i]);
    //        lstAllUsers.Items.Clear();

    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Redirect("error.aspx?error=" + ex.Message);
    //    }
    //}

    //protected void DeSelectMultipleUSer(object sender, EventArgs e)
    //{
    //    /// <summary>
    //    /// 	Description: Adds single user from the userslist box to selected user list box
    //    /// 	
    //    ///
    //    /// 	Date:3rd/Sept/2007
    //    /// 	Author:Oliver
    //    /// 	Parameter:
    //    ///		input: 
    //    ///		output: 
    //    /// 	Example:  
    //    /// Modified on:11th/Sept/2007
    //    /// </summary>

    //    try
    //    {
    //        //lstAllUsers.Items.Clear();
    //        for (int i = 0; i < lstSelectedUsers.Items.Count; i++)
    //            lstAllUsers.Items.Add(lstSelectedUsers.Items[i]);
    //        lstSelectedUsers.Items.Clear();
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Redirect("error.aspx?error=" + ex.Message);
    //    }

//}
#endregion
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
        {
            if (null != Request.QueryString["id"])
                Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmInvProductDetails.aspx?id=" + Request.QueryString["id"].ToString());
            else
                Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmInvProductDetails.aspx");
        }

   }

    protected void BackToProductList(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Navigate to inv product list
        ///	
        ///
        /// 	Date:12th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>


        HttpContext.Current.Response.Redirect("frmInvProductList.aspx");
    
    }
    protected int ParentCategory(int subcategory)
    {
        /// <summary>
        /// 	Description: Navigate to inv product list
        ///	
        ///
        /// 	Date:12th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        int parentCategory = 0;

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT * FROM t_category WHERE id=(SELECT parent_category FROM t_category WHERE id=" + subcategory + ")");
            while (reader.Read())
            {
                parentCategory = Convert.ToInt32(reader["id"]);

            }
            reader.Close();
            return parentCategory;
        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
            return parentCategory;
        }
    }
  

    protected int AssetAllocated()
    {
        /// <summary>
        /// 	Description: Check for existence of assetallocation in t_assetallocation table  
        ///	
        ///
        /// 	Date:17th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        int assetAllocated = 0;

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id from t_assetallocation WHERE product_id=" + ProdID);

            while (reader.Read())
            {
                assetAllocated = Convert.ToInt32(reader["id"]);

            }
            reader.Close();
            return assetAllocated;
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
            return assetAllocated;
        }
    }

    protected void ddVendor_TextChanged(object sender, EventArgs e)
    {
        VendorDetails(ddlVendor.SelectedValue);
    }

    protected void VendorDetails(string id)
    {

        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :13/01/2008 01:03:04 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
             
        try
        {
          readerVendor =GeneralClass.Program.gRetrieveRecord("SELECT * FROM t_invVendorMaster WHERE id ="+id);
            while(readerVendor.Read())
            {
                txtEditVendorName.Text = readerVendor["vendor_name"].ToString();
                txtEditVendorDescription.Text = readerVendor["Description"].ToString();
                txtEditVendorContactPerson.Text = readerVendor["contact_person"].ToString();
            }
            if (readerVendor != null)
            readerVendor.Close();
        }
        catch (Exception VendorDetails_Exp)
        {
            if (readerVendor != null)
                readerVendor.Close();
        }    
    }

    protected void ModifyVendorDetails(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: Olivery
        /// Date :13/01/2008 02:20:57 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        //try
        //{

        if (txtEditVendorName.Text != "")
        {
            GeneralClass.Program.Add("vendor_name", txtEditVendorName.Text, "S");
            GeneralClass.Program.Add("description", txtEditVendorDescription.Text, "S");
            GeneralClass.Program.Add("contact_person", txtEditVendorContactPerson.Text, "S");

            GeneralClass.Program.UpdateRecordStatement("t_invVendorMaster", "id", ddlVendor.SelectedValue);
        }
           // LoadVendor();

            if (null != Request.QueryString["id"])
                HttpContext.Current.Response.Redirect("redirect_1.aspx?INVPRODUCT=" + Request.QueryString["id"].ToString());

        //}
        //catch (Exception ModifyVendorDetails_Exp)
        //{

        //}
    
    }

}






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
using System.DirectoryServices;
using System.IO;
using System.Text;
public partial class frmNewRequest : System.Web.UI.Page
{
  System.Data.Odbc.OdbcDataReader reader;
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
            LoadAllocatedItems(); // load allocated items to the logged user      


            if (null != Session["UserFullName"])
            {
                txtFullname.Text = Session["UserFullName"].ToString();
            }
            fillBuildingNamesDDL();
            
           

        }

        DisplayOptions();
        //LdapUserInfo("wtest", "wstaff", "test123");

        LoggedAs();
    }
    protected void fillBuildingNamesDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the Building Names drop down list "ddlBuildingName"
        /// Author: mutawakelm
        /// Date :10/19/2010 3:12:18 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strBuildingNameQuery = "SELECT * FROM t_buildingCode";
            reader = GeneralClass.Program.gRetrieveRecord(strBuildingNameQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ListItem building2 = new ListItem();
                    building2.Text = reader["building_name"].ToString();
                    building2.Value = reader["building_id"].ToString();
                    ddlBuildingName.Items.Add(building2);


                }
                reader.Close();
            }
            else
                reader.Close();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void SaveNewRequest(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save new request
        /// 1-check pc technician availability.
        /// 2-check token.
        /// 3-if pc technician available then assign the task.
        /// 4-if not then insert the request into the waiting list table "t_request_queue".
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:mutawakel
        /// 	Parameter:
        ///		input:user details specified in the textbox
        ///		output:
        /// 	Example:  SaveUserDetails()
        /// </summary>

        try{
            
            //The following code will be used to check the avaialability of a pc technician
        technician tech=new technician();
        string strAvailableTecId = tech.mCheckAvailability(ddlBuildingName.SelectedValue.ToString());
        if (!string.IsNullOrEmpty(strAvailableTecId))
        {
          
            int intRequestId=0;
            //The following code will be used to insert the new request 
            if (null != Session["UserID"])
            {
                if (Session["UserID"].ToString() != "")
                {
                    string createby = Session["UserID"].ToString();
                    //GeneralClass.Program.Add("category_id", ddlCategory.SelectedValue, "I");
                    GeneralClass.Program.Add("priority", ddlPriority.SelectedItem.Text, "S");
                    GeneralClass.Program.Add("Status_id", "2", "I");
                    GeneralClass.Program.Add("Description", txtDescription.Text.Trim(), "S");
                    GeneralClass.Program.Add("Department", txtDepartment.Text.Trim(), "S");
                    GeneralClass.Program.Add("Location", txtLocation.Text.Trim(), "S");
                    GeneralClass.Program.Add("created_by", Session["UserID"].ToString(), "S");
                    GeneralClass.Program.Add("requester_mail", Session["mail"].ToString(), "S");
                    GeneralClass.Program.Add("building_code", ddlBuildingName.SelectedItem.Value, "I");
                    if (null != Session["AllocationID"] && Session["AllocationID"].ToString() != "")
                        GeneralClass.Program.Add("allocation_id", Session["AllocationID"].ToString(), "I");
                    if (chbRequestedFor.Checked)
                        GeneralClass.Program.Add("requested_for", ddlRequestedFor.SelectedValue.ToString(), "S");

                    int intReturnID = GeneralClass.Program.InsertRecordStatement("t_requests");
                    intRequestId = intReturnID;
                    if (intReturnID > 0)
                    {
                        #region
                        Byte[] blob = null; System.IO.FileStream fs = null;

                        //------------------------------//
                        string extension = System.IO.Path.GetExtension(UploadedFile.PostedFile.FileName).ToLower();
                        string MIMEType = "";
                        vldFileType.Text = "";
                        if (!string.IsNullOrEmpty(extension))
                        {
                            switch (extension)
                            {
                                case ".gif":
                                    MIMEType = "image/gif";
                                    break;
                                case ".jpg":
                                    MIMEType = "image/jpeg";
                                    break;
                                case ".jpeg":
                                    MIMEType = "image/jpeg";
                                    break;
                                case ".jpe":
                                    MIMEType = "image/jpeg";
                                    break;
                                case ".png":
                                    MIMEType = "image/png";
                                    break;
                                case ".bmp":
                                    MIMEType = "image/bmp";
                                    break;
                                default:
                                    vldFileType.Text = "Invalid File Type";
                                    break;
                            }

                            if (UploadedFile.HasFile && vldFileType.Text == "")
                            {
                                string FilePath = Request.PhysicalApplicationPath + "attachedFiles\\";     //" "C:\\";


                                string strFileName = FilePath + UploadedFile.FileName;
                                UploadedFile.SaveAs(strFileName);


                                //==============saveas the file in the server directory ex: C:\\UPFiles\a.bmp====================//
                                fs = new System.IO.FileStream(strFileName.Trim(), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                blob = new Byte[fs.Length];
                                fs.Read(blob, 0, blob.Length);
                                fs.Close();
                                System.Data.Odbc.OdbcCommand p_Command = null;
                                System.Data.Odbc.OdbcParameter prm = null;
                                System.Data.Odbc.OdbcParameter prm1 = null;
                                System.Data.Odbc.OdbcParameter prm3 = null;
                                p_Command = new System.Data.Odbc.OdbcCommand("{ call StoreAttchment (?,?,?)}", Program.REG_CONN);

                                p_Command.CommandType = CommandType.StoredProcedure;

                                prm = p_Command.Parameters.Add("@filecontent", System.Data.Odbc.OdbcType.Image);
                                prm.Direction = ParameterDirection.Input;
                                prm.Value = blob;

                                prm1 = p_Command.Parameters.Add("@request_id", System.Data.Odbc.OdbcType.BigInt);
                                prm1.Direction = ParameterDirection.Input;
                                prm1.Value = intReturnID;

                                prm3 = p_Command.Parameters.Add("@doc", System.Data.Odbc.OdbcType.VarChar);
                                prm3.Direction = ParameterDirection.Input;
                                //prm3.Value = strFileName;//UploadedFile.PostedFile.FileName.ToString();
                                prm3.Value = MIMEType;
                                p_Command.ExecuteNonQuery();
                                p_Command = null;
                            }
                        }

                        #endregion
                        //The following code was added by mutawakel to create a new txt file for the help 
                        //desk watcher 
                        // create the writer and open the file 
                        string strTxtFileName = strAvailableTecId + "$" + RandomString(10, true) + ".txt";
                        TextWriter tw = new StreamWriter("C:\\help_desk_new_request\\" + strTxtFileName);
                        // write a line of text to the file 
                        tw.WriteLine("From : " + Session["UserID"].ToString() + "\n");
                        tw.WriteLine("Request for :" + ddlRequestedFor.SelectedValue.ToString() + "\n");
                        tw.WriteLine("Department:" + txtDepartment.Text.Trim().ToString() + "\n");
                        tw.WriteLine("Description : " + txtDescription.Text.Trim().ToString() + "\n");
                        tw.WriteLine("Date / time : " + DateTime.Now.ToString() + "\n");
                        tw.Close();
                        //The following code will be used to assign the task
                        
                        GeneralClass.Program.Add("request_id", intRequestId.ToString(), "I");
                        GeneralClass.Program.Add("assigned_to", strAvailableTecId, "S");

                        int intReturnID2 = GeneralClass.Program.InsertRecordStatement("t_assignment");
                        //The following code will be used to insert the mobile
                        //The following code will be used to update the status of the selected pc techinican
                        GeneralClass.Program.Add("status", "2", "I");
                        GeneralClass.Program.UpdateRecordStatement("t_users", "id","'"+ strAvailableTecId+"'");
                        string mobile = GetMobileNo(strAvailableTecId);
                        //if (mobile != "")
                        //    GeneralClass.Program.SendMobileMessage("INSERT INTO sms_alert(contactname,mobile,smstype,smsstatus,messagetext)values('" + strAvailableTecId + "','" + mobile + "'" + ",2,1,'A new task is assigned')");
                        Response.Redirect("~/frmUserRequestList.aspx", true);
                        
                    }
                    else
                        Response.Redirect("Error.aspx?error=There is error occured, please check no. of characters in your description, it should be within 4000 characters ");
                }
               
            }

        }
        else//case of all tehcinicans are busy, so put it in the waiting list
        {
            int intRequestId = 0;//This variale will hold the id of the new request
            //The following code will be used to insert new request
            if (null != Session["UserID"])
                if (Session["UserID"].ToString() != "")
                {
                    string createby = Session["UserID"].ToString();
                    //GeneralClass.Program.Add("category_id", ddlCategory.SelectedValue, "I");
                    GeneralClass.Program.Add("priority", ddlPriority.SelectedItem.Text, "S");
                    GeneralClass.Program.Add("Status_id", "1", "I");
                    GeneralClass.Program.Add("Description", txtDescription.Text.Trim(), "S");
                    GeneralClass.Program.Add("Department", txtDepartment.Text.Trim(), "S");
                    GeneralClass.Program.Add("Location", txtLocation.Text.Trim(), "S");
                    GeneralClass.Program.Add("created_by", Session["UserID"].ToString(), "S");
                    GeneralClass.Program.Add("requester_mail", Session["mail"].ToString(), "S");
                    GeneralClass.Program.Add("building_code", ddlBuildingName.SelectedItem.Value, "I");
                    if (null != Session["AllocationID"] && Session["AllocationID"].ToString() != "")
                        GeneralClass.Program.Add("allocation_id", Session["AllocationID"].ToString(), "I");
                    if (chbRequestedFor.Checked)
                        GeneralClass.Program.Add("requested_for", ddlRequestedFor.SelectedValue.ToString(), "S");

                    int intReturnID = GeneralClass.Program.InsertRecordStatement("t_requests");
                    intRequestId = intReturnID;
                    if (intReturnID > 0)
                    {
                        #region
                        Byte[] blob = null; System.IO.FileStream fs = null;

                        //------------------------------//
                        string extension = System.IO.Path.GetExtension(UploadedFile.PostedFile.FileName).ToLower();
                        string MIMEType = "";
                        vldFileType.Text = "";

                        switch (extension)
                        {
                            case ".gif":
                                MIMEType = "image/gif";
                                break;
                            case ".jpg":
                                MIMEType = "image/jpeg";
                                break;
                            case ".jpeg":
                                MIMEType = "image/jpeg";
                                break;
                            case ".jpe":
                                MIMEType = "image/jpeg";
                                break;
                            case ".png":
                                MIMEType = "image/png";
                                break;
                            case ".bmp":
                                MIMEType = "image/bmp";
                                break;
                            default:
                                vldFileType.Text = "Invalid File Type";
                                break;
                        }

                        if (UploadedFile.HasFile && vldFileType.Text == "")
                        {
                            string FilePath = Request.PhysicalApplicationPath + "attachedFiles\\";     //" "C:\\";


                            string strFileName = FilePath + UploadedFile.FileName;
                            UploadedFile.SaveAs(strFileName);


                            //==============saveas the file in the server directory ex: C:\\UPFiles\a.bmp====================//
                            fs = new System.IO.FileStream(strFileName.Trim(), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                            blob = new Byte[fs.Length];
                            fs.Read(blob, 0, blob.Length);
                            fs.Close();
                            System.Data.Odbc.OdbcCommand p_Command = null;
                            System.Data.Odbc.OdbcParameter prm = null;
                            System.Data.Odbc.OdbcParameter prm1 = null;
                            System.Data.Odbc.OdbcParameter prm3 = null;
                            p_Command = new System.Data.Odbc.OdbcCommand("{ call StoreAttchment (?,?,?)}", Program.REG_CONN);

                            p_Command.CommandType = CommandType.StoredProcedure;

                            prm = p_Command.Parameters.Add("@filecontent", System.Data.Odbc.OdbcType.Image);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = blob;

                            prm1 = p_Command.Parameters.Add("@request_id", System.Data.Odbc.OdbcType.BigInt);
                            prm1.Direction = ParameterDirection.Input;
                            prm1.Value = intReturnID;

                            prm3 = p_Command.Parameters.Add("@doc", System.Data.Odbc.OdbcType.VarChar);
                            prm3.Direction = ParameterDirection.Input;
                            //prm3.Value = strFileName;//UploadedFile.PostedFile.FileName.ToString();
                            prm3.Value = MIMEType;
                            p_Command.ExecuteNonQuery();
                            p_Command = null;
                        }

                        #endregion
                        //The following code was added by mutawakel to create a new txt file for the help 
                        //desk watcher 
                        // create the writer and open the file 
                        string strTxtFileName = Session["UserID"].ToString() + "$" + RandomString(10, true) + ".txt";
                        TextWriter tw = new StreamWriter("C:\\help_desk_new_request\\" + strTxtFileName);
                        // write a line of text to the file 
                        tw.WriteLine("From : " + Session["UserID"].ToString() + "\n");
                        tw.WriteLine("Request for :" + ddlRequestedFor.SelectedValue.ToString() + "\n");
                        tw.WriteLine("Department:" + txtDepartment.Text.Trim().ToString() + "\n");
                        tw.WriteLine("Description : " + txtDescription.Text.Trim().ToString() + "\n");
                        tw.WriteLine("Date / time : " + DateTime.Now.ToString() + "\n");
                        tw.Close();
                    }

                    else
                        Response.Redirect("Error.aspx?error=There is error occured, please check no. of characters in your description, it should be within 4000 characters ");
                }
            //The following code will be used to insert the new request to the queue
            GeneralClass.Program.Add("req_id", intRequestId.ToString(), "I");
            GeneralClass.Program.Add("building_code", ddlBuildingName.SelectedItem.Value, "I");
            GeneralClass.Program.InsertRecordStatement("t_request_queue");
            Response.Redirect("~/frmUserRequestList.aspx", true);


        }
        
        //if (null != Session["UserID"])
        //    if (Session["UserID"].ToString() != "")
        //    {   
        //        string createby = Session["UserID"].ToString();
        //        GeneralClass.Program.Add("category_id", ddlCategory.SelectedValue, "I");
        //        GeneralClass.Program.Add("priority", ddlPriority.SelectedItem.Text, "S");
        //        GeneralClass.Program.Add("Status_id", "1", "I");
        //        GeneralClass.Program.Add("Description", txtDescription.Text.Trim(), "S");
        //        GeneralClass.Program.Add("Department",txtDepartment.Text.Trim(), "S");
        //        GeneralClass.Program.Add("Location", txtLocation.Text.Trim(), "S");               
        //        GeneralClass.Program.Add("created_by", Session["UserID"].ToString(), "S");
        //        GeneralClass.Program.Add("requester_mail", Session["mail"].ToString(), "S");
        //        if(null!=Session["AllocationID"] && Session["AllocationID"].ToString()!="")
        //        GeneralClass.Program.Add("allocation_id", Session["AllocationID"].ToString(), "I");
        //        if(chbRequestedFor.Checked)
        //            GeneralClass.Program.Add("requested_for", ddlRequestedFor.SelectedValue.ToString(), "S");

        //        int intReturnID = GeneralClass.Program.InsertRecordStatement("t_requests");
                
        //        if (intReturnID > 0)
        //        {
        //            #region
        //                Byte[] blob = null; System.IO.FileStream fs = null;

        //                //------------------------------//
        //                string extension = System.IO.Path.GetExtension(UploadedFile.PostedFile.FileName).ToLower();
        //                string MIMEType = "";
        //                vldFileType.Text = "";

        //                switch (extension)
        //                {
        //                    case ".gif":
        //                        MIMEType = "image/gif";
        //                        break;
        //                    case ".jpg":
        //                        MIMEType = "image/jpeg";
        //                        break;
        //                    case ".jpeg":
        //                        MIMEType = "image/jpeg";
        //                        break;
        //                    case ".jpe":
        //                        MIMEType = "image/jpeg";
        //                        break;
        //                    case ".png":
        //                        MIMEType = "image/png";
        //                        break;
        //                    case ".bmp":
        //                        MIMEType = "image/bmp";
        //                        break;                           
        //                    default:
        //                        vldFileType.Text = "Invalid File Type";                                 
        //                        break;
        //                }

        //                if (UploadedFile.HasFile && vldFileType.Text=="")
        //                {
        //                    string FilePath = Request.PhysicalApplicationPath + "attachedFiles\\";     //" "C:\\";
                           
                            
        //                    string strFileName= FilePath + UploadedFile.FileName;
        //                    UploadedFile.SaveAs(strFileName);
                           
                           
        //                    //==============saveas the file in the server directory ex: C:\\UPFiles\a.bmp====================//
        //                    fs = new System.IO.FileStream(strFileName.Trim(), System.IO.FileMode.Open, System.IO.FileAccess.Read);
        //                    blob = new Byte[fs.Length];
        //                    fs.Read(blob, 0, blob.Length);
        //                    fs.Close();
        //                    System.Data.Odbc.OdbcCommand p_Command = null;
        //                    System.Data.Odbc.OdbcParameter prm = null;
        //                    System.Data.Odbc.OdbcParameter prm1 = null;
        //                    System.Data.Odbc.OdbcParameter prm3 = null;
        //                    p_Command = new System.Data.Odbc.OdbcCommand("{ call StoreAttchment (?,?,?)}", Program.REG_CONN);

        //                    p_Command.CommandType = CommandType.StoredProcedure;

        //                    prm = p_Command.Parameters.Add("@filecontent", System.Data.Odbc.OdbcType.Image);
        //                    prm.Direction = ParameterDirection.Input;
        //                    prm.Value = blob;

        //                    prm1 = p_Command.Parameters.Add("@request_id", System.Data.Odbc.OdbcType.BigInt);
        //                    prm1.Direction = ParameterDirection.Input;
        //                    prm1.Value = intReturnID;

        //                    prm3 = p_Command.Parameters.Add("@doc", System.Data.Odbc.OdbcType.VarChar);
        //                    prm3.Direction = ParameterDirection.Input;
        //                    //prm3.Value = strFileName;//UploadedFile.PostedFile.FileName.ToString();
        //                    prm3.Value = MIMEType;
        //                    p_Command.ExecuteNonQuery();
        //                    p_Command = null;
        //                }

        //            #endregion
        //                //The following code was added by mutawakel to create a new txt file for the help 
        //                //desk watcher 
        //                // create the writer and open the file 
        //                string strTxtFileName=Session["UserID"].ToString()+"$"+RandomString(10,true)+".txt";
        //                TextWriter tw = new StreamWriter("C:\\help_desk_new_request\\" +strTxtFileName );
        //                // write a line of text to the file 
        //                tw.WriteLine("From : " + Session["UserID"].ToString() + "\n");
        //                tw.WriteLine("Request for :" +ddlRequestedFor.SelectedValue.ToString() + "\n");
        //                tw.WriteLine("Department:" + txtDepartment.Text.Trim().ToString() + "\n");
        //                tw.WriteLine("Description : "+txtDescription.Text.Trim().ToString() + "\n");
        //                tw.WriteLine("Date / time : " + DateTime.Now.ToString() + "\n");
        //                tw.Close(); 

        //            Response.Redirect("~/frmUserRequestList.aspx", true);
        //        }

        //        else
        //            Response.Redirect("Error.aspx?error=There is error occured, please check no. of characters in your description, it should be within 4000 characters ");
        //    }
        //    else
        //    {
        //        lblSavedStat.Text = "Sorry you request is not saved ";
        //    }
        }
        catch (Exception ex)
        {
           
        }  
       

  
    }
    protected string GetMobileNo(string userid)
    {
        /// <summary>
        /// 	Description: Retrieve mobile no from the t_user table
        ///	
        /// 
        /// 	Date:4th/Dec/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        string mobile = "";

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT mobile FROM t_users WHERE id='" + userid + "'");
            while (reader.Read())
            {
                if (DBNull.Value != reader["mobile"] && reader["mobile"].ToString() != "")
                    mobile = reader["mobile"].ToString();
            }
            reader.Close();
            return mobile;
        }
        catch (Exception ex)
        {
            return mobile;
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }
    protected string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
        {
            return builder.ToString().ToLower();
            return builder.ToString();
        }
        else return "nothing";
    }
    protected void BacktoList(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: back to user request list  
        ///	
        ///
        /// 	Date:22/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        Response.Redirect("frmUserRequestlist.aspx");

    }


    protected void DisplayOptions()
    {
        /// <summary>
        /// 	Description: Display controls based on user rights
        ///	
        ///
        /// 	Date:21/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>

        if ((null != Session["Admin"] && Session["Admin"].ToString() == "true") || (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString()=="true"))
        {
            //if (Session["Admin"].ToString() == "true")
            //{
            
                hlkUserlist.Enabled = true;
                hlkUserlist.Visible = true;
                
                hlkAdminRequestList.Enabled = true;
                hlkAdminRequestList.Visible = true;                         

               
            //}
        }
        else
        {
            //hlkUserRequestList.Enabled = true;
            //hlkUserRequestList.Visible = true;



            if (null != Session["Technician"])
            {
                if (Session["Technician"].ToString() == "true")
                {

                }
            }
            else
            { 
            
            }

        }

    }


    public void LdapUserInfo(string logID, string username, string pwd)
    {
        /// <summary>
        /// 	Description: fetch userdetails from the LDAP
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>


        try
        {
            string domainAndUsername = username.Trim();
            DirectoryEntry entry = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", domainAndUsername, pwd);
            Object obj = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();
            if (null == result)
            {
             //   return string.Empty;
            }
            else
            {
                // Update the new path to the user in the directory
                //_path = result.Path;
                //_filterAttribute = (string)result.Properties["cn"][0];
                string fullName = result.GetDirectoryEntry().Properties["Name"].Value.ToString();
               // return (string)result.Properties["cn"][0].ToString();


            }
        }
        catch (Exception exp)
        {
            //return string.Empty;
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
                   // Hlk.NavigateUrl = "frmTechRequestList.aspx?id=" + Session["UserID"].ToString();
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


    protected void AttachFile(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: attaching file 
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>

       

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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmNewRequest.aspx");
    }

    protected void LoadAllocatedItems()
    {
        /// <summary>
        /// 	Description: Load the allocated items to the logged in user 
        ///	
        ///
        /// 	Date:8th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>   
        ddlAllocatedAsset.Items.Clear();
        
        ListItem l1 = new ListItem();
        l1.Value="0";
        l1.Text = "   -- Select one --   ";
       if(this.Label13.Text.ToString()=="»‰œ")
           l1.Text = "   -- Õœœ «Õœ «·»‰Êœ --   ";
        ddlAllocatedAsset.Items.Add(l1);
        
        try
        {

            reader = GeneralClass.Program.gRetrieveRecord("SELECT t_assetallocation.id,t_assetallocation.product_id,asset_number,product_name FROM t_invproductdetails,t_assetallocation WHERE  t_assetallocation.product_id=t_invproductdetails.id AND allocated_to='" + Session["UserID"].ToString() + "'");
            while (reader.Read())
            {
                // Session["AllocationID"] = Convert.ToInt32(reader[0]);
                ListItem li = new ListItem();
                li.Value = reader["product_id"].ToString();
                li.Text = reader["asset_number"].ToString() + " (" + reader["product_name"].ToString() + ")";
                ddlAllocatedAsset.Items.Add(li);
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

    protected void ddlAllocatedAsset_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Load the allocated items to the logged in user 
        ///	
        ///
        /// 	Date:8th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>   

        string strQuery = "";
       
            

        // To get the AssetAllocation id based on the user selection

        if (ddlAllocatedAsset.SelectedIndex != 0)
        {
            if (chbRequestedFor.Checked && ddlRequestedFor.SelectedIndex != 0)
                strQuery = "SELECT t_assetallocation.id,t_assetallocation.product_id,product_name FROM t_invproductdetails,t_assetallocation WHERE  t_assetallocation.product_id=t_invproductdetails.id AND t_assetallocation.product_id =" + ddlAllocatedAsset.SelectedValue + " AND allocated_to='" + ddlRequestedFor.SelectedValue + "'";
            else
                strQuery = " SELECT t_assetallocation.id,t_assetallocation.product_id,product_name FROM t_invproductdetails,t_assetallocation WHERE  t_assetallocation.product_id=t_invproductdetails.id AND t_assetallocation.product_id =" + ddlAllocatedAsset.SelectedValue + " AND allocated_to='" + Session["UserID"].ToString() + "'";
            reader = GeneralClass.Program.gRetrieveRecord(strQuery);
            while (reader.Read())
            {
                Session["AllocationID"] = Convert.ToInt32(reader[0]);
            }
            reader.Close();
        }
        else
            Session["AllocationID"] = "";
    }

    protected void chbRequestedFor_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chbRequestedFor.Checked)
        {
            
            LoadRequestForUsers();
        }
        if (!chbRequestedFor.Checked)
        {
            ddlRequestedFor.SelectedIndex = 0;
            LoadAllocatedItems();         
        }    
    }
    protected void LoadRequestForUsers()
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
        if(this.Label7.Text.ToString()=="«ÿ·» „‰")
            l1.Text = "-- Õœœ «”„ «·„Œ ’ -- ";
        ddlRequestedFor.Items.Add(l1);

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
                if ("" != strLoginName.Trim())
                    if (strFullName != string.Empty)
                    {
                        ListItem li = new ListItem();
                        li.Value = strLoginName;
                        li.Text = strFullName;
                        ddlRequestedFor.Items.Add(li);
                    }
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

    protected void ddlRequestedFor_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Load the allocated items to the selected Ldap user 
        ///	
        ///
        /// 	Date:10th/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>   
        
        try
        {
            if (chbRequestedFor.Checked)
            {
                ddlAllocatedAsset.Items.Clear();
                ListItem l1 = new ListItem();
                l1.Value = "0";
                l1.Text = "   -- Select one --   ";
                if(this.Label13.Text.ToString()=="»‰œ")
                    l1.Text = "   -- Õœœ «Õœ «·»‰Êœ --   ";
               
                ddlAllocatedAsset.Items.Add(l1);


                System.Data.Odbc.OdbcDataReader reader = GeneralClass.Program.gRetrieveRecord("SELECT t_assetallocation.id,t_assetallocation.product_id,product_name FROM t_invproductdetails,t_assetallocation WHERE  t_assetallocation.product_id=t_invproductdetails.id AND allocated_to='" + ddlRequestedFor.SelectedValue + "'");
                while (reader.Read())
                {
                    //   Session["AllocationID"] = Convert.ToInt32(reader[0]);
                    ListItem li = new ListItem();
                    li.Value = reader["product_id"].ToString();
                    li.Text = reader["product_name"].ToString();
                    ddlAllocatedAsset.Items.Add(li);
                }
                reader.Close();
                GeneralClass.Program.AddUser(ddlRequestedFor.SelectedValue.ToString(), ddlRequestedFor.SelectedItem.Text.ToString()); // to make an new entry in user is not exist in the t_users
            }
           }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }
  
    protected void mOwnRequestClick(object sender, EventArgs e)
    {
        try
        {
            if (null != Session["ITMember"])
            {
                if (Session["ITMember"].ToString() == "true")
                    Response.Redirect("./frmITRequestList.aspx");
            }
            else
                Response.Redirect("./frmUserRequestList.aspx");
        }
        catch (Exception exp)
        {
        }
    }
   
}

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
using System.Net.Mail;

public partial class Default2 : System.Web.UI.Page
{
    OdbcDataReader reader;
    string strPriority;
    string strCategory;
    string strEvaluationStatus = "";//This variable will hold the evaluation of the request "solved" or "not solved"
    int intCurrentRate = 0;//This variable will hold the current rate number 
    string strServiceComment = "";//This variable will holdt the service comment

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(rating.CurrentRating.ToString());

        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired");
        }
        
        if (!IsPostBack)
        {
            LoadRequestDetails();
            DisplayOptions();
        }
        LoggedAs();    
    }

    protected void LoadRequestDetails()
    {
        /// <summary>
        /// 	Description: Populate data from the t_Requests table 
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: Request details will be assigned to textboxes
        /// 	Example:  LoadRequestDetails();
        /// </summary>
        try
        {
            string strRequestQuery = "SELECT ct.category,t.priority,t.description,t.department,t.location,st.status,t.image,t.created_date,t.requester_submit,t.requester_rate,t.requester_comment,u.Full_name FROM t_requests t,t_category ct,t_status st,t_users u WHERE t.category_id = ct.id AND t.status_id = st.id  AND t.created_by = u.id AND t.id=" + Request.QueryString["id"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strRequestQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txtFullname.Text = reader["full_name"].ToString();
                    txtCatagory.Text = reader["category"].ToString();
                    strCategory = reader["category"].ToString();
                    txtPriority.Text = reader["priority"].ToString();
                    strPriority = reader["priority"].ToString(); // used for assigning priority in case editing unassigned requests
                    txtDepartment.Text = reader["department"].ToString();
                    txtLocation.Text = reader["location"].ToString();
                    txtStatus.Text = reader["status"].ToString();
                    txtCreatedOn.Text = reader["created_date"].ToString();
                    txtCreatedOn.Text = string.Format("{0:dd/MMM/yyy, ddd hh:mm:ss tt}", Convert.ToDateTime(txtCreatedOn.Text));
                    txtDescription.Text = reader["description"].ToString();
                    strEvaluationStatus = reader["requester_submit"].ToString();
                    if(!string.IsNullOrEmpty(reader["requester_rate"].ToString()))
                    intCurrentRate = int.Parse(reader["requester_rate"].ToString());
                      if ((reader["requester_comment"] != null) && (reader["requester_comment"] != ""))
                          strServiceComment = reader["requester_comment"].ToString(); 

                }
                reader.Close();
            }
            else reader.Close();
            DisplayRequestFor();
            //The following code will determine the selected radio button list items which will be selected
            if (strEvaluationStatus == "Solved")
            {
                
                lblCurrentEvaluation.Text = "user evalutated that problem was solved.";
                lblCurrentEvaluation.Visible = true;
                rating.CurrentRating = intCurrentRate;
                requesterStatusTable.Visible = true;
                txtComments.Text = strServiceComment;
                
            }
            else
                if (strEvaluationStatus == "NotSolved")
                {

                    lblCurrentEvaluation.Text = "user evalutated that problem was not solved.";
                    lblCurrentEvaluation.Visible = true;
                    rating.CurrentRating = intCurrentRate;
                    requesterStatusTable.Visible = true;
                    txtComments.Text = strServiceComment;
                }
                else
                {
                    requesterStatusTable.Visible = false;
                    lblCurrentEvaluation.Visible = true;
                    if (!((null != Session["Admin"] && Session["Admin"].ToString() == "true") || (null != Session["Technician"] && Session["Technician"].ToString() == "true")))
                    {
                    lblCurrentEvaluation.Text = "You did not evaluate this request. Please evaluate it now by clicking the following button:";
                    btnEvaluation.Visible = true;
                    }
                }

        }
        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
        }
    }

    protected void BackToList(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Back to home page
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>

        if ((null != Session["Admin"] && Session["Admin"].ToString() == "true") || (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString() == "true"))
        {
           // if (Session["Admin"].ToString() == "true")
                Response.Redirect("frmAdminRequestList.aspx");
        }
        else
        {
            //if (null != Session["LocalAdmin"])
            //    if (Session["LocalAdmin"].ToString() == "true")
            //        Response.Redirect("frmAdminRequestList.aspx");
            //    else
            //    {
                    if (null != Session["Technician"])
                    {
                        if (Session["Technician"].ToString() == "true")
                            Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
                    }
                    else
                        Response.Redirect("frmUserRequestList.aspx");
                //}

        }
    }

    protected void DisplayOptions()
    {
        /// <summary>
        /// 	Description: Display controls based on user rights
        ///	
        ///
        /// 	Date:19/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>

        if ((null != Session["Admin"] && Session["Admin"].ToString() == "true") || (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString() == "true"))
        {
        
         // if (Session["Admin"].ToString() == "true" || Session["LocalAdmin"].ToString() == "true")
         //{            
            OptDisplayAdminCancelRequest(); 

             LoadAssignedTo();

             //hlkUserList.Enabled = true;
             //hlkUserList.Visible = true;
             hlkAdminRequestList.Enabled = true;
             hlkAdminRequestList.Visible = true;
             hlkUserRequestList.Enabled = true;
             hlkUserRequestList.Visible = true;
             hlkNewRequest.Enabled = true;
             hlkNewRequest.Visible = true;

             DisplayRequestServiceHistory();

             lnkAdminComment.Enabled = true;
             lnkAdminComment.Visible = true;
             pnlAdminCommentEntry.Enabled = true;
             pnlAdminCommentEntry.Visible = true;

             //pnlAdminComment.Enabled = true;
             //pnlAdminComment.Visible = true;
             //lblAdmincomments.Enabled = true;
             //lblAdmincomments.Visible = true;
              OptDisplayAdminComment();
             

             AdminUpdateStatus();
       
             if (null != Session["Unassigned"])
             {
                 if (Session["Unassigned"].ToString() == "false")
                 {
                     AssignAssignedTo();
                     ddlAssignedTo.Enabled = false;
                     ddlAssignedTo.Visible = false;
                     txtAssignedTo.Enabled = true;
                     txtAssignedTo.Visible = true;
                     btnAssign.Enabled = false;
                     btnAssign.Visible = false;                     
                    
                 }
                 else 
                 {
                     ddlAssignedTo.Enabled = true;
                     ddlAssignedTo.Visible = true;
                     txtAssignedTo.Enabled = false;
                     txtAssignedTo.Visible = false;
                     btnAssign.Enabled = true;
                     btnAssign.Visible = true;                   

                 }
             }
        // }         
     }
     else
     {
        if (null != Session["Technician"])
         {
             if (Session["Technician"].ToString() == "true")
             {
                 OptDisplayAdminCancelRequest();
                 ddlAssignedTo.Enabled = false;
                 ddlAssignedTo.Visible = false;
                 txtAssignedTo.Enabled = false;
                 txtAssignedTo.Visible = false;
                 lblAssignedTo.Enabled = false;
                 lblAssignedTo.Visible = false;
                 txtStatus.Enabled = false;
                 txtStatus.Visible = false;
                 lblStatus.Enabled = true;
                 lblStatus.Visible = true;
                 ddlStatus.Enabled = true;
                 ddlStatus.Visible = true;
                 LoadStatus();
                 AssignStatus();
                 
                 
                 
                 
               
                 btnAssign.Enabled = false;
                 btnAssign.Visible = false;
                 requesterStatusTable.Visible = false;
                 //DisplayRequestServiceHistory();

                 hlkTechReqestList.Enabled = true;
                 hlkTechReqestList.Visible = true;
                 hlkUserRequestList.Enabled = false;
                 hlkUserRequestList.Visible = false;
                 
                 
                 
                 lnkAdminComment.Enabled = false;
                 lnkAdminComment.Visible = false;
                 pnlAdminCommentEntry.Enabled = false;
                 pnlAdminCommentEntry.Visible = false;

                 //pnlAdminComment.Enabled = true;
                 //pnlAdminComment.Visible = true;
                 //lblAdmincomments.Enabled = true;
                 //lblAdmincomments.Visible = true;
                 OptDisplayAdminComment();
                 

                
             }
         }
         else
         {
             btnAssign.Enabled = false;
             btnAssign.Visible = false;
             AssignAssignedTo();
             ddlAssignedTo.Enabled = false;
             ddlAssignedTo.Visible = false;
             txtAssignedTo.Enabled = true;
             txtAssignedTo.Visible = true;
             lblStatus.Enabled = true;
             lblStatus.Visible = true;
             txtStatus.Enabled = true;
             txtStatus.Visible = true;


             hlkUserRequestList.Enabled = true;
             hlkUserRequestList.Visible = true;

             hlkNewRequest.Enabled = true;
             hlkNewRequest.Visible = true;

             DisplayRequestServiceHistory();
             OptDisplayCancelRequest();
             
             
         
         }

     }
    
    }


    protected void AssignAssignedTo()
    {
        /// <summary>
        /// 	Description: Diplay request assignedto name
        ///	
        ///
        /// 	Date:19/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT u.full_name, a.assigned_to FROM t_assignment a, t_users u WHERE u.id=a.assigned_to AND request_id= " + Request.QueryString["id"].ToString());
            while (reader.Read())
            {
               // ddlAssignedTo.SelectedValue = reader["assigned_to"].ToString();
                txtAssignedTo.Text = reader["full_name"].ToString();                
                
            }
            reader.Close();
        }
        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
        }
    }

    protected void LoadAssignedTo()
    {
        /// <summary>
        /// 	Description:  Load Technician list based on usergroup rights
        ///	
        ///
        /// 	Date:19/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        string groups = "";
        ListItem lis = new ListItem();
        ListItem lis1 = new ListItem();
        lis.Value = "0";
        lis.Text = "Un Assinged";
        lis1.Value = "123456";
        lis1.Text = "Local Admin";
        ddlAssignedTo.Items.Add(lis);
        ddlAssignedTo.Items.Add(lis1);

        try
        {
            //reader = GeneralClass.Program.gRetrieveRecord("SELECT id,full_name,user_group FROM t_users");

          //  reader = GeneralClass.Program.gRetrieveRecord("select id,full_name,user_group from t_userrights");
            reader = GeneralClass.Program.gRetrieveRecord("select id,full_name,user_group from t_users");


            while (reader.Read())
            {                
                if (reader["user_group"]!=DBNull.Value)
                {
                    groups = reader["user_group"].ToString();

                    string[] grps = groups.Split(',');

                    for (int i = 0; i <= grps.Length - 1; i++)
                    {
                        //if (grps[i] == "1")
                        //{
                        //    cblGroups.Items[0].Selected = true;
                        //}

                        if (grps[i] == "2")
                        {
                            ListItem li = new ListItem();
                            li.Value = reader["id"].ToString();
                            li.Text = reader["full_name"].ToString();
                            ddlAssignedTo.Items.Add(li);
                        }

                        //if (grps[i] == "3")
                        //{
                        //    cblGroups.Items[2].Selected = true;
                        //}
                    }
                }

            }
            reader.Close();
        }
        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
        }    
    }

protected void  AssignTask(object sender, EventArgs e)
{
    /// <summary>
    /// 	Description: Back to home page
    ///	
    ///
    /// 	Date:18/Aug/2007
    /// 	Author:Oliver
    /// 	Parameter:
    ///		input:
    ///		output:
    /// 	Example:  
    /// </summary>

    //try
    //{

    string mobile = GetMobileNo(ddlAssignedTo.SelectedValue.ToString());
    
    
       GeneralClass.Program.Add("request_id",Request.QueryString["id"].ToString(), "I");
        GeneralClass.Program.Add("assigned_to", ddlAssignedTo.SelectedValue.ToString(),"S");

        int intReturnID = GeneralClass.Program.InsertRecordStatement("t_assignment");

        GeneralClass.Program.Add("status_id", "2", "I");

        int intUpdtID = GeneralClass.Program.UpdateRecordStatement("t_requests", "id", Request.QueryString["id"].ToString());

     // Mobile Alert(message )

      //  string mobile = GetMobileNo(Request.QueryString["id"].ToString());
    //*****************
    if(mobile!="")   
     GeneralClass.Program.SendMobileMessage("INSERT INTO sms_alert(contactname,mobile,smstype,smsstatus,messagetext)values('" +ddlAssignedTo.SelectedValue.ToString()+ "','" + mobile + "'" + ",2,1,'A new task is assigned')");
    //*****************

    if (intReturnID > 0)
        {
            //if (null != Session["mail"] && Session["mail"].ToString != "") // call for send an alert mail to the assigned person(Technician)
            //{
            //    SendMail(Session["mail"].ToString, Session["UserFullName"].ToString());
            //}
           Response.Redirect("frmAdminRequestList.aspx");            
        }
        else
            Response.Redirect("error.aspx?error=" + "Record is not saved");
    //}
    //catch (Exception ex)
    //{
    //    Response.Redirect("error.aspx?error="+ex.Message);
    //}

}
    
   protected void LoadStatus()
    {
        /// <summary>
        /// 	Description: Load Status into drop list  
        ///	
        ///
        /// 	Date:20/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadCategory();
        /// </summary>

       try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,status FROM t_status");
            while (reader.Read())
            {
                if (Convert.ToInt32(reader["id"]) > 1)
                  {
                   ListItem li = new ListItem();
                   li.Value = reader["id"].ToString();
                   li.Text = reader["status"].ToString();
                   ddlStatus.Items.Add(li);
                  }
            }
                        
        reader.Close();
        }
        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
        }

    }

    protected void AssignStatus()
    {
        /// <summary>
        /// 	Description: Diplays request assignedto name
        ///	
        ///
        /// 	Date:20/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT status_id FROM t_requests WHERE id=" + Request.QueryString["id"].ToString());
            while (reader.Read())
            {
                ddlStatus.SelectedValue = reader["status_id"].ToString();
            }
            reader.Close();
        }
        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
        }
    }

    protected void DisplayRequestServiceHistory()
    {
        /// <summary>
        /// 	Description: fill the details of repair by the service provider usually "pc technician"
        ///	
        ///
        /// 	Date:10/5/2008
        /// 	Author:Mutawakel
        /// 	Parameter:
        ///		input: 
        ///		output: Requestlist in the gridview
        /// 	Example:  DisplayUnAssignedRequest()
        /// </summary>
        try
        {
            string strRequestHistoryQuery = "SELECT modified_date,comment,rs.status,u.Full_name FROM t_requestServicehistory rs,t_users u WHERE (rs.serviced_by = u.id and request_id=" + Request.QueryString["id"].ToString() + " and rs.status='Completed') ORDER BY modified_date DESC";
            reader = GeneralClass.Program.gRetrieveRecord(strRequestHistoryQuery);
            if (reader.HasRows)
            {
                reader.Read();
                string[] dateTimeSplitter = reader["modified_date"].ToString().Split(' ');
                txtCompletionDate.Text = dateTimeSplitter[0];
                txtCompletionTime.Text = dateTimeSplitter[1] + " " + dateTimeSplitter[2];
                txtCompletedBy.Text = reader["Full_name"].ToString();
                txtProerAction.Text = reader["comment"].ToString();
                reader.Close();
            }
            else
            {
                reader.Close();
                
                
                this.lblNoComplete.Visible = true;
                
            }

            
          
        }
        catch (OdbcException ex)
        {
            if (reader != null)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }

    protected void UpdateStatus(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Back to home page
        ///	
        ///
        /// 	Date:20/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>


        string strStatus = "";

        if (Convert.ToInt32(ddlStatus.SelectedValue) == 3)
            strStatus = InProgresStatus();
        else
            strStatus = ddlStatus.SelectedItem.Text;
        
        
        //try
        //{
        
        
        GeneralClass.Program.Add("request_id", Request.QueryString["id"].ToString(), "I");
        //GeneralClass.Program.Add("comment", txtComments.Text.Trim(), "S");
        GeneralClass.Program.Add("status", strStatus, "S");
        GeneralClass.Program.Add("status_id", ddlStatus.SelectedValue.ToString(), "I");
        GeneralClass.Program.Add("serviced_by", Session["UserID"].ToString(), "S");

        int intReturnID = GeneralClass.Program.InsertRecordStatement("t_requestServicehistory");


        GeneralClass.Program.Add("status_id", ddlStatus.SelectedValue.ToString(), "I");

        int intUpdtID = GeneralClass.Program.UpdateRecordStatement("t_requests", "id", Request.QueryString["id"].ToString());

        if (intReturnID > 0)
            Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
        else
            Response.Redirect("error.aspx?error=" + "Record is not saved");
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error="+ex.Message);
        //}
    
    }

    protected string InProgresStatus()
    {
        /// <summary>
        /// 	Description: Back to home page
        ///	
        ///
        /// 	Date:20/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>

        string strProgress = "In Progress ";
        int count = 1;

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT count(id) FROM t_requestServicehistory WHERE status_id=3 AND request_id=" + Request.QueryString["id"].ToString());
            while (reader.Read())
            {
                if (Convert.ToInt32(reader[0]) != 0)
                {
                    count += Convert.ToInt32(reader[0]);
                    strProgress += count.ToString();
                }
                else
                {
                    strProgress += count.ToString();
                }

            }
            reader.Close();
            return strProgress;
        }
        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
            return strProgress;
        }

    }


    

    protected void GotoRequestList(object sender, EventArgs e)
    {
        Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
    
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
            Response.Redirect("frmEditLogUserInfo.aspx?logid=" + Session["UserID"].ToString() + "& backto=frmRequestDetails.aspx?id=" + Request.QueryString["id"].ToString());
    }

    protected void FileOpen(object o, EventArgs e)
    {
        System.IO.FileStream fs = null;
        Byte[] blob = null;

        OdbcDataReader reader = GeneralClass.Program.gRetrieveRecord("SELECT attachment,doc_name FROM t_attchament where request_id=" + Convert.ToUInt32(Request.QueryString["id"]));
        try
        {
            while (reader.Read())
            {
                //blob = new Byte[(reader.GetBytes(0, 0, null, 0, int.MaxValue))];
                //reader.GetBytes(0, 0, blob, 0, blob.Length);

                try
                {
                    //fs = new System.IO.FileStream(reader["doc_name"].ToString(),
                                   // System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);

                   // fs= new System.IO.FileStream(

                    //Response.ContentType = "image/jpeg";
                    //Response.BinaryWrite((byte[])reader["attachment"]);
                    
                    //string FileName = fs.Name;
                    //fs.Write(blob, 0, blob.Length);
                    //fs.Flush();
                    //fs.Close();
                    //=====================//
                                //string FileName=reader["doc_name"].ToString();
                               
                                //if(""!=FileName)  System.Diagnostics.Process.Start(FileName);
                    //===================//

                    Response.ContentType = reader["doc_name"].ToString();
                    Response.BinaryWrite((byte[])reader["attachment"]);

                }
                catch (System.IO.IOException exp)
                {
                    
                }
            }
            reader.Close();
        }
        catch (OdbcException exp_)
        {
            if (null != reader)
                reader.Close();
        }
    }


    protected void SaveAdminComment(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Navigate to the Editor page of user info edit
        ///	
        ///
        /// 	Date:1st/Sept/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: 
        /// 	Example: 
        /// </summary>    
        
            if (txtAdminComment.Text != null && txtAdminComment.Text != "")
            {
                GeneralClass.Program.Add("request_id", Request.QueryString["id"].ToString(), "I");
                GeneralClass.Program.Add("comment", txtAdminComment.Text.Trim(), "S");
                int intReturnID = GeneralClass.Program.InsertRecordStatement("t_AdminComment");
            }
            txtAdminComment.Text = "";
            OptDisplayAdminComment();
            
            HttpContext.Current.Response.Redirect("redirect_1.aspx?ID=" + Request.QueryString["id"].ToString());
       
    }
 
   

    protected void OptDisplayAdminComment()
    {
        /// <summary>
        /// 	Description: Display option for showing the admin comment grid  
        ///	
        ///
        /// 	Date:1st/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        txtAdminComment.Text = "";
        
        try
        {

            reader = GeneralClass.Program.gRetrieveRecord("SELECT count(id) FROM t_admincomment WHERE request_id =" + Request.QueryString["id"].ToString());
            while (reader.Read())
            {
                if (Convert.ToInt32(reader[0]) > 0)
                {
                    
                    
                    
                    
                    // DisplayAdminComment();
                }
                else
                {
                    
                    
                    
                    
                }

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

    protected void OptDisplayCancelRequest()
    {
        /// <summary>
        /// 	Description: display option for the cancelrequest button
        ///	
        ///
        /// 	Date:1st/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        bool unassigned = false;
        
        try
        {

            reader = GeneralClass.Program.gRetrieveRecord("SELECT status_id from t_requests WHERE id=" + Request.QueryString["id"].ToString());
            while (reader.Read())
            {
                if (Convert.ToInt32(reader[0]) == 1)  // status id for unassigned  request
                   unassigned = true;
            }
            reader.Close();
            
            if (unassigned)  // status id for unassigned  request
            {
                
                btnCancelRequest.Enabled = true;
                btnCancelRequest.Visible = true;
                btnSave.Enabled = true;
                btnSave.Visible = true;
                ddlCategory.Enabled = true;
                ddlCategory.Visible = true;                
                txtCatagory.Enabled = false;
                txtCatagory.Visible = false;
                LoadCategory();
                ddlCategory.Items.FindByText(strCategory).Selected = true;
                txtAssignedTo.Enabled = false;
                txtAssignedTo.Visible = false;
                lblAssignedTo.Enabled = false;
                lblAssignedTo.Visible = false;
                ddlPriority.Enabled = true;
                ddlPriority.Visible = true;                          
                ddlPriority.Items.FindByText(strPriority).Selected=true;
                txtPriority.Enabled = false;
                txtPriority.Visible = false;
                txtDepartment.ReadOnly = false;
                txtLocation.ReadOnly = false;
                txtDescription.ReadOnly = false;
            }
            else
            {
                btnCancelRequest.Enabled = false;
                btnCancelRequest.Visible = false;
                btnSave.Enabled = false;
                btnSave.Visible = false;
                ddlCategory.Enabled = false;
                ddlCategory.Visible = false;
                txtCatagory.Enabled = true;
                txtCatagory.Visible = true;
                txtAssignedTo.Enabled = true;
                txtAssignedTo.Visible = true;
                lblAssignedTo.Enabled = true;
                lblAssignedTo.Visible = true;
                ddlPriority.Enabled = false;
                ddlPriority.Visible = false;
                txtPriority.Enabled = true;
                txtPriority.Visible = true;
                txtDepartment.ReadOnly = true;
                txtLocation.ReadOnly = true;
                txtDescription.ReadOnly = true;

            }

        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + ex.Message);
        }
    }

    protected void OptDisplayAdminCancelRequest()
    {
        /// <summary>
        /// 	Description: display option for the cancelrequest button
        ///	
        ///
        /// 	Date:23rd/Feb/2008
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>

        string strUser = "";
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            strUser = Session["UserID"].ToString();
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT id,status_id,created_by,requested_for FROM t_requests WHERE status_id=1 AND (created_by='" + strUser + "' OR requested_for='" + strUser + "') AND id=" + Request.QueryString["id"].ToString());
            while (reader.Read())
            {
                btnCancelRequest.Visible = true;
                btnCancelRequest.Enabled = true;
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



    protected void CancelRequest(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: display option for the cancelrequest button
        ///	
        ///
        /// 	Date:1st/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        //try
        //{
        string strUser = "";
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            strUser = Session["UserID"].ToString();

        GeneralClass.Program.Add("status_id", "0", "I");

        int intUpdtID = GeneralClass.Program.UpdateRecordStatement("t_requests", "id", Request.QueryString["id"].ToString());

        GeneralClass.Program.Add("request_id", Convert.ToInt32(Request.QueryString["id"]).ToString(), "I");
        //  GeneralClass.Program.Add("cancelled_on", DateTime.Now.ToShortDateString(), "S");
        // GeneralClass.Program.Add("description",txt...,"S");
        GeneralClass.Program.Add("cancelled_by", strUser, "S");
        GeneralClass.Program.InsertRecordStatement("t_cancelledRequests");

        GeneralClass.Program.Add("request_id", Request.QueryString["id"].ToString(), "I");
        GeneralClass.Program.Add("comment", "Cancelled....", "S");
        GeneralClass.Program.Add("status", "Cancelled", "S");
        GeneralClass.Program.Add("status_id", "0", "I");
        GeneralClass.Program.Add("serviced_by", Session["UserID"].ToString(), "S");

        int intReturnID = GeneralClass.Program.InsertRecordStatement("t_requestServicehistory");



        Response.Redirect("frmUserRequestList.aspx");

        //}
        //catch (Exception ex)
        //{

        //    Response.Redirect("error.aspx?error=" + ex.Message);
        //}
    }

    protected void AdminUpdateStatus()
    {
        bool pcteckOK = false;
      
        OdbcDataReader reader = GeneralClass.Program.gRetrieveRecord("SELECT t_requests.id, t_assignment.assigned_to FROM t_requests LEFT OUTER JOIN t_assignment ON t_requests.id = t_assignment.request_id where t_requests.id=" + Request.QueryString["id"].ToString());
        while (reader.Read())
        { 
         if(null != Session["UserID"] && Session["UserID"].ToString()!="")
             if (reader["assigned_to"].ToString() == Session["UserID"].ToString())
             {
                 pcteckOK = true;
             }
        }
        reader.Close();

        if (pcteckOK == true)
        {
            txtStatus.Enabled = false;
            txtStatus.Visible = false;
            lblStatus.Enabled = true;
            lblStatus.Visible = true;
            ddlStatus.Enabled = true;
            ddlStatus.Visible = true;
            LoadStatus();
            AssignStatus();
            
            
            
            
            btnUpdateStatus.Enabled = true;
            btnUpdateStatus.Visible = true;
            btnAssign.Enabled = false;
            btnAssign.Visible = false;
        
        }

    }

    protected void OnClickAdminComment(object sender, EventArgs e)
    {
        /// <summary>
        /// Description: to tackle the duplicate entry in admin coment while refresh the page
        /// 
        ///
        /// 	Date:2nd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>      

      //  Response.Write("<script language='javascript'> alert('hai'); </script>");

    }
    

    protected void LoadCategory()
    {
        /// <summary>
        /// 	Description: Load categorie into drop list  
        ///	
        ///
        /// 	Date:2nd/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  LoadCategory();
        /// </summary>

        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSet("t_category", "*", "");

            ddlCategory.DataValueField = "id";
            ddlCategory.DataTextField = "category";
            ddlCategory.DataSource = ds;
            ddlCategory.DataBind();

        }
        catch (Exception ex)
        {
            
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }


    protected void SaveRequestChanges(object sender, EventArgs e)
    {
        /// <summary>
        /// 	Description: Save the Changes to the unassigned request
        ///	
        ///
        /// 	Date:20/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output:
        /// 	Example:  
        /// </summary>       

        //try
        //{

        GeneralClass.Program.Add("category_id", ddlCategory.SelectedValue, "I");
        GeneralClass.Program.Add("priority", ddlPriority.SelectedItem.Text, "S");
        GeneralClass.Program.Add("Description", txtDescription.Text.Trim(), "S");
        GeneralClass.Program.Add("Department", txtDepartment.Text.Trim(), "S");
        GeneralClass.Program.Add("Location", txtLocation.Text.Trim(), "S");
       
        //GeneralClass.Program.Add("created_by", Session["UserID"].ToString(), "S");
      

        //GeneralClass.Program.Add("status_id", ddlStatus.SelectedValue.ToString(), "I");

        int intUpdtID = GeneralClass.Program.UpdateRecordStatement("t_requests", "id", Request.QueryString["id"].ToString());

        if (intUpdtID > 0)
            Response.Redirect("frmUserRequestList.aspx?id=" + Session["UserID"].ToString());
        else
            Response.Redirect("error.aspx?error=" + "Record is not saved");
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("error.aspx?error="+ex.Message);
        //}
    
    
    }

    protected void DisplayRequestFor()
    {
        /// <summary>
        /// 	Description: display in case request is made of another person
        ///	
        ///
        /// 	Date:10th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT r.requested_for,u.full_name FROM t_requests r, t_users u WHERE r.requested_for = u.id and r.id=" + Request.QueryString["id"].ToString());
            while (reader.Read())
            {
                if (null != reader["full_name"] && reader["full_name"].ToString() != "")
                {
                    pnlRequestFor.Enabled = true;
                    pnlRequestFor.Visible = true;
                    lblRequestFor.Text = reader["full_name"].ToString();
                }
                else
                {
                    pnlRequestFor.Enabled = false;
                    pnlRequestFor.Visible = false;
                }
  
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

    protected void SendMail(string senderMailID, string senderName)
    {
        /// <summary>
        /// 	Description: Method sends a mail  
        ///	
        ///
        /// 	Date:17th/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>


        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();

        try
        {
            MailAddress fromAddress = new MailAddress(senderMailID, senderName);
            

            smtpClient.Host = "mail.med.ksuhs.edu.sa";

            //         smtpClient.Port = 25;
            message.From = fromAddress;

            // To address collection of MailAddress
            //message.To.Add("olivery@ksau-hs.edu.sa");
            //message.Subject = txtSubject.Text;

            //message.IsBodyHtml = false;
            //message.Body = txtMessage.Text;
            //smtpClient.Send(message);

            //lblStatus.Text = "Email successfully sent.";
        }
        catch (Exception ex)
        {
            //lblStatus.Text = "Send Email Failed...." + ex.Message;
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

        string mobile="";

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
    protected void InitializeCulture()
    {

        if (Session["Language"] != null)
        {

            string culture = Session["Language"].ToString();
            if (string.IsNullOrEmpty(culture))
                culture = "Auto";
            UICulture = culture;
            this.Culture = culture;
            if (culture != "Auto")
            {
                System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
                System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
                System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
                base.InitializeCulture();
            }
        }



    }
 

    private void mBack()
    {
        try
        {
            if ((null != Session["Admin"] && Session["Admin"].ToString() == "true") || (null != Session["LocalAdmin"] && Session["LocalAdmin"].ToString() == "true"))
            {
                // if (Session["Admin"].ToString() == "true")
                Response.Redirect("frmAdminRequestList.aspx");
            }
            else
            {
                //if (null != Session["LocalAdmin"])
                //    if (Session["LocalAdmin"].ToString() == "true")
                //        Response.Redirect("frmAdminRequestList.aspx");
                //    else
                //    {
                if (null != Session["Technician"])
                {
                    if (Session["Technician"].ToString() == "true")
                        Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
                }
                else
                    Response.Redirect("frmUserRequestList.aspx");
                //}

            }
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
            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }


    protected void btnEvaluation_Click(object sender, EventArgs e)
    {
        try
        {
            string popupScript = "<script language='javascript'>" +
 "window.open('frmServiceEvaluation.aspx?id=" + Request.QueryString["id"].ToString() + "', 'CustomPopUp', " +
 "'width=445, height=380, menubar=yes, resizable=no')" +
 "</script>";

            Page.RegisterStartupScript("PopupScript", popupScript);
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
}

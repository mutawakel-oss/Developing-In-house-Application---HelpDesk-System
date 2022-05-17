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
using System.DirectoryServices;

public partial class Default2 : System.Web.UI.Page
{
    OdbcDataReader reader;
    string strPriority;
    string strCategory;
    string strStatus="";//This variable will hold the status of the requset
    string userid = "";
    

    protected void Page_Load(object sender, EventArgs e)
    {
        HyperLink LB1 = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
        if (null != Session["UserID"])
            userid = Session["UserID"].ToString();
        
        if (Session.Count == 0)
        {
            Response.Redirect("error.aspx?error=Session Expired");
        }

        LB1.Text = "Log Out";
        if (!IsPostBack)
        {
            LoadCategory();
            LoadRequestDetails();
            DisplayOptions();
            mCheckCompletion();
            mCheckUserGroup();
        }
        LoggedAs(); 
        //The following code to store the user id
        
        
        
    }
    protected void mCheckUserGroup()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to check the user group of the user 
        /// in order to decide if the escalation panel should be visible or no.
        /// Author: mutawakelm
        /// Date :1/19/2009 12:02:07 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strGroupCheckQuery = "SELECT user_group FROM t_users WHERE id='"+userid+"'";
            reader = GeneralClass.Program.gRetrieveRecord(strGroupCheckQuery);
            if (reader.HasRows)
            {
                reader.Read();
                if (((reader["user_group"].ToString() == "2") || (reader["user_group"].ToString() == "4")) && ((strStatus == "2") || (strStatus == "3")))
                {
                    escalationPanel.Visible = true;
                    ddlCategory.Enabled = true;
                    ddlCategory.Visible = true;
                    txtCatagory.Enabled = false;
                    txtCatagory.Visible = false;
                }
               
                reader.Close();

            }
            else reader.Close();

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mCheckCompletion()
    {


        //=====================================================//
        /// <summary>
        /// Description:
        /// Author: mutawakelm
        /// Date :10/05/2008 03:59:16 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strRequestHistoryQuery = "SELECT modified_date,comment,rs.status,u.Full_name FROM t_requestServicehistory rs,t_users u WHERE (rs.serviced_by = u.id and request_id=" + Request.QueryString["id"].ToString() + " and rs.status='Completed') ORDER BY modified_date DESC";
            reader = GeneralClass.Program.gRetrieveRecord(strRequestHistoryQuery);
            if (reader.HasRows)
            {
                this.btnEvaluate.Enabled = true;
                reader.Close();
            }
            else
            {
                this.btnEvaluate.Enabled = false ;
                reader.Close();
            }


        }
        catch (Exception mCheckCompletion_Exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(mCheckCompletion_Exp.Message.ToString());
        }
         
    }
    
    protected void LoadRequestDetails()
    {
        /// <summary>
        /// 	Description: Populate data from the t_Requests table 
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: Request details will be assigned to textboxes
        /// 	Example:  LoadRequestDetails();
        /// </summary>
        try
        {
            string strCommentQuery = "";
            string strCreatedBy = "";//This variable will hold the user name of the user who create the request
            string strRequestQuery = "SELECT ct.category,t.priority,t.created_by,t.description,t.department,t.status_id,t.location,st.status,t.image,t.created_date,u.Full_name FROM t_requests t,t_category ct,t_status st,t_users u WHERE t.category_id = ct.id AND t.status_id = st.id  AND t.created_by = u.id AND t.id=" + Request.QueryString["id"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strRequestQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txtFullname.Text = reader["full_name"].ToString();
                    txtCatagory.Text = reader["category"].ToString();
                    ddlCategory.Items.FindByText(reader["category"].ToString()).Selected = true;
                    strCategory = reader["category"].ToString();
                    txtPriority.Text = reader["priority"].ToString();
                    strPriority = reader["priority"].ToString(); // used for assigning priority in case editing unassigned requests
                    txtDepartment.Text = reader["department"].ToString();
                    txtLocation.Text = reader["location"].ToString();
                    txtStatus.Text = reader["status"].ToString();
                    strStatus = reader["status_id"].ToString();
                    txtCreatedOn.Text = reader["created_date"].ToString();
                    txtCreatedOn.Text = string.Format("{0:dd/MMM/yyy, ddd hh:mm:ss tt}", Convert.ToDateTime(txtCreatedOn.Text));
                    strCreatedBy = reader["created_by"].ToString();
                    txtDescription.Text = reader["description"].ToString();
                    lblDueDate.Text = "Due by: " + string.Format("{0:dd/MMM/yyy, ddd hh:mm:ss tt}", Convert.ToDateTime(txtCreatedOn.Text).AddDays(2));

                }
                reader.Close();
                //The following code will display the supporter`s comment
                strCommentQuery = "SELECT status,comment,modified_date FROM t_RequestServiceHistory WHERE request_id=" + Request.QueryString["id"].ToString() + " order by modified_date asc";
                reader = GeneralClass.Program.gRetrieveRecord(strCommentQuery);
                if (reader.HasRows)
                {
                    txtComments.Text = "Status" + "            " + "Date" + "                         " + "Comment";
                    txtComments.Text += "\n==============================================================";
                    while (reader.Read())
                    {
                        if (reader["comment"] != null)
                        {
                            if (reader["comment"].ToString() != "")
                            {
                                txtComments.Text += "\n" + reader["status"].ToString() + "        " + reader["modified_date"].ToString() + "           " + reader["comment"].ToString();
                                txtComments.Text += "\n----------------------------------------------------------";
                            }
                        }

                    }
                    reader.Close();
                }
                else reader.Close();
                pulluser("", "wstaff", "Ldap@KSAU!23", strCreatedBy);
            }
            else reader.Close();
            DisplayRequestFor();
        }
        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
        }
       }
    private void pulluser(string domain, string username, string pwd, string userloginName)
    {
        /// <summary>
        /// 	Description: Import users from the LDAP
        ///	
        ///
        /// 	Date:3/2/2009
        /// 	Author:Mutawakelm
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  pullusers("", txtusername.Text, txtpwd.Text)
        /// </summary>

        try
        {
            DirectoryEntry entry1 = new DirectoryEntry("LDAP://10.8.128.100", username, pwd);//OU=staff,OU=collegeusers,OU=mis
            DirectorySearcher mySearcher = new DirectorySearcher(entry1);
            mySearcher.Filter = "(&(objectClass=user)(SAMAccountName=" + userloginName + "))";
            mySearcher.SearchScope = SearchScope.Subtree;
            SearchResult results;
            results = mySearcher.FindOne();
            if (!(results == null))
            {
                txtExtensionNo.Text = results.GetDirectoryEntry().Properties["telephonenumber"].Value.ToString();
                txtPagerNo.Text = results.GetDirectoryEntry().Properties["pager"].Value.ToString();
            }
        }
        catch (Exception ex)
        {

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
             hlkTechReqestList.Enabled = true;
             hlkTechReqestList.Visible = true;
             lbtnTaskList.Enabled = true;
             lbtnTaskList.Visible = true;
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
             DisplayAdminComment();

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
                     ddlAssignedTo.Enabled = false ;
                     ddlAssignedTo.Visible = false;
                     txtAssignedTo.Enabled = true;
                     txtAssignedTo.Visible = true;
                     btnAssign.Enabled = false;
                     btnAssign.Visible = false;                   

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
                 lblComments.Enabled = true;
                 lblComments.Visible = true;
                 txtComments.Enabled = true;
                 txtComments.Visible = true;
                 btnUpdateStatus.Enabled = true;
                 btnUpdateStatus.Visible = true;
                 btnAssign.Enabled = false;
                 btnAssign.Visible = false;
                 DisplayRequestServiceHistory();
                 hlkTechReqestList.Enabled = true;
                 hlkTechReqestList.Visible = true;
                 lbtnTaskList.Enabled = false;
                 lbtnTaskList.Visible = false;
                 pnlAdminComment.Enabled = true;
                 pnlAdminComment.Visible = true;
                 lnkAdminComment.Enabled = false;
                 lnkAdminComment.Visible = false;
                 pnlAdminCommentEntry.Enabled = false;
                 pnlAdminCommentEntry.Visible = false;
                 OptDisplayAdminComment();
                 DisplayAdminComment();
                 

                
             }
         }
        else
             if (null != Session["ITMember"])
             {
                 if (Session["ITMember"].ToString() == "true")
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
                     lblComments.Enabled = true;
                     lblComments.Visible = true;
                     txtComments.Enabled = true;
                     txtComments.Visible = true;
                     btnUpdateStatus.Enabled = true;
                     btnUpdateStatus.Visible = true;
                     btnAssign.Enabled = false;
                     btnAssign.Visible = false;

                     DisplayRequestServiceHistory();

                     hlkTechReqestList.Enabled = true;
                     hlkTechReqestList.Visible = true;
                     lbtnTaskList.Enabled = false;
                     lbtnTaskList.Visible = false;
                     pnlAdminComment.Enabled = true;
                     pnlAdminComment.Visible = true;

                     lnkAdminComment.Enabled = false;
                     lnkAdminComment.Visible = false;
                     pnlAdminCommentEntry.Enabled = false;
                     pnlAdminCommentEntry.Visible = false;

                     //pnlAdminComment.Enabled = true;
                     //pnlAdminComment.Visible = true;
                     //lblAdmincomments.Enabled = true;
                     //lblAdmincomments.Visible = true;
                     OptDisplayAdminComment();
                     DisplayAdminComment();

                 }
             }

             else
             {
                 lblComments.Enabled = true;
                 lblComments.Visible = true;
                 txtComments.Enabled = true;
                 txtComments.Visible = true;
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
                 lbtnTaskList.Enabled = true;
                 lbtnTaskList.Visible = true;
                 hlkNewRequest.Enabled = true;
                 hlkNewRequest.Visible = true;
                 DisplayRequestServiceHistory();
                 OptDisplayCancelRequest();
                 lblDueDate.Enabled = false;
                 lblDueDate.Visible = false;
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
            ds = GeneralClass.Program.gDataSetGridView("SELECT modified_date,comment,status,u.Full_name FROM t_requestServicehistory rs,t_users u WHERE rs.serviced_by = u.id and request_id=" + Request.QueryString["id"].ToString() + " ORDER BY modified_date DESC", "t_requestsservicehistory");
            gdvRequestService.DataSource = ds;
            gdvRequestService.DataBind();
        }
        catch (Exception ex)
        {
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
        string strUserId = Session["UserID"].ToString();
        try
        {
            string strStatus = "";
            string strToMail = "";//This variable will hold the mail of the requester
            string strTechnicanBuildingQuery = "";
            string strTechnicianBuilding = "";
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("helpdesk@ksau-hs.edu.sa", "Help Desk System");

            if (ddlCategory.SelectedItem.Text != "None")
            {
                if (Convert.ToInt32(ddlStatus.SelectedValue) == 3)
                    strStatus = InProgresStatus();
                else
                    strStatus = ddlStatus.SelectedItem.Text;


                //try
                //{


                GeneralClass.Program.Add("request_id", Request.QueryString["id"].ToString(), "I");
                GeneralClass.Program.Add("comment", txtComments.Text.Trim(), "S");
                GeneralClass.Program.Add("status", strStatus, "S");
                GeneralClass.Program.Add("status_id", ddlStatus.SelectedValue.ToString(), "I");
                GeneralClass.Program.Add("serviced_by", Session["UserID"].ToString(), "S");

                int intReturnID = GeneralClass.Program.InsertRecordStatement("t_requestServicehistory");


                GeneralClass.Program.Add("status_id", ddlStatus.SelectedValue.ToString(), "I");
                GeneralClass.Program.Add("category_id", ddlCategory.SelectedValue.ToString(), "I");

                int intUpdtID = GeneralClass.Program.UpdateRecordStatement("t_requests", "id", Request.QueryString["id"].ToString());

                if (intReturnID > 0)
                {
                    if (ddlStatus.SelectedValue.ToString() == "5")
                    {
                        string requesterMail = "SELECT requester_mail,created_by FROM t_requests WHERE id=" + Request.QueryString["id"].ToString();
                        reader = GeneralClass.Program.gRetrieveRecord(requesterMail);
                        if (reader.HasRows)
                        {
                            reader.Read();
                            strToMail = reader["requester_mail"].ToString();
                            //The following code will send an e-mail to the user ask about service evaluation
                            smtpClient.Host = "mail1.ksuhs.edu.sa";
                            message.From = fromAddress;
                            message.To.Add(strToMail);
                            message.Subject = "Evaluation of a help desk service.";//This should have a distinguished name
                            message.IsBodyHtml = false;
                            string strMessage = "Dear, " + reader["created_by"].ToString() + "\n\nPlease evaluate the help desk service provided upon the request number   : " + Request.QueryString["id"].ToString() + "\nwhich was :\n" + txtDescription.Text.ToString() + "\n Kindly click the following link and take a second to evaluate the service quality, where 1 means poor and 5 means excellent: http://helpdesk.ksau-hs.edu.sa/helpdesk/frmCallEvaluationPopup.aspx?id=" + Request.QueryString["id"].ToString() + "\nYour evaluation and comments will be seen only by the IT Manager, and it will be used for the continuous enhancement purposes for the help desk services.\nThank You.";
                            reader.Close();
                            message.Body = strMessage;
                            smtpClient.Send(message);
                            message.Dispose();

                        }
                        else reader.Close();

                    }
                    else
                        if (ddlStatus.SelectedValue.ToString() == "6")
                        {
                            string requesterMail = "SELECT requester_mail,created_by FROM t_requests WHERE id=" + Request.QueryString["id"].ToString();
                            reader = GeneralClass.Program.gRetrieveRecord(requesterMail);
                            if (reader.HasRows)
                            {
                                reader.Read();
                                strToMail = reader["requester_mail"].ToString();
                                //The following code will send an e-mail to the user ask about service evaluation
                                smtpClient.Host = "mail1.ksuhs.edu.sa";
                                message.From = fromAddress;
                                message.To.Add(strToMail);
                                message.Subject = "Evaluation of a help desk service.";//This should have a distinguished name
                                message.IsBodyHtml = false;
                                string strMessage = "Dear, " + reader["created_by"].ToString() + "\n\nThe helpdesk service request : " + Request.QueryString["id"].ToString() + " has been closed on " + DateTime.Now.ToString() + " due to your unavailability, please initiate a new request if you still in need for the service.";
                                reader.Close();
                                message.Body = strMessage;
                                smtpClient.Send(message);
                                message.Dispose();

                            }
                            else reader.Close();
                        }

                    //The following code will be used to check the availability of a new task
                    if ((ddlStatus.SelectedValue.ToString() == "3") || (ddlStatus.SelectedValue.ToString() == "4") || (ddlStatus.SelectedValue.ToString() == "5") || (ddlStatus.SelectedValue.ToString() == "6"))
                    {
                        //The following code will be used to update the status of the pc technician
                        //The following code will be used to update the status of the selected pc techinican
                        GeneralClass.Program.Add("status", "1", "I");
                        GeneralClass.Program.UpdateRecordStatement("t_users", "id", "'" + Session["UserID"].ToString() + "'");
                        //The following code will be used to check the assigned technician building.
                        strTechnicanBuildingQuery = "SELECT building_code FROM t_users where id='" + Session["UserID"].ToString() + "'";
                        reader = GeneralClass.Program.gRetrieveRecord(strTechnicanBuildingQuery);
                        if (reader.HasRows)
                        {
                            reader.Read();
                            strTechnicianBuilding = reader["building_code"].ToString();
                            reader.Close();
                        }
                        //The following code will be used to look for the highest priority task
                        task taskSearch = new task();
                        taskSearch.mGetMostPriority(Session["UserID"].ToString(), strTechnicianBuilding);
                    }
                    Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
                }
                else
                    Response.Redirect("error.aspx?error=" + "Record is not saved");
            }

        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Redirect("frmTechRequestList.aspx?id=" + strUserId);
        }
        finally
        {
            if (reader != null)
                reader.Close();

        }
    
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

protected void gdvRequestService_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            ds = GeneralClass.Program.gDataSetGridView("SELECT modified_date,comment,status,u.Full_name FROM t_requestServicehistory rs,t_users u WHERE rs.serviced_by = u.id and request_id=" + Request.QueryString["id"].ToString() + " ORDER BY modified_date DESC", "t_requestsservicehistory");
            gdvRequestService.DataSource = ds;
            gdvRequestService.PageIndex = e.NewPageIndex;
            gdvRequestService.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
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
            DisplayAdminComment();
            HttpContext.Current.Response.Redirect("redirect_1.aspx?ID=" + Request.QueryString["id"].ToString());
       
    }
    protected void DisplayAdminComment()
    {
        /// <summary>
        /// 	Description: Populate admin comments from t_adminComment table  
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
            DataSet ds;
            ds = GeneralClass.Program.gDataSetGridView("SELECT * FROM t_admincomment WHERE request_id =" + Request.QueryString["id"].ToString() + " ORDER BY comment_date DESC", "t_admincomment");
            gdvAdminComment.DataSource = ds;
            gdvAdminComment.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);
        }

    }
    protected void gdvAdminComment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        /// <summary>
        /// 	Description: Navigate paging admin comments from t_adminComment table  
        ///	
        ///
        /// 	Date:1st/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  
        /// </summary>
        try
        {
            DataSet ds;
            ds = GeneralClass.Program.gDataSetGridView("SELECT * FROM t_admincomment WHERE request_id =" + Request.QueryString["id"].ToString() + " ORDER BY comment_date DESC", "t_admincomment");
            gdvAdminComment.DataSource = ds;

            gdvAdminComment.PageIndex = e.NewPageIndex;

            gdvAdminComment.DataBind();
        }
        catch (Exception ex)
        {
            Response.Redirect("error.aspx?error=" + ex.Message);           
            
        }
    
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
                    pnlAdminComment.Enabled = true;
                    pnlAdminComment.Visible = true;
                    lblAdmincomments.Enabled = true;
                    lblAdmincomments.Visible = true;
                    // DisplayAdminComment();
                }
                else
                {
                    pnlAdminComment.Enabled = false;
                    pnlAdminComment.Visible = false;
                    lblAdmincomments.Enabled = false;
                    lblAdmincomments.Visible = false;
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
                ddlCategory.Enabled =false;
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
            lblComments.Enabled = true;
            lblComments.Visible = true;
            txtComments.Enabled = true;
            txtComments.Visible = true;
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


    protected void btnEvaluate_Click(object sender, EventArgs e)
    {
        
        //=====================================================//
	        /// <summary>
         /// Description:This function will redirect the user to the page of request evaluation
         /// Author: mutawakelm
        /// Date :10/05/2008 01:32:53 PM
         /// Parameter:
         /// input:
         /// output:
         /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            Response.Redirect("./frmRequestStatus.aspx?id="+ Request.QueryString["id"].ToString());

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mFindItMembers(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to search about IT Members from t_users table 
        /// the creteria is if the user group of the user is "4" then he is an IT Member.
        /// Author: mutawakelm
        /// Date :1/19/2009 9:13:49 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (chkEscalate.Checked == true)
            {
                string strITMembersQuery = "SELECT * FROM t_users WHERE user_group='4' or id='meguida'";
                int counter = 1;//This variable will be used to determine the index of the escalation drop down list.
                reader = GeneralClass.Program.gRetrieveRecord(strITMembersQuery);
                if (reader.HasRows)
                {
                    escalteToDDL.Enabled = true;
                    escalteToDDL.Items.Add("-Select IT Member-");
                    while (reader.Read())
                    {
                        escalteToDDL.Items.Add(reader["full_name"].ToString());
                        escalteToDDL.Items[counter].Value = reader["id"].ToString();
                        counter++;
                    }
                    reader.Close();
                }
                else reader.Close();
                //The following code will check if the user is supervisor to display the technicians list
                if (userid == "meguida") 
                {
                    string strSupervisorQuery = "SELECT * FROM t_users WHERE user_group='2'";
                    int counter2 = counter;
                    reader = GeneralClass.Program.gRetrieveRecord(strSupervisorQuery);
                    if (reader.HasRows)
                    {
                       
                            escalteToDDL.Enabled = true;
                            while (reader.Read())
                            {
                                escalteToDDL.Items.Add(reader["full_name"].ToString());
                                escalteToDDL.Items[counter2].Value = reader["id"].ToString();
                                counter2++;
                            }
                            reader.Close();
                        

                    }
                    else reader.Close();
                }
            }
            else
            {
                escalteToDDL.Items.Clear();
                escalteToDDL.Enabled = false;
                btnEscalateTo.Enabled = false;
            }

        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mEscalationDDLSelcted(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to enable the escalation button when the ddl selection index changed
        /// Author: mutawakelm
        /// Date :1/19/2009 9:32:09 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            btnEscalateTo.Enabled = true;
        }
        catch (Exception exp)
        {

        }
    }
    protected void mBtnEscalateClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used when the escalation button clicked to call the escalation function "mEscalateTo"
        /// Author: mutawakelm
        /// Date :1/19/2009 9:36:04 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mEscalateTo();
        }
        catch (Exception exp)
        {

        }
    }
    protected void mEscalateTo()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to escalate a task to an IT MEMBER
        /// STEPS OF ESCALATION:
        /// 1-Retrieve the old escalations.
        /// 2-Update the task "assigned to" and "assignment date" and "escalator" fields.
        /// Author: mutawakelm
        /// Date :1/19/2009 9:37:55 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strTechnicanBuildingQuery = "";
            string strTechnicianBuilding = "";
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("helpdesk@ksau-hs.edu.sa", "Help Desk System");
            //This segement of function will be used to retrieve the old escalations of the task
            string strOldEscalations = null;//This variable will hold the old escalations
            string strEscalator = "";
            int intUpdateResult = 0;//This variable will hold the result of the update query
            string strEscalationQuery = "SELECT assigned_to,escalator FROM t_assignment WHERE request_id="+Request.QueryString["id"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strEscalationQuery);
            if (reader.HasRows)
            {
                reader.Read();
                if (reader["escalator"] != null)
                    strOldEscalations = reader["escalator"].ToString();
                strEscalator = reader["assigned_to"].ToString();
                reader.Close();
            }
            else reader.Close();
            //The following segment will be used to apply the escalation as the steps mentioned in the description
            GeneralClass.Program.Add("assigned_to", escalteToDDL.SelectedItem.Value, "S");
            GeneralClass.Program.Add("assigned_date", DateTime.Now.ToString(), "S");
            if ((strOldEscalations == null) || (strOldEscalations==""))
                GeneralClass.Program.Add("escalator", strEscalator, "S");
            else
                GeneralClass.Program.Add("escalator", strOldEscalations+"|"+strEscalator, "S");
            intUpdateResult= GeneralClass.Program.UpdateRecordStatement("t_assignment", "request_id", Request.QueryString["id"].ToString());
            if (intUpdateResult > 0)
            {
                //The following code will be used to send an email for the user which the task was escalated to
                    smtpClient.Host = "mail1.ksuhs.edu.sa";
                    message.From = fromAddress;
                    message.To.Add(escalteToDDL.SelectedItem.Value.ToString()+"@ksau-hs.edu.sa");
                    message.Subject = "New Escalated Task.";//This should have a distinguished name
                    message.IsBodyHtml = false;
                    string strMessage = "Dear, " + escalteToDDL.SelectedItem.Text.ToString() + "\n\nA new task was escalated to you from " + strEscalator + " , request number is : " + Request.QueryString["id"].ToString() + "\n\nPlease find the escalated task.\n\nWith regards\n\nHelp Desk System";
                    message.Body = strMessage;
                    smtpClient.Send(message);
                    message.Dispose();
                //End of sendig mail code
                if (Session["Technician"] != null)
                {
                    if (Session["Technician"].ToString() == "true")
                    {
                        GeneralClass.Program.Add("status", "1", "I");
                        GeneralClass.Program.UpdateRecordStatement("t_users", "id", "'" + Session["UserID"].ToString() + "'");
                        //The following code will be used to check the assigned technician building.
                        strTechnicanBuildingQuery = "SELECT building_code FROM t_users where id='" + Session["UserID"].ToString() + "'";
                        reader = GeneralClass.Program.gRetrieveRecord(strTechnicanBuildingQuery);
                        if (reader.HasRows)
                        {
                            reader.Read();
                            strTechnicianBuilding = reader["building_code"].ToString();
                            reader.Close();
                        }
                        //The following code will be used to look for the highest priority task
                        task taskSearch = new task();
                        taskSearch.mGetMostPriority(Session["UserID"].ToString(),strTechnicianBuilding);
                        Response.Redirect("frmTechRequestList.aspx?id=" + Session["UserID"].ToString());
                    }

                }
                else
                Response.Redirect("frmUserRequestList.aspx");
            }
            //The following code will be used to check if there is a new task for the pc techincian
   
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mOwnRequestClick(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the request list page
        /// Author: mutawakelm
        /// Date :1/20/2009 9:09:51 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
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

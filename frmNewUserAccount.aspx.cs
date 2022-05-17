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
using System.Net.Mail;
using System.DirectoryServices;
using System.Data.Odbc;
using System.Text;
using System.Drawing;


using System.Net;
using System.IO;
using System.Threading;



public partial class frmNewUserAccount : System.Web.UI.Page
{
    OdbcDataReader reader;
    bool bEmail = false;
    bool bMcurriculum = false;
    bool bMMeducation = false;
    bool bLibray = false;
    string strBadgeNo = "";
    string strDfileName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["NEWUSERREQUESTID"] != null)
        //{
        //    strDfileName = Request.QueryString["FN"].ToString();
        //    lblStatus.Text = "Your Request is successfully submitted, Service Request ID:  " + Request.QueryString["NEWUSERREQUESTID"].ToString();
        //}

        //if (strDfileName != "")
        //{
        //    lbtnDownloadRform.Enabled = true;
        //    lbtnDownloadRform.Visible = true;
        //    lbtnDownloadRform.Text = "Click here to download your Request Form";
        //}
        //else
        //{
        //    lbtnDownloadRform.Enabled = false;
        //    lbtnDownloadRform.Visible = false;
        //    lbtnDownloadRform.Text = "";
        //}
        //Session["Language"] = "en-US";
        
         
        
    }
    protected void SendRequest(object sender, EventArgs e)
    {
        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();
        bool success = false;

        try
        {
            MailAddress fromAddress = new MailAddress(txtEmail.Text, txtName.Text + " - " + txtDepartment.Text);
            //smtpClient.Host = "mail.med.ksuhs.edu.sa";

            smtpClient.Host = "mail1.ksuhs.edu.sa";
            message.From = fromAddress;
            // To address collection of MailAddress
         // message.To.Add("olivery@ksau-hs.edu.sa");
            message.To.Add("helpdesk@ksau-hs.edu.sa");
            message.Subject = "New User Account Request";//txtSubject.Text;
            message.IsBodyHtml = false;
            Int64 ticketID = SaveRequest();
            string strMessage = "REQUEST ID : " + ticketID.ToString() + "\nName       : " + txtName.Text + "\nBadge No   : " + txtBadgeNo.Text + "\nPhone/Pager: " + txtPhone.Text + "\nDepartment : " + txtDepartment.Text + "\nEmail      : " + txtEmail.Text + "\nJob Title  : " + txtSubject.Text;

            strMessage += "\n\n  Requested for the following Service Access ";

            if (chbEmail.Checked == true)
            {
                strMessage += "\n\t * Email Access";
            }
            if (chbCurriculum.Checked == true)
            {
                strMessage += "\n\t * Online Medicine Curriculum";
            }
            if (chbMedicalEducationBlackBoard.Checked == true)
            {
                strMessage += "\n\t * Online Master of Medical Education Program";
            }
            if (chbLibrary.Checked == true)
            {
                strMessage += "\n\t * Online Library System";
            }        
                       
            
            message.Body = strMessage;

            smtpClient.Send(message);
            bEmail = chbEmail.Checked;
            bMcurriculum = chbCurriculum.Checked;
            bMMeducation = chbMedicalEducationBlackBoard.Checked;
            bLibray = chbLibrary.Checked;
            strBadgeNo = txtBadgeNo.Text;
            
            if (bMcurriculum)
                strDfileName = "RequestForms" + strBadgeNo;
            else
                strDfileName = "NewAccountRequestForm" + strBadgeNo;
            lblFileNameHidden.Text = strDfileName;

            success = true;

            //lblconfirmation.Text = "Your Request is successfully send to HelpDesk, You can check your new account status on this same page later";

            btnBack.Focus();
            if (success)
            {
                //string strName = txtName.Text;
                //strBadgeNo = txtBadgeNo.Text;
                //string strSubject = txtSubject.Text;
                //string strDept = txtDepartment.Text;
                //string strPhone = txtPhone.Text;
                //string strTicketID = ticketID.ToString();
                //bEmail = chbEmail.Checked;
                //bMcurriculum = chbCurriculum.Checked;
                //bMMeducation = chbMedicalEducationBlackBoard.Checked;
                //bLibray = chbLibrary.Checked;
                //PrintNewAccountRequestForm(strName, strBadgeNo, strSubject, strDept, strPhone, strTicketID);

                //if (bMcurriculum)
                //    strDfileName = "RequestForms" + strBadgeNo;
                //else
                //    strDfileName = "NewAccountRequestForm" + strBadgeNo;
                AutoResponseMail(txtEmail.Text, ticketID.ToString());
                lblconfirmation.Text = "Your Request is successfully submitted, Service Request ID:  " + ticketID.ToString();
                ModalPopupExtender1.Show();
                //lblStatus.Text = "Your Request is successfully submitted, Service Request ID:  " + ticketID.ToString();

                //string strEmail = txtEmail.Text;
                //string strName = txtName.Text;
                //strBadgeNo = txtBadgeNo.Text;
                //string strSubject = txtSubject.Text;
                //string strDept = txtDepartment.Text;
                //string strPhone = txtPhone.Text;
                //string strTicketID = ticketID.ToString();
                //bEmail = chbEmail.Checked;
                //bMcurriculum = chbCurriculum.Checked;
                //bMMeducation = chbMedicalEducationBlackBoard.Checked;
                //bLibray = chbLibrary.Checked;
                //chbCurriculum.Checked = false;
                //chbEmail.Checked = false;
                //chbMedicalEducationBlackBoard.Checked = false;
                //chbLibrary.Checked = false;

                //txtEmail.Text = "";
                //txtName.Text = "";
                //txtSubject.Text = "";
                //txtBadgeNo.Text = "";
                //txtPhone.Text = "";
                //txtDepartment.Text = "";
                //AutoResponseMail(strEmail, ticketID.ToString());

                //PrintNewAccountRequestForm(strName, strBadgeNo, strSubject, strDept, strPhone, strTicketID);

                //if (bMcurriculum)
                //    strDfileName = "RequestForms" + strBadgeNo;
                //else
                //    strDfileName = "NewAccountRequestForm" + strBadgeNo;

                //HttpContext.Current.Response.Redirect("redirect_1.aspx?NEWUSERREQUESTID=" + strTicketID + "&FN=" + strDfileName);
                // System.Diagnostics.Process.Start("C:\\RequestForms.pdf");              
            }
        }
        catch (Exception ex)
        {
            if (success == false)
                lblStatus.Text = "Sorry, Your Request is not submitted - " + ex.Message;
        }
    }
    protected void cancelRequest(object sender, EventArgs e)
    {
        txtEmail.Text = "";
        txtName.Text = "";
        txtSubject.Text = "";
        txtBadgeNo.Text = "";
        txtPhone.Text = "";
        txtDepartment.Text = "";
       // txtMessage.Text = "";
        lblStatus.Text = "";
        lbtnDownloadRform.Visible = false;
        lbtnDownloadRform.Enabled = false;
        lbtnDownloadRform.Text = "";
        chbCurriculum.Checked = false;
        chbEmail.Checked = false;
        chbMedicalEducationBlackBoard.Checked = false;
        chbLibrary.Checked = false;

       // ModalPopupExtenderRequestForm.Show();       
        
    }
    protected void BackToMainSite(object sender, EventArgs e)
    {
        string FileName = "c:\\hlpDesk\\RequestForms\\" + lblFileNameHidden.Text.Trim() + ".pdf";
        //System.Diagnostics.Process.Start(FileName); 
        HttpContext.Current.Response.Redirect("login.aspx"); 
    }
    protected Int64 SaveRequest()
    {
        Int64 RequestID = 0;

        GeneralClass.Program.Add("name", txtName.Text, "S");
        GeneralClass.Program.Add("badgeNo", txtBadgeNo.Text.Trim(), "I");
        GeneralClass.Program.Add("phone", txtPhone.Text, "S");
        GeneralClass.Program.Add("department", txtDepartment.Text, "S");
        GeneralClass.Program.Add("subject", txtSubject.Text, "S");
        GeneralClass.Program.Add("email", txtEmail.Text, "S");
        GeneralClass.Program.Add("request_description", "New User Account Request", "S");
        RequestID = GeneralClass.Program.InsertRecordStatement("t_emailservicerequest");

        return RequestID;
    }

    protected void NewUserAccountStatus(object sender, EventArgs e)
    {
        pnlNewVendor.Enabled = false;
        pnlNewVendor.Visible = false;
        lblHeading.Text = " Account status";
        lblInstruction.Text = "";
        pnlAccountStatus.Enabled = true;
        pnlAccountStatus.Visible = true;
        lbtnNewAccount.Enabled = true;
        lbtnNewAccount.Visible = true;
        lbtnAccountStatus.Enabled = false;
        lbtnAccountStatus.Visible = false;
    }

    protected void NewAccount(object sender, EventArgs e)
    {
        pnlNewVendor.Enabled = true;
        pnlNewVendor.Visible = true;
        pnlAccountStatus.Enabled = false;
        pnlAccountStatus.Visible = false;
        lbtnAccountStatus.Visible = true;
        lbtnAccountStatus.Enabled = true;
        lbtnNewAccount.Enabled = false;
        lbtnNewAccount.Visible = false;
        lblHeading.Text = " New User Account Request";
        lblInstruction.Text = "Please fill following details, and click send to submit your New User Account Request";
    }


    protected void CurrentAccountStatus(object sender, EventArgs e)
    {
        lblLogin.Text = "";
                    lblpwd.Text = "";
                    lblfullName.Text = "";
                    lblMailID.Text = "";
        
        string strFullName = GetFullName(Convert.ToInt32(txtBadg.Text.Trim()));

        if (strFullName != "")
            UserAccountStatus(strFullName);
        else
            lblMailID.Text = "Your request is not availble, Please check your badge no.";
    }

    private void UserAccountStatus(string Fname)
    {
        /// <summary>
        /// 	Description: Import users from the LDAP
        ///	
        ///
        /// 	Date:26/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  pullusers("", txtusername.Text, txtpwd.Text)
        /// </summary>

        try
        {


            DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", "wstaff", "Ldap@KSAU!23");//OU=staff,OU=collegeusers,OU=mis
            
          //  DirectoryEntry entry1 = new DirectoryEntry("LDAP://OU=mis,DC=med,DC=ksuhs,DC=edu,DC=sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis

            DirectorySearcher mySearcher = new DirectorySearcher(entry1);
            SearchResultCollection results;
            results = mySearcher.FindAll();

            string strFullName;
            string strEMail;
            string strBadgeNo;
            string strTitle;
            string strDepartment;
            string strPager;
            string strTele;
            string strMobile;
            string strLoginName;


            DirectorySearcher dSearch = new DirectorySearcher(entry1);
            dSearch.Filter = "(&(objectCategory=user)(cn=*))";


            foreach (SearchResult sResultSet in dSearch.FindAll())
            {
                strFullName = GetProperty(sResultSet, "Name");
                strBadgeNo = GetProperty(sResultSet, "employeeid");
                strEMail = GetProperty(sResultSet, "mail");
                strTele = GetProperty(sResultSet, "telephonenumber");
                strDepartment = GetProperty(sResultSet, "department");
                strTitle = GetProperty(sResultSet, "title");
                strLoginName = GetProperty(sResultSet, "sAMAccountName");
                
                strPager = GetProperty(sResultSet, "pager");

                if (strFullName.Trim().ToUpper() == Fname.Trim().ToUpper())
                {
                    lblLogin.Text = "Login Name : " + strLoginName;
                    lblpwd.Text = "Password  : 123456";
                    lblfullName.Text = "Full Name : " + strFullName;
                    lblMailID.Text = "Email : " + strEMail;
                }
                               
                strMobile = GetProperty(sResultSet, "mobile");

                //if (strMobile.Trim()="966540461128")
                //    lblCurrentStatus.Text = strMobile.Trim();
            }  

        }
        catch (Exception ex)
        {

        }
    }

    public static string GetProperty(SearchResult searchResult, string PropertyName)
    {
        /// <summary>
        /// 	Description: Returns property for the LDAP data fetching
        ///	
        ///
        /// 	Date:26/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  GetProperty(sResultSet, "sAMAccountName");
        /// </summary>
        //HttpContext.Current.Response.Write(searchResult.Path);
        try
        {
            if (searchResult.Properties.Contains(PropertyName))
                return searchResult.Properties[PropertyName][0].ToString();
            else
                return string.Empty;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    protected string GetFullName(int bagdeNo)
    {
        string name = "";        
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT name FROM t_emailservicerequest WHERE badgeNo="+ bagdeNo);
            while (reader.Read())
            {
                if (reader["name"] != null && reader["name"].ToString() != "")
                    name = reader["name"].ToString();
            }
            reader.Close();
            return name;
        }
        catch (Exception NumberOfUnAssignedRequests_Exp)
        {
            return name;
            Response.Redirect("error.aspx?error=" + NumberOfUnAssignedRequests_Exp.Message);
        }
    
    }

   


    //private ITable CreateTable(PDFDocument doc) 
    //{ 
    //    ITable table = doc.CreateTable();
    //    int colsCount = 2; int rowsCount = 10;
    //    table.Style.BordersWidth.All = 1;
    //    table.Style.CellSpacing = 5;
    //    for (
    //        int j = 0; j < colsCount; j++) { table.Columns.CreateColumn();
    //    } 
    //    for (int i = 0; i < rowsCount; i++)
    //    { 
    //        ITableRow row = table.Rows.CreateRow(); 
    //        for (int j = 0; j < colsCount; j++)
    //        { 
    //            row.Cells.CreateCell(); 
    //            table.Rows[i].Height = (doc.LastPage.DrawingHeight / rowsCount) - 10; 
    //        }
    //    }
    //    for (int i = 0; i < table.Rows.Count; i++)
    //    {
    //        for (int j = 0; j < table.Columns.Count; j++) 
    //        {
    //            table.Rows[i].Cells[j].Style.BordersWidth.All = 5;
    //            table.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(252, 172, 38);
    //            table.Columns[j].Width = 75; 
    //        } 
    //    }
    //    table.Columns[0].Width = (doc.LastPage.DrawingWidth / 2) - 30f;
    //    table.Columns[1].Width = (doc.LastPage.DrawingWidth / 2) - 30f;
    //    return table;
    //}

    protected void downLoad(object sender, EventArgs e)
    {


        string FileName = AppDomain.CurrentDomain.BaseDirectory + "OnlineCurriculumAccessAgreement.pdf";
        Response.ContentType = "application/acrobatreader";
        Response.AppendHeader("Content-Disposition", "attachment; filename=OnlineCurriculumAccessAgreement.pdf");
        Response.TransmitFile(FileName);        
         
    }

    protected void DownloadRequestForm(object sender, EventArgs e)
    {
        if (lblFileNameHidden.Text != "")
        {
            string FileName = "c:\\hlpDesk\\RequestForms\\" + lblFileNameHidden.Text.Trim()+ ".pdf";
            Response.ContentType = "application/acrobatreader";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + lblFileNameHidden.Text.Trim() + ".pdf");
            Response.TransmitFile(FileName);
        }
        
    }
    
    
    private void AutoResponseMail(string mailID,string ServiceID)
    {
       SmtpClient  smtpClient1 = new SmtpClient();
       MailMessage mMsg = new MailMessage();
                
        MailAddress fromAddress = new MailAddress("helpdesk@ksau-hs.edu.sa");
       //smtpClient.Host = "mail.med.ksuhs.edu.sa";

        smtpClient1.Host = "mail1.ksuhs.edu.sa";
        mMsg.From = fromAddress;
        // To address collection of MailAddress
        // message.To.Add("olivery@ksau-hs.edu.sa");
        mMsg.To.Add(mailID);
        mMsg.Subject = "New User Account Request";//txtSubject.Text;
        mMsg.IsBodyHtml = false;

        string strMessage = "  Date : " +  DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString(); 
        
         strMessage += "\n  Service Request ID   : " + ServiceID;
        
        strMessage += "\n\n  You have made Request for the following " ;
        
        if(chbEmail.Checked==true)
        {
          strMessage += "\n\t * Email Access";
        }
        if(chbCurriculum.Checked==true)
        {
          strMessage += "\n\t * Online Medicine Curriculum";
        }  
        if(chbMedicalEducationBlackBoard.Checked==true)
        {
          strMessage += "\n\t * Online Master of Medical Education Program";
        }  
        if(chbLibrary.Checked==true)
        {
          strMessage += "\n\t * Online Library System";
        }
        strMessage += "\n\n     Use Service Request ID for follow up";

        strMessage += "\n\n\tNB:- Fax Photocopy of your BadgeID with Request Form to 2520088 X 41036.";
        if (chbCurriculum.Checked == true)
        {
           strMessage += "\n\t     Please download Agreement for Online Medicine Curriculum and fax a Copy of signed Agreement";
        }

        mMsg.Body = strMessage;
        smtpClient1.Send(mMsg);
    }


    private void PdfMerge()
    {
        string FileName0 = "c:\\hlpDesk\\RequestForms\\NewAccountRequestForm" + strBadgeNo + ".pdf";
        string FileName = "c:\\hlpDesk\\RequestForms\\OnlineCurriculumAccessAgreement.pdf";
        string MergedForm1 = "c:\\hlpDesk\\RequestForms\\RequestForms" + strBadgeNo + ".pdf";

        FileStream inputStream1 = new FileStream(FileName0, FileMode.Open);
        FileStream inputStream2 = new FileStream(FileName, FileMode.Open);
        FileStream outputStream = new FileStream(MergedForm1, FileMode.Create);

        

        outputStream.Close();
        inputStream1.Close();
        inputStream2.Close();        
        
        //string FileName = AppDomain.CurrentDomain.BaseDirectory + "OnlineCurriculumAccessAgreement.pdf";

        //FileStream inputStream1 = new FileStream("NewAccountRequestForm" + strBadgeNo + ".pdf", FileMode.Open);
        //FileStream inputStream2 = new FileStream(FileName, FileMode.Open);
        //FileStream outputStream = new FileStream("RequestForms.pdf", FileMode.Create);

        //Syncfusion.Pdf.PDFDocument.Merge(outputStream, new Stream[] { inputStream1, inputStream2 });

        //outputStream.Close();
        //inputStream1.Close();
        //inputStream2.Close();
        //System.Diagnostics.Process.Start("RequestForms.pdf");
    }
 
    protected void InitializeCulture()
    {
        //Response.Write(Request.QueryString["Language"].ToString());
        //string culture = Request.QueryString["Language"].ToString();
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

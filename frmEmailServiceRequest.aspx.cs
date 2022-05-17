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

public partial class frmEmailServiceRequest : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {
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
            //message.To.Add("olivery@ksau-hs.edu.sa");
            message.To.Add("helpdesk@ksau-hs.edu.sa");
            message.Subject = txtSubject.Text;
            message.IsBodyHtml = false;
            Int64 ticketID = SaveRequest();
            string strMessage = "REQUEST ID   : " + ticketID.ToString() + "\nName       : " + txtName.Text + "\nBadge No   : " + txtBadgeNo.Text + "\nPhone/Pager: " + txtPhone.Text + "\nDepartment : " + txtDepartment.Text + "\nEmail      : " + txtEmail.Text + "\n\nSubject    : " + txtSubject.Text + "\n\n\t" + txtMessage.Text;
            message.Body = strMessage;
                     
            smtpClient.Send(message);
            success = true;
            
            lblconfirmation.Text ="Your Request is successfully send to HelpDesk, Support person will contact you.  Your RequestID: " + ticketID.ToString();
                               
            //lblStatus.Text = "Your Request is successfully submitted";
            txtEmail.Text = "";
            txtName.Text = "";
            txtSubject.Text = "";
            txtBadgeNo.Text = "";
            txtPhone.Text = "";
            txtDepartment.Text = "";
            txtMessage.Text = "";
            if(success)
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
          if(success==false)
            lblStatus.Text = "Sorry, Your Request is not submitted - " + ex.Message ;
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
        txtMessage.Text = "";
        lblStatus.Text = "";       
    }
    protected void BackToMainSite(object sender, EventArgs e)
    {
       HttpContext.Current.Response.Redirect("http://com.ksau-hs.edu.sa/arb/index.php?Itemid=1&option=com_content");
    
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
        GeneralClass.Program.Add("request_description", txtMessage.Text, "S");
        RequestID = GeneralClass.Program.InsertRecordStatement("t_emailservicerequest");

        return RequestID;
    }
}

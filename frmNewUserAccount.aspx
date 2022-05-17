<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmNewUserAccount.aspx.cs" Inherits="frmNewUserAccount" Title="HelpDesk|New User Account" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">  
<script  type="text/javascript">
</script>

<div   id="body"  style="width:1000px">
		<div id="col_main_left">
			<div id="user_assistance">
				<a id="content_start"></a>
				<h3 runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
					<asp:Label runat="server" id="lblhlpLink" Text="Help and Other Links" meta:resourcekey="lblhlpLinkResource1"></asp:Label>
					</h3>				
			</div>				
			<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>						
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">
		<asp:LinkButton ID="lbtnAccountStatus" runat="server" Text="Account Status" Font-Size="Smaller" Font-Bold="False"  CausesValidation="False" Font-Overline="False" ForeColor="Black" OnClick = "NewUserAccountStatus" meta:resourcekey="lbtnAccountStatusResource1"></asp:LinkButton>
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">
		<asp:LinkButton ID="lbtnNewAccount" runat="server" Text="New Account" Font-Size="Smaller" Font-Bold="False"  CausesValidation="False" Font-Overline="False" ForeColor="Black" OnClick = "NewAccount" Enabled="False" Visible="False" meta:resourcekey="lbtnNewAccountResource1"></asp:LinkButton>
		</asp:TableCell>
		</asp:TableRow>			
		</asp:Table>
		</div>			
		</div>
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' id="col_main_right">	
		<h2 class="section">
		       <asp:Label  ID = "lblHeading" Text="New UserAccount Request" runat="server" meta:resourcekey="lblHeadingResource1"></asp:Label>		       
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <asp:Label ID="manStar" Text="*" runat="server" ForeColor="Red" meta:resourcekey="manStarResource1"></asp:Label> <asp:Label ID="Label1" Text="Mandantory Field" runat="server" Font-Bold="False" Font-Size="Smaller" meta:resourcekey="Label1Resource1"></asp:Label>             		         	         
		         </h2>
		          <asp:Label ID="lblInstruction" runat="server" Font-Bold="False"  Font-Size="Smaller" Text="Please fill following details, and click send to submit your New User Account Request" meta:resourcekey="lblInstructionResource1"></asp:Label>		
			     <br />		     
		
      <asp:Panel ID="pnlNewVendor" CssClass="modalPopup"  Width="782px" Height="230px" BorderStyle="Double" runat="server">    
    <asp:Table ID="Table20" runat="server" Width="100%">
    <asp:TableRow runat="server">
    <asp:TableCell Width ="150px" Font-Size="Smaller"  runat="server">
    <asp:Label runat="server" ID="lblFullName1" Text="Full Name" meta:resourcekey="lblFullName1Resource1"></asp:Label>
     
    </asp:TableCell>
    <asp:TableCell  runat="server">
    <asp:TextBox ID="txtName" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1px" BackColor="White"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator14"  runat="server" ControlToValidate="txtName" ErrorMessage="Name: " meta:resourcekey="RequiredFieldValidator14Resource1">*</asp:RequiredFieldValidator>
    </asp:TableCell>        
    </asp:TableRow>    
    <asp:TableRow runat="server">
    <asp:TableCell Width ="150px"  Font-Size="Smaller"  runat="server">
    <asp:Label runat="server" ID="Label2" Text="Badge No" meta:resourcekey="Label2Resource1"></asp:Label>
     
    </asp:TableCell>
    <asp:TableCell  runat="server">
    <asp:TextBox ID="txtBadgeNo" runat="server" Width="80px" BorderStyle="Solid"  BorderWidth="1px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ControlToValidate="txtBadgeNo" ErrorMessage="Badge No. " meta:resourcekey="RequiredFieldValidator1Resource1">*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" ID="regVldBadgeNo" ControlToValidate="txtBadgeNo" ValidationExpression="^[0-9]*"  ErrorMessage="Badge No. " meta:resourcekey="regVldBadgeNoResource1" >*</asp:RegularExpressionValidator>
    <asp:Label Font-Size="Smaller" runat="server" meta:resourcekey="LabelResource1">  Phone / Pager </asp:Label>
  &nbsp;
   <asp:TextBox ID="txtPhone" runat="server" Width="300px" BorderStyle="Solid"  BorderWidth="1px"></asp:TextBox>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ControlToValidate="txtPhone"  ErrorMessage="Pager No. " meta:resourcekey="RequiredFieldValidator2Resource1">*</asp:RequiredFieldValidator>
   </asp:TableCell>        
    </asp:TableRow>
    <asp:TableRow runat="server">
    <asp:TableCell Width ="150px"  Font-Size="Smaller"  runat="server">
   <asp:Label runat="server" ID="Label3" Text="Department" meta:resourcekey="Label3Resource1"></asp:Label>
    
    </asp:TableCell>
    <asp:TableCell runat="server">
    <asp:TextBox ID="txtDepartment" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  runat="server" ControlToValidate="txtDepartment"  ErrorMessage="Department: ">*</asp:RequiredFieldValidator>
    </asp:TableCell>        
    </asp:TableRow>    
     <asp:TableRow runat="server">
    <asp:TableCell Width ="150px"  Font-Size="Smaller"  runat="server">
    <asp:Label runat="server" ID="Label4" Text="Job Title" meta:resourcekey="Label4Resource1"></asp:Label>
    
    </asp:TableCell>
    <asp:TableCell  runat="server">
    <asp:TextBox ID="txtSubject" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ControlToValidate="txtSubject"  ErrorMessage="JobTitle: " meta:resourcekey="RequiredFieldValidator4Resource1">*</asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>
    
    <asp:TableRow runat="server">
    <asp:TableCell Width ="150px"  Font-Size="Smaller"  runat="server">
    <asp:Label runat="server" ID="Label5" Text="Email" meta:resourcekey="Label5Resource1"></asp:Label>
    
    </asp:TableCell>
    <asp:TableCell  runat="server">
    <asp:TextBox ID="txtEmail" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ControlToValidate="txtEmail"  ErrorMessage="Email: " meta:resourcekey="RequiredFieldValidator6Resource1" >*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail" ValidationExpression=".*@.*\..*" ErrorMessage="Email: " meta:resourcekey="revEmailResource1">*</asp:RegularExpressionValidator>
    
    </asp:TableCell>    
    </asp:TableRow>
    <asp:TableRow runat="server">
    <asp:TableCell Width ="150px"  Font-Size="Smaller"  BorderColor="Red" BorderStyle="Solid" BorderWidth="0px" runat="server">
    <asp:Label runat="server" ID="Label6" Text="Choose Online Services" meta:resourcekey="Label6Resource1"></asp:Label>
    
    </asp:TableCell>
    <asp:TableCell Font-Size="Smaller"  BorderColor="Red" BorderStyle="Solid" BorderWidth="0px" runat="server">
      <asp:CheckBox  runat="server" ID="chbEmail"/> 
      <asp:Label runat="server" ID="Label7" Text="E-mail" meta:resourcekey="Label7Resource1"></asp:Label>
       &nbsp;
     <asp:CheckBox  runat="server" ID="chbCurriculum"/> 
     <asp:Label runat="server" ID="Label8" Text="Medicine Curriculum" meta:resourcekey="Label8Resource1"></asp:Label>
           &nbsp;
     <asp:CheckBox  runat="server" ID="chbMedicalEducationBlackBoard"/> 
     <asp:Label runat="server" ID="Label9" Text="Master of Medical Education" meta:resourcekey="Label9Resource1"></asp:Label>
     &nbsp;
     <asp:CheckBox  runat="server" ID="chbLibrary"/> 
     <asp:Label runat="server" ID="Label10" Text="Online Libray System " meta:resourcekey="Label10Resource1"></asp:Label>
                
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    <hr />    &nbsp;
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnFake" PopupControlID="mb" BackgroundCssClass="modalBackground" DropShadow="True" BehaviorID="ModalPopupExtender1" DynamicServicePath="" Enabled="True">
    </ajaxToolkit:ModalPopupExtender>
          &nbsp; &nbsp;&nbsp;


<asp:Panel  ID="mb" runat="server" CssClass="modalPopup" Style="display:none" Width="300px">
<asp:Panel ID="Panel3" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
<div><b>
<asp:Label runat="server" ID="Label11" Text="Confirmation" meta:resourcekey="Label11Resource1"></asp:Label>
</b>
</div>
</asp:Panel>
<div dir="rtl" id="divInfo" runat ="server"><span style="font-family:Tahoma; font-size:small"><b>
<asp:Label runat="server" ID="lblconfirmation" meta:resourcekey="lblconfirmationResource1"></asp:Label></b>
<asp:Label runat="server" ID="lblFileNameHidden" Visible="False"></asp:Label>
<p>
<p>
<asp:LinkButton runat="server" ID="lnkDownloadForm" Text="Click here to Download Request Form" OnClick="DownloadRequestForm" meta:resourcekey="lnkDownloadFormResource1"></asp:LinkButton>
</p>
</p>
</span></div>
<br />
<br />
<div style="text-align:center;">
<asp:Button ID="Button1"  runat="server" Text="OK" ValidationGroup="validConfim" OnClick ="BackToMainSite" Width="80px" meta:resourcekey="Button1Resource1" />
<asp:Button ID="btnFake" style=" display:none" runat="server" Text="Fake"/>
</div>
</asp:Panel>   

    <asp:Table ID="Table21" runat="server" Width="100%">
    <asp:TableRow meta:resourcekey="TableRowResource9" runat="server">
    <asp:TableCell  HorizontalAlign="Center" runat="server">
    <asp:Button runat="server"  ID = "btnSave" Text="Send" OnClick="SendRequest" UseSubmitBehavior="False" meta:resourcekey="btnSaveResource1" />
    <asp:Button runat="server"  CausesValidation="False" Text="Cancel" OnClick="cancelRequest" meta:resourcekey="ButtonResource1" />
    <asp:Button runat="server" ID="btnBack" CausesValidation="False" Text="Back" OnClick="BackToMainSite" meta:resourcekey="btnBackResource1" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton runat ="server" Text="Download Medical Curriculum Agreement" Font-Size="X-Small" CausesValidation="False" OnClick="downLoad" meta:resourcekey="LinkButtonResource1"></asp:LinkButton>
    </asp:TableCell>
     </asp:TableRow>
    <asp:TableRow runat="server">
    <asp:TableCell runat="server">
    <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatusResource1"></asp:Label> &nbsp;
    <asp:LinkButton ID="lbtnDownloadRform" Font-Size="X-Small" Font-Underline="False" CausesValidation="False" Enabled="False" Visible="False" runat="server" Font-Bold="False" OnClick="DownloadRequestForm" meta:resourcekey="lbtnDownloadRformResource1"></asp:LinkButton> 
    <asp:ValidationSummary ID="validSummary" ShowMessageBox="True" HeaderText="You must make a valid entry for the following fields:"
 runat="server" ShowSummary="False" meta:resourcekey="validSummaryResource1" />
    </asp:TableCell>
    </asp:TableRow>
     </asp:Table>    
 </asp:Panel>

<%--<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderRequestForm" CancelControlID="btnRequestFormCancel" runat="server" TargetControlID="btnFake" PopupControlID="pnlRequestForm">
</ajaxToolkit:ModalPopupExtender>--%>
<%--

<asp:Panel runat="server" id="pnlRequestForm" BackColor="white" Width="800" Height="825">
<div style="background-color:blue">
<b>ACCOUNT CREATION REQUEST FORM</b>
</div>
<asp:Panel runat="server" BorderStyle="Solid" BorderColor="black" BorderWidth="1">
<center>
<asp:Table runat="server" Width="100%">
<asp:TableRow>
<asp:TableCell>
<asp:Table runat ="server" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Width="97%">
<asp:TableHeaderRow>
<asp:TableHeaderCell HorizontalAlign="Left" Font-Size="Small" ForeColor="Black">Employee Information</asp:TableHeaderCell>
</asp:TableHeaderRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="1" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
JobRequest No:
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Date Submitted:
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Start Date:
</asp:TableCell>
</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="1" BorderColor="black" >
<asp:TableCell ColumnSpan="2" Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Requestor Name:
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Badge No:
</asp:TableCell>
</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="1" BorderColor="black" >
<asp:TableCell ColumnSpan="2" Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Position/Title:
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Office No:
</asp:TableCell>
</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="1" BorderColor="black" >
<asp:TableCell ColumnSpan="3" Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Department Section:
</asp:TableCell>
</asp:TableRow>
</asp:Table>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>

</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
<asp:Table ID="Table2" runat ="server" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Width="97%">
<asp:TableHeaderRow>
<asp:TableHeaderCell Font-Bold="true" Font-Size="Small" HorizontalAlign="Left">
Authorization
</asp:TableHeaderCell >

</asp:TableHeaderRow>

<asp:TableRow BorderStyle="Solid" BorderWidth="1" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Manager
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Badge No:
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Signature:
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Date:
</asp:TableCell>
</asp:TableRow>
</asp:Table>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell Font-Bold="false">
FOR MIS USE ONLY
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
<asp:Table ID="Table3" runat ="server" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Width="97%">
<asp:TableHeaderRow BorderStyle="Solid" BorderWidth="1" BorderColor="black">
<asp:TableHeaderCell HorizontalAlign="Left" Font-Size="Small" ForeColor="Black">Operating System</asp:TableHeaderCell>
</asp:TableHeaderRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Single Boot:
<asp:CheckBox runat="server"/>
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Dual Boot:
<asp:CheckBox runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Others:______________________
</asp:TableCell>
</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="1" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Windows XP:
<asp:CheckBox  runat="server"/>
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Windows 2000:
<asp:CheckBox runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Others:______________________
</asp:TableCell>
</asp:TableRow>
</asp:Table>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
<asp:Table ID="Table4" runat ="server" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Width="97%">
<asp:TableHeaderRow BorderStyle="Solid" BorderWidth="1" BorderColor="black">
<asp:TableHeaderCell HorizontalAlign="Left" Font-Size="Small" ForeColor="Black">Security and Protection</asp:TableHeaderCell>
</asp:TableHeaderRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
ANTIVIRUS
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Application
<asp:CheckBox ID="CheckBox2" runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Definition File Update:
<asp:CheckBox  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Scanned Computer:
<asp:CheckBox ID="CheckBox1"  runat="server" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
ANTISPYWARE
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Application
<asp:CheckBox ID="CheckBox3" runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Definition File Update:
<asp:CheckBox ID="CheckBox4"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Scanned Computer:
<asp:CheckBox ID="CheckBox5"  runat="server" />
</asp:TableCell>
</asp:TableRow>
</asp:Table>

</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell></asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
<asp:Table ID="Table5" runat ="server" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Width="97%">
<asp:TableHeaderRow BorderStyle="Solid" BorderWidth="1" BorderColor="black">
<asp:TableHeaderCell HorizontalAlign="Left" Font-Size="Small" ForeColor="Black">Drivers</asp:TableHeaderCell>
</asp:TableHeaderRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Acrobat Reader
<asp:CheckBox  runat="server"/>
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
winZip
<asp:CheckBox ID="CheckBox6" runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Live Meeting
<asp:CheckBox ID="CheckBox7"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Breeze:
<asp:CheckBox ID="CheckBox8"  runat="server" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Citrix
<asp:CheckBox  runat="server"/>
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
PC Anywhere
<asp:CheckBox ID="CheckBox9" runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
VPN Client
<asp:CheckBox ID="CheckBox10"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Dial-up
<asp:CheckBox ID="CheckBox11"  runat="server" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
AS/400 Client
<asp:CheckBox ID="CheckBox12"  runat="server"/>
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Unix Client
<asp:CheckBox ID="CheckBox13" runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
NetWare Client
<asp:CheckBox ID="CheckBox14"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Wireless Client
<asp:CheckBox ID="CheckBox15"  runat="server" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell ColumnSpan="4" HorizontalAlign="Left" Font-Size="Small">
Other Application _______________________________________________________________________________________
</asp:TableCell>
</asp:TableRow>
</asp:Table>

</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
<asp:Table ID="Table6" runat ="server" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Width="97%">
<asp:TableHeaderRow BorderStyle="Solid" BorderWidth="1" BorderColor="black">
<asp:TableHeaderCell HorizontalAlign="Left" Font-Size="Small" ForeColor="Black">Extra Software(s)</asp:TableHeaderCell>
</asp:TableHeaderRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Access
<asp:CheckBox ID="CheckBox16"  runat="server"/>
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
FrontPage
<asp:CheckBox ID="CheckBox17" runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Visio
<asp:CheckBox ID="CheckBox18"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Project
<asp:CheckBox ID="CheckBox19"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Note Keeper
<asp:CheckBox ID="CheckBox24"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
SMS
<asp:CheckBox ID="CheckBox25"  runat="server" />
</asp:TableCell>

</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Nero
<asp:CheckBox ID="CheckBox20"  runat="server"/>
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Net Support
<asp:CheckBox ID="CheckBox21" runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Adobe Pro
<asp:CheckBox ID="CheckBox22"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Photoshop
<asp:CheckBox ID="CheckBox23"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
Mind Mapper
<asp:CheckBox ID="CheckBox26"  runat="server" />
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
SIS
<asp:CheckBox ID="CheckBox27"  runat="server" />
</asp:TableCell>
</asp:TableRow>
</asp:Table>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
<asp:Table ID="Table7" runat ="server" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Width="97%">
<asp:TableHeaderRow BorderStyle="Solid" BorderWidth="1" BorderColor="black">
<asp:TableHeaderCell HorizontalAlign="Left" Font-Size="Small" ForeColor="Black">Online Services Access</asp:TableHeaderCell>
</asp:TableHeaderRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
<asp:CheckBox ID="CheckBox28"  runat="server"/>
Master of Medical Education Program
</asp:TableCell>
<asp:TableCell ColumnSpan="2" Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
<asp:CheckBox ID="CheckBox29" runat="server" /> Online Medicine Curriculum(attached signed AGREEMENT)
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
<asp:CheckBox runat="server" /> Online Registration Admin
</asp:TableCell>
<asp:TableCell ColumnSpan="2" Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
<asp:CheckBox ID="CheckBox31" runat="server" /> Online Recruitment System
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
<asp:CheckBox ID="CheckBox32" runat="server" /> Online Library System
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
<asp:CheckBox ID="CheckBox33" runat="server" /> Online HelpDesk Admin
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="0" BorderColor="black">
<asp:CheckBox ID="CheckBox30"  runat="server" /> KSAU-HS E-mail
</asp:TableCell>
</asp:TableRow>
</asp:Table>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
<asp:Table ID="Table9" runat ="server" BorderColor="black" BorderStyle="Solid" BorderWidth="1" Width="97%">
<asp:TableHeaderRow BorderStyle="Solid" BorderWidth="1" BorderColor="black">
<asp:TableHeaderCell HorizontalAlign="Left" Font-Size="Small" ForeColor="Black">Creation</asp:TableHeaderCell>
</asp:TableHeaderRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BackColor="gray" BorderWidth="1" BorderColor="black">
User Name
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="right" BorderStyle="Solid" BorderWidth="1" BackColor="gray" BorderColor="black">
It is a single sign on?
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
<asp:Table runat="server">
<asp:TableRow>
<asp:TableCell>
_____Yes
</asp:TableCell>
<asp:TableCell>
_____No
</asp:TableCell>
</asp:TableRow>

</asp:Table>

</asp:TableCell>
</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BackColor="gray" BorderWidth="1" BorderColor="black">
Temporary Password
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="right" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Specify:
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
&nbsp;
</asp:TableCell>
</asp:TableRow>
<asp:TableRow BorderStyle="Solid" BorderWidth="0" BorderColor="black" >
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BackColor="gray" BorderWidth="1" BorderColor="black">
Created by
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="right" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
Signature:
</asp:TableCell>
<asp:TableCell Font-Size="Small"  HorizontalAlign="left" BorderStyle="Solid" BorderWidth="1" BorderColor="black">
&nbsp;
</asp:TableCell>
</asp:TableRow>

</asp:Table>
</asp:TableCell>
</asp:TableRow>

</asp:Table>
</center>
</asp:Panel>

<asp:Button runat="server" ID="btnRequestFormCancel" CausesValidation="false" Text="Cancel" />
<asp:Button runat="server" id="btnRequestPrint" ValidationGroup="ValidPrint" Text="Print" OnClick="PrintRequest" />
</asp:Panel>
 
--%> 
<asp:Panel ID= "pnlAccountStatus" Width="782px" Height="180px" runat="server" Enabled="False" Visible="False">
<asp:Table ID="Table1" runat="server" Width="100%">
     <asp:TableRow runat="server">
    <asp:TableCell Width ="25%" Font-Size="Smaller"  runat="server" >    
    <asp:Label runat="server" ID="Label12" Text="Badge No. " meta:resourcekey="Label12Resource1"></asp:Label>
    
    &nbsp;&nbsp;
    <asp:TextBox ID="txtBadg" runat="server" Width="80px" BorderStyle="Solid"  BorderWidth="1px" BackColor="White"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="checkStatus"  runat="server" ControlToValidate="txtBadg" meta:resourcekey="RequiredFieldValidator3Resource1">*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtBadg" ValidationExpression="^[0-9]*" ValidationGroup="checkStatus" meta:resourcekey="RegularExpressionValidator1Resource1" >*</asp:RegularExpressionValidator>
    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="checkStatus" ShowMessageBox="True" HeaderText="You must make a valid entry for Badge No"
 runat="server" ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" />    
    </asp:TableCell>
    <asp:TableCell  runat="server">
    <asp:Button runat="server" Text="Check Status" ValidationGroup="checkStatus" meta:resourcekey="ButtonResource2" OnClick="CurrentAccountStatus"></asp:Button>
</asp:TableCell>        
    </asp:TableRow>  
     <asp:TableRow runat="server">
    <asp:TableCell  ColumnSpan="2" Font-Bold="True" Font-Size="Smaller" runat="server">
    <asp:Label runat="server" ID="lblfullName" meta:resourcekey="lblfullNameResource1"></asp:Label>
    </asp:TableCell>
    <asp:TableCell runat="server"></asp:TableCell>
    </asp:TableRow>    
     <asp:TableRow runat="server">
    <asp:TableCell  ColumnSpan="2" Font-Bold="True" Font-Size="Smaller" runat="server">
    <asp:Label runat="server" ID="lblLogin" meta:resourcekey="lblLoginResource1"></asp:Label>
    </asp:TableCell>
    <asp:TableCell runat="server"></asp:TableCell>
    </asp:TableRow>
      
      <asp:TableRow runat="server">
    <asp:TableCell ColumnSpan="2" Font-Bold="True" Font-Size="Smaller" runat="server">
    <asp:Label runat="server" ID="lblpwd" meta:resourcekey="lblpwdResource1"></asp:Label>
        </asp:TableCell>
    <asp:TableCell runat="server"></asp:TableCell>
    </asp:TableRow>
     <asp:TableRow runat="server">
    <asp:TableCell  ColumnSpan="2" Font-Bold="True" Font-Size="Smaller" runat="server">
    <asp:Label runat="server" ID="lblMailID" meta:resourcekey="lblMailIDResource1"></asp:Label>
    </asp:TableCell>
    <asp:TableCell meta:resourcekey="TableCellResource26" runat="server"></asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    <br /> 
</asp:Panel>
        
		<%-- </ContentTemplate>
		</ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>	--%>	
		</div>		
		</div>	
		
</asp:Content>



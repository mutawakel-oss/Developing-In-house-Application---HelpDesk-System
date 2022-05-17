<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmEmailServiceRequest.aspx.cs" Inherits="frmEmailServiceRequest" Title="Email Service Request" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">  

<script type="text/javascript">


</script>

<div id="body" style="width:1000px">
		<div id="col_main_left">
			
			<div>						
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
		
		
		</asp:Table>
		</div>
			
		</div>
		<div id="col_main_right">
	
		<h2 class="section">
		       Email Service Request &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="manStar" Text="*" runat="server" ForeColor="red"></asp:Label> <asp:Label ID="Label1" Text="Mandantory Field" runat="server" Font-Bold="false" Font-Size="Smaller"></asp:Label>             
		         	         
		         </h2>
		          <asp:Label ID="Label2" runat="server" Font-Bold="false"  Font-Size="Smaller" Text="Please fill following details, and click send to submit your Help Request"></asp:Label>		
			     <br />     
		     
		<center>
         
      <asp:Panel ID="pnlNewVendor" CssClass="modalPopup"  BackColor ="#92B27F" Width="782px" Height="502px" runat="server">
    
    <asp:Table ID="Table20" runat="server" Width="100%">
    <asp:TableRow>
    <asp:TableCell Width ="150px" Font-Size="Smaller" HorizontalAlign="Left">
    Name 
    </asp:TableCell>
    <asp:TableCell HorizontalAlign="Left">
    <asp:TextBox ID="txtName" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1" BackColor="white" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator14"  runat="server" ControlToValidate="txtName" ErrorMessage="Name: ">*</asp:RequiredFieldValidator>
    </asp:TableCell>        
    </asp:TableRow>
    
    <asp:TableRow>
    <asp:TableCell Width ="150px"  Font-Size="Smaller" HorizontalAlign="Left">
    Badge No 
    </asp:TableCell>
    <asp:TableCell HorizontalAlign="Left">
    <asp:TextBox ID="txtBadgeNo" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ControlToValidate="txtBadgeNo" ErrorMessage="Badge No. ">*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" ID="regVldBadgeNo" ControlToValidate="txtBadgeNo" ValidationExpression="^[0-9]*"  ErrorMessage="Badge No. " >*</asp:RegularExpressionValidator>
  </asp:TableCell>        
    </asp:TableRow>
    
    <asp:TableRow>
    <asp:TableCell Width ="150px"  Font-Size="Smaller" HorizontalAlign="Left">
    Phone / Pager 
    </asp:TableCell>
    <asp:TableCell HorizontalAlign="Left">
    <asp:TextBox ID="txtPhone" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ControlToValidate="txtPhone"  ErrorMessage="Pager No. ">*</asp:RequiredFieldValidator>
    </asp:TableCell>        
    </asp:TableRow>
    <asp:TableRow>
    <asp:TableCell Width ="150px"  Font-Size="Smaller" HorizontalAlign="Left">
    Department
    </asp:TableCell>
    <asp:TableCell HorizontalAlign="Left">
    <asp:TextBox ID="txtDepartment" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  runat="server" ControlToValidate="txtDepartment"  ErrorMessage="Department: " >*</asp:RequiredFieldValidator>
    </asp:TableCell>        
    </asp:TableRow>
    
     <asp:TableRow>
    <asp:TableCell Width ="150px"  Font-Size="Smaller" HorizontalAlign="Left">
    Subject
    </asp:TableCell>
    <asp:TableCell HorizontalAlign="Left">
    <asp:TextBox ID="txtSubject" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1" ></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ControlToValidate="txtSubject"  ErrorMessage="Subject: ">*</asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>
    
    <asp:TableRow>
    <asp:TableCell Width ="150px"  Font-Size="Smaller" HorizontalAlign="Left">
    Email
    </asp:TableCell>
    <asp:TableCell HorizontalAlign="Left">
    <asp:TextBox ID="txtEmail" runat="server" Width="500px" BorderStyle="Solid"  BorderWidth="1"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ControlToValidate="txtEmail"  ErrorMessage="Email: " >*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail" ValidationExpression=".*@.*\..*" ErrorMessage="Email: "  Display="Static">*</asp:RegularExpressionValidator>
 
    </asp:TableCell>    
    </asp:TableRow>
    
    <asp:TableRow>
    <asp:TableCell Width ="150px"  Font-Size="Smaller" HorizontalAlign="Left" VerticalAlign="Top">
    Request Description 
    </asp:TableCell>
    <asp:TableCell HorizontalAlign="Left">
    <asp:TextBox ID="txtMessage" runat="server" Width="500px" TextMode="MultiLine" Height="250px" BorderStyle="Solid"  BorderWidth="1" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" ControlToValidate="txtMessage"  ErrorMessage="Description: ">*</asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>   
    </asp:Table>
    <hr />
    
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
TargetControlID="btnFake"   
 PopupControlID="mb"
BackgroundCssClass="modalBackground"
DropShadow="true">
</ajaxToolkit:ModalPopupExtender>

<asp:Panel  ID="mb" runat="server" CssClass="modalPopup" Style="display:none" Width="300">
<asp:Panel ID="Panel3" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
<div><b>
Confirmation
</b>
</div>
</asp:Panel>
<b>
<div dir="rtl" id="divInfo" runat ="server"><span style="font-family:Tahoma; font-size:small">
<asp:Label runat="server" ID="lblconfirmation"></asp:Label>
</span></div></b>
<br />
<br />
<div style="text-align:center;">

<asp:Button ID="Button1"  runat="server" Text="OK" ValidationGroup="validConfim" OnClick ="BackToMainSite" Width="80px" />
</div>
</asp:Panel>
   
    <asp:Table ID="Table21" runat="server">
    <asp:TableRow>
    <asp:TableCell HorizontalAlign="Center">
    <asp:Button runat="server" ID = "btnSave" Text="Send" OnClick="SendRequest" UseSubmitBehavior="false" />
    <asp:Button runat="server" ID="btnNewVendor" CausesValidation="false" Text="Cancel" OnClick="cancelRequest" />
    <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="BackToMainSite" />
    <asp:Button ID="btnFake" Style="display: none;" runat="server" Text="Fake" />
    </asp:TableCell>
     </asp:TableRow>
    <asp:TableRow>
    <asp:TableCell>
    <asp:Label ID="lblStatus" runat="server"></asp:Label>
    <asp:ValidationSummary ID="validSummary" ShowMessageBox="true" HeaderText="You must enter a valid entry for the following fields:"
 runat="server" ShowSummary="false" />
    </asp:TableCell>
    </asp:TableRow>
     </asp:Table>    
 </asp:Panel>
      
       </center>   
		<%-- </ContentTemplate>
		</ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>	--%>	
		</div>		
		</div>
		
</asp:Content>


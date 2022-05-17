<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmUserComplain.aspx.cs" Inherits="frmNewUserAccount" Title="HelpDesk|User Feedback" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">  
<script  type="text/javascript">
</script>

<div   id="body"  style="width:1000px">
		<div id="col_main_left">
			
			
		</div>
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' id="col_main_right">	
		<h2 class="section">
		       <asp:Label  ID = "lblHeading" Text="User Feedback" runat="server" ></asp:Label>		       
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <asp:Label ID="manStar" Text="*" runat="server" ForeColor="Red" meta:resourcekey="manStarResource1"></asp:Label> <asp:Label ID="Label1" Text="Mandantory Field" runat="server" Font-Bold="False" Font-Size="Smaller" meta:resourcekey="Label1Resource1"></asp:Label>             		         	         
        
		         </h2>
		          <div id="Div1"  runat="server"    class="content_right"  >
		          <asp:Panel ID="pnlNewVendor" CssClass="modalPopup"   BorderStyle="Double" runat="server" Width="777px">    
		          
		         <asp:Table ID="tblComplain" runat=server>
		         <asp:TableRow>
		         <asp:TableCell ColumnSpan=2>
                     <asp:RadioButtonList ID="rdCategory" runat="server" RepeatDirection=horizontal>
                     <asp:ListItem Text="Comment" Value="1" Selected=true></asp:ListItem>
                     <asp:ListItem Text="Suggestion" Value="2"></asp:ListItem>
                     <asp:ListItem Text="Complain" Value="3"></asp:ListItem>
                     </asp:RadioButtonList>
		         </asp:TableCell>
		         </asp:TableRow>
		         <asp:TableRow>
		         <asp:TableCell >
		         <asp:Label ID="lblSubject" runat=server Text="Subject"></asp:Label>
		         
		         </asp:TableCell>
		         <asp:TableCell>
		         <asp:TextBox ID="txtSubject" runat=server Width="300px"></asp:TextBox>
		         <asp:RequiredFieldValidator ID="subjectValidator" runat=server ControlToValidate="txtSubject" ErrorMessage="*" ValidationGroup="complainGroup"></asp:RequiredFieldValidator>
		         </asp:TableCell>
		         </asp:TableRow>
		         <asp:TableRow>
		         <asp:TableCell>
		         <asp:Label ID="lblDescription" runat=server Text="Description"></asp:Label>
		         </asp:TableCell>
		         <asp:TableCell>
		         <asp:TextBox ID="txtDescription" runat=server TextMode=multiLine Height="200px" Width="400px"></asp:TextBox>
		         <asp:RequiredFieldValidator ID="descriptionValidator" runat=server ControlToValidate="txtDescription" ErrorMessage="*" ValidationGroup="complainGroup"></asp:RequiredFieldValidator>
		         </asp:TableCell>
		         </asp:TableRow>
		         <asp:TableRow>
		         <asp:TableCell>
		         </asp:TableCell>
		         <asp:TableCell>
		         <asp:Button ID="btnSubmit" runat=server Text="Send" CausesValidation=true ValidationGroup="complainGroup"  OnClick="mSubmitComplain"/>
		         
		       &nbsp;&nbsp;&nbsp;&nbsp;
		         <asp:Button ID="btnGoToLogin" runat=server Text="Back" PostBackUrl="~/frmUserRequestList.aspx" />
		         </asp:TableCell>
		         </asp:TableRow>
		         </asp:Table>
		         </asp:Panel>
		</div>
      
		</div>	
		    
            
		</div>	
		
</asp:Content>



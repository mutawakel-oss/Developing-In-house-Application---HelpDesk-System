<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmMessage.aspx.cs" Inherits="frmNewUserAccount" Title="HelpDesk|New User Account" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>


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
					<hr />
					</h3>		
					<br /><br /><br />	
			</div>				
			
		</div>
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' id="col_main_right">	
		<h2 class="section">
		       <asp:Label  ID = "lblHeading" Text="Complain Sent" runat="server" ></asp:Label>		       
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;              		         	         
        
		         </h2>
		          <div id="Div1"  runat="server"    class="content_right"  >
		       <asp:Label ID="txtMessage" runat=server  ForeColor=red Font-Size=medium></asp:Label>
		       
		       <asp:Button ID="btnGoToLogin" runat=server Text="Back" PostBackUrl="~/frmUserRequestList.aspx" />
		</div>
		</div>	
		</div>	
		
</asp:Content>



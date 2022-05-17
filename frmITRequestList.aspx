<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmITRequestList.aspx.cs" Inherits="frmUserRequestList" Title="Help Desk" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

<div  id="body"  style="width:1000px">
		<div id="col_main_left">
			
			<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>						
		<asp:Table runat="server" Width="100%" ID="Table8"  BackColor="#EBEBEB">		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">		
		<asp:ImageButton ID="hlkUserList" runat="server" ImageUrl="~/Images/User Management.png"  PostBackUrl="~/frmLdapUsers.aspx" Width="190px" Visible=false />
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">		
		<asp:ImageButton ID="hlkAdminRequestList" runat="server" ImageUrl="~/Images/Own_Requests.png" PostBackUrl="~/frmAdminRequestList.aspx" Width="190px" Visible=false />
		</asp:TableCell>
		</asp:TableRow>	
		<asp:TableRow>
		<asp:TableCell>
		<asp:ImageButton ID="lnkHomePage" runat="server" ImageUrl="~/Images/Home Page.png" OnClick="DisplayHomePage" Width="190px" />
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>
		<asp:ImageButton ID="lbtnTaskList" runat="server" ImageUrl="~/Images/Task List.png" OnClick="DisplayTaskList" Width="190px" />
		</asp:TableCell>
		</asp:TableRow>	
		<asp:TableRow>
		<asp:TableCell>
		<asp:ImageButton ID="lnkOwnRequest" runat="server" ImageUrl="~/Images/Own_Requests.png" OnClick="DisplayOwnRequest" Width="190px" />
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">	
		<asp:ImageButton ID="hlkNewRequest" runat="server" ImageUrl="~/Images/New_Helpdesk_Request.png" OnClick="NewRequest" Width="190px" />
		</asp:TableCell>
		</asp:TableRow>					
		</asp:Table>
		</div>
		<div style=" background-color:#EBEBEB">
		<asp:Image runat="server" ImageUrl="~/Images/status.GIF" ID="imgDescription" meta:resourcekey="imgDescriptionResource1" />		
		</div>	
				
			
		</div>
		<div id="col_main_right">
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' style="border-color:blue; background-color:#EBEBEB">
		
		<asp:Table ID="Table5" runat="server">
		<asp:TableRow runat="server">		
		<asp:TableCell Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label1" Text="Login user :" meta:resourcekey="Label1Resource1"></asp:Label>
		<asp:Label ID="lblLogUser" runat="server" ForeColor="Blue" Width="180px" meta:resourcekey="lblLogUserResource1"></asp:Label>
		</asp:TableCell>		
		<asp:TableCell Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label2" Text="Badge #:" meta:resourcekey="Label2Resource1"></asp:Label>
		<asp:Label ID="lblBadgeNo" runat="server"  ForeColor="Blue" Width="70px" meta:resourcekey="lblBadgeNoResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label3" Text="Department:" meta:resourcekey="Label3Resource1"></asp:Label>
		<asp:Label ID="lblDepartment" runat="server"  ForeColor="Blue" Width="140px" meta:resourcekey="lblDepartmentResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label4" Text="Title:" meta:resourcekey="Label4Resource1"></asp:Label>
		<asp:Label ID="lblTitle" runat="server"  ForeColor="Blue" Width="140px" meta:resourcekey="lblTitleResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell Font-Size="8pt" runat="server">
		<asp:LinkButton ID="lbtnUserInfoEdit" runat="server" Text="Edit" OnClick="EditLogUserInfo" meta:resourcekey="lbtnUserInfoEditResource1" ></asp:LinkButton>		
		</asp:TableCell>		
		</asp:TableRow>
		</asp:Table>
		</div>	
		
		
		<h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section">
		<asp:Label runat="server" ID="Label5" Text="Requests" meta:resourcekey="Label5Resource1"></asp:Label>
				</h2>		
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
		<table width="100%"><tr><td>
		    <asp:GridView ID="gdvRequestList" runat="server" Height="75px" RowStyle-Height="5px" 
            Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="True" PageSize="29" OnPageIndexChanging="gdvRequestList_PageIndexChanging" meta:resourcekey="gdvRequestListResource1">
            <PagerSettings PageButtonCount="15" />
           
           <Columns>                
           <asp:HyperLinkField  HeaderText="Request ID" 
           DataTextField= "id" Target="_parent" DataNavigateUrlFields="id" 
           DataNavigateUrlFormatString= "frmRequestDetails.aspx?id={0}" meta:resourcekey="HyperLinkFieldResource1" >
               <ItemStyle HorizontalAlign="Center" />
           </asp:HyperLinkField> 
                   
           <asp:BoundField DataField="description" HeaderText="Description" meta:resourcekey="BoundFieldResource1" >
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>
           <asp:BoundField DataField="category" HeaderText="Category" meta:resourcekey="BoundFieldResource2" >
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle Width="100px" />
           </asp:BoundField>
           <asp:BoundField DataField="assigned_to" HeaderText="Assigned To" meta:resourcekey="BoundFieldResource3">
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>
           <asp:BoundField DataField="Status" headertext="Current Status" meta:resourcekey="BoundFieldResource4" >
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>
           <asp:BoundField DataField="created_date" headertext="Requested On" HtmlEncode="False" DataFormatString="{0:dd/MMM/yyyy ddd,hh:mm:ss tt}" meta:resourcekey="BoundFieldResource5">
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>                     
           </Columns>
                <RowStyle Height="5px" />
            <EditRowStyle BackColor="#999999" />
           
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Middle" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>		
		</td></tr></table>	</div>	
		</div>		
    <asp:Label ID="languageLabel" runat="server" Text="Label" Visible="False"></asp:Label></div>		
		
</asp:Content>




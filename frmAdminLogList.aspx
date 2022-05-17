<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" 
AutoEventWireup="true" CodeFile="frmAdminLogList.aspx.cs" Inherits="Default2" Title="Help Desk" %>
<%@register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

<div id="body" style="width:1000px" >
<div id="col_main_left">
			
			<div>
						
		<asp:Table Width="100%" ID="Table1"  BackColor="#EBEBEB" runat="server">
		<asp:TableRow>
		<asp:TableCell>
		
		<asp:HyperLink runat="server" Text="User Management" NavigateUrl="~/frmLdapUsers.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>
        <asp:LinkButton ID="hlkNewRequest" runat="server" Text="New Request" OnClick="NewRequest"  Font-Size="smaller" Font-Bold="false"  ForeColor="black" Font-Underline="false" ></asp:LinkButton>
		</asp:TableCell>
		</asp:TableRow>
		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkUserRequestList" runat="server" Text="Own Requests" NavigateUrl="~/frmUserRequestList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkTechStatus" runat="server" Text="Technicians Status" NavigateUrl="~/frmTechAvailability.aspx"     Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink1" runat="server" Text="Report" NavigateUrl="~/frmRequestHandledReport.aspx"     Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		
		</asp:Table>
		
		</div>		
		<%--<div style=" background-color:#EBEBEB">
		<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/status.GIF" />
		
		</div>--%>
		
		</div>		
		<div id="col_main_right">
		<div style="border-color:blue; background-color:#EBEBEB">
		<asp:Table ID="Table5" runat="server">
		<asp:TableRow ID="TableRow1" runat="server">		
		<asp:TableCell ID="TableCell1" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label1" Text="Login user :" meta:resourcekey="Label1Resource1"></asp:Label>
		<asp:Label ID="lblLogUser" runat="server" ForeColor="Blue" Width="180px" meta:resourcekey="lblLogUserResource1"></asp:Label>
		</asp:TableCell>		
		<asp:TableCell ID="TableCell2" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label2" Text="Badge #:" meta:resourcekey="Label2Resource1"></asp:Label>
		<asp:Label ID="lblBadgeNo" runat="server"  ForeColor="Blue" Width="70px" meta:resourcekey="lblBadgeNoResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell ID="TableCell3" Font-Size="8pt" runat="server"> 
		<asp:Label runat="server" ID="Label3" Text="Department:" meta:resourcekey="Label3Resource1"></asp:Label>
		<asp:Label ID="lblDepartment" runat="server"  ForeColor="Blue" Width="140px" meta:resourcekey="lblDepartmentResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell ID="TableCell4" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label4" Text="Title:" meta:resourcekey="Label4Resource1"></asp:Label>
		<asp:Label ID="lblTitle" runat="server"  ForeColor="Blue" Width="140px" meta:resourcekey="lblTitleResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell ID="TableCell5" Font-Size="8pt" runat="server">
		<asp:LinkButton ID="lbtnUserInfoEdit" runat="server" Text="Edit" OnClick="EditLogUserInfo" meta:resourcekey="lbtnUserInfoEditResource1" ></asp:LinkButton>		
		</asp:TableCell>		
		</asp:TableRow>
		</asp:Table>
		</div>		
		
		<h2 class="section" align="left">
		Date:
		<asp:TextBox id= "txtDate"  Width="100px" runat="server" ></asp:TextBox>
	<asp:Image ID="imgStartDate" runat="server"  ImageUrl="~/Images/date.gif"/>
	<ajaxToolkit:CalendarExtender Format="yyyy-MM-dd" id="calDeliveryDt" runat="server" PopupButtonID="imgStartDate" TargetControlID="txtDate" BehaviorID="calStartDateDt" Enabled="True"></ajaxToolkit:CalendarExtender>
	<asp:RegularExpressionValidator ID="startDateVlidator" runat="server" ControlToValidate="txtDate" ErrorMessage="Format Error" ValidationExpression="\d{4}\-\d{2}\-\d{2}" ValidationGroup="dateGroup" ></asp:RegularExpressionValidator>
	<asp:RequiredFieldValidator ID="dataValidator" runat="server" ControlToValidate="txtDate" ErrorMessage="*" ValidationGroup="dateGroup"></asp:RequiredFieldValidator>
	<asp:Button ID="btnSearchLog" runat="server" Text="Search" CausesValidation="true" ValidationGroup="dateGroup" OnClick="mSearchLog" />
		
				 </h2>
		<table width="100%"><tr><td>
		    <asp:GridView ID="logGrid" runat="server" Height="75px" RowStyle-Height="5px" 
            Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" GridLines="Both" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="true" PageSize="29" >
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <Columns>                
            <asp:BoundField DataField="id" headertext="Log ID" ItemStyle-Width="5%" />    
           <asp:BoundField DataField="done_by" HeaderText="Done By" ItemStyle-Width="15%" />
           <asp:BoundField DataField="affected_by" headertext="Affected By" ItemStyle-Width="15%"  />
           <asp:BoundField DataField="previous_status" headertext="Previous Status"  ItemStyle-Width="25%"/>    
           <asp:BoundField DataField="new_status" headertext="New Status" ItemStyle-Width="25%" />    
           <asp:BoundField DataField="date" headertext="Date/Time" ItemStyle-Width="20%" />    
            
            </Columns>
            <RowStyle BackColor="#F7F6F3" BorderStyle="Solid" BorderWidth="1px" ForeColor="#333333" Height="10px" HorizontalAlign="Center" VerticalAlign="Middle" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Middle" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <PagerSettings PageButtonCount="15" />
        </asp:GridView>
        
   
		
		</td></tr></table>
		
		
		</div>		
		</div>
</asp:Content>



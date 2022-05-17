<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmTechRequestList.aspx.cs" Inherits="frmTechRequestList" Title="Help Desk" %>
<%@ register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
<div id="body" style="width:1000px" >
<div id="col_main_left">
			
			<div>						
		<asp:Table Width="100%" ID="Table1"  BackColor="#EBEBEB" runat="server">
		<asp:TableRow>
		<asp:TableCell>		
		<%--<asp:HyperLink ID="hlkNewProduct" runat="server" Text=" New Product" NavigateUrl="~/frmInvProductDetails.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>--%>
		</asp:TableCell>
		</asp:TableRow>		
				
		</asp:Table>
		</div>
			
			
			<div style=" background-color:#EBEBEB">
		<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/status.GIF" />
		
		</div>
		
		
		
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
		<asp:ImageButton ID="imgButEdit" runat="server" ImageUrl="~/Images/edit.png" OnClick="EditLogUserInfo" width="60px" />
		</asp:TableCell>		
		</asp:TableRow>
		</asp:Table>
		</div>
		
		
		<h2 class="section">
		<asp:Table ID="taskPerioud" runat=server >
		<asp:TableRow>
		<asp:TableCell>
		<asp:LinkButton ID="btnClear" runat=server Text="Clear Dates" Font-Size=smaller OnClick="mClearDates"></asp:LinkButton>
		&nbsp;&nbsp;
		<asp:Label ID="lblStartDate" runat=server Text="Start Date"></asp:Label>
		</asp:TableCell>
		<asp:TableCell>
		<asp:TextBox id= "txtStartDate"  Width="100px" runat="server" ></asp:TextBox>
	<asp:Image ID="imgStartDate" runat="server"  ImageUrl="~/Images/date.gif"/>
	<ajaxToolkit:CalendarExtender Format="yyyy-MM-dd" id="calDeliveryDt" runat="server" PopupButtonID="imgStartDate" TargetControlID="txtStartDate" BehaviorID="calStartDateDt" Enabled="True"></ajaxToolkit:CalendarExtender>
	<asp:RegularExpressionValidator ID="startDateVlidator" runat="server" ControlToValidate="txtStartDate" ErrorMessage="*" ValidationExpression="\d{4}\-\d{2}\-\d{2}" ValidationGroup="dateGroup" ></asp:RegularExpressionValidator>
		</asp:TableCell>
		<asp:TableCell>
		<asp:Label ID="lblEndDate" runat=server Text="End Date"></asp:Label>
		</asp:TableCell>
		<asp:TableCell>
<asp:TextBox id= "txtEndDate"  Width="100px" runat="server" ></asp:TextBox>
	<asp:Image ID="imgEndDate" runat="server"  ImageUrl="~/Images/date.gif"/>
	<ajaxToolkit:CalendarExtender Format="yyyy-MM-dd" id="CalendarExtender1" runat="server" PopupButtonID="imgEndDate" TargetControlID="txtEndDate" BehaviorID="calEndDate" Enabled="True"></ajaxToolkit:CalendarExtender>		
	<asp:RegularExpressionValidator ID="endDateValidator" runat="server" ControlToValidate="txtEndDate" ErrorMessage="*" ValidationExpression="\d{4}\-\d{2}\-\d{2}" ValidationGroup="dateGroup" ></asp:RegularExpressionValidator>

		</asp:TableCell>
		<asp:TableCell>
		<asp:Label ID="lblStatus" runat=server Text="Status"></asp:Label>
		</asp:TableCell>
		<asp:TableCell>
		<asp:DropDownList id="ddlStatusFilter" runat="server"  ></asp:DropDownList>
		</asp:TableCell>
		<asp:TableCell>
		<asp:Button ID="btnSearch" runat=server Text="Search" OnClick="btnSearchClicked" ValidationGroup="dateGroup" CausesValidation=true />
		</asp:TableCell>
		</asp:TableRow>
		</asp:Table>
				 </h2>
		<table width="100%">
		<tr>
		<td>
		<asp:Label ID="lblTasksNoTitle" runat=server Text="Total Tasks No: " ForeColor=red></asp:Label>
		<asp:Label ID="lblTaskNo" runat=server ></asp:Label>
		</td>
		
		</tr>
		<tr><td>
		    <asp:GridView ID="gdvTechRequests" runat="server" Height="75px" RowStyle-Height="4px" 
            Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" GridLines="Both" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="true" PageSize="29" OnPageIndexChanging="gdvTechRequests_PageIndexChanging">
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <Columns>                
           <asp:HyperLinkField  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%"  HeaderStyle-Width="55px" HeaderStyle-Height="4px" HeaderText="Request ID" DataTextField= "id" Target="_parent" datanavigateurlfields ="id" datanavigateurlformatstring= "frmRequestDetails.aspx?id={0}" /> 
           <asp:BoundField DataField="full_name" headertext="Requester Name" ItemStyle-Width="30%"  />
           <asp:BoundField DataField="category" HeaderText="Category" ItemStyle-Width="10%" />
           <asp:BoundField DataField="priority" headertext="Priority" ItemStyle-Width="10%" />  
           <asp:BoundField DataField="created_date" headertext="Requested on" HtmlEncode="false" DataFormatString="{0:dd/MMM/yyyy ddd,hh:mm:ss tt}" ItemStyle-Width="20%" />   
           <asp:BoundField DataField="assigned_date" HeaderText="Assigned On" HtmlEncode="false" DataFormatString="{0:dd/MMM/yyyy ddd,hh:mm:ss tt}" ItemStyle-Width="20%" />
             
            
            </Columns>
            <RowStyle BackColor="#F7F6F3" BorderStyle="Solid" BorderWidth="1px" ForeColor="#333333" Height="10px" HorizontalAlign="Center" VerticalAlign="Middle" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Middle" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <PagerSettings PageButtonCount="15" />
        </asp:GridView>
        
        <asp:GridView ID="gdvAssignedRequests" runat="server" Height="75px" RowStyle-Height="5px"  Enabled="false" Visible="false"
            Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" GridLines="Both" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="true" PageSize="29">
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <Columns>                
           <asp:HyperLinkField  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%"  HeaderStyle-Width="55px" HeaderStyle-Height="5px" HeaderText="Request ID" DataTextField= "id" Target="_parent" datanavigateurlfields ="id" datanavigateurlformatstring= "frmRequestDetails.aspx?id={0}" /> 
           <asp:BoundField DataField="category" HeaderText="category" ItemStyle-Width="10%" />
           <asp:BoundField DataField="assignedto" headertext="Assigned to" ItemStyle-Width="25%"  />
           <asp:BoundField DataField="status" HeaderText="Status " ItemStyle-Width="10%" />
           <asp:BoundField DataField ="full_name" HeaderText="Requester" ItemStyle-Width="25%" />
           <asp:BoundField DataField="created_date" headertext="created on" ItemStyle-Width="20%" />    
           
            
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

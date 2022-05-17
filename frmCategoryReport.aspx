<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmCategoryReport.aspx.cs" Inherits="frmCategoryReport" Title="Help Desk | Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
<div id="body" style="width:1000px" >
<%--<div id="col_main_left">
			<div id="user_assistance">
				<a id="content_start"></a>
				<h3>
					Help and Other Links</h3>								
			</div>
		<div>						
		<asp:Table Width="100%" ID="Table1"  BackColor="#EBEBEB" runat="server">
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkNewProduct" runat="server" Text=" New Product" NavigateUrl="~/frmInvProductDetails.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>				
		</asp:TableCell>
		</asp:TableRow>
		</asp:Table>
		</div>
		</div>--%>
		<div id="div1">
		<div style="border-color:blue; background-color:#EBEBEB">
		<asp:Table ID="Table5" runat="server">		
		<asp:TableRow ID="TableRow1" runat="server">		
		<asp:TableCell>
		<asp:DropDownList ID="ddlReport" Width="150px" runat="server" AutoPostBack="true">
		<asp:ListItem Value="0" Text="--Select Report Type-- "></asp:ListItem>
		<asp:ListItem Value="1" Text="Category wise"></asp:ListItem>
		<asp:ListItem Value="2" Text="Item Model wise"></asp:ListItem>
		</asp:DropDownList>
		</asp:TableCell>
		<asp:TableCell>
		&nbsp;&nbsp;&nbsp;&nbsp;
		</asp:TableCell>
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
		<asp:TableCell ID="TableCell6" Font-Size="8pt" runat="server">
		&nbsp;&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnBack" Text="Back" OnClick="BackToProductList" />
		</asp:TableCell>	
		</asp:TableRow>
		</asp:Table>
		</div>			
			<div>
			 <CR:CrystalReportViewer EnableDatabaseLogonPrompt="false"  ToolPanelWidth="130"   BestFitPage="True" ID="CrystalReportViewer1" runat="server" AutoDataBind="true" HasZoomFactorList="False" />       
			</div>	
			
		</div>		
		</div>

</asp:Content>


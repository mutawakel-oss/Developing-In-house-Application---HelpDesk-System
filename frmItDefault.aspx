<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmItDefault.aspx.cs" Inherits="frmTechnicianDefault" Title="Help Desk |IT Member Home Page" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
<div id="body" style="width:1000px">
		<div id="col_main_left">
			
			<div>						
		<asp:Table Width="100%" ID="Table2"  BackColor="#EBEBEB" runat="server">
	   
	          <asp:TableRow>
		        <asp:TableCell>
		        <asp:HyperLink ID="hlkInventory" runat="server" Text=" Inventory"  NavigateUrl="~/frmInvProductList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="blue" Font-Underline="false" Visible=false></asp:HyperLink>
                 	</asp:TableCell>		        
		    </asp:TableRow>	          
				
		</asp:Table>
		</div>
			<div>						
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
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
		<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Own_Requests.png" OnClick="DisplayOwnRequest" Width="190px" />
		</asp:TableCell>
		</asp:TableRow>
	    <asp:TableRow ID="TableRow2" runat="server">
		<asp:TableCell ID="TableCell6" runat="server">	
		<asp:ImageButton ID="hlkNewRequest" runat="server" ImageUrl="~/Images/New_Helpdesk_Request.png" OnClick="NewRequest" Width="190px" />
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow>
		<asp:TableCell>
		<asp:ImageButton ID="lnkAdminPage" runat="server" ImageUrl="~/Images/Admin page.png" OnClick="mAdminPageClicked" Width="190px" Visible=false />
		</asp:TableCell>
		</asp:TableRow>
		</asp:Table>
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
		<asp:ImageButton ID="lbtnUserInfoEdit" runat="server" ImageUrl="~/Images/edit.png" OnClick="EditLogUserInfo" Width="60px" />
		</asp:TableCell>		
		</asp:TableRow>
		</asp:Table>
		</div>		
		
		<div class="content_right">
		
		
			</div>
			
			<asp:Table ID="Table1"  runat="server" Width="700px" Height="150px">
			<asp:TableRow>
			<asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
			
			 <asp:Table ID="table3"  runat="server" CellPadding="0" CellSpacing="0" BorderColor="ActiveBorder" BorderStyle="Solid" BorderWidth="0" Width="500px">
			 <asp:TableHeaderRow BorderStyle="Solid" BackColor="ActiveCaption" BorderColor="ActiveBorder">
			 <asp:TableCell Font-Bold="true" Font-Size="Smaller" ForeColor="white" HorizontalAlign="Center" ColumnSpan="3" BorderStyle="None" >
			 Task Summary
			 </asp:TableCell>
			 </asp:TableHeaderRow>
			 
			  <asp:TableRow  Height="40px" BackColor="#EBEBEB">
		        <asp:TableCell VerticalAlign="Middle" Font-Size="X-Small" HorizontalAlign="Left" BorderStyle="None">
		        <asp:Image ID="img_open" ToolTip="Click the link to open see Assigned" runat="server" ImageUrl="~/Images/summary_open.gif" />		        
		        </asp:TableCell>
		        <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Left">
		            <asp:LinkButton id = "lbtnAssignedTasks" Text="Escalated Tasks"  Font-Size="Small" runat="server"  ForeColor="Black" Font-Underline="false" OnClick="DisplayTaskList"></asp:LinkButton>
		        </asp:TableCell>
		        <asp:TableCell Font-Size="X-Small" HorizontalAlign="Center" BorderStyle="None">
		        <asp:LinkButton id = "lbtnAssignedTaskCount" Text="0"  Font-Size="Small"  runat="server"  ForeColor="Black" Font-Underline="false" OnClick="DisplayTaskList"></asp:LinkButton>
		        </asp:TableCell>
		        </asp:TableRow>
		        <asp:TableRow Height="30px" BackColor="#F4F4F4">
		         <asp:TableCell VerticalAlign="Middle" Font-Size="X-Small" HorizontalAlign="Left" BorderStyle="None">
		        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/summary_duetoday.gif" />		        
		        </asp:TableCell>
		        <asp:TableCell  HorizontalAlign="Left" BorderStyle="None">
		        <asp:LinkButton id = "lbtnRequestOverDue" Text="Requests Over Due" Font-Size="Small"  runat="server" ForeColor="Black" Font-Underline="false" OnClick="DisplayOverDueTaskList"></asp:LinkButton>
		        </asp:TableCell>
		        <asp:TableCell  HorizontalAlign="Center" BorderStyle="None">
		        <asp:LinkButton id = "lbtnRequestOverDueCount" Text="0 " Font-Size="Small" runat="server" ForeColor="Black" Font-Underline="false" OnClick="DisplayOverDueTaskList"></asp:LinkButton>
		        </asp:TableCell>
		        </asp:TableRow>		        
		    </asp:Table>			 
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table> 
		    	
		</div>		
		</div>		
		
</asp:Content>





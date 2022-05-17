<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmAdminDefault.aspx.cs" Inherits="frmAdminDefault" Title="Help Desk | Admin Options" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
<div id="body" style="width:1000px">
		<div id="col_main_left">
			
			<div>						
                <asp:Table Width="100%" ID="Table2" BackColor="#EBEBEB" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ImageButton ID="LinkButton8" runat="server" ImageUrl="~/Images/User Management.png"
                                Width="190px" PostBackUrl="~/frmLdapUsers.aspx" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ImageButton ID="LinkButton7" runat="server" ImageUrl="~/Images/Inventory.png"
                                PostBackUrl="~/frmInvProductList.aspx" Width="190px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ImageButton ID="LinkButton6" runat="server" ImageUrl="~/Images/Request Management.png"
                                PostBackUrl="~/frmAdminRequestList.aspx" Width="190px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ImageButton ID="LinkButton5" runat="server" ImageUrl="~/Images/New_Helpdesk_Request.png"
                                OnClick="NewRequest" Width="190px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ImageButton ID="lnkVeralRequest" runat="server" ImageUrl="~/Images/verbal.png"
                                PostBackUrl="~/frmmanualrequest.aspx" Width="190px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ImageButton ID="hlkTechStatus" runat="server" ImageUrl="~/Images/Technicians Status.png"
                                PostBackUrl="~/frmTechAvailability.aspx" Width="190px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ImageButton ID="lnkComplainCheck" runat="server" ImageUrl="~/Images/complain_check.png"
                                PostBackUrl="~/frmrcomplaincheck.aspx" Width="190px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ImageButton ID="lnkReport" runat="server" ImageUrl="~/Images/Reports.png" PostBackUrl="~/frmRequestHandledReport.aspx"
                                Width="190px" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ImageButton ID="lnkSystemLog" runat="server" ImageUrl="~/Images/system_log.png"
                                PostBackUrl="~/frmAdminLogList.aspx" Width="190px" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
		</div>
	    <div>						
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkUserList" runat="server" Text="User List" NavigateUrl="~/frmUserList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false" Enabled="false" Visible="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkUserRequestList" runat="server" Text="Requests" NavigateUrl="~/frmUserRequestList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false" Enabled="false" Visible="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkAdminRequestList" runat="server" Text="All Requests" NavigateUrl="~/frmAdminRequestList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false" Enabled="false" Visible="false"></asp:HyperLink>
		
		<%--<asp:LinkButton ID="hlkTechReqestList" runat="server" OnClick = "GotoRequestList" Text="Requests" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false" Enabled="false" Visible="false"></asp:LinkButton>--%>
		</asp:TableCell>
		</asp:TableRow>	
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkNewRequest" runat="server" Text="New Request" NavigateUrl="~/frmNewRequest.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false" Enabled="false" Visible="false"></asp:HyperLink>
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
		<asp:LinkButton ID="lbtnUserInfoEdit" runat="server" Text="Edit" OnClick="EditLogUserInfo" meta:resourcekey="lbtnUserInfoEditResource1" ></asp:LinkButton>		
		</asp:TableCell>		
		</asp:TableRow>
		</asp:Table>
		</div>
		
		<%--<ajaxToolkit:TabContainer ID="tb_main" runat="server" >
		
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="Administrator">
		<ContentTemplate>--%>
		<div class="content_right">
		
		
			</div>
			
		<%--	<h2 class="section">
		Admin Options
		</h2>--%>
			<asp:Table  runat="server" Width="700px" Height="150px">
			<asp:TableRow>
			<asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
			
			 <asp:Table ID="table_1"  runat="server" CellPadding="0" CellSpacing="0" BorderColor="ActiveBorder" BorderStyle="Solid" BorderWidth="0" Width="500px">
			 <asp:TableHeaderRow BorderStyle="Solid" BackColor="ActiveCaption" BorderColor="ActiveBorder">
			 <asp:TableCell Font-Bold="true" Font-Size="Smaller" ForeColor="white" HorizontalAlign="Center" ColumnSpan="3" BorderStyle="None" >
			 Request Summary
			 </asp:TableCell>
			 </asp:TableHeaderRow>
			 
			  <asp:TableRow  Height="40px" BackColor="#EBEBEB">
		        <asp:TableCell VerticalAlign="Middle" Font-Size="X-Small" HorizontalAlign="Left" BorderStyle="None">
		        <asp:Image ID="img_open" ToolTip="Click the link to open see Un Assigned Request" runat="server" ImageUrl="~/Images/summary_open.gif" />		        
		        </asp:TableCell>
		        <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Left">
		            <asp:LinkButton id = "lbtnUnAssignedRequests" Text="Un Assigned Request"  Font-Size="Small" PostBackUrl="~/frmAdminRequestList.aspx" runat="server"  ForeColor="Black" Font-Underline="false"></asp:LinkButton>
		        </asp:TableCell>
		        <asp:TableCell Font-Size="X-Small" HorizontalAlign="Center" BorderStyle="None">
		        <asp:LinkButton id = "lbtnUnAssignedRequestCount" Text="0"  Font-Size="Small" PostBackUrl="~/frmAdminRequestList.aspx" runat="server"  ForeColor="Black" Font-Underline="false"></asp:LinkButton>
		        </asp:TableCell>
		        </asp:TableRow>
		        <asp:TableRow Height="30px" BackColor="#F4F4F4">
		         <asp:TableCell VerticalAlign="Middle" Font-Size="X-Small" HorizontalAlign="Left" BorderStyle="None">
		        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/summary_duetoday.gif" />		        
		        </asp:TableCell>
		        <asp:TableCell  HorizontalAlign="Left" BorderStyle="None">
		        <asp:LinkButton id = "lbtnPendingRequests" Text=" Pending Requests" Font-Size="Small"  PostBackUrl="~/frmAdminRequestList.aspx?pending=true" runat="server" ForeColor="Black" Font-Underline="false"></asp:LinkButton>
		        </asp:TableCell>
		        <asp:TableCell  HorizontalAlign="Center" BorderStyle="None">
		        <asp:LinkButton id = "lbtnPendignRequestCount" Text="0 " Font-Size="Small"  PostBackUrl="~/frmAdminRequestList.aspx?pending=true" runat="server" ForeColor="Black" Font-Underline="false"></asp:LinkButton>
		        </asp:TableCell>
		        </asp:TableRow>
		        <asp:TableRow Height="30px" BackColor="#EBEBEB">
		        
		         <asp:TableCell VerticalAlign="Middle" Font-Size="X-Small" HorizontalAlign="Left" BorderStyle="None">
		        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/summary_overdue_icon.gif" />		        
		        </asp:TableCell>
		        
		        <asp:TableCell  HorizontalAlign="Left" BorderStyle="None">
		        
		        <asp:LinkButton id = "lbtnRequestOverDue" Text=" Request Overdue " Font-Size="Small"  runat="server"  ForeColor="Black" Font-Underline="false" PostBackUrl="~/frmAdminRequestList.aspx?dueToday=true" ></asp:LinkButton>
              	</asp:TableCell>
		        <asp:TableCell  HorizontalAlign="Center" BorderStyle="None">
		        <asp:LinkButton id = "lbtnRequestOverDueCount"  Text=" 0 " Font-Size="Small"  runat="server"  ForeColor="Black" Font-Underline="false" PostBackUrl="~/frmAdminRequestList.aspx?dueToday=true"></asp:LinkButton>
              	</asp:TableCell>
		    </asp:TableRow>	
		    </asp:Table>			 
			</asp:TableCell>
			</asp:TableRow>
			</asp:Table> 
		    		       <%-- </ContentTemplate>
		</ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>--%>
		<%--<center>
        <asp:Button ID="btnAssign" runat="server" OnClick="AssignTask" Text="Assign" />
        <asp:Button ID="btnBack" runat="server" OnClick="BackToList" CausesValidation="false" Text="Back" />
       </center>	--%>	
		</div>		
		</div>		
		
</asp:Content>



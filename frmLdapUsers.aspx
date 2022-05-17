<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmLdapUsers.aspx.cs" Inherits="frmLdapUsers" Title="Help Desk | Users" %>

<%@ Register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

<div id="body" style="width:1000px" >
<div id="col_main_left">
			
		<div>						
		<asp:Table Width="100%" ID="Table1"  BackColor="#EBEBEB" runat="server">
		<asp:TableRow>
		<asp:TableCell>		
		<asp:LinkButton ID="lbtnNewRecepient" runat="server" Text="New Inv. Recipient" Font-Size="Smaller" Font-Bold="false"  Font-Overline="false" ForeColor="black" OnClick="NewRecepient"></asp:LinkButton>
	   </asp:TableCell>	   
	   </asp:TableRow>
	   <asp:TableRow>
		<asp:TableCell>		
		<asp:LinkButton ID="lbtnNewTechnician" runat="server" Text="New PC Technician" Font-Size="Smaller" Font-Bold="false"  Font-Overline="false" ForeColor="black" OnClick="AddNewPCTech"></asp:LinkButton>
	   </asp:TableCell>	   
	   </asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>		
		<asp:LinkButton ID="lbtnNewSecretary" runat="server" Text="Assign Dept. Secretary" Font-Size="Smaller" Font-Bold="false"  Font-Overline="false" ForeColor="black" OnClick ="AddNewSecretary"></asp:LinkButton>
	   </asp:TableCell>	   
	   </asp:TableRow>	
	  	
	  	<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink1" runat="server" Text=" Home " NavigateUrl="~/frmAdmindefault.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>	
		
		
		<%--<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink1" runat="server" Text=" All Requests" NavigateUrl="~/frmAdminRequestList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink2" runat="server" Text="New Request" NavigateUrl="~/frmnewrequest.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkUserRequestList" runat="server" Text="Own Requests" NavigateUrl="~/frmUserRequestList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		--%>		
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
		
			
		<h2 class="section">
				Users 
				</h2>
		
		<table width="100%"><tr><td>
		    <asp:GridView ID="gdvUserList" runat="server" Height="75px" 
            Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" GridLines="Both" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="true" PageSize="25" OnPageIndexChanging="gdvUserList_PageIndexChanging">
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <Columns>                
           <asp:HyperLinkField  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="35%" HeaderText="Full Name" DataTextField= "full_name" Target="_parent" datanavigateurlfields="login_name" datanavigateurlformatstring= "frmUserDetails.aspx?logid={0}" /> 
           
          
           <asp:BoundField DataField="badge_number" HeaderText="Badge No"  ItemStyle-Width="10%"/>
           <asp:BoundField DataField="email_address" HeaderText="Email Address"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" />
           <asp:BoundField DataField="phone_ext" headertext="Extension" ItemStyle-Width="10%" />
          <%-- <asp:BoundField DataField="mobile" headertext="Mobile " ItemStyle-Width="10%" />--%>
           <asp:BoundField DataField="department_name" HeaderText="Department" ItemStyle-Width="20%" />
         <%--  <asp:BoundField DataField="job_title"  HeaderText="Title" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%"/>
           <asp:BoundField DataField="login_name"  HeaderText="Login ID" ItemStyle-Width="10%" />--%>
           
           
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



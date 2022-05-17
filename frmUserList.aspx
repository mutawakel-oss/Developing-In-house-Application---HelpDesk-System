<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmUserList.aspx.cs" Inherits="Default3" Title="Help Desk" %>
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
		</asp:TableRow>
				
		</asp:Table>
		</div>
		
		</div>
		
		<div id="col_main_right">
			
		<h2 class="section">
				User List
				</h2>
		
		<table width="100%"><tr><td>
		    <asp:GridView ID="gdvUserList" runat="server" Height="75px" 
            Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" GridLines="Both" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="true" PageSize="29" OnPageIndexChanging="gdvUserList_PageIndexChanging">
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <Columns>                
           <asp:HyperLinkField  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" HeaderText="Full Name" DataTextField= "full_name" Target="_parent" datanavigateurlfields="id" datanavigateurlformatstring= "frmUserDetails.aspx?id={0}" /> 
           
          
           <asp:BoundField DataField="badge_number" HeaderText="Badge No"  ItemStyle-Width="10%"/>
           <asp:BoundField DataField="email_address" HeaderText="Email Address"  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />
           <asp:BoundField DataField="phone_ext" headertext="Extension" ItemStyle-Width="10%" />
          <%-- <asp:BoundField DataField="mobile" headertext="Mobile " ItemStyle-Width="10%" />--%>
           <asp:BoundField DataField="department_name" HeaderText="Department" ItemStyle-Width="15%" />
           <asp:BoundField DataField="job_title"  HeaderText="Title" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%"/>
           <asp:BoundField DataField="login_name"  HeaderText="Login ID" ItemStyle-Width="10%" />
           
           
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

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmInvProductList.aspx.cs" Inherits="frmInvProductList" Title="Inventory | Product List" %>
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
		<asp:HyperLink ID="hlkNewProduct" runat="server" Text=" New Product" NavigateUrl="~/frmInvProductDetails.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink1" runat="server" Text=" Reports" NavigateUrl="~/frmCategoryReport.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>
		<%--<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink1" runat="server" Text=" Store Details" NavigateUrl="~/frmStroreDetails.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>--%>
		<asp:TableRow>
		<asp:TableCell>		
		<%--<asp:LinkButton ID="lbtnNewRecepient" runat="server" Text="Recepient" Font-Size="Smaller" Font-Bold="false"  Font-Overline="false" ForeColor="black" OnClick="NewRecepient"></asp:LinkButton>--%>
		
		<%--<asp:HyperLink ID="hlkRecepient" runat="server" Text="Recepient" NavigateUrl="~/frmInvRecepient.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>--%>
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
			<asp:Table ID="Table2" runat="server">
				<asp:TableRow>
				<asp:TableCell Font-Size="Smaller" HorizontalAlign="Left">		
				Allocation Type
				<asp:DropDownList ID="ddlAllocated" runat="server" Width="85" Font-Size="Smaller" AutoPostBack="true" OnSelectedIndexChanged="ddlAllocated_SelectdIndexChanged">
				    <asp:ListItem  Value="2" Text="ALL"></asp:ListItem>
				    <asp:ListItem Value="1" Text ="Allocated"></asp:ListItem>
				    <asp:ListItem Value="0" Text ="Not Allocated"></asp:ListItem>				    
				</asp:DropDownList>						
				</asp:TableCell >
						
				<asp:TableCell Font-Size="Smaller" HorizontalAlign="Left">		
				Category
				<asp:DropDownList ID="ddlCatgory" runat="server" Width="85" AutoPostBack="true" Font-Size="Smaller" OnSelectedIndexChanged = "OnCategoryChanged">				    		    
				</asp:DropDownList>
				
				</asp:TableCell>
				<asp:TableCell Font-Size="Smaller" HorizontalAlign="Left">		
				Product
				<asp:DropDownList ID="ddlProduct" runat="server" Width="109" AutoPostBack="true" Font-Size="Smaller" OnSelectedIndexChanged="onProductChanged">				   			    
				</asp:DropDownList>		
				</asp:TableCell>
				</asp:TableRow>
				</asp:Table>
				<asp:Table ID="searchTable" runat=server>
				<asp:TableHeaderRow>
				<asp:TableCell Font-Size="Smaller" HorizontalAlign="Left">
				Serial No:
				<asp:TextBox ID="txtSerialNo" runat=server Width="100px"></asp:TextBox>
				</asp:TableCell>
				<asp:TableCell Font-Size="Smaller" HorizontalAlign="Left">MIS Asset No
				<asp:TextBox ID="txtMissAssetNo" runat="server" Font-Size="Small"></asp:TextBox>								
				</asp:TableCell>		
				<asp:TableCell>
				<asp:Button ID="btnSearch1" runat=server Text="Search" OnClick="mSearch" />
				</asp:TableCell>
				</asp:TableHeaderRow>
				</asp:Table>				
				<asp:Table ID="tab12" runat="server">
				<asp:TableRow>
				<asp:TableCell Font-Size="Small" Width="300px">
				<asp:Label runat="server" ID="lblItem" Text="No. of Items:" Visible="false"></asp:Label>&nbsp;
				<asp:Label runat="server" id="lblAvailable" Visible="false"></asp:Label> 
				</asp:TableCell>
				<asp:TableCell Width="500px" HorizontalAlign="Right" Font-Size="Small">
				<asp:Label runat="server" ID="lblallotteduser" Text="Items Allocated to :"></asp:Label>&nbsp;
				<asp:DropDownList runat="server" ID="ddlAllocatedUser"  Font-Size="Smaller" AutoPostBack="true" OnSelectedIndexChanged="onAllocatedUserChange"></asp:DropDownList>				
				</asp:TableCell>
				</asp:TableRow>
				</asp:Table>
				
		
		<table id="tabGrd" width="100%"><tr><td>
		    <asp:GridView ID="gdvInvProductList" runat="server" Height="75px" HeaderStyle-Height="4px" HeaderStyle-Font-Underline="false"
            Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" GridLines="Both" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="true" PageSize="20" OnPageIndexChanging="gdvInvProductList_PageIndexChanging" PagerSettings-Mode="NextPreviousFirstLast" PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next" PagerSettings-PreviousPageText="Prev" PagerSettings-FirstPageText="First" AllowSorting="true" OnSorting="GridView_Sorting">
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <Columns>                
                     
           <asp:HyperLinkField SortExpression="vendor_serial_no" HeaderText="Serial No" DataTextField="vendor_serial_no" DataNavigateUrlFields="id" DataNavigateUrlFormatString="frminvproductdetails.aspx?id={0}">
               <ItemStyle Width="13%" />
           </asp:HyperLinkField>
             <asp:BoundField SortExpression="asset_number" DataField="asset_number" HeaderText="MIS Asset No" HeaderStyle-Font-Underline="true">
                 <ItemStyle HorizontalAlign="Left" Width="20%" />
             </asp:BoundField>
           <asp:BoundField SortExpression="category" DataField="category" headertext="Category">
               <ItemStyle Width="13%" />
           </asp:BoundField>
           <asp:BoundField SortExpression="product_name" DataField ="product_name"  HeaderText="Product Name"  >
               <ItemStyle Width="17%" />
           </asp:BoundField>
           <asp:BoundField SortExpression="allocated_user" DataField="allocated_user" HeaderText="Allocated To" />
           <asp:BoundField SortExpression="location" DataField="location"  HeaderText="Location " >
               <ItemStyle Width="17%" />
           </asp:BoundField>
           
            </Columns>
            <RowStyle BackColor="#F7F6F3" BorderStyle="Solid" BorderWidth="1px" ForeColor="#333333" Height="10px" HorizontalAlign="Left" VerticalAlign="Middle" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" VerticalAlign="Middle" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="4px" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <PagerSettings PageButtonCount="15" FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" PreviousPageText="Prev"/>
        </asp:GridView>		
		</td></tr></table>		
		</div>		
		</div>		
		
</asp:Content>



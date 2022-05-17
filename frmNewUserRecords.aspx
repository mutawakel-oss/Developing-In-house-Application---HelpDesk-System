<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmNewUserRecords.aspx.cs" Inherits="frmUserRequestList" Title="Help Desk" Culture="auto"  %>
<%@register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

<div  id="body"  style="width:1000px">
		<div id="col_main_left">
			
			<div >						
		<asp:Table runat="server" Width="100%" ID="Table8"  BackColor="#EBEBEB">		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">		
		<asp:HyperLink ID="hlkUserList" runat="server" Text="User Management" NavigateUrl="~/frmLdapUsers.aspx" Font-Size="Smaller" Font-Bold="False" ForeColor="Black" Font-Underline="False" Enabled="False" Visible="False" ></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">		
		<asp:HyperLink ID="hlkAdminRequestList" runat="server" Text="All Requests" NavigateUrl="~/frmAdminRequestList.aspx" Font-Size="Smaller" Font-Bold="False" ForeColor="Black" Font-Underline="False" Enabled="False" Visible="False" ></asp:HyperLink>		
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">	
		
		
		</asp:TableCell>
		</asp:TableRow>					
		</asp:Table>
		</div>
		<div style=" background-color:#EBEBEB">
            &nbsp;</div>	
				
			
		</div>
		<div id="col_main_right">
		<div  style="border-color:blue; background-color:#EBEBEB">
            &nbsp;</div>	
		
		
		<h2 class="section">
		<asp:Label runat="server" ID="Label5" Text="New Requests" ></asp:Label>
				</h2>		
		<div >
		<table width="100%"><tr><td>
		    <asp:GridView ID="gdvRequestList" runat="server" Height="75px" RowStyle-Height="5px" 
            Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="True" PageSize="29"   >
            <PagerSettings PageButtonCount="15" />
           
           <Columns>                
               <asp:BoundField DataField="request_id" HeaderText="Request ID" />
                   
           <asp:BoundField DataField="name" HeaderText="Name"  >
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>
           <asp:BoundField DataField="badgeNo" HeaderText="Badge NO"  >
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle Width="100px" />
           </asp:BoundField>
           <asp:BoundField DataField="phone" HeaderText="Phone">
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>
           <asp:BoundField DataField="department" headertext="Department" >
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>
           <asp:BoundField DataField="subject" headertext="Subject" >
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>
           <asp:BoundField DataField="email" headertext="E-Mail" >
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>                     
            <asp:BoundField DataField="created_on" headertext="Created On" HtmlEncode="False" DataFormatString="{0:dd/MMM/yyyy ddd,hh:mm:ss tt}" >
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>   
               <asp:TemplateField HeaderText="Status     " ConvertEmptyStringToNull="False">
                   <EditItemTemplate>
                       <asp:CheckBox ID="CheckBox1" runat="server" />
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:CheckBox ID="CheckBox1" runat="server" />
                   </ItemTemplate>
               </asp:TemplateField>
          
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
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<asp:Button ID="btnCreate" runat="server"  Text="Create" OnClick="btnCreate_Click" Height="35px" Width="91px" />	
    <asp:Label ID="languageLabel" runat="server" Text="Label" Visible="False"></asp:Label></div>		
		
</asp:Content>




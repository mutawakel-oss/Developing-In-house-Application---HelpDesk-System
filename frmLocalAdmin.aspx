<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmLocalAdmin.aspx.cs" Inherits="frmLocalAdmin" Title="Help Desk" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">


<div id="body" style="width:1000px">
		<div id="col_main_left">
			
			<div>						
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkUserList" runat="server" Text="User List" NavigateUrl="~/frmUserList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		
		<asp:TableRow>
		<asp:TableCell>		
		<%--<asp:HyperLink ID="hlkAdminRequestList" runat="server" Text="Requests" NavigateUrl="~/frmAdminRequestList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false" Enabled="false" Visible="false"></asp:HyperLink>--%>
		</asp:TableCell>
		</asp:TableRow>		
		</asp:Table>
		</div>
			
			
		</div>
		<div id="col_main_right">
		
		<ajaxToolkit:TabContainer ID="tb_main" runat="server" >
		
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="Import Users">
		<ContentTemplate>
		<div class="content_right">
			</div>
			 <asp:Table ID="table_1" runat="server">		  		 
			 
		    <asp:TableRow>		        
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">		        
		        User Name</asp:TableCell>
		        <asp:TableCell>
		          <asp:TextBox ID="txtUserName"  Width="210px" runat="server"></asp:TextBox>		                     
		        </asp:TableCell>		        
		    </asp:TableRow>
		    <asp:TableRow>		        
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">		        
		        Password</asp:TableCell>
		        <asp:TableCell>
		          <asp:TextBox ID="txtPassword"  Width="210px" runat="server" TextMode="Password" ></asp:TextBox>		                     
		        </asp:TableCell>		        
		    </asp:TableRow>
		    
		    <asp:TableRow>
		    <asp:TableCell></asp:TableCell>
		    <asp:TableCell HorizontalAlign="Right"> 
		    <asp:Button ID="btnImportUsers" Text="Import Users" runat="server" OnClick="ImportUsers" />
		    </asp:TableCell>
		    </asp:TableRow>
		    <asp:TableRow>
		    <asp:TableCell>
		    <asp:Label ID="lblResult" runat="server"></asp:Label>
		    </asp:TableCell>
		    </asp:TableRow> 		    
		
		 </asp:Table>
	
		       		        
		        </ContentTemplate>
		</ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>
		
		</div>		
		</div>		
		
</asp:Content>


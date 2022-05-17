<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmEditLogUserInfo.aspx.cs" Inherits="frmEditLogUserInfo" Title="Help Desk | Login User Info" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
<div  id="body" style="width:1000px">
		<div id="col_main_left">
			
			<div>						
		<%--<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink2" runat="server" Text="User Management"  NavigateUrl="~/frmLdapUsers.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink1" runat="server" Text="Requests" NavigateUrl="~/frmAdminRequestList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>
		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink3" runat="server" Text="New Request" NavigateUrl="~/frmNewRequest.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		
		</asp:Table>--%>
		</div>
			
		</div>
		<div id="col_main_right">
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' style="border-color:blue; background-color:#EBEBEB">
		<asp:Table ID="Table9" runat="server">
		<asp:TableRow ID="TableRow1" runat="server" >		
		<asp:TableCell ID="TableCell1" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label5" Text="Login user :" meta:resourcekey="Label1Resource1"></asp:Label>
		<asp:Label ID="lblLogUser" runat="server" ForeColor="Blue" Width="180px" meta:resourcekey="lblLogUserResource1"></asp:Label>
		</asp:TableCell>		
		<asp:TableCell ID="TableCell2" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label6" Text="Badge #:" meta:resourcekey="Label2Resource1"></asp:Label>
		<asp:Label ID="lblBadgeNo" runat="server"  ForeColor="Blue" Width="70px" meta:resourcekey="lblBadgeNoResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell ID="TableCell3" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label7" Text="Department:" meta:resourcekey="Label3Resource1"></asp:Label>
		<asp:Label ID="lblDepartment" runat="server"  ForeColor="Blue" Width="140px" meta:resourcekey="lblDepartmentResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell ID="TableCell4" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label8" Text="Title:" meta:resourcekey="Label4Resource1"></asp:Label>
		<asp:Label ID="lblTitle" runat="server"  ForeColor="Blue" Width="140px" meta:resourcekey="lblTitleResource1"></asp:Label>
		</asp:TableCell>				
		</asp:TableRow>
		</asp:Table>
		</div>
		
		
		<%--<ajaxToolkit:TabContainer ID="tb_main" runat="server" >
		
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="User Details">
		<ContentTemplate>--%>
		<h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section">
		<asp:Label runat="server" ID="lblPD" Text="Personal details" meta:resourcekey="lblPDResource1"></asp:Label>
		                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="manStar" Text="*" runat="server" ForeColor="Red" meta:resourcekey="manStarResource1"></asp:Label> <asp:Label ID="Label1" Text="Mandantory Field" runat="server" Font-Bold="False" Font-Size="Smaller" meta:resourcekey="Label1Resource2"></asp:Label>      
		         </h2>
		         
		     <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>    
		 <asp:Table ID="table_1" runat="server">
		    <asp:TableRow runat="server">
		        <asp:TableCell Width="130px" Font-Size="Smaller" runat="server">
		        <asp:Label runat="server" ID="Label9" Text="Full Name" meta:resourcekey="Label9Resource1"></asp:Label>
		        </asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtFullname"  Width="210px" runat="server" ReadOnly="True" Enabled=false></asp:TextBox>
		        </asp:TableCell>
		    </asp:TableRow>
		    <asp:TableRow runat="server">
		        <asp:TableCell Font-Size="Smaller" runat="server">
		        
		           <asp:Label runat="server" ID="Label10" Text="Employee ID" meta:resourcekey="Label10Resource1"></asp:Label>
		            
		        </asp:TableCell>
		        
		        <asp:TableCell Font-Size="Smaller" runat="server">
		        <asp:TextBox ID="txtEmployeeID" Width="210px" runat="server" Enabled=false></asp:TextBox>
		        
		        </asp:TableCell>
		    </asp:TableRow>
		   
		    
		 </asp:Table>
		 </div>
         
         <h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section">
		         <asp:Label runat="server" ID="Label11" Text="Contact details" meta:resourcekey="Label11Resource1"></asp:Label>
		         
		         </h2>
		         <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
		         <asp:Table ID="table_2" runat="server">
		            <asp:TableRow runat="server">
		        <asp:TableCell Width="130px" Font-Size="Smaller" runat="server">
		            <asp:Label runat="server" ID="Label12" Text="E Mail" meta:resourcekey="Label12Resource1"></asp:Label>
		            
		        </asp:TableCell>
		        
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtEmail" Width="210px" runat="server" ReadOnly="True" Enabled=False></asp:TextBox>
		        </asp:TableCell>
		    </asp:TableRow>
		    
		     <asp:TableRow runat="server">
		        <asp:TableCell Font-Size="Smaller" runat="server">
		          
		            <asp:Label runat="server" ID="Label13" Text="sss" meta:resourcekey="Label13Resource1"></asp:Label>
		            
		        </asp:TableCell>
		        
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtPhone" Width="210px" runat="server"></asp:TextBox>
		    
		        </asp:TableCell>
		        
		    </asp:TableRow>
		    
		      <asp:TableRow runat="server">
		        <asp:TableCell Font-Size="Smaller" runat="server">
		        
		            <asp:Label runat="server" ID="Label14" Text="Pager"  meta:resourcekey="Label14Resource1"></asp:Label>
		            
		        </asp:TableCell>
		        
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtMobile" Width="210px" runat="server"></asp:TextBox>
		        
		        
		        
		        </asp:TableCell>
		    </asp:TableRow>
		         </asp:Table>
		         </div>
		         
		          <h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section">
		         <asp:Label runat="server" ID="Label15" Text="Department details" meta:resourcekey="Label15Resource1"></asp:Label>
		         
		         </h2>
		         <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
		          <asp:Table ID="table3" runat="server">
		            <asp:TableRow runat="server">
		        <asp:TableCell Width="130px" Font-Size="Smaller" runat="server">
		            <asp:Label runat="server" ID="Label16" Text="Department Name" meta:resourcekey="Label16Resource1"></asp:Label>
		            
		        </asp:TableCell>
		        
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtDepartment" Width="210px" runat="server" Enabled=false></asp:TextBox>
		        </asp:TableCell>
		    </asp:TableRow>
		    
		     <asp:TableRow runat="server">
		        <asp:TableCell Font-Size="Smaller" runat="server">
		            <asp:Label runat="server" ID="Label17" Text="Job Title" meta:resourcekey="Label17Resource1"></asp:Label>
		            
		        </asp:TableCell>
		        
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtTitle" Width="210px" runat="server" Enabled=false></asp:TextBox>
		        </asp:TableCell>
		    </asp:TableRow>
		         </asp:Table>	
		         </div>	         
		          <h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section">
		         <asp:Label runat="server" ID="Label18" Text="Login details" meta:resourcekey="Label18Resource1"></asp:Label>
		         
		         </h2>	
		         <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>	         
		          <asp:Table ID="table4" runat="server">
		            <asp:TableRow runat="server">
		        <asp:TableCell Width="130px" Font-Size="Smaller" runat="server">
		            <asp:Label runat="server" ID="Label19" Text="Login Name" meta:resourcekey="Label19Resource1"></asp:Label>
		            
		        </asp:TableCell>		        
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtLogin" Width="210px" runat="server" ReadOnly="True" Enabled=false></asp:TextBox>
		        </asp:TableCell>
		    </asp:TableRow>		         
		         <asp:TableRow runat="server">
		         
		         <asp:TableCell runat="server"></asp:TableCell>
		         </asp:TableRow>
		         </asp:Table>
		         </div>
		         
		        <%-- <h2 class="section">
		         Group details
		         </h2>
		         
		          <asp:Table ID="table7" runat="server">
		            <asp:TableRow>
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">
		
		        </asp:TableCell>
		        
		        <asp:TableCell>
		 
		        
		        <asp:CheckBoxList ID="cblGroups" runat="server">
		 
		        </asp:CheckBoxList>
		        </asp:TableCell>
		    </asp:TableRow>
		         
		         </asp:Table>--%>
		        
		        <%--</ContentTemplate>
		</ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>--%>
		<center>
         <asp:Button id="btnSave" runat="server" Text="Save" OnClick="SaveDetails" meta:resourcekey="btnSaveResource1" CausesValidation=true ValidationGroup="contactGroup"  />
         <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="Back" OnClick="BackToHome" meta:resourcekey="btnBackResource1" />
       </center>
		</div>		
		</div>		
		
</asp:Content>



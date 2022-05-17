<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmInvRecepient.aspx.cs" Inherits="frmInvRecepient" Title="Inventory | Product Details" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">  
<div id="body" style="width:1000px">
		<div id="col_main_left">
			
			<div>						
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
		<%--<asp:TableRow>
		<asp:TableCell>		
		<asp:LinkButton ID="lbtnNewTechnician" runat="server" Text="New PC Technician" Font-Size="Smaller" Font-Bold="false"  Font-Overline="false" ForeColor="black" OnClick="AddNewTechnician"></asp:LinkButton>
	   </asp:TableCell>	   
	   </asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>		
		<asp:LinkButton ID="lbtnNewSecretary" runat="server" Text="Assign Dept. Secretary" Font-Size="Smaller" Font-Bold="false"  Font-Overline="false" ForeColor="black" OnClick="AddNewSecretary"></asp:LinkButton>
	   </asp:TableCell>	   
	  </asp:TableRow>	
		
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
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkProductList" runat="server" Text="Inventory"  NavigateUrl="~/frmInvProductList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>--%>	
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkback" runat="server" Text="Back"  NavigateUrl="~/frmLdapusers.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>
		</asp:Table>
		</div>
			
		</div>
		<div id="col_main_right">
		<div style="border-color:blue; background-color:#EBEBEB">
		<asp:Table ID="Table9" runat="server">
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
		           
		<ajaxToolkit:TabContainer ID="tb_main" runat="server" >
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="Inventory Recepient">
		<ContentTemplate><h2 class="section">
		         Choose Recipient       
		         </h2>		         		         
		 <asp:Table ID="table_1" runat="server" Width="55%">
		    <asp:TableRow>
		        <asp:TableCell Width="140px" Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left" >
		        
		        <asp:ListBox id="lstAllUsers" runat="server" SelectionMode="Multiple" Height="200px" Width="180px"></asp:ListBox> 		        
		        
		        </asp:TableCell>
		        <asp:TableCell  Width="100px" VerticalAlign="Middle">
		                <asp:Table runat="server">
		                <asp:TableRow>
		                <asp:TableCell>
		                <asp:Button  runat="server" ID="btnSelectOne" Text="  >  " OnClick="SelectSingleUSer" Width="75px"/>
		                </asp:TableCell>
		                </asp:TableRow>
		                <asp:TableRow>
		                <asp:TableCell>
		                <asp:Button ID="btnSelectAll" Text=" >> " runat="server" Width="75px" OnClick="SelectMultipleUSer"/>
		                </asp:TableCell>
		                </asp:TableRow>
		                
		                <asp:TableRow>
		                <asp:TableCell Height="20px">
		                
		                </asp:TableCell>
		                </asp:TableRow>
		                
		                <asp:TableRow>
		                <asp:TableCell>                
		                 <asp:Button ID="btnDeSelectOne" Text="  <   " runat="server" OnClick="DeSelectSingleUSer" Width="75px" />
		                </asp:TableCell>
		                </asp:TableRow>
		                <asp:TableRow>
		                <asp:TableCell>
		                <asp:Button  ID="btnDeSelectAll" Text=" << " runat="server" Width="75px" OnClick="DeSelectMultipleUSer"/>
		                </asp:TableCell>
		                </asp:TableRow>
		                </asp:Table>
		        </asp:TableCell>		              
		          <asp:TableCell  Width="140px">
		          <asp:ListBox ID="lstSelectedUsers"  runat="server" Height="200px" Width="180px">		         		        
		          </asp:ListBox>		          
		        </asp:TableCell>
		    </asp:TableRow>	  		   
		 </asp:Table>       
		        <hr>		
		<center>
         <asp:Button id="btnSave"  runat="server" Text="Save" OnClick="SaveRecepient" />
         <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="GoBack"/>
       </center>   
		        
		        
		 </ContentTemplate>
		</ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>
		
		</div>		
		</div>		
		
</asp:Content>





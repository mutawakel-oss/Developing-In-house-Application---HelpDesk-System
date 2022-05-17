<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmAssignDeptSecretary.aspx.cs" Inherits="frmAssignDeptSecretary" Title="Help Desk | Department Secretary" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">  
<div id="body" style="width:1000px">
		<div id="col_main_left">
							
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
		
		<%--<asp:TableRow>
		<asp:TableCell>		
		<asp:LinkButton ID="lbtNewRecipient" runat="server" Text="New Inv. Recipient" CausesValidation="false" Font-Size="Smaller" Font-Bold="false"  Font-Overline="false" ForeColor="black" OnClick="NewRecepient"></asp:LinkButton>
	   </asp:TableCell>	   
	   </asp:TableRow>				
		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:LinkButton ID="lbtnNewPCTech" runat="server" Text="New PC Technician" Font-Size="Smaller" Font-Bold="false"  CausesValidation="false" Font-Overline="false" ForeColor="black" OnClick="AddNewPCTech"></asp:LinkButton>
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
		<asp:HyperLink ID="HyperLink3" runat="server" Text="Inventory"  NavigateUrl="~/frmInvProductList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		--%>
		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink3" runat="server" Text="Back"  NavigateUrl="~/frmldapusers.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
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
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="Assign Deparment Secretary">		
		<ContentTemplate>
		
		<%-- <h2 class="section">
		         Choose Recepient       
		         </h2>--%>
		         
		         <asp:Table runat="server">
		         <asp:TableRow>
		         <asp:TableCell>
		           Department :       
		         </asp:TableCell>
		         <asp:TableCell>
		         <asp:DropDownList runat="server" ID="ddlDepartments"  AutoPostBack ="true" Width="350px" OnSelectedIndexChanged="ddlDepartments_OnSelectedIndexChanged">
		         </asp:DropDownList>
		         <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDepartments" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
		        <asp:LinkButton ID="lbtnNewDepartment" runat="server" Text="New" Font-Bold="true" Font-Size="Smaller"></asp:LinkButton>
    
                <asp:Panel ID="pnlNewDepartment" CssClass="modalPopup"  Height="122px" Width="412px" runat="server">
		         <asp:Panel ID="Panel3" runat ="server" Width="410px" Height="120" BorderColor="black" BorderStyle="Solid" BorderWidth="1">
    <asp:Table ID="Table13" runat="server" Width ="50%">
    <asp:TableRow Width ="50%">
    <asp:TableCell Font-Size="Smaller" HorizontalAlign="Left">
    Enter Department Name
    </asp:TableCell>
    <asp:TableCell></asp:TableCell>    
    </asp:TableRow>    
    </asp:Table>
    <hr />    
    <asp:Table ID="Table14" runat="server">
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left">
    Department
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtNewDepartment" runat="server" Width="300px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator11"  runat="server" ControlToValidate="txtNewDepartment" ValidationGroup="grpDept" ErrorMessage="*"></asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>
    </asp:Table>
    <hr />
    <asp:Table ID="Table15" runat="server">
    <asp:TableRow>
    <asp:TableCell>
    <asp:Button runat="server" ID = "btnSaveNewDeparment" Text="Save" ValidationGroup="grpDept" OnClick="SaveNewDepartment" />
    <asp:Button runat="server" ID="btnCancelNewDepartment" CausesValidation="false" Text="Cancel" />
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    </asp:Panel>   		         
 </asp:Panel>		        
   <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1"  TargetControlID="lbtnNewDepartment" PopupControlID="pnlNewDepartment" CancelControlID="btnCancelNewDepartment" runat="server"></ajaxToolkit:ModalPopupExtender>
		     	        
		        
		         </asp:TableCell>
		         </asp:TableRow>
		         </asp:Table>		         		         
		         
		         Select User
		         
		 <asp:Table ID="table_1" runat="server" Width="55%">
		    <asp:TableRow>
		        <asp:TableCell Width="140px" Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left" >
		        
		        <asp:ListBox id="lstAllUsers" runat="server" SelectionMode="Multiple" Height="200px" Width="180px"></asp:ListBox> 		        
		        
		        </asp:TableCell>
		        <asp:TableCell  Width="100px" VerticalAlign="Middle">
		                <asp:Table ID="Table2" runat="server">
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
         <asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="Back" OnClick ="BackToHome"  />
       </center>	        
		 </ContentTemplate>
		</ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>
		
		</div>		
		</div>		
		
</asp:Content>



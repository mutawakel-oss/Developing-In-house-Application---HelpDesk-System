<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmUserDetails.aspx.cs" 
Inherits="frmUserDetails" Title="Help Desk | User Details" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
<div id="body" style="width:1000px">
		<div id="col_main_left">
			<div id="user_assistance">
				<a id="content_start"></a>
				<h3>
					Help and Other Links</h3>				
			</div>
			<div>						
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
		
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
		
		</asp:Table>
		</div>
			
		</div>
		<div id="col_main_right">
		<div style="border-color:blue; background-color:#EBEBEB">
		<asp:Table ID="Table9" runat="server">
		<asp:TableRow>		
		<asp:TableCell Font-Size="8">
		Login user :<asp:Label ID="lblLogUser" runat="server" ForeColor="blue" Width="180px"></asp:Label>
		</asp:TableCell>		
		<asp:TableCell Font-Size="8">
		Badge #:<asp:Label ID="lblBadgeNo" runat="server"  ForeColor="blue" Width="70px"></asp:Label>
		</asp:TableCell>
		<asp:TableCell Font-Size="8">
		Department:<asp:Label ID="lblDepartment" runat="server"  ForeColor="blue" Width="140px"></asp:Label>
		</asp:TableCell>
		<asp:TableCell Font-Size="8">
		Title:<asp:Label ID="lblTitle" runat="server"  ForeColor="blue" Width="140px"></asp:Label>
		</asp:TableCell>
		<asp:TableCell Font-Size="8">
		<asp:LinkButton ID="lbtnUserInfoEdit" runat="server" Text="Edit" OnClick="EditLogUserInfo"></asp:LinkButton>		
		</asp:TableCell>		
		</asp:TableRow>
		</asp:Table>
		</div>
		
		
		<ajaxToolkit:TabContainer ID="tb_main" runat="server" >
		
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="User Details">
		<ContentTemplate><h2 class="section">
		         Inventory         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="manStar" Text="*" runat="server" ForeColor="red"></asp:Label> <asp:Label ID="Label1" Text="Mandantory Field" runat="server" Font-Bold="false" Font-Size="Smaller"></asp:Label>      
		         </h2>
		         
		         
		 <asp:Table ID="table_1" runat="server">
		    <asp:TableRow>
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">
		        <%--<asp:Label id = "vald1"  Text="*" runat="server" ForeColor="red"></asp:Label>--%>
		        Full Name</asp:TableCell>
		        <asp:TableCell>
		        <asp:TextBox ID="txtFullname"  Width="210px" runat="server" ReadOnly="true"></asp:TextBox>
		                     <%--  <asp:RequiredFieldValidator ID="vldFullname" ControlToValidate="txtFullname"  ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>--%>
		        </asp:TableCell>
		    </asp:TableRow>
		    <asp:TableRow>
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		        <asp:Label id = "Label2"  Text="*" runat="server" ForeColor="red"></asp:Label>
		            Employee ID
		        </asp:TableCell>
		        
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		        <asp:TextBox ID="txtEmployeeID" Width="210px" runat="server"></asp:TextBox>
		        <asp:RequiredFieldValidator ID="vldEmployeeID" ControlToValidate="txtEmployeeID"  ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		        </asp:TableCell>
		    </asp:TableRow>
		   
		    
		 </asp:Table>
         
         <h2 class="section">
		         Contact details
		         </h2>
		         <asp:Table ID="table_2" runat="server">
		            <asp:TableRow>
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">
		            E Mail
		        </asp:TableCell>
		        
		        <asp:TableCell>
		        <asp:TextBox ID="txtEmail" Width="210px" runat="server" ReadOnly="true"></asp:TextBox>
		        </asp:TableCell>
		    </asp:TableRow>
		    
		     <asp:TableRow>
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		          <asp:Label id = "Label3"  Text="*" runat="server" ForeColor="red"></asp:Label>
		            Phone
		        </asp:TableCell>
		        
		        <asp:TableCell>
		        <asp:TextBox ID="txtPhone" Width="210px" runat="server"></asp:TextBox>
		     
		       <asp:RequiredFieldValidator ID="vldPhone" ControlToValidate="txtPhone"  ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		    
		        </asp:TableCell>
		        
		    </asp:TableRow>
		    
		      <asp:TableRow>
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		        <asp:Label id = "Label4"  Text="*" runat="server" ForeColor="red"></asp:Label>
		            Mobile
		        </asp:TableCell>
		        
		        <asp:TableCell>
		        <asp:TextBox ID="txtMobile" Width="210px" runat="server"></asp:TextBox>
		        
		        <asp:RequiredFieldValidator ID="vldMobile" ControlToValidate="txtMobile"  ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		        
		        </asp:TableCell>
		    </asp:TableRow>
		         </asp:Table>
		         
		          <h2 class="section">
		         Department details
		         </h2>
		         
		          <asp:Table ID="table3" runat="server">
		            <asp:TableRow>
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">
		            Department Name
		        </asp:TableCell>
		        
		        <asp:TableCell>
		        <asp:TextBox ID="txtDepartment" Width="210px" runat="server"></asp:TextBox>
		        </asp:TableCell>
		    </asp:TableRow>
		    
		     <asp:TableRow>
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		            Job Title
		        </asp:TableCell>
		        
		        <asp:TableCell>
		        <asp:TextBox ID="txtTitle" Width="210px" runat="server"></asp:TextBox>
		        </asp:TableCell>
		    </asp:TableRow>
		         </asp:Table>		         
		          <h2 class="section">
		         Login details
		         </h2>		         
		          <asp:Table ID="table4" runat="server">
		            <asp:TableRow>
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">
		            Login Name
		        </asp:TableCell>		        
		        <asp:TableCell>
		        <asp:TextBox ID="txtLogin" Width="210px" runat="server" ReadOnly="true"></asp:TextBox>
		        </asp:TableCell>
		    </asp:TableRow>		         
		         <asp:TableRow>
		         <asp:TableCell font-Size="Smaller" Font-Bold="true">		        
		         Reset Password
		         </asp:TableCell>
		         <asp:TableCell>
		         <asp:LinkButton ID="lnkResetPassord" runat="server" Text="Reset Password"></asp:LinkButton>
		         <asp:Panel ID="pnl_reset_password" runat="server" CssClass="modalPopup"  Height="200px" Width="340px" BorderStyle="Solid" >
		            <div  style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:30px" >            
                 <h2 class="section">
                    Reset Password </h2></div>                    
                    <asp:Table ID="Table5" runat="server">
                    <asp:TableHeaderRow VerticalAlign="Middle">
                    <asp:TableHeaderCell></asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                    <asp:TableCell>
                    
                    <asp:Table ID="Table6" runat="server">
                    <asp:TableRow>
                        <asp:TableCell Text="Login Name" Font-Size="Smaller" Font-Bold="true">
                        
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label  Font-Size="Smaller" Font-Bold="true" ID="lblLoginName" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Middle">
                    <asp:TableCell font-Size="Smaller" Font-Bold="true">New password</asp:TableCell>
                    <asp:TableCell>
                     <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server"></asp:TextBox>
                     </asp:TableCell>
                    </asp:TableRow>
                    </asp:Table>
                    </asp:TableCell>
                    </asp:TableRow>                                       
                    </asp:Table>
                    
                    <div align="center">                   
                      <asp:Button ID="btnReset" CausesValidation="false" runat="server" Text="Reset Password" />
                      <asp:Button ID="btnCancelPWD" CausesValidation="false" runat="server" Text="Cancel" />
                      </div>
		         </asp:Panel>
		         <ajaxToolkit:ModalPopupExtender ID="modal" runat="server"  BackgroundCssClass="modalBackground" 
		            TargetControlID="lnkResetPassord"  PopupControlID="pnl_reset_password"
		            CancelControlID="btnCancelPWD">		         		         
		         </ajaxToolkit:ModalPopupExtender>
		       
		         </asp:TableCell>
		         </asp:TableRow>
		         </asp:Table>
		         
		         <h2 class="section">
		         Group details
		         </h2>
		         
		          <asp:Table ID="table7" runat="server">
		            <asp:TableRow>
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">
		
		        </asp:TableCell>
		        
		        <asp:TableCell>
		 
		        
		        <asp:CheckBoxList ID="cblGroups" runat="server">
		 
		        </asp:CheckBoxList>
		        <asp:CheckBox  runat="server" ID="chbInvRecepient" Text="Inventory Recepient"/>
		        
		        </asp:TableCell>
		    </asp:TableRow>
		         
		         </asp:Table>
		        
		        </ContentTemplate>
		</ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>
		<center>
            &nbsp;<asp:Button id="btnSave" OnClick="SaveUserDetails" runat="server" Text="Save"  />
         <asp:Button ID="btnBack" runat="server" Text="Back"  OnClick="BackToList"/>
       </center>
		</div>		
		</div>		
		
</asp:Content>




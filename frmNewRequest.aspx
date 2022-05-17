<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" 
AutoEventWireup="true" CodeFile="frmNewRequest.aspx.cs" Inherits="frmNewRequest" Title="Help Desk | New Request Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
<div  id="body"  style="width:1000px">
		<div id="col_main_left">
				<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>						
                    <asp:Table Width="100%" ID="Table8" BackColor="#EBEBEB" runat="server">
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Own_Requests.png"
                                    OnClick="mOwnRequestClick" Width="190px" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">
                                <asp:HyperLink ID="hlkAdminRequestList" runat="server" Text="All Requests" NavigateUrl="~/frmAdminRequestList.aspx"
                                    Font-Size="Smaller" Font-Bold="False" ForeColor="blue" Font-Underline="False"
                                    Enabled="False" Visible="False" meta:resourcekey="hlkAdminRequestListResource1"></asp:HyperLink>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">
                                <asp:HyperLink ID="hlkUserlist" runat="server" Text="User Management" NavigateUrl="~/frmLdapUsers.aspx"
                                    Font-Size="Smaller" Font-Bold="False" ForeColor="blue" Font-Underline="False"
                                    Enabled="False" Visible="False" meta:resourcekey="hlkUserlistResource1"></asp:HyperLink>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
			</div>			
						
		</div>
    <div id="col_main_right" style="font-size: small">
        <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' style="border-color: blue;
            background-color: #EBEBEB">
            <asp:Table ID="Table5" runat="server">
                <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell ID="TableCell1" Font-Size="8pt" runat="server">
                        <asp:Label runat="server" ID="Label2" Text="Login user :" meta:resourcekey="Label1Resource1"></asp:Label>
                        <asp:Label ID="lblLogUser" runat="server" ForeColor="Blue" Width="180px" meta:resourcekey="lblLogUserResource1"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell2" Font-Size="8pt" runat="server">
                        <asp:Label runat="server" ID="Label3" Text="Badge #:" meta:resourcekey="Label2Resource1"></asp:Label>
                        <asp:Label ID="lblBadgeNo" runat="server" ForeColor="Blue" Width="70px" meta:resourcekey="lblBadgeNoResource1"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell3" Font-Size="8pt" runat="server">
                        <asp:Label runat="server" ID="Label4" Text="Department:" meta:resourcekey="Label3Resource1"></asp:Label>
                        <asp:Label ID="lblDepartment" runat="server" ForeColor="Blue" Width="140px" meta:resourcekey="lblDepartmentResource1"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell4" Font-Size="8pt" runat="server">
                        <asp:Label runat="server" ID="Label5" Text="Title:" meta:resourcekey="Label4Resource1"></asp:Label>
                        <asp:Label ID="lblTitle" runat="server" ForeColor="Blue" Width="140px" meta:resourcekey="lblTitleResource1"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell5" Font-Size="8pt" runat="server">
                        <asp:ImageButton ID="imgButEdit" runat="server" ImageUrl="~/Images/edit.png" OnClick="EditLogUserInfo"
                            Width="60px" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
		
		<%--<ajaxToolkit:TabContainer ID="tb_main" runat="server" >
		
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="New Request">
		<ContentTemplate>
		--%>
			<%--<h2 class="section">
				New Request</h2>--%>
			<div class="content_right">
						
			</div>
			
			<h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section">
			<asp:Label runat="server" ID="lblsubTit" Text="New Request" meta:resourcekey="lblsubTitResource1"></asp:Label>			

			
				</h2>
			
				 <%--  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="manStar" Text="* " runat="server" ForeColor="red"></asp:Label> <asp:Label ID="Label2" Text="Mandantory Field" runat="server" Font-Bold="false" Font-Size="Smaller"></asp:Label>                           --%>
				 <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
				
			 <asp:Table ID="table_1" runat="server">
		    <asp:TableRow runat="server">		        
		        <asp:TableCell Width="18%" runat="server">
		        <asp:Label ID="lbl5" runat="server" Text="Full Name" meta:resourcekey="lbl5Resource1"></asp:Label>		        
		        </asp:TableCell>
		        <asp:TableCell Width="25%" HorizontalAlign="Left" runat="server">
		               <asp:TextBox ID="txtFullname"  Width="250px" runat="server" ReadOnly="True"></asp:TextBox>    	        
		             </asp:TableCell>
		             <asp:TableCell Width="20%"  HorizontalAlign="Left" runat="server">
		             <asp:Label ID="Label13" runat="server" Text="Item" meta:resourcekey="Label13Resource1"></asp:Label>&nbsp;&nbsp;
		             </asp:TableCell>
		             <asp:TableCell Width="37%" runat="server">
		             <asp:DropDownList ID="ddlAllocatedAsset" AutoPostBack="True" runat="server" Width ="250px" OnSelectedIndexChanged="ddlAllocatedAsset_OnSelectedIndexChanged">
		             </asp:DropDownList>
		             </asp:TableCell>		             		        
		    </asp:TableRow>		    
		 </asp:Table>
		 </div>
		 <hr />
		 <h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section">
		         <asp:Label ID="Label6" runat="server" Text="Request details" meta:resourcekey="Label6Resource1"></asp:Label>		         
		         </h2>	
		         <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>     
		        <asp:Panel  ID="pnlRequestedFor" runat="server">
		         <asp:Table runat="server" ID="TabRDetails">
		         <asp:TableRow runat="server">
		               
		         </asp:TableRow>		         
		         </asp:Table>
		         </asp:Panel>
		         </div>
		         <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
		         <asp:Table ID="table_2" runat="server" Width="100%">
		         
		            <asp:TableRow runat="server">
		     <asp:TableCell ID="TableCell6" Width="120px" runat="server">
		          <asp:Label ID="Label7" runat="server" Text="Request for" meta:resourcekey="Label7Resource1"></asp:Label>		        		         
		         <asp:CheckBox ID="chbRequestedFor" runat="server" AutoPostBack="True" OnCheckedChanged="chbRequestedFor_OnCheckedChanged"/>
		         </asp:TableCell>
		         <asp:TableCell ID="TableCell7" runat="server">
		         <asp:DropDownList ID="ddlRequestedFor" AutoPostBack="True" Width="180px" runat="server" OnSelectedIndexChanged="ddlRequestedFor_OnSelectedIndexChanged">
		          </asp:DropDownList>
		         </asp:TableCell>		   
		         
		        <asp:TableCell Width="75px" runat="server">
		                      <asp:Label ID="Label9" runat="server" Text="Priority" meta:resourcekey="Label9Resource1"></asp:Label>
		                     
		        </asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:DropDownList ID="ddlPriority" runat="server" Width="220px">
		        <asp:ListItem Value="0" Text="Low" meta:resourcekey="ListItemResource1"></asp:ListItem>
		        <asp:ListItem Value="1" Text="Normal" meta:resourcekey="ListItemResource2"></asp:ListItem>
		        <asp:ListItem Value="2" Text="Medium" meta:resourcekey="ListItemResource3"></asp:ListItem>
		        <asp:ListItem Value="3" Text="High" meta:resourcekey="ListItemResource4"></asp:ListItem>
		        </asp:DropDownList>		              
		    	</asp:TableCell>
		        </asp:TableRow>
		        <asp:TableRow runat="server">
		        <asp:TableCell Width="120px" runat="server">
		        <asp:Label ID="Label10" runat="server" Text="Department" meta:resourcekey="Label10Resource1"></asp:Label>		        
		        </asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtDepartment" runat="server" Width="180px"></asp:TextBox>
		        </asp:TableCell>
		        <asp:TableCell runat="server" Visible="false" >
		        <asp:Label ID="Label11" runat="server" Text="Location" meta:resourcekey="Label11Resource1"></asp:Label>
		        </asp:TableCell>
		        <asp:TableCell runat="server" Visible="false">
		        <asp:TextBox ID="txtLocation" runat="server" Width="250px"></asp:TextBox>
		        </asp:TableCell>
		        <asp:TableCell>
		                <asp:Label ID="lblBuildingName" runat="server" Text="Building"></asp:Label>
		                <font color="red">*</font>
		            </asp:TableCell>
		            <asp:TableCell ID="TableCell8" runat="server" ColumnSpan="3">
		                <asp:DropDownList ID="ddlBuildingName" runat="server" Width="300px" >
		                    <asp:ListItem Selected="True">--Select Your Building--</asp:ListItem>
		                    
		                </asp:DropDownList>
		                <asp:RequiredFieldValidator id="validBuildingName"  runat="server" ErrorMessage="Please select your building"  ControlToValidate="ddlBuildingName"  InitialValue="--Select Your Building--" ValidationGroup="requestGroup"> </asp:RequiredFieldValidator>

		            </asp:TableCell>
		        </asp:TableRow >
		        <asp:TableRow runat="server">
		            
		        </asp:TableRow>
		        </asp:Table>
		        </div>
		        <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
		        <asp:Table runat="server" ID="tabDescrip">
		        
		        <asp:TableRow runat="server">		        
		        <asp:TableCell  VerticalAlign="Top" Width="16%" runat="server">
		                     <asp:Label ID="Label12" runat="server" Text="Description" meta:resourcekey="Label12Resource1"></asp:Label>
		                     <font color="red">*</font>
		                     
		        </asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtDescription" runat="server" MaxLength="3500" TextMode="MultiLine" Width="643px" Height="60px"></asp:TextBox>
		        <asp:RequiredFieldValidator id="validDescription"  runat="server" ErrorMessage="*"  ControlToValidate="txtDescription"   ValidationGroup="requestGroup"> </asp:RequiredFieldValidator>
		    	</asp:TableCell>
		        </asp:TableRow>	
		        <asp:TableRow runat="server">
		        <asp:TableCell runat="server">
		        <asp:Label ID="Label1" text="File Attachment" runat="server" meta:resourcekey="Label1Resource2"></asp:Label>
		        </asp:TableCell>
		        <asp:TableCell runat="server">
		       
		       <asp:FileUpload  ID="UploadedFile" runat="server" meta:resourcekey="UploadedFileResource1" />  
		        
		        </asp:TableCell>
		        </asp:TableRow>		
		        <asp:TableRow runat="server">
		        <asp:TableCell runat="server"></asp:TableCell>
		        </asp:TableRow>		        
		        <asp:TableRow runat="server"> 
		        
		        <asp:TableCell ColumnSpan="2" runat="server">
		        <center>
         <asp:Label runat="server" ID="vldFileType" meta:resourcekey="vldFileTypeResource1"></asp:Label>
         <asp:Button id="btnSave" OnClick="SaveNewRequest"  runat="server" Text="Send" meta:resourcekey="btnSaveResource1" ValidationGroup="requestGroup" CausesValidation="true" />
         <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="Back" OnClick="BacktoList" meta:resourcekey="btnBackResource1" />
         
         
         
       </center>
       
       <font color="red">* Mandatory Field.</font>
		        </asp:TableCell>		        
		        </asp:TableRow> 
		        <asp:TableRow runat="server">
		        <asp:TableCell runat="server">
		        <asp:Label ID="lblSavedStat" runat="server" meta:resourcekey="lblSavedStatResource1"></asp:Label>
		        </asp:TableCell>
		        </asp:TableRow>       		        	        
		        </asp:Table>
		        </div>		        
		 <%-- </ContentTemplate>
		</ajaxToolkit:TabPanel>		
	
		</ajaxToolkit:TabContainer>--%>
		
		</div>		
		</div>		
    
 
</asp:Content>



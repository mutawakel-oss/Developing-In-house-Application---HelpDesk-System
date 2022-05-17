<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmRequestDetails.aspx.cs" Inherits="Default2" Title="Help Desk" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
<div  id="body" style="width:1000px">
		<div id="col_main_left">
			
			<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>						
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server">
		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server"></asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">		
		<asp:LinkButton ID="lbtnTaskList" Text="Own Requests" runat="server" ForeColor="blue" Font-Underline="false" Font-Size="Smaller" Font-Bold="false" OnClick="mOwnRequestClick"></asp:LinkButton>
		</asp:TableCell>
		</asp:TableRow>		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">		
		<asp:ImageButton ID="hlkTechReqestList" runat="server" ImageUrl="~/Images/Own_Requests.png" OnClick="GotoRequestList" Visible=false Width="190px" CausesValidation=false />
		</asp:TableCell>
		</asp:TableRow>	
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">		
		<asp:HyperLink ID="hlkNewRequest" runat="server" Text="New Request" NavigateUrl="~/frmNewRequest.aspx" Font-Size="Smaller" Font-Bold="False" ForeColor="blue" Font-Underline="False" Enabled="False" Visible="False" meta:resourcekey="hlkNewRequestResource1"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>			
		</asp:Table>
		</div>
			
			
		</div>
		<div id="col_main_right">
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' style="border-color:blue; background-color:#EBEBEB">
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
		<h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section">
		<asp:Label runat="server" ID="Label5" Text="Request Details" meta:resourcekey="Label5Resource1"></asp:Label>
				</h2>	
		
		<%--<ajaxToolkit:TabContainer ID="tb_main" runat="server" >
		
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="Request Details">
		<ContentTemplate>--%>
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="content_right">
			</div>
			<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
			 <asp:Panel  runat="server" ID="pnlRequestFor" Enabled="False" Visible="False">
			 <asp:Table runat="server" ID="tblRfor">
			 <asp:TableRow runat="server">
			 <asp:TableCell Width="120px" Font-Size="Smaller"  Font-Italic="True" meta:resourcekey="TableCellResource5" runat="server" Text="Request For "></asp:TableCell>
			 <asp:TableCell runat="server">
			  <asp:Label ID="lblRequestFor"  Font-Size="Smaller"  Font-Italic="True" runat="server" meta:resourcekey="lblRequestForResource1"></asp:Label>
			 </asp:TableCell>
			 </asp:TableRow>			 
			 </asp:Table>
			 </asp:Panel>
			 </div>
			 <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
			 <asp:Table ID="table_1" runat="server" Width="344px"> 	 
			  <asp:TableRow runat="server">		        
		        <asp:TableCell Width="120px" Font-Size="Smaller" meta:resourcekey="TableCellResource7" runat="server" Text="		        
		        Requester Name" ></asp:TableCell>
		        <asp:TableCell runat="server">
		          <asp:TextBox ID="txtFullname"  Width="210px" runat="server" ReadOnly="True" meta:resourcekey="txtFullnameResource1"></asp:TextBox>		                     
		        </asp:TableCell>	     
		    </asp:TableRow>	
		 </asp:Table>
		 <asp:Table id="escalationTable" runat=server  Font-Size="Smaller" >
		   <asp:TableRow ID="TableRow2" runat="server">
		     <asp:TableCell ID="TableCell6" runat="server">
		        <asp:Panel ID="escalationPanel" runat=server Visible=false >
		        <asp:CheckBox ID="chkEscalate" runat=server AutoPostBack=True OnCheckedChanged="mFindItMembers" />
		        <asp:Label ID="lblEscalatTo" runat=server Text="Escalate To:"></asp:Label>
		        <asp:DropDownList ID="escalteToDDL" AutoPostBack=True runat=server Enabled=False OnSelectedIndexChanged="mEscalationDDLSelcted"  ></asp:DropDownList>
		        <asp:Button ID="btnEscalateTo" runat=server Text="Escalate" Enabled=False OnClick="mBtnEscalateClicked" CausesValidation=false />
		        </asp:Panel>
		        </asp:TableCell>	      
		    </asp:TableRow>	    
		 </asp:Table>
		 </div>
		 
	<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
	
		 <asp:Table ID="table_2" runat="server">
		 
		    <asp:TableRow runat="server" >
		        <asp:TableCell Width="120px" Font-Size="Smaller" meta:resourcekey="TableCellResource9" runat="server" Text="Category">*</asp:TableCell>		        
		        <asp:TableCell Width="41%" runat="server" >
		        <asp:TextBox ID="txtCatagory" runat="server" ReadOnly="True" Width="210px"></asp:TextBox>
		        <asp:DropDownList ID="ddlCategory" runat="server" Width="215px" Enabled="False" Visible="False"></asp:DropDownList> 
		        
		        </asp:TableCell>
		       
		        <asp:TableCell Width="105px" Font-Size="Smaller" runat="server"> 
		        <asp:Label ID="lblStatus" Text="Status" runat="server"  Enabled="False" Visible="False" meta:resourcekey="lblStatusResource1"></asp:Label>
		        </asp:TableCell>
		        <asp:TableCell Width="160px" HorizontalAlign="Right" runat="server">
		        <asp:TextBox ID="txtStatus" runat="server" Enabled="False" Visible="False" Width="210px" ReadOnly="True"></asp:TextBox>
		        <asp:DropDownList ID="ddlStatus" runat="server" Enabled="False" Visible="False" Width="210px"></asp:DropDownList>
		        <asp:RequiredFieldValidator ID="vldCategory" runat="server" ControlToValidate="ddlStatus" InitialValue="2" ErrorMessage="*" meta:resourcekey="vldCategoryResource1"></asp:RequiredFieldValidator>
		        </asp:TableCell>		        	        
		        </asp:TableRow>
		        <asp:TableRow runat="server">
		        <asp:TableCell Font-Size="Smaller" meta:resourcekey="TableCellResource13" runat="server" Text="Department" ></asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtDepartment" runat="server" Width="210px" ReadOnly="True" meta:resourcekey="txtDepartmentResource1">
		        </asp:TextBox></asp:TableCell>
		        <asp:TableCell Font-Size="Smaller" meta:resourcekey="TableCellResource15" runat="server" Text="Location"></asp:TableCell>
		        <asp:TableCell HorizontalAlign="Left" runat="server">
		        <asp:TextBox ID="txtLocation" runat="server" Width="210px" ReadOnly="True" meta:resourcekey="txtLocationResource1"></asp:TextBox></asp:TableCell>
		        </asp:TableRow>	
		        <asp:TableRow runat="server">
		        <asp:TableCell Font-Size="Smaller" meta:resourcekey="TableCellResource17" runat="server" Text="Priority"></asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtPriority" runat="server" ReadOnly="True" Width="210px"></asp:TextBox>	
		        <asp:DropDownList ID="ddlPriority" runat="server" Width="215px" Enabled="False" Visible="False">
		        <asp:ListItem Value="0" Text="Low" meta:resourcekey="ListItemResource1"></asp:ListItem>
		        <asp:ListItem Value="1" Text="Normal" meta:resourcekey="ListItemResource2"></asp:ListItem>
		        <asp:ListItem Value="2" Text="Medium" meta:resourcekey="ListItemResource3"></asp:ListItem>
		        <asp:ListItem Value="3" Text="High" meta:resourcekey="ListItemResource4"></asp:ListItem>
		        </asp:DropDownList>		                
		    	</asp:TableCell>		        
		        <asp:TableCell Font-Size="Smaller" meta:resourcekey="TableCellResource19" runat="server" Text="Requested on" ></asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtCreatedOn" runat="server" Width="210px"></asp:TextBox>		                
		    	</asp:TableCell>
		        </asp:TableRow>
		    </asp:Table>
		    
		    <asp:Table runat="server" ID="tabDescrib">    
		        <asp:TableRow runat="server">
		        <asp:TableCell Font-Size="Smaller" VerticalAlign="Top" Width="16%" meta:resourcekey="TableCellResource21" runat="server" Text="Description"></asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtDescription" runat="server"  MaxLength="500" TextMode="MultiLine" Width="640px" Height="60px" ReadOnly="True"></asp:TextBox>
		         </asp:TableCell>
		        </asp:TableRow>
		          <asp:TableRow>
		       <asp:TableCell ID="TableCell7" ColumnSpan=2 Font-Size="Smaller" VerticalAlign="Top" Width="16%"  runat="server">
		       <asp:Label ID="lblExtensionNo" runat=server Text="Extension No:" ></asp:Label>
		       <asp:Label ID="txtExtensionNo" runat=server ForeColor=red></asp:Label>
		       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		       <asp:Label ID="lblPagerNo" runat=server Text="Pager No:"></asp:Label>
		       <asp:Label ID="txtPagerNo" runat=server ForeColor=red ></asp:Label>
		       </asp:TableCell>
		       </asp:TableRow>
		       	</asp:Table>
		       
		       <asp:Table runat="server" Width="100%" ID="tabFileatta">		        
		        <asp:TableRow runat="server">
		        <asp:TableCell Font-Size="Smaller" Width="16%" meta:resourcekey="TableCellResource23" runat="server" Text="File Attached"></asp:TableCell>
		        <asp:TableCell Width="41%" runat="server">	       
		        <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="Smaller" Text="Attachment" CausesValidation="False" OnClick="FileOpen" meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>
		    	&nbsp;&nbsp;
		    	<asp:Label ID="lblDueDate" runat="server" Font-Size="Smaller" Font-Italic="True" Text=" Due Date " meta:resourcekey="lblDueDateResource1"></asp:Label>
		    	</asp:TableCell>		         
		       
		        <asp:TableCell Font-Size="Smaller"  Width="105px" HorizontalAlign="Right" runat="server">
		               <asp:Label ID="lblAssignedTo" Text="Assigned To" runat="server" meta:resourcekey="lblAssignedToResource1"></asp:Label>
		        </asp:TableCell>
		        <asp:TableCell runat="server">		       
		        <asp:DropDownList ID="ddlAssignedTo" runat="server" Width="210px"></asp:DropDownList>
		        <asp:TextBox ID="txtAssignedTo" runat="server" Enabled="False" Visible="False" ReadOnly="True" Width="210px"></asp:TextBox>
		        <asp:RequiredFieldValidator ID="vldAssinged" ValidationGroup="Assign" ControlToValidate="ddlAssignedTo" InitialValue="0" ErrorMessage="*" runat="server" meta:resourcekey="vldAssingedResource1"></asp:RequiredFieldValidator>		                
		    	</asp:TableCell>
		        </asp:TableRow>	        	        
		        </asp:Table>
		        
		        <asp:Table runat="server" ID="tabComment">		        	
		            
		        <asp:TableRow runat="server"> 
		        <asp:TableCell VerticalAlign="Top" Width="16%" runat="server">
		         <asp:Label ID="lblComments" runat="server" Text="Comments" Enabled="False" Visible="False" Font-Size="8pt" Font-Bold="True" meta:resourcekey="lblCommentsResource1"></asp:Label> 
		        </asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtComments" runat="server" Enabled="False" Visible="False"  MaxLength="500" TextMode="MultiLine" Width="640px" Height="80px"></asp:TextBox>
		        <asp:RequiredFieldValidator ID="commentValidator" runat="server" ControlToValidate="txtComments" ValidationGroup="updateGroup" ErrorMessage="*"></asp:RequiredFieldValidator>
		        </asp:TableCell>
		       
		        </asp:TableRow>
		        </asp:Table>
        <asp:Button ID="btnEvaluate" runat="server" OnClick="btnEvaluate_Click" Text="Service Evaluation" CausesValidation="False" /><br />
		        
		        <asp:Table runat="server" Width="100%" ID="tabButt">
		            <asp:TableRow HorizontalAlign="Right" Width="100%" runat="server">		        	        
		                <asp:TableCell HorizontalAlign="Right" runat="server">
                            <asp:Button ID="btnAssign" runat="server" OnClick="AssignTask" Text="Assign" ValidationGroup="Assign" meta:resourcekey="btnAssignResource1"/>
                            <asp:Button ID="btnBack" runat="server" OnClick="BackToList" CausesValidation="False" Text="Back" meta:resourcekey="btnBackResource1" />
                            <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status"  OnClick="UpdateStatus" ValidationGroup="updateGroup" CausesValidation=true Enabled="False" Visible="False" meta:resourcekey="btnUpdateStatusResource1"/>
                            <asp:Button ID="btnSave" runat="server"  Text="Save"  OnClick="SaveRequestChanges" Enabled="False" Visible="False" meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnCancelRequest" runat="server" Text="Cancel Request" Enabled="False" Visible="False" OnClick="CancelRequest" meta:resourcekey="btnCancelRequestResource1" />
                      &nbsp;&nbsp;&nbsp;
		            <asp:LinkButton ID="lnkAdminComment" runat="server" Font-Size="Smaller" Text="Admin Comment" Enabled="False" Visible="False" OnClick="OnClickAdminComment" meta:resourcekey="lnkAdminCommentResource1"></asp:LinkButton>		       
		            <asp:Panel ID="pnlAdminCommentEntry" runat="server" CssClass="modalPopup"  Height="250px" Width="470px" BorderStyle="Solid" Enabled="False" Visible="False">
		            <div  style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Blue;height:30px" >            
                 <h2 class="section" style=" background-color:Gray; text-align:left;">
                    <asp:Label runat="server" ID="lblAdminC" Text="Admin Comment" meta:resourcekey="lblAdminCResource1"></asp:Label> </h2></div>                   
                    
                    <asp:Table ID="Table3" runat="server">
                    <asp:TableHeaderRow VerticalAlign="Middle" runat="server">
                    <asp:TableHeaderCell runat="server"></asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow runat="server">
                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    <asp:Label runat="server" Text="Comment :" meta:resourcekey="LabelResource1"></asp:Label>
</asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow HorizontalAlign="Left" runat="server">
                 <asp:TableCell HorizontalAlign="Left" runat="server">
                   <asp:TextBox ID="txtAdminComment" TextMode="MultiLine" ValidationGroup="grp1" Width="460px" Height="150px"  runat="server" EnableViewState="False"></asp:TextBox>                  
                 <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAdminComment" ErrorMessage="*" meta:resourcekey="RequiredFieldValidatorResource1"></asp:RequiredFieldValidator>
                  </asp:TableCell>                                    
                    </asp:TableRow>                                       
                    </asp:Table>
                    <div style=" text-align:center">                   
                      <asp:Button ID="btnDone" runat="server"  ValidationGroup="grp1" OnClick="SaveAdminComment" Text="Done" meta:resourcekey="btnDoneResource1" />
                      <asp:Button ID="btnCancelPWD" CausesValidation="False" runat="server" Text="Cancel" meta:resourcekey="btnCancelPWDResource1" />
                      </div>
                      </asp:Panel>
		        
		         <ajaxToolkit:ModalPopupExtender ID="modal" runat="server"  BackgroundCssClass="modalBackground" 
		            TargetControlID="lnkAdminComment"  PopupControlID="pnlAdminCommentEntry" 
		            
		            CancelControlID="btnCancelPWD" BehaviorID="modal" DynamicServicePath="" Enabled="True">		         		         
		         </ajaxToolkit:ModalPopupExtender>                            
		                </asp:TableCell>
		            </asp:TableRow>		          		        	        
		        </asp:Table>
		       
		       <br />
		       <asp:Table runat="server" ID="tabcoom">
                <asp:TableRow  HorizontalAlign="Left" runat="server">
                <asp:TableCell runat="server">
                <asp:Label ID="lblAdmincomments" Text="Admin Comments" runat="server" Font-Size="8pt" Font-Bold="True" Enabled="False" Visible="False" meta:resourcekey="lblAdmincommentsResource1"></asp:Label>
                </asp:TableCell>
                </asp:TableRow>		       
		       </asp:Table>
		       
		       <asp:Panel runat="server" ID="pnlAdminComment" BorderWidth="1px" BorderColor="Black" Enabled="False" Visible="False"> 
		       
		       <div id="divAdminComments" visible="false">
		                <table width="100%" id="tComme"><tr><td>		        
		                     <asp:GridView ID="gdvAdminComment" runat="server" Height="50px"  Font-Size="12pt" 
                                    Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="True" PageSize="6" OnPageIndexChanging="gdvAdminComment_PageIndexChanging"> 
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                    <Columns>                
                                   <asp:BoundField DataField ="comment_date" headertext ="Date" HtmlEncode="False" DataFormatString="{0:dd/MMM/yyyy ddd,hh:mm:ss tt}" meta:resourcekey="BoundFieldResource1">
                                       <ItemStyle Width="20%" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Comment" HeaderText="Comment" meta:resourcekey="BoundFieldResource2" >
                                       <ItemStyle Width="75%" HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   
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
		       </asp:Panel>   
		       </div>
		        
		        <h2 runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="section">
		        <asp:Label runat="server" ID="lblServiceH" Text="Service History" meta:resourcekey="lblServiceHResource1"></asp:Label>
		        </h2>
		            <table width="100%" id="tabRserv"><tr><td>		        
		                    <asp:GridView ID="gdvRequestService" runat="server" Height="50px" RowStyle-Height="3px"  Font-Size="12pt"
                                    Style="position: static" Width="100%" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="True" PageSize="9" OnPageIndexChanging="gdvRequestService_PageIndexChanging"> 
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                    <Columns>                
                                   <asp:BoundField DataField ="modified_date" headertext ="Date" HtmlEncode="False" DataFormatString="{0:dd/MMM/yyyy ddd,hh:mm:ss tt}" meta:resourcekey="BoundFieldResource3">
                                       <ItemStyle Width="20%" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Comment" HeaderText="Comment" meta:resourcekey="BoundFieldResource4" >
                                       <ItemStyle Width="45%" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Status" HeaderText="Status" meta:resourcekey="BoundFieldResource5" >
                                       <ItemStyle Width="10%" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Full_name" headertext="Serviced By" meta:resourcekey="BoundFieldResource6">
                                       <ItemStyle Width="25%" />
                                   </asp:BoundField>
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
		             
		             		        
		       <%--</ContentTemplate>
		    </ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>--%>
		<%--<center>
        <asp:Button ID="btnAssign" runat="server" OnClick="AssignTask" Text="Assign" />
        <asp:Button ID="btnBack" runat="server" OnClick="BackToList" CausesValidation="false" Text="Back" />
       </center>	--%>	
       <font color="red">* Mandatory Field.</font>
		</div>		
		</div>		
		
</asp:Content>


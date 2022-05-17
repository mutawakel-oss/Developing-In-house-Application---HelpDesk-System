<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmRequestStatus.aspx.cs" Inherits="Default2" Title="Service Evaluation Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
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
		<asp:HyperLink ID="hlkUserRequestList" runat="server" Text="Requests" NavigateUrl="~/frmUserRequestList.aspx" Font-Size="Smaller" Font-Bold="False" ForeColor="Black" Font-Underline="False" Enabled="False" Visible="False" meta:resourcekey="hlkUserRequestListResource1"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		
		
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">		
		<asp:HyperLink ID="hlkAdminRequestList" runat="server" Text="All Requests" NavigateUrl="~/frmAdminRequestList.aspx" Font-Size="Smaller" Font-Bold="False" ForeColor="Black" Font-Underline="False" Enabled="False" Visible="False" meta:resourcekey="hlkAdminRequestListResource1"></asp:HyperLink>
		
		<asp:LinkButton ID="hlkTechReqestList" runat="server" OnClick = "GotoRequestList" Text="Requests" Font-Size="Smaller" Font-Bold="False" ForeColor="Black" Font-Underline="False" Enabled="False" Visible="False" meta:resourcekey="hlkTechReqestListResource1"></asp:LinkButton>
		</asp:TableCell>
		</asp:TableRow>	
		<asp:TableRow runat="server">
		<asp:TableCell runat="server">		
		<asp:HyperLink ID="hlkNewRequest" runat="server" Text="New Request" NavigateUrl="~/frmNewRequest.aspx" Font-Size="Smaller" Font-Bold="False" ForeColor="Black" Font-Underline="False" Enabled="False" Visible="False" meta:resourcekey="hlkNewRequestResource1"></asp:HyperLink>
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
		<asp:Label runat="server" ID="Label5" Text="Service Evaluation Page" meta:resourcekey="Label5Resource1" Width="238px" Font-Size="Large"></asp:Label>
				</h2>	
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="content_right">
            </div>
            <asp:Label ID="lblRequestDetails" runat="server" BackColor="#E0E0E0" Font-Size="Medium" meta:resourcekey="Label5Resource1"
                Text="Request Details" Width="114px" ForeColor=blue></asp:Label>
            <br />
            <br />
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
                 &nbsp;<asp:Table ID="table_1" runat="server"> 	 
			  <asp:TableRow runat="server">		        
		        <asp:TableCell Width="120px" Font-Size="Smaller" meta:resourcekey="TableCellResource7" runat="server" Text="		        
		        Requester Name" ></asp:TableCell>
		        <asp:TableCell runat="server">
		          <asp:TextBox ID="txtFullname"  Width="250px" runat="server" ReadOnly="True" meta:resourcekey="txtFullnameResource1"></asp:TextBox>		                     
		        </asp:TableCell>		       	        
		    </asp:TableRow>		    
		 </asp:Table>
		 </div>
	<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
		 <asp:Table ID="table_2" runat="server">
		    <asp:TableRow runat="server" >
		        <asp:TableCell Width="120px" Font-Size="Smaller" meta:resourcekey="TableCellResource9" runat="server" Text="Category"></asp:TableCell>		        
		        <asp:TableCell Width="41%" runat="server" >
		        <asp:TextBox ID="txtCatagory" runat="server" ReadOnly="True" Width="210px"></asp:TextBox>
		        <asp:DropDownList ID="ddlCategory" runat="server" Width="215px" Enabled="False" Visible="False"></asp:DropDownList> 
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory" InitialValue="0" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
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
		       	</asp:Table>
		       <asp:Table runat="server" Width="100%" ID="tabFileatta">		        
		        <asp:TableRow runat="server">
		        <asp:TableCell Font-Size="Smaller"  Width="105px"  runat="server">
		               <asp:Label ID="lblAssignedTo" Text="Assigned To" runat="server" meta:resourcekey="lblAssignedToResource1"></asp:Label>
		        </asp:TableCell>
		        <asp:TableCell runat="server">		       
		        <asp:DropDownList ID="ddlAssignedTo" runat="server" Width="210px"></asp:DropDownList>
		        <asp:TextBox ID="txtAssignedTo" runat="server" Enabled="False" Visible="False" ReadOnly="True" Width="250px"></asp:TextBox>
		        <asp:RequiredFieldValidator ID="vldAssinged" ValidationGroup="Assign" ControlToValidate="ddlAssignedTo" InitialValue="0" ErrorMessage="*" runat="server" meta:resourcekey="vldAssingedResource1"></asp:RequiredFieldValidator>		                
		    	</asp:TableCell>
		        </asp:TableRow>	        	        
		        </asp:Table>
		       	<hr />
        <asp:Label ID="lblRepairDetails" runat="server" BackColor="#E0E0E0" Font-Size="Medium" meta:resourcekey="Label5Resource1"
            Text="Repair Deatails" Width="114px" ForeColor=blue></asp:Label><br />
        <br />
		       	<asp:Table runat="server" ID="tabCompletion">    
		        <asp:TableRow ID="TableRow2" runat="server">
		        <asp:TableCell ID="TableCell6" Font-Size="Smaller" VerticalAlign="Top" Width="16%" meta:resourcekey="TableCellResource21" runat="server" Text="Completed By:"></asp:TableCell>
		        <asp:TableCell ID="TableCell7" runat="server">
		        <asp:TextBox ID="txtCompletedBy" runat="server"   Width="210px"  ReadOnly="True"></asp:TextBox>
		         </asp:TableCell>
		        </asp:TableRow>
		        <asp:TableRow>
		        <asp:TableCell Font-Size=smaller VerticalAlign=top Width="16%"  runat="server" Text="Date"></asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtCompletionDate" runat="server" Width="210px" ReadOnly=true></asp:TextBox>
		        </asp:TableCell>
		        </asp:TableRow>
		        <asp:TableRow>
		        <asp:TableCell Font-Size=Smaller VerticalAlign=top Width="16%" runat="server" Text="Time"></asp:TableCell>
		        <asp:TableCell runat=server> 
		        <asp:TextBox ID="txtCompletionTime" runat=server Width="210px" ReadOnly=true></asp:TextBox>
		        </asp:TableCell>
		        </asp:TableRow>
		        <asp:TableRow>
		        <asp:TableCell Font-Size=smaller VerticalAlign=top Width="16%" runat=server Text="Proper Action" ></asp:TableCell>
		        <asp:TableCell runat="server">
		        <asp:TextBox ID="txtProerAction" runat="server" Width="210px" ReadOnly=true></asp:TextBox>
		        </asp:TableCell>
		        </asp:TableRow>
		        </asp:Table>
		        <hr />
        <asp:Label ID="lblEvaluationDetails" runat="server" BackColor="#E0E0E0" Font-Size="Medium" meta:resourcekey="Label5Resource1"
            Text="Evaluation Details" Width="139px" ForeColor=blue></asp:Label><br />
        <br />
            <asp:Label ID="lblCurrentEvaluation" runat=server Visible=false ></asp:Label>&nbsp;<br />
        <asp:Button ID="btnEvaluation" runat="server" OnClick="btnEvaluation_Click" Text="Evaluate"
            Visible="False" Width="95px" /><br />
		        <asp:Table  runat="server" ID="requesterStatusTable">
		        <asp:TableRow>
		        <asp:TableCell>
		        <asp:Label ID="lblNoComplete" runat="server" Text="The request did not completed." Visible=false></asp:Label>
		        </asp:TableCell>
		        </asp:TableRow>
		        
		        <asp:TableRow>
		        <asp:TableCell>
		        <asp:Label ID="lblRate"  runat=server Text="Rate of the service: "></asp:Label>
		        </asp:TableCell>
		            <asp:TableCell>
		                <ajaxToolkit:Rating ID="rating" runat="server"
    CurrentRating="2"
    MaxRating="5"
    StarCssClass="ratingStar"
    WaitingStarCssClass="savedRatingStar"
    FilledStarCssClass="filledRatingStar"
    EmptyStarCssClass="emptyRatingStar"></ajaxToolkit:Rating>    
		            </asp:TableCell>
		        </asp:TableRow>
		       
		        <asp:TableRow>
		        <asp:TableCell>
		        <asp:TextBox ID="txtComments" runat="server" Height="115px" MaxLength="200" Width="150px" TextMode="MultiLine"></asp:TextBox>
		        </asp:TableCell>
		        </asp:TableRow>
		       	</asp:Table>
        <br />	       	
		       	
    <br />
		       <hr />
        &nbsp;
		        <br />
		        
		        <asp:Table runat="server" Width="100%" ID="tabButt">
		            <asp:TableRow HorizontalAlign="Right" Width="100%" runat="server">		        	        
		                <asp:TableCell HorizontalAlign="Right" runat="server">
                            <asp:Button ID="btnAssign" runat="server" OnClick="AssignTask" Text="Assign" ValidationGroup="Assign" meta:resourcekey="btnAssignResource1"/>
                            <asp:Button ID="btnBack" runat="server" OnClick="BackToList" CausesValidation="False" Text="Back" meta:resourcekey="btnBackResource1" />
                            <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status"  OnClick="UpdateStatus" Enabled="False" Visible="False" meta:resourcekey="btnUpdateStatusResource1" />
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
        &nbsp;<br />
        &nbsp;
		       </div>
		             		        
		       <%--</ContentTemplate>
		    </ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>--%>
		<%--<center>
        <asp:Button ID="btnAssign" runat="server" OnClick="AssignTask" Text="Assign" />
        <asp:Button ID="btnBack" runat="server" OnClick="BackToList" CausesValidation="false" Text="Back" />
       </center>	--%>	
		</div>		
		</div>		
		
</asp:Content>


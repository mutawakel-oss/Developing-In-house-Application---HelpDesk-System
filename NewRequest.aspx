<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
CodeFile="NewRequest.aspx.cs" Inherits="Default2" Title="Untitled Page" %>
<%@register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">


<div id="body">
		<div id="col_main_left">
			<div id="user_assistance">
				<a id="content_start"></a>
				<h3>
					Help and Other Links</h3>
				
			</div>
		</div>
		<div id="col_main_right">
		
		<ajaxToolkit:TabContainer ID="tb_main" runat="server" >
		
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="New Request">
		<ContentTemplate>
		
			<%--<h2 class="section">
				New Request</h2>--%>
			<div class="content_right">
			</div>
			<%--<h2 class="section">
				New Request</h2>--%>
			 <asp:Table ID="table_1" runat="server">
		    <asp:TableRow>
		        
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">		        
		        Full Name</asp:TableCell>
		        <asp:TableCell>
		        
		        <asp:TextBox ID="txtFullname"  Width="210px" runat="server"></asp:TextBox>		                     
		        </asp:TableCell>		        
		    </asp:TableRow>		    
		 </asp:Table>
		 <hr />
		 <h2 class="section">
		         Request details
		         </h2>
		         <asp:Table ID="table_2" runat="server">
		            <asp:TableRow>
		        <asp:TableCell Width="130px" Font-Size="Smaller" Font-Bold="true">
		            Category
		        </asp:TableCell>
		        
		        <asp:TableCell>
		        <asp:DropDownList ID="ddlCategory" runat="server" ></asp:DropDownList>
		    
		        </asp:TableCell>
		        </asp:TableRow>
		    
		        <asp:TableRow>
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		                     Priority
		        </asp:TableCell>
		        <asp:TableCell>
		        <asp:DropDownList ID="ddlPriority" runat="server">
		        <asp:ListItem Value="0" Text="Low"></asp:ListItem>
		        <asp:ListItem Value="1" Text="Normal"></asp:ListItem>
		        <asp:ListItem Value="2" Text="Medium"></asp:ListItem>
		        <asp:ListItem Value="3" Text="High"></asp:ListItem>
		        </asp:DropDownList>
		                
		    	</asp:TableCell>
		        </asp:TableRow>
		        <asp:TableRow>
		        
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true" VerticalAlign="Top">
		                     Decription
		        </asp:TableCell>
		        <asp:TableCell>
		        <asp:TextBox ID="txtDescription" runat="server"  MaxLength="500" TextMode="MultiLine" Width="400px" Height="200px" > </asp:TextBox>
		    	</asp:TableCell>
		        </asp:TableRow>	
		        <asp:TableRow>
		        <asp:TableCell></asp:TableCell>
		        <asp:TableCell>
		       
		         <asp:LinkButton ID="lnkAttachFile" runat="server" Text="Attach a File"></asp:LinkButton>
		         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		         <asp:Label text="Attached Files" runat="server"></asp:Label>
		         <asp:TextBox ID="txtAttachedFiles" runat="server"></asp:TextBox>
		         <asp:Panel ID="pnl_reset_password" runat="server" CssClass="modalPopup"  Height="200px" Width="340px" BorderStyle="Solid" >
		            <div  style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:30px" >            
                 <h2 class="section">
                    Attach File </h2></div>                    
                    <asp:Table runat="server">
                    <asp:TableHeaderRow VerticalAlign="Middle">
                    <asp:TableHeaderCell></asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                    <asp:TableCell>
                    
                    <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell Font-Size="Smaller" Font-Bold="true">
                        
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label  Font-Size="Smaller" Font-Bold="true" ID="lblLoginName" runat="server"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow VerticalAlign="Middle">
                    <asp:TableCell font-Size="Smaller" Font-Bold="true">Choose a file</asp:TableCell>
                    <asp:TableCell>
                     <asp:TextBox ID="txtFileList" TextMode="Password" runat="server"></asp:TextBox>
                     <asp:Button ID="btnBrowse" Text="Browse..." runat="server" />
                    </asp:TableCell>                    
                    </asp:TableRow>
                    </asp:Table>
                    
                    </asp:TableCell>
                    </asp:TableRow>
                                       
                    </asp:Table>
                    <div align="center">                   
                      <asp:Button ID="btnDone" CausesValidation="false" runat="server" Text="Done" />
                      <asp:Button ID="btnCancelPWD" CausesValidation="false" runat="server" Text="Cancel" />
                      </div>
		         </asp:Panel>
		         <ajaxToolkit:ModalPopupExtender ID="modal" runat="server"  BackgroundCssClass="modalBackground" 
		            TargetControlID="lnkAttachFile"  PopupControlID="pnl_reset_password"
		            CancelControlID="btnCancelPWD">		         		         
		         </ajaxToolkit:ModalPopupExtender>
		        
		        </asp:TableCell>
		        </asp:TableRow>		
		        <asp:TableRow>
		        <asp:TableCell></asp:TableCell>
		        </asp:TableRow>		        
		        <asp:TableRow> 
		        
		        <asp:TableCell ColumnSpan="2">
		        <center>
         <asp:Button id="btnSave" OnClick = "SaveNewRequest"  runat="server" Text="Save"  />
         <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
       </center>
		        </asp:TableCell>
		        </asp:TableRow>        		        	        
		        </asp:Table>		        
		        </ContentTemplate>
		</ajaxToolkit:TabPanel>		
		<%--<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Request List">
	<ContentTemplate>
		<h2 class="section">
				Filter By <asp:DropDownList id="ddlFilter" runat="server"></asp:DropDownList></h2>
		
		<h2 class="section">
				Request List</h2>
				
		
		<table width="50%"><tr><td>
		    <asp:GridView ID="gdvRequestList" runat="server" Height="75px" 
            Style="position: static" Width="1015px" AutoGenerateColumns="False" CellPadding="0" GridLines="Both" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" Font-Names="BrowalliaUPC" HorizontalAlign="Left" AllowPaging="true" PageSize="29" >
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <Columns>                
           <asp:HyperLinkField  ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10px" HeaderText="Request ID" DataTextField= "id" Target="_parent" datanavigateurlfields="id" datanavigateurlformatstring= "frmRequestDetails.aspx?id={0}" /> 
           <asp:BoundField DataField="category" HeaderText="category" ItemStyle-Width="20px" />
           <asp:BoundField DataField="Status" headertext="Status" ItemStyle-Width="20px"  />
           <asp:BoundField DataField="created_date" headertext="created_date" ItemStyle-Width="20px" />                     
           
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
		
		
		</ContentTemplate>	
		</ajaxToolkit:TabPanel>		
		--%>
		</ajaxToolkit:TabContainer>
		
		</div>		
		</div>		
		
</asp:Content>


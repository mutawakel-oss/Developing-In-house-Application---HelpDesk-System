<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmInvProductDetails.aspx.cs" Inherits="frmInvProductDetails" Title="Help Desk | Inventory" %>
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
		<asp:HyperLink ID="hlkProductList" runat="server" Text="Product List"  NavigateUrl="~/frmInvProductList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		
		<%--<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink1" runat="server" Text="Requests" NavigateUrl="~/frmAdminRequestList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>
		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink3" runat="server" Text="New Request" NavigateUrl="~/frmNewRequest.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		--%>
		
		</asp:Table>
		</div>			
		</div>
		<div id="col_main_right">
		<div style="border-color:blue; background-color:#EBEBEB">
		<asp:Table ID="Table9" runat="server">
		<asp:TableRow ID="TableRow1" runat="server">		
		<asp:TableCell ID="TableCell1" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label2" Text="Login user :" meta:resourcekey="Label1Resource1"></asp:Label>
		<asp:Label ID="lblLogUser" runat="server" ForeColor="Blue" Width="180px" meta:resourcekey="lblLogUserResource1"></asp:Label>
		</asp:TableCell>		
		<asp:TableCell ID="TableCell2" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label3" Text="Badge #:" meta:resourcekey="Label2Resource1"></asp:Label>
		<asp:Label ID="lblBadgeNo" runat="server"  ForeColor="Blue" Width="70px" meta:resourcekey="lblBadgeNoResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell ID="TableCell3" Font-Size="8pt" runat="server"> 
		<asp:Label runat="server" ID="Label4" Text="Department:" meta:resourcekey="Label3Resource1"></asp:Label>
		<asp:Label ID="lblDepartment" runat="server"  ForeColor="Blue" Width="140px" meta:resourcekey="lblDepartmentResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell ID="TableCell4" Font-Size="8pt" runat="server">
		<asp:Label runat="server" ID="Label5" Text="Title:" meta:resourcekey="Label4Resource1"></asp:Label>
		<asp:Label ID="lblTitle" runat="server"  ForeColor="Blue" Width="140px" meta:resourcekey="lblTitleResource1"></asp:Label>
		</asp:TableCell>
		<asp:TableCell ID="TableCell5" Font-Size="8pt" runat="server">
		<asp:LinkButton ID="lbtnUserInfoEdit" runat="server" Text="Edit" OnClick="EditLogUserInfo" meta:resourcekey="lbtnUserInfoEditResource1" ></asp:LinkButton>		
		</asp:TableCell>		
		</asp:TableRow>
		</asp:Table>
		</div>		
           
		<ajaxToolkit:TabContainer ID="tb_main" runat="server" >
		
		<ajaxToolkit:TabPanel ID="general_tab" runat="server" HeaderText="Inventory">
		<ContentTemplate><h2 class="section">
		         Product details         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="manStar" Text="*" runat="server" ForeColor="red"></asp:Label> <asp:Label ID="Label1" Text="Mandantory Field" runat="server" Font-Bold="false" Font-Size="Smaller"></asp:Label>      
		         </h2>		
		         
		          <asp:Table ID="table10" runat="server" Width="58%">
		    <asp:TableRow HorizontalAlign="Left">
		        <asp:TableCell Width="140px" Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left">
		        Category</asp:TableCell>
		        <asp:TableCell  HorizontalAlign="Left">
		        <asp:DropDownList ID="ddlCategory"  Width="265px"  AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged"></asp:DropDownList>
		                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCategory" ValidationGroup="SaveValidation" InitialValue="0" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		          <asp:LinkButton ID="lbtnNewCategory" runat="server" Text="New" Font-Bold="true" Font-Size="Smaller"></asp:LinkButton>
                <asp:Panel ID="pnlNewCategory" CssClass="modalPopup"  Height="122px" Width="412px" runat="server">
		         <asp:Panel ID="Panel3" runat ="server" Width="410px" Height="120" BorderColor="black" BorderStyle="Solid" BorderWidth="1">
    <asp:Table ID="Table13" runat="server" Width ="50%">
    <asp:TableRow Width ="50%">
    <asp:TableCell Font-Size="Smaller" HorizontalAlign="Left">
    Enter Category Name
    </asp:TableCell>
    <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    
    </asp:Table>
    <hr />
    
    <asp:Table ID="Table14" runat="server">
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left">
    Category 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtNewCategory" runat="server" Width="300px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator11"  runat="server" ControlToValidate="txtNewCategory" ValidationGroup="grpTwo" ErrorMessage="*"></asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>
    </asp:Table>
    <hr />
    <asp:Table ID="Table15" runat="server">
    <asp:TableRow>
    <asp:TableCell>
    <asp:Button runat="server" ID = "btnSaveNewCategory" Text="Save" ValidationGroup="grpTwo" OnClick="SaveNewCategory" />
    <asp:Button runat="server" ID="btnCancelNewCategory" CausesValidation="false" Text="Cancel" />
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    </asp:Panel>   
		         
 </asp:Panel>
		        
   <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1"  TargetControlID="lbtnNewCategory" PopupControlID="pnlNewCategory" CancelControlID="btnCancelNewCategory" runat="server"></ajaxToolkit:ModalPopupExtender>
		        
		        
		        </asp:TableCell>
		        </asp:TableRow>	  
		    <asp:TableRow HorizontalAlign="Left">
		    
		        <asp:TableCell   Font-Size="Smaller" Font-Bold="true">
		                Sub Category
		        </asp:TableCell>
		        
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left" >
		       <asp:DropDownList ID="ddlSubCategory" Width="265px" runat="server" AutoPostBack="true" OnSelectedIndexChanged= "ddlSubCategory_OnSelectedIndexChanged"></asp:DropDownList>
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlSubCategory" ValidationGroup="SaveValidation" InitialValue="0" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		          <asp:LinkButton ID="lbtnNewSubCategory" runat="server" Text="New" ></asp:LinkButton>
                <asp:Panel ID="pnlNewSubCategory" CssClass="modalPopup"  Height="142px" Width="412px" runat="server">
		         <asp:Panel ID="Panel4" runat ="server" Width="410px" Height="140" BorderColor="black" BorderStyle="Solid" BorderWidth="1">
    <asp:Table ID="Table16" runat="server" Width ="50%">
    <asp:TableRow Width ="50%">
    <asp:TableCell HorizontalAlign="Left">
    Enter SubCategory details
    </asp:TableCell>
    <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    
    </asp:Table>
    <hr />
    
    <asp:Table ID="Table17" runat="server">
     <asp:TableRow>
     <asp:TableCell Width ="90px" HorizontalAlign="Left">
     Category 
    </asp:TableCell>
    <asp:TableCell>
    <asp:DropDownList runat="server" ID="ddlParentCategory" Width="305px"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator13"  runat="server" InitialValue="0" ControlToValidate="ddlParentCategory" ValidationGroup="grpThree" ErrorMessage="*"></asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>
    
    <asp:TableRow>
     <asp:TableCell Width ="90px" HorizontalAlign="Left">
    Sub Category 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtNewSubCategory" runat="server" Width="300px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator12"  runat="server" ControlToValidate="txtNewSubCategory" ValidationGroup="grpThree" ErrorMessage="*"></asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>
    </asp:Table>
    <hr />
    <asp:Table ID="Table18" runat="server">
    <asp:TableRow>
    <asp:TableCell>
    <asp:Button runat="server" ID = "btnSaveNewSubCategory" Text="Save" ValidationGroup="grpThree" OnClick="SaveNewSubCategory"/>
    <asp:Button runat="server" ID="btnCancelNewSubCategory" CausesValidation="false" Text="Cancel" />
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    </asp:Panel>   
		         
 </asp:Panel>
		        
   <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2"  TargetControlID="lbtnNewSubCategory" PopupControlID="pnlNewSubCategory" CancelControlID="btnCancelNewSubCategory" runat="server"></ajaxToolkit:ModalPopupExtender>
		        
		        </asp:TableCell>
		    </asp:TableRow>	   		    
		 </asp:Table>    
		 
		  <asp:Table ID="table5" runat="server" Width="70%">
		  <%--  test row 
		  <asp:TableRow>
		  <asp:TableCell  Width="140px" Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left">
		                Product Name
		        </asp:TableCell>		        
		         
		         <asp:TableCell Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left" >
		     
		        <asp:DropDownList ID = "ddlProductName1"   Width ="260px" runat="server"></asp:DropDownList>
		        
		  </asp:TableCell>
		  </asp:TableRow>
		  
		    --%>
		 <asp:TableRow>
		        <asp:TableCell  Width="140px" Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left">
		                Product Name
		        </asp:TableCell>
		        
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left" >
		       <%--<asp:TextBox ID="txtProductName" Width="260px" runat="server"></asp:TextBox>--%>
		        <asp:DropDownList ID = "ddlProductName"   Width ="265px" runat="server"></asp:DropDownList>
		     
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlProductName" ValidationGroup="SaveValidation" InitialValue="0" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		       
                <asp:LinkButton ID="lbtnNewProductName" runat="server" Text="New"></asp:LinkButton>
                <asp:Panel ID="pnlNewProductName" CssClass="modalPopup"  Height="122px" Width="412px" runat="server">
		         <asp:Panel ID="Panel1" runat ="server" Width="410px" Height="120" BorderColor="black" BorderStyle="Solid" BorderWidth="1">
    <asp:Table ID="Table3" runat="server" Width ="50%">
    <asp:TableRow Width ="50%">
    <asp:TableCell HorizontalAlign="Left">
    Enter product Details
    </asp:TableCell>
    <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    
    </asp:Table>
    <hr />
    
    <asp:Table ID="Table6" runat="server">
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left">
    Product Name 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtNewProductName" runat="server" Width="300px" ></asp:TextBox>
    <asp:RequiredFieldValidator  runat="server" ControlToValidate="txtNewProductName" ValidationGroup="grpOne" ErrorMessage="*"></asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>
    </asp:Table>
    <hr />
    <asp:Table ID="Table7" runat="server">
    <asp:TableRow>
    <asp:TableCell>
    <asp:Button runat="server" ID = "Button1" Text="Save" ValidationGroup="grpOne" OnClick="SaveNewProductName"/>
    <asp:Button runat="server" ID="btnCancelNewProductName" CausesValidation="false" Text="Cancel" />
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    </asp:Panel>   
		         
 </asp:Panel>
		        
   <ajaxToolkit:ModalPopupExtender  TargetControlID="lbtnNewProductName" PopupControlID="pnlNewProductName" CancelControlID="btnCancelNewProductName" runat="server"></ajaxToolkit:ModalPopupExtender>
		        </asp:TableCell>
		    </asp:TableRow>	   	
		      
		   <asp:TableRow>
		        <asp:TableCell  Font-Size="Smaller" Font-Bold="true">
		                Vendor
		        </asp:TableCell>
		        
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		       <asp:DropDownList ID="ddlVendor" Width="265px" runat="server"  AutoPostBack="true" OnSelectedIndexChanged ="ddVendor_TextChanged"></asp:DropDownList>
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlVendor" ValidationGroup="SaveValidation" InitialValue="0" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		          <asp:LinkButton ID="lbtnNewVendor" runat="server" Text="New" ></asp:LinkButton>&nbsp;&nbsp;
		          <asp:LinkButton ID="lbtnVendorDetails" runat="server" Text="Vendor Details"></asp:LinkButton>
                <asp:Panel ID="pnlNewVendor" CssClass="modalPopup"  Width="412px" Height="252px" runat="server">
		         <asp:Panel ID="Panel5" runat ="server" Width="410px" Height="250" BorderColor="black" BorderStyle="Solid" BorderWidth="1">
    <asp:Table ID="Table19" runat="server" Width ="50%">
    <asp:TableRow Width ="50%">
    <asp:TableCell HorizontalAlign="Left">
    Enter Vendor Details
    </asp:TableCell>
    <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    
    </asp:Table>
    <hr />
    
    <asp:Table ID="Table20" runat="server">
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left">
    Name 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtNewVendorName" runat="server" Width="300px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator14"  runat="server" ControlToValidate="txtNewVendorName" ValidationGroup="grpFour" ErrorMessage="*"></asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left" VerticalAlign="Top">
    Description 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtNewVendorDescription" runat="server" Width="300px" TextMode="MultiLine" Height="100px" ></asp:TextBox>
    </asp:TableCell>    
    </asp:TableRow>
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left">
    Contact Person 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtNewVendorContactPerson" runat="server" Width="300px" ></asp:TextBox>
    </asp:TableCell>    
    </asp:TableRow>
    </asp:Table>
    <hr />
    <asp:Table ID="Table21" runat="server">
    <asp:TableRow>
    <asp:TableCell HorizontalAlign="Center">
    <asp:Button runat="server" ID = "btnSaveNewVendor" Text="Save" ValidationGroup="grpFour" OnClick="SaveNewVendor"/>
    <asp:Button runat="server" ID="btnNewVendor" CausesValidation="false" Text="Cancel" />
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    </asp:Panel>   
		         
 </asp:Panel>
		        
   <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3"  TargetControlID="lbtnNewVendor" PopupControlID="pnlNewVendor" CancelControlID="btnNewVendor" runat="server"></ajaxToolkit:ModalPopupExtender>
   <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4"  TargetControlID="lbtnVendorDetails" PopupControlID="PnlVendorDetails" CancelControlID="btnNewVendor" runat="server"></ajaxToolkit:ModalPopupExtender>
		   
		   <!-- Vendor Details -->
		             <asp:Panel ID="PnlVendorDetails" CssClass="modalPopup"  Width="412px" Height="252px" runat="server">
		         <asp:Panel ID="Panel6" runat ="server" Width="410px" Height="250" BorderColor="black" BorderStyle="Solid" BorderWidth="1">
    <asp:Table ID="Table22" runat="server" Width ="50%">
    <asp:TableRow Width ="50%">
    <asp:TableCell HorizontalAlign="Left">
    Vendor Details
    </asp:TableCell>
    <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    
    </asp:Table>
    <hr />
    
    <asp:Table ID="Table23" runat="server">
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left">
    Name 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtEditVendorName" runat="server" Width="300px" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator15"  runat="server" ControlToValidate="txtEditVendorName" ValidationGroup="grpVEdit" ErrorMessage="*"></asp:RequiredFieldValidator>
    </asp:TableCell>    
    </asp:TableRow>
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left" VerticalAlign="Top">
    Description 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtEditVendorDescription" runat="server" Width="300px" TextMode="MultiLine" Height="100px" ></asp:TextBox>
    </asp:TableCell>    
    </asp:TableRow>
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left">
    Contact Person 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtEditVendorContactPerson" runat="server" Width="300px" ></asp:TextBox>
    </asp:TableCell>    
    </asp:TableRow>
    </asp:Table>
    <hr />
    <asp:Table ID="Table24" runat="server">
    <asp:TableRow>
    <asp:TableCell HorizontalAlign="Center">
    <asp:Button runat="server" ID = "Button2" Text="Save" ValidationGroup="grpVEdit" OnClick="ModifyVendorDetails"/>
    <asp:Button runat="server" ID="Button3" CausesValidation="false" Text="Cancel" />
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    </asp:Panel>   
		         
 </asp:Panel>
		   
		   
		   
		   
		    <!-- -->    
		        </asp:TableCell>
		    </asp:TableRow>	   			    	    
		 </asp:Table> 
		 
		 <asp:Table ID="table12" runat="server" Width="56%">	
		 <asp:TableRow>		    
		       <asp:TableCell  Width="140px" Font-Size="Smaller" Font-Bold="true">
		                SPR #
		        </asp:TableCell>		        
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left">
		        <asp:TextBox ID="txtSprNo" Width="260px" runat="server"></asp:TextBox>
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtSprNo" ValidationGroup="SaveValidation" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		        </asp:TableCell>		        
		    </asp:TableRow>	  			    
		    	<asp:TableRow>
		        <asp:TableCell Width="140px" Font-Size="Smaller" Font-Bold="true">
		        Delivery Date</asp:TableCell>
		        <asp:TableCell Width="40%">
		        <asp:TextBox ID="txtDeliveryDate"  Width="260px" runat="server"></asp:TextBox>
		        <asp:Image ID="imgDeliveryDt" runat="server"  ImageUrl="~/Images/date.gif"/>
		        <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="calDeliveryDt" runat="server" PopupButtonID="imgDeliveryDt" TargetControlID="txtDeliveryDate"></ajaxToolkit:CalendarExtender>
		        </asp:TableCell>
		        </asp:TableRow> 		          
		 </asp:Table>    
		 
		 <asp:Table ID="table11" runat="server" Width="58%">
		      <asp:TableRow>
		     
		        <asp:TableCell  Width="140px" Font-Size="Smaller" Font-Bold="true">
		                Received By
		        </asp:TableCell>
		        
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		        <asp:DropDownList ID="ddlReceivedBy" Width="265px" runat="server"></asp:DropDownList>
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlReceivedBy" ValidationGroup="SaveValidation" InitialValue="0" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
             	        
		       		       
		        </asp:TableCell>
		        </asp:TableRow>
		    
		      <asp:TableRow>
		        <asp:TableCell Width="140px" Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left" >
		        Vendor Serial #</asp:TableCell>
		        <asp:TableCell  HorizontalAlign="Left">
		        <asp:TextBox ID="txtSerialNo"  Width="260px" runat="server"></asp:TextBox>
		                    <asp:RequiredFieldValidator ID="vldserialno" ControlToValidate="txtSerialNo" ValidationGroup="SaveValidation"  ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		        </asp:TableCell>
		    </asp:TableRow>	  
		    	    
		 </asp:Table>
		         
		 <asp:Table ID="table_1" runat="server" Width="56%">
		 	  
		        <asp:TableRow>
		        <asp:TableCell Width="140px" Font-Size="Smaller" Font-Bold="true">
		        Warranty Expire on</asp:TableCell>
		        <asp:TableCell Width="40%" HorizontalAlign="Left">
		        <asp:TextBox ID="txtExpiryDate"  Width="260px" runat="server"></asp:TextBox>
		        <asp:Image  id="imgCal" runat="server" ImageUrl="~/Images/date.gif" />
		        <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" ID="calExpiryDate" TargetControlID="txtExpiryDate" BehaviorID="imgCal"  PopupButtonID="imgCal" runat="server"></ajaxToolkit:CalendarExtender>
		        </asp:TableCell>		    
		    </asp:TableRow>	
		      <asp:TableRow>
		        <asp:TableCell Width="140px" Font-Size="Smaller" Font-Bold="true"  >
		        Date of Tagging</asp:TableCell>
		        <asp:TableCell>
		        <asp:TextBox ID="txtTagDate" Width="260px" runat="server" ></asp:TextBox>		        
		        <asp:Image ID="imgTagdate" runat="server" ImageUrl="~/Images/date.gif" />
		        <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" ID="CalendarExtender1" runat="server" PopupButtonID="imgTagdate" TargetControlID="txtTagDate"></ajaxToolkit:CalendarExtender>
		        </asp:TableCell>
		     </asp:TableRow>		   	    
		 </asp:Table>	 
		 
		 <asp:Table ID="table2" runat="server" Width="65%">
		  <asp:TableRow>
		        <asp:TableCell  Font-Size="Smaller" Font-Bold="true" HorizontalAlign="Left">
		                MIS Asset #
		        </asp:TableCell>		        
		        <asp:TableCell Font-Size="Smaller" Font-Bold="true" >
		        <asp:TextBox ID="txtMisAssetNo" Width="260px" runat="server"></asp:TextBox>
		        <asp:RequiredFieldValidator ID="vldmisassetno" ControlToValidate="txtMisAssetNo" ValidationGroup="SaveValidation" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		        </asp:TableCell>
		    </asp:TableRow>
		            <asp:TableRow>
		            <asp:TableCell Width="140px" Font-Size="Smaller" Font-Bold="true"  >
		            Approved (Y/N)</asp:TableCell>
		            <asp:TableCell>
		            <asp:RadioButtonList ID="rbtnApproved" runat="server" RepeatDirection="Horizontal" Width="100px">     
		            <asp:ListItem Text="Yes" Value="1"></asp:ListItem><asp:ListItem Text="No" Value="0" Selected="True">
		             </asp:ListItem>
		            </asp:RadioButtonList>	
		            </asp:TableCell> 
		            </asp:TableRow>
		        </asp:Table>
		        <asp:Panel ID="pnlAssetAllocation" runat="server" BorderColor="green" BorderStyle="Solid" BorderWidth="1" Width="65%" Enabled="false" Visible="false">
		         <h2 class="section"> Asset Allocation</h2>
		         <asp:Table ID="table4" runat="server" Width="100%">
		           <asp:TableRow>
		            <asp:TableCell  Width="140px" Font-Size="Smaller" Font-Bold="true">
		                    Allocated To
		            </asp:TableCell>    		        
		            <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		            <asp:DropDownList ID="ddlAllocateTo" Width="350px"  AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAllocateTo_OnSelectedIndexChanged">
		            <asp:ListItem Value="0" Text="-- Select Allocate To --"></asp:ListItem>
		            <asp:ListItem Value="1" Text="User"></asp:ListItem>
		            <asp:ListItem Value="2" Text="Department"></asp:ListItem>
		            </asp:DropDownList>
		            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="ddlAllocateTo"  InitialValue="0" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		              </asp:TableCell>		            
		            </asp:TableRow>	
		            <asp:TableRow>
		            <asp:TableCell  Width="140px" Font-Size="Smaller" Font-Bold="true">
		                    Owner 
		            </asp:TableCell>    		        
		            <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		            <asp:DropDownList ID="ddlOwnerName"  AutoPostBack="true" Width="350px" runat="server" OnSelectedIndexChanged="ddlOwnerName_OnSelectedIndexChanged">
		            <asp:ListItem Value="0" Text="-- Select Owner Name --"></asp:ListItem>
		             </asp:DropDownList>
		            <asp:RequiredFieldValidator ID="vldOwnerName" ControlToValidate="ddlOwnerName"  InitialValue="0" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		              </asp:TableCell>		            
		            </asp:TableRow>
		            	 
		            	 <asp:TableRow>
		            <asp:TableCell  Width="140px" Font-Size="Smaller" Font-Bold="true">
		                 <asp:Label runat="server" ID="lblsecretary" Text ="Dept. Secretary" Enabled="false"  Visible="false"></asp:Label>  
		            </asp:TableCell>    		        
		            <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		            <asp:DropDownList ID="ddlDeptSecretary" Width="350px" runat="server" Enabled="false" Visible="false">
		             </asp:DropDownList>
		             <%--<asp:LinkButton ID="lbtnNewSecretary" runat="server" Text="New" CausesValidation="false" Enabled="false" Visible="false" OnClick="AddNewSecretary"></asp:LinkButton>--%>
		            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="ddlDeptSecretary"  InitialValue="0" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		              </asp:TableCell>		            
		            </asp:TableRow>
		            
		            <asp:TableRow>		            
		            <asp:TableCell  Width="140px" Font-Size="Smaller" Font-Bold="true">
		                    Building
		            </asp:TableCell>    		        
		            <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		            <asp:TextBox ID="txtBuilding" Width="345px" runat="server"></asp:TextBox>
		            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtBuilding" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		              </asp:TableCell>		            
		            </asp:TableRow>		
		            <asp:TableRow>
		            <asp:TableCell  Width="140px" Font-Size="Smaller" Font-Bold="true">
		                    Room #
		            </asp:TableCell>    		        
		            <asp:TableCell Font-Size="Smaller" Font-Bold="true">
		            <asp:TextBox ID="txtRoom" Width="345px" runat="server"></asp:TextBox>
		            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtRoom"  InitialValue="0" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
		              </asp:TableCell>		            
		            </asp:TableRow>	
		            <asp:TableRow>
		        <asp:TableCell Width="140px" Font-Size="Smaller" Font-Bold="true"  >
		        Date of Allocation</asp:TableCell>
		        <asp:TableCell>
		        <asp:TextBox ID="txtAllocationDate" Width="330px" runat="server" ></asp:TextBox>		        
		        <asp:Image ID="imgAllocationDt" runat="server" ImageUrl="~/Images/date.gif" />
		        <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy"  ID="calAllocationDt" runat="server" PopupButtonID="imgAllocationDt" TargetControlID="txtAllocationDate"></ajaxToolkit:CalendarExtender>
		        </asp:TableCell>
		     </asp:TableRow>
		            		            	        
		            </asp:Table>		        		        
		        </asp:Panel>
		        <hr>
		<center>
         <asp:Button ID="btnAllocate" runat="server" Text="Asset Allocate"  Enabled="false" Visible="false" OnClick="AssetAllocation"/>
         <asp:Button id="btnSave"  runat="server" Text="Save" ValidationGroup="SaveValidation" OnClick="SaveProduct"  />
         <asp:Button ID="btnBack" CausesValidation="false" runat="server" Text="Back" OnClick="BackToProductList"  />
       </center>   
		 </ContentTemplate>
		</ajaxToolkit:TabPanel>						
		</ajaxToolkit:TabContainer>		
		</div>		
		</div>
		
</asp:Content>




<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmrComplainCheck.aspx.cs" Inherits="frmNewUserAccount" Title="HelpDesk|User Complains" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">  
<script  type="text/javascript">
</script>

<div   id="body"  style="width:1000px">
		<div id="col_main_left">
			
			
		</div>
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' id="col_main_right">	
		<h2 class="section">
		       <asp:Label  ID = "lblHeading" Text="User Complains" runat="server" ></asp:Label>		       
            
		         </h2>
		          <div id="Div1"  runat="server"    class="content_right"  >
		          <asp:Panel ID="pnlNewVendor" CssClass="modalPopup"   BorderStyle="Double" runat="server" Width="777px">    
		          <div align=center>
		          <asp:Label ID="lblUserComplains" runat=server Text="User Complains Grid" ForeColor=red Font-Size=Large></asp:Label>
		          <br />
		          </div>
		   <asp:DataGrid id="userComplainsGrid" runat="server" Width="100%" BackColor="White" ForeColor="Black" HorizontalAlign="Center" GridLines="Vertical" CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" AutoGenerateColumns="False" AllowPaging=true PageSize=10 OnPageIndexChanged="mGetNextPage">
           <FooterStyle BackColor="#CCCC99" />
           <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
           <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
           <AlternatingItemStyle BackColor="White" />
           <ItemStyle BackColor="#F7F7DE" />
           <Columns>
               <asp:BoundColumn HeaderText="Id" DataField="no"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Category" DataField="category"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="User Name" DataField="userName"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Subject" DataField="subject"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Descriptoin" DataField="description"></asp:BoundColumn>
               <asp:BoundColumn HeaderText="Date/Time" DataField="date"></asp:BoundColumn>
           </Columns>
           <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
       </asp:DataGrid> 
		         </asp:Panel>
		</div>
      
		</div>	
		    
            
		</div>	
		
</asp:Content>



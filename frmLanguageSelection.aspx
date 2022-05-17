<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmLanguageSelection.aspx.cs" Inherits="Default2" Title="Language Setting Page" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
				<div id="body"  style="width:1000px">
				<div id="Div1">
			<div id="Div2" style="background-image:url(./Images/back1.jpg)">
				<a id="A1"></a>
				<h3 id="H3_1"  style="background-image:url(./Images/back1.jpg)">
					<asp:Label runat="server" ID="lblhelplink" Text="Help and Other Links" meta:resourcekey="lblhelplinkResource1" Font-Size="Small" ForeColor="Black"></asp:Label>					
					
					
					</h3>
					
					</div>					
		</div>
		<div id="col_main_left">
			<div id="user_assistance">
				<a id="content_start"></a>
				
							
			</div>
			<div>						&nbsp;</div>
			
		</div>
		
		<div   id="col_main_right" style="background-image:url(./Images/back2.jpg)" >
		  <br />
		    <br />
		      
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<asp:Label runat="server" ID="lblMessage" >Please Choose The Language</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList  ID="ddSelLanguage" runat="server" AutoPostBack="false" Width="148px">
            <asp:ListItem Value="en-US">english</asp:ListItem>
            <asp:ListItem Value="ar-EG">«·⁄—»Ì…</asp:ListItem>
        </asp:DropDownList>&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server">«·—Ã«¡ «Œ Ì«— ·€… «·≈” Œœ«„</asp:Label><br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button  runat="server" ID="btnLanguage" OnClick="setCmbLanguage" Text="Submit" />
        <br />
		        <br />
		          <br />
		          
		      <br />
        </div>
        
        
        </div>
</asp:Content>


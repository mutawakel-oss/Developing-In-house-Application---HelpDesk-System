<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Default2" Title="Help | Error" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
				<div id="body"  style="width:1000px">
		<div id="col_main_left">
			<div id="user_assistance">
				<a id="content_start"></a>
				<asp:Label runat="server" ID="errorMessage" Font-Bold="True" ForeColor="Black" meta:resourcekey="errorMessageResource1" EnableViewState="False" ></asp:Label>
							
			</div>
			<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>						
		<asp:Table Width="100%" ID="Table8"  BackColor="#EBEBEB" runat="server" meta:resourcekey="Table8Resource1">
		
		
		
		</asp:Table>
		</div>
			
		</div>
		<div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' id="col_main_right">
		<asp:Label runat="server" ID="lblMessage" meta:resourcekey="lblMessageResource1" Font-Italic="True" Font-Size="Small"></asp:Label>
		&nbsp;&nbsp;&nbsp;&nbsp;
		<asp:LinkButton id="lbtnLogin" runat="server" Font-Size="Small" Text="Click here to login Again" PostBackUrl="~/Login.aspx" meta:resourcekey="lblMessageResource2" Enabled="false" Visible="false"></asp:LinkButton>
		</div>
        </div>
</asp:Content>


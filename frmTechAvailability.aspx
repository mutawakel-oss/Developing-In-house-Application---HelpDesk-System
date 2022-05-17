<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmTechAvailability.aspx.cs" Inherits="_Default" Title="PC Technician Status Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SecondBar" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
 <div id="body" style="width:1000px" >
<div id="col_main_left">
		
			<div>
						
		<asp:Table Width="100%" ID="Table1"  BackColor="#EBEBEB" runat="server">
		<asp:TableRow>
		<asp:TableCell>
		<asp:HyperLink ID="HyperLink3" runat="server" Text="Home" NavigateUrl="~/frmAdminDefault.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>		
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>
		
		<asp:HyperLink ID="HyperLink1" runat="server" Text="User Management" NavigateUrl="~/frmLdapUsers.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
		<asp:TableCell>
        <asp:LinkButton ID="hlkNewRequest" runat="server" Text="New Request" OnClick="NewRequest"  Font-Size="smaller" Font-Bold="false"  ForeColor="black" Font-Underline="false" ></asp:LinkButton>
		</asp:TableCell>
		</asp:TableRow>
		
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="hlkUserRequestList" runat="server" Text="Own Requests" NavigateUrl="~/frmUserRequestList.aspx" Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
	
		<asp:TableRow>
		<asp:TableCell>		
		<asp:HyperLink ID="HyperLink2" runat="server" Text="Report" NavigateUrl="~/frmRequestHandledReport.aspx"     Font-Size="Smaller" Font-Bold="false" ForeColor="black" Font-Underline="false"></asp:HyperLink>
		</asp:TableCell>
		</asp:TableRow>		
		
		</asp:Table>
		
		</div>		
		<%--<div style=" background-color:#EBEBEB">
		<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/status.GIF" />
		
		</div>--%>
		
		</div>		
		<div id="col_main_right">
		<div style="border-color:blue; background-color:#EBEBEB">
		<div align=center>
		<asp:Label ID="lblTechStatus" runat=server Text="PC Technicians Status Control Panel" ForeColor=blue Font-Size=Medium Font-Bold=true ></asp:Label>
		</div>
		<br />
    <asp:DataGrid ID="technicianGrid" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" HorizontalAlign="Center"
         OnEditCommand="technicianGrid_EditCommand">
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" Mode="NumericPages" />
        <ItemStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <Columns>
            <asp:TemplateColumn HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelection" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>

            <asp:EditCommandColumn CancelText="Cancel" EditText="Change" HeaderText="Follow"
                UpdateText="Update"></asp:EditCommandColumn>
        </Columns>
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    </asp:DataGrid>
    <asp:Table ID="tblBuildingEnrollment" runat="server" HorizontalAlign="center">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblBuildingName" runat="server" Text="Building Name:"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddlBuilding" runat="server">
                    <asp:ListItem Selected="True">--Select Your Building--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator id="validBuildingName"  runat="server" ErrorMessage="Please select a building"  ControlToValidate="ddlBuilding"  InitialValue="--Select Your Building--" ValidationGroup="BuildingGroup"> </asp:RequiredFieldValidator>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnEnroll" runat="server" Text="Enroll" OnClick="mEnroll" CausesValidation="true" ValidationGroup="BuildingGroup" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
    </div>
    </div>
 </div>
</asp:Content>


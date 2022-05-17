<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmRequestHandledReport.aspx.cs" Inherits="frmRequestHandledReport" Title="Reporting Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
    
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
<div id="body" style="width:1000px" >
<%--<div id="col_main_left">
			<div id="user_assistance">
				<a id="content_start"></a>
				<h3>
					Help and Other Links</h3>
			</div>	
			<div>
						
		<asp:Table Width="100%" ID="Table1"  BackColor="#EBEBEB" runat="server">
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
		</div>	--%>
		<%--<div id="col_main_right">--%>
		<div style="border-color:blue; background-color:#EBEBEB">
		<asp:Table ID="Table5" runat="server">
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
<div>
    <br />
    
    <br />
    <div align=center>
    <asp:Table   ID="Table2" runat="server" Width="994px">
    <asp:TableRow runat="server">
    <asp:TableCell runat="server" ColumnSpan=1>
    <asp:Label ID="lblPerioud" runat=server Text="Perioud:"></asp:Label>
    </asp:TableCell>
    <asp:TableCell>
    <asp:DropDownList ID="ddlPerioud" runat=server AutoPostBack=True OnSelectedIndexChanged="mDeterminePerioud">
    <asp:ListItem Text="--Select--" Value="none"></asp:ListItem>
    <asp:ListItem Text="Today" Value="today"></asp:ListItem>
    <asp:ListItem Text="Current Week" Value="week"></asp:ListItem>
    <asp:ListItem Text="Current Month" Value="month"></asp:ListItem>
    </asp:DropDownList>
    </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
    <asp:TableCell runat="server">
    <asp:Label ID="lblDate" runat=server Text="Date   :"></asp:Label>
    </asp:TableCell>
    <asp:TableCell runat="server">
    <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="From"></asp:Label>
    </asp:TableCell>
    <asp:TableCell runat="server">
    <asp:TextBox id= "txtDeliveryDate"  Width="100px" runat="server"></asp:TextBox>
	<asp:Image ID="imgDeliveryDt" runat="server"  ImageUrl="~/Images/date.gif"/>
	<ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="calDeliveryDt" runat="server" PopupButtonID="imgDeliveryDt" TargetControlID="txtDeliveryDate" BehaviorID="calDeliveryDt" Enabled="True"></ajaxToolkit:CalendarExtender></asp:TableCell>
    <asp:TableCell runat="server">
    
    &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text="To" Font-Size="Small"></asp:Label>
    </asp:TableCell>
    <asp:TableCell runat="server">
    &nbsp;<asp:TextBox ID="txtToDate"  Width="100px" runat="server"></asp:TextBox>
    <asp:Image ID="imgDeliveryDt2" runat="server"  ImageUrl="~/Images/date.gif"/>
     <ajaxToolkit:CalendarExtender Format="dd/MMM/yyyy" id="CalendarExtender1"  runat="server" PopupButtonID="imgDeliveryDt2" TargetControlID="txtToDate" BehaviorID="CalendarExtender1" Enabled="True" ></ajaxToolkit:CalendarExtender>  
    </asp:TableCell>
    <asp:TableCell Width="500px" HorizontalAlign="Right" Font-Size="Small" runat="server"><asp:LinkButton runat="server" Text="_Close_ " PostBackUrl="~/frmAdminRequestList.aspx" BorderWidth="1px" BorderStyle="Solid" Font-Underline="True" ForeColor="Navy"></asp:LinkButton>
</asp:TableCell> 
   
    </asp:TableRow>
    </asp:Table>
    
    </div>
    
    <ajaxToolkit:Accordion
    ID="MyAccordion"
    runat="Server"
    SelectedIndex="0"
    HeaderCssClass="accordionHeader"
    HeaderSelectedCssClass="accordionHeaderSelected"
    ContentCssClass="accordionContent"
    AutoSize="None"
    FadeTransitions="true"
    TransitionDuration="250"
    FramesPerSecond="40"
    RequireOpenedPane="false"
    SuppressHeaderPostbacks="true" >
    <Panes>
        <ajaxToolkit:AccordionPane
            HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Technician History Report -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</Header>
            <Content>
            
    <asp:Table id="tblOptions" runat=server Width="21px">
    <asp:TableRow ID="TableRow2" runat="server">
    <asp:TableCell>
    <asp:Label ID="lblTechName" runat=server Text="Technician Name:" Width="140px"></asp:Label>
    </asp:TableCell>
    <asp:TableCell ID="TableCell6" runat="server">
    <asp:DropDownList ID="ddlTechnicianName"  runat="server" ></asp:DropDownList>
    </asp:TableCell>
    <asp:TableCell>
    <asp:Label ID="lblCategory" runat=server Text="Category:" Width="80px"></asp:Label>
    </asp:TableCell>
    <asp:TableCell>
    <asp:DropDownList ID="ddlCategory"  runat="server"   >
    </asp:DropDownList>
    </asp:TableCell>
    <asp:TableCell>
    <asp:Button ID="btnDisplayTech" runat=server OnClick="mDisplayTechReport" Text="Display Technician History Report" />
    </asp:TableCell> 
    </asp:TableRow>
    <asp:TableRow>
    <asp:TableCell ColumnSpan=4>
    <asp:Label ID="unloadMessage" ForeColor=red runat=server Visible=false Width="405px"></asp:Label>
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    
    <br />
    
    
            </Content>
        </ajaxToolkit:AccordionPane>  
        <ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Technician Evaluation Report -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</Header>
            <Content>
<asp:Table ID="tblTechEvaluation" runat=server >
    <asp:TableRow>
    <asp:TableCell>
    <asp:Label ID="lblCreteria" runat=server Text="Evaluation Creteria :"></asp:Label>
    </asp:TableCell>
    <asp:TableCell>
    <asp:DropDownList ID="ddlEvaluationCreteria" runat=server OnSelectedIndexChanged="m_evaluate_technicians" ></asp:DropDownList>
    </asp:TableCell>
    <asp:TableCell>
    <asp:Label ID="lblCategoryStatus" runat=server Text="Status" ></asp:Label>
    </asp:TableCell>
    <asp:TableCell>
    <asp:DropDownList ID="ddlCategoryStatus" runat=server ></asp:DropDownList>
    </asp:TableCell>
    <asp:TableCell>
    <asp:Button ID="btnDisplayTechEvaluation" runat=server Text="Display Technicians Evaluation Report" OnClick="m_evaluate_technicians" />
    </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
    <asp:TableCell ColumnSpan=4>
    <asp:Label ID="lblTechEval" runat=server Visible=false ForeColor=red></asp:Label>
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
            </Content>
            </ajaxToolkit:AccordionPane>      
        <ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header> Service Evaluation Report -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</Header>
            <Content>
            <asp:LinkButton ID="lnkFullRepport" runat=server   Font-Underline=true Text="Display The Full Request Report" OnClick="LoadServiceReport"></asp:LinkButton>
            <br />
            <asp:Label ID="lblServiceReportError" ForeColor=red runat=server Visible=false Width="405px"></asp:Label>
            </Content>
            </ajaxToolkit:AccordionPane>      
          <ajaxToolkit:AccordionPane HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <Header>User Requests Evaluation Report  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</Header>
            <Content>
<asp:Table ID="Table1" runat=server >
  <asp:TableRow>
  <asp:TableCell>
  <asp:LinkButton ID="lnkUserReqReport" runat=server Text="Display User Requests Report"  OnClick="mUsersReqReportLinkClicked"></asp:LinkButton>
  </asp:TableCell>
  </asp:TableRow>
  <asp:TableRow>
  <asp:TableCell>
  <asp:Label ID="lblUserRequestReprotError" ForeColor=red runat=server Visible=false Width="405px"></asp:Label>
  </asp:TableCell>
  </asp:TableRow>
</asp:Table>
            </Content>
            </ajaxToolkit:AccordionPane>      
        
    </Panes>            
    <HeaderTemplate>...</HeaderTemplate>
    <ContentTemplate>...</ContentTemplate>
</ajaxToolkit:Accordion>

    <br />
</div>

    
    <br />
    
    
    
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />

	</div>
</asp:Content>


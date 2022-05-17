<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" CodeFile="Login.aspx.cs"
    Inherits="Login_aspx" Title="Login" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="Main" runat="server">
    <div id="body" style="width: 1000px">
        <div id="col_main_left">
            <div id="user_assistance" style="background-image: url(./Images/Login.jpg)">
                
                <h3 id="H3_1" runat="server" dir='<%$ Resources:MulResource, TextDirection %>'>
                    <asp:Label runat="server" ID="lblhelplink" Text="Help and Other Links" ForeColor="white" meta:resourcekey="lblhelplinkResource1"></asp:Label>
                </h3>
            </div>
        </div>
        <div id="col_main_right" style="background-color: #D7D6C4">
           
            <div runat="server" dir='<%$ Resources:MulResource, TextDirection %>' class="content_right"
                style="width: 788px; height: 250px">
                <asp:Panel ID="AccessNoticePanel" runat="server" EnableViewState="False" Visible="False">
                </asp:Panel>
                <%-- The following Area will be used to place the announcement --%>
                <marquee id="newsFlash" scrolldelay="0" scrollamount="4">
                    <div style="width: 0px" onmouseover="newsFlash.stop();" onmouseout="newsFlash.start();">
                        <nobr>
                            <font color="red"><b>NEW:</b></font><font color="blue"> HelpDesk System started providing
                                the support services for the users of the new COM and CON female colleges building
                                in Riyadh.</font></nobr></div>
                </marquee>
                <hr />
                <div >
                    <asp:Table runat="server" BorderColor="Blue" BorderWidth="0px" BackImageUrl="~/Images/back245.jpg"
                        Width="100%"  >
                       
                        <asp:TableRow  runat="server">
                            <asp:TableCell  runat="server">
                                <div class="LoginPanel" style="background-image:url(images/loginTableBackground.png); background-repeat:no-repeat">
                                <asp:Table ID="Table1" runat="server"        >
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <br /><br /><br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow >
                                        <asp:TableCell>
                                            &nbsp;
                                            <asp:Label ID="lblDomainName" runat="server" Text="Domain"></asp:Label>
                                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                                            <asp:DropDownList ID="ddlDomain" runat="server">
                                                <asp:ListItem Text="Med" Value="1" Selected="true"></asp:ListItem>
                                                <asp:ListItem Text="Ksuhs" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Riyadh" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" >
                                        <asp:TableCell runat="server">
                                            <asp:Login ID="LoginConrol" runat="server" TitleText="" UserNameLabelText="User Name"
                                                PasswordLabelText="Password" CssClass="login_box" OnAuthenticate="LoginConrol_Authenticate"
                                                RememberMeText="Login as Local Admin" meta:resourcekey="LoginConrolResource1">
                                                <TextBoxStyle CssClass="text"></TextBoxStyle>
                                            </asp:Login>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                </div>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
               
                <asp:Table ID="tblLinks" runat="server">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="4">
                            <asp:Label runat="server" ID="lblText1" Text="Help Desk system helps to provide service support for all users of College of Medicine, with help of their userid and password.  The Support will be provided by support team based on Request ID and priority.  Use Help Desk for all kinds of service requests other means of support requests are not encouraged and less priority will be given."
                                meta:resourcekey="lblText1Resource1"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButton2" runat="server" Text="New Account" OnClick="CreateNew"
                                meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                        <br />
                            <asp:LinkButton ID="lnkPasswordForgote" runat="server" Text="Forgot Your Password?"
                                OnClick="mForgotePassword" ForeColor="blue"></asp:LinkButton>
                       &nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkHelpDeskPolicy" runat="server" Text="Help Desk Policy" OnClick="mLinkPolicyClicked"
                                ForeColor="blue" visible="false"></asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
               
                <asp:Table runat="server" ID="tblone" Visible="false">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server">
                            <asp:Table ID="Table2" runat="server" BorderColor="Blue" BorderWidth="0px" Width="100%"
                                BackImageUrl="~/Images/back3.jpg">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server">
                                        <asp:Label runat="server" ID="Label1" Text=" A service request can be send without having a user account of College of Medicine, by clicking the following link, but less priority will be given."
                                            meta:resourcekey="Label1Resource1"></asp:Label>
                                        <asp:LinkButton runat="server" ID="lbtnContactUs" Text="" meta:resourcekey="lbtnContactUsResource1"></asp:LinkButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
					
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                            <asp:Table ID="Table3" runat="server" BorderColor="Blue" Width="100%" BorderWidth="0px"
                                BackImageUrl="~/Images/back4.jpg">
                                <asp:TableRow runat="server">
                                </asp:TableRow>
                            </asp:Table>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Panel ID="pnlNewVendor" CssClass="modalPopup" BackColor="WhiteSmoke" Width="512px"
                                Height="512px" runat="server">
                                <asp:Table ID="Table19" runat="server" Width="50%">
                                    <asp:TableRow Width="50%" runat="server">
                                        <asp:TableCell HorizontalAlign="Left" Font-Bold="True" Font-Italic="True" runat="server">
                                            <asp:Label runat="server" ID="Label8" Text="Quick Service Request" meta:resourcekey="Label8Resource1"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                <hr />
                                <asp:Table ID="Table20" runat="server">
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="90px" Font-Bold="True" Font-Size="Smaller" HorizontalAlign="Left"
                                            runat="server">
                                            <asp:Label runat="server" ID="lblName" Text="Name" meta:resourcekey="lblNameResource1"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server">
                                            <asp:TextBox ID="txtName" runat="server" Width="400px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtName"
                                                ValidationGroup="grpFour" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator14Resource1"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="90px" Font-Bold="True" Font-Size="Smaller" HorizontalAlign="Left"
                                            runat="server">
                                            <asp:Label runat="server" ID="Label2" Text="Badge No" meta:resourcekey="Label2Resource1"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server">
                                            <asp:TextBox ID="txtBadgeNo" runat="server" Width="400px" meta:resourcekey="txtBadgeNoResource1"></asp:TextBox>
                                            <asp:RegularExpressionValidator runat="server" ID="regVldBadgeNo" ControlToValidate="txtBadgeNo"
                                                ValidationExpression="^[0-9]*" ValidationGroup="grpFour" ErrorMessage="*" meta:resourcekey="regVldBadgeNoResource1"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBadgeNo"
                                                ValidationGroup="grpFour" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="90px" Font-Bold="True" Font-Size="Smaller" HorizontalAlign="Left"
                                            runat="server">
                                            <asp:Label runat="server" ID="Label3" Text="Phone / Pager " meta:resourcekey="Label3Resource1"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server">
                                            <asp:TextBox ID="txtPhone" runat="server" Width="400px" meta:resourcekey="txtPhoneResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone"
                                                ValidationGroup="grpFour" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="90px" Font-Bold="True" Font-Size="Smaller" HorizontalAlign="Left"
                                            runat="server">
                                            <asp:Label runat="server" ID="Label4" Text="Department" meta:resourcekey="Label4Resource1"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server">
                                            <asp:TextBox ID="txtDepartment" runat="server" Width="400px" meta:resourcekey="txtDepartmentResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDepartment"
                                                ValidationGroup="grpFour" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="90px" Font-Bold="True" Font-Size="Smaller" HorizontalAlign="Left"
                                            runat="server">
                                            <asp:Label runat="server" ID="Label5" Text="Subject" meta:resourcekey="Label5Resource1"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server">
                                            <asp:TextBox ID="txtSubject" runat="server" Width="400px" meta:resourcekey="txtSubjectResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject"
                                                ValidationGroup="grpFour" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="90px" Font-Bold="True" Font-Size="Smaller" HorizontalAlign="Left"
                                            runat="server">
                                            <asp:Label runat="server" ID="Label6" Text="Email" meta:resourcekey="Label6Resource1"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server">
                                            <asp:TextBox ID="txtEmail" runat="server" Width="400px" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                                            <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail"
                                                ValidationExpression=".*@.*\..*" ErrorMessage="*" ValidationGroup="grpFour" meta:resourcekey="revEmailResource1"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
                                                ValidationGroup="grpFour" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell Width="90px" Font-Bold="True" Font-Size="Smaller" HorizontalAlign="Left"
                                            VerticalAlign="Top" runat="server">
                                            <asp:Label runat="server" ID="Label7" Text="Request Description" meta:resourcekey="Label7Resource1"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server">
                                            <asp:TextBox ID="txtMessage" runat="server" Width="400px" TextMode="MultiLine" Height="250px"
                                                meta:resourcekey="txtMessageResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMessage"
                                                ValidationGroup="grpFour" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                <hr />
                                <asp:Table ID="Table21" runat="server">
                                    <asp:TableRow runat="server">
                                        <asp:TableCell HorizontalAlign="Center" runat="server">
                                            <asp:Button runat="server" ID="btnSaveNewVendor" Text="Send" ValidationGroup="grpFour"
                                                OnClick="SendRequest" meta:resourcekey="btnSaveNewVendorResource1" />
                                            <asp:Button runat="server" ID="btnNewVendor" CausesValidation="False" Text="Cancel"
                                                meta:resourcekey="btnNewVendorResource1" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:Panel>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" TargetControlID="lbtnContactUs"
                                PopupControlID="pnlNewVendor" CancelControlID="btnNewVendor" runat="server" BehaviorID="ModalPopupExtender3"
                                DynamicServicePath="" Enabled="True">
                            </ajaxToolkit:ModalPopupExtender>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                
                <asp:Label ID="languageLabel" runat="server" Text="en-US" Visible="False"></asp:Label>
                <asp:Label ID="startFlag" runat="server" Text="first" Visible="False"></asp:Label><%--<asp:DropDownList ID ="ddlLogin" runat="server">
				<asp:ListItem> Med</asp:ListItem>
				<asp:ListItem>Local Administrator</asp:ListItem>
				</asp:DropDownList>--%><%--<p>
					<asp:HyperLink ID="RegisterLink" runat="server" NavigateUrl="~/Register.aspx">Create an Account</asp:HyperLink>
				</p>
				<p>
					<asp:LinkButton ID="ForgotPasswordButton" runat="server" OnClick="ForgotPasswordButton_Click">Forgot Password?</asp:LinkButton>
				</p>
				<asp:PasswordRecovery ID="PasswordRecovery" runat="server" Visible="False" UserNameTitleText=""
					QuestionTitleText="Step 2: Identity Confirmation." UserNameInstructionText="Step 1: Enter your User Name."
					Width="280px" OnInit="PasswordRecovery_Init" OnSendMailError="PasswordRecovery_SendMailError">
					<TitleTextStyle Font-Bold="True"></TitleTextStyle>
					<InstructionTextStyle Font-Bold="True"></InstructionTextStyle>
					<LabelStyle Wrap="False" />
				</asp:PasswordRecovery>--%>
            </div>
            <div align="center" >
                <br />
                <br /><br /><br /><br />
                <font color="green" size="2px">ITS Developers &copy; 2012 College of Medicine</font></div>
        </div>
    </div>
</asp:Content>

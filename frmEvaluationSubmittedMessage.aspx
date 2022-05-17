<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="frmEvaluationSubmittedMessage.aspx.cs" Inherits="frmEvaluationSubmittedMessage" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Suceessful Submission</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

<asp:Panel ID="Panel1" runat="server" Height="372px" Width="435px" BorderStyle="Ridge">
    <asp:Image ID="Image1" runat="server" Height="69px" ImageUrl="~/Images/HeaderBanner.gif"
        Width="440px" /><br />
    <br />
<asp:Label ID="lblSuccessfultSubmit" runat=server Text="Your evaluation was submitted successfully. Thanks." Font-Bold="True" Height="41px" Width="438px" ></asp:Label>
  <ajaxToolkit:AnimationExtender id="MyExtender"
  runat="server" TargetControlID="Panel1">
  <Animations>
    <OnLoad >
      <FadeIn Duration="3" Fps="20" />
    </OnLoad >
  </Animations>
</ajaxToolkit:AnimationExtender>
    </asp:Panel>
</form>
</body>
</html>

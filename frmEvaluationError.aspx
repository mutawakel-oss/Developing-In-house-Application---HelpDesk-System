<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEvaluationError.aspx.cs" Inherits="frmServiceEvaluation" %>
<%@ register Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Evaluation/Rating page</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="372px" Width="435px" BorderStyle="Ridge">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Image ID="Image1" runat="server" Height="69px" ImageUrl="~/Images/HeaderBanner.gif"
            Width="440px" /><div>
     <asp:Label ID="lblEvaluationDetails" runat="server" BackColor="#E0E0E0" Font-Size="Medium" meta:resourcekey="Label5Resource1"
            Text="Evaluation / Rating Page" Width="205px" ForeColor=Blue style="left: 109px; position: absolute; text-align: center" Font-Bold="True"></asp:Label>
                <br />
                <br />
        <br />
            <asp:Label ID="lblCurrentEvaluation" runat=server Height="53px" >Sorry you can not evaluate the service again. You made that in the past.</asp:Label></div>
        <ajaxToolkit:AnimationExtender id="MyExtender"
  runat="server" TargetControlID="Panel1">
  <Animations>
    <OnLoad >
      <FadeIn Duration="0.5" Fps="20" />
    </OnLoad >
  </Animations>
</ajaxToolkit:AnimationExtender>
        </asp:Panel>
    </form>
</body>
</html>

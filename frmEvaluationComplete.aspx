<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="frmEvaluationComplete.aspx.cs" Inherits="frmEvaluationComplete2" %>



<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
<div  id="body" style="width:1000px">
<asp:Label ID="lblCompleteEvaluation" runat=server Text="Thanks.Your evaluation has been sent."></asp:Label>
<br />
<br />
<asp:Button ID="btnBack"  runat=server OnClick="mGoBack" Text="Go to request list" />
<br />
<br />
<br />
<br />

</div>
</asp:Content>


<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Bug.ascx.vb" Inherits="SkinTweaker.Bug" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/labelcontrol.ascx" %>
<li class="Row">
    <ul>
        <li class="Details Action">
            <asp:LinkButton ID="lbKillOrRestore" runat="server"></asp:LinkButton></li>
        <li class="Details ID">
            <dnn:label ID="lblID" runat="server" /></li>
        <li class="Details Name">
            <asp:Label ID="lblName" runat="server" /></li>
        <li class="Details Status">
            <asp:Label ID="lblStatus" runat="server" /></li>
        <li class="Details Notes">
            <asp:Label ID="lblNotes" runat="server" /></li>
    </ul>
</li>

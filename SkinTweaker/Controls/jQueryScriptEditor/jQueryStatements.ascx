<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="jQueryStatements.ascx.vb"
    Inherits="SkinTweaker.jQueryStatements" %>
<%@ Register TagPrefix="uc1" Src="jQueryStatement.ascx" TagName="jQueryStatement" %>
<ul class="StatementList">
    <li class="Row">
        <ul class="Headers">
            <li class="Header Remove">&nbsp;</li>
            <li class="Header SelectorType">Selector Type</li>
            <li class="Header SelectorName">Selector Name</li>
            <li class="Header PluginName">Plugin \ Function Name</li>
            <li class="Header PluginParameters">Parameters</li>
        </ul>
    </li>
    <asp:PlaceHolder ID="phStatements" runat="server"></asp:PlaceHolder>
</ul>
<ul class="dnnActions dnnClear">
    <li>
        <asp:LinkButton ID="lbAddStatement" runat="server" CssClass="dnnPrimaryAction" OnClientClick='return setCookie("SkinTweaker_AddStatementClicked", "True", "-1");'>Add a Statement</asp:LinkButton></li>
    <li>
        <asp:LinkButton ID="lbRemoveAllStatements" runat="server" CssClass="dnnSecondaryAction">Remove All Statements</asp:LinkButton></li>
</ul>

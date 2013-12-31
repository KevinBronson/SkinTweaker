<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="jQueryStatement.ascx.vb"
    Inherits="SkinTweaker.jQueryStatement" %>
<%@ Register TagPrefix="uc1" Src="../AutoComplete.ascx" TagName="AutoComplete" %>
<li class="Row">
    <ul class="Statement">
        <li class="Part Remove">
            <asp:LinkButton ID="lbRemove" runat="server" ToolTip="Remove Statement">X</asp:LinkButton>
        </li>
        <li class="Part SelectorType">
            <asp:DropDownList ID="ddlSelectorType" runat="server" AutoPostBack="True" CssClass="ddlSelectorType">
                <asp:ListItem Value="2">Attribute</asp:ListItem>
                <asp:ListItem Value="3">CssClass</asp:ListItem>
                <asp:ListItem Value="4">Element</asp:ListItem>
                <asp:ListItem Value="5">ID</asp:ListItem>
                <asp:ListItem Value="6">Input</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="Part SelectorName">
            <uc1:AutoComplete ID="acSelectorName" runat="server" CssClass="txtSelectorName" />
        </li>
        <li class="Part PluginName">
            <uc1:AutoComplete ID="acPluginName" runat="server" CssClass="txtPluginName"  />
        </li>
        <li class="Part PluginParameters">
            <asp:TextBox ID="txtParameters" runat="server" CssClass="txtParameters"></asp:TextBox></li>
    </ul>
</li>

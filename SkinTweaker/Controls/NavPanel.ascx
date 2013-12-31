<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NavPanel.ascx.vb" Inherits="SkinTweaker.NavPanel" %>
<div id="NavPanel">
    <h1>
        <asp:HyperLink ID="hlSkinTweakerSite" CssClass="hlSkinTweakerSite" runat="server"
            NavigateUrl="http://www.skintweaker.com" Target="_blank" ToolTip="Visit the SkinTweaker.com website"><img src="/DesktopModules/SkinTweaker/images/ST_24x24.png" alt="SkinTweakerLogo" />Skin<span class="SpanPart1">Tweaker</span><span class="SpanPart2">.com</span></asp:HyperLink></h1>
    <hr />
    <h1>
        Navigation Panel</h1>
    <hr />
    <div class="lblDemoModeMessage">
        <asp:Label ID="lblDemoModeMessage" runat="server" Text="Currently in Demo Mode" Visible="False"></asp:Label>
    </div>
    <ul>
        <li class="MainLink">
            <asp:LinkButton ID="lbIntroduction" runat="server" OnClientClick='return setCookie("SkinTweaker_PageViewRequest", "0", "-1");'><b>Introduction</b></asp:LinkButton></li>
        <li class="MainLink">
            <asp:LinkButton ID="lbSelectATheme" runat="server" OnClientClick='return setCookie("SkinTweaker_PageViewRequest", "1", "-1");'><b>Select Theme</b></asp:LinkButton></li>
        <li class="MainLink">
            <asp:LinkButton ID="lbCustomThemes" runat="server" OnClientClick='return setCookie("SkinTweaker_PageViewRequest", "2", "-1");'><b>Custom Themes</b></asp:LinkButton><ol>
                <li class="OderedLink">
                    <asp:LinkButton ID="lbDownload" runat="server" OnClientClick='return setCookie("SkinTweaker_PageViewRequest", "3", "-1");'>Download</asp:LinkButton></li>
                <li class="OderedLink">
                    <asp:LinkButton ID="lbInstall" runat="server" OnClientClick='return setCookie("SkinTweaker_PageViewRequest", "4", "-1");'>Install</asp:LinkButton></li>
                <li class="OderedLink">
                    <asp:LinkButton ID="lbSelect" runat="server" OnClientClick='return setCookie("SkinTweaker_PageViewRequest", "1", "-1");'>Select </asp:LinkButton></li>
            </ol>
        </li>
        <li class="MainLink">
            <asp:HyperLink ID="hlInstallation" runat="server" ToolTip="View installation and setup instructions."
                NavigateUrl="http://www.skintweaker.com/Help.aspx" Target="_blank"><b>Help</b></asp:HyperLink>
        </li>
        <li class="MainLink">&nbsp;</li>
        <li class="MainLink">
            <hr />
            <h3 style="text-align: center">
                Advanced Features</h3>
        </li>
        <li class="MainLink">
            <asp:LinkButton ID="lbjQueryScriptEditor" runat="server" OnClientClick='return setCookie("SkinTweaker_PageViewRequest", "6", "-1");'><b>jQuery Script Editor</b></asp:LinkButton></li>
        <li class="MainLink">
            <asp:LinkButton ID="lbBugHunter" runat="server" OnClientClick='return setCookie("SkinTweaker_PageViewRequest", "7", "-1");'><b>Bug Hunter</b></asp:LinkButton></li>
    </ul>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <hr />
    <p>
        <asp:Label ID="lblDotNetNukeVersion" runat="server" CssClass="lblVersionNumber" />
        <asp:Label ID="lblSkinTweakerVersion" runat="server" CssClass="lblVersionNumber" />
    </p>
</div>

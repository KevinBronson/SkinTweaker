<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ThemeSelector.ascx.vb" Inherits="SkinTweaker.ThemeSelector" %>
<%@ Register TagPrefix="uc2" Src="jQueryScriptEditor/ScriptSelector.ascx" TagName="ScriptSelector" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/labelcontrol.ascx" %>
<ul id="ThemeSelectorControls">
    <li class="label lblThemes">
        <dnn:label ID="lblThemes" runat="server" Text="CSS Theme" Suffix=":" HelpText="This is the name of the CSS Theme." />
    </li>
    <li class="control ddlThemes">
        <asp:DropDownList ID="ddlThemes" runat="server" AutoPostBack="True" />
    </li>
    <li class="label lbljQueryScripts">
        <dnn:label ID="lbljQueryScripts" runat="server" Text="jQuery Script" 
            Suffix=":" HelpText="This is the name of the jQuery Script." />
    </li>
    <li class="control ddlScriptSelector">
        <uc2:ScriptSelector ID="ddlScriptSelector" runat="server" IncludeNONE="True" /></li>
    <li class="label lblApplyCssScopeToPage">
        <dnn:label ID="lblApplyCssScopeToPage" runat="server" Text="Apply to Page"
            Suffix=":" HelpText="Applies the CSS Theme to the entire page of every page on portal. You can apply the CSS Theme to an entire page or just to sections of a page. See doumentation for more information." />
    </li>
    <li class="control ddlApplyCssScope">
        <asp:DropDownList ID="ddlApplyCssScopeToBody" runat="server" AutoPostBack="True">
            <asp:ListItem Text="True" Value="True"></asp:ListItem>
            <asp:ListItem Text="False" Value="False"></asp:ListItem>
        </asp:DropDownList>
    </li>
</ul>
<p>
    &nbsp;</p>
<span class="SimpleTextWarning">Be sure to check the test results below before applying the selected theme.</span>
<asp:LinkButton ID="lbApplyThemeNow" CssClass="lbApplyThemeNow" runat="server">Apply Settings Now</asp:LinkButton>
<%--        The following selector is changing some of the css, for example the dialog boxes are startign to show up.
        <script type="text/javascript">
            $(document).ready(function () {
                $('#switcher').themeswitcher();
            });
        </script>
        <script type="text/javascript"
  src="http://jqueryui.com/themeroller/themeswitchertool/">
</script>
<div id="switcher"></div>--%>
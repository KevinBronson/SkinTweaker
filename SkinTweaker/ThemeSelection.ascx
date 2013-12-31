<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ThemeSelection.ascx.vb" Inherits="SkinTweaker.ThemeSelection" %>
<%-- Imports --%>
<%@ Import Namespace="SkinTweaker" %>
<%-- Test Area Controls --%>
<%@ Register TagPrefix="uc1" Src="Controls/TestAreas/TestAreajQueryUIControls.ascx" TagName="TestAreajQueryUIControls" %>
<%@ Register TagPrefix="uc2" Src="Controls/TestAreas/TestAreaASP_StandardControls.ascx" TagName="TestAreaASP_StandardControls" %>
<%@ Register TagPrefix="uc3" Src="Controls/TestAreas/TestAreaStandardHTML.ascx" TagName="TestAreaStandardHTML" %>
<%-- DNN Test Area Controls --%>
<%@ Register tagprefix="uc4" src="Controls/TestAreas/TestAreaDNN4_Controls.ascx" tagname="TestAreaDNN4_Controls"  %>
<%@ Register tagprefix="uc5" src="Controls/TestAreas/TestAreaDNN5_Controls.ascx" tagname="TestAreaDNN5_Controls"  %>
<%@ Register TagPrefix="uc6" Src="Controls/TestAreas/TestAreaDNN6_Controls.ascx" TagName="TestAreaDNN6_Controls" %>
<%-- Drop Down Selectors --%>
<%@ Register TagPrefix="uc10" Src="Controls/ThemeSelector.ascx" TagName="ThemeSelector" %>
<%@ Register TagPrefix="uc11" Src="Controls/SkinSelector.ascx" TagName="SkinSelector" %>
<%-- DNN Label --%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>

<h1>
    Theme Selection</h1>

<div id="TestingToolControlPanel">
    <div id="TestingToolControlPanelContents">
        <h2>
            Current Portal Settings
        </h2>
        <hr />
        <ul id="CurrentPortalSettings">
            <li class="Header">
                <asp:Label ID="lblTheme" runat="server" Text="CSS Theme:" />
            </li>
            <li class="Data">
                <asp:Label ID="lblPortalCssThemeName" runat="server" />
            </li>
            <li class="Header">
                <asp:Label ID="lbljQueryScripts" runat="server" Text="jQuery Script:" />
            </li>
            <li class="Data">
                <asp:Label ID="lblPortaljQueryScriptName" runat="server" />
            </li>
            <li class="Header">
                <asp:Label ID="lblApplyCssScopeToPage" runat="server" Text="Apply to Page:" />
            </li>
            <li class="Data">
                <asp:Label ID="lblPortalApplyCssScopeToPage" runat="server" />
            </li>
        </ul>
        <p>
            &nbsp;</p>
        <h2>
            Test and Modify Portal Settings</h2>
        <hr />
        <p>
            Changing the selected values here will automatically update the Test Area preview
            below. Once you are satisfied with the test results, you can apply the settings
            to your entire website portal by clicking the big red button below. It is highly
            recommended that you backup your website prior to applying settings.
        </p>
        <hr />
        <uc10:ThemeSelector ID="ThemeSelector1" runat="server" />
        <p>
            &nbsp;</p>
    </div>
</div>
<div style="clear: both;">
</div>
<div style="clear: both;">
</div>
<div>
    <h1>
        TEST AREA
    </h1>
</div>
<div style="width: 700px;">
    <div style="margin: auto;">
        <div class="TestAreaOptionsContainerWrapper">
            <div class="TestAreaOptionsContainerWrapperContents">
                <div style="float: right;">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="CSS Scope Name:" />&nbsp;</b>
                    <asp:Label ID="Label2" runat="server"><em><%= "." & SkinTweaker.Common.Variables.CSS_ScopeName%></em></asp:Label>
                </div>
                <h2>
                    Additional Test Area Options</h2>
                <hr />
                <ul class="AdditionalTestOptions">
                    <li class="Header Controls">
                        <dnn:Label ID="lblTestControls" runat="server" Text="Control Set" Suffix=":" HelpText="Select control set(s) to be displayed below." />
                    </li>
                    <li class="Data">
                        <asp:DropDownList ID="ddlTestControls" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="0" Text=" -- SHOW ALL -- " Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="jQuery UI Controls"></asp:ListItem>
                            <asp:ListItem Value="99" Text="DotNetNuke Controls"></asp:ListItem>
                            <asp:ListItem Value="2" Text="ASP.NET Standard Controls"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Standard HTML"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="Header Skin">
                        <dnn:Label ID="lblSkinSelector" runat="server" Text="Skin"
                            Suffix=":" HelpText="Select a skin to be applied to this test page ONLY. This will NOT affect the rest of your website." />
                    </li>
                    <li class="Data">
                        <uc11:SkinSelector ID="ddlSkinSelector" runat="server" /></li>
                </ul>
                <hr />
                <p>
                    Things don't seem to work right? Please make sure that you have <em>completely</em>
                    finished the <a href="http://skintweaker.com/Help/Installation.aspx" target="_blank">Installation
                        and Setup</a> process.
                </p>
            </div>
        </div>
    </div>
</div>
<asp:Panel ID="pnlTestControls1" runat="server">
    <uc1:TestAreajQueryUIControls ID="TestAreajQueryUIControls1" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlTestControls4" runat="server" Visible="False">
    <uc4:TestAreaDNN4_Controls ID="TestAreaDNN4_Controls1" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlTestControls5" runat="server" Visible="False">
    <uc5:TestAreaDNN5_Controls ID="TestAreaDNN5_Controls1" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlTestControls6" runat="server" Visible="False">
    <uc6:TestAreaDNN6_Controls ID="TestAreaDNN6_Controls1" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlTestControls2" runat="server">
    <uc2:TestAreaASP_StandardControls ID="TestAreaASP_StandardControls1" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlTestControls3" runat="server">
    <uc3:TestAreaStandardHTML ID="TestAreaStandardHTML1" runat="server" />
</asp:Panel>

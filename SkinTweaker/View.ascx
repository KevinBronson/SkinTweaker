<%@ Control Language="vb" Inherits="SkinTweaker.View" AutoEventWireup="false" Explicit="True"
    CodeBehind="View.ascx.vb" %>
<%@ Register TagPrefix="ucx" Src="Controls/NavPanel.ascx" TagName="NavPanel" %>
<%@ Register TagPrefix="ucx" Src="Introduction.ascx" TagName="Introduction" %>
<%@ Register TagPrefix="ucx" Src="ThemeSelection.ascx" TagName="ThemeSelection" %>
<%@ Register TagPrefix="ucx" Src="CustomThemes.ascx" TagName="CustomThemes" %>
<%@ Register TagPrefix="ucx" Src="Download.ascx" TagName="Download" %>
<%@ Register TagPrefix="ucx" Src="Install.ascx" TagName="Install" %>
<%@ Register TagPrefix="ucx" Src="AdvancedSettings.ascx" TagName="AdvancedSettings" %>
<%@ Register TagPrefix="ucx" Src="Controls/BugHunter.ascx" TagName="BugHunter" %>
<%@ Register TagPrefix="ucx" Src="Controls/jQueryScriptEditor/jQueryScriptEditor.ascx" TagName="jQueryScriptEditor" %>
<div id="SkinTweakerModule" class="SkinTweakerModule">
    <div id="LeftPaneArea">
        <ucx:NavPanel ID="NavPanel1" runat="server" />
    </div>
    <div id="RightPaneArea">
        <asp:Label runat="server" ID="lblWarning" CssClass="dnnFormMessage dnnFormWarning MessageDisplay"
            Visible="False" EnableViewState="False" />
        <asp:Label runat="server" ID="lblInfo" CssClass="dnnFormMessage dnnFormInfo MessageDisplay" Visible="False"
            EnableViewState="False" />
        <asp:Label runat="server" ID="lblSuccess" CssClass="dnnFormMessage dnnFormSuccess MessageDisplay" 
            Visible="False" EnableViewState="False" />
        <asp:Label runat="server" ID="lblError" CssClass="dnnFormMessage dnnFormValidationSummary MessageDisplay"
            Visible="False" EnableViewState="False" />
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View0" runat="server">
                <ucx:Introduction ID="Introduction1" runat="server" />
            </asp:View>
            <asp:View ID="View1" runat="server">
                <ucx:ThemeSelection ID="ThemeSelection1" runat="server" />
            </asp:View>
            <asp:View ID="View2" runat="server">
                <ucx:CustomThemes ID="CustomThemes1" runat="server" />
            </asp:View>
            <asp:View ID="View3" runat="server">
                <ucx:Download ID="Download1" runat="server" />
            </asp:View>
            <asp:View ID="View4" runat="server">
                <ucx:Install ID="Install1" runat="server" />
            </asp:View>
            <asp:View ID="View5" runat="server">
                <ucx:AdvancedSettings ID="AdvancedSettings1" runat="server" />
            </asp:View>
            <asp:View ID="View6" runat="server">
                <ucx:jqueryscripteditor id="jQueryScriptEditor1" runat="server" />
            </asp:View>
            <asp:View ID="View7" runat="server">
                <ucx:bughunter id="BugHunter1" runat="server" />
            </asp:View>
        </asp:MultiView>
    </div>
</div>
<div style="clear: both;">
</div>

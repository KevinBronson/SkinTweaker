<%@ Control Language="vb" Inherits="SkinTweaker.AdvancedSettings" AutoEventWireup="false"
    CodeBehind="AdvancedSettings.ascx.vb" %>
<%--<%@ Register TagPrefix="uc1" Src="Controls/BugHunter.ascx" TagName="BugHunter" %>
<%@ Register TagPrefix="uc2" Src="Controls/jQueryScriptEditor/jQueryScriptEditor.ascx" TagName="jQueryScriptEditor" %>--%>
<div class="ST_PageWrapper">
    <div class="ST_PageContents">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
            <asp:View ID="vwDemoMode" runat="server">
                <h1>
                    Advanced Settings - Not Available in Demo Mode</h1>
            </asp:View>
            <asp:View ID="vwNormal" runat="server">
                <h1>
                    Advanced Settings</h1>
                <p>
                    &nbsp;</p>
                <%--<uc1:BugHunter ID="BugHunter1" runat="server" />
                <uc2:jQueryScriptEditor ID="jQueryScriptEditor1" runat="server" />--%>
            </asp:View>
        </asp:MultiView>
    </div>
</div>

<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TestAreaDNN5_Controls.ascx.vb"
    Inherits="SkinTweaker.TestAreaDNN5_Controls" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="URL" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Skin" Src="~/controls/SkinControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Security.Permissions.Controls"
    Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<div class="<%=GetScopeClass%>">
    <div class="TestAreaContainerWrapper">
        <div class="TestAreaContainerWrapperContents">
            <ul class="TestControlApplyScopeCheckBox">
                <li>
                    <dnn:Label ID="lblApplyScope" CssClass="lblApplyScope" Text="Apply CSS Theme to This Test Control Set"
                        runat="server" Suffix=":" HelpText="Applies the CSS Theme directly to this test control set. Thus it does not apply the CSS Theme to the entire page. See documentation for more information." />
                </li>
                <li>
                    <asp:CheckBox ID="chkApplyScope" runat="server" AutoPostBack="True" />
                </li>
            </ul>
            <h2>
                DotNetNuke 5.x Controls</h2>
            <p>
                Below you will find various samples of how DotNetNuke's administrative controls
                will look with SkinTweaker installed on your system.
            </p>
            <p>
                &nbsp;</p>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Section Headers</h3>
                    <br />
                    <dnn:SectionHead ID="dshSection1" CssClass="Head" runat="server" Text="Section 1"
                        Section="Section1" IncludeRule="False" IsExpanded="False" />
                    <table id="Section1" cellspacing="2" cellpadding="2" summary="This is Section 1"
                        border="0" runat="server">
                        <tr>
                            <td>
                                Section 1 content.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dnn:SectionHead ID="dshSubsection1" CssClass="Head" runat="server" Text="Subsection 1"
                                    Section="Subsection1" IncludeRule="False" IsExpanded="False" />
                                <table id="Subsection1" cellspacing="2" cellpadding="2" summary="This is Subsection 1"
                                    border="0" runat="server">
                                    <tr>
                                        <td>
                                            Subsection 1 content.
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dnn:SectionHead ID="dshSubsection2" CssClass="Head" runat="server" Text="Subsection 2"
                                    Section="Subsection2" IncludeRule="False" IsExpanded="False" />
                                <table id="Subsection2" cellspacing="2" cellpadding="2" summary="This is Subsection 2"
                                    border="0" runat="server">
                                    <tr>
                                        <td>
                                            Subsection 2 content.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <dnn:SectionHead ID="SectionHead2" CssClass="Head" runat="server" Text="Section 2"
                        Section="Section2" IncludeRule="False" IsExpanded="False" />
                    <table id="Section2" cellspacing="2" cellpadding="2" summary="This is Section 2"
                        border="0" runat="server">
                        <tr>
                            <td>
                                Section 2 content.
                            </td>
                        </tr>
                    </table>
                    <p>
                        &nbsp;</p>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Module Permissions Grid</h3>
                    <table id="tblDetails" cellspacing="2" cellpadding="2" summary="Appearance Design Table"
                        border="0" runat="server">
                        <tr id="rowPerm" runat="server">
                            <td colspan="2" align="left">
                                <table border="0" cellpadding="0" cellspacing="0" style="text-align: center;">
                                    <tr>
                                        <td>
                                            <dnn:ModulePermissionsGrid ID="dgPermissions" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <p>
                        &nbsp;</p>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Command Buttons
                    </h3>
                    <br />
                    <p>
                        <dnn:CommandButton ID="cmdUpdate" runat="server" CssClass="CommandButton" ImageUrl="~/images/save.gif"
                            CausesValidation="False" Text="Update" />
                        &nbsp;
                        <dnn:CommandButton ID="cmdDelete" runat="server" CssClass="CommandButton" ImageUrl="~/images/delete.gif"
                            CausesValidation="False" Text="Delete" />
                        &nbsp;
                        <dnn:CommandButton ID="cmdCancel" runat="server" CssClass="CommandButton" ImageUrl="~/images/lt.gif"
                            CausesValidation="False" Text="Cancel" />
                    </p>
                    <p>
                        &nbsp;</p>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Captcha Control</h3>
                    <dnn:CaptchaControl ID="ctlCaptcha" CaptchaWidth="130" CaptchaHeight="40" CssClass="Normal"
                        runat="server" ErrorStyle-CssClass="NormalRed" />
                    <p>
                        &nbsp;</p>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        URL Control</h3>
                    <dnn:URL ID="ctlIcon" runat="server" style="width: 475px" ShowImages="true" ShowUrls="False"
                        ShowTabs="False" ShowLog="False" ShowTrack="False" Required="False" ShowNone="true" />
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Skin Control</h3>
                    <br />
                    <dnn:Skin ID="ctlModuleContainer" __designer:dtid="281479271678042" runat="server"
                        Width="450px" DefaultKey="Page"></dnn:Skin>
                    <p>
                        &nbsp;</p>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Text Editor</h3>
                    <dnn:TextEditor ID="TextEditor1" runat="server" Width="650px" Height="350px" />
                    <p>
                        &nbsp;</p>
                </div>
            </div>
            <%--

            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        TITLE GOES HERE</h3>
                    CONTENT GOES HERE
                    <p>
                        &nbsp;</p>
                </div>
            </div>

            --%>
        </div>
    </div>
</div>

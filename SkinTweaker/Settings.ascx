<%@ Control Language="vb" AutoEventWireup="false" Inherits="SkinTweaker.Settings"
    CodeBehind="Settings.ascx.vb" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/labelcontrol.ascx" %>
<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead">
    <a href="" class="dnnSectionExpanded">Basic Settings</a></h2>
<div class="SkinTweakerModuleSettings">
    <div class="dnnForm">
        <fieldset>
            <div class="dnnFormItem">
                <ul>
                    <li>
                        <dnn:label ID="lblDemoModeON" runat="server" Text="Enable Demo Mode" Suffix=":" HelpText="Check this box to turn on Demo Mode. This mode is used for the demo website."
                            CssClass="lblDemoModeON" />
                    </li>
                    <li>
                        <asp:CheckBox ID="chkDemoModeON" runat="server" CssClass="chkDemoModeON" />
                    </li>
                </ul>
            </div>
            <div class="dnnFormItem">
                <ul>
                    <li>
                        <dnn:label ID="lblDebuggingModeON" runat="server" Text="Enable Debugging" Suffix=":"
                            HelpText="Check this box to turn on Debug Mode which is used for testing and troubleshooting purposes." />
                    </li>
                    <li>
                        <asp:CheckBox ID="chkDebuggingModeON" runat="server" />
                    </li>
                </ul>
            </div>
            <div class="dnnFormItem">
                <ul>
                    <li>
                        <dnn:label ID="lblLoggingON" runat="server" Text="Enable Logging" Suffix=":" HelpText="Check this box to turn on logging. Logs will be saved under ~/DesktopModule/SkinTweaker/Logs." />
                    </li>
                    <li>
                        <asp:CheckBox ID="chkLoggingON" runat="server" />
                    </li>
                </ul>
            </div>
            <asp:Panel ID="pnlDNN5" runat="server" Visible="False">
                <div class="dnnFormItem">
                    <ul>
                        <li>
                            <dnn:label ID="lblAddjQuery" runat="server" Text="Add jQuery and/or jQuery UI" Suffix=":"
                                HelpText="Check this box to have jQuery and/or jQuery UI automatically added. See documentation for more detials." />
                        </li>
                        <li>
                            <asp:CheckBox ID="chkAddjQuery" runat="server" />
                        </li>
                    </ul>
                </div>
                <asp:Panel ID="pnlDNN4" runat="server" Visible="False">
                    <div class="dnnFormItem">
                        <ul>
                            <li>
                                <dnn:label ID="lbljQueryVersionNumber" runat="server" Text="jQuery Version Number"
                                    Suffix=":" HelpText="Please specify the version number of jQuery that you would like to use in this format '#.#.#'. For Example the default value is 1.6.4" />
                            </li>
                            <li>
                                <asp:TextBox ID="txtjQueryVersionNumber" runat="server"/>
                            </li>
                        </ul>
                    </div>
                </asp:Panel>
                <div class="dnnFormItem">
                    <ul>
                        <li>
                            <dnn:label ID="lbljQueryUI_VersionNumber" runat="server" Text="jQuery UI Version Number"
                                Suffix=":" HelpText="Please specify the version number of jQuery UI that you would like to use in this format '#.#.#'. For Example the default value is 1.8.16" />
                        </li>
                        <li>
                            <asp:TextBox ID="txtjQueryUI_VersionNumber" runat="server"/>
                        </li>
                    </ul>
                </div>
            </asp:Panel>
        </fieldset>
    </div>
</div>

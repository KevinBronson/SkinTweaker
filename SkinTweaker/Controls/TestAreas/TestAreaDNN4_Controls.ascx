<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TestAreaDNN4_Controls.ascx.vb"
    Inherits="SkinTweaker.TestAreaDNN4_Controls" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
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
                DotNetNuke 4.x Controls</h2>
            <p>
                Below you will find samples of how DotNetNuke's controls will look. If something
                doesn't look quite right to you, then please refer to ... for comparison.
            </p>
            <p>
                &nbsp;</p>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <p>
                        &nbsp;</p>
                </div>
            </div>
        </div>
    </div>
</div>

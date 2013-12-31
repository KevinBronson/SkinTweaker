<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TestAreaDNN6_Controls.ascx.vb"
    Inherits="SkinTweaker.TestAreaDNN6_Controls" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
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
                DotNetNuke 6.x Controls</h2>
            <p>
                Below you will find samples of how DotNetNuke's controls will look. If something
                doesn't look quite right to you, then please refer to the demos in
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://uxguide.dotnetnuke.com/UIPatterns/Overview.aspx"
                    Target="_blank">DotNetNuke's UX Guide</asp:HyperLink>
                for comparison.
            </p>
            <p>
                &nbsp;</p>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Dialog Box</h3>
                    <a name="#DialogueBoxes" style="text-decoration: none" />
                    <script type="text/javascript">
                        jQuery(function ($) {
                            $('#dialogs-demo .alert').click(function (event) {
                                event.preventDefault();
                                $.dnnAlert({
                                    text: 'This is a sample Alert Message.'
                                });
                            });
                        });
                    </script>
                    <script type="text/javascript">
                        jQuery(function ($) {
                            $('#dialogs-demo .confirm').dnnConfirm();
                        });
                    </script>
                    <div id="dialogs-demo">
                        <a class="confirm dnnPrimaryAction" href="#DialogueBoxes">Click to see a Confirm Message</a>&nbsp;&nbsp;&nbsp;
                        <a class="alert dnnPrimaryAction" href="#">Click to see an Alert Message</a>
                    </div>
                    <p>
                        &nbsp;</p>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Messages Demo</h3>
                    <div class="dnnFormMessage dnnFormWarning">
                        This is a warning!</div>
                    <div class="dnnFormMessage dnnFormInfo">
                        This is informational!</div>
                    <div class="dnnFormMessage dnnFormSuccess">
                        This is a success message!</div>
                    <div class="dnnFormMessage dnnFormValidationSummary">
                        This is an error!</div>
                    <div class="dnnForm">
                        <fieldset>
                            <div class="dnnFormItem">
                                <label>
                                    A simple label</label>
                                <input />
                                <span class="dnnFormMessage dnnFormError">This is invalid!</span>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Tabs Demo</h3>
                    <script type="text/javascript">
                        jQuery(function ($) {
                            $('#tabs-demo').dnnTabs();
                        });
                    </script>
                    <div class="dnnForm" id="tabs-demo">
                        <ul class="dnnAdminTabNav">
                            <li><a href="#Nunctincidunt">Nunc tincidunt</a></li>
                            <li><a href="#Proindolor">Proin dolor</a></li>
                            <li><a href="#Aeneanlacinia">Aenean lacinia</a></li>
                        </ul>
                        <div id="Nunctincidunt" class="dnnClear">
                            <p>
                                Proin elit arcu, rutrum commodo, vehicula tempus, commodo a, risus. Curabitur nec
                                arcu. Donec sollicitudin mi sit amet mauris. Nam elementum quam ullamcorper ante.
                                Etiam aliquet massa et lorem. Mauris dapibus lacus auctor risus. Aenean tempor ullamcorper
                                leo. Vivamus sed magna quis ligula eleifend adipiscing. Duis orci. Aliquam sodales
                                tortor vitae ipsum. Aliquam nulla. Duis aliquam molestie erat. Ut et mauris vel
                                pede varius sollicitudin. Sed ut dolor nec orci tincidunt interdum. Phasellus ipsum.
                                Nunc tristique tempus lectus.</p>
                        </div>
                        <div id="Proindolor" class="dnnClear">
                            <p>
                                Morbi tincidunt, dui sit amet facilisis feugiat, odio metus gravida ante, ut pharetra
                                massa metus id nunc. Duis scelerisque molestie turpis. Sed fringilla, massa eget
                                luctus malesuada, metus eros molestie lectus, ut tempus eros massa ut dolor. Aenean
                                aliquet fringilla sem. Suspendisse sed ligula in ligula suscipit aliquam. Praesent
                                in eros vestibulum mi adipiscing adipiscing. Morbi facilisis. Curabitur ornare consequat
                                nunc. Aenean vel metus. Ut posuere viverra nulla. Aliquam erat volutpat. Pellentesque
                                convallis. Maecenas feugiat, tellus pellentesque pretium posuere, felis lorem euismod
                                felis, eu ornare leo nisi vel felis. Mauris consectetur tortor et purus.</p>
                        </div>
                        <div id="Aeneanlacinia" class="dnnClear">
                            <p>
                                Mauris eleifend est et turpis. Duis id erat. Suspendisse potenti. Aliquam vulputate,
                                pede vel vehicula accumsan, mi neque rutrum erat, eu congue orci lorem eget lorem.
                                Vestibulum non ante. Class aptent taciti sociosqu ad litora torquent per conubia
                                nostra, per inceptos himenaeos. Fusce sodales. Quisque eu urna vel enim commodo
                                pellentesque. Praesent eu risus hendrerit ligula tempus pretium. Curabitur lorem
                                enim, pretium nec, feugiat nec, luctus a, lacus.</p>
                            <p>
                                Duis cursus. Maecenas ligula eros, blandit nec, pharetra at, semper at, magna. Nullam
                                ac lacus. Nulla facilisi. Praesent viverra justo vitae neque. Praesent blandit adipiscing
                                velit. Suspendisse potenti. Donec mattis, pede vel pharetra blandit, magna ligula
                                faucibus eros, id euismod lacus dolor eget odio. Nam scelerisque. Donec non libero
                                sed nulla mattis commodo. Ut sagittis. Donec nisi lectus, feugiat porttitor, tempor
                                ac, tempor vitae, pede. Aenean vehicula velit eu tellus interdum rutrum. Maecenas
                                commodo. Pellentesque nec elit. Fusce in lacus. Vivamus a libero vitae lectus hendrerit
                                hendrerit.</p>
                        </div>
                    </div>
                    <p>
                        &nbsp;</p>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Panels Demo</h3>
                    <script type="text/javascript">
                        jQuery(function ($) {
                            var setupModule = function () {
                                $('#panels-demo').dnnPanels();
                                $('#panels-demo .dnnFormExpandContent a').dnnExpandAll({
                                    targetArea: '#panels-demo'
                                });
                            };
                            setupModule();
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                                // note that this will fire when _any_ UpdatePanel is triggered,
                                // which may or may not cause an issue
                                setupModule();
                            });
                        });
                    </script>
                    <div class="dnnForm" id="panels-demo">
                        <h2 id="H1" class="dnnFormSectionHead">
                            <a href="#">Nunc tincidunt</a></h2>
                        <fieldset class="dnnClear">
                            <p>
                                Proin elit arcu, rutrum commodo, vehicula tempus, commodo a, risus. Curabitur nec
                                arcu. Donec sollicitudin mi sit amet mauris. Nam elementum quam ullamcorper ante.
                                Etiam aliquet massa et lorem. Mauris dapibus lacus auctor risus. Aenean tempor ullamcorper
                                leo. Vivamus sed magna quis ligula eleifend adipiscing. Duis orci. Aliquam sodales
                                tortor vitae ipsum. Aliquam nulla. Duis aliquam molestie erat. Ut et mauris vel
                                pede varius sollicitudin. Sed ut dolor nec orci tincidunt interdum. Phasellus ipsum.
                                Nunc tristique tempus lectus.</p>
                        </fieldset>
                        <h2 id="H2" class="dnnFormSectionHead">
                            <a href="#">Proin dolor</a></h2>
                        <fieldset class="dnnClear">
                            <p>
                                Morbi tincidunt, dui sit amet facilisis feugiat, odio metus gravida ante, ut pharetra
                                massa metus id nunc. Duis scelerisque molestie turpis. Sed fringilla, massa eget
                                luctus malesuada, metus eros molestie lectus, ut tempus eros massa ut dolor. Aenean
                                aliquet fringilla sem. Suspendisse sed ligula in ligula suscipit aliquam. Praesent
                                in eros vestibulum mi adipiscing adipiscing. Morbi facilisis. Curabitur ornare consequat
                                nunc. Aenean vel metus. Ut posuere viverra nulla. Aliquam erat volutpat. Pellentesque
                                convallis. Maecenas feugiat, tellus pellentesque pretium posuere, felis lorem euismod
                                felis, eu ornare leo nisi vel felis. Mauris consectetur tortor et purus.</p>
                        </fieldset>
                        <h2 id="H3" class="dnnFormSectionHead">
                            <a href="#">Aenean lacinia</a></h2>
                        <fieldset class="dnnClear">
                            <p>
                                Mauris eleifend est et turpis. Duis id erat. Suspendisse potenti. Aliquam vulputate,
                                pede vel vehicula accumsan, mi neque rutrum erat, eu congue orci lorem eget lorem.
                                Vestibulum non ante. Class aptent taciti sociosqu ad litora torquent per conubia
                                nostra, per inceptos himenaeos. Fusce sodales. Quisque eu urna vel enim commodo
                                pellentesque. Praesent eu risus hendrerit ligula tempus pretium. Curabitur lorem
                                enim, pretium nec, feugiat nec, luctus a, lacus.</p>
                            <p>
                                Duis cursus. Maecenas ligula eros, blandit nec, pharetra at, semper at, magna. Nullam
                                ac lacus. Nulla facilisi. Praesent viverra justo vitae neque. Praesent blandit adipiscing
                                velit. Suspendisse potenti. Donec mattis, pede vel pharetra blandit, magna ligula
                                faucibus eros, id euismod lacus dolor eget odio. Nam scelerisque. Donec non libero
                                sed nulla mattis commodo. Ut sagittis. Donec nisi lectus, feugiat porttitor, tempor
                                ac, tempor vitae, pede. Aenean vehicula velit eu tellus interdum rutrum. Maecenas
                                commodo. Pellentesque nec elit. Fusce in lacus. Vivamus a libero vitae lectus hendrerit
                                hendrerit.</p>
                        </fieldset>
                    </div>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        DataGrid Demo</h3>
                    <div class="dnnForm dnnSecurityRoles">
                        <table cellspacing="0" border="0" style="border-style: None; width: 98%; border-collapse: collapse;"
                            id="dnn_ctr623_DataGridDemo_grdRoles" class="dnnGrid">
                            <tbody>
                                <tr valign="top" class="dnnGridHeader">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Name
                                    </td>
                                    <td>
                                        Description
                                    </td>
                                    <td>
                                        Auto
                                    </td>
                                </tr>
                                <tr align="left" class="dnnGridItem">
                                    <td>
                                        <a href="#">
                                            <img alt="Edit" src="http://uxguide.dnngallery.com/icons/sigma/Edit_16X16_Standard.png" /></a>
                                    </td>
                                    <td>
                                        <a href="#">
                                            <img alt="Edit" src="http://uxguide.dnngallery.com/icons/sigma/Users_16X16_Standard.png" /></a>
                                    </td>
                                    <td>
                                        Administrators
                                    </td>
                                    <td>
                                        Portal Administration
                                    </td>
                                    <td>
                                        <img style="border-width: 0px;" src="/icons/sigma/Unchecked_16X16_Standard.png" id="dnn_ctr623_DataGridDemo_grdRoles_ctl02_Image2" />
                                    </td>
                                </tr>
                                <tr align="left" class="dnnGridAltItem">
                                    <td>
                                        <a href="#">
                                            <img alt="Edit" src="http://uxguide.dnngallery.com/icons/sigma/Edit_16X16_Standard.png" /></a>
                                    </td>
                                    <td>
                                        <a href="#">
                                            <img alt="Edit" src="http://uxguide.dnngallery.com/icons/sigma/Users_16X16_Standard.png" /></a>
                                    </td>
                                    <td>
                                        Registered Users
                                    </td>
                                    <td>
                                        Registered Users
                                    </td>
                                    <td>
                                        <img style="border-width: 0px;" src="/icons/sigma/Checked_16X16_Standard.png" id="dnn_ctr623_DataGridDemo_grdRoles_ctl03_Image1" />
                                    </td>
                                </tr>
                                <tr align="left" class="dnnGridItem">
                                    <td>
                                        <a href="#">
                                            <img alt="Edit" src="http://uxguide.dnngallery.com/icons/sigma/Edit_16X16_Standard.png" /></a>
                                    </td>
                                    <td>
                                        <a href="#">
                                            <img alt="Edit" src="http://uxguide.dnngallery.com/icons/sigma/Users_16X16_Standard.png" /></a>
                                    </td>
                                    <td>
                                        Subscribers
                                    </td>
                                    <td>
                                        A public role for portal subscriptions
                                    </td>
                                    <td>
                                        <img style="border-width: 0px;" src="/icons/sigma/Checked_16X16_Standard.png" id="dnn_ctr623_DataGridDemo_grdRoles_ctl04_Image1">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <p>
                        &nbsp;</p>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <div class="dnnForm" id="form-demo">
                        <h3>
                            A simple form example.</h3>
                        <a name="#SimpleFormExample" style="text-decoration: none" />
                        <div class="dnnFormItem dnnFormHelp dnnClear">
                            <p class="dnnFormRequired">
                                <asp:Label ID="lblRequired" runat="server" Text="Required Indicator" /></p>
                        </div>
                        <fieldset>
                            <div class="dnnFormItem">
                                <dnn:Label ID="lblName" runat="server" ControlName="NameTextBox" Text="Name" Suffix=":" />
                                <asp:TextBox runat="server" ID="NameTextBox" CssClass="dnnFormRequired" />
                            </div>
                            <div class="dnnFormItem">
                                <dnn:Label ID="lblDescription" runat="server" ControlName="DescriptionTextBox" Text="Description"
                                    Suffix=":" />
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="DescriptionTextBox" />
                            </div>
                            <div class="dnnFormItem">
                                <dnn:Label ID="lblChoice" runat="server" ControlName="ChoiceDropDown" Text="Choice"
                                    Suffix=":" />
                                <asp:DropDownList runat="server" ID="ChoiceDropDown">
                                    <asp:ListItem Text="-- Make a Choice --" />
                                    <asp:ListItem Text="First Choice" />
                                    <asp:ListItem Text="Second Choice" />
                                </asp:DropDownList>
                            </div>
                            <div class="dnnFormItem">
                                <dnn:Label ID="lblRate" runat="server" ControlName="RateRadioButtons" Text="Rate"
                                    Suffix=":" />
                                <asp:RadioButtonList runat="server" ID="RateRadioButtons" RepeatDirection="Horizontal"
                                    CssClass="dnnFormRadioButtons">
                                    <asp:ListItem Text="1" />
                                    <asp:ListItem Text="2" />
                                    <asp:ListItem Text="3" />
                                    <asp:ListItem Text="4" />
                                    <asp:ListItem Text="5" />
                                </asp:RadioButtonList>
                            </div>
                            <div class="dnnFormItem">
                                <dnn:Label ID="lblAgree" runat="server" ControlName="AgreeCheckbox" Text="Agree"
                                    Suffix=":" />
                                <asp:CheckBox runat="server" ID="AgreeCheckbox" />
                            </div>
                            <div class="dnnFormItem">
                                <asp:Label ID="lblStartDatePicker" runat="server" CssClass="dnnFormLabel" Text="Date: " />
                                <asp:PlaceHolder ID="phDatePicker" runat="server"></asp:PlaceHolder>
                            </div>
                        </fieldset>
                        <ul class="dnnActions dnnClear">
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="dnnPrimaryAction" Text="Save" /></li>
                            <li>
                                <asp:HyperLink ID="HyperLinkCancel" runat="server" CssClass="dnnSecondaryAction"
                                    Text="Cancel" /></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Text Editor Demo</h3>
                    <div class="dnnFormItem">
                        <dnn:TextEditor ID="TextEditor1" runat="server" Width="650px" />
                    </div>
                    <p>
                        &nbsp;</p>
                </div>
            </div>
        </div>
    </div>
</div>

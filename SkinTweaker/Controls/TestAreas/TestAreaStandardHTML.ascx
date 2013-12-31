<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TestAreaStandardHTML.ascx.vb"
    Inherits="SkinTweaker.TestAreaStandardHTML" %>
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
                    <asp:CheckBox ID="chkApplyScope" runat="server" AutoPostBack="True" Checked="True" />
                </li>
            </ul>
            <h2>
                Standard HTML</h2>
            <p>
                &nbsp;</p>
            <div id="FunctionalityCheck" class="demoContainer">
                <div class="demoContainerContents">
                    <p>
                        &nbsp;</p>
                    <h2>
                        Inputs</h2>
                    <p>
                        <input id="Button1" type="button" value="button" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="Reset1" type="reset" value="reset" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="Submit1" type="submit" value="submit" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="Text1" type="text" value="text" />&nbsp;&nbsp;&nbsp;&nbsp; Password:
                        <input id="Password1" type="password" value="abcdefg" />
                    </p>
                    <p>
                        File:
                        <input id="File1" type="file" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="Checkbox1" type="checkbox" />Checkbox
                        <input id="Radio1" type="radio" />Radio
                    </p>
                    Horizontal Rule:
                    <hr />
                    <h2>
                        Other</h2>
                    <p>
                        <img alt="img tag" src="" title="img tag" />
                    </p>
                    <p>
                        <button type="button">
                            button tag</button>
                    </p>
                    <p>
                        Select:
                        <select id="Select1">
                            <option>option 1</option>
                            <option>option 2</option>
                            <option>option 3</option>
                        </select></p>
                    <p>
                        <textarea id="TextArea1" cols="20" rows="2">textarea</textarea>
                    </p>
                    <p>
                        Table:
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    &nbsp;1
                                </td>
                                <td>
                                    &nbsp;2
                                </td>
                                <td>
                                    &nbsp;3
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;4
                                </td>
                                <td>
                                    &nbsp;5
                                </td>
                                <td>
                                    &nbsp;6
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;7
                                </td>
                                <td>
                                    &nbsp;8
                                </td>
                                <td>
                                    &nbsp;9
                                </td>
                            </tr>
                        </table>
                    </p>
                </div>
            </div>
        </div>
    </div>
</div>

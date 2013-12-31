<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TestAreaASP_StandardControls.ascx.vb"
    Inherits="SkinTweaker.TestAreaASP_StandardControls" %>
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
                ASP.NET Standard Controls</h2>
            <p>
                &nbsp;</p>
            <div id="FunctionalityCheck" class="demoContainer">
                <div class="demoContainerContents">
                    <p>
                        &nbsp;</p>
                    <h2>
                        ASP Buttons</h2>
                    <asp:Button ID="Button1" runat="server" Text="Button" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="ImageButton" />
                    <hr />
                    <h2>
                        Common ASP Controls</h2>
                    <p>
                        <asp:Label ID="Label1" runat="server" Text="This is a Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TextBox1" runat="server">TextBox</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="CheckBox" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="RadioButton1" runat="server" Text="RadioButton" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
                    </p>
                    <p>
                        FileUpload:&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
                    </p>
                    <p>
                        Calendar:<br />
                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                    </p>
                    <p>
                        Table:
                    </p>
                    <p>
                        <asp:Table ID="Table1" runat="server">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>TableHeaderRow:TableHeaderCell</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell>TableRow:TableCell</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableFooterRow>
                                <asp:TableCell>TableFooterRow:TableCell</asp:TableCell></asp:TableFooterRow>
                        </asp:Table>
                    </p>
                    <hr />
                    <h2>
                        ASP Image Controls</h2>
                    <asp:Image ID="Image1" runat="server" ToolTip="Image" AlternateText="Image" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageMap ID="ImageMap1" runat="server" ToolTip="ImageMap" AlternateText="ImageMap">
                    </asp:ImageMap>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="ImageButton" AlternateText="ImageButton" />
                    <hr />
                    <h2>
                        ASP Lists</h2>
                    <table>
                        <tr>
                            <td>
                                BulletedList
                            </td>
                            <td>
                                CheckBoxList
                            </td>
                            <td>
                                DropDownList
                            </td>
                            <td>
                                RadioButtonList
                            </td>
                            <td>
                                ListBox
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:BulletedList ID="BulletedList1" runat="server" ToolTip="asp:BulletedList">
                                    <asp:ListItem>asp:ListItem 1</asp:ListItem>
                                    <asp:ListItem>asp:ListItem 2</asp:ListItem>
                                </asp:BulletedList>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" ToolTip="asp:CheckBoxList">
                                    <asp:ListItem>asp:ListItem 1</asp:ListItem>
                                    <asp:ListItem>asp:ListItem 2</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" ToolTip="asp:DropDownList">
                                    <asp:ListItem>asp:ListItem 1</asp:ListItem>
                                    <asp:ListItem>asp:ListItem 2</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                    <asp:ListItem>asp:ListItem 1</asp:ListItem>
                                    <asp:ListItem>asp:ListItem 2</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:ListBox ID="ListBox1" runat="server">
                                    <asp:ListItem>ListItem 1</asp:ListItem>
                                    <asp:ListItem>ListItem 2</asp:ListItem>
                                    <asp:ListItem>ListItem 3</asp:ListItem>
                                    <asp:ListItem>ListItem 4</asp:ListItem>
                                    <asp:ListItem>ListItem 5</asp:ListItem>
                                    <asp:ListItem>ListItem 6</asp:ListItem>
                                    <asp:ListItem>ListItem 7</asp:ListItem>
                                    <asp:ListItem>ListItem 8</asp:ListItem>
                                </asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>

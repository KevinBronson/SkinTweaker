<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Options.ascx.vb" Inherits="SkinTweaker.Options" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<ul class="CheckBoxList">
    <li class="Option">
        <ul class="CheckBoxList">
            <li class="Box">
                <asp:CheckBox ID="chkChangeAllButtons" runat="server" AutoPostBack="True" />
            </li>
            <li class="Header">
                <dnn:Label ID="lblChangeAllButtons" Text="Change all buttons to jQuery buttons" runat="server"
                    HelpText="This will attempt to change all the buttons on a page to jQuery style buttons." />
            </li>
        </ul>
    </li>
    <li class="Option">
        <ul class="CheckBoxList">
            <li class="Box">
                <asp:CheckBox ID="chkFillInCalendars" runat="server" AutoPostBack="True" />
            </li>
            <li class="Header">
                <dnn:Label ID="lblFillInCalendars" Text="Fill-in Calendars / Date Pickers" runat="server"
                    HelpText="If your calendars or datepickers look empty, that is no colors and formatting just numbers in a wire grid, then try this." />
            </li>
        </ul>
    </li>
    <li class="Option">
        <ul class="CheckBoxList">
            <li class="Box">
                <asp:CheckBox ID="chkFillInBoxes" runat="server" AutoPostBack="True" />
            </li>
            <li class="Header">
                <dnn:Label ID="lblFillInBoxes" Text="Fill-in Dialog Boxes" runat="server" HelpText="If your dialog boxes look empty, that is no colors and formatting just a group of words floating around if anything at all, then try this." />
            </li>
        </ul>
    </li>
    <li class="Option">
        <ul class="CheckBoxList">
            <li class="Box">
                <asp:CheckBox ID="chkAutoCompleteFix" runat="server" AutoPostBack="True" />
            </li>
            <li class="Header">
                <dnn:Label ID="lblAutoCompleteFix" Text="Autocomplete List Correction" runat="server"
                    HelpText="If your jQuery autocomplete or combo-box dropdowns look funny, then try this. NOTE: This tweak is meant to work with Bug 4 dead." />
            </li>
        </ul>
    </li>
</ul>

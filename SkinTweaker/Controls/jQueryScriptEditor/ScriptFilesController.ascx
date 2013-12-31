<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ScriptFilesController.ascx.vb"
    Inherits="SkinTweaker.ScriptFilesController" %>
<%@ Register TagPrefix="uc1" Src="ScriptSelector.ascx" TagName="ScriptSelector" %>
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwDefaultCommandBar" runat="server">
        Script Name:&nbsp;<uc1:ScriptSelector ID="ddlScriptSelector" runat="server" IncludeNONE="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="chkFileIsLoaded" runat="server" Visible="False" Text="File Is Loaded"
            Enabled="False" ToolTip="READ ONLY: Indicates whether or not a file is currently loaded." />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lbReload" runat="server" CssClass="dnnSecondaryAction" Visible="False"><b>Reload Script</b></asp:LinkButton>
    </asp:View>
    <asp:View ID="vwNewFileCommandBar" runat="server">
        New Script Name:
        <asp:TextBox ID="txtNewName" runat="server"></asp:TextBox>
        <asp:CheckBox ID="chkCopyCurrentScript" runat="server" Text="Copy Current Script" />
    </asp:View>
</asp:MultiView>
<ul class="dnnActions dnnClear">
    <li>
        <asp:LinkButton ID="lbSave" runat="server" CssClass="dnnPrimaryAction">Save</asp:LinkButton></li>
    <li>
        <asp:LinkButton ID="lbNew" runat="server" CssClass="dnnSecondaryAction">New</asp:LinkButton></li>
    <li>
        <asp:LinkButton ID="lbCopy" runat="server" CssClass="dnnSecondaryAction">Copy</asp:LinkButton></li>
    <li>
        <asp:LinkButton ID="lbCancel" runat="server" CssClass="dnnSecondaryAction" Visible="False">Cancel</asp:LinkButton></li>
    <li>
        <asp:LinkButton ID="lbDelete" runat="server" CssClass="dnnSecondaryAction" 
            onclientclick="return confirm('Click OK to delete this script.');">Delete</asp:LinkButton></li>
</ul>

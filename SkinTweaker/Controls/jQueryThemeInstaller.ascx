<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="jQueryThemeInstaller.ascx.vb"
    Inherits="SkinTweaker.jQueryThemeInstaller" %>
<div id="ThemeInstaller" class="StandardContainerWrapper">
    <div class="StandardContainerWrapperContents">
       <h1>
            jQuery Theme Installer
        </h1>
        <hr />
        <ul id="jQueryThemeInstaller">
            <li class="Control FileUpload1">
                <asp:Label runat="server" ID="lblFileUpload1" Text="jQuery theme zip file:"></asp:Label>
                <asp:FileUpload ID="FileUpload1" runat="server" /></li>
            <li class="Control chkOverWriteExistingTheme">
                <asp:CheckBox ID="chkOverWriteExistingTheme" runat="server" Text="Overwrite Existing Theme" /></li>
        </ul>
        <ul class="dnnActions dnnClear">
            <li class="Control btnInstallNow">
                <asp:LinkButton ID="lbInstallNow" runat="server" CssClass="dnnPrimaryAction">Install Now</asp:LinkButton>
            </li>
        </ul>
    </div>
</div>

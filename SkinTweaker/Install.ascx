<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Install.ascx.vb" Inherits="SkinTweaker.Install" %>
<%@ Register Src="Controls/jQueryThemeInstaller.ascx" TagName="jQueryThemeInstaller"
    TagPrefix="uc1" %>
<div class="ST_PageWrapper">
    <div class="ST_PageContents">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
            <asp:View ID="vwDemoMode" runat="server">
                <h1>
                    Installing A New Theme - Not Available in Demo Mode</h1>
            </asp:View>
            <asp:View ID="vwNormal" runat="server">
                <h1>
                    Installing A New Theme</h1>
                <p>
                    Now your download should be complete. The theme that you have just downloaded will
                    be in the form of a zip file. Use the Automated Installation Tool below to find
                    and install the zip file that you just downloaded.
                </p>
                <h2>
                    Automated Installation Tool</h2>
                <p>
                    Use this to browser for the new theme zip file and install it. Please note that
                    this feature may not work on some hosting services. If you are not able to install
                    the new theme using this tool, then you can do it manually. See the section below
                    for instructions on how to install a new theme manually.
                </p>
                <p>
                    First, browse for the zip file. Second click the 'Install Now' button. Third be
                    patient and watch for a message box at the top of the page. This may take several
                    seconds to complete.</p>
                <p>
                    &nbsp;</p>
                <uc1:jQueryThemeInstaller ID="jQueryThemeInstaller1" runat="server" />
                <p>
                    If the install is a success, proceed to step 3. Select.
                </p>
                <hr />
                <p>
                    &nbsp;</p>
                <h2>
                    Manual Installation</h2>
                <p>
                    To manually install, you must know 2 things. One, the location of the zip file that
                    you just downloaded. And two, where the SkinTweaker Themes directory is located.
                    Finding the zip file is up to you. The SkinTweaker Themes directory will be located
                    in your DotNetNuke root directory under \DesktopModules\SkinTweaker\Themes\css\.
                    Once you figure these two things out, keep reading.
                </p>
                <p>
                    Open the zip and look at what is inside. There should be a folder called 'css'.
                    Inside 'css' you will find a folder with the same name as your theme. You need to
                    copy this folder (the one named after your theme, not 'css') to the SkinTweaker
                    Themes directory (~\DesktopModules\SkinTweaker\Themes\css\).
                </p>
                <a name="#ClearCache"></a>
                <h2>
                    Clearing the Cache</h2>
                <p>
                    When you install a new theme, you will usually have to clear the cache. If not,
                    the new theme may not show up in the theme selection drop down list. Click 'Tools'
                    to see the clear cache command shown in the screenshot below. The screenshot below
                    is from DotNetNuke version 6.</p>
                <img src="DesktopModules/SkinTweaker/images/ClearCacheScreenShot.jpg" />
                <p>
                    &nbsp;</p>
                <p>
                    That's it. Now you can move on to step 3. Select.</p>
                <p>
                    &nbsp;</p>
            </asp:View>
        </asp:MultiView>
    </div>
</div>

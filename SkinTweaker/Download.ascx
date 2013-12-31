<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Download.ascx.vb" Inherits="SkinTweaker.Download" %>
<div class="ST_PageWrapper">
    <div class="ST_PageContents">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
            <asp:View ID="vwDemoMode" runat="server">
                <h1>
                    Download - Not Available in Demo Mode</h1>
            </asp:View>
            <asp:View ID="vwNormal" runat="server">
                <h1>
                    Download</h1>
                <p>
                    To download a new theme you must go to the <a href="http://jqueryui.com/themeroller/"
                        target="_blank">jQuery UI ThemeRoller</a> site. You can either download one
                    of their premade themes, or create a custom theme. Either way, once you have made
                    your selection, you need to download the theme. The theme will be contained in a
                    zip file. This zip is what you will install in step number 2.
                </p>
                <h2>
                    Instructions</h2>
                <p>
                    Go to the <a href="http://jqueryui.com/themeroller/" target="_blank">jQuery UI ThemeRoller</a>
                    site and choose or create a Theme.</p>
                <p>
                    Once you have choosen or creaed a theme and are ready to download, you will need
                    to specify the following on the download page:
                </p>
                <strong>Advanced Theme Settings:</strong>
                <ul>
                    <li><strong>CSS Scope: .</strong>SkinTweaker</li>
                    <li><strong>Theme Folder Name:</strong> new-theme-name</li>
                </ul>
                <p>
                    These settings can be found on the download page. As of this writing, the settings
                    are found on the right hand side of the download page. You must click "> Advanced
                    Themes Settings" for the settings to appear under it. Below is a screenshot of the
                    settings.
                </p>
                <img src="DesktopModules/SkinTweaker/images/ThemeRollerSettings.png" />
                <p>
                    &nbsp;</p>
                <p>
                    <strong>Notice the period (.) before "SkinTweaker" above. Your theme will NOT work without
                        the period(.)</strong></p>
                <p>
                    Now countinue with the download. Once the download is complete, continue to step
                    2. Install.
                </p>
                <p>
                    <em>For further assistance with the jQuery ThemeRoller please refer to the help on the
                        jQuery ThemeRoller website.</em>
                </p>
                <p>
                    &nbsp;</p>
            </asp:View>
        </asp:MultiView>
    </div>
</div>

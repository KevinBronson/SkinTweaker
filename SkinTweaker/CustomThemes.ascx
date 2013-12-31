<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CustomThemes.ascx.vb"
    Inherits="SkinTweaker.CustomThemes" %>
<div class="ST_PageWrapper">
    <div class="ST_PageContents">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
            <asp:View ID="vwDemoMode" runat="server">
            <h1>
                    Custom Themes - Not Available in Demo Mode</h1>
            </asp:View>
            <asp:View ID="vwNormal" runat="server">
                <h1>
                    Custom Themes</h1>
                <p>
                    If you need something different than the supplied jQuery themes then you can create
                    a custom one. The basic steps to do this are very simple. First, download a custom
                    theme using the jQuery ThemeRoller. Secondly install the downloaded theme and then
                    select it. That's it.
                </p>
                <p>
                    For details on how to do this, refer to the steps listed in the Navigation Panel
                    to the left. Follow each one in order; 1. Download, 2. Install and 3. Select.
                </p>
            </asp:View>
        </asp:MultiView>
    </div>
</div>

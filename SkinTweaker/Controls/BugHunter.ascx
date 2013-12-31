<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BugHunter.ascx.vb"
    Inherits="SkinTweaker.BugHunter" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<div id="BugHunter" class="StandardContainerWrapper">
    <div class="StandardContainerWrapperContents">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
            <asp:View ID="vwDemoMode" runat="server">
                <h1>
                    Bug Hunter - Not Available in Demo Mode</h1>
            </asp:View>
            <asp:View ID="vwNormal" runat="server">
                <h1>
                    <asp:Label ID="lblTitle" runat="server" Text="Bug Hunter"></asp:Label>
                </h1>
                <hr />
                <div class="BugsList">
                    <ul>
                        <li class="Row">
                            <ul>
                                <li class="Headers Action">Action</li>
                                <li class="Headers ID">ID</li>
                                <li class="Headers Name">Bug Name</li>
                                <li class="Headers Status">Status</li>
                                <li class="Headers Notes">Notes</li>
                            </ul>
                        </li>
                        <li class="Row">
                            <hr />
                        </li>
                        <asp:PlaceHolder ID="phBugs" runat="server"></asp:PlaceHolder>
                        <li class="Row">
                            <hr />
                        </li>
                        <li class="Row">
                            <ul>
                                <li class="Details CreateBackups">
                                    <dnn:Label ID="lblCreateBackups" runat="server" Text="Create Backups" Suffix=":"
                                        HelpText="Automatically backs up a file before making changes to it. The back file will have the same name and location as the original and a '.bak' file extension." />
                                </li>
                                <li class="Details">
                                    <asp:CheckBox ID="chkCreateBackups" runat="server" Checked="True" />
                                </li>
                                <li class="Details">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Need
                                    to kill a bug but can't due to version?
                                    <asp:HyperLink ID="hlMailTo" runat="server" NavigateUrl="mailto:bug.version.issue@skintweaker.com"
                                        ToolTip="If the bug is a problem, send an email.">Report It</asp:HyperLink>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <ul class="dnnActions dnnClear">
                    <li>
                        <asp:LinkButton ID="lbRefresh" CssClass="dnnPrimaryAction" runat="server">Refresh Bugs List</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="lbKillBugs" CssClass="dnnSecondaryAction" runat="server" ToolTip="Applies fixes to all of the bugs listed above."
                            OnClientClick="return confirm('This will make changes to the website files. Each changed file will be appended \nwith a stamp that identifies the change made and when it was done.\nBackups will be created IF you have checked the option to do so.  \n\nClick OK to continue.');">Kill All Bugs</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="lbRestoreBugs" CssClass="dnnSecondaryAction" runat="server" ToolTip="Undoes all fixes to the bugs listed above."
                            OnClientClick="return confirm('This will make changes to the website files. Each changed file will be appended \nwith a stamp that identifies the change made and when it was done.\nBackups will be created IF you have checked the option to do so.  \n\nClick OK to continue.');">Restore All Bugs</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="lbRestoreSystem" CssClass="dnnSecondaryAction" runat="server"
                            ToolTip="Restores all changed files to original versions." OnClientClick="return confirm('This will first backup, and then restore all changed files back to thier original \nstate; that is, when Bug Hunter was first run. Be mindful that any\nchanges you have made to the files since then will also be overwritten. \n\nClick OK to continue.');">RESTORE SYSTEM</asp:LinkButton></li>
                </ul>
            </asp:View>
        </asp:MultiView>
    </div>
</div>

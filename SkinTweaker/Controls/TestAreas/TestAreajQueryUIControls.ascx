<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TestAreajQueryUIControls.ascx.vb"
    Inherits="SkinTweaker.TestAreajQueryUIControls" %>
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
                jQuery UI Controls</h2>
            <p>
                &nbsp;</p>
            <div id="FunctionalityCheck" class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Functionality Check</h3>
                    <script type="text/javascript">
                        $(function () {
                            $("#EffectTest1").click(function () {
                                $("#EffectTest1").effect("pulsate");
                                return false;
                            });
                        });
                    </script>
                    <button id="EffectTest1" class="DemoButton">
                        Pulsate Effect</button>
                    <script type="text/javascript">
	                $(function () {
	                    $("#dialog").dialog({
	                        autoOpen: false,
	                    });

	                    $("#opener").click(function () {
	                        $("#dialog").dialog("open");
	                        return false;
	                    });
	                });
                    </script>
                    <div id="dialog" title="Basic dialog">
                        <p>
                            This is a dialog box.</p>
                    </div>
                    <script type="text/javascript">
                        $(function () {
                            $("#datepicker").datepicker();
                        });
                    </script>
                    <button id="opener" class="DemoButton">
                        Open a Dialog Box</button>
                    &nbsp;Select a Date:&nbsp;<input id="datepicker" type="text" />
                    <script type="text/javascript">
                        $(function () {
                            $("#EffectTest2").click(function () {
                                $("#EffectTest2").hide();
                                return false;
                            });
                        });
                    </script>
                    <button id="EffectTest2" class="DemoButton">
                        Hide Effect</button>
                    <p>
                        &nbsp;</p>
                    <p>
                        TIP: If the dialog box or the calendar look empty, then make sure: 1. a CSS Theme
                        is selected and 2. check the selected jQuery Script's Tweaks (You can view a jQuery
                        Script's Tweaks via the jQuery Script Editor
                        <asp:LinkButton ID="lbjQueryScriptEditor" runat="server" OnClientClick='return setCookie("SkinTweaker_PageViewRequest", "6", "-1");'>
                    jQuery Script Editor</asp:LinkButton>).
                    </p>
                </div>
            </div>
            <div id="StandardControlComparison" class="demoContainer">
                <div class="demoContainerContents">
                    <h3>
                        Control Comparison</h3>
                    <p>
                        Compare the controls below to the
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://jqueryui.com/themeroller/"
                            Target="_blank">jQuery UI Theme Roller</asp:HyperLink>
                        page.</p>
                    <div id="ColumnOne">
                        <h3>
                            Accordion</h3>
                        <script type="text/javascript">
                            $(function () {
                                $("#accordion").accordion();
                            });
                        </script>
                        <div id="accordion">
                            <h3>
                                <a href="#">Section 1</a></h3>
                            <div>
                                <p>
                                    Mauris mauris ante, blandit et, ultrices a, suscipit eget, quam. Integer ut neque.
                                    Vivamus nisi metus, molestie vel, gravida in, condimentum sit amet, nunc. Nam a
                                    nibh. Donec suscipit eros. Nam mi. Proin viverra leo ut odio. Curabitur malesuada.
                                    Vestibulum a velit eu ante scelerisque vulputate.
                                </p>
                            </div>
                            <h3>
                                <a href="#">Section 2</a></h3>
                            <div>
                                <p>
                                    Sed non urna. Donec et ante. Phasellus eu ligula. Vestibulum sit amet purus. Vivamus
                                    hendrerit, dolor at aliquet laoreet, mauris turpis porttitor velit, faucibus interdum
                                    tellus libero ac justo. Vivamus non quam. In suscipit faucibus urna.
                                </p>
                            </div>
                            <h3>
                                <a href="#">Section 3</a></h3>
                            <div>
                                <p>
                                    Nam enim risus, molestie et, porta ac, aliquam ac, risus. Quisque lobortis. Phasellus
                                    pellentesque purus in massa. Aenean in pede. Phasellus ac libero ac tellus pellentesque
                                    semper. Sed ac felis. Sed commodo, magna quis lacinia ornare, quam ante aliquam
                                    nisi, eu iaculis leo purus venenatis dui.
                                </p>
                                <ul>
                                    <li>List item one</li>
                                    <li>List item two</li>
                                    <li>List item three</li>
                                </ul>
                            </div>
                            <h3>
                                <a href="#">Section 4</a></h3>
                            <div>
                                <p>
                                    Cras dictum. Pellentesque habitant morbi tristique senectus et netus et malesuada
                                    fames ac turpis egestas. Vestibulum ante ipsum primis in faucibus orci luctus et
                                    ultrices posuere cubilia Curae; Aenean lacinia mauris vel est.
                                </p>
                                <p>
                                    Suspendisse eu nisl. Nullam ut libero. Integer dignissim consequat lectus. Class
                                    aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.
                                </p>
                            </div>
                        </div>
                        <h3>
                            Tabs</h3>
                        <script type="text/javascript">
                            $(function () {
                                $("#tabs").tabs();
                                $("#tabs").addClass("SkinTweaker")
                            });
                        </script>
                        <div id="tabs">
                            <ul>
                                <li><a href="#tabs-1">Nunc tincidunt</a></li>
                                <li><a href="#tabs-2">Proin dolor</a></li>
                                <li><a href="#tabs-3">Aenean lacinia</a></li>
                            </ul>
                            <div id="tabs-1">
                                <p>
                                    Proin elit arcu, rutrum commodo, vehicula tempus, commodo a, risus. Curabitur nec
                                    arcu. Donec sollicitudin mi sit amet mauris. Nam elementum quam ullamcorper ante.
                                    Etiam aliquet massa et lorem. Mauris dapibus lacus auctor risus. Aenean tempor ullamcorper
                                    leo. Vivamus sed magna quis ligula eleifend adipiscing. Duis orci. Aliquam sodales
                                    tortor vitae ipsum. Aliquam nulla. Duis aliquam molestie erat. Ut et mauris vel
                                    pede varius sollicitudin. Sed ut dolor nec orci tincidunt interdum. Phasellus ipsum.
                                    Nunc tristique tempus lectus.</p>
                            </div>
                            <div id="tabs-2">
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
                            <div id="tabs-3">
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
                        <h3>
                            Dialog</h3>
                        <script type="text/javascript">
            $(function () {
                $("#opener2").button({icons: {primary: "ui-icon-newwin"}});
                $("#opener2").click(function () { $("#dialog").dialog("open"); return false; });
                $("#dialog2").dialog({autoOpen: false,});
            });
                        </script>
                        <div id="opener2">
                            Open Dialog</div>
                        <div id="dialog2" title="Basic dialog">
                            <p>
                                This is a dialog box.</p>
                        </div>
                        <h3>
                            Overlay and Shadow Classes</h3>
                        <p>
                            Shadows may not be supported now; still looking into the matter.</p>
                        <h3>
                            Framework Icons</h3>
                        <script type="text/javascript">
                            $(function () {
                                var icons = ['carat-1-n', 'carat-1-ne', 'carat-1-e', 'carat-1-se', 'carat-1-s', 'carat-1-sw', 'carat-1-w', 'carat-1-nw', 'carat-2-n-s', 'carat-2-e-w', 'triangle-1-n', 'triangle-1-ne', 'triangle-1-e', 'triangle-1-se', 'triangle-1-s', 'triangle-1-sw', 'triangle-1-w', 'triangle-1-nw', 'triangle-2-n-s', 'triangle-2-e-w', 'arrow-1-n', 'arrow-1-ne', 'arrow-1-e', 'arrow-1-se', 'arrow-1-s', 'arrow-1-sw', 'arrow-1-w', 'arrow-1-nw', 'arrow-2-n-s', 'arrow-2-ne-sw', 'arrow-2-e-w', 'arrow-2-se-nw', 'arrowstop-1-n', 'arrowstop-1-e', 'arrowstop-1-s', 'arrowstop-1-w', 'arrowthick-1-n', 'arrowthick-1-ne', 'arrowthick-1-e', 'arrowthick-1-se', 'arrowthick-1-s', 'arrowthick-1-sw', 'arrowthick-1-w', 'arrowthick-1-nw', 'arrowthick-2-n-s', 'arrowthick-2-ne-sw', 'arrowthick-2-e-w', 'arrowthick-2-se-nw', 'arrowthickstop-1-n', 'arrowthickstop-1-e', 'arrowthickstop-1-s', 'arrowthickstop-1-w', 'arrowreturnthick-1-w', 'arrowreturnthick-1-n', 'arrowreturnthick-1-e', 'arrowreturnthick-1-s', 'arrowreturn-1-w', 'arrowreturn-1-n', 'arrowreturn-1-e', 'arrowreturn-1-s', 'arrowrefresh-1-w', 'arrowrefresh-1-n', 'arrowrefresh-1-e', 'arrowrefresh-1-s', 'arrow-4', 'arrow-4-diag', 'extlink', 'newwin', 'refresh', 'shuffle', 'transfer-e-w', 'transferthick-e-w', 'folder-collapsed', 'folder-open', 'document', 'document-b', 'note', 'mail-closed', 'mail-open', 'suitcase', 'comment', 'person', 'print', 'trash', 'locked', 'unlocked', 'bookmark', 'tag', 'home', 'flag', 'calculator', 'cart', 'pencil', 'clock', 'disk', 'calendar', 'zoomin', 'zoomout', 'search', 'wrench', 'gear', 'heart', 'star', 'link', 'cancel', 'plus', 'plusthick', 'minus', 'minusthick', 'close', 'closethick', 'key', 'lightbulb', 'scissors', 'clipboard', 'copy', 'contact', 'image', 'video', 'script', 'alert', 'info', 'notice', 'help', 'check', 'bullet', 'radio-off', 'radio-on', 'pin-w', 'pin-s', 'play', 'pause', 'seek-next', 'seek-prev', 'seek-end', 'seek-first', 'stop', 'eject', 'volume-off', 'volume-on', 'power', 'signal-diag', 'signal', 'battery-0', 'battery-1', 'battery-2', 'battery-3', 'circle-plus', 'circle-minus', 'circle-close', 'circle-triangle-e', 'circle-triangle-s', 'circle-triangle-w', 'circle-triangle-n', 'circle-arrow-e', 'circle-arrow-s', 'circle-arrow-w', 'circle-arrow-n', 'circle-zoomin', 'circle-zoomout', 'circle-check', 'circlesmall-plus', 'circlesmall-minus', 'circlesmall-close', 'squaresmall-plus', 'squaresmall-minus', 'squaresmall-close', 'grip-dotted-vertical', 'grip-dotted-horizontal', 'grip-solid-vertical', 'grip-solid-horizontal', 'gripsmall-diagonal-se', 'grip-diagonal-se'];
                                for (var icon in icons) {
                                    $("ul#icons #button" + [icon]).button({ icons: { primary: "ui-icon-" + icons[icon] }, text: false });
                                }
                            });
                        </script>
                        <ul id="icons">
                            <%=GetButtons()%>
                        </ul>
                    </div>
                    <%-- End ColumnOne --%>
                    <div id="ColumnTwo">
                        <h3>
                            Botton</h3>
                        <script type="text/javascript">
                            $(function () {
                                $(".DemoButton").button();
                            });
                        </script>
                        <button class="DemoButton">
                            A button element</button>
                        <br />
                        <br />
                        <script type="text/javascript">
                            $(function () {
                                $("#radio").buttonset();
                            });
                        </script>
                        <div id="radio">
                            <input type="radio" id="radio1" name="radio" /><label for="radio1">Choice 1</label>
                            <input type="radio" id="radio2" name="radio" checked="checked" /><label for="radio2">Choice
                                2</label>
                            <input type="radio" id="radio3" name="radio" /><label for="radio3">Choice 3</label>
                        </div>
                        <br />
                        <h3>
                            Slider</h3>
                        <script type="text/javascript">
                            $(function () {
                                $("#slider-range").slider({
                                    range: true,
                                    min: 0,
                                    max: 500,
                                    values: [75, 300]
                                })
                            });
                        </script>
                        <div id="slider-range">
                        </div>
                        <br />
                        <h3>
                            Autocomplete</h3>
                        <script type="text/javascript">
                            $(function () {
                                var availableTags = [
			                    "ActionScript",
			                    "AppleScript",
			                    "Asp",
			                    "BASIC",
			                    "C",
			                    "C++",
			                    "Clojure",
			                    "COBOL",
			                    "ColdFusion",
			                    "Erlang",
			                    "Fortran",
			                    "Groovy",
			                    "Haskell",
			                    "Java",
			                    "JavaScript",
			                    "Lisp",
			                    "Perl",
			                    "PHP",
			                    "Python",
			                    "Ruby",
			                    "Scala",
			                    "Scheme"
		                        ];
                                $("#tags").autocomplete({
                                    source: availableTags
                                });
                            });
                        </script>
                        <input id="tags" />
                        <br />
                        <h3>
                            Datepicker</h3>
                        <script type="text/javascript">
                            $(function () {
                                $(".datepicker").datepicker();
                            });
                        </script>
                        <div class="datepicker">
                        </div>
                        <h3>
                            Progressbar</h3>
                        <script type="text/javascript">
                            $(function () {
                                $("#progressbar").progressbar({
                                    value: 37
                                });
                            });
                        </script>
                        <div id="progressbar">
                        </div>
                        <br />
                        <h3>
                            Highlight / Error</h3>
                        <div class="ui-widget">
                            <div class="ui-state-highlight ui-corner-all">
                                <p>
                                    <span style="float: left; margin-right: .3em;" class="ui-icon ui-icon-info"></span>
                                    <b>Info:</b> This is the highlight style.</p>
                            </div>
                        </div>
                        <br />
                        <div class="ui-widget">
                            <div class="ui-state-error ui-corner-all">
                                <p>
                                    <span style="float: left; margin-right: .3em;" class="ui-icon ui-icon-alert"></span>
                                    <b>Alert:</b> This is the Error style.</p>
                            </div>
                        </div>
                    </div>
                    <%-- End ColumnTwo --%>
                    <div style="clear: both;">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

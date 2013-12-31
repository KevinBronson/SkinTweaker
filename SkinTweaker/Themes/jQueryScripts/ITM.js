	$(function () {
$(window).bind("load", function () {$("#ui-datepicker-div").wrap('<div class="SkinTweaker"></div>');});
$(window).bind("load", function () {$(".ui-dialog").not(".dnnFormPopup").wrap('<div class="SkinTweaker"></div>');});
$(window).bind("load", function () {$(".ui-autocomplete").not('[class~="dnnForm"]').wrap('<div class="SkinTweaker"></div>');});
		$(".button").button();
	});
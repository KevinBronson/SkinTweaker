$(function () {
    //$("#Body").addClass("SkinTweakerModule");
    $(".dnnPrimaryAction").button();
    //$(".dnnSecondaryAction").button();
    $(window).bind("load", function () { $(".ui-autocomplete").not('[class~="dnnForm"]').wrap('<div class="SkinTweakerModule"></div>'); });
});
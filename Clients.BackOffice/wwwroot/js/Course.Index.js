$(function () {

    /* Setup remove button in grids */

    $("a[data-modal]").click(function () {
        var targetId = $(this).data("targetid");
        var modal = $(this).data("modal");
        $(modal).data("targetid", targetId)
                .modal({ show: true, backdrop: "static" });
        return false;
    });
    $(".site-modal-remove").each(function (index, element) {
        var destinationUrl = $(element).data("url");
        $(".btn-primary", $(element)).click(function () {
            var targetId = $(element).data("targetid");
            $.post(destinationUrl, { id: targetId, __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() })
                .done(function () { location.reload(); })
                .fail(function () { alert("Error deleting item"); });
        });
    });
});

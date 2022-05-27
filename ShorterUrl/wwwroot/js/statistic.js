$(function () {

    $("[delete-link]").click(function () {
        var itemIdToDelete = $(this).closest("[delete-link]").attr("data-shortId");
        $("#deleteItemModal").data("shortId", itemIdToDelete);
    });

    $("#deleteItemModal").click(function () {
       
        $("#deleteItemModal").modal("hide");

        var shortId = $("#deleteItemModal").data("shortId");
        DeleteLink(shortId);
    });
})


function DeleteLink(shortId) {
    
    $.ajax({
        url: "?handler=DeleteLink",
        type: 'POST',
        data: JSON.stringify(shortId),
        contentType: 'application/json',
        dataType: 'json',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        }
    }).done(function (response) {
        if (response.isSuccess == true) {
            $('#successAlertId').show();
            setTimeout(
                function () {
                    location.reload();
                }, 3000);
        } else {
            $("#errorMessageId").text(response.message);
            $('#errorAlertId').show();
            setTimeout(
                function () {
                    location.reload();
                }, 3000);
        }
        
    }).fail(function () {
        $("#errorMessageId").text("Something was wrong.");
        
        $('#errorAlertId').show();
        setTimeout(
            function () {
                location.reload();
            }, 3000);
    });
}
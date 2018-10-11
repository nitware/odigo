var toUrl;
var fromUrl;
var form;

$(document).ready(function () {

    $('#submit').click(function () {
        alert(fromUrl);
        $.ajax({
            type: "POST",
            url: fromUrl,
            data: form.serialize(),

            beforeSend: function () {
                $("#busy").show();
            },
            complete: function () {
                $("#busy").hide();
            },
            success: function (result) {
                var jsonResult = $.parseJSON(result);

                if (jsonResult.isDone) {
                    window.location.href = toUrl;
                }
                else {
                    alert(jsonResult.message);
                }
            },
            error: function () {
                alert("Operation failed!");
            }
        });

        return false;
    });



})
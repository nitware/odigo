
var toUrl;
var fromUrl;

$(document).ready(function () {

    $("#btnPay").click(function () {
        //alert("sending request ...");
        //alert(toUrl);
        //alert(fromUrl);
        
        $.ajax({
            type: "POST",
            url: fromUrl,
            data: $("#frmCostImplication").serialize(),

            beforeSend: function () {
                $("#busy").show();
            },
            complete: function () {
                $("#busy").hide();
            },
            success: function (result) {
                var jsonResult = $.parseJSON(result);

                if (jsonResult.isValid) {
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

        //return false;
    });
    
    $("#btnTest").click(function () {
        alert("now its working!!!");
    });






})
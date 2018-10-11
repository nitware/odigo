
var jqXHRData;
var test;

var file_upload_control;
var upload_button;
var file_path_display_textbox;
var upload_progress_div;
var image_control;
var post_method_url;
var default_avatar;

$(document).ready(function () {
    
    var src = image_control.attr('src');
    
    if (src == undefined) {
        image_control.attr('src', default_avatar);
    }
       
    initSimpleFileUpload();

    upload_button.on('click', function () {
        //alert("uploading ..." + post_method_url);
        //alert(file_path_display_textbox.val(this.files[0].name));
        //alert(jqXHRData);

        if (jqXHRData) {
            jqXHRData.submit();
        }
        else {
            alert("No file selected! Please select a file using the browse button before cliking the Upload button")
        }

        return false;
    });

    file_upload_control.on('change', function () {
        file_path_display_textbox.val(this.files[0].name);
    });
       
    function initSimpleFileUpload() {
        'use strict';

        file_upload_control.fileupload({
            url: post_method_url,
            dataType: 'json',

            add: function (e, data) {
                jqXHRData = data
            },
            send: function (e) {
                upload_progress_div.show();
            },
            done: function (event, data) {
                if (data.result.isUploaded) {
                    //alert("success");
                }
                else {
                    //file_path_display_textbox.val("");
                    alert(data.result.message);
                }

                image_control.attr('src', data.result.displayImageUrl);
                upload_progress_div.hide();
            },
            fail: function (event, data) {
                upload_progress_div.hide();

                if (data.files[0].error) {
                    alert(data.files[0].error);
                }
            }
        });



    }


})
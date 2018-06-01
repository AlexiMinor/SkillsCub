// Write your JavaScript code.
var lastDateTime;
function removeRow(id) {
    $('#row-' + id).hide('slow', function () { $(this).remove(); });
};

function removeFileRow(id) {
    $(`[id="row-${id}"]`).eq(0).hide('slow', function () { $(this).remove(); });
};

function submit(id) {
    $.ajax({
        url: '/Request/Submit',
        type: 'POST',
        dataType: 'json',
        data: {
            id: id
        },
        success: function () {
            removeRow(id);
        },
        error: function () {

        }
    });
};

function reject(id) {
    $.ajax({
        url: '/Request/Reject',
        type: 'POST',
        dataType: 'json',
        data: {
            id: id
        },
        success: function () {
            removeRow(id);
        },
        error: function () {

        }
    });
};

function removeFile(filename, exId) {
    $.ajax({
        url: '/File/Remove',
        type: 'POST',
        dataType: 'json',
        data: {
            filename: filename,
            exerciseId: exId
        },
        success: function () {
            removeFileRow(filename);
        },
        error: function () {

        }
    });
};

function insertMessage(recieverId) {
    $.ajax({
        url: '/Message/Insert',
        type: 'POST',
        dataType: 'json',
        data: {
            "messageText": $('#message')[0].value,
            "recieverId": recieverId,
            "courseId": getUrlId
        },
        success: function () {
            $('#message').eq(0).val('');
        },
        error: function () {

        }
    });
}

function displayNewMessages(data) {
    $.each(data,
        function(index, value) {
            if (value.isYour) {
                $('#chat').append(
                    '<li><a href="#"><div class="card-body style-default">' +
                    '<h4 class="comment-title">' +
                    value.messageSender +
                    ' <small>' +
                    value.sendedDateTime +
                    '</small></h4>' +
                    '<p>' +
                    value.messageText +
                    '</p>' +
                    '</div></a></li>');
            } else {
                $('#chat').append(
                    '<li><a href="#"><div class="card-body">' +
                    '<h4 class="comment-title">' +
                    value.messageSender +
                    ' <small>' +
                    value.sendedDateTime +
                    '</small></h4>' +
                    '<p>' +
                    value.messageText +
                    '</p>' +
                    '</div></a></li>');
            }
        });
}

function getUrlId() {

    var url = window.location.pathname;
    return url.substring(url.lastIndexOf('/') + 1);
}

function GetMessages(lastDateTime) {
    if (lastDateTime === undefined) {
        $.ajax({
            url: '/Message/Get',
            type: 'GET',
            dataType: 'json',

            data: {
                "courseId": getUrlId,
            },
            success: function (data) {
                if (data !== undefined) {
                    displayNewMessages(data);
                    lastDateTime = data[data.length - 1].sendedDateTime;
                    setTimeout(GetMessages(lastDateTime), 5000);
                } else {
                    setTimeout(GetMessages(), 5000);
                }
            },
            error: function() {
                setTimeout(GetMessages(), 5000);
            }
        });
    } else {
        $.ajax({
            url: '/Message/Get',
            type: 'GET',
            dataType: 'json',

            data: {
                "courseId": getUrlId,
                "lastMessageTime": lastDateTime
            },
            success: function (data) {
                if (data !== undefined) {
                    displayNewMessages(data);
                    lastDateTime = data[data.length - 1].sendedDateTime;
                    setTimeout(GetMessages(lastDateTime), 5000);
                } else {
                    setTimeout(GetMessages(lastDateTime), 5000);

                }
            },
            error: function () {
                setTimeout(GetMessages(), 5000);
            }
        });
    }
    
}

$(document).ready(function () {
    $('.remove-file-button').click(function () {
        let filename = $(this)[0].id;
        let exId = $('#exerciseId')[0].value;
        removeFile(filename, exId);
    });

    GetMessages(lastDateTime);
});



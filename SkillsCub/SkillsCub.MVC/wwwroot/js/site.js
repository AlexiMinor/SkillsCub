// Write your JavaScript code.

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

$(document).ready(function () {
    $('.remove-file-button').click(function () {
        let filename = $(this)[0].id;
        let exId = $('#exerciseId')[0].value;
        console.log("filename", filename);
        console.log("exId", exId);
        removeFile(filename, exId);

    });
});



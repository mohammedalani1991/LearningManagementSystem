function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Subscribers/GetData",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get();


function BtnSearchRestPageNumber() {
    $('#page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}

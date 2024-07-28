function Get() {    
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionTransaction/GetData",
        dataType: 'html',
        success: function (data) {
            $('#GetData').html(data);
        }
    });
}

function ShowStudentSubscriptionTransactionTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionTransaction/ShowTable/",
        data: { cheque: $('#cheque').val() },
        success: function (data) {
            HideSpinner();
            $('#EditTable .modal-content').html(data);
            $(".td").map(function () {
                table.forEach(element => {
                    if ($(this).html().trim() == element) {
                        $(this).parent().find("input[type='checkbox']").prop('checked', true)
                    }
                }
                );
            })
            $('#EditTable').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetData1(data) {
    $('#GetData').html(data);
}
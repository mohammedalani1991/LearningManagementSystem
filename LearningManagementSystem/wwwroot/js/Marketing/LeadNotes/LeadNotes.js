function Get() {
    $.ajax({
        type: "GET",
        url: "/Marketing/LeadNotes/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}


function ShowLeadNotesTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/LeadNotes/ShowTable/",
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
    $('.modal-backdrop').hide();

}
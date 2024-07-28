function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetType/GetData",
        dataType: "html",
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get()

function ShowEditTable() {
    ShowSpinner(); 
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetType/ShowTable/",
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

function ShowDetailsTargetType(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetType/ShowDetails/",
        data: { id: id},
        success: function (data) {
            HideSpinner();
            $('#DetailsTargetType .modal-content').html(data);
            $('#DetailsTargetType').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteTargetType(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetType/ShowDelete/",
        data: { id: id},
        success: function (data) {
            HideSpinner();
            $('#DeleteTargetType .modal-content').html(data);
            $('#DeleteTargetType').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/Marketing/TargetType/DeleteTargetType/",
        { id: id },
        function (data) {
            if (data !== null) {
                HideSpinner();
                $('#DeleteTargetType').modal("hide");
                toastr.success(SuccesDelete);
                Get()
            } else {
                HideSpinner();
                toastr.error(ErrorDelete);
            }
        });
}



function ShowEditTargetType(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetType/ShowEdit/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#EditTargetType .modal-content').html(data);
            $('#EditTargetType').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function EditForm() {
    let obj = {
        Id: $("#Id").val(),
        TypeId: $("#TypeId").val(),
        StartedFinancialMonthId: $("#StartedFinancialMonthId").val(),
        Year: $("#Year").val(),
    }
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Marketing/TargetType/EditTargetType/",
        data: { targetTypeViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            $('#EditTargetType').modal("hide");
            HideSpinner();
            toastr.success(SuccesEdit);
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorEdit);
        },
    });
}


function GetData(data) {
    $('#main').html(data);
}

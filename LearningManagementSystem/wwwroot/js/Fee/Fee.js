function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Fees/GetData",
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

let arr = [];
function ShowAddFee() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Fees/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#EditFeeDiv').empty();
            $('#CreateFee .modal-content').html(data);
            $('#CreateFee').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Fees/ShowTable/",
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

function ShowDetailsFee(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Fees/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsFee .modal-content').html(data);
            $('#DetailsFee').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteFee(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Fees/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteFee .modal-content').html(data);
            $('#DeleteFee').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/Training/Fees/DeleteFee/",
        { id: id },
        function (data) {
            if (data !== null) {
                $('#DeleteFee').modal("hide");
                HideSpinner();
                $(".modal-backdrop").remove();
                Get();
            } else {
                HideSpinner();
                toastr.error(ErrorMessage);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChanged(langId) {
    ShowEditFee($('#FeeId').val(), langId.value);
}


function ShowEditFee(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Fees/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#CreateFeeDiv').empty();
            $('#EditFee .modal-content').html(data);
            $('#EditFee').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditProgram(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Fees/ShowProgram/",
        data: { FeesId: id },
        success: function (data) {
            HideSpinner();
            $('#EditPrograms .modal-content').html(data);
            $('#EditPrograms').modal("show");
            $.ajax({
                type: "GET",
                url: "/Training/Fees/GetProgramsByFeeId/",
                data: { id: $("#FeesId").val() },
                success: function (data1) {
                    for (var i = 0; i < data1.length; i++) {
                        arr[i] = data1[i].programId;
                        $(".ProgramsId").map(function (y, x) {
                            if ($(".ProgramsId:eq(" + y + ")").val() == data1[i].programId)
                                $(".ProgramsId:eq(" + y + ")").parent().find("input[type='checkbox']").prop('checked', true);
                        })
                    }
                }, error: function (data) {
                    toastr.error(ErrorMessage);
                },
            });
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CheckboxClicked(current) {
    arr.map(function (x) {
        if ($(current).parent().parent().find(".ProgramsId").val() == x)
            arr = arr.filter(e => e !== x)
    })
    if ($(current).is(':checked'))
        arr.push($(current).parent().parent().find(".ProgramsId").val())
}

function AddPrograms() {
    let obj = {
        id: $("#FeesId").val(),
        ProgramId: arr,
    }
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Training/Fees/AddPrograms/",
        data: { feeViewModel : obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            $('#EditPrograms').modal("hide");
            HideSpinner();
            toastr.success(SuccesCreate);
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorCreate);
        },
    });
}

function ChangePage() {
    $.ajax({
        type: "GET",
        url: "/Training/Fees/GetProgramsByFeeId/",
        data: { id: $("#FeesId").val() },
        success: function (data1) {
            setTimeout(function () {
                for (var i = 0; i < data1.length; i++) {
                    $(".ProgramsId").map(function (y, x) {
                        if ($(".ProgramsId:eq(" + y + ")").val() == data1[i].programId)
                            $(".ProgramsId:eq(" + y + ")").parent().find("input[type='checkbox']").prop('checked', true);
                    })
                }
            },100)
        }
    });
}

function GetData1(data) {
    $('#EditProgramsDiv').html(data);
    $.ajax({
        type: "GET",
        url: "/Training/Fees/GetProgramsByFeeId/",
        data: { id: $("#FeesId").val() },
        success: function (data1) {
            for (var i = 0; i < data1.length; i++) {
                arr[i] = data1[i].programId;
                $(".ProgramsId").map(function (y, x) {
                    if ($(".ProgramsId:eq(" + y + ")").val() == data1[i].programId)
                        $(".ProgramsId:eq(" + y + ")").parent().find("input[type='checkbox']").prop('checked', true);
                })
            }
        }
    });
}

function AddFee() {
    $("#Fee-Create").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            Title: "required",
            Description: "required",
            Amount: "required",
        }
    })

    let obj = {
        LanguageId: $("#LanguageId").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        Amount: $("#Amount").val(),
        BranchId: $("#BranchId").val(),
        MainDrawingId: $("#MainDrawingId").val(),
        IsConnectedWithSubscription: $("#IsConnectedWithSubscription").is(":checked"),
        IsMainRating: $("#IsMainRating").is(":checked"),
    }

    if ($("#Fee-Create").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Training/Fees/CreateFee/",
            data: { feeViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $('#CreateFee').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            },
        });
    }
}

function EditFee() {
    $("#Fee-Edit").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            Title: "required",
            Description: "required",
            Amount: "required",
            BranchId: "required",
        }
    })

    let obj = {
        Id: $("#Id").val(),
        LanguageId: $("#LanguageId").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        Amount: $("#Amount").val(),
        BranchId: $("#BranchId").val(),
        MainDrawingId: $("#MainDrawingId").val(),
        IsConnectedWithSubscription: $("#IsConnectedWithSubscription").is(":checked"),
        IsMainRating: $("#IsMainRating").is(":checked"),
    }

    if ($("#Fee-Edit").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Training/Fees/EditFee/",
            data: { feeViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesEdit);
                $('#EditFee').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorEdit);
            },
        });
    }
}

function GetData(data) {
    $('#main').html(data);
}
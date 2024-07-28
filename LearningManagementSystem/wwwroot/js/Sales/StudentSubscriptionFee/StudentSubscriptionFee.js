function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFees/GetData",
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

function ShowStudentSubscriptionFeesTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFees/ShowTable/",
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

function ShowAddStudentSubscriptionFees(studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFees/ShowCreate/",
        data: { id: studentId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionFee .modal-content').html(data);
            $('#StudentSubscriptionFee').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

} function ShowLinkStudentSubscriptionFees(id, languageId) {
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFees/ShowLink/",
        data: { id: id, studentId: $("#StudentId").val(), languageId: languageId },
        success: function (data) {
            $('#StudentSubscriptionFee .modal-content').html(data);
            $('#StudentSubscriptionFee').modal("show");
        }
    });
}

function CreateStudentSubscriptionFee1() {
    var StudentSubscriptionFeeFrm = $('#StudentSubscriptionFee-Create');    
    if ($('#StudentSubscriptionFee-Create').valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionFeeFrm.attr('method'),
            url: StudentSubscriptionFeeFrm.attr('action'),
            data: StudentSubscriptionFeeFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(addMassege);
                    $('#StudentSubscriptionFee').modal("hide");
                    HideSpinner();
                    if (typeof GetStudentSubscriptionFees === "function")
                        GetStudentSubscriptionFees();
                    else
                        Get()
                }
                else{
                    HideSpinner();
                    toastr.error(data.error);
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}
function LinkStudentSubscriptionFee() {
    var StudentSubscriptionFeeFrm = $('#StudentSubscriptionFee-Link');
    if (StudentSubscriptionFeeFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionFeeFrm.attr('method'),
            url: StudentSubscriptionFeeFrm.attr('action'),
            data: StudentSubscriptionFeeFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(addMassege);
                    $('#StudentSubscriptionFee').modal("hide");
                    HideSpinner();
                    if (typeof GetStudentSubscriptionFees === "function")
                        GetStudentSubscriptionFees();
                    else
                        Get()
                }
                else{
                    HideSpinner();
                    toastr.error(data.error);
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowEditStudentSubscriptionFees(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFees/ShowEdit/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionFee .modal-content').html(data);
            $('#StudentSubscriptionFee').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsStudentSubscriptionFees(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFees/ShowDetails/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionFee .modal-content').html(data);
            $('#StudentSubscriptionFee').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteStudentSubscriptionFees(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFees/ShowDelete/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionFee .modal-content').html(data);
            $('#StudentSubscriptionFee').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditStudentSubscriptionFee1() {
    var StudentSubscriptionFeeEditFrm = $('#StudentSubscriptionFee-Edit');
    if ($('#StudentSubscriptionFee-Edit').valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionFeeEditFrm.attr('method'),
            url: StudentSubscriptionFeeEditFrm.attr('action'),
            data: StudentSubscriptionFeeEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(editMassege);
                    $('#StudentSubscriptionFee').modal("hide");
                    HideSpinner();
                    if (typeof GetStudentSubscriptionFees === "function")
                        GetStudentSubscriptionFees();
                    else
                        Get()
                }
                else{
                    HideSpinner();
                    toastr.error(data.error);
                }

            }, error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    }
}

function ConfirmDeleteStudentSubscriptionFee(id) {
    ShowSpinner();
    $.post("/Sales/StudentSubscriptionFees/DeleteStudentSubscriptionFees/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#StudentSubscriptionFee').modal("hide");
                HideSpinner();
                if (typeof GetStudentSubscriptionFees === "function")
                    GetStudentSubscriptionFees();
                else
                    Get()
            } else {
                HideSpinner();
                toastr.error(ErrorMessage);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function UnLinkStudentSubscriptionFees(id) {
    ShowSpinner();
    $.post("/Sales/StudentSubscriptionFees/UnLinkStudentSubscriptionFees/",
        { id: id },
        function (data) {
            HideSpinner();
            if (data !== null) {
                toastr.success(SuccesLink);
                $('#StudentSubscriptionFee').modal("hide");
                if (typeof GetStudentSubscriptionFees === "function")
                    GetStudentSubscriptionFees();
                else
                    Get()
            } else {
                toastr.error(ErrorMessage);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetData1(data) {
    $('#GetData').html(data);
    $('.modal-backdrop').hide();

}
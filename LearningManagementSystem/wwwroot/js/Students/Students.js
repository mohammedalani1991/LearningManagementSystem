function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Students/GetData",
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

function ShowAddStudent(companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Students/ShowCreate/",
        data: { id: companyId },
        success: function (data) {
            HideSpinner();
            $('#CreateStudent .modal-content').html(data);
            $('#CreateStudent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTableStudents() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Students/ShowTable/",
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

var studentCreateFrm = $('#Student-Create');
studentCreateFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: studentCreateFrm.attr('method'),
        url: studentCreateFrm.attr('action'),
        data: studentCreateFrm.serialize(),
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateStudent').modal("hide");
            HideSpinner();
            $(".modal-backdrop").remove();
            setTimeout(
                function () {
                    if ($('#CompanyStudentInner').val() == undefined) {
                        location.reload();
                    }
                    GetStudents();
                }, 1000);
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
            console.log(data);
        },
    });
});

function CloseCreateStudent() {
    $('#CreateStudent').modal("hide");
}


function ShowEditStudent(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Students/ShowEdit/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#EditStudent .modal-content').html(data);
            $('#EditStudent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseEditStudent() {
    $('#EditStudent').modal("hide");
}

function ShowDetailsStudent(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Students/ShowDetails/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#DetailsStudent .modal-content').html(data);
            $('#DetailsStudent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteStudent(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Students/ShowDelete/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#DeleteStudent .modal-content').html(data);
            $('#DeleteStudent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseDeleteStudent() {
    $('#DeleteStudent').modal("hide");
}

var studentFrm = $('#StudentsForm-Edit');
studentFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: studentFrm.attr('method'),
        url: studentFrm.attr('action'),
        data: studentFrm.serialize(),
        success: function (data) {
            toastr.success(editMassege);
            $('#EditStudent').modal("hide");
            HideSpinner();
            $(".modal-backdrop").remove();
            setTimeout(
                function () {
                    if ($('#CompanyStudentInner').val() == undefined) {
                        location.reload();
                    }
                    GetStudents();
                }, 1000);
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function onDdlChanged(langId, companyId) {

    ShowEditStudent($('#StudentId').val(), langId.value, companyId);
}

function ConfirmDelete(id) {
    ShowSpinner(); 
    $.post("/Companies/Students/DeleteStudent/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteStudent').modal("hide");
                HideSpinner();
                $(".modal-backdrop").remove();
                setTimeout(
                    function () {
                        if ($('#CompanyStudentInner').val() == undefined) {
                            location.reload();
                        }
                        GetStudents();
                    }, 1000);
            } else {
                toastr.error(deleteMassegeError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseDetailsStudent() {
    $('#DetailsStudent').modal("hide");
}


function ShowAssignStudent(companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Students/ShowAssignStudent/",
        data: { companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#AssignStudent .modal-content').html(data);
            $('#AssignStudent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseAssignStudent() {
    $('#AssignStudent').modal("hide");
}


function GetData1(data) {
    if (typeof GetNotes === "undefined")
        $('#main').html(data);
    else
        $('#GetData').html(data);
}

function GetProgramData(id) {
    $.ajax({
        type: "GET",
        url: "/Training/Programs/GetProgram/",
        data: { id: $(id).val() },
        success: function (data) {
            $("#SubscriptionPrice").val(data.price)
            $("#InstallmentsNumber").val(data.installmentsNumber)
        }
    });
}

function ChangeCoverageRate(val) {
    if (!$("#SubscriptionPrice").val()) {
        toastr.warning("Please Choose a Program First");
        $("#CoverageValue").val(0)
    }
    else
        $("#CoverageRatio").val((($(val).val() / $("#SubscriptionPrice").val()) * 100).toFixed(2))
}

function ChangeCoverageValue(val) {
    if (!$("#SubscriptionPrice").val()) {
        toastr.warning("Please Choose a Program First");
        $("#CoverageRatio").val(0)
    }
    else
        $("#CoverageValue").val(($("#SubscriptionPrice").val() * ($(val).val()) / 100))
}

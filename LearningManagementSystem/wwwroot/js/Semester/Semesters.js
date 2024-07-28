function Get() {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Semester/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}

Get();

function ShowAddSemester() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Semester/ShowCreate/",
        success: function (data) {
            if (data !== null) {

                $('#CreateSemester .modal-content').html(data);
                $('#CreateSemester').modal("show");
            }
            HideSpinner();

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Semester/ShowTable/",
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

var SemesterFrm = $('#Semester-Create');
SemesterFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: SemesterFrm.attr('method'),
        url: SemesterFrm.attr('action'),
        data: SemesterFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data.success) {
                toastr.success(addMassege);
                $('#CreateSemester').modal("hide");
                HideSpinner();
                Get()
            } else {
                toastr.warning(data.message);
                HideSpinner();
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsSemester(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Semester/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {

                $('#DetailsSemester .modal-content').html(data);
                $('#DetailsSemester').modal("show");
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteSemester(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Semester/ShowDelete/",
        data: { id: id },
        success: function (data) {
            if (data !== null) {

                $('#DeleteSemester .modal-content').html(data);
                $('#DeleteSemester').modal("show");
            }
            HideSpinner();
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/ControlPanel/Semester/DeleteSemester/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteSemester').modal("hide");
                    HideSpinner();
                    Get()
            }
            else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    ShowEditSemester($('#SemesterId').val(), langId.value);
}


var SemesterEditFrm = $('#Semester-Edit');
SemesterEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: SemesterEditFrm.attr('method'),
        url: SemesterEditFrm.attr('action'),
        data: SemesterEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data.success) {
                toastr.success(editMassege);
                $('#EditSemester').modal("hide");
                HideSpinner();
                Get()
            } else {
                toastr.warning(data.message);
                HideSpinner();
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditSemester(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Semester/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {

                $('#EditSemester .modal-content').html(data);
                $('#EditSemester').modal("show");
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData(data) {
    $('#main').html(data);
}
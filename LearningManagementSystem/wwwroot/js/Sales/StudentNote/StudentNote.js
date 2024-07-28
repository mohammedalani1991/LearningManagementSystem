function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/GetData",
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

function ShowAddStudentNote(studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/ShowCreate/",
        data: { id: studentId },
        success: function (data) {
            HideSpinner();
            $('#EditStudentNotesDiv').empty();
            $('#CreateStudentNotes .modal-content').html(data);
            $('#CreateStudentNotes').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowNoteTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/ShowTable/",
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

function CreateNote() {
    var StudentNotesFrm = $('#StudentNote-Create');
    $('#Description').val(tinymce.get("Description").getContent())
    if (StudentNotesFrm.valid()) {
        if ($('#Description').val()) {
            ShowSpinner();
            $.ajax({
                type: StudentNotesFrm.attr('method'),
                url: StudentNotesFrm.attr('action'),
                data: StudentNotesFrm.serialize(),
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(addMassege);
                    $('#CreateStudentNotes').modal("hide");
                    HideSpinner();
                    if (typeof GetStudentNotes === "function")
                        GetStudentNotes()
                    else
                        Get()
                },
                error: function (data) {
                    HideSpinner();
                    toastr.error(addMassegeError);
                },
            });
        }
        else {
            $("#Description-error").show();
            $("#Description-error").html("This field is required.");
        }
    }
}

function ShowEditStudentNote(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/ShowEdit/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#CreateStudentNotesDiv').empty();
            $('#EditStudentNotes .modal-content').html(data);
            $('#EditStudentNotes').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDetailsStudentNote(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/ShowDetails/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#DetailsStudentNotes .modal-content').html(data);
            $('#DetailsStudentNotes').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteStudentNote(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/ShowDelete/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#DeleteStudentNotes .modal-content').html(data);
            $('#DeleteStudentNotes').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditNote() {
    var StudentNotesEditFrm = $('#StudentNote-Edit');
    $('#Description').val(tinymce.get("Description").getContent())
    if (StudentNotesEditFrm.valid()) {
        if ($('#Description').val()) {
            ShowSpinner();
            $.ajax({
                type: StudentNotesEditFrm.attr('method'),
                url: StudentNotesEditFrm.attr('action'),
                data: StudentNotesEditFrm.serialize(),
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(editMassege);
                    $('#EditStudentNotes').modal("hide");
                    HideSpinner();
                    if (typeof GetStudentNotes === "function")
                        GetStudentNotes()
                    else
                        Get()
                },

                error: function (data) {
                    HideSpinner();
                    toastr.error(editMassegeError);
                },
            });
        }
        else {
            $("#Description-error").show();
            $("#Description-error").html("This field is required.");
        }
    }
}

function ConfirmNoteDelete(id) {
    ShowSpinner();
    $.post("/Sales/StudentNote/DeleteStudentNote/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteStudentNotes').modal("hide");
                HideSpinner();
                if (typeof GetStudentNotes === "function")
                    GetStudentNotes();
                else
                    Get()
            }
            else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChangedStudentNote(langId, studentId) {
    ShowEditStudentNote($('#StudentNoteId').val(), langId.value, studentId);
}


function GetData1(data) {
        $('#GetData').html(data);
}


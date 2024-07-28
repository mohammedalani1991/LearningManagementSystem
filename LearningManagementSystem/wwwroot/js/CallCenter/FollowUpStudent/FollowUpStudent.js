function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CallCenter/FollowUpStudent/GetData",
        dataType: 'html',
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
        url: "/CallCenter/FollowUpStudent/ShowTable/",
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

function GetData(data) {
    $("#main").html(data);
}

function ShowNotes(studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/GetData",
        dataType: 'html',
        data: { studentId: studentId },
        success: function (data) {
            HideSpinner();
            $("#StudentNotesDiv").empty();
            $('#StudentNotes .modal-content').html(data);
            $("#NoteTable").hide();
            $('#StudentNotes').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowAddStudentNote(studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/ShowCreate/",
        data: { id: studentId },
        success: function (data) {
            HideSpinner();
            $('#EditNotesDiv').empty();
            $('#AddStudentNotes .modal-content').html(data);
            $('#AddStudentNotes').modal("show");
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
                    toastr.success(SuccesCreate);
                    $('#AddStudentNotes').modal("hide");
                    HideSpinner();
                    ShowNotes($("#studentId").val())
                },
                error: function (data) {
                    HideSpinner();
                    toastr.error(ErrorCreate);
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
            $('#AddStudentNotesDiv').empty();
            $('#EditNotes .modal-content').html(data);
            $('#EditNotes').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDetailsStudentNote(id, langId, studentId) {
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/ShowDetails/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            $('#DetailsStudentNotes .modal-content').html(data);
            $('#DetailsStudentNotes').modal("show");
        }
    });
}

function ShowDeleteStudentNote(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/ShowDelete/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            $('#DeleteStudentNotes .modal-content').html(data);
            $('#DeleteStudentNotes').modal("show");
            HideSpinner();
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
                    toastr.success(SuccesEdit);
                    $('#EditNotes').modal("hide");
                    HideSpinner();
                    ShowNotes($("#studentId").val())
                },

                error: function (data) {
                    HideSpinner();
                    toastr.error(ErrorEdit);
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
                HideSpinner();
                toastr.success(SuccesDelete);
                $('#DeleteStudentNotes').modal("hide");
                ShowNotes($("#studentId").val())
            }
            else {
                toastr.error(ErrorDelete);
                HideSpinner();
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChangedStudentNote(langId, studentId) {
    ShowEditStudentNote($('#StudentNoteId').val(), langId.value, studentId);
}


function ShowDelete(id) {
    $("#FolloUpId").val(id);
    $("#Delete").modal("show");
}

function ShowFinish(id) {
    $("#FolloUpId").val(id);
    $("#Finish").modal("show");
}

function Delete() {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/CallCenter/FollowUpStudent/Delete/",
        data: { id: $("#FolloUpId").val() },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(SuccesDelete);
            $("#Delete").modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorDelete);
        },
    });
}

function Finish() {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/CallCenter/FollowUpStudent/Finish/",
        data: { id: $("#FolloUpId").val() },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(SuccesFinish);
            $("#Finish").modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorFinish);
        },
    });
}


function ChangeTableFollowUpStudent() {
    table = [];
    $(".TableCheckBox").map(function () {
        if ($(this).is(':checked'))
            table.push($(this).parent().parent().find(".td").html().trim())
    })
    $("#table1").val(table);
    $("#Form2").submit();
    $("#EditTable").modal("hide");
}

//Sms//

function SetAll(item) {
    if ($(item).is(':checked'))
        $(".Checkbox").map((i, x) => {
            $(x).prop('checked', true);
            CheckboxClicked($(x), $(x).val())
        })
    else {
        $("#SetAll").prop('checked', false);
        $(".Checkbox").map((i, x) => {
            $(x).prop('checked', false);
            CheckboxClicked($(x), $(x).val())
        })
    }
}

function CheckboxClicked(item, current) {
    arr.map(function (x) {
        if (current == x)
            arr = arr.filter(e => e !== x)
    })
    if ($(item).is(':checked'))
        arr.push(current)

    SetDisable(arr.length)
}

function SetDisable(i) {
    if (i > 0)
        $("#Send_SMS_Btn").prop("disabled", false)
    else
        $("#Send_SMS_Btn").prop("disabled", true)
}

function ShowSendSms() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Global/Sms/GetSmsStudent/",
        success: function (data) {
            HideSpinner();
            $('#Sms .modal-content').html(data);
            $('#Sms').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function SendSms() {
    $("#Send_Sms").validate({
        errorElement: "span",
        rules: {
            SmsMessage: "required",
        }
    })
    let obj = {
        Ids: arr,
        Message: $("#SmsMessage").val(),
        TypeId: typeId,
        Source: smsSource,
        IsExtraMobile: $("#IsExtraMobile").is(":checked"),
    }

    if ($("#Send_Sms").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Global/Sms/SendSms/",
            data: { smsViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $('#Sms').modal("hide");
                HideSpinner();
                SetAll();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            },
        });
    }
}

$("#Sms").on('hidden.bs.modal', function () {
    $("#Send_Sms").trigger("reset")
})

function SetMesaage(data) {
    if(data.value > 0)
        $("#SmsMessage").val($(data).find('option:selected').text());
}
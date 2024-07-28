function Get() {
    $.ajax({
        type: "GET",
        url: "/BankOfQuestion/Question/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}

Get();

function ShowAddQuestion() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/BankOfQuestion/Question/ShowCreate/",
        success: function (data) {
            if (data !== null) {
                $('#CreateQuestion .modal-content').html(data);
                $('#CreateQuestion').modal("show");
            }
            HideSpinner();

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#CreateQuestion').on('hidden.bs.modal', function () {
    $('#CreateQuestionDiv').empty();
})

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/BankOfQuestion/Question/ShowTable/",
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


var QuestionFrm = $('#Question-Create');
QuestionFrm.submit(function (e) {

    var formdata = QuestionFrm.serializeArray();
    var data = {};
    $(formdata).each(function (index, obj) {
        data[obj.name] = obj.value;
    });
    data['OptionList'] = [];
    for (var i = 0; i < $("#QuestionOptions").find(".optionOfQuestion").length; i++) {
        var name = $("#QuestionOptions").find(".optionOfQuestion .option")[i].value;
        var isChecked = $("#QuestionOptions").find(".optionOfQuestion :checkbox")[i].checked;
        data['OptionList'].push({ Name: name, IsCorrect: isChecked });
    }

    e.preventDefault();
    ShowSpinner();

    if (data['OptionList'].every(item => item.IsCorrect === false) && (data["Type"] == 2 || data["Type"] == 3)) {
        HideSpinner();
        toastr.warning(correctAnswerError);
    } else
        $.ajax({
            type: QuestionFrm.attr('method'),
            url: QuestionFrm.attr('action'),
            data: data,
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateQuestion').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
});

function ShowDetailsQuestion(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/BankOfQuestion/Question/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {

                $('#DetailsQuestion .modal-content').html(data);
                $('#DetailsQuestion').modal("show");
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteQuestion(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/BankOfQuestion/Question/ShowDelete/",
        data: { id: id },
        success: function (data) {
            if (data !== null) {

                $('#DeleteQuestion .modal-content').html(data);
                $('#DeleteQuestion').modal("show");
            }
            HideSpinner();
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/BankOfQuestion/Question/DeleteQuestion/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteQuestion').modal("hide");
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
    ShowEditQuestion($('#QuestionId').val(), langId.value);
}


var QuestionEditFrm = $('#Question-Edit');
QuestionEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();

    var formdata = QuestionEditFrm.serializeArray();
    var data = {};
    $(formdata).each(function (index, obj) {
        data[obj.name] = obj.value;
    });

    data['OptionList'] = [];
    for (var i = 0; i < $("#QuestionOptions").find(".optionOfQuestion").length; i++) {
        var name = $("#QuestionOptions").find(".optionOfQuestion .option")[i].value;
        var isChecked = $("#QuestionOptions").find(".optionOfQuestion :checkbox")[i].checked;
        var id = ($("#QuestionOptions").find(".optionOfQuestion .option")[i]).getAttribute('data-id');
        data['OptionList'].push({ Id: id, Name: name, IsCorrect: isChecked });
    }

    if (data['OptionList'].every(item => item.IsCorrect === false) && (data["Type"] == 2 || data["Type"] == 3)) {
        HideSpinner();
        toastr.warning(correctAnswerError);
    } else
        $.ajax({
            type: QuestionEditFrm.attr('method'),
            url: QuestionEditFrm.attr('action'),
            data: data,
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditQuestion').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
});

function ShowEditQuestion(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/BankOfQuestion/Question/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {
                $('#EditQuestion .modal-content').html(data);
                $('#EditQuestion').modal("show");
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#EditQuestion').on('hidden.bs.modal', function () {
    $('#EditQuestionDiv').empty();
})

function GetData(data) {
    $('#main').html(data);
}




function AddOption() {
    $("#QuestionOptions").prepend(option);
}

function DeleteOption(current) {
    debugger;
    var n = $(current).parent().parent().parent().find(".row").length - 2;
    if (n > 0) {
        $(current).parent().parent().remove();
    }
}

function ChangeQuestionTypefunc(item) {
    if (item == 2 || item == 3) {
        $("#QuestionOptionsDiv").show();
        $(".optionOfQuestion").remove();
        $("#QuestionOptions").prepend(option);
        $("#QuestionOptions").prepend(option);
    }
    else {
        $("#QuestionOptionsDiv").hide();
        $(".optionOfQuestion").remove();
    }
}

function ShowQuestionTypefunc(item) {
    if (item != 1) {
        $("#QuestionOptionsDiv").show();

    }
    else {
        $("#QuestionOptionsDiv").hide();
    }
}



function OpenModel() {
    $('#fileModel').modal('toggle');
    $('#fileModel').modal('toggle');
}

$('#fileModel').on('hidden.bs.modal', function (e) {
    $('#file-upload').val('')
})

$('#CreateExcelQuestion').on('hidden.bs.modal', function (e) {
    $('#Questions-Create').trigger("reset");
})

function UpdateFile() {
    ShowSpinner();
    var file = $('#file-upload').prop('files')[0];
    var formData = false;
    if (window.FormData) {
        formData = new FormData();
    }
    formData.append("file", file);
    $.ajax({
        type: "POST",
        url: "/BankOfQuestion/Question/GetExcelData",
        data: formData,
        contentType: false,
        cache: false,
        processData: false,
        success: function (result) {
            if (result) {
                $('#SuccessModel').modal('toggle');
                data = result;
                $('#CreateExcelQuestion').modal('toggle');
            }
            else
                toastr.error(editMassegeError);
            HideSpinner();
        },
        error: function (req, status, error) {
            toastr.error(editMassegeError);
            HideSpinner();
        }
    })
    $('#fileModel').modal('toggle');
    $('#file-upload').val('');
}

function AddQuestions() {
    ShowSpinner();

    let obj = {};
    obj.Data = data;
    obj.LanguageId = $("#LanguageId").val();
    obj.CategoryId = $("#CategoryIdForModal").val();
    obj.CourseId = $("#CourseIdForModal").val();

    $.ajax({
        type: "POST",
        url: "/BankOfQuestion/Question/CreateQuestions",
        data: { questionViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (result) {
            toastr.success(addMassege)
            $('#CreateExcelQuestion').modal('toggle');
            HideSpinner();
            Get();
        },
        error: function (req, status, error) {
            toastr.error(addMassegeError);
            HideSpinner();
        }
    })
}



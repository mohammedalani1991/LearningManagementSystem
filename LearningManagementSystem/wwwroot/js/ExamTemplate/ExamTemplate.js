function Get() {
    var URL = "/ControlPanel/ExamTemplate/GetData";
    if ($("#ExamTemplateCurrentUrl").val())
        URL = $("#ExamTemplateCurrentUrl").val();

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: URL,
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

function ShowAddExamTemplate(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ExamTemplate/ShowCreate/",
        data: { languageId: langId },
        success: function (data) {
            HideSpinner();
            $("#EditExamTemplateDiv").empty();
            $('#CreateExamTemplate .modal-content').html(data);
            $('#CreateExamTemplate').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}
function onDdlChangedForCreate(langId) {
    ShowAddExamTemplate(langId.value);
}
function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ExamTemplate/ShowTable/",
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

var ExamTemplateFrm = $('#ExamTemplate-Create');
ExamTemplateFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ExamTemplateFrm.attr('method'),
        url: ExamTemplateFrm.attr('action'),
        data: ExamTemplateFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateExamTemplate').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsExamTemplate(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ExamTemplate/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsExamTemplate .modal-content').html(data);
            $('#DetailsExamTemplate').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}




function ShowDeleteExamTemplate(id, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ExamTemplate/ShowDelete/",
        data: { id: id, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteExamTemplate .modal-content').html(data);
            $('#DeleteExamTemplate').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/ControlPanel/ExamTemplate/DeleteExamTemplate/",
        { id: id, page: page},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteExamTemplate').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditExamTemplate($('#ExamTemplateId').val(), langId.value,page);
}


var ExamTemplateEditFrm = $('#ExamTemplate-Edit');
ExamTemplateEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ExamTemplateEditFrm.attr('method'),
        url: ExamTemplateEditFrm.attr('action'),
        data: ExamTemplateEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditExamTemplate').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditExamTemplate(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ExamTemplate/ShowEdit/",
        data: { id: id, languageId: langId, page:page },
        success: function (data) {
            $("#CreateExamTemplateDiv").empty();
            $('#EditExamTemplate .modal-content').html(data);
            $('#EditExamTemplate').modal("show");
        }

    });
}
function BtnSearchRestPageNumber() {
    $('#page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}

function ShowImageView(ImageUrl) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ExamTemplate/ShowImage/",
        data: { ImageUrl: ImageUrl },
        success: function (data) {
            HideSpinner();
            $('#ShowImageExamTemplate .modal-content').html(data);
            $('#ShowImageExamTemplate').modal("show");
        }

    });
}

function ShowAddRemoveExamQuestions(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ExamQuestion/ShowAddRemoveExamQuestions/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#AddRemoveTemplateExamQuestions .modal-content').html(data);
            $('#AddRemoveTemplateExamQuestions').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowExamQuestions(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ExamQuestion/ShowExamQuestions/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#ShowExamQuestions .modal-content').html(data);
            $('#ShowExamQuestions').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
$("#AddRemoveTemplateExamQuestions").on('show.bs.modal', function () {
    $('#AddFromQuestionBank button[type="submit"]').click();

});

$("#AddRemoveTemplateExamQuestions").on('hide.bs.modal', function () {
    $('#CreateExamTemplateDiv').empty();
    $('#EditExamTemplateDiv').empty();

    setTimeout(function () {
        $('#AddRemoveTemplateExamQuestionsDiv').empty();
        $('#ShowExamQuestionsDiv').empty();
    }, 500);
});


$("#ShowExamQuestions").on('hide.bs.modal', function () {
    $('#CreateExamTemplateDiv').empty();
    $('#EditExamTemplateDiv').empty();

    setTimeout(function () {
        $('#AddRemoveTemplateExamQuestionsDiv').empty();
        $('#ShowExamQuestionsDiv').empty();
    }, 500);
});

$("#main").on('show.bs.dropdown', function () {
    $('.table-responsive').css("overflow", "inherit");
});

$('#main').on('hide.bs.dropdown', function () {
    $('.table-responsive').css("overflow", "auto");
})



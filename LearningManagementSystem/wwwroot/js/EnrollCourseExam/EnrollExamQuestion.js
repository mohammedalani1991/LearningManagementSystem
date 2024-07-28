

function ShowEditQuestion(QuestionID,langId, EnrollCourseExamId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollExamQuestion/ShowEdit/",
        data: { id: QuestionID,languageId: langId, EnrollCourseExamId: EnrollCourseExamId },
        success: function (data) {
            HideSpinner();
            $('#ShowAddQuestions .modal-content').html(data);
            $('#ShowAddQuestions').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowAddQuestions(langId, EnrollCourseExamId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollExamQuestion/ShowCreate/",
        data: { languageId: langId, EnrollCourseExamId: EnrollCourseExamId },
        success: function (data) {
            HideSpinner();
            $('#ShowAddQuestions .modal-content').html(data);
            $('#ShowAddQuestions').modal("show");

      

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}




$("#ShowAddQuestions").on('show.bs.modal', function () {
    $('#BankExamQuestionsGetData').addClass("CustmModle");
});






function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}



var QuestionFrm = $('#Question-Create');
QuestionFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    var ViewModleEnrollCourseExamId = $("#ViewModleEnrollCourseExamId").val();
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
    data['ViewModleEnrollCourseExamId'] = ViewModleEnrollCourseExamId;
    ShowSpinner();
    $.ajax({
        type: QuestionFrm.attr('method'),
        url: QuestionFrm.attr('action'),
        data: data ,
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "Ok") {
                toastr.success(addMassege);
                $('#ShowAddQuestions').modal("hide");
                HideSpinner();
            } else {
                HideSpinner();
                toastr.error(editMassegeErrorQuestions);
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});



function onDdlChanged(langId) {
    ShowEditQuestion($('#QuestionId').val(), langId.value);
}


var QuestionEditFrm = $('#Question-Edit');
QuestionEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();

    var ViewModleEnrollCourseExamId = $("#ViewModleEnrollCourseExamId").val();
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
    data['ViewModleEnrollCourseExamId'] = ViewModleEnrollCourseExamId;

    $.ajax({
        type: QuestionEditFrm.attr('method'),
        url: QuestionEditFrm.attr('action'),
        data: data ,
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "Ok") {
                toastr.success(editMassege);
                $('#ShowAddQuestions').modal("hide");
                HideSpinner();
            } else {
                HideSpinner();
                toastr.error(editMassegeErrorQuestions);
            }

        
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});





function AddOption() {
    $("#QuestionOptions").prepend(option);
}

function DeleteOption(current) {

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









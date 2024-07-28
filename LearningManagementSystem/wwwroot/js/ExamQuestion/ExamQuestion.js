
var Form_TemplateAddBankExamQuestions = $('#Form_TemplateAddBankExamQuestions');
Form_TemplateAddBankExamQuestions.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: Form_TemplateAddBankExamQuestions.attr('method'),
        url: Form_TemplateAddBankExamQuestions.attr('action'),
        data: Form_TemplateAddBankExamQuestions.serialize(),
        success: function (data) {
            HideSpinner();
            $('#AddRemoveTemplateExamQuestions #AddFromQuestionBank #TemplateBankExamQuestionsGetData').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
});


var Form_TemplateAddRandomQuestion = $('#Form_TemplateAddRandomQuestion');
Form_TemplateAddRandomQuestion.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    var serializeData = Form_TemplateAddRandomQuestion.serialize();
    $.ajax({
        type: Form_TemplateAddRandomQuestion.attr('method'),
        url: Form_TemplateAddRandomQuestion.attr('action'),
        data: serializeData,
        success: function (data) {
            HideSpinner();
            if (data == "No Data") {
                toastr.error(editMassegeError);

            } else if (data == "Fail") {
                toastr.error(editMassegeError);
            } else {
                toastr.success(editMassege);
                GetAddedRandomQuestions(serializeData);
            }
         
        },
        error: function (data) {
            toastr.error(editMassegeError);
            HideSpinner();
        },
    });
});
function GetAddedRandomQuestions(serializeData) {
    ShowSpinner();
    $.ajax({
        type: "Post",
        url: "/ControlPanel/ExamQuestion/TemplateRandomExamQuestionsGetData/",
        data: serializeData,
        success: function (data) {
            HideSpinner();
            $('#AddRemoveTemplateExamQuestions #AddRandomQuestion #TemplateRandomExamQuestionsGetData').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}


$("#TemplateBankExamQuestionsGetData,#TemplateRandomExamQuestionsGetData").off('click').on("click", "[attr-tamplate-id]", function () {
    ShowSpinner();
    var Tamplateid = $(this).attr("attr-tamplate-id");
    var Questionid = $(this).attr("attr-Question-id");
    var checkedRow = $(this).is(":checked");
    $.ajax({
        type: "Post",
        url:"/ControlPanel/ExamQuestion/AddRemoveTemplateBankExamQuestions/",
        data: { Tamplateid: Tamplateid, Questionid: Questionid, checkedRow: checkedRow },
        success: function (data) {
            if (data) {
                toastr.success(editMassege);
                $(".NumberSelectedQuestions").text(data);
                HideSpinner();
            } else {
                HideSpinner();
                toastr.error(editMassegeError);
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });

});

$("#Form_TemplateAddRandomQuestion").off('click').on("click", "#btnDeleteAllQuestions", function () {
    ShowSpinner();
    var Tamplateid = $("#Form_TemplateAddRandomQuestion #id").val();
    $.ajax({
        type: "Post",
        url: "/ControlPanel/ExamQuestion/DeleteTemplateBankExamQuestions/",
        data: { Tamplateid: Tamplateid},
        success: function (data) {
            if (data == "Success") {
                toastr.success(editMassege);
                $(".AddRandomQuestion").click();
                HideSpinner();

            } else {
                HideSpinner();
                toastr.error(editMassegeError);
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });

});

$("#Form_TemplateAddBankExamQuestions").off('click').on("click", "#btnDeleteAllQuestions", function () {
    ShowSpinner();
    var Tamplateid = $("#Form_TemplateAddBankExamQuestions #id").val();
    $.ajax({
        type: "Post",
        url: "/ControlPanel/ExamQuestion/DeleteTemplateBankExamQuestions/",
        data: { Tamplateid: Tamplateid },
        success: function (data) {
            if (data == "Success") {
                toastr.success(editMassege);
                $(".AddFromQuestionBank").click();
                HideSpinner();

            } else {
                HideSpinner();
                toastr.error(editMassegeError);
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });

});

$(".QuestionDiv").off('click').on("click", ".QuestionTabs li", function () {
    $(".QuestionTabs li").removeClass("active");
    $(this).addClass("active");
    if ($(this).hasClass("AddRandomQuestion")) {
        GetAddedRandomQuestions(Form_TemplateAddRandomQuestion.serialize());
    } else {
        $('#AddFromQuestionBank button[type="submit"]').click();
    }
});

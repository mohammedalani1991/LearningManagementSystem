function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalExams/GetData",
        dataType: 'html',
        data: { searchText: $("#searchText").val(), page: $("#page").val() },
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get();

function ShowAddPracticalExam() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalExams/Create/",
        success: function (data) {
            HideSpinner();
            $('#CreatePracticalExam .modal-content').html(data);
            $('#CreatePracticalExam').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreatePracticalExam() {
    var PracticalExamFrm = $('#PracticalExam-Create');
    if (PracticalExamFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: PracticalExamFrm.attr('method'),
            url: PracticalExamFrm.attr('action'),
            data: PracticalExamFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreatePracticalExam').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}

function ShowDetailsPracticalExam(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalExams/Details/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetailsPracticalExam .modal-content').html(data);
            $('#DetailsPracticalExam').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function onDdlChanged(langId) {
    ShowEditPracticalExam($('#Id').val(), langId.value);
}


function EditPracticalExam() {
    var PracticalExamEditFrm = $('#PracticalExam-Edit');
    if (PracticalExamEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: PracticalExamEditFrm.attr('method'),
            url: PracticalExamEditFrm.attr('action'),
            data: PracticalExamEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditPracticalExam').modal("hide");
                HideSpinner();
                Get();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    }
};

function ShowEditPracticalExam(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalExams/Edit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditPracticalExam .modal-content').html(data);
            $('#EditPracticalExam').modal("show");
        }

    });
}

function GetData(data) {
    $('#main').html(data);
}


function OpenDelete(id) {
    $("#DeleteId").val(id);
    $("#DeleteModal").modal("show");
}

function Delete() {
    $.ajax({
        type: "POST",
        url: "/ControlPanel/PracticalExams/Delete",
        data: {
            id: $("#DeleteId").val()
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(deleteMassege);
            Get()
            $("#DeleteModal").modal("hide");
        },
        error: function (req, status, error) {
            toastr.error(deleteMassegeError);
        }
    });
}

function ShowAddQuestions(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalExams/GetQuestions",
        dataType: 'html',
        data: { PracticalExamId: id, searchText1: $("#searchText1").val(), page1: $("#page1").val() },
        success: function (data) {
            HideSpinner();
            $('#AddQuestions .modal-content').html(data);
            $('#AddQuestions').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData1(data) {
    $('#AddQuestions .modal-content').html(data);
}

function ChangePagenation1() {
    $("#pagination1").val($("#pagin1").val());
    $("#Form2").submit();
}
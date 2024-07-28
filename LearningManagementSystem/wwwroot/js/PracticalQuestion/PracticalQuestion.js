function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalQuestions/GetData",
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

function ShowAddPracticalQuestion() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalQuestions/Create/",
        success: function (data) {
            HideSpinner();
            $('#CreatePracticalQuestion .modal-content').html(data);
            $('#CreatePracticalQuestion').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreatePracticalQuestion() {
    var PracticalQuestionFrm = $('#PracticalQuestion-Create');
    if (PracticalQuestionFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: PracticalQuestionFrm.attr('method'),
            url: PracticalQuestionFrm.attr('action'),
            data: PracticalQuestionFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreatePracticalQuestion').modal("hide");
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

function ShowDetailsPracticalQuestion(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalQuestions/Details/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetailsPracticalQuestion .modal-content').html(data);
            $('#DetailsPracticalQuestion').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function onDdlChanged(langId) {
    ShowEditPracticalQuestion($('#Id').val(), langId.value);
}

function EditPracticalQuestion() {
    var PracticalQuestionEditFrm = $('#PracticalQuestion-Edit');
    if (PracticalQuestionEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: PracticalQuestionEditFrm.attr('method'),
            url: PracticalQuestionEditFrm.attr('action'),
            data: PracticalQuestionEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditPracticalQuestion').modal("hide");
                HideSpinner();
                Get();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    }
}

function ShowEditPracticalQuestion(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalQuestions/Edit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditPracticalQuestion .modal-content').html(data);
            $('#EditPracticalQuestion').modal("show");
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
        url: "/ControlPanel/PracticalQuestions/Delete",
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
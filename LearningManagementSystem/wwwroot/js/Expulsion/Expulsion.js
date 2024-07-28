function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Expulsion/GetData",
        dataType: 'html',
        data: { searchText: $("#searchText").val(), page: $("#page").val()},
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get();

function ShowAddExpulsion() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Expulsion/Create/",
        success: function (data) {
            HideSpinner();
            $('#CreateExpulsion .modal-content').html(data);
            $('#CreateExpulsion').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

var ExpulsionFrm = $('#Expulsion-Create');
ExpulsionFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ExpulsionFrm.attr('method'),
        url: ExpulsionFrm.attr('action'),
        data: ExpulsionFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateExpulsion').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsExpulsion(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Expulsion/Details/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetailsExpulsion .modal-content').html(data);
            $('#DetailsExpulsion').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function onDdlChanged(langId) {
    ShowEditExpulsion($('#Id').val(), langId.value);
}


var ExpulsionEditFrm = $('#Expulsion-Edit');
ExpulsionEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ExpulsionEditFrm.attr('method'),
        url: ExpulsionEditFrm.attr('action'),
        data: ExpulsionEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditExpulsion').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditExpulsion(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Expulsion/Edit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditExpulsion .modal-content').html(data);
            $('#EditExpulsion').modal("show");
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
        url: "/ControlPanel/Expulsion/Delete",
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

function ShowStudents(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Expulsion/ShowStudents/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#StudensModal .modal-content').html(data);
            $('#StudensModal').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowCancelExpulsion(id) {
    $("#CancelExpelStudentId").val(id);
    $('#StudensModal').modal("hide");
    $("#CancelExpulsionModal").modal('show');
}

function CancelExpulsion() {
    $.ajax({
        type: "POST",
        url: "/ControlPanel/EnrollmentCourse/CancelExpulsion",
        data: {
            EnrollStudentCourseId: $("#CancelExpelStudentId").val()
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(CancelExpelMassege);
            $("#CancelExpulsionModal").modal("hide");
        },
        error: function (req, status, error) {
            toastr.error(CancelExpelMassegeError);
        }
    });
}

$('#CancelExpulsionModal').on('hidden.bs.modal', function () {
    ShowStudents($("#ExpulsionId").val());
})
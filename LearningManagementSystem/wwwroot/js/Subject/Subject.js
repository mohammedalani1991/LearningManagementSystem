function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Subject/GetData",
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

function ShowAddSubject() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Subject/Create/",
        success: function (data) {
            HideSpinner();
            $('#CreateSubject .modal-content').html(data);
            $('#CreateSubject').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

var SubjectFrm = $('#Subject-Create');
SubjectFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: SubjectFrm.attr('method'),
        url: SubjectFrm.attr('action'),
        data: SubjectFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateSubject').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsSubject(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Subject/Details/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetailsSubject .modal-content').html(data);
            $('#DetailsSubject').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function onDdlChanged(langId) {
    ShowEditSubject($('#Id').val(), langId.value);
}


var SubjectEditFrm = $('#Subject-Edit');
SubjectEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: SubjectEditFrm.attr('method'),
        url: SubjectEditFrm.attr('action'),
        data: SubjectEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditSubject').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditSubject(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Subject/Edit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditSubject .modal-content').html(data);
            $('#EditSubject').modal("show");
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
        url: "/ControlPanel/Subject/Delete",
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
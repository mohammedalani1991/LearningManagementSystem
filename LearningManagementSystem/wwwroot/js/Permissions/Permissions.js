function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Permissions/GetData",
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

function ShowAddPermissions() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Permissions/Create/",
        success: function (data) {
            HideSpinner();
            $('#CreatePermissions .modal-content').html(data);
            $('#CreatePermissions').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

var PermissionsFrm = $('#Permissions-Create');
PermissionsFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: PermissionsFrm.attr('method'),
        url: PermissionsFrm.attr('action'),
        data: PermissionsFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreatePermissions').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsPermissions(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Permissions/Details/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsPermissions .modal-content').html(data);
            $('#DetailsPermissions').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditPermissions($('#PermissionsId').val(), langId.value,page);
}


var PermissionsEditFrm = $('#Permissions-Edit');
PermissionsEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: PermissionsEditFrm.attr('method'),
        url: PermissionsEditFrm.attr('action'),
        data: PermissionsEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditPermissions').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditPermissions(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Permissions/Edit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditPermissions .modal-content').html(data);
            $('#EditPermissions').modal("show");
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
        url: "/ControlPanel/Permissions/Delete",
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
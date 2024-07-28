function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ManagementStandard/GetData",
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

function ShowAddManagementStandard() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ManagementStandard/Create/",
        success: function (data) {
            HideSpinner();
            $('#CreateManagementStandard .modal-content').html(data);
            $('#CreateManagementStandard').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

var ManagementStandardFrm = $('#ManagementStandard-Create');
ManagementStandardFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ManagementStandardFrm.attr('method'),
        url: ManagementStandardFrm.attr('action'),
        data: ManagementStandardFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateManagementStandard').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsManagementStandard(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ManagementStandard/Details/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetailsManagementStandard .modal-content').html(data);
            $('#DetailsManagementStandard').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function onDdlChanged(langId) {
    ShowEditManagementStandard($('#Id').val(), langId.value);
}


var ManagementStandardEditFrm = $('#ManagementStandard-Edit');
ManagementStandardEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ManagementStandardEditFrm.attr('method'),
        url: ManagementStandardEditFrm.attr('action'),
        data: ManagementStandardEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditManagementStandard').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditManagementStandard(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/ManagementStandard/Edit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditManagementStandard .modal-content').html(data);
            $('#EditManagementStandard').modal("show");
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
        url: "/ControlPanel/ManagementStandard/Delete",
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
function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/AcademicSupervisionStandard/GetData",
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

function ShowAddAcademicSupervisionStandard() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/AcademicSupervisionStandard/Create/",
        success: function (data) {
            HideSpinner();
            $('#CreateAcademicSupervisionStandard .modal-content').html(data);
            $('#CreateAcademicSupervisionStandard').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

var AcademicSupervisionStandardFrm = $('#AcademicSupervisionStandard-Create');
AcademicSupervisionStandardFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: AcademicSupervisionStandardFrm.attr('method'),
        url: AcademicSupervisionStandardFrm.attr('action'),
        data: AcademicSupervisionStandardFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateAcademicSupervisionStandard').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsAcademicSupervisionStandard(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/AcademicSupervisionStandard/Details/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetailsAcademicSupervisionStandard .modal-content').html(data);
            $('#DetailsAcademicSupervisionStandard').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function onDdlChanged(langId) {
    ShowEditAcademicSupervisionStandard($('#Id').val(), langId.value);
}


function Edit() {
    var AcademicSupervisionStandardEditFrm = $('#AcademicSupervisionStandard-Edit');
    ShowSpinner();
    $.ajax({
        type: AcademicSupervisionStandardEditFrm.attr('method'),
        url: AcademicSupervisionStandardEditFrm.attr('action'),
        data: AcademicSupervisionStandardEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditAcademicSupervisionStandard').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
}

function ShowEditAcademicSupervisionStandard(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/AcademicSupervisionStandard/Edit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditAcademicSupervisionStandard .modal-content').html(data);
            $('#EditAcademicSupervisionStandard').modal("show");
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
        url: "/ControlPanel/AcademicSupervisionStandard/Delete",
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
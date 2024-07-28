function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/TrainerRateMeasure/GetData",
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

function ShowAddTrainerRateMeasure() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/TrainerRateMeasure/Create/",
        success: function (data) {
            HideSpinner();
            $('#CreateTrainerRateMeasure .modal-content').html(data);
            $('#CreateTrainerRateMeasure').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

var TrainerRateMeasureFrm = $('#TrainerRateMeasure-Create');
TrainerRateMeasureFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: TrainerRateMeasureFrm.attr('method'),
        url: TrainerRateMeasureFrm.attr('action'),
        data: TrainerRateMeasureFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateTrainerRateMeasure').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsTrainerRateMeasure(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/TrainerRateMeasure/Details/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetailsTrainerRateMeasure .modal-content').html(data);
            $('#DetailsTrainerRateMeasure').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function onDdlChanged(langId) {
    ShowEditTrainerRateMeasure($('#Id').val(), langId.value);
}


var TrainerRateMeasureEditFrm = $('#TrainerRateMeasure-Edit');
TrainerRateMeasureEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: TrainerRateMeasureEditFrm.attr('method'),
        url: TrainerRateMeasureEditFrm.attr('action'),
        data: TrainerRateMeasureEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditTrainerRateMeasure').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditTrainerRateMeasure(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/TrainerRateMeasure/Edit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditTrainerRateMeasure .modal-content').html(data);
            $('#EditTrainerRateMeasure').modal("show");
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
        url: "/ControlPanel/TrainerRateMeasure/Delete",
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
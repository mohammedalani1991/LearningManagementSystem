function Get() {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Equipments/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}

Get();

function ShowAddEquipments() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Equipments/ShowCreate/",
        success: function (data) {
            if (data !== null) {

                $('#CreateEquipments .modal-content').html(data);
                $('#CreateEquipments').modal("show");
            }
            HideSpinner();

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Equipments/ShowTable/",
        success: function (data) {
            HideSpinner();
            $('#EditTable .modal-content').html(data);
            $(".td").map(function () {
                table.forEach(element => {
                    if ($(this).html().trim() == element) {
                        $(this).parent().find("input[type='checkbox']").prop('checked', true)
                    }
                }
                );
            })
            $('#EditTable').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

var EquipmentsFrm = $('#Equipments-Create');
EquipmentsFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: EquipmentsFrm.attr('method'),
        url: EquipmentsFrm.attr('action'),
        data: EquipmentsFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateEquipments').modal("hide");
            HideSpinner();
                Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsEquipments(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Equipments/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {

                $('#DetailsEquipments .modal-content').html(data);
                $('#DetailsEquipments').modal("show");
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteEquipments(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Equipments/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {

                $('#DeleteEquipments .modal-content').html(data);
                $('#DeleteEquipments').modal("show");
            }
            HideSpinner();
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/ControlPanel/Equipments/DeleteEquipment/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteEquipments').modal("hide");
                    HideSpinner();
                    Get()
            }
            else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    ShowEditEquipments($('#EquipmentsId').val(), langId.value);
}


var EquipmentsEditFrm = $('#Equipments-Edit');
EquipmentsEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: EquipmentsEditFrm.attr('method'),
        url: EquipmentsEditFrm.attr('action'),
        data: EquipmentsEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditEquipments').modal("hide");
            HideSpinner();
                Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditEquipments(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Equipments/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {

                $('#EditEquipments .modal-content').html(data);
                $('#EditEquipments').modal("show");
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData(data) {
    $('#main').html(data);
}
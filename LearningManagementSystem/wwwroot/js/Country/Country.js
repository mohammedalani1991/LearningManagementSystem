function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Country/GetData",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get();

function ShowAddCountry() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Country/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#CreateCountry .modal-content').html(data);
            $('#CreateCountry').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Country/ShowTable/",
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

var CountryFrm = $('#Country-Create');
CountryFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CountryFrm.attr('method'),
        url: CountryFrm.attr('action'),
        data: CountryFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateCountry').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsCountry(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Country/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCountry .modal-content').html(data);
            $('#DetailsCountry').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCountry(id, langId, page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Country/ShowDelete/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            HideSpinner();
            $('#DeleteCountry .modal-content').html(data);
            $('#DeleteCountry').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id, page) {
    ShowSpinner();
    $.post("/ControlPanel/Country/DeleteCountry/",
        { id: id, page: page },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteCountry').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditCountry($('#CountryId').val(), langId.value, page);
}


var CountryEditFrm = $('#Country-Edit');
CountryEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CountryEditFrm.attr('method'),
        url: CountryEditFrm.attr('action'),
        data: CountryEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditCountry').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditCountry(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Country/ShowEdit/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            $('#EditCountry .modal-content').html(data);
            $('#EditCountry').modal("show");
        }

    });
}

function GetData(data) {
    $('#main').html(data);
}

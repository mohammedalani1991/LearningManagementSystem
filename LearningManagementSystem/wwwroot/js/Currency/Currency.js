function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Currency/GetData",
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

function ShowAddCurrency() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Currency/Create/",
        success: function (data) {
            HideSpinner();
            $('#CreateCurrency .modal-content').html(data);
            $('#CreateCurrency').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

var CurrencyFrm = $('#Currency-Create');
CurrencyFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CurrencyFrm.attr('method'),
        url: CurrencyFrm.attr('action'),
        data: CurrencyFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateCurrency').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsCurrency(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Currency/Details/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetailsCurrency .modal-content').html(data);
            $('#DetailsCurrency').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function onDdlChanged(langId) {
    ShowEditCurrency($('#Id').val(), langId.value);
}


var CurrencyEditFrm = $('#Currency-Edit');
CurrencyEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CurrencyEditFrm.attr('method'),
        url: CurrencyEditFrm.attr('action'),
        data: CurrencyEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditCurrency').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditCurrency(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Currency/Edit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditCurrency .modal-content').html(data);
            $('#EditCurrency').modal("show");
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
        url: "/ControlPanel/Currency/Delete",
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
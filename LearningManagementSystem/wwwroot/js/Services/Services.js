function Get() {
    $.ajax({
        type: "GET",
        url: "/Companies/Services/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}

Get();

function ShowEditServiceTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Services/ShowTable/",
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

function ShowAddService(companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Services/ShowCreate/",
        data: { id: companyId },
        success: function (data) {
            HideSpinner();
            $('#EditServicesDiv').empty();
            $('#CreateServices .modal-content').html(data);
            $('#CreateServices').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreateService() {
    var service = $('#Service-Create');
    $('#Description').val(tinymce.get("Description").getContent())
    if (service.valid()) {
        if ($('#Description').val()) {
            $.ajax({
                type: service.attr('method'),
                url: service.attr('action'),
                data: service.serialize(),
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(addMassege);
                    $('#CreateServices').modal("hide");
                    if (typeof GetService === "function")
                        GetService();
                    else
                        Get()
                },
                error: function (data) {
                    toastr.error(addMassegeError);
                },
            });
        }
        else {
            $("#Description-error").show();
            $("#Description-error").html("This field is required.");
        }
    }
}

function ShowEditTable() {
    $.ajax({
        type: "GET",
        url: "/Companies/Services/ShowTable/",
        success: function (data) {
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
    });
}


function ShowEditService(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Services/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#CreateServicesDiv').empty();
            $('#EditServices .modal-content').html(data);
            $('#EditServices').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDetailsService(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Services/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsServices .modal-content').html(data);
            $('#DetailsServices').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseDetailsService() {
    $('#DetailsServices').modal("hide");
}

function ShowDeleteService(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Services/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteServices .modal-content').html(data);
            $('#DeleteServices').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditService() {
    var Service = $('#Services-Edit');
    $('#Description').val(tinymce.get("Description").getContent())
    if (Service.valid()) {
        if ($('#Description').val()) {
            $.ajax({
                type: Service.attr('method'),
                url: Service.attr('action'),
                data: Service.serialize(),
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(editMassege);
                    $('#EditServices').modal("hide");
                    if (typeof GetPayments === "function")
                        GetPayments();
                    else
                        Get()
                },
                error: function (data) {
                    toastr.error(editMassegeError);
                },
            });
        }
        else {
            $("#Description-error").show();
            $("#Description-error").html("This field is required.");
        }
    }
}


function ConfirmDeleteService(id) {
    $.post("/Companies/Services/DeleteService/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteServices').modal("hide");
                if (typeof GetPayments === "function")
                    GetPayments();
                else
                    Get()
            } else {
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged4(langId) {
    ShowEditService($('#ServiceId').val(), langId.value);
}

function GetData1(data) {
    if (typeof GetPayments === "undefined")
        $('#main').html(data);
    else
        $('#GetData').html(data);
}
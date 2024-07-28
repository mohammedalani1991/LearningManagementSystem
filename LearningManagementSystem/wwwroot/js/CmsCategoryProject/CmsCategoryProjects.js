function Get() {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsCategoryProject/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}

Get();

function ShowAddCmsCategoryProject() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsCategoryProject/ShowCreate/",
        success: function (data) {
            if (data !== null) {

                $('#CreateCmsCategoryProject .modal-content').html(data);
                $('#CreateCmsCategoryProject').modal("show");
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
        url: "/ControlPanel/CmsCategoryProject/ShowTable/",
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

var CmsCategoryProjectFrm = $('#CmsCategoryProject-Create');
CmsCategoryProjectFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CmsCategoryProjectFrm.attr('method'),
        url: CmsCategoryProjectFrm.attr('action'),
        data: CmsCategoryProjectFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateCmsCategoryProject').modal("hide");
            HideSpinner();
                Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsCmsCategoryProject(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsCategoryProject/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {

                $('#DetailsCmsCategoryProject .modal-content').html(data);
                $('#DetailsCmsCategoryProject').modal("show");
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteCmsCategoryProject(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsCategoryProject/ShowDelete/",
        data: { id: id },
        success: function (data) {
            if (data !== null) {

                $('#DeleteCmsCategoryProject .modal-content').html(data);
                $('#DeleteCmsCategoryProject').modal("show");
            }
            HideSpinner();
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/ControlPanel/CmsCategoryProject/DeleteCmsCategoryProject/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteCmsCategoryProject').modal("hide");
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
    ShowEditCmsCategoryProject($('#CmsCategoryProjectId').val(), langId.value);
}


var CmsCategoryProjectEditFrm = $('#CmsCategoryProject-Edit');
CmsCategoryProjectEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CmsCategoryProjectEditFrm.attr('method'),
        url: CmsCategoryProjectEditFrm.attr('action'),
        data: CmsCategoryProjectEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditCmsCategoryProject').modal("hide");
            HideSpinner();
                Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditCmsCategoryProject(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsCategoryProject/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {

                $('#EditCmsCategoryProject .modal-content').html(data);
                $('#EditCmsCategoryProject').modal("show");
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
function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Branch/GetData",
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

function ShowAddBranch() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Branch/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#CreateBranch .modal-content').html(data);
            $('#CreateBranch').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Branch/ShowTable/",
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

var BranchFrm = $('#Branch-Create');
BranchFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: BranchFrm.attr('method'),
        url: BranchFrm.attr('action'),
        data: BranchFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateBranch').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsBranch(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Branch/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsBranch .modal-content').html(data);
            $('#DetailsBranch').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteBranch(id, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Branch/ShowDelete/",
        data: { id: id, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteBranch .modal-content').html(data);
            $('#DeleteBranch').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/ControlPanel/Branch/DeleteBranch/",
        { id: id, page: page},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteBranch').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditBranch($('#BranchId').val(), langId.value,page);
}


var BranchEditFrm = $('#Branch-Edit');
BranchEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: BranchEditFrm.attr('method'),
        url: BranchEditFrm.attr('action'),
        data: BranchEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditBranch').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditBranch(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Branch/ShowEdit/",
        data: { id: id, languageId: langId, page:page },
        success: function (data) {
            $('#EditBranch .modal-content').html(data);
            $('#EditBranch').modal("show");
        }

    });
}

function GetData(data) {
    $('#main').html(data);
}

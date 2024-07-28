function Get() {
    var URL = "/ControlPanel/Assignment/GetData";
    if ($("#AssignmentCurrentUrl").val())
        URL = $("#AssignmentCurrentUrl").val();

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: URL,
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

function ShowAddAssignment(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Assignment/ShowCreate/",
        data: { languageId: langId },
        success: function (data) {
            $("#EditAssignmentDiv").empty();
            HideSpinner();
            $('#CreateAssignment .modal-content').html(data);
            $('#CreateAssignment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}
function onDdlChangedForCreate(langId) {
    ShowAddAssignment(langId.value);
}
function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Assignment/ShowTable/",
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

var AssignmentFrm = $('#Assignment-Create');
AssignmentFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: AssignmentFrm.attr('method'),
        url: AssignmentFrm.attr('action'),
        data: AssignmentFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "AddMassegeErrorInvalidDates") {
                toastr.error(AddMassegeErrorInvalidDates);
                HideSpinner();
            } else {
                toastr.success(addMassege);
                $('#CreateAssignment').modal("hide");
                HideSpinner();
                Get()
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsAssignment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Assignment/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsAssignment .modal-content').html(data);
            $('#DetailsAssignment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteAssignment(id, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Assignment/ShowDelete/",
        data: { id: id, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteAssignment .modal-content').html(data);
            $('#DeleteAssignment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/ControlPanel/Assignment/DeleteAssignment/",
        { id: id, page: page},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteAssignment').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditAssignment($('#AssignmentId').val(), langId.value,page);
}


var AssignmentEditFrm = $('#Assignment-Edit');
AssignmentEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: AssignmentEditFrm.attr('method'),
        url: AssignmentEditFrm.attr('action'),
        data: AssignmentEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "EditMassegeErrorInvalidDates") {
                toastr.error(EditMassegeErrorInvalidDates);
                HideSpinner();
            } else {
                toastr.success(editMassege);
                $('#EditAssignment').modal("hide");
                HideSpinner();
                Get();
            }

        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditAssignment(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Assignment/ShowEdit/",
        data: { id: id, languageId: langId, page:page },
        success: function (data) {
            $("#CreateAssignmentDiv").empty();
            $('#EditAssignment .modal-content').html(data);
            $('#EditAssignment').modal("show");
        }

    });
}
function BtnSearchRestPageNumber() {
    $('#page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}

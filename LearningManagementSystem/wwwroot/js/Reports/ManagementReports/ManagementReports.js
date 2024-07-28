$(".tms-nav-link").click(function (event) {
    $(".tms-nav-link").removeClass("active");
    $(".tms-col-nav-item-icon i").removeClass("active");
    $(this).addClass("active");
    $(this).parent().parent().children().eq(1).children().eq(0).addClass("active");
});
function ManagementReportsToggle(){
    if($("#EditCompanyToggle").is(":hidden"))
        return;
    $(".tms-nav-div").toggle();
    $("#myTabContent").toggle();
}

function GetAuditLogs() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/ManagementReports/GetAuditLogs",
        dataType: 'html',
        success: function (data) {
            $('#GetData').html(data);
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetAuditLogDetails(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/ManagementReports/AuditLogDetails",
        dataType: 'html',
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#AuditLogDetails .modal-content').html(data);
            $('#AuditLogDetails').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetAuditLogDelete(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/ManagementReports/AuditLogDelete",
        dataType: 'html',
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DeleteAuditLog .modal-content').html(data);
            $('#DeleteAuditLog').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function DeleteAuditLog() {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Reports/ManagementReports/AuditLogDeleteConfirmed",
        data: { id: $("#Id").val() },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(SuccesDelete);
            $('#DeleteAuditLog').modal("hide");
            HideSpinner();
            GetAuditLogs()
        }, error: function () {
            HideSpinner();
            toastr.error(ErrorDelete);
        }
    });
}

function GetSystemLogs() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/ManagementReports/GetSystemLogs",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetSystemLogsDetails(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/ManagementReports/SystemLogDetails",
        dataType: 'html',
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#SystemLogDetails .modal-content').html(data);
            $('#SystemLogDetails').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetSystemLogsDelete(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/ManagementReports/DeleteSystemLog",
        dataType: 'html',
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DeleteSystemLog .modal-content').html(data);
            $('#DeleteSystemLog').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function DeleteSystemLogs() {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Reports/ManagementReports/SystemLogDeleteConfirmed",
        data: { id: $("#Id").val() },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(SuccesDelete);
            $('#DeleteSystemLog').modal("hide");
            HideSpinner();
            GetSystemLogs()
        }, error: function () {
            HideSpinner();
            toastr.error(ErrorDelete);
        }
    });
}


function GetNotificationsReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/ManagementReports/GetNotificationsReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    });
}


$("#first-tab").click(function () {
    GetAuditLogs()
})

$("#second-tabBranches").click(function () {
    GetSystemLogs()
})

function GetData2(data) {
    $("#GetData").html(data);
}


function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/ManagementReports/ShowTable/",
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
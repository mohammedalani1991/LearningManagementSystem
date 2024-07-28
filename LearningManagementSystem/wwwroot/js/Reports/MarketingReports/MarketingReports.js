$('.tms-reports').addClass("active");
$('.tms-sub-marketing-reports').addClass("active");
$('.tms-sub-top-nav  .tms_sub_reports').addClass("active");
$(".tms-nav-link").click(function (event) {
    $(".tms-nav-link").removeClass("active");
    $(".tms-col-nav-item-icon i").removeClass("active");
    $(this).addClass("active");
    $(this).parent().parent().children().eq(1).children().eq(0).addClass("active");
});
function MarketingReportsToggle(){
    if($("#EditCompanyToggle").is(":hidden"))
        return;
    $(".tms-nav-div").toggle();
    $("#myTabContent").toggle();
}
function GetEnrolledInUniversitiesReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/GetEnrolledInUniversitiesReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ViewCommunicationsChannelsReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/GetCommunicationsChannelsReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ViewSMSReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/GetSMSReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetConsultantsFollowUpReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/GetConsultantsFollowUpReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetTargetingReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/GetTargetingReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetBranchNumbersReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/GetBranchNumbersReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetCallCenterFollowUpReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/GetCallCenterFollowUpReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetAdAnalysisReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/GetAdAnalysisReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetData2(data) {
    $("#GetData").html(data);
}




function ShowEnrolledTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/ShowEnrolledTable/",
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

function ShowCommunicationsChannelsTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/ShowCommunicationsChannelsTable/",
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

function ShowSMSTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/ShowSMSTable/",
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
function ShowConsultantsFollowUpTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/ShowConsultantsFollowUpTable/",
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
function ShowTargetingReportTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/ShowTargetingReportTable/",
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
function ShowBranchNumbersTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/ShowBranchNumbersTable/",
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

function ShowCallCenterFollowupTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/ShowCallCenterFollowupTable",
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

function ShowAdAnalysisTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/MarketingReports/ShowAdAnalysisTable",
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
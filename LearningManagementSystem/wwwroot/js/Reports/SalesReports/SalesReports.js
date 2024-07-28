$('.tms-reports').addClass("active");
$('.tms-sub-sales-reports').addClass("active");
$('.tms-sub-top-nav  .tms_sub_reports').addClass("active");

$(".tms-nav-link").click(function (event) {
    $(".tms-nav-link").removeClass("active");
    $(".tms-col-nav-item-icon i").removeClass("active");
    $(this).addClass("active");
    $(this).parent().parent().children().eq(1).children().eq(0).addClass("active");
});
function SalesReportsToggle(){
    if($("#EditCompanyToggle").is(":hidden"))
        return;
    $(".tms-nav-div").toggle();
    $("#myTabContent").toggle();
}
function GetSalesReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetSalesReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetCancellationsReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetCancellationsReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetInterestsReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetInterestsReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetFetchReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetFetchReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetFeesReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetFeesReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data); 
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetInstallmentsReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetInstallmentsReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetReceivablesReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetReceivablesReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetDiplomaReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetDiplomaReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetProgramsSynthesisReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetProgramsSynthesisReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetProgramsAchievementsReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetProgramsAchievementsReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetCashCollectionReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetCashCollectionReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetCheckCollectionReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetCheckCollectionReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetAggregateNumbersReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/GetAggregateNumbersReport",
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


function ShowSalesTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowSalesTable/",
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

function ShowCancellationsTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowCancellationsTable/",
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

function ShowInterestsTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowInterestsTable/",
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

function ShowFetchTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowFetchTable/",
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
function ShowFeesTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowFeesTable/",
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
function ShowInstallmentsTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowInstallmentsTable/",
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
function ShowReceivablesTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowReceivablesTable/",
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
function ShowDiplomaTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowDiplomaTable/",
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

function ShowProgramsSynthesisTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowProgramsSynthesisTable/",
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
function ShowProgramsAchievementsTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowProgramsAchievementsTable/",
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

function ShowCashCollectionTable(){
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowCashCollectionTable/",
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
function ShowCheckCollectionTable(){
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowCheckCollectionTable/",
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
function ShowAggregateNumbersTable(){
    ShowSpinner();  
    $.ajax({
        type: "GET",
        url: "/Reports/SalesReports/ShowAggregateNumbersTable/",
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




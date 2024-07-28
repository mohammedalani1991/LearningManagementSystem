function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Target/GetData",
        dataType: "html",
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

Get()

function ShowAddTarget() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Target/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#CreateTarget .modal-content').html(data);
            $('#CreateTarget').modal("show");
            GetTargetType();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetTargetType() {
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetType/GetOrCreate/",
        success: function (data1) {
            $("#DateInput").val(data1.input);
            $("#TargetTypeId").val(data1.id);
        },
        error: function () {
            toastr.error(ErrorMessage);
        }
    });
}

function GetInput(id) {
    $.ajax({
        type: "GET",
        url: "/Marketing/Target/GetInput/",
        data: { id: id },
        success: function (data) {
            $("#DateInput1").val(data);
            document.getElementById("Date_input").innerHTML = data
        },
        error: function () {
            toastr.error(ErrorMessage);
        }
    });
}


function AddTarget() {
    let obj = {
        TargetTypeId: $("#TargetTypeId").val(),
        TargetSales: $("#TargetSales").val(),
        TargetCollection: $("#TargetCollection").val(),
        BranchId: $("#BranchId").val(),

    }
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Marketing/Target/CreateTarget/",
        data: { targetViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            $('#CreateTarget').modal("hide");
            HideSpinner();
            toastr.success(SuccesCreate);
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorCreate);
        },
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Target/ShowTable/",
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


function ShowDetailsTarget(id) {
    $.ajax({
        type: "GET",
        url: "/Marketing/Target/ShowDetails/",
        data: { id: id },
        success: function (data) {
            $('#DetailsTarget .modal-content').html(data);
            $('#DetailsTarget').modal("show");
        }
    });
}


function ShowDeleteTarget(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Target/ShowDelete/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DeleteTarget .modal-content').html(data);
            $('#DeleteTarget').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/Marketing/Target/DeleteTarget/",
        { id: id },
        function (data) {
            if (data !== null) {
                $('#DeleteTarget').modal("hide");
                HideSpinner();
                toastr.success(SuccesDelete);
                Get()
            } else {
                HideSpinner();
                toastr.error(ErrorDelete);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowEditTarget(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Target/ShowEdit/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#EditTarget .modal-content').html(data);
            $('#EditTarget').modal("show");
            GetInput(id);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditForm() {
    let obj = {
        Id: $("#TargetId").val(),
        TargetSales: $("#TargetSales").val(),
        TargetCollection: $("#TargetCollection").val(),
        BranchId: $("#BranchId").val(),
    }
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Marketing/Target/EditTarget/",
        data: { targetViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            $('#EditTarget').modal("hide");
            HideSpinner();
            toastr.success(SuccesEdit);
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorEdit);
        },
    });
}

function AddAdvisor(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetAdvisor/GetTargetAdvisors/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#TargetAdvisors .modal-content').html(data);
            $('#TargetAdvisors').modal("show");
            GetAdivsor(id)
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
let arr = [];

function GetAdivsor(id) {
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetAdvisor/GetTargetAdvisor/",
        data: { id: id },
        success: function (data) {
            document.getElementById("Branch").innerHTML = data.branchName;
            document.getElementById("TargetCollectionText").innerHTML = data.targetCollection
            $(".AdvisorId").map(function (y, x) {
                data.ids.map(function (z, e) {
                    arr[e] = data.values[e];
                    if ($(".AdvisorId:eq(" + y + ")").val() == z)
                        $(".AdvisorId:eq(" + y + ")").parent().find(".Values").val(data.values[e]);
                })
            })
        }
    });
}

function GetData1(data) {
    GetAdivsor($("#id").val());
    $('#TargetAdvisorsDiv').html(data);
}

function ChangePagenation3() {
    $("#pagination3").val($("#pagin3").val());
    $("#Form2").submit();
}


function AddAdvisorTarget(page = false) {
    let obj = {
        TargetId: $("#id").val(),
        Values: $.map($('.Values'), function (el) { return el.value; }),
        AdvisorIds: $.map($('.AdvisorId'), function (el) { return el.value; }),
    }
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Marketing/TargetAdvisor/CreateTargetAdvisor/",
        data: { targetAdvisorViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            if (data.isOver)
                toastr.warning(data.message)
            else if (!page) {
                $('#TargetAdvisors').modal("hide");
                toastr.success(SuccesCreate);
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorCreate);
        },
    });
}

function GetData(data) {
    $('#main').html(data);
}

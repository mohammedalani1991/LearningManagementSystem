function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetsProgram/GetData",
        dataType: 'html',
        data: { id: $("#Id").val() },
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get()

function GetData1(data) {
    $('#main').html(data);
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetsProgram/ShowTable/",
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

function ShowAddTargetProgram(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetsProgram/ShowCreate/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#EditTargetProgramDiv').empty();
            $('#CreateTargetProgram .modal-content').html(data);
            $('#CreateTargetProgram').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowDetailsTargetProgram(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetsProgram/ShowDetails/",
        data: { id: id, },
        success: function (data) {
            HideSpinner();
            if (data !== null) {
                $('#DetailsTargetProgram .modal-content').html(data);
                $('#DetailsTargetProgram').modal("show");
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseAddTargetProgram() {
    $('#CreateTargetProgram').modal("hide");
}


function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetsProgram/ShowTable/",
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

function ShowDeleteTargetProgram(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetsProgram/ShowDelete/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DeleteTargetProgram .modal-content').html(data);
            $('#DeleteTargetProgram').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    $.post("/Marketing/TargetsProgram/DeleteTargetProgram/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(SuccesDelete);
                $('#DeleteTargetProgram').modal("hide");
                Get();
            }
            else {
                toastr.error(ErrorDelete);
            }
        });
}


function ShowEditTargetProgram(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetsProgram/ShowEdit/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#CreateTargetProgramDiv').empty();
            $('#EditTargetProgram .modal-content').html(data);
            $('#EditTargetProgram').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function AddTargetProgram() {
    $("#Create_Target_Program").validate({
        errorElement: "span",
        rules: {
            ProgramId: "required",
            ProgramPrice: "required",
            TargetLength: "required",
            TargetSale: "required",
        }
    })
    let obj = {
        ProgramPrice: $("#ProgramPrice").val(),
        TargetLength: $("#TargetLength").val(),
        TargetSale: $("#TargetSale").val(),
        MonthId: $("#MonthId").val(),
        TargetId: $("#Id").val(),
        ProgramId: $("#ProgramId").val(),
        MonthIds: $('#MonthId option').map((i,s) => { return s.value}).get()
    }

    if ($("#Create_Target_Program").valid()) {
        $.ajax({
            type: "POST",
            url: "/Marketing/TargetsProgram/CreateTargetProgram/",
            data: { targetProgramViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $('#CreateTargetProgram').modal("hide");
                Get();
            },
            error: function (data) {
                toastr.error(ErrorCreate);
            },
        });
    }
}

function EditForm() {
    let obj = {
        Id: $("#TargetProgramId").val(),
        ProgramPrice: $("#ProgramPrice").val(),
        TargetLength: $("#TargetLength").val(),
        TargetSale: $("#TargetSale").val(),
        MonthId: $("#MonthId").val(),
        ProgramId: $("#ProgramId").val(),
        TargetId: $("#Id").val(),
    }
    $.ajax({
        type: "POST",
        url: "/Marketing/TargetsProgram/EditTargetProgram/",
        data: { targetProgramViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(SuccesEdit);
            $('#EditTargetProgram').modal("hide");
            Get()
        },
        error: function (data) {
            toastr.error(ErrorEdit);
        },
    });
}

function AddAdvisorProgram(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetProgramAdvisor/GetTargetProgramAdvisors/",
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



function setPrice() {
    var program = document.getElementById('ProgramId')

    $.ajax({
        type: "GET",
        url: "/Training/Programs/GetPrice/" + program.value,
        success: function (data) {
            var price = document.getElementById('ProgramPrice')
            price.value = data.price;
            setTotal();
        },
        error: function () {
            toastr.error(ErrorMessage);
        }
    });
}

function setTotal() {
    var price = document.getElementById('ProgramPrice')
    var quantity = document.getElementById('TargetLength')
    var total = document.getElementById('TargetSale')
    total.value = price.value * quantity.value;
}


let arr = [];
function GetAdivsor(id) {
    $.ajax({
        type: "GET",
        url: "/Marketing/TargetProgramAdvisor/GetTargetProgramAdvisor/",
        data: { id: id },
        success: function (data) {
            console.log(data)
            document.getElementById("TargetLengthText").innerHTML = data.targetLength,
                document.getElementById("ProgramId").innerHTML = data.programName

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


function ChangePagenation3() {
    $("#pagination3").val($("#pagin3").val());
    $("#Form2").submit();
}


function GetData(data) {
    GetAdivsor($("#TargetProgramId").val());
    $('#TargetAdvisorsDiv').html(data);
}

function AddAdvisorTarget(page = false) {
    let obj = {
        TargetProgramId: $("#TargetProgramId").val(),
        Values: $.map($('.Values'), function (el) { return el.value; }),
        AdvisorIds: $.map($('.AdvisorId'), function (el) { return el.value; }),
    }
    $.ajax({
        type: "POST",
        url: "/Marketing/TargetProgramAdvisor/CreateTargetProgramAdvisor/",
        data: { targetProgramAdvisorViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data.isOver)
                toastr.warning(data.message)
            else if (!page) {
                $('#TargetAdvisors').modal("hide");
                toastr.success(SuccesCreate);
            }
        },
        error: function (data) {
            toastr.error(ErrorCreate);
        },
    });
}


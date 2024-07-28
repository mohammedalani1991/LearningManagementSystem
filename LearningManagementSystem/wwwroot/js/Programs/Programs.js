function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Programs/GetData",
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

function ShowAddProgram() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Programs/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#EditProgramDiv').empty();
            $('#CreateProgram .modal-content').html(data);
            $('#CreateProgram').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Programs/ShowTable/",
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



function ShowDeleteProgram(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Programs/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteProgram .modal-content').html(data);
            $('#DeleteProgram').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/Training/Programs/DeleteProgram/",
        { id: id },
        function (data) {
            $('#DeleteProgram').modal("hide");
            HideSpinner();
            toastr.success(SuccesDelete)
            Get()
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChanged(langId) {
    ShowEditProgram($('#ProgramId').val(), langId.value);
}



function ShowEditProgram(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Programs/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#CreateProgramDiv').empty();
            $('#EditProgram .modal-content').html(data);
            $('#EditProgram').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddEquipment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Programs/ShowEquipmentTable/",
        data: { programId: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#EquipmentTable .modal-content').html(data);
            $('#EquipmentTable').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddRow() {
    $("#AddRow").show();
    $("#addIcon").hide();
}

function HideAddRow() {
    $("#AddRow").hide();
    $("#addIcon").show();
}

function CreateProgramEquipment() {
    var obj = {};
    obj.ProgramId = $("#programId").val();
    obj.EquipmentId = $("#Equipment").val();
    obj.Quantity = $("#Quantity").val();
    obj.Description = $("#Description").val();
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Training/Programs/CreateProgramEquipment/",
        data: { programEquipmentViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            toastr.success(SuccesCreate)
            $.ajax({
                type: "GET",
                url: "/Training/Programs/ShowEquipmentTable/",
                data: { programId: data.id },
                success: function (data) {
                    $('#EquipmentTable .modal-content').html(data);
                }
            });
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorCreate)
        },
    });
}

function GetData1(data) {
    $('#EquipmentTable .modal-content').html(data);
}

function Delete(id) {
    $("#delete_item_id").val(id);
    $('#delete_confirmation_modal').modal('show');
}

function HideConfirmationModel() {
    $('#delete_confirmation_modal').modal('hide');
}

function DeleteItem() {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Training/Programs/DeleteProgramEquipment/",
        data: { id: $("#delete_item_id").val() },
        success: function (data) {
            $('.modal-backdrop:eq(1)').remove();
            HideSpinner();
            toastr.success(SuccesDelete)
            $.ajax({
                type: "GET",
                url: "/Training/Programs/ShowEquipmentTable/",
                data: { programId: data.id },
                success: function (data) {
                    $('#EquipmentTable .modal-content').html(data);
                }
            });
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorDelete)
        },
    });
}

function UpdateProgramEquipment(id) {
    $("#" + id + ' p').hide();
    $("#" + id + ' .edit').hide();
    $("#" + id + ' .delete').hide();
    $("#" + id + ' input').show();
    $("#" + id + ' select').show();
    $("#" + id + ' .save').show();
    $("#" + id + ' .cancel').show();
}

function CancelEdit(id) {
    $("#" + id + ' p').show();
    $("#" + id + ' .edit').show();
    $("#" + id + ' .delete').show();
    $("#" + id + ' input').hide();
    $("#" + id + ' select').hide();
    $("#" + id + ' .save').hide();
    $("#" + id + ' .cancel').hide();
}

function SaveProgramEquipment(id) {
    var obj = {};
    obj.Id = id;
    obj.EquipmentId = $("#"+id+' #item_EquipmentId').val();
    obj.Quantity = $("#" + id +' #item_Quantity').val();
    obj.Description = $("#" + id +' #item_Description').val();
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Training/Programs/EditProgramEquipment/",
        data: { programEquipmentViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            toastr.success(SuccesEdit)
            $.ajax({
                type: "GET",
                url: "/Training/Programs/ShowEquipmentTable/",
                data: { programId: data.id },
                success: function (data) {
                    $('#EquipmentTable .modal-content').html(data);
                }
            });
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorEdit)
        },
    });
}


function AddProgram() {
    $("#Program-Create").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            Title: "required",            
            TypeId: "required",
            Price: "required",
            Status: "required",
        }
    })

    let obj = {
        LanguageId: $("#LanguageId").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        Status: $("#Status").val(),
        TypeId: $("#TypeId").val(),
        Price: $("#Price").val(),
        FirstPayment: $("#FirstPayment").val(),
        InstallmentsNumber: $("#InstallmentsNumber").val(),
        Ordiny: $("#Ordiny").is(":checked"),
    }

    if ($("#Program-Create").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Training/Programs/CreateProgram/",
            data: { programViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $('#CreateProgram').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            },
        });
    }
}

function EditProgram() {
    $("#Program-Edit").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            Title: "required",            
            TypeId: "required",
            Price: "required",
            Status: "required",
        }
    })

    let obj = {
        Id: $("#Id").val(),
        LanguageId: $("#LanguageId").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        Status: $("#Status").val(),
        TypeId: $("#TypeId").val(),
        Price: $("#Price").val(),
        FirstPayment: $("#FirstPayment").val(),
        InstallmentsNumber: $("#InstallmentsNumber").val(),
        Ordiny: $("#Ordiny").is(":checked"),
    }

    if ($("#Program-Edit").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Training/Programs/EditProgram/",
            data: { programViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesEdit);
                $('#EditProgram').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorEdit);
            },
        });
    }
}

function ChangeProgram(id) {
    $("#typeId").val($("#Programs").val())
    $("#Form1").submit();
}


function GetData(data) {
    $('#main').html(data);
}
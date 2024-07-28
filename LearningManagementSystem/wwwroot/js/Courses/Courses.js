function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Courses/GetData",
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

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Courses/ShowTable/",
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


function ShowDetailsCourse(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Courses/Details/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCourse .modal-content').html(data);
            $('#DetailsCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteCourse(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Courses/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteCourse .modal-content').html(data);
            $('#DeleteCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    $.post("/Training/Courses/DeleteCourse/",
        { id: id },
        function (data) {
            if (data !== null) {
                $('#DeleteCourse').modal("hide");
                toastr.success(SuccesDelete)
                Get()
            } else {
                toastr.error(ErrorDelete)
            }
        });
}

function GetData(data) {
    $('#main').html(data);
}

function ShowAddEquipment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Courses/ShowEquipmentTable/",
        data: { courseId: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#EquipmentTable .modal-content').html(data);
            $('#EquipmentTable').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreateCourseEquipment() {
    var obj = {};
    obj.CourseId = $("#courseId").val();
    obj.EquipmentId = $("#Equipment").val();
    obj.Quantity = $("#Quantity").val();
    obj.Description = $("#Description").val();
    $.ajax({
        type: "POST",
        url: "/Training/Courses/CreateCourseEquipment/",
        data: { courseEquipmentViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(SuccesCreate)
            $.ajax({
                type: "GET",
                url: "/Training/Courses/ShowEquipmentTable/",
                data: { courseId: data.id },
                success: function (data) {
                    $('#EquipmentTable .modal-content').html(data);
                }
            });
        },
        error: function (data) {
            toastr.error(ErrorCreate)
        },
    });
}

function GetData1(data) {
    $('#EquipmentTable .modal-content').html(data);
}

function ShowAddRow() {
    $("#AddRow").show();
    $("#addIcon").hide();
}

function HideAddRow() {
    $("#AddRow").hide();
    $("#addIcon").show();
}

function SaveCourseEquipment(id) {
    var obj = {};
    obj.Id = id;
    obj.EquipmentId = $("#" + id + ' #item_EquipmentId').val();
    obj.Quantity = $("#" + id + ' #item_Quantity').val();
    obj.Description = $("#" + id + ' #item_Description').val();
    $.ajax({
        type: "POST",
        url: "/Training/Courses/EditCourseEquipment/",
        data: { courseEquipmentViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(SuccesEdit)
            $.ajax({
                type: "GET",
                url: "/Training/Courses/ShowEquipmentTable/",
                data: { courseId: data.id },
                success: function (data) {
                    $('#EquipmentTable .modal-content').html(data);
                }
            });
        },
        error: function (data) {
            toastr.error(ErrorEdit)
        },
    });
}

function Delete(id) {
    $("#delete_item_id").val(id);
    $('#delete_confirmation_modal').modal('show');
}

function HideConfirmationModel() {
    $('#delete_confirmation_modal').modal('hide');
}

function DeleteItem() {
    $.ajax({
        type: "POST",
        url: "/Training/Courses/DeleteCourseEquipment/",
        data: { id: $("#delete_item_id").val() },
        success: function (data) {
            $('.modal-backdrop:eq(1)').remove();
            toastr.success(SuccesDelete)
            $.ajax({
                type: "GET",
                url: "/Training/Courses/ShowEquipmentTable/",
                data: { courseId: data.id },
                success: function (data) {
                    $('#EquipmentTable .modal-content').html(data);
                }
            });
        },
        error: function (data) {
            toastr.error(ErrorDelete)
        },
    });
}

function UpdateCourseEquipment(id) {
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

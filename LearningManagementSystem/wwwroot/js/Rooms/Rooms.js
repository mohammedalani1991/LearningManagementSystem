function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/GetData",
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

function ShowAddRooms() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/ShowCreate/",
        data: {},
        success: function (data) {
            HideSpinner();
            $('#CreateRooms .modal-content').html(data);
            $('#CreateRooms').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/ShowTable/",
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

var RoomFrm = $('#Rooms-Create');
RoomFrm.submit(function (e) {
    ShowSpinner();
    e.preventDefault();
    $.ajax({
        type: RoomFrm.attr('method'),
        url: RoomFrm.attr('action'),
        data: RoomFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateRooms').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function CloseAddRoom() {
    $('#CreateRooms').modal("hide");
}

function onDdlChanged(langId) {    
    ShowEditRoom($('#RoomId').val(), langId.value);
}

function ShowEditRoom(id, languageId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/ShowEdit/",
        data: { id: id, languageId: languageId },
        success: function (data) {
            HideSpinner();
            $('#EditRooms .modal-content').html(data);
            $('#EditRooms').modal("show");
            
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function CloseEditRoom() {
    $('#EditRooms').modal("hide");
}


function ShowDetailsRoom(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/ShowDetails/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetailsRooms .modal-content').html(data);
            $('#DetailsRooms').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseDetailsRoom() {
    $('#DetailsRooms').modal("hide");
}


function ShowDeleteRoom(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/ShowDelete/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DeleteRooms .modal-content').html(data);
            $('#DeleteRooms').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseDeleteRoom() {
    $('#DeleteRooms').modal("hide");
}

var roomsEditFrm = $('#Rooms-Edit');
roomsEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: roomsEditFrm.attr('method'),
        url: roomsEditFrm.attr('action'),
        data: roomsEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditRooms').modal("hide");
            HideSpinner();
            Get();

        }, error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

var ReservationFrm = $('#Reservation-Create');
ReservationFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ReservationFrm.attr('method'),
        url: ReservationFrm.attr('action'),
        data: ReservationFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#ReservationRooms').modal("hide");
            HideSpinner();  
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
            console.log(data);
        },
    });
});

function ConfirmRoomDelete(id) {
    ShowSpinner();
    $.post("/Operations/Rooms/DeleteRoom/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteRooms').modal("hide");
                HideSpinner();
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        }
    );
}

function ShowDeleteRoomReservations(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/ShowDeleteRoomReservations/",
        data: { id: id, langId: langId },
        success: function (data) {
            $('#RoomReservations').modal("hide");
            HideSpinner();
            $('#DeleteRoomReservations .modal-content').html(data);
            $('#DeleteRoomReservations').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowReservation(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/ShowRoomReservations/",
        data: { roomId: id },
        success: function (data) {
            HideSpinner();
            $('#RoomReservations .modal-content').html(data);
            $('#RoomReservations').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowCreateReservation(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/ShowCreateReservation/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#ReservationRooms .modal-content').html(data);
            $('#ReservationRooms').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseReservation() {
    $('#ReservationRooms').modal("hide");
}
function CloseRoomReservations() {
    $('#RoomReservations').modal("hide");
}

function CloseRoomReservationDelete() {
    $('#DeleteRoomReservations').modal("hide");
    $('#RoomReservations').modal("show");

}


function ConfirmRoomReservationDelete(id) {
    ShowSpinner();
    $.post("/Operations/Rooms/DeleteRoomReservation/",
        { id: id },
        function (data) {
            HideSpinner();
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteRoomReservations').modal("hide");
                ShowReservation($('#RoomId').val())

            } else {
                toastr.error(deleteMassegeError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddEquipment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Rooms/ShowEquipmentTable/",
        data: { roomId: id, languageId: langId },
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

function CreateRoomEquipment() {
    var obj = {};
    obj.RoomId = $("#roomId").val();
    obj.EquipmentId = $("#Equipment").val();
    obj.Quantity = $("#Quantity").val();
    obj.Description = $("#Description").val();
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Operations/Rooms/CreateRoomEquipment/",
        data: { roomEquipmentViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            toastr.success(addMassege);
            $.ajax({
                type: "GET",
                url: "/Operations/Rooms/ShowEquipmentTable/",
                data: { roomId: data.id },
                success: function (data) {
                    $('#EquipmentTable .modal-content').html(data);
                }
            });
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
}

function GetData(data) {
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
        url: "/Operations/Rooms/DeleteRoomEquipment/",
        data: { id: $("#delete_item_id").val() },
        success: function (data) {
            $('.modal-backdrop:eq(1)').remove();
            HideSpinner();
            toastr.success(deleteMassege);
            $.ajax({
                type: "GET",
                url: "/Operations/Rooms/ShowEquipmentTable/",
                data: { roomId: data.id },
                success: function (data) {
                    $('#EquipmentTable .modal-content').html(data);
                }
            });
        },
        error: function (data) {
            HideSpinner();
            toastr.error(deleteMassegeError);
        },
    });
}

function UpdateRoomEquipment(id) {
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

function SaveRoomEquipment(id) {
    var obj = {};
    obj.Id = id;
    obj.EquipmentId = $("#" + id + ' #item_EquipmentId').val();
    obj.Quantity = $("#" + id + ' #item_Quantity').val();
    obj.Description = $("#" + id + ' #item_Description').val();

    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Operations/Rooms/EditRoomEquipment/",
        data: { roomEquipmentViewModel: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            toastr.success(editMassege);
            $.ajax({
                type: "GET",
                url: "/Operations/Rooms/ShowEquipmentTable/",
                data: { roomId: data.id },
                success: function (data) {
                    $('#EquipmentTable .modal-content').html(data);
                }
            });
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
}

function GetData(data) {
    $('#main').html(data);
}
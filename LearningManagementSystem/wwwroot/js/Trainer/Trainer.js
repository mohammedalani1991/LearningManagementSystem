function ShowAddTrainer() {
    $.ajax({
        type: "GET",
        url: "/Training/Trainers/ShowCreate/",
        success: function (data) {
            $('#CreateTrainer .modal-content').html(data);
            $('#CreateTrainer').modal("show");
        }
    });
}

var trainerFrm = $('#Trainer-Create');
trainerFrm.submit(function (e) {
    e.preventDefault();
    $.ajax({
        type: trainerFrm.attr('method'),
        url: trainerFrm.attr('action'),
        data: trainerFrm.serialize(),
        success: function (data) {
            $('#CreateTrainer').modal("hide");
            $(".modal-backdrop").remove();
            location.reload();
        },
        error: function (data) {
            console.log('An error occurred.');
            console.log(data);
        },
    });
});

function ShowDetailsTrainer(id, langId) {
    $.ajax({
        type: "GET",
        url: "/Training/Trainers/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#DetailsTrainer .modal-content').html(data);
            $('#DetailsTrainer').modal("show");
        }
    });
}


function ShowDeleteTrainer(id, langId) {
    $.ajax({
        type: "GET",
        url: "/Training/Trainers/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#DeleteTrainer .modal-content').html(data);
            $('#DeleteTrainer').modal("show");
        }
    });
}

function ConfirmDelete(id) {
    $.post("/Training/Trainers/DeleteTrainer/",
        { id: id },
        function (data) {
            if (data !== null) {
                $('#DeleteTrainer').modal("hide");
                $(".modal-backdrop").remove();
                location.reload();
            } else {
                alert("Error while delete Trainer");
            }
        });
}

function onDdlChanged(langId) {
    ShowEditTrainer($('#Id').val(), langId.value);
}


var trainerEditFrm = $('#Trainer-Edit');
trainerEditFrm.submit(function (e) {
    e.preventDefault();
    $.ajax({
        type: trainerEditFrm.attr('method'),
        url: trainerEditFrm.attr('action'),
        data: trainerEditFrm.serialize(),
        success: function (data) {
            $('#EditTrainer').modal("hide");
            $(".modal-backdrop").remove();
            location.reload();
        },
        error: function (data) {
            console.log('An error occurred.');
            console.log(data);
        },
    });
});

function ShowEditTrainer(id, langId) {
    $.ajax({
        type: "GET",
        url: "/Training/Trainers/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditTrainer .modal-content').html(data);
            $('#EditTrainer').modal("show");
        }

    });
}

function ShowEditPassword(id) {
    $.ajax({
        type: "GET",
        url: "/Training/Trainers/ShowEditPassword/",
        data: { id: id },
        success: function (data) {
            $('#EditPassword .modal-content').html(data);
            $('#EditPassword').modal("show");
        }

    });
}

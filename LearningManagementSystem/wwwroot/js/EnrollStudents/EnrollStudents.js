
function ShowDetailsCourses(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollStudents/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsEnrollStudents .modal-content').html(data);
            $('#DetailsEnrollStudents').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}



function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}
function GetData(data) {
    $('#MyCourses').html(data);
}



function ShowDetailsCourses(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourses/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsTrainerCourses .modal-content').html(data);
            $('#DetailsTrainerCourses').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}



function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}
function GetData(data) {
    $('#MyCoursesData').html(data);
}
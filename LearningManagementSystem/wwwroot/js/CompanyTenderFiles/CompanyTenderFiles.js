function DeleteCompanyTenderFileS(id) {
    ShowSpinner();    
    $.post("/Companies/CompanyTenderFiles/Delete/",
        { id: id },
        function (data) {
            if (data !== null) {
                HideSpinner();
                ShowCompanyTenderFiles($("#companyTenderId").val());
            } else {
                alert("Error while delete Company Tenders");
                HideSpinner();
            }
        });

}

var companyTenderFileFrm = $('#CompanyTenderFile');
companyTenderFileFrm.submit(function (e) {
    ShowSpinner();
    var input = document.getElementById('files');
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
    formData.append("companyTenderId", $("#companyTenderId").val());
    e.preventDefault();
    $.ajax({
        type: companyTenderFileFrm.attr('method'),
        url: companyTenderFileFrm.attr('action'),
        data: formData,
        contentType: false, // NEEDED, DON'T OMIT THIS (requires jQuery 1.6+)
        processData: false, // NEEDED, DON'T OMIT THIS
        success: function (data) {
            ShowCompanyTenderFiles($("#companyTenderId").val());
            HideSpinner();
        },
        error: function (data) {
            console.log('An error occurred.');
            console.log(data);
            HideSpinner();
        },
    });
});

var CompanyTenderFilesForm = $('#CompanyTenderFilesForm');
CompanyTenderFilesForm.submit(function (e) {    
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CompanyTenderFilesForm.attr('method'),
        url: CompanyTenderFilesForm.attr('action'),
        data: CompanyTenderFilesForm.serialize(),
        success: function (data) {
            $('#CompanyTenderFiels .modal-content').html(data);
            HideSpinner();
        },
        error: function (data) {
            HideSpinner();
            console.log('An error occurred.');
            console.log(data);
        },
    });
});
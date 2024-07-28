
function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Regiment/GetData",
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

function ShowAddRegiment() {
      ShowSpinner();
      $.ajax({
          type: "GET",
          url: "/Training/Regiment/ShowCreate/",
          success: function (data) {
                  HideSpinner();
                  $('#EditRegimentDiv').empty();
                  $('#CreateRegiment .modal-content').html(data);
                  $('#CreateRegiment').modal("show");
                  $('select').selectize({
                      sortField: 'text'
                  });
          }
      }).fail(function( jqXHR, textStatus, errorThrown ){
          HideSpinner();
      });
  
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Regiment/ShowTable/",
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

function ShowDetailsRegiment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Regiment/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsRegiment .modal-content').html(data);
            $('#DetailsRegiment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteRegiment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Regiment/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteRegiment .modal-content').html(data);
            $('#DeleteRegiment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/Training/Regiment/DeleteRegiment/",
        { id: id },
        function (data) {
            toastr.success(SuccesDelete);
            $('#DeleteRegiment').modal("hide");
            HideSpinner();
            Get()
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChanged(langId) {
    ShowEditRegiment($('#RegimentId').val(), langId.value);
}


function ShowEditRegiment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Training/Regiment/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#CreateRegimentDiv').empty();
            $('#EditRegiment .modal-content').html(data);
            $('#EditRegiment').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function AddRegiment() {
    $("#Regiment-Create").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            Title: "required",
            Description: "required",
            Status: "required",
        }
    })

    let obj = {
        LanguageId: $("#LanguageId").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        Status: $("#Status").val(),
    }

    if ($("#Regiment-Create").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Training/Regiment/CreateRegiment/",
            data: { regimentViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $('#CreateRegiment').modal("hide");
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

function EditRegiment() {
    $("#Regiment-Edit").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            Title: "required",
            Description: "required",
            Status: "required",
        }
    })

    let obj = {
        Id: $("#Id").val(),
        LanguageId: $("#LanguageId").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        Status: $("#Status").val(),
    }

    if ($("#Regiment-Edit").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Training/Regiment/EditRegiment/",
            data: { regimentViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesEdit);
                $('#EditRegiment').modal("hide");
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

function GetData(data) {
    $('#main').html(data);
}
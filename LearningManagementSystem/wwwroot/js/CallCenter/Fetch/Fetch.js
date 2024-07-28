function Get() {
    $.ajax({
        type: "GET",
        url: "/CallCenter/CallCenterFetch/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}

Get()

function CheckFetch(id) {
    $.ajax({
        type: "GET",
        url: "/CallCenter/CallCenterFetch/CheckFetch",
        dataType: 'json',
        data: { id: id },
        success: function (result) {
            if (!result.taken) {
                if (result.sourceOne) {
                    Get()
                    $.ajax({
                        type: "GET",
                        url: "/CallCenter/CallCenterFetch/GetFetch",
                        dataType: 'html',
                        data: { id: id },
                        success: function (data) {
                            $('#Fetch .modal-content').html(data);
                            $('#Fetch').modal("show");
                        }, error: function (data) {
                            toastr.error(ErrorFetch);
                        }
                    });
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "/CallCenter/CallCenterFetch/Fetch",
                        data: { id:id },
                        headers: {
                            'RequestVerificationToken': Token
                        },
                        success: function (data) {
                            Get()
                            toastr.success(SuccesFetch);
                        }, error: function (data) {
                            toastr.error(ErrorFetch);
                        }
                    })
                }
            } else {
                Get()
                toastr.warning(ErrorTaken);
            }
        }
    });
}

function SetInterest() {
    $("#Interest-Form").validate({
        errorElement: "span",
        rules: {
            Programs: "required",
            HowSerious: "required",
            Interest_Description: "required",
        }
    })
    if ($("#Interest-Form").valid()) {
        $("#Interest").val($("#Programs").find(":selected").text());
        $("#ProgramId").val($("#Programs").val());
        $("#HowSerious1").val($("#HowSerious").val());
        $("#Interest_Description1").val($("#Interest_Description").val());
        $("#InterestModal").modal("hide");
    }
}

function Fetch() {
    $("#Fetch-Form").validate({
        errorElement: "span",
        rules: {
            Title: "required",
            Description: "required",
            NoteType: "required",
            NoteDate: "required",
            'ContactViewModel.Mobile': {
                required: true,
                number: true
            },
            ExtraMobile: {
                required: true,
                number: true
            },
            CollegeExam: "required",
            AcademicTest: "required",
        }
    })

    var obj = {
        Id: $("#Id").val(),
        'ContactViewModel.Mobile': $("#ContactViewModel_Mobile").val(),
        ExtraMobile: $("#ExtraMobile").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        CollegeId: $("#CollegeExam").val(),
        SpecialtyId: $("#AcademicTest").val(),
        IsExternalStudy: $("#StudyOut").is(":checked"),
        IsTawjehe: $("#Options").is(":checked"),
        TypeId: $("#NoteType").val(),
        ProgramId: $("#ProgramId").val(),
        HowSerious1: $("#HowSerious1").val(),
        Interest_Description1: $("#Interest_Description1").val(),
    }

    if ($("#Fetch-Form").valid()) {
        if ($("#Interest").val() == "") {
            $("#InterestModal").modal("show");
        } else {
            $.ajax({
                type: "POST",
                url: "/CallCenter/CallCenterFetch/FetchSourceOne",
                data: { leadViewModel: obj },
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(SuccesFetch);
                    $('#Fetch').modal("hide");
                    $('#Edit').modal("hide");
                    $("#Interest-Form").trigger("reset");
                    Get()
                },
                error: function (data) {
                    toastr.error(ErrorFetch);
                },
            });
        }
    }
}

function GetData1(data) {
    $('#Fetch .modal-content').html(data);
}

function ChangePagenation1() {
    $("#pagination").val($("#pagin").val())
    $("#Form2").submit();
}


$("#InterestModal").on("hidden.bs.modal", function () {
    setTimeout(function () {
        $("body").addClass("modal-open");
    }, 500)
});
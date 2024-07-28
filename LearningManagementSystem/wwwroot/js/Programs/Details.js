$(document).ready(function () {
    $("#yearpicker").yearpicker({
        year: new Date().getFullYear(),
        startYear: 2010,
        endYear: new Date().getFullYear() + 10,
        onChange: function (value) {
            Year = value;
            $.ajax({
                type: "GET",
                url: "/Training/Programs/ProgramPlanDetail/",
                data: { id: $("#ProgramId").val(), year: value, languageId: $("#LanguageId").val() },
                success: function (data) {
                    $("#PlanCourses").empty();
                    $.each(data.programPlanCourses, function () {
                        $("#PlanCourses").append("<tr class='tms-tr'><td>" + this.courseId + "</td><td>" + this.name + "</td><td><a onclick='ShowDelete(" + this.id + ")' title='"+DeleteText+"' class='btn btn-sm btn-danger'><i class='fa fa-trash text-white'></i></a></td></tr>")
                    });
                    $("#PlanId").val(data.id);
                    $("#PlanDescription").val(data.description);
                    tinymce.get("PlanDescription").setContent(data.description);
                    $("#AddPlan").html(EditText)
                }, error: function (req, status, error) {
                    $("#PlanCourses").empty();
                    $("#PlanId").val(0);
                    tinymce.get("PlanDescription").setContent("");
                    $("#AddPlan").html(AddText)
                }
            });
        },
    });

    $.ajax({
        type: "GET",
        url: "/Training/Programs/GetCourses/",
        success: function (data) {
            $.each(data, function () {
                $("#Courses").append($("<option />").val(this.id).text(this.courseName));
                
            });
            $("#Courses").trigger('chosen:updated')
            //SetChoices();
        }, error: function (req, status, error) {
            toastr.error(ErrorMessage)
        }
    });
});

function AddPlan() {
    var obj = {
        Id: $("#PlanId").val(),
        ProgramId: $("#ProgramId").val(),
        YearId: Year,
        Description: tinymce.get("PlanDescription").getContent(),
        LanguageId: $("#LanguageId").val()
    }

    $.ajax({
        type: "POST",
        url: "/Training/Programs/CreateProgramPlan",
        data: {
            programPlanViewModel: obj
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(SuccesEdit)
            setTimeout(function () {
                location.reload();
            },1000)
        },
        error: function (req, status, error) {
            toastr.error(ErrorEdit)
        }
    });
}

function AddPlanCourses() {
    var obj = {
        Id: $("#PlanId").val(),
        ProgramId: $("#ProgramId").val(),
        YearId: Year,
        Description: tinymce.get("PlanDescription").getContent() ?? Empty,
        LanguageId: $("#LanguageId").val(),
        Ids: $("#Courses").val(),
    }
    $.ajax({
        type: "POST",
        url: "/Training/Programs/CreateProgramPlanCourses",
        data: {
            programPlanViewModel: obj
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(SuccesCreate)
            setTimeout(function () {
                location.reload();
            }, 1000)
        },
        error: function (req, status, error) {
            toastr.error(ErrorCreate)
        }
    });
}

function SetChoices() {
    var multipleCancelButton = new Choices("#Courses", {
        removeItemButton: true,
        maxItemCount: 100,
        searchResultLimit: 100,
        renderChoiceLimit: 100,
        noResultsText: 'No results found',
        noChoicesText: 'No choices to choose from',
    });
}

function ShowDelete(id) {
    $("#CourseNumber").val(id);
    $("#DeletePlanCource").modal("show")

}

function onDdlChanged(current) {
    $.ajax({
        type: "GET",
        url: "/Training/Programs/ProgramPlanDetail/",
        data: { id: $("#ProgramId").val(), year: $("#yearpicker").val(), languageId: $("#LanguageId").val() },
        success: function (data) {
            $("#PlanId").val(data.id);
            $("#PlanDescription").val(data.description);
            tinymce.get("PlanDescription").setContent(data.description);
        }, error: function (req, status, error) {
        }
    });
}

function Delete() {
    $.ajax({
        type: "POST",
        url: "/Training/Programs/DeleteProgramPlanCourse",
        data: { id: $("#CourseNumber").val() },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (result) {
            toastr.success(SuccesDelete)
            setTimeout(function () {
                location.reload();
            }, 1000)        },
        error: function (req, status, error) {
            toastr.error(ErrorDelete)
        }
    });
}

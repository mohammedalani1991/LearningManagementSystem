function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/GetData",
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

function ProgramSectionEditToggles(){
    if($("#EditCompanyToggle").is(":hidden"))
        return;
    $(".tms-nav-div").toggle();
    $("#myTabContent").toggle();
}
function ShowAddProgramSection(programId, position) {
    if (position == 'InnerProgramSections') {
        $('#InnerProgramSections').val('InnerProgramSections');
    }
    else {
        $('#InnerProgramSections').val('');
    }
    $('#ProgramSections').modal("hide");
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/ShowCreate/",
        data: { programId: programId },
        success: function (data) {
           
            HideSpinner();
            $('#CreateProgramSections .modal-content').html(data);
            $('#CreateProgramSections').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/ShowTable/",
        success: function (data) {
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
            HideSpinner();  
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowProgramSections(programId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/Index/",
        data: { programId: programId },
        success: function (data) {
            HideSpinner();
            $('#ProgramSections .modal-content').html(data);
            $('#ProgramSections').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseAddProgramSection() {
    if ($('#InnerProgramSections').val() == '') {
        ShowProgramSections($('#programId').val());
    }

    $('#CreateProgramSections').modal("hide");
}

function CloseProgramSections() {
    $('#ProgramSections').modal("hide");
    $(".modal-backdrop").remove();
}

var ProgramSectionFrm = $('#ProgramSection-Create');
ProgramSectionFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ProgramSectionFrm.attr('method'),
        url: ProgramSectionFrm.attr('action'),
        data: ProgramSectionFrm.serialize(),
        success: function (data) {
          toastr.success(addMassege);
            $('#CreateProgramSections').modal("hide");
            HideSpinner();
            if ($('#InnerProgramSections').val() == '') {
                ShowProgramSections($('#programId').val());
            }
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});


function ShowEditProgramSection(id, langId) {
    $('#ProgramSections').modal("hide");
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#EditProgramSections .modal-content').html(data);
            $('#EditProgramSections').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseEditProgramSection() {
    ShowProgramSections($('#programId').val());

    $('#EditProgramSections').modal("hide");
}

async function ShowDetailsProgramSection(id, langId) {
    $('#ProgramSections').modal("hide");

    ShowSpinner();
    var call = await $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsProgramSections .modal-content').html(data);
            $('#DetailsProgramSections').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

    return call;
}

function CloseDetailsProgramSection() {
    $('#DetailsProgramSections').modal("hide");
    ShowProgramSections($('#programId').val());

}

function ShowDeleteProgramSection(id, langId) {
    $('#ProgramSections').modal("hide");

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteProgramSections .modal-content').html(data);
            $('#DeleteProgramSections').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseDeleteProgramSection() {
    $('#DeleteProgramSections').modal("hide");
    ShowProgramSections($('#programId').val());

}


function CloseAssignStudent() {
    $('#ProgramSectionsAssignStudent').modal("hide");
    ShowProgramSections($('#programId').val());

}
function CloseUnAssignStudent() {
    $('#ProgramSectionsUnAssignStudent').modal("hide");
    ShowProgramSections($('#programId').val());

}

var ProgramSectionEditFrm = $('#ProgramSection-Edit');
ProgramSectionEditFrm.submit(function (e) {

    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ProgramSectionEditFrm.attr('method'),
        url: ProgramSectionEditFrm.attr('action'),
        data: ProgramSectionEditFrm.serialize(),
        success: function (data) {
            toastr.success(editMassege);
            HideSpinner();

            if (data.status != undefined && data.status == 2) // 2 : Faild
            {
                $('#EditProgramSections .modal-content').html(data.ajaxReturn);
                $('#EditProgramSections').modal("show");
                return;
            }

            $('#EditProgramSections').modal("hide");
            $(".modal-backdrop").remove();
            ShowProgramSections($('#programId').val());

        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ConfirmDelete(id) {
    ShowSpinner();

    $.post("/Operations/ProgramSections/DeleteProgramSection/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);

                $('#DeleteProgramSections').modal("hide");
                HideSpinner();
                $(".modal-backdrop").remove();
                ShowProgramSections($('#programId').val());

            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

}

function onProgramSectionDdlChanged(langId) {

    ShowEditProgramSection($('#ProgramSectionId').val(), langId.value);
}


function ShowAssignStudent(programSectionId) {
    $('#ProgramSections').modal("hide");
    $('#ProgramSectionsUnAssignStudent .modal-content').html("");

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/GetUnAssignedStudent/",
        data: { programSectionId: programSectionId },
        success: function (data) {
            HideSpinner();
            $('#ProgramSectionsAssignStudent .modal-content').html(data);
            $('#ProgramSectionsAssignStudent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

}


$(document).on('click',"#second-tabStudents",function () {
    $('#ProgramSections').modal("hide");
    $('#ProgramSectionsUnAssignStudent .modal-content').html("");

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/ProgramSectionStudent/",
        data: { programSectionId: $("#ProgramSectionId").val() },
        success: function (data) {
            HideSpinner();
            $('#GetStudents').html(data);
            $('#GetStudents').modal("show");
            if ($(".modal-backdrop").length > 1)
                $(".modal-backdrop:eq(0)").remove();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
})
$(document).on('click',"#thered-tabSectionCourses",function () {
    ShowSectionCourse();
})

function ShowSectionCourse() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/ShowSectionCourse/",
        data: { programSectionId: $("#ProgramSectionId").val() },
        success: function (data) {
            HideSpinner();
            $('#CreateSectionCourses').html(data);
            $('#CreateSectionCourses').modal("show");
            if ($(".modal-backdrop").length > 1)
                $(".modal-backdrop:eq(0)").remove();
            if ($("#ProgramSectionData").children().length != 0)
                $("#AddSectionCourse").remove();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

}
function ShowUnAssignStudent(programSectionId) {
    $('#ProgramSections').modal("hide");
    $('#ProgramSectionsAssignStudent .modal-content').html("");
    ShowSpinner();

    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/GetAssignedStudent/",
        data: { programSectionId: programSectionId },
        success: function (data) {
            HideSpinner();
            $('#ProgramSectionsUnAssignStudent .modal-content').html(data);
            $('#ProgramSectionsUnAssignStudent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

}

function AddSectionCourse(event) {
    event.preventDefault();
    if (this.value != '') {
        ShowSpinner();
        $.post("/Operations/ProgramSections/AddSectionCourses/",
            { YearId: $('#YearId').val(), programSectionId: $('#ProgramSectionId').val() },
            function (data) {
                toastr.success(addMassege);
                HideSpinner();
                if (data !== null) {
                    ShowSectionCourse();

                } else {
                    HideSpinner();
                    toastr.error(addMassegeError);
                }
            }).fail(function (jqXHR, textStatus, error) {
            HideSpinner();
        });
    }
}


function ViewSectionCourseAttendance(id, programSectionId) {
    $('#DetailsProgramSections').modal("hide");
    $('#ProgramSectionsUnAssignStudent .modal-content').html("");
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/ViewSectionCourseAttendance/",
        data: { id ,programSectionId },
        success: function (data) {
            HideSpinner();
            $(".modal-backdrop:eq(0)").remove();
            $('#SectionCourseAttendance .modal-content').html(data);
            $('#programSectionId').val(programSectionId)
            document.getElementById('DateValue').valueAsDate = new Date();
            $('#SectionCourseAttendance').modal("show");
            $('#DetailsProgramSections .modal-content').html("");
            GetAttendance(null, $("#DateValue").val())
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseAttendance() {
    $('#SectionCourseAttendance').modal("hide");
    ShowDetailsProgramSection($('#Id').val()).then(() => {
        $("#thered-tabSectionCourses").click();
    })
}

function GetAttendance(date, val) {
    arr=[]
    setDate = val ?? date.value;
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/ProgramSections/GetSectionCourseAttendance/",
        data: { programSectionId: $('#programSectionId').val(), dateTime: val ?? date.value },
        success: function (data) {
            HideSpinner();
            if (!data.length)
                $(".StudentId").map(function (y, x) {
                    $(".StudentId:eq(" + y + ")").parent().find("input[type='checkbox']").prop('checked', false);
                    $(".StudentId:eq(" + y + ")").parent().find("input[type='text']").prop('disabled', "");
                    $(".StudentId:eq(" + y + ")").parent().find("input[type='text']").val("");
                })
            else {
                for (var i = 0; i < data.length; i++) {
                    arr[i] = data[i].isPresent ? data[i].studentId : 0;
                    $(".StudentId").map(function (y, x) {
                        if ($(".StudentId:eq(" + y + ")").val() == data[i].studentId && data[i].isPresent) {
                            $(".StudentId:eq(" + y + ")").parent().find("input[type='checkbox']").prop('checked', true);
                            $(".StudentId:eq(" + y + ")").parent().find("input[type='text']").prop('disabled', "disabled");
                            $(".StudentId:eq(" + y + ")").parent().find("input[type='text']").val("");
                        }
                        else if ($(".StudentId:eq(" + y + ")").val() == data[i].studentId && !data[i].isPresent) {
                            $(".StudentId:eq(" + y + ")").parent().find("input[type='checkbox']").prop('checked', false);
                            $(".StudentId:eq(" + y + ")").parent().find("input[type='text']").prop('disabled', "");
                            $(".StudentId:eq(" + y + ")").parent().find("input[type='text']").val(data[i].absenceNote);
                        }
                    })
                }
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

async function AddAttendance(changePage=false) {
    arr = arr.filter(function (val) {
        return val !== 0;
    });
    var obj = {};
    obj.SectionCourseId = $("#programSectionId").val();
    obj.Date = $("#DateValue").val();
    obj.StudentIds = arr;
    obj.StudentIds1 = $.map($('.StudentId'), function (el) { return el.value; });;
    obj.AbsenceNotes = $.map($('.AbsenceNote'), function (el) { return el.value; });

    ShowSpinner();
    var call = await $.ajax({
        type: "POST",
        url: "/Operations/ProgramSections/AddStudentAttendance/",
        data: { studentAttendance: obj },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            arr=[]
            if (!changePage)
             toastr.success(AddAttendanceSuccessMessage)
        },
        error: function (data) {
            HideSpinner();
            toastr.error(AddAttendanceErrorMessage)
        },
    });

    return call;
}

function CheckboxClicked(current) {
    arr.map(function (x, y) {
        if ($(current).parent().parent().find(".StudentId").val() == x)
            arr = arr.filter(e => e !== x)
    })
    if ($(current).is(':checked')) {
        arr.push(parseInt($(current).parent().parent().find(".StudentId").val()))
        $(current).parent().parent().find("input[type='text']").val("")
    }
    else
        $(current).parent().parent().find("input[type='text']").prop('disabled', "")
}

function GetData1(data) {
    $('#SectionCourseAttendance .modal-content').html(data);
    GetAttendance(null, setDate)
    $("#DateValue").val(setDate);
}

function ChangePagenation22() {
    $("#pagination22").val($("#pagin22").val())
    $("#Form2").submit()
}

function ChangePage() {
    AddAttendance(true).then(() => {
        GetAttendance(null, setDate);
        $("#DateValue").val(setDate);
    })
}

function GetData(data) {
    $('#main').html(data);
}
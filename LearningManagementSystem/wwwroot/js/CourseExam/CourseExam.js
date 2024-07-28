function Get() {
    ShowSpinner();
    var CheckAbilityToEnrollStudentExam = $("#CheckAbilityToEnrollStudentExam").val();
    $.ajax({
        type: "GET",
        url: "/CourseExam/GetData",
        dataType: 'html',
        data: { EnrollCourseExamId: $("#EnrollCourseExamId").val(), EnrollTeacherCourseId: $("#EnrollTeacherCourseId").val(), CheckAbilityToEnrollStudentExam: CheckAbilityToEnrollStudentExam },
        success: function (data) {
            $('#GetData').html(data);
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}




Get();

function CreateTimer(minute) {
    if (minute > 0) {
        // Set the date we're counting down to
        var countDownDate = new Date(new Date().getTime() + (minute * 60000)).getTime();
       
        if ($(".CTimeLeft").length != 0) {
            // Update the count down every 1 second
            var x = setInterval(function () {

                // Get today's date and time
                var now = new Date().getTime();

                // Find the distance between now and the count down date
                var distance = countDownDate - now;

                // Time calculations for days, hours, minutes and seconds
                var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                var seconds = Math.floor((distance % (1000 * 60)) / 1000);

                // Output the result in an element with id="demo"
                if (hours > 0)
                    document.getElementById("timer").innerHTML = " " + hours +"h " + minutes + "m " + seconds + "s ";
                    else
                    document.getElementById("timer").innerHTML = " " + minutes + "m " + seconds + "s ";

                // If the count down is over, write some text
                if (distance < 0) {
                    clearInterval(x);
                    document.getElementById("timer").innerHTML = EXPIRED;
                    $("#BtnConfirmSubmitExam").remove();
                    AutoSubmitExam();

                }
            }, 1000);
        }
    } else {
        $(".CTimeLeft").remove();
    }
}

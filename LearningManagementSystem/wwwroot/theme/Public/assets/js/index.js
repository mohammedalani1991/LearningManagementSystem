/******** swiper *************** */
let swiperSliderhome = new Swiper(".Swipersliderhome", {
    pagination: {
        el: ".swiper-pagination",
    },
    spaceBetween: 1,
    keyboard: {
        enabled: true,
    },
    autoplay: {
        delay: 5000,
        disableOnInteraction: false,
    },
    loop: true,
});
let swiperStudents = new Swiper(".Students", {
    delay: 10000,

    breakpoints: {
        "@0.00": {
            slidesPerView: 1.5,
            spaceBetween: 15,
            centeredSlides: true,
            initialSlide: 3,
            grid: {
                rows: 1,
            },
        },
        992: {
            grid: {
                rows: 2,
            },
            slidesPerView: 3,
            spaceBetween: 0,
        },
    }
});

let swipersecCources = new Swiper(".secCources", {
    slidesPerView: 1,
    spaceBetween: 20,
    breakpoints: {
        "@0.00": {
            slidesPerView: 1.8,
            spaceBetween: 20,
            initialSlide: 1,
        },
        "@1.50": {
            slidesPerView: 3.5,
            spaceBetween: 30,
        },
        1800: {
            slidesPerView: 4,
            spaceBetween: 33,
        }
    },
    navigation: {
        nextEl: ".secCources .swiper-button-next",
        prevEl: ".secCources .swiper-button-prev",
    },
});
let swipermySwiperSaying = new Swiper(".mySwiperSaying", {
    slidesPerView: 1,
    spaceBetween: 16,

    breakpoints: {
        "@0.00": {
            slidesPerView: 1.3,
            spaceBetween: 16,
            initialSlide: 2,
        },
        "@0.65": {
            slidesPerView: 1.2,
            spaceBetween: 16,
        },

        "@1.00": {
            slidesPerView: 2.8,
            spaceBetween: 40,
        },
        "@1.50": {
            slidesPerView: 2.6,
            spaceBetween: 43,
        },
    },
    navigation: {
        nextEl: ".mySwiperSaying .swiper-button-next",
        prevEl: ".mySwiperSaying .swiper-button-prev",
    },

});
let swiperActiveProjects = new Swiper(".ActiveProjects", {
    slidesPerView: 3,
    spaceBetween: 47,
    initialSlide: 1,
    breakpoints: {
        "@0.00": {
            slidesPerView: 1.5,
            spaceBetween: 11,
            initialSlide: 2,
            centeredSlides: true,
        },
        "@0.75": {
            slidesPerView: 3,
            spaceBetween: 47,
            initialSlide: 2,
            centeredSlides: true,
        },
        "@1.00": {
            slidesPerView: 3.1,
            spaceBetween: 40,
            centeredSlides: false,
        },
    },
    navigation: {
        nextEl: ".ActiveProjects .swiper-button-next",
        prevEl: ".ActiveProjects .swiper-button-prev",
    },

});
let swiperSwiperTrainers = new Swiper(".SwiperTrainers", {
    slidesPerView: 1,
    spaceBetween: 10,
    initialSlide: 2,
    breakpoints: {
        "@0.00": {
            slidesPerView: 1.5,
            spaceBetween: 10,
            initialSlide: 2,
            centeredSlides: true,
        },
        "@0.75": {
            slidesPerView: 2.5,
            spaceBetween: 20,
            initialSlide: 2,
            centeredSlides: true,
        },
        "@1.00": {
            slidesPerView: 4,
            spaceBetween: 20,
            centeredSlides: false,
        },
        "@1.50": {
            slidesPerView: 4,
            spaceBetween: 20,
            centeredSlides: false,
        },
    },
});


/* video swipper */
var swiper = new Swiper('.swiper-container', {
    direction: 'vertical',
    slidesPerView: 4,
    spaceBetween: 30,
    mousewheel: true,
    grabCursor: true,
    autoplay: {
        delay: 2000,
        disableOnInteraction: false,
    },
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
    },
});

/******** Animate *************** */

AOS.init();
let valueDisplays = document.querySelectorAll(".num");
valueDisplays.forEach((valueDisplay) => {
    let startValue = -1;
    let endValue = parseInt(valueDisplay.getAttribute("data-val"));
    let duration = 1; // Set to 1, but the actual delay will be approximately 4ms due to browser restrictions
    let increment = 10; // Increase this number to count up faster
    let counter = setInterval(function () {
        startValue += increment; // Increase by more than 1 at a time
        if (startValue >= endValue) {
            startValue = endValue; // Make sure we don't go over the end value
            clearInterval(counter);
        }
        valueDisplay.textContent = startValue;
    }, duration);
});


/****************** validation ***********************************/
(function () {
    'use strict'
    var forms = document.querySelectorAll('.needs-validation')
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }
                form.classList.add('was-validated')
            }, false)
        })
})()

/******************** Repeat Password ****************************************/

function repassword() {
    sewar = document.getElementById("validationRNPassword");

    if (document.getElementById("Input.NewPassword").value === document.getElementById("Input.ConfirmPassword").value) {
        sewar.style.display = 'none';
    } else {
        sewar.style.display = 'block';
    }
}


/******************* login **********************/
const togglePassword = document.getElementsByClassName("togglePassword");
const password = document.getElementsByClassName("password");
for (let tp = 0; tp < togglePassword.length; tp++) {
    togglePassword[tp].addEventListener("click", function () {
        password[tp].setAttribute("type", password[tp].getAttribute("type") === "password" ? "text" : "password");
        this.classList.toggle("fa-eye")
        this.classList.toggle("fa-eye-slash");
    });
}

var paused = false,
    nterval = setInterval(function () {
        (!paused) && $('#next3').trigger('click');
    }, 3000);
$('.slideshow, .controls').hover(function () {
    paused = true;
}, function () {
    paused = false;
});



/************************** sign up ******************************* */

let Days = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];// index => month [0-11]
$(document).ready(function () {
    let option = '<option value="day">day</option>';
    let selectedDay = "day";
    for (let i = 1; i <= Days[0]; i++) { //add option days
        option += '<option value="' + i + '">' + i + '</option>';
    }
    $('#day').append(option);
    $('#day').val(selectedDay);

    option = '<option value="month">month</option>';
    let selectedMon = "month";
    for (let i = 1; i <= 12; i++) {
        option += '<option value="' + i + '">' + i + '</option>';
    }
    $('#month').append(option);
    $('#month').val(selectedMon);

    option = '<option value="month">month</option>';
    selectedMon = "month";
    for (let i = 1; i <= 12; i++) {
        option += '<option value="' + i + '">' + i + '</option>';
    }
    $('#month2').append(option);
    $('#month2').val(selectedMon);

    let d = new Date();
    option = '<option value="year">year</option>';
    selectedYear = "year";
    // for (let i = 1990;i <= d.getFullYear();i++){// years start i
    //     option += '<option value="'+ i + '">' + i + '</option>';
    // }
    for (let i = d.getFullYear(); i >= 1990; i--) {
        option += '<option value="' + i + '">' + i + '</option>';
    }
    $('#year').append(option);
    $('#year').val(selectedYear);
});
function isLeapYear(year) {
    year = parseInt(year);
    if (year % 4 != 0) {
        return false;
    } else if (year % 400 == 0) {
        return true;
    } else if (year % 100 == 0) {
        return false;
    } else {
        return true;
    }
}

function change_year(select) {
    if (isLeapYear($(select).val())) {
        Days[1] = 29;

    }
    else {
        Days[1] = 28;
    }
    if ($("#month").val() == 2) {
        let day = $('#day');
        let val = $(day).val();
        $(day).empty();
        option = '<option value="day">day</option>';
        for (let i = 1; i <= Days[1]; i++) { //add option days
            option += '<option value="' + i + '">' + i + '</option>';
        }
        $(day).append(option);
        if (val > Days[month]) {
            val = 1;
        }
        $(day).val(val);
    }
}
function change_month(select) {
    day = $('#day');
    let val = $(day).val();
    $(day).empty();
    option = '<option value="day">day</option>';
    let month = parseInt($(select).val()) - 1;
    for (let i = 1; i <= Days[month]; i++) { //add option days
        option += '<option value="' + i + '">' + i + '</option>';
    }
    $(day).append(option);
    if (val > Days[month]) {
        val = 1;
    }
    $(day).val(val);
}



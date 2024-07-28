function Get() {
    $.ajax({
        type: "GET",
        url: "/Sales/SortFetchData/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
            dragula([
                document.getElementById('PublicFetch'),
            ])
                .on('drag', function (el) {

                    // add 'is-moving' class to element being dragged
                    el.classList.add('is-moving');
                })
                .on('dragend', function (el) {
                    var s = $(".CommunicationChannelId").map((i, e) => e.value).get();
                    $.ajax({
                        type: "POST",
                        url: "/Sales/SortFetchData/SetPublicOrder",
                        data: { ids: s },
                        headers: {
                            'RequestVerificationToken': Token
                        },
                        success: function (data) {
                            toastr.success(SuccesEdit);
                        },
                        error: function (data) {
                            toastr.error(ErrorEdit);
                        },
                    });

                    // remove 'is-moving' class from element after dragging has stopped
                    el.classList.remove('is-moving');

                    // add the 'is-moved' class for 600ms then remove it
                    window.setTimeout(function () {
                        el.classList.add('is-moved');
                        window.setTimeout(function () {
                            el.classList.remove('is-moved');
                        }, 600);
                    }, 100);
                });
            dragula([
                document.getElementById('PrivateFetch'),
            ])
                .on('drag', function (el) {

                    // add 'is-moving' class to element being dragged
                    el.classList.add('is-moving');
                })
                .on('dragend', function (el) {
                    var s = $(".PrivateFetchId").map((i, e) => e.value).get();
                    $.ajax({
                        type: "POST",
                        url: "/Sales/SortFetchData/SetPrivateOrder",
                        data: { ids: s },
                        headers: {
                            'RequestVerificationToken': Token
                        },
                        success: function (data) {
                            toastr.success(SuccesEdit);
                        },
                        error: function (data) {
                            toastr.error(ErrorEdit);
                        },
                    });
                    // remove 'is-moving' class from element after dragging has stopped
                    el.classList.remove('is-moving');

                    // add the 'is-moved' class for 600ms then remove it
                    window.setTimeout(function () {
                        el.classList.add('is-moved');
                        window.setTimeout(function () {
                            el.classList.remove('is-moved');
                        }, 600);
                    }, 100);
                });
        }, error: function (data) {
            toastr.error(ErrorMessage);
        }
    });
}

Get();

function GetPriviteData(id) {
    $.ajax({
        type: "Get",
        url: "/Sales/SortFetchData/GetPrivateData",
        dataType: 'json',
        data: {id:id.value},
        success: function (data) {
            $("#PrivateFetch").empty();
            $.each(data, function (i, x) {
                $("#PrivateFetch").append("<div class='d-flex p-3 justify-content-between drag-item left-line grabbable'><input value ='" + x.id + "'class= 'PrivateFetchId d-none'/><div class='d-flex'><h6 class='mx-1 my-auto bg-danger px-2 text-white' style='border-radius:1rem'>" + x.count + "</h6><div><h5 class='mx-4 my-auto'>" + x.name + "</h5><h6 class='mx-4 px-1 pt-1 my-auto' style='color:#4c9ced'>" + x.advicer +"</h6></div></div><div><h6 class='mx-4 my-auto'>"+x.date+"</h6><div></div>")
            })
            $("#privateNum").html(data.length)
        }
        , error: function (data) {
            toastr.error(ErrorMessage);
        }
    })
}

let arr = [];

function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Discount/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get();

function ShowAddDiscount() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Discount/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#CreateDiscount .modal-content').html(data);
            $('#CreateDiscount').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetAcceptedDiscount() {
    if ($("#AcceptedAnotherDiscount").is(":checked"))
        $.ajax({
            type: "GET",
            url: "/ControlPanel/Discount/Discounts",
            data: { id: $('#Id').val() },
            success: function (data) {
                $('#Discounts').html(data);
                SetAcceptedDiscounts($("#Id").val());
            }
        });
    else
        $('#Discounts').empty();
}

function SetAcceptedDiscounts(id) {
    if ($("#AcceptedAnotherDiscount").is(":checked"))
        $.ajax({
            type: "GET",
            url: "/ControlPanel/Discount/AcceptedDiscounts",
            data: { id: id },
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    arr[i] = data[i];
                    $(".DiscountId").map(function (y, x) {
                        if ($(".DiscountId:eq(" + y + ")").val() == data[i])
                            $(".DiscountId:eq(" + y + ")").parent().find("input[type='checkbox']").prop('checked', true);
                    })
                }
            }, error: function (data) {
                toastr.error(ErrorMessage);
            },
        });
    else
        arr = [];
}

function GetAcceptedDiscountForDetials() {
    if ($("#AcceptedAnotherDiscount").is(":checked"))
        $.ajax({
            type: "GET",
            url: "/ControlPanel/Discount/DiscountsForDetials",
            data: { id: $('#Id').val() },
            success: function (data) {
                $('#Discounts').html(data);
            }
        });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Discount/ShowTable/",
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

function ShowDetailsDiscount(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Discount/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#DetailsDiscount .modal-content').html(data);
            $('#DetailsDiscount').modal("show");
            GetAcceptedDiscountForDetials();
        }
    });
}


function ShowDeleteDiscount(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Discount/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#DeleteDiscount .modal-content').html(data);
            $('#DeleteDiscount').modal("show");
            GetAcceptedDiscountForDetials();
        }
    });
}

function ConfirmDelete() {
    ShowSpinner();
    $.post("/ControlPanel/Discount/DeleteDiscount/",
        { id: $("#Id").val() },
        function (data) {
            toastr.success(SuccesDelete);
            $('#DeleteDiscount').modal("hide");
            HideSpinner();
            Get()
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChanged(langId) {
    ShowEditDiscount($('#DiscountId').val(), langId.value);
}


function ShowEditDiscount(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Discount/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#EditDiscount .modal-content').html(data);
            $('#EditDiscount').modal("show");
            GetAcceptedDiscount();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CheckboxClicked(current) {
    arr.map(function (x) {
        if ($(current).parent().parent().find(".DiscountId").val() == x)
            arr = arr.filter(e => e !== x)
    })
    if ($(current).is(':checked'))
        arr.push($(current).parent().parent().find(".DiscountId").val())
}

function AddDiscount() {
    $("#Discount-Create").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            Name: "required",
            Percent: {
                min: 1,
                max: 100,
                required: true,
                number: true
            },
            TypeId: "required",
        }
    })

    let obj = {
        LanguageId: $("#LanguageId").val(),
        Name: $("#Name").val(),
        Percent: $("#Percent").val(),
        TypeId: $("#TypeId").val(),
        AcceptedAnotherDiscount: $("#AcceptedAnotherDiscount").is(":checked"),
        Ids: arr,
    }

    if ($("#Discount-Create").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/ControlPanel/Discount/CreateDiscount/",
            data: { discountViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $('#CreateDiscount').modal("hide");
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

function EditDiscount() {
    $("#Discount-Edit").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            Name: "required",
            Percent: "required",
            TypeId: "required",
        }
    })

    let obj = {
        Id: $("#Id").val(),
        LanguageId: $("#LanguageId").val(),
        Name: $("#Name").val(),
        Percent: $("#Percent").val(),
        TypeId: $("#TypeId").val(),
        AcceptedAnotherDiscount: $("#AcceptedAnotherDiscount").is(":checked"),
        Ids: arr,
    }

    if ($("#Discount-Edit").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/ControlPanel/Discount/EditDiscount/",
            data: { discountViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesEdit);
                $('#EditDiscount').modal("hide");
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

function GetData1(data) {
    $('#Discounts').html(data);
    SetAcceptedDiscounts($("#Id").val());
}

function ChangePage() {
    setTimeout(function () {
        SetAcceptedDiscounts($("#Id").val());
    }, 200);
}

$('#CreateDiscount,#EditDiscount,#DetailsDiscount,#DeleteDiscount').on('hidden.bs.modal', function () {
    $("#CreateDiscountDiv").empty();
    $("#EditDiscountDiv").empty();
    $("#DetailsDiscountDiv").empty();
    $("#DeleteDiscountDiv").empty();
})


$('#EditDiscount,#CreateDiscount').on('hidden.bs.modal', function () {
    arr = []
})
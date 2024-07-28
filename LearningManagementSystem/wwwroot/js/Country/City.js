function GetCountryCities() {
    var CountryId = $("#Input_CountryId").val();
    if (CountryId != '' && CountryId != undefined) {
        $.ajax({
            type: "POST",
            url: "/ControlPanel/Country/GetCountryCities",
            dataType: 'json',
            cache: false,
            data: { id: CountryId },
            success: function (data) {
                $("#Input_CityId").empty();
                $("#Input_CityId").append(data.result);
                $('#Input_CityId').trigger('chosen:updated');
            }
        });
    } else {
        $("#Input_CityId").empty();
        $('#Input_CityId').trigger('chosen:updated');

    }
}
function GetCountryCities2() {
    var CountryId = $("#CountryId").val();
    if (CountryId != '' && CountryId != undefined) {
        $.ajax({
            type: "POST",
            url: "/Home/GetCountryCities",
            dataType: 'json',
            data: { id : CountryId },
            success: function (data) {
                console.log(data)
                $("#CityId").empty();
                $("#CityId").append(data.result);
                $('#CityId').trigger('chosen:updated');
            }
        });
    } else {
        $("#CityId").empty();
        $('#CityId').trigger('chosen:updated');

    }
}

function GetCountryCities3() {
    var CountryId = $("#ContactViewModel_CountryId").val();
    debugger;
    if (CountryId != '' && CountryId != undefined) {
        $.ajax({
            type: "POST",
            url: "/ControlPanel/Country/GetCountryCities",
            dataType: 'json',
            cache: false,
            data: { id: CountryId },
            success: function (data) {
                $("#ContactViewModel_CityId").empty();
                $("#ContactViewModel_CityId").append(data.result);
                $('#ContactViewModel_CityId').trigger('chosen:updated');
            }
        });
    } else {
        $("#ContactViewModel_CityId").empty();
        $('#ContactViewModel_CityId').trigger('chosen:updated');

    }
}

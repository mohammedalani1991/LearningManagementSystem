function GetSystemGroupUsers(id) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/SystemGroups/GetUsers",
        data: {
            id: id
        },
        dataType: "json",
        success: function (result) {
            var list = $(".UpdaterId");
            $.each(result, function () {
                var option = $("<option />").val(this.value).text(this.text);
                if (this.selected) {
                    option.attr('selected', 'selected');
                }
                list.append(option);
            });
            SetChoices();
        },
        error: function (req, status, error) {
            alert("Error while Getting Updater");
        }
    });
}
function SetChoices() {
    setTimeout(function () {
        var multipleCancelButton = new Choices(('.UpdaterId'), {
            removeItemButton: true,
            maxItemCount: 100,
            searchResultLimit: 100,
            renderChoiceLimit: 100,
            noResultsText: 'No results found',
            noChoicesText: 'No choices to choose from',
        });
    }, 500);
}
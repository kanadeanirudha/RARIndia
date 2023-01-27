var RARIndiaCommanFunction = {
    Initialize: function () {
        RARIndiaCommanFunction.constructor();
    },
    constructor: function () {
    },


    GetDepartmentByCentreCode: function () {
        var selectedItem = $("#SelectedCentreCode").val();
        if ($("#SelectedCentreCode").val() != "") {
            $.ajax({
                cache: false,
                type: "GET",
                url: "/GeneralDepartmentMaster/GetDepartmentsByCentreCode",
                data: { "centreCode": selectedItem },
                success: function (data) {
                    $("#SelectedDepartmentID").html("").html(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Departments.","error")
                }
            });
            //$('#btnCreate').hide();
        }
        else {
            $('#myDataTable tbody').empty();
        }
    },
}

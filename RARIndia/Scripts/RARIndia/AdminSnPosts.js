var AdminSnPosts = {
    Initialize: function () {
        AdminSnPosts.constructor();
    },
    constructor: function () {
    },
    LoadList: function (controllerName, methodName) {
        $('#DataTables_SearchById').val("")
        if ($("#SelectedCentreCode").val() == "") {
            RARIndiaNotification.DisplayNotificationMessage("Please select centre.", "error");
        }
        else if ($("#SelectedDepartmentID").val() === "") {
            RARIndiaNotification.DisplayNotificationMessage("Please select department.", "error");
        }
        else {
            RARIndiaDataTable.LoadList(controllerName, methodName);
        }
    },
}

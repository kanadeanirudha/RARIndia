var AdminRoleMaster = {
    Initialize: function () {
        AdminRoleMaster.constructor();
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
$("#MonitoringLevel").change(function () {
    var monitoringLevel = $("#MonitoringLevel").val();
    if (monitoringLevel == "Self") {
        $("#SelectedRoleWiseCentresDivId").hide();
        $("#SelectedCentreNameForSelfDivId").show();
    }
    else {
        $("#SelectedCentreNameForSelfDivId").hide();
        $("#SelectedRoleWiseCentresDivId").show();
    }
});
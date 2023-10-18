var GymUserRegistration = {
    Initialize: function () {
        GymUserRegistration.constructor();
    },
    constructor: function () {
    },
}
$("#GymMembershipPlanMasterId").change(function () {
    var gymMembershipPlanMasterId = $("#GymMembershipPlanMasterId").val();
    var gymPlanDurationId = $("#GymPlanDurationMasterId").val();
    if (gymMembershipPlanMasterId != "" && gymPlanDurationId != "") {
        var data = { gymMembershipPlanMasterId: gymMembershipPlanMasterId, gymPlanDurationId: gymPlanDurationId };
        $.ajax({
            type: "POST",
            url: "/GymUserRegistration/GetMembershipPlanDurationAmount",
            data: data,
            success: function (response) {
                $("#MembershipPlanAmount").val(response)
            },
            failure: function (response) {
                RARIndiaNotification.DisplayNotificationMessage("Failed to display record.", "error")
            },
            error: function (response) {
                RARIndiaNotification.DisplayNotificationMessage("Failed to display record.", "error")
            }
        });
    }
    else {
        $("#MembershipPlanAmount").val("0");
    }
});

$("#GymPlanDurationMasterId").change(function () {
    var gymMembershipPlanMasterId = $("#GymMembershipPlanMasterId").val();
    var gymPlanDurationId = $("#GymPlanDurationMasterId").val();
    if (gymMembershipPlanMasterId != "" && gymPlanDurationId != "") {
        var data = { gymMembershipPlanMasterId: gymMembershipPlanMasterId, gymPlanDurationId: gymPlanDurationId };
        $.ajax({
            type: "POST",
            url: "/GymUserRegistration/GetMembershipPlanDurationAmount",
            data: data,
            success: function (response) {
                $("#MembershipPlanAmount").val(response)
            },
            failure: function (response) {
                RARIndiaNotification.DisplayNotificationMessage("Failed to display record.", "error")
            },
            error: function (response) {
                RARIndiaNotification.DisplayNotificationMessage("Failed to display record.", "error")
            }
        });
    }
    else {
        $("#MembershipPlanAmount").val("0");
    }
});

$("#PreLaunchOrSpecialOffer").change(function () {
    $("#PreLaunchSpecialOfferAmount").val("0");
    if ($("#PreLaunchOrSpecialOffer").prop("checked") == true) {
        $('#PreLaunchSpecialOfferAmount').prop('readonly', false);
    }
    else {
        $('#PreLaunchSpecialOfferAmount').prop('readonly', true);
    }
});

function numberOnly(id) {
    // Get element by id which passed as parameter within HTML element event
    var element = document.getElementById(id);
    // This removes any other character but numbers as entered by user
    element.value = element.value.replace(/[^0-9]/gi, "");
}
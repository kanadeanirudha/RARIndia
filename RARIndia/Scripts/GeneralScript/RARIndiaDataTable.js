var RARIndiaDataTable = {
    Initialize: function () {
        RARIndiaDataTable.constructor();
    },
    constructor: function () {
    },

    // LoadList method is used to load List page
    LoadList: function (controllerName, methodName) {
        var dataTableModel = new Object();
        dataTableModel.SearchBy = $('#DataTables_SearchById').val();
        dataTableModel.SortByColumn = "";
        dataTableModel.SortBy = "";
        dataTableModel.PageIndex = 1 /*$('#txtName').val()*/;
        dataTableModel.PageSize = $('#DataTables_PageSizeId').val();
        $("#DataTables_PageSizeId").attr("disabled", true)
        $("#DataTables_SearchById").attr("disabled", true)
        $.ajax(
            {
                cache: false,
                type: "POST",
                dataType: "html",
                url: "/" + controllerName + "/" + methodName,
                data: JSON.stringify(dataTableModel),
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    //Rebind Grid Data
                    $('#DataTablesDivId').html(result);
                    $("#DataTables_PageSizeId").attr("disabled", false)
                    $("#DataTables_SearchById").attr("disabled", false)
                }
            });
    },
}

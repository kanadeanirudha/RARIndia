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
        dataTableModel.PageIndex = $('#DataTables_PageIndexId').val();
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

    // LoadList method is used to load List page
    LoadListFirst: function (controllerName, methodName) {
        debugger
        var dataTableModel = new Object();
        dataTableModel.SearchBy = $('#DataTables_SearchById').val();
        dataTableModel.SortByColumn = "";
        dataTableModel.SortBy = "";
        dataTableModel.PageIndex = 1;
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

    LoadListPrevious: function (controllerName, methodName) {
        debugger
        var dataTableModel = new Object();
        dataTableModel.SearchBy = $('#DataTables_SearchById').val();
        dataTableModel.SortByColumn = "";
        dataTableModel.SortBy = "";
        dataTableModel.PageIndex = $('#DataTables_PageIndexId').val() == "1" ? 1 : dataTableModel.PageIndex = parseInt($('#DataTables_PageIndexId').val()) - 1;
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
    // LoadList method is used to load List page
    LoadListLast: function (controllerName, methodName, pageSize) {
        var dataTableModel = new Object();
        dataTableModel.SearchBy = $('#DataTables_SearchById').val();
        dataTableModel.SortByColumn = "";
        dataTableModel.SortBy = "";
        dataTableModel.PageIndex = pageSize;
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

    LoadListNext: function (controllerName, methodName) {
        var dataTableModel = new Object();
        dataTableModel.SearchBy = $('#DataTables_SearchById').val();
        dataTableModel.SortByColumn = "";
        dataTableModel.SortBy = "";
        dataTableModel.PageIndex = parseInt($('#DataTables_PageIndexId').val()) + 1;
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

    LoadListSortBy: function (controllerName, methodName, e) {
        debugger
        var dataTableModel = new Object();
        dataTableModel.SearchBy = $('#DataTables_SearchById').val();
        dataTableModel.SortByColumn = "";
        dataTableModel.SortBy = "";
        dataTableModel.PageIndex = $('#DataTables_PageIndexId').val();
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

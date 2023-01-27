var RARIndiaDataTable = {
    Initialize: function () {
        RARIndiaDataTable.constructor();
    },
    constructor: function () {
    },

    // LoadList method is used to load List page
    LoadList: function (controllerName, methodName) {
        var dataTableModel = BindDataTableModel($('#DataTables_PageIndexId').val());
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
        var dataTableModel = BindDataTableModel(1);
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
        var PageIndex = $('#DataTables_PageIndexId').val() == "1" ? 1 : dataTableModel.PageIndex = parseInt($('#DataTables_PageIndexId').val()) - 1;
        var dataTableModel = BindDataTableModel(PageIndex);
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
        var dataTableModel = BindDataTableModel(pageSize);
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
        var PageIndex = parseInt($('#DataTables_PageIndexId').val()) + 1;
        var dataTableModel = BindDataTableModel(PageIndex);

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
        var dataTableModel = BindDataTableModel($('#DataTables_PageIndexId').val());
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

function BindDataTableModel(PageIndex) {
    var dataTableModel = new Object();
    dataTableModel.SearchBy = $('#DataTables_SearchById').val();
    dataTableModel.SortByColumn = "";
    dataTableModel.SortBy = "";
    dataTableModel.PageIndex = PageIndex;
    dataTableModel.PageSize = $('#DataTables_PageSizeId').val();
    $("#DataTables_PageSizeId").attr("disabled", true);
    $("#DataTables_SearchById").attr("disabled", true);
    return dataTableModel;
}

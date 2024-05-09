$(document).ready(function () {
    GetAllDropDowns();
    GetTransactionLst();
    // ddlItem();
    
});

var SaveTransaction = function () {
    debugger;
    var Transaction_id = $("#hdnid").val();
    var Item_id = $("#ddlItem").val();
    var Department_id = $("#ddlItem").val();
    var Vendor_id = $("#ddlItem").val();
    var Transaction_date = $("#txtTDate").val();
    var Quantity = $("#txtTQ").val();
    
    var model = {
        Transaction_id: Transaction_id,
        Item_id: Item_id,
        Transaction_date: Transaction_date,
        Quantity: Quantity,
        TransactionType: $("#ddltrantype").val(),
        Vendor_id: Vendor_id,
        Department_id: Department_id
    };

    $.ajax({
        url: "/Transactions/SaveTransaction",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            
                alert(response.Message);
         
        },
        error: function (error) {
            alert("Error: " + error.responseJSON.ErrorMessage);
        }
    });
};

var GetAllDropDowns = function () {
    //debugger;

    $.ajax({
        url: "/Transactions/GetAllDropDowns",
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var dept = "";                 
            var items = "";
            var vender = "";
            $("#ddlItem").empty();
            $("#ddlvender").empty();
            $("#ddldepartment").empty();
            items = "<option value = '' > Please Select</option >";
            $.each(response.model.items, function (index, elementValue) {
                items += " <option value='" + elementValue.ItemID + "'>" + elementValue.ItemName + "</option>";

            });
            dept = "<option value = '' > Please Select</option >";

            $.each(response.model.dept, function (index, elementValue) {
                dept += " <option value='" + elementValue.DepartmentID + "'>" + elementValue.DepartmentName + "</option>";

            });

            vender = "<option value = '' > Please Select</option >";

            $.each(response.model.vendors, function (index, elementValue) {
                vender += " <option value='" + elementValue.VendorID + "'>" + elementValue.VendorName + "</option>";

            });
            $("#ddlItem").append(items);
            $("#ddlvender").append(vender);
            $("#ddldepartment").append(dept);

            
        }
    });
};

var GetTransactionLst = function () {
   /* debugger*/

    $.ajax({
        url: "/Transactions/GetTransactionLst",
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblTransaction tbody").empty();

            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.Transaction_id +
                    "</td><td>" + elementValue.Item_name +
                    "</td><td>" + elementValue.Vendor_name +
                    "</td><td>" + elementValue.Department_name +
                    "</td><td>" + elementValue.TransactionType +
                    "</td><td>" + elementValue.Quantity +
                    "</td><td>" + elementValue.Transaction_date +
                    "</td><td><button type='button' class='btn btn-primary btn-sm' onclick='EditTransaction(" + elementValue.Transaction_id + ")'><i class='bi bi-pencil-square' aria-hidden='true'></i></button>&nbsp;&nbsp;<button type='button' class='btn btn-danger btn-sm' onclick='DeleteTransaction(" + elementValue.Transaction_id + ")'><i class='bi bi-trash-fill' aria-hidden='true'></i></button>&nbsp;<button type='button' class='btn btn-success btn-sm' onclick='DetailsById(" + elementValue.Transaction_id + ")'><i class='bi bi-eye-fill' aria-hidden='true'></i></button></td></tr>";

            });


            $("#tblTransaction tbody").append(html);
            //var DataTable = require('datatables.net');

            //let table = new DataTable('#myTable', {});
            $('#tblTransaction').DataTable();
        }
    });
};


var DeleteTransaction = function (Transaction_id) {
    var confirmed = confirm("Are you sure you want to delete?");

    if (!confirmed) {
        alert("Deletion cancelled.");
        return;
    }
    var model = { Transaction_id: Transaction_id };

    $.ajax({
        url: "/Transactions/DeleteTransaction",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response.Message);
            GetTransactionList();
        },
        error: function (error) {
            alert("Error: " + error.responseJSON.ErrorMessage);
        }
    });
};

var EditTransaction = function (Transaction_id) {
    debugger;
    var model = { Transaction_id: Transaction_id };

    $.ajax({
        url: "/Transactions/EditTransaction",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#hdnid").val(response.model.Transaction_id);
            $("#ddlItem").val(response.model.Item_id);
            $("#ddldepartment").val(response.model.Department_id);
            $("#ddlvender").val(response.model.Vendor_id);
            $("#txtT").val(response.model.TransactionType);
            $("#txtTQ").val(response.model.Quantity);


            var datetimeselected = new Date(response.model.Transaction_date);

            var day = ("0" + datetimeselected.getDate()).slice(-2);
            var month = ("0" + (datetimeselected.getMonth() + 1)).slice(-2);

            var sele_date = datetimeselected.getFullYear() + "-" + (month) + "-" + (day);
            $('#txtTDate').val(sele_date);

        }
    });
};
var DetailsById = function (Transaction_id) {
    //debugger;
    var model = { Transaction_id: Transaction_id }
    $.ajax({
        url: "/Transactions/EditTransaction",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#CategoryModal").modal('show');

            $("#Detail").empty();

            var html = "";
            html += "<div class='row'>";
            html += "<div class='col-sm-6'>";
            html += "<b>TransactionId:</b>&nbsp&nbsp&nbsp<span>" + response.model.Transaction_id + "</span>";
            html += "</br>";
            html += "<b>Item Name:</b>&nbsp&nbsp&nbsp<span>" + response.model.Item_name + "</span>";
            html += "</br>";
            html += "<b>Department Name:</b>&nbsp&nbsp&nbsp<span>" + response.model.Department_name + "</span>";
            html += "</br>";
            html += "<b>Vendor Name:</b>&nbsp&nbsp&nbsp<span>" + response.model.Vendor_name + "</span>";
            html += "</br>";
            html += "<b>Quntity:</b>&nbsp&nbsp&nbsp<span>" + response.model.Quantity + "</span>";
            html += "</br>";
            html += "<b>Transactin Date:</b>&nbsp&nbsp&nbsp<span>" + response.model.Transaction_date + "</span>";
            html += "</br>";
            html += "<b>transactinType:</b>&nbsp&nbsp&nbsp<span>" + response.model.TransactionType + "</span>";
            html += "</br>";
            html += "</br>";
            html += "</div>";
            html += "</div>";

            $("#Detail").append(html);
        }
    });
}





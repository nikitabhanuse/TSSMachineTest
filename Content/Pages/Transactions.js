$(document).ready(function () {
    GetTransactionList();
    ddlItem();
});

var SaveTransaction = function () {
    debugger;
    var Transaction_id = $("#hdnid").val();
    var Item_id = $("#ddlItem").val();
    //var Item_name = $("#txtname").val();
   // var Department_id = $("#ddlItem").val();
  //  var Vendor_id = $("#ddlItem").val();
    var Transaction_date = $("#txtTDate").val();
    var Quantity = $("#txtTQ").val();
    var model = {
        Transaction_id: Transaction_id,
        Item_id: Item_id,
        //Item_name: Item_name,
        Transaction_date: Transaction_date,
        Quantity: Quantity,
    };

    $.ajax({
        url: "/Transactions/SaveTransaction",
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

var GetTransactionList = function () {
    //debugger;

    $.ajax({
        url: "/Transactions/GetTransactionList",
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblTransaction tbody").empty();

            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.Item_name +
                    "</td><td>" + elementValue.TransactionType +
                    "</td><td>" + elementValue.Quantity +
                    "</td><td>" + elementValue.Transaction_date +
                    "</td><td><button type='button' class='btn btn-primary btn-sm' onclick='EditTransaction(" + elementValue.Transaction_id + ")'><i class='bi bi-pencil-square' aria-hidden='true'></i></button>&nbsp;&nbsp;<button type='button' class='btn btn-danger btn-sm' onclick='DeleteTransaction(" + elementValue.Transaction_id + ")'><i class='bi bi-trash-fill' aria-hidden='true'></i></button>&nbsp;</td></tr>";

            });


            $("#tblTransaction tbody").append(html);
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
    //debugger;
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
            $("#ddlItem").append("<p value='" + response.model.Item_id + "'>" + response.model.Item_name + "</p>");
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



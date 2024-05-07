
$(document).ready(function () {
    ItemList();
    ddlItem();
});

var SaveItem = function () {

    var Item_id = $("#hdnid").val();
    var Item_name = $("#txtname").val();
    var Category = $("#ddlCategory").val();
    var Rate = $("#txtrate").val();
    var Balance_quantity = $("#txtbalqty").val();

    var model = {
        Item_id: Item_id,
        Item_name: Item_name,
        Category: Category,
        Rate: Rate,
        Balance_quantity: Balance_quantity
    };

    //debugger;

    $.ajax({
        url: "/ItemMaster/SaveItem",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        //contentType: false,
        datType: "JSON",

        success: function (response) {
            alert(response.Message);
            //location.reload();
        }

    });

}

//.....list..........
var ItemList = function () {

    //debugger;
    $.ajax({
        url: "/ItemMaster/ItemList",
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            var html = "";
            $("#tblItem tbody").empty();
            $.each(response.model, function (Index, elementvalue) {
                html += "<tr><td>" + elementvalue.Item_id +
                    "</td><td>" + elementvalue.Item_name +
                    "</td><td>" + elementvalue.Category +
                    "</td><td>" + elementvalue.Rate +
                    "</td><td>" + elementvalue.Balance_quantity +
                    "</td><td><input type='button' value='delete' onclick='DeleteItem(" + elementvalue.Item_id + ")'></td><td><input type='button' value='Edit' onclick='EditItem(" + elementvalue.item_id + ")'></td></tr>";
            });

            $("#tblItem tbody").append(html);
        }

    });

}

var DeleteItem = function (Item_id) {
    var model = { Item_id: Item_id }

    $.ajax({
        url: "/ItemMaster/DeleteItem",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",

        dataType: "JSON",
        success: function (response) {
            alert(response.Message);
        }

    });

}

//var EditItem = function (Item_id) {
//    var model = { Item_id: Item_id }

//    debugger;

//    $.ajax({
//        url: "/ItemMaster/EditItem",
//        method: "post",
//        data: JSON.stringify(model),
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (response) {
//            $("#hdnid").val(response.Message.Item_id);
//            $("#txtname").val(response.Message.Item_name);
//            $("#txtcat").val(response.Message.Category);
//            $("#txtrate").val(response.Message.Rate);
//            $("#txtbalqty").val(response.Message.Balance_quantity);


//            //location.reload();
//        }

//    });
//}
var ddlItem = function () {
    debugger;
    $.ajax({
        url: "/ItemMaster/ItemList",
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#ddlItem").empty();

            $.each(response.model, function (index, elementValue) {
                html += " <option value='" + elementValue.Item_id + "'>" + elementValue.Item_name + "</option>";

            });


            $("#ddlItem").append(html);
        }
    });
};
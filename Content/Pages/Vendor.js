$(document).ready(function () {
    VendorList();
});


var SaveVendor = function () {

    var Vendor_id = $("#hdnid").val();
    var Vendor_name = $("#txtvendorname").val();

    var model = {
        Vendor_id: Vendor_id,
        Vendor_name: Vendor_name
    };

    //debugger;

    $.ajax({
        url: "/Vendor/SaveVendor",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        //contentType: false,
        datType: "JSON",

        success: function (response) {
            alert(response.Message);
            location.reload();
        }

    });

}

//.....list..........
var VendorList = function () {

    //debugger;
    $.ajax({
        url: "/Vendor/VendorList",
        method: "post",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {

            var html = "";
            $("#tblVendor tbody").empty();
            $.each(response.model, function (Index, elementvalue) {
                html +=
                    "<tr><td>" + elementvalue.Vendor_id +
                "</td><td>" + elementvalue.Vendor_name +

                    "</td><td><input type='button' value='delete' onclick='DeleteVendor(" + elementvalue.Vendor_id + ")'></td><td><input type='button' value='Edit' onclick='EditVendor(" + elementvalue.Vendor_id

                    + ")'></td></tr>";
            })

            $("#tblVendor tbody").append(html);
        }

    });

}

var DeleteVendor = function (Vendor_id) {
    var model = { Vendor_id: Vendor_id }

    $.ajax({
        url: "/Vendor/DeleteVendor",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",

        dataType: "JSON",
        success: function (response) {
            alert(response.Message);
        }

    });
}


var EditVendor = function (Vendor_id) {
    var model = { Vendor_id: Vendor_id }
    // debugger;

    $.ajax({
        url: "/Vendor/EditVendor",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (response) {
            $("#hdnid").val(response.Message.Vendor_id);
            $("#txtname").val(response.Message.Vendor_name);

            // location.reload();
        }

    });
}



$(document).ready(function () {
    DepartList();
});


var SaveDept = function () {
    var Department_id = $("#hdnid").val();
    var Department_name = $("#txtdeptname").val();

    var model = {

        Department_id: Department_id,
        Department_name: Department_name
    };

    //debugger;

    $.ajax({
        url: "/Department/SaveDept",
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

//....List.....
var DepartList = function () {
    $.ajax({
        url: "/Department/DepartList",
        method: "post",
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblDepartment tbody").empty();
            $.each(response.model, function (index, elementvalue) {
                html += "<tr><td>" + elementvalue.Department_id +
                    "</td><td>" + elementvalue.Department_name +

                    "</td><td><input type='button' value='delete' onclick='DeleteDepart(" + elementvalue.Department_id + ")'></td><td><input type='button' value='Edit' onclick='EditDepart(" + elementvalue.Department_id + ")'></td></tr > ";

            });
            $("#tblDepartment tbody").append(html);
        }
    })
}

//..........delete.......
var DeleteDepart = function (Department_id) {
    var model = { Department_id: Department_id };
    // debugger;

    $.ajax({
        url: "/Department/DeleteDepart",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (response) {
            alert("delete successfully");
        }

    });
}

//.........edit...........

var EditDepart = function (Department_id) {
    var model = { Department_id: Department_id };
    debugger;
    $.ajax({
        url: "/Department/EditDepart",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#hdnid").val(response.Message.Department_id);
            $("#txtdeptname").val(response.Message.Department_name);


        }
    });
}


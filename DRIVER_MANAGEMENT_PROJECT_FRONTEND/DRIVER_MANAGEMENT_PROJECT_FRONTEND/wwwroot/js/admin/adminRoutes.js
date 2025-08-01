$(document).ready(function () {
    var token = $('input[name="__RequestVerificationToken"]').val();

    if ($('#routesTable').length === 0) {
        console.error("Table not found!");
        return;
    }

    $('#routesTable').DataTable({
        "processing": true,
        "serverSide": false,
        "ajax": {
            "url": "/Admin/GetRoutesAjax",
            "type": "POST",
            "headers": {
                "RequestVerificationToken": token
            },
            "dataSrc": function (json) {
                console.log("Fetched data:", json);
                return json || [];
            }
        },
        "columns": [
            { "data": "id", "visible": false },
            {
                "data": "routeName",
                "render": function (data) {
                    return data !== null ? data : "-";
                }
            },
            {
                "data": "distance",
                "render": function (data) {
                    return data !== null ? data : "-";
                }
            },
            {
                "data": "duration",
                "render": function (data) {
                    return data !== null ? data : "-";
                }
            }

        ]
    });
});
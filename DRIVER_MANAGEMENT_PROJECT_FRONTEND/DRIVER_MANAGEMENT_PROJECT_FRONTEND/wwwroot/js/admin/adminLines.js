$(document).ready(function () {
    var token = $('input[name="__RequestVerificationToken"]').val();

    if ($('#linesTable').length === 0) {
        console.error("Table not found!");
        return;
    }

    $('#linesTable').DataTable({
        "processing": true,
        "serverSide": false,
        "ajax": {
            "url": "/Admin/GetLinesAjax",
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
                "data": "lineCode",
                "render": function (data) {
                    return data !== null ? data : "-";
                }
            },
            {
                "data": "lineName",
                "render": function (data) {
                    return data !== null ? data : "-";
                }
            },
            {
                "data": "isActive",
                "render": function (data) {
                    return data
                        ? '<button class="btn btn-sm btn-success btn-toggle-active">A</button>'
                        : '<button class="btn btn-sm btn-secondary btn-toggle-active">P</button>';
                }
            }

        ]
    });
});
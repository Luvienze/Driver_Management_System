$(document).ready(function () {
    var token = $('input[name="__RequestVerificationToken"]').val();

    if ($('#vehiclesTable').length === 0) {
        console.error("Table not found!");
        return;
    }

    $('#vehiclesTable').DataTable({
        "processing": true,
        "serverSide": false,
        "ajax": {
            "url": "/Admin/GetVehiclesAjax",
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
                "data": "doorNo",
                "render": function (data) {
                    return data ?? "-";
                }
            },
            {
                "data": "capacity", 
                "render": function (data) {
                    return data ?? "-";
                }
            },
            {
                "data": "fuelTank",
                "render": function (data) {
                    return data ?? "-";
                }
            },
            {
                "data": "plate",
                "render": function (data) {
                    return data ?? "-";
                }
            },
            {
                "data": "personOnFoot",
                "render": function (data) {
                    return data ?? "-";
                }
            },
            {
                "data": "personOnSit",
                "render": function (data) {
                    return data ?? "-";
                }
            },
            {
                "data": "suitableForDisabled",
                "render": function (data) {
                    if (data === true) {
                        return '<span class="badge bg-success">Suitable</span>';
                    } else if (data === false) {
                        return '<span class="badge bg-danger">Not Suitable</span>';
                    }
                    return "-";
                }
            },
            {
                "data": "modelYear",
                "render": function (data) {
                    return data ?? "-";
                }
            },
            { "data": "garageId", "visible": false },
            {
                "data": "garageName",
                "render": function (data) {
                    return data ?? "-";
                }
            },
            {
                "data": "status",
                "render": function (data) {
                    switch (data) {
                        case 0:
                            return '<span class="badge bg-success">READY FOR SERVICE</span>';
                        case 1:
                            return '<span class="badge bg-warning text-dark">MALFUNCTION</span>';
                        case 2:
                            return '<span class="badge bg-danger">DAMAGED</span>';
                        case 3:
                            return '<span class="badge bg-secondary">OUT OF SERVICE</span>';
                        case 4:
                            return '<span class="badge bg-info text-dark">UNDER MAINTENANCE</span>';
                        default:
                            return '<span class="badge bg-dark">Bilinmiyor</span>';
                    }
                }
            }
        ]

    });
});
function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(window.location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}

$(document).ready(function () {
    const registrationNumber = getUrlParameter('registrationNumber');

    if (!registrationNumber || registrationNumber.trim() === "") {
        console.error("Invalid registration number provided.");
        return;
    }

    const directionMap = {
        0: { text: "DEPARTURE", class: "badge bg-info" },
        1: { text: "ARRIVAL", class: "badge bg-warning" }
    };

    const statusMap = {
        0: { text: "COMPLETED", class: "badge bg-success" },
        1: { text: "IN PROGRESS", class: "badge bg-primary" },
        2: { text: "CANCELLED", class: "badge bg-danger" },
        3: { text: "PLANNED", class: "badge bg-secondary" }
    };

    $('#dailyTasksTable').DataTable({
        "destroy": true,
        "ajax": {
            "url": "/Driver/GetTasksByDriver",
            "type": "POST",
            "contentType": "application/json",
            "dataType": "json",
            "data": function () {
                return JSON.stringify(registrationNumber); 
            },
            "dataSrc": function (json) {
                return json || [];
            }
        },
        "columns": [
            { "data": "id", "className": "task-id-column" },
            { "data": "registrationNumber" },
            { "data": "doorNo" },
            { "data" : "routeId" , "visible" : false},
            { "data": "routeName" },
            { "data": "lineCode" },
            {
                "data": "direction",
                "render": function (data) {
                    const direction = directionMap[data];
                    return direction ? `<span class="${direction.class}">${direction.text}</span>` : "-";
                }
            },
            {
                "data": "dateOfStart",
                "render": function (data) {
                    return data ? new Date(data).toLocaleDateString('en-GB') : "-";
                }
            },
            {
                "data": "dateOfEnd",
                "render": function (data) {
                    return data ? new Date(data).toLocaleDateString('en-GB') : "-";
                }
            },
            {
                "data": "passengerCount",
                "render": function (data) {
                    return data !== null ? data : "-";
                }
            },
            {
                "data": "orerStart",
                "render": function (data) {
                    return data ? new Date(data).toLocaleString('en-GB') : "-";
                }
            },
            {
                "data": "orerEnd",
                "render": function (data) {
                    return data ? new Date(data).toLocaleString('en-GB') : "-";
                }
            },
            {
                "data": "chiefStart",
                "render": function (data) {
                    return data ? new Date(data).toLocaleString('en-GB') : "-";
                }
            },
            {
                "data": "chiefEnd",
                "render": function (data) {
                    return data ? new Date(data).toLocaleString('en-GB') : "-";
                }
            },
            {
                "data": "status",
                "render": function (data) {
                    const status = statusMap[data];
                    return status ? `<span class="${status.class}">${status.text}</span>` : "-";
                }
            }
        ]
    });
});

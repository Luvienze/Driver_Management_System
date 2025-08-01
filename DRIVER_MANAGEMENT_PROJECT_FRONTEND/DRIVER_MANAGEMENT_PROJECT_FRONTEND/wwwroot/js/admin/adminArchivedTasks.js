$(document).ready(function () {
    var token = $('input[name="__RequestVerificationToken"]').val();

    const Directions = {
        0: "DEPARTURE",
        1: "ARRIVAL"
    }

    const Status = {
        0: "COMPLETED",
        1: "IN PROGRESS",
        2: "CANCELLED",
        3: "PLANNED"
    }

    if ($('#archivedTasksTable').length === 0) {
        console.error("Table not found!");
        return;
    }

    $('#archivedTasksTable').DataTable({
        "processing": true,
        "serverSide": false,
        "ajax": {
            "url": "/Admin/GetArchivedTasksAjax",
            "type": "POST",
            "headers": {
                "RequestVerificationToken": token
            },
            "dataSrc": function (json) {
                // console.log("Tasks:", json);
                return json || [];
            }
        },
        "columns": [
            { "data": "id", "visible": false },
            {
                "data": "registrationNumber",
                "render": function (data, type, row) {
                    return `
                    <span class="clickable-cell text-primary fw-semibold"
                          data-type="registrationNumber"
                          data-value="${data}"
                          style="cursor: pointer;">
                        ${data}
                    </span>
                `;
                }
            },
            {
                "data": "doorNo",
                "render": function (data, type, row) {
                    return `
                    <span class="clickable-cell text-primary fw-semibold"
                          data-type="doorNo"
                          data-value="${data}"
                          style="cursor: pointer;">
                        ${data}
                    </span>
                `;
                }
            },
            { "data": "routeId", "visible": false },
            {
                "data": "routeName",
                "render": function (data, type, row) {
                    return `
                    <span class="clickable-cell text-primary fw-semibold"
                          data-type="routeName"
                          data-value="${data}"
                          style="cursor: pointer;">
                        ${data}
                    </span>
                `;
                }
            },
            {
                "data": "lineCode",
                "render": function (data, type, row) {
                    return `
                    <span class="clickable-cell text-primary fw-semibold"
                          data-type="lineCode"
                          data-value="${data}"
                          style="cursor: pointer;">
                        ${data}
                    </span>
                `;
                }
            },

            {
                "data": "direction",
                "render": function (data) {
                    const directionMap = {
                        0: { text: "DEPARTURE", class: "badge bg-info" },
                        1: { text: "ARRIVAL", class: "badge bg-warning" }
                    };

                    const direction = directionMap[data];
                    if (!direction) return "-";
                    return `<span class="${direction.class}">${direction.text}</span>`;
                }
            },
            {
                "data": "dateOfStart",
                "render": function (data) {
                    return data ? new Date(data).toLocaleString() : "-";
                }
            },
            {
                "data": "dateOfEnd",
                "render": function (data) {
                    return data ? new Date(data).toLocaleString() : "-";
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
                    return data ? new Date(data).toLocaleString() : "-";
                }
            },
            {
                "data": "orerEnd",
                "render": function (data) {
                    return data ? new Date(data).toLocaleString() : "-";
                }
            },
            {
                "data": "chiefStart",
                "render": function (data) {
                    return data ? new Date(data).toLocaleString() : "-";
                }
            },
            {
                "data": "chiefEnd",
                "render": function (data) {
                    return data ? new Date(data).toLocaleString() : "-";
                }
            },
            {
                "data": "status",
                "render": function (data) {
                    const statusMap = {
                        0: { text: "COMPLETED", class: "badge bg-success" },
                        1: { text: "IN PROGRESS", class: "badge bg-primary" },
                        2: { text: "CANCELLED", class: "badge bg-danger" },
                        3: { text: "PLANNED", class: "badge bg-secondary" }
                    };

                    const status = statusMap[data];
                    if (!status) return "-";
                    return `<span class="${status.class}">${status.text}</span>`;
                }
            }
        ]
    });
});
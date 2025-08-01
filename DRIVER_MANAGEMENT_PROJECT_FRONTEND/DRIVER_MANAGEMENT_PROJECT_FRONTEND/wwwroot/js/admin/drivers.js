function addSpacesToPascalCase(text) {
    if (!text) return '';
    return text.replace(/([a-z])([A-Z])/g, '$1 $2');
}

const dayOffEnum = {
    0: "Monday",
    1: "Tuesday",
    2: "Wednesday",
    3: "Thursday",
    4: "Friday",
    5: "Saturday",
    6: "Sunday"
};

const cadreEnum = {
    0: "Urban Public Transport",
    1: "Intercity Transport",
    2: "Corporate Shuttle Service",
    3: "Tourism and Charter Service",
    4: "Night Shift Operation",
    5: "Backup and Relief Staff",
    6: "Training and Evaluation",
    7: "VIP and Private Transport",
    8: "Municipal Staff Transport",
    9: "Airport Logistic Transfer"
};

$(document).ready(function () {

    $('#driverTable').DataTable({
        "processing": true,
        "serverSide": false,
        "ajax": {
            "url": "/Admin/GetDriversAjax",
            "type": "POST",
            "headers": {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            "dataSrc": ""
        },
        "columns": [
            { "data": "id", "visible": false },
            {
                "data": null,
                "render": function (data) {
                    return `${data.personFirstName} ${data.personLastName}`;
                }
            },
            { "data": "phoneNumber" },
            {
                "data": "registrationNumber",
                "render": function (data) {
                    return `<span class="clickable-cell text-primary fw-semibold"
                        style="cursor: pointer;">${data}</span>`;
                }
            },
            { "data": "garage" },
            {
                "data": null,
                "render": function (data) {
                    return `${data.chiefFirstName} ${data.chiefLastName}`;
                }
            },
            {
                "data": "cadre",
                "render": function (data) {
                    return cadreEnum[data] || "Unknown";
                }
            },
            {
                "data": "dayOff",
                "render": function (data) {
                    return dayOffEnum[data] || "Unknown";
                }
            },
            {
                "data": "isActive",
                "render": function (data) {
                    return data
                        ? '<button class="btn btn-sm btn-success btn-toggle-active">A</button>'
                        : '<button class="btn btn-sm btn-secondary btn-toggle-active">P</button>';
                }
            },
            {
                "data": "registrationNumber",
                "render": function (data, type, row) {
                        return `<div class="btn-group btn-group-sm" role="group">
                <button class="btn btn-info btnTaskInfo" data-id="${data}"><i class="fas fa-info-circle"></i></button>
                            </div>`;    
                },
                "orderable": false,
                "searchable": false
            }

        ]
    });

});

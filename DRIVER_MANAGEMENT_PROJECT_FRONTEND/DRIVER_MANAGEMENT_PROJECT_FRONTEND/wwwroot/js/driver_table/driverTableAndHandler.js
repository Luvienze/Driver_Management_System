function addSpacesToPascalCase(text) {
    if (!text) return '';
    return text.replace(/([a-z])([A-Z])/g, '$1 $2');
}

const dayOffEnum = {
    0: "Monday", 1: "Tuesday", 2: "Wednesday", 3: "Thursday",
    4: "Friday", 5: "Saturday", 6: "Sunday"
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

let driverTableInstance = null;

$(document).ready(async function () {
    const token = $('input[name="__RequestVerificationToken"]').val();
    const chiefRegNo = window.currentUserRegistrationNumber;
    window.currentChiefId = null;

    try {
        const chiefData = await fetch('/Chief/GetChiefByRegistrationNumber', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'X-Requested-With': 'XMLHttpRequest',
                'RequestVerificationToken': token
            },
            body: `registrationNumber=${encodeURIComponent(chiefRegNo)}`
        }).then(res => {
            if (!res.ok) throw new Error(`Could not fetch chief data: ${res.status}`);
            return res.json();
        });

        window.currentChiefId = chiefData.id;

        if ($('#driverTable').length === 0) {
            console.error("driverTable not found.");
            return;
        }

        if ($.fn.DataTable.isDataTable('#driverTable')) {
            $('#driverTable').DataTable().destroy();
        }

        driverTableInstance = $('#driverTable').DataTable({
            processing: true,
            serverSide: false,
            ajax: {
                url: "/Driver/GetDrivers",
                type: "POST",
                headers: { 'RequestVerificationToken': token },
                dataSrc: json => json || []
            },
            columns: [
                { data: "id", visible: false },
                {
                    data: null,
                    render: data => `${data.personFirstName} ${data.personLastName}`
                },
                { data: "phoneNumber" },
                {
                    data: "registrationNumber",
                    render: data => `<span class="clickable-cell text-primary fw-semibold"
                                        data-type="registrationNumber"
                                        data-value="${data}"
                                        style="cursor:pointer;">${data}</span>`
                },
                { data: "garage" },
                {
                    data: null,
                    render: data => `${data.chiefFirstName} ${data.chiefLastName}`
                },
                {
                    data: "cadre",
                    render: data => cadreEnum[data] || "Unknown"
                },
                {
                    data: "dayOff",
                    render: data => dayOffEnum[data] || "Unknown"
                },
                {
                    data: "isActive",
                    render: data => data
                        ? '<button class="btn btn-sm btn-success btn-toggle-active">A</button>'
                        : '<button class="btn btn-sm btn-secondary btn-toggle-active">P</button>'
                },
                {
                    data: "chiefId",
                    render: (data, type, row) => {
                        if (data === window.currentChiefId) {
                            return `
                                <div class="btn-group btn-group-sm">
                                    <button class="btn btn-warning btnEdit" data-id="${row.registrationNumber}">
                                        <i class="fas fa-edit"></i>
                                    </button>
                                    <button class="btn btn-danger btnDelete" data-id="${row.registrationNumber}">
                                        <i class="fas fa-trash-alt"></i>
                                    </button>
                                    <button class="btn btn-info btnTaskInfo" data-id="${row.registrationNumber}">
                                        <i class="fas fa-info-circle"></i>
                                    </button>
                                </div>`;
                        } else {
                            return '';
                        }
                    },
                    orderable: false,
                    searchable: false
                }
            ]
        });

        $('.modal').on('hidden.bs.modal', function () {
            if (driverTableInstance) {
                driverTableInstance.columns.adjust().draw(false);
            }
        });

        setupEventHandlers(driverTableInstance, token);

    } catch (error) {
        console.error('Could not fetch chief data:', error);
        alert('Could not fetch chief data. Please reload the page.');
    }
});

function setupEventHandlers(table, token) {
    function canCurrentChiefManage(driverChiefId) {
        return driverChiefId === window.currentChiefId;
    }

    let toggleButton = null;
    let deleteButton = null;

    const confirmModal = new bootstrap.Modal(document.getElementById('confirmActiveToggleModal'));

    $('#driverTable tbody').on('click', 'td', function (e) {
        if ($(e.target).closest('button').length > 0) return;

        const rowData = table.row($(this).closest('tr')).data();
        const colIdx = table.cell(this).index().column;
        const colName = table.column(colIdx).dataSrc();

        if (colName === 'registrationNumber') {
            openPersonCardModal(rowData.registrationNumber);
        }
    });

    $('#driverTable tbody').on('click', '.btn-toggle-active', function (e) {
        e.stopPropagation();

        const rowData = table.row($(this).closest('tr')).data();
        if (!canCurrentChiefManage(rowData.chiefId)) {
            alert("Not authorized to change active state of this driver.");
            return;
        }

        toggleButton = $(this);
        confirmModal.show();
    });

    $('#confirmToggleActiveBtn').on('click', function () {
        if (!toggleButton) return;

        const rowData = table.row(toggleButton.closest('tr')).data();
        if (!rowData) {
            alert('No driver found with given registration number!');
            return;
        }

        const registrationNumber = rowData.registrationNumber;

        $.ajax({
            url: '/Driver/ToggleActive',
            method: 'POST',
            data: { registrationNumber },
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            headers: { 'RequestVerificationToken': token },
            success: () => {
                confirmModal.hide();
                table.ajax.reload(null, false);
            },
            error: () => {
                alert('An error has occurred during changing state.');
            }
        });
    });

    $('#driverTable').on('click', '.btnDelete', function (e) {
        e.stopPropagation();

        const rowData = table.row($(this).closest('tr')).data();
        if (!canCurrentChiefManage(rowData.chiefId)) {
            alert("Not authorized to delete this driver.");
            return;
        }

        deleteButton = $(this);
        $('#confirmDeleteModal').modal('show');
    });

    $('#confirmDeleteBtn').on('click', function () {
        if (!deleteButton) return;

        const registrationNumber = deleteButton.data('id');

        $.ajax({
            url: '/Driver/Delete',
            method: 'POST',
            data: { registrationNumber },
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            headers: { 'RequestVerificationToken': token },
            success: () => {
                alert("Driver has been deleted.");
                $('#confirmDeleteModal').modal('hide');
                table.ajax.reload(null, false);
            },
            error: () => {
                alert('An error occurred during delete.');
            }
        });
    });

    $('#driverTable tbody').on('click', '.btnTaskInfo', function (e) {
        e.stopPropagation();

        const registrationNumber = $(this).data('id');
        if (!registrationNumber) {
            alert('Could not find registration number.');
            return;
        }
        window.openTaskListCardModal(registrationNumber);
    });

    $('#btnAddDriver').on('click', function (e) {
        e.stopPropagation();
        window.openAddDriverModal();
    });

    $('#driverTable tbody').on('click', '.btnEdit', function (e) {
        e.stopPropagation();

        const rowData = table.row($(this).closest('tr')).data();
        if (!rowData || !rowData.chiefId) {
            alert("Row data not found or chiefId is missing.");
            return;
        }
        if (!canCurrentChiefManage(rowData.chiefId)) {
            alert("Not authorized to edit this driver.");
            return;
        }

        const registrationNumber = $(this).data('id');
        if (!registrationNumber) {
            alert('Could not find registration number.');
            return;
        }
        window.openEditDriverModal(registrationNumber);
    });
}

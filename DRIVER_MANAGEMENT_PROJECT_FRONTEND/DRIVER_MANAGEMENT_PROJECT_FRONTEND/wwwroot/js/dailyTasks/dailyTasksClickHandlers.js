$(document).ready(function () {
    var table = $('#dailyTasksTable').DataTable();

    $('#dailyTasksTable tbody').on('click', 'td', function () {
        var rowData = table.row($(this).closest('tr')).data();
        var colIdx = table.cell(this).index().column;
        var colName = table.column(colIdx).dataSrc();

        switch (colName) {
            case 'doorNo':
                openVehicleCardModal(rowData.doorNo);
                break;

            case 'registrationNumber':
                openPersonCardModal(rowData.registrationNumber);
                break;

            case 'routeName':
                openRouteCardModal(rowData.routeName);
                break;

            case 'lineCode':
                openLineCardModal(rowData.lineCode);
                break;


            default:
                break;
        }
       
    })
    $('#btnAddTask').on('click', function (e) {
        e.stopPropagation();
        window.openAddTaskModal();
    });

    $('#dailyTasksTable tbody').on('click', '.btnEdit', function (e) {
        e.stopPropagation();

        const taskId = $(this).data('id');
        if (!taskId) {
            alert('Could not found task id.');
            return;
        }
        window.openEditTaskModal(taskId);
    });

    $('#dailyTasksTable').on('click', '.btnDelete', function (e) {
        e.stopPropagation();
        deleteButton = $(this);
        $('#confirmDeleteModal').modal('show');
    });

    $('#confirmDeleteBtn').on('click', function () {
        if (!deleteButton) return;

        var row = deleteButton.closest('tr');
        var table = $('#dailyTasksTable').DataTable();
        var rowData = table.row(row).data();

        var taskDto = {
            id: rowData.id, 
            registrationNumber: rowData.registrationNumber,
            doorNo: rowData.doorNo,
            routeId: rowData.routeId,
            routeName: rowData.routeName,
            lineCode: rowData.lineCode,
            direction: rowData.direction,
            dateOfStart: rowData.dateOfStart,
            dateOfEnd: rowData.dateOfEnd,
            passengerCount: rowData.passengerCount,
            orerStart: rowData.orerStart,
            orerEnd: rowData.orerEnd,
            chiefStart: rowData.chiefStart,
            chiefEnd: rowData.chiefEnd,
            status: 2
        };

        $.ajax({
            url: '/Task/UpdateTask',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(taskDto),
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                alert("Task has been cancelled.");
                $('#confirmDeleteModal').modal('hide');
                table.ajax.reload(null, false);
            },
            error: function () {
                alert('An error occurred during delete.');
            }
        });
    });

});
$(document).ready(async function () {
    const token = $('input[name="__RequestVerificationToken"]').val();
    
    var table = $('#driverTable').DataTable();

    $('#driverTable tbody').on('click', 'td', function (e) {
        if ($(e.target).closest('button').length > 0) {
            return;
        }

        var rowData = table.row($(this).closest('tr')).data();
        var colIdx = table.cell(this).index().column;
        var colName = table.column(colIdx).dataSrc();

        if (colName === 'registrationNumber') {
            openPersonCardModal(rowData.registrationNumber);
        }
    });
    
    $('#driverTable tbody').on('click', '.btnTaskInfo', function (e) {
        e.stopPropagation();

        var row = $(this).closest('tr');
        var rowData = table.row(row).data();

        const registrationNumber = $(this).data('id');
        if (!registrationNumber) {
            alert('Could not found registration number.');
            return;
        }
        window.openTaskListCardModal(registrationNumber);
    });

});

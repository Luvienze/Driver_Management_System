$(document).ready(function () {
    var table = $('#archivedTasksTable').DataTable();

    $('#archivedTasksTable tbody').on('click', 'td', function () {
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
    });
});
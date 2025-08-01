window.openEditTaskModal = async function (taskId) {
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
    if (!token || !taskId) {
        console.error("CSRF token or registration number is missing.");
        alert("An error has been occurred.");
        return;
    }
    var table = $('#dailyTasksTable').DataTable();
    const chiefRegNo = window.currentUserRegistrationNumber;
    function createModalHtml(contentHtml) {
        return `
        <div class="modal fade" id="editTaskModal" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Edit Task</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" id="editTaskModalCloseBtn"></button>
                    </div>
                    <div class="modal-body">
                        ${contentHtml}
                    </div>
                    <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" id="editTaskModalCancelBtn">Close</button>
                        <button type="button" class="btn btn-primary" id="saveTaskBtn">Save</button>
                    </div>
                </div>
            </div>
        </div>`;
    }
    function combineTodayWithTime(timeStr) {
        if (typeof timeStr !== 'string' || !timeStr.includes(':')) return null;

        const now = new Date();
        const [hours, minutes] = timeStr.split(':');
        now.setHours(parseInt(hours));
        now.setMinutes(parseInt(minutes));
        now.setSeconds(0);
        now.setMilliseconds(0);

        // YYYY-MM-DDTHH:mm:ss
        const year = now.getFullYear();
        const month = String(now.getMonth() + 1).padStart(2, '0');
        const day = String(now.getDate()).padStart(2, '0');
        const hour = String(now.getHours()).padStart(2, '0');
        const minute = String(now.getMinutes()).padStart(2, '0');
        const second = String(now.getSeconds()).padStart(2, '0');

        return `${year}-${month}-${day}T${hour}:${minute}:${second}`;
    }
    function extractTimeFromISO(isoString) {
        if (!isoString) return '';
        const date = new Date(isoString);
        const hours = String(date.getHours()).padStart(2, '0');
        const minutes = String(date.getMinutes()).padStart(2, '0');
        return `${hours}:${minutes}`;
    }
   
    try {
        const modalRes = await fetch('/Task/GetEditTaskModalHtml', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'X-Requested-With': 'XMLHttpRequest',
                'RequestVerificationToken': token
            },
            body: new URLSearchParams({ taskId })
        });

        if (!modalRes.ok) throw new Error(`Modal HTML could not load: ${modalRes.status}`);
        const modalHtml = await modalRes.text();

        document.getElementById("editTaskModal")?.remove();
        document.body.insertAdjacentHTML("beforeend", createModalHtml(modalHtml));
        const modalInstance = new bootstrap.Modal(document.getElementById("editTaskModal"));
        modalInstance.show();

        // FETCH TASK BY ID IN ROW
        const taskRes = await fetch(`/Task/GetTaskById?taskId=${taskId}`, {
            method: 'POST',
            headers: {
                'X-Requested-With': 'XMLHttpRequest',
                'RequestVerificationToken': token
            }
        });
        const task = await taskRes.json();

        // SET VALUES FROM TABLE AT START
        $('#orerStart').val(extractTimeFromISO(task.orerStart));
        $('#orerEnd').val(extractTimeFromISO(task.orerEnd));
        $('#passengerCount').val(task.passengerCount);
        $('#direction').val(task.direction);

        // GET CHIEF BY LOGIN CREDENTIALS
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

        const chiefId = chiefData?.id;
        if (!chiefId) {
            throw new Error("Chief ID is not found in chiefData");
        }

        const formDataChief = new FormData();
        formDataChief.append("Id", chiefId);

        const formDataVehicle = new FormData();
        formDataVehicle.append("chief", chiefId);

        const [chiefs, vehicles, routes, lines] = await Promise.all([

            fetch('/Chief/GetActiveDrivers', {
                method: 'POST',
                body: formDataChief,
                headers: {
                    'X-Requested-With': 'XMLHttpRequest',
                    'RequestVerificationToken': token
                }
            }).then(r => r.json()),

            fetch('/Vehicle/GetVehicleListByChief', {
                method: 'POST',
                body: formDataVehicle,
                headers: {
                    'X-Requested-With': 'XMLHttpRequest',
                    'RequestVerificationToken': token
                }
            }).then(r => r.json()),

            fetch('/Route/GetRouteList', {
                method: 'POST',
                body: '{}',
                headers: {
                    'Content-Type': 'application/json',
                    'X-Requested-With': 'XMLHttpRequest',
                    'RequestVerificationToken': token
                }
            }).then(r => r.json()),

            fetch('/Line/GetLineList', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'X-Requested-With': 'XMLHttpRequest',
                    'RequestVerificationToken': token
                }
            }).then(r => r.json())
        ]);

        // SET DATA TO MODAL FIELDS

        chiefs.forEach(chief => {
            $('#registrationNumber').append(`<option value="${chief.registrationNumber}">
        ${chief.personFirstName} ${chief.personLastName}</option>`);
        });

        vehicles.forEach(vehicle => {
            $('#doorNo').append(`<option value="${vehicle.doorNo}">${vehicle.doorNo}</option>`);
        });

        routes.forEach(route => {
            $('#routeId').append(`<option value="${route.id}">${route.routeName}</option>`);
        });

        lines.forEach(line => {
            $('#lineCode').append(`<option value="${line.lineCode}">${line.lineCode}</option>`);
        });

        $('#registrationNumber').val(task.registrationNumber);
        $('#doorNo').val(task.doorNo);
        $('#routeId').val(task.routeId);
        $('#lineCode').val(task.lineCode);
        $('#status').val(task.status);

        document.getElementById("editTaskModalCloseBtn").addEventListener('click', () => {
            modalInstance.hide();
            location.reload();
        });

        document.getElementById("editTaskModalCancelBtn").addEventListener('click', () => {
            modalInstance.hide();
            location.reload();
        });

        $('#saveTaskBtn').off('click').on('click', function () {
            var token = $('input[name="__RequestVerificationToken"]').val();

            const chiefStartTime = $('#chiefStart').val();
            const chiefEndTime = $('#chiefEnd').val();

            if (chiefStartTime && chiefEndTime && chiefStartTime >= chiefEndTime) {
                alert('Chief başlangıç zamanı, bitiş zamanından büyük veya eşit olamaz.');
                return;
            }

            const taskDto = {
                id: taskId,
                registrationNumber: $('#registrationNumber').val(),
                doorNo: $('#doorNo').val(),
                routeId: parseInt($('#routeId').val()),
                routeName: $('#routeId option:selected').text(),
                lineCode: $('#lineCode').val(),
                direction: parseInt($('#direction').val()) || 0,
                dateOfStart: chiefStartTime ? combineTodayWithTime(chiefStartTime) : combineTodayWithTime($('#orerStart').val()),
                dateOfEnd: chiefEndTime ? combineTodayWithTime(chiefEndTime) : combineTodayWithTime($('#orerEnd').val()),
                orerStart: combineTodayWithTime($('#orerStart').val()),
                orerEnd: combineTodayWithTime($('#orerEnd').val()),
                passengerCount: parseInt($('#passengerCount').val()) || 0,
                chiefStart: combineTodayWithTime(chiefStartTime) || null,
                chiefEnd: combineTodayWithTime(chiefEndTime) || null,
                status: parseInt($('#status').val()) || 0
            };

            //console.log('JSON sent to /Task/UpdateTask', JSON.stringify(taskDto, null, 2));

            $.ajax({
                url: '/Task/UpdateTask',
                type: 'POST',
                contentType: 'application/json',
                headers: { 'RequestVerificationToken': token },
                data: JSON.stringify(taskDto),
                success: function () {
                    alert('Task has been updated succesfully!');
                    modalInstance.hide();
                    table.ajax.reload();
                },
                error: function (xhr) {
                    console.error('Error on updating task:', xhr.responseText);
                    alert('An error has been occurred during updating task!');
                }
            });
        });

        document.getElementById("editTaskModal").addEventListener('hidden.bs.modal', function () {
            this.remove();
        });

    } catch (error) {
        console.error("An error has been occurred loading Modal HTML:", error);
        alert("Error: " + error.message);
    }
};

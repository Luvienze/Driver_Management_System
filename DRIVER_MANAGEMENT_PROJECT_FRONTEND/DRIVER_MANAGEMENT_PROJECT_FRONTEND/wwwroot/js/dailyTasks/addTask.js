window.openAddTaskModal = async function () {
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
    const chiefRegNo = window.currentUserRegistrationNumber;
    var table = $('#dailyTasksTable').DataTable();
    function createModalHtml(contentHtml) {
        return `
    <div class="modal fade" id="addTaskModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add New Task</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" id="addTaskModalCloseBtn"></button>
                </div>
                <div class="modal-body">
                    ${contentHtml}
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" id="addTaskModalCancelBtn">Close</button>
                    <button type="button" class="btn btn-primary" id="saveTaskBtn">Save</button>
                </div>
            </div>
        </div>
    </div>`;
    }
    function combineTodayWithTime(timeStr) {
        if (!timeStr) return null;
        const now = new Date();
        const [hours, minutes] = timeStr.split(':');
        now.setHours(parseInt(hours));
        now.setMinutes(parseInt(minutes));
        now.setSeconds(0);
        now.setMilliseconds(0);

        const year = now.getFullYear();
        const month = String(now.getMonth() + 1).padStart(2, '0');
        const day = String(now.getDate()).padStart(2, '0');
        const hour = String(now.getHours()).padStart(2, '0');
        const minute = String(now.getMinutes()).padStart(2, '0');
        const second = String(now.getSeconds()).padStart(2, '0');

        return `${year}-${month}-${day}T${hour}:${minute}:${second}`;
    }
    function isOverlapping(existingStart, existingEnd, newStart, newEnd) {
        return (
            (newStart >= existingStart && newStart < existingEnd) ||
            (newEnd > existingStart && newEnd <= existingEnd) ||
            (newStart <= existingStart && newEnd >= existingEnd)
        );
    }
    function showInfoModal(title, message) {
        const titleEl = document.getElementById('infoModalTitle');
        const bodyEl = document.getElementById('infoModalBody');
        const modalEl = document.getElementById('infoModal');

        if (!titleEl || !bodyEl || !modalEl) {
            console.error('Could not foud info modal elements!');
            return;
        }

        titleEl.textContent = title;
        bodyEl.textContent = message;

        modalEl.style.zIndex = 1060;
        const infoModal = new bootstrap.Modal(modalEl);

        infoModal.show();

        document.getElementById("addTaskModalCloseBtn").addEventListener('click', () => {
            modalInstance.hide();
        });

        document.getElementById("addTaskModalCancelBtn").addEventListener('click', () => {
            modalInstance.hide();
        });
    }

    try {
        const modalRes = await fetch('/Task/GetAddTaskModalHTML', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'X-Requested-With': 'XMLHttpRequest',
                'RequestVerificationToken': token
            }
        });

        if (!modalRes.ok) throw new Error(`Modal HTML could not load: ${modalRes.status}`);
        const modalHtml = await modalRes.text();

        document.getElementById("addTaskModal")?.remove();
        document.body.insertAdjacentHTML("beforeend", createModalHtml(modalHtml));
        const modalInstance = new bootstrap.Modal(document.getElementById("addTaskModal"));
        modalInstance.show();

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

        // FETCH DAILY TASK TABLE TO CHECK TASKS START DATE
        const dailyTasks = await fetch('/Task/GetDailyTasks', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-Requested-With': 'XMLHttpRequest',
                'RequestVerificationToken': token
            }
        }).then(r => r.json());
    
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
        $('#registrationNumber').empty();
        $('#registrationNumber').append(`<option value="" disabled selected>Choose Driver</option>`);

        chiefs.forEach(driver => {
            const fullName = `${driver.personFirstName} ${driver.personLastName}`;
            $('#registrationNumber').append(
                `<option value="${driver.registrationNumber}">${fullName}</option>`
            );
        });

        $('#doorNo').empty();
        vehicles.forEach(vehicle => $('#doorNo').append(`<option value="${vehicle.doorNo}">${vehicle.doorNo}</option>`));

        $('#routeId').empty();
        $('#routeId').append(`<option value="" disabled selected>Choose Route</option>`);
        routes.forEach(route => {
            $('#routeId').append(`<option value="${route.id}">${route.routeName}</option>`);
        });

        $('#lineCode').empty();
        lines.forEach(line => $('#lineCode').append(`<option value="${line.lineCode}">${line.lineCode}</option>`));

        $('#passengerCount').val(0);
        document.getElementById("addTaskModalCloseBtn").addEventListener('click', () => {
            modalInstance.hide();
            location.reload();
        });

        document.getElementById("addTaskModalCancelBtn").addEventListener('click', () => {
            modalInstance.hide();
            location.reload();
        });

        $('#saveTaskBtn').off('click').on('click', function () {
            const registrationNumberEl = $('#registrationNumber');
            const doorNoEl = $('#doorNo');
            const routeIdEl = $('#routeId');
            const lineCodeEl = $('#lineCode');
            const orerStartEl = $('#orerStart');
            const orerEndEl = $('#orerEnd');

            const fields = [
                { el: registrationNumberEl, name: "Driver" },
                { el: doorNoEl, name: "Door No" },
                { el: routeIdEl, name: "Route" },
                { el: lineCodeEl, name: "Line Code" },
                { el: orerStartEl, name: "Orer Start" },
                { el: orerEndEl, name: "Orer End" },
            ];

            let hasError = false;

            $('.is-invalid').removeClass('is-invalid');
            $('.invalid-feedback').remove();

            fields.forEach(({ el, name }) => {
                if (!el.val()) {
                    el.addClass('is-invalid');
                    el.after(`<div class="invalid-feedback">${name} is required.</div>`);
                    hasError = true;
                }
            });

            const orerStart = combineTodayWithTime(orerStartEl.val());
            const orerEnd = combineTodayWithTime(orerEndEl.val());

            if (!hasError && new Date(orerStart) >= new Date(orerEnd)) {
                orerStartEl.addClass('is-invalid');
                orerEndEl.addClass('is-invalid');
                orerEndEl.after(`<div class="invalid-feedback">Start date must be earlier than end date.</div>`);
                hasError = true;
            }

            if (hasError) return;

            const newStart = new Date(orerStart);
            const newEnd = new Date(orerEnd);
            const selectedDriver = registrationNumberEl.val();

            const overlappingTask = dailyTasks.find(task => {
                if (task.registrationNumber !== selectedDriver) return false;

                const taskStart = new Date(task.dateOfStart);
                const taskEnd = new Date(task.dateOfEnd);

                return isOverlapping(taskStart, taskEnd, newStart, newEnd);
            });

            if (overlappingTask) {
                showInfoModal('Time Collapsed', `A task has already been assigned between ${overlappingTask.dateOfStart} - ${overlappingTask.dateOfEnd} hours.`);
                return;
            }

            var token = $('input[name="__RequestVerificationToken"]').val();

            const taskDto = {
                id: 0,
                registrationNumber: registrationNumberEl.val(),
                doorNo: doorNoEl.val(),
                routeId: parseInt(routeIdEl.val()),
                routeName: $('#routeId option:selected').text(),
                lineCode: lineCodeEl.val(),
                direction: parseInt($('#direction').val()) || 0,
                dateOfStart: orerStart,
                dateOfEnd: orerEnd,
                orerStart: orerStart,
                orerEnd: orerEnd,
                passengerCount: 0,
                chiefStart: null,
                chiefEnd: null,
                status: 3
            };

            console.log('JSON sent to /Task/AddNewTask', JSON.stringify(taskDto, null, 2));

            $.ajax({
                url: '/Task/AddNewTask',
                type: 'POST',
                contentType: 'application/json',
                headers: { 'RequestVerificationToken': token },
                data: JSON.stringify(taskDto),
                success: function () {
                    showInfoModal('Succesfull', 'Task has been added succesfully!');
                    modalInstance.hide();
                    setTimeout(() => location.reload(), 1500);
                    table.ajax.reload();
                },
                error: function (xhr) {
                    console.error('Error on adding new task:', xhr.responseText);
                    showInfoModal('Error', 'An error has been occurred adding task!');
                }

            });
        });

        document.getElementById("addTaskModal").addEventListener('hidden.bs.modal', function () {
            this.remove();
        });

    } catch (error) {
        console.error("An error has been occured loading modal", error);
        showInfoModal("Error", "Modal could not load: " + error.message);
    }

};



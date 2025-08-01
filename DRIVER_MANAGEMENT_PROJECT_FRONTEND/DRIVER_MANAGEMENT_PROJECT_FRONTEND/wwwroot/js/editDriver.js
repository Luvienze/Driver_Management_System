
window.openEditDriverModal = async function (registrationNumber) {
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
    if (!token || !registrationNumber) {
        console.error("CSRF token or registration number is missing.");
        alert("An error has been occurred.");
        return;
    }
    const table = $('#driverTable').DataTable();
    table.ajax.reload(null, false);

    function populateDriverForm(driver, chiefData) {
        $('#id').val(driver.id || '');
        $('#personFirstName').val(driver.personFirstName || '');
        $('#personLastName').val(driver.personLastName || '');
        $('#phoneNumber').val(driver.phoneNumber || '');
        $('#registrationNumber').val(driver.registrationNumber || '');
        $('#cadre').val(Number.isInteger(driver.cadre) && driver.cadre >= 0 && driver.cadre <= 9 ? driver.cadre : 0);
        $('#dayOff').val(Number.isInteger(driver.dayOff) && driver.dayOff >= 0 && driver.dayOff <= 6 ? driver.dayOff : 0);
        $('#isActive').val(driver.active?.toString() || 'false');
        $('#editDriverGarage').val(chiefData?.garageName || chiefData?.garageName || '');
        $('#chiefName').val((chiefData?.chiefFirstName || driver.chiefFirstName || '') + ' ' + (chiefData?.chiefLastName || driver.chiefLastName || ''));
        $('#chiefId').val(chiefData?.id || driver.chiefId || 0);
        $('#isActive').prop('checked', driver.isActive === 1 || driver.isActive === true);
    }

    function createModalHtml(contentHtml) {
        return `
        <div class="modal fade" id="editDriverModal"tabindex="-1" >
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Update Driver Info</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"  id="editModalCloseBtn" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        ${contentHtml}
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"  id="editModalCancelBtn">Close</button>
                        <button type="button" class="btn btn-primary" id="saveDriverBtn">Save</button>
                    </div>
                </div>
            </div>
        </div>`;
    }

    try {

        const modalRes = await fetch('/Driver/GetDriverModalHtml', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'X-Requested-With': 'XMLHttpRequest',
                'RequestVerificationToken': token
            },
            body: new URLSearchParams({ registrationNumber })
        });
        document.getElementById("editDriverModal")?.remove();

        if (!modalRes.ok) throw new Error(`Could not load Modal HTML: ${modalRes.status}`);
        const modalHtml = await modalRes.text();

        document.body.insertAdjacentHTML("beforeend", createModalHtml(modalHtml));

        const modalElement = document.getElementById("editDriverModal");

        const modalInstance = new bootstrap.Modal(modalElement);

        modalInstance.show();

        const [chiefData, driverData] = await Promise.all([
            fetch('/Chief/GetPersonChiefByRegistrationNumber', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'X-Requested-With': 'XMLHttpRequest',
                    'RequestVerificationToken': token
                },
                body: new URLSearchParams({ registrationNumber })
            }).then(res => { if (!res.ok) throw new Error(`Could not fetch chief data: ${res.status}`); return res.json(); }),
            fetch('/Driver/GetDriverByRegistrationNumber', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'X-Requested-With': 'XMLHttpRequest',
                    'RequestVerificationToken': token
                },
                body: new URLSearchParams({ registrationNumber })
            }).then(r => { if (!r.ok) throw new Error(`Could not fetch driver data: ${r.status}`); return r.json(); })
        ]);

        document.getElementById("editModalCloseBtn").addEventListener('click', () => {
            modalInstance.hide();
            $('#editDriverForm')[0].reset();
            table.ajax.reload(null, false);
        });

        document.getElementById("editModalCancelBtn").addEventListener('click', () => {
            modalInstance.hide();
            $('#editDriverForm')[0].reset();
            table.ajax.reload(null, false);
        });

        populateDriverForm(driverData, chiefData);

        $('#saveDriverBtn').off('click').on('click', function () {
            const form = $('#editDriverForm')[0];
            if (!form.checkValidity()) {
                form.reportValidity();
                return;
            }

            const driverDto = {
                id: parseInt($('#id').val()) || driverData?.id || 0,
                personFirstName: $('#personFirstName').val() || '',
                personLastName: $('#personLastName').val() || '',
                phoneNumber: $('#phoneNumber').val() || '',
                registrationNumber: $('#registrationNumber').val() || '',
                garage: $('#editDriverGarage').val() || '',
                chiefId: parseInt($('#chiefId').val()) || driverData?.chiefId || 0,
                chiefFirstName: ($('#chiefName').val() || '').split(' ')[0] || '',
                chiefLastName: ($('#chiefName').val() || '').split(' ').slice(1).join(' ') || '',
                cadre: parseInt($('#cadre').val()) || 0,
                dayOff: parseInt($('#dayOff').val()) || 0,
                isActive: $('#isActive').is(':checked')
            };


            $.ajax({
                url: '/Driver/UpdateDriver',
                type: 'POST',
                contentType: 'application/json',
                headers: { 'RequestVerificationToken': token },
                data: JSON.stringify(driverDto),
                success: function (response) {
                    alert('Driver has been updated successfully!');
                    modalInstance.hide();
                    $('#editDriverForm')[0].reset();
                    table.ajax.reload(null, false);
                },
                error: function (xhr, status, error) {
                    console.error('Update driver error:', {
                        status: xhr.status,
                        responseText: xhr.responseText,
                        responseJSON: xhr.responseJSON,
                        sentData: driverDto
                    });
                    alert(`Update error: ${xhr.status} ${xhr.responseText}`);
                }
            });
        });

        document.getElementById("editDriverModal").addEventListener('hidden.bs.modal', function () {
            this.remove();
            $('#editDriverForm')[0].reset();
            table.ajax.reload(null, false);
            $('#driverTable').focus();
        });
    } catch (error) {
        console.error("An error has been occurred during loading modal:", error);
        alert("An error has been occurred: " + error.message);
    }
};

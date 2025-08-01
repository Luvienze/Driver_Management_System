window.openAddDriverModal = async function () {
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
    const chiefRegNo = window.currentUserRegistrationNumber;
    const table = $('#driverTable').DataTable();
    table.ajax.reload(null, false);

    function populateDriverForm(driver, chiefData) {
        $('#id').val(driver?.id || '');
        $('#firstName').val(driver?.personFirstName || '');
        $('#lastName').val(driver?.personLastName || '');
        $('#phone').val(driver?.phoneNumber || '');
        $('#registrationNumber').val(driver?.registrationNumber || '');
        $('#cadre').val(Number.isInteger(driver?.cadre) && driver.cadre >= 0 && driver.cadre <= 9 ? driver.cadre : 0);
        $('#dayOff').val(Number.isInteger(driver?.dayOff) && driver.dayOff >= 0 && driver.dayOff <= 6 ? driver.dayOff : 0);
        $('#isActive').val(driver?.active?.toString() || 'true');
        $('#addDriverGarage').val(chiefData?.garageName || chiefData?.garageName || '');
        $('#chiefId').val(chiefData?.id || driver?.chiefId || 0);
        $('#chiefFirstName').val(chiefData?.chiefFirstName || driver?.chiefFirstName || '');
        $('#chiefLastName').val(chiefData?.chiefLastName || driver?.chiefLastName || '');
        $('#isActive').prop('checked', driver?.isActive === 1 || driver?.isActive === true);
        $('#isDeleted').val(driver?.isDeleted?.toString() || 'false');
    }
    function createModalHtml(contentHtml) {
        return `
        <div class="modal fade" id="addDriverModal" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add New Driver</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" id="addModalCloseBtn"></button>
                    </div>
                    <div class="modal-body">
                        ${contentHtml}
                    </div>
                    <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" id="addModalCancelBtn">Close</button>
                        <button type="button" class="btn btn-primary" id="saveDriverBtn">Save</button>
                    </div>
                </div>
            </div>
        </div>`;
    }
    function capitalize(input) {
        if (!input) return '';
        const trimmed = input.trim();
        return trimmed.charAt(0).toUpperCase() + trimmed.slice(1).toLowerCase();
    }

    try {
        const modalRes = await fetch('/Driver/GetAddDriverModalHTML', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'X-Requested-With': 'XMLHttpRequest',
                'RequestVerificationToken': token
            }
        });

        if (!modalRes.ok) throw new Error(`Could not load Modal HTML: ${modalRes.status}`);
        const modalHtml = await modalRes.text();

        document.getElementById("addDriverModal")?.remove();

        document.body.insertAdjacentHTML("beforeend", createModalHtml(modalHtml));

        const modalInstance = new bootstrap.Modal(document.getElementById("addDriverModal"));

        modalInstance.show();

        $('#selectImageBtn').on('click', function () {
            $('#fileInput').click();
        });

        $('#fileInput').on('change', async function () {
            const file = this.files[0];
            if (file) {
                try {
                    const formData = new FormData();
                    formData.append('file', file);

                    const uploadResponse = await fetch('/Driver/UploadImage', {
                        method: 'POST',
                        body: formData
                    });

                    if (!uploadResponse.ok) throw new Error(`Upload failed with status ${uploadResponse.status}`);

                    const result = await uploadResponse.text();
                    $('#imageUrl').val(result);
                } catch (err) {
                    alert("Image upload failed: " + err.message);
                }
            }
        });

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

        document.getElementById("addModalCloseBtn").addEventListener('click', () => {
            modalInstance.hide();
            $('#addDriverForm')[0].reset();
            table.ajax.reload();
        });

        document.getElementById("addModalCancelBtn").addEventListener('click', () => {
            modalInstance.hide();
            $('#addDriverForm')[0].reset();
            table.ajax.reload(null, false);
        });

        populateDriverForm(null, chiefData);


        $('#saveDriverBtn').off('click').on('click', function () {
            const form = $('#addDriverForm')[0];
            if (!form.checkValidity()) {
                form.reportValidity();
                return;
            }
            const imageFile = $('#fileInput')[0].files[0];
            // CHECK IF DRIVER IS UNDER 18 YEARS OLD
            const dateOfBirthStr = $('#dateOfBirth').val();
            if (!dateOfBirthStr) {
                alert('Date of birth is required.');
                return;
            }

            const birthDate = new Date(dateOfBirthStr);
            const today = new Date();
            const age = today.getFullYear() - birthDate.getFullYear();
            const monthDiff = today.getMonth() - birthDate.getMonth();
            const dayDiff = today.getDate() - birthDate.getDate();
            const isUnder18 = age < 18 || (age === 18 && (monthDiff < 0 || (monthDiff === 0 && dayDiff < 0)));
            if (isUnder18) {
                alert('Driver must be at least 18 years old.');
                return;
            }
            console.log("imageUrl input değeri:", $('#imageUrl').val());

            const personDto = {
                id: 0,
                firstName: capitalize($('#firstName').val()),
                lastName: capitalize($('#lastName').val()),
                gender: parseInt($('#gender').val()) || 0,
                dateOfBirth: $('#dateOfBirth').val() ? new Date($('#dateOfBirth').val()).toISOString() : new Date().toISOString(),
                phone: $('#phone').val().trim() || '',
                address: $('#address').val().trim() || '',
                imageUrl: imageFile ? imageFile.name : null,
                bloodGroup: parseInt($('#bloodGroup').val()) || 0,
                registrationNumber: $('#registrationNumber').val().trim() || '',
                dateOfStart: $('#dateOfStart').val() ? new Date($('#dateOfStart').val()).toISOString() : new Date().toISOString(),
                isDeleted: $('#isDeleted').val() === 'true'
            };

            const driverDto = {
                id: 0,
                personFirstName: capitalize($('#firstName').val()),
                personLastName: capitalize($('#lastName').val()),
                phoneNumber: $('#phone').val().trim() || '',
                registrationNumber: $('#registrationNumber').val().trim() || '',
                garage: $('#addDriverGarage').val().trim() || '',
                chiefId: parseInt($('#chiefId').val()) || 0,
                chiefFirstName: capitalize($('#chiefFirstName').val()),
                chiefLastName: capitalize($('#chiefLastName').val()),
                cadre: parseInt($('#cadre').val()) || 0,
                dayOff: parseInt($('#dayOff').val()) || 0,
                isActive: $('#isActive').val() === 'true'
            };

            const personDriverRequestDto = {
                personDto: personDto,
                driverDto: driverDto
            };

            const formData = new FormData();
            formData.append('data', JSON.stringify(personDriverRequestDto)); 

            if (imageFile) {
                formData.append('file', imageFile);
            }

            console.log('JSON sent to /Driver/AddNewDriver', JSON.stringify(personDriverRequestDto, null, 2));

            $.ajax({
                url: '/Driver/AddNewDriver',
                type: 'POST',
                processData: false,
                contentType: false,
                headers: {
                    'RequestVerificationToken': token
                },
                data: formData,
                success: function (response) {
                    alert('Driver has been added successfully!');
                    modalInstance.hide();
                    $('#addDriverForm')[0].reset();
                    table.ajax.reload(null, false);
                },
                error: function (xhr) {
                    console.error('Error:', xhr);
                    alert(`Error: ${xhr.status} - ${xhr.responseText}`);
                }
            });
        });

        document.getElementById("addDriverModal").addEventListener('hidden.bs.modal', function () {
            this.remove();
            $('#addDriverForm')[0].reset();
            table.ajax.reload(null, false);
            $('#driverTable').focus();
        });

    } catch (error) {
        console.error("An error has occurred during loading modal:", error);
        alert("An error has occurred: " + error.message);
    }
};
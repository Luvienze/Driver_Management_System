window.openVehicleCardModal = function openVehicleCardModal(doorNo) {
    if (!doorNo) return;

    fetch('/Vehicle/GetVehicleCard', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: new URLSearchParams({ doorNo: doorNo })
    })
        .then(response => {
            if (!response.ok) throw new Error("An error has occurred.");
            return response.text();
        })
        .then(html => {
            let existingModal = document.getElementById("vehicleCardModal");
            if (existingModal) existingModal.remove();

            const modalHtml = `
        <div class="modal fade" id="vehicleCardModal" tabindex="-1" aria-labelledby="vehicleCardModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="vehicleCardModalLabel">Vehicle Card - ${doorNo}</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" id="vehicleModalCancelBtn"</button>
                    </div>
                    <div class="modal-body">
                        ${html}
                    </div>
                </div>
            </div>
        </div>`;

            document.body.insertAdjacentHTML('beforeend', modalHtml);

            const modal = new bootstrap.Modal(document.getElementById("vehicleCardModal"));
            modal.show();

            document.getElementById("vehicleModalCancelBtn").addEventListener('click', () => {
                modal.hide();
            });

            document.getElementById("vehicleCardModal").addEventListener('hidden.bs.modal', function () {
                this.remove();
            });
        })
        .catch(error => {
            console.error("Could not load vehicle card:", error);
            alert("Could not load vehicle card.");
        });
}

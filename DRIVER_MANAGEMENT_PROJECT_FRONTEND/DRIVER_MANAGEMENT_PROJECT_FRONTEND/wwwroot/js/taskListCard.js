window.openTaskListCardModal = function openTaskListCardModal(registrationNumber) {
    if (!registrationNumber) return;
    const table = $('#driverTable').DataTable();

    fetch('/Driver/GetTaskListCard', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: new URLSearchParams({ registrationNumber: registrationNumber })
    })
        .then(response => {
            if (!response.ok) throw new Error("An error has occurred.");
            return response.text();
        })
        .then(html => {
            document.getElementById("taskListCardModal")?.remove();
            document.getElementById("taskNotFoundModal")?.remove();

            if (!html.trim() || html.includes('id="taskNotFoundModal"')) {
                document.body.insertAdjacentHTML('beforeend', html);
                const notFoundModal = new bootstrap.Modal(document.getElementById("taskNotFoundModal"));
                notFoundModal.show();
                document.getElementById("taskNotFoundModal").addEventListener('hidden.bs.modal', function () {
                    this.remove();
                });
                return;
            }

            const modalHtml = `
        <div class="modal fade" id="taskListCardModal" tabindex="-1" aria-labelledby="taskListCardModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg" style="max-width: 900px;"> 
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="taskListCardModalLabel">Driver Task Card - ${registrationNumber}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" id="taskListCardModalCloseBtn" aria-label="Close"></button>
                    </div>
                    <div class="modal-body" style="overflow-x: hidden;"> 
                        ${html}
                    </div>
                </div>
            </div>
        </div>`;

            document.body.insertAdjacentHTML('beforeend', modalHtml);

            const modal = new bootstrap.Modal(document.getElementById("taskListCardModal"));
            modal.show();

            document.getElementById("taskListCardModalCloseBtn").addEventListener('click', () => {
                modal.hide();
                table.ajax.reload(null, false);
            });

            document.getElementById("taskListCardModalCloseBtn").addEventListener('hidden.bs.modal', function () {
                this.remove();
            });
        })
        .catch(error => {
            console.error("Could not load task card:", error);
            alert("Could not load task card.");
        });
};

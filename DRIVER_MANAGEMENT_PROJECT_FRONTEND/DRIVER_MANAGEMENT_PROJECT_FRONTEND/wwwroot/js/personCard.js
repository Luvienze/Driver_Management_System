window.openPersonCardModal = function openPersonCardModal(registrationNumber) {
    if (!registrationNumber) return;

    fetch('/Person/GetPersonCard', {
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
            let existingModal = document.getElementById("personCardModal");
            if (existingModal) existingModal.remove();

            const modalHtml = `
        <div class="modal fade" id="personCardModal" tabindex="-1" aria-labelledby="personCardModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="personCardModalLabel">Person Card - ${registrationNumber}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" id="personCardModalCancelBtn"</button>
                    </div>
                    <div class="modal-body">
                        ${html}
                    </div>
                </div>
            </div>
        </div>`;

            document.body.insertAdjacentHTML('beforeend', modalHtml);

            const modal = new bootstrap.Modal(document.getElementById("personCardModal"));
            modal.show();

            document.getElementById("personCardModalCancelBtn").addEventListener('click', () => {
                modal.hide(); 
            });

            document.getElementById("personCardModal").addEventListener('hidden.bs.modal', function () {
                this.remove();
            });
        })
        .catch(error => {
            console.error("Could not load person card:", error);
            alert("Could not load person card.");
        });
}

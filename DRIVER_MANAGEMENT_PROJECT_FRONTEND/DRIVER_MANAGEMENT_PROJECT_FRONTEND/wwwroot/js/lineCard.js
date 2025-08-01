window.openLineCardModal = function openLineCardModal(lineCode) {
    if (!lineCode) return;

    fetch('/Line/GetLineCard', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: new URLSearchParams({ lineCode: lineCode })
    })
        .then(response => {
            if (!response.ok) throw new Error("An error has occurred.");
            return response.text();
        })
        .then(html => {
            let existingModal = document.getElementById("lineCardModal");
            if (existingModal) existingModal.remove();

            const modalHtml = `
        <div class="modal fade" id="lineCardModal" tabindex="-1" aria-labelledby="lineCardModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="lineCardModalLabel">Line Card - ${lineCode}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        ${html}
                    </div>
                </div>
            </div>
        </div>`;

            document.body.insertAdjacentHTML('beforeend', modalHtml);

            const modal = new bootstrap.Modal(document.getElementById("lineCardModal"));
            modal.show();

            document.getElementById("lineCardModal").addEventListener('hidden.bs.modal', function () {
                this.remove();
            });
        })
        .catch(error => {
            console.error("Could not load line card:", error);
            alert("Could not load line card.");
        });
}

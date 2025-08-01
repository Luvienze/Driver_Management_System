window.openRouteCardModal = function openRouteCardModal(routeName) {
    if (!routeName) return;

    fetch('/Route/GetRouteCard', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: new URLSearchParams({ routeName: routeName })
    })
        .then(response => {
            if (!response.ok) throw new Error("An error has occurred.");
            return response.text();
        })
        .then(html => {
            let existingModal = document.getElementById("routeCardModal");
            if (existingModal) existingModal.remove();

            const modalHtml = `
        <div class="modal fade" id="routeCardModal" tabindex="-1" aria-labelledby="routeCardModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="routeCardModalLabel">Vehicle Card - ${routeName}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" id="routeCardModalCancelBtn"></button>
                    </div>
                    <div class="modal-body">
                        ${html}
                    </div>
                </div>
            </div>
        </div>`;

            document.body.insertAdjacentHTML('beforeend', modalHtml);

            const modal = new bootstrap.Modal(document.getElementById("routeCardModal"));
            modal.show();

            document.getElementById("routeCardModalCancelBtn").addEventListener('click', () => {
                modal.hide();
            });

            document.getElementById("routeCardModal").addEventListener('hidden.bs.modal', function () {
                this.remove();
            });
        })
        .catch(error => {
            console.error("Could not load route card:", error);
            alert("Could not load route card.");
        });
}

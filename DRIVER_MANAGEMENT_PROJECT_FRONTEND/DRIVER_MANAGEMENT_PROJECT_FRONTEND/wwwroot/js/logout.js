document.addEventListener("DOMContentLoaded", function () {
    var confirmLogoutBtn = document.getElementById("confirmLogoutBtn");
    if (!confirmLogoutBtn) return;

    confirmLogoutBtn.addEventListener("click", function () {
        var tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        var token = tokenInput ? tokenInput.value : "";

        fetch('/Identity/Account/Logout', {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: `__RequestVerificationToken=${encodeURIComponent(token)}`
        }).then(response => {
            if (response.redirected) {
                window.location.href = response.url;
            } else {
                alert("Logout failed.");
            }
        }).catch(() => {
            alert("Could not connect to server.");
        });
    });
});

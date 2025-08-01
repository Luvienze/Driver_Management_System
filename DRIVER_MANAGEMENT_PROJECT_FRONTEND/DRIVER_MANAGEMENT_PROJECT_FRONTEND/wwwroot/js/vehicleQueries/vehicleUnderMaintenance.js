async function fetchUnderMaintenanceVehicles() {
    try {
        const response = await fetch('/Vehicle/GetVehicleByStatus?status=4', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            throw new Error(`API error: ${response.status}`);
        }
        const count = await response.json();
        document.getElementById('totalUnderMaintenanceBox').textContent = count;
    } catch (error) {
        console.error('Could not fetch vehicles on under maintenance count:', error);
        document.getElementById('totalUnderMaintenanceBox').textContent = 'Error';
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchUnderMaintenanceVehicles();
});
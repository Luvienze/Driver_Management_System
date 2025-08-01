async function fetchOutOfServiceVehicles() {
    try {
        fetch('/Vehicle/GetVehicleByStatus', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: '3'
        });
        if (!response.ok) {
            throw new Error(`API error: ${response.status}`);
        }
        const count = await response.json();
        document.getElementById('totalOutOfServiceBox').textContent = count;
    } catch (error) {
        console.error('Could not fetch vehicles on out of service count:', error);
        document.getElementById('totalOutOfServiceBox').textContent = 'Error';
    }
}


document.addEventListener("DOMContentLoaded", () => {
    fetchOutOfServiceVehicles();
});
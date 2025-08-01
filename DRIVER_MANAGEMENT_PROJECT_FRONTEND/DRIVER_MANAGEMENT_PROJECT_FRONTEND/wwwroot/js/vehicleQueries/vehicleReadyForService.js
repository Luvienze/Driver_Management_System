async function fetchReadyForServiceVehicles() {
    try {
        fetch('/Vehicle/GetVehicleByStatus', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: '0'
        });
        if (!response.ok) {
            throw new Error(`API error: ${response.status}`);
        }
        const count = await response.json();
        document.getElementById('totalReadyForServiceBox').textContent = count;
    } catch (error) {
        console.error('Could not fetch vehicles on ready for service count:', error);
        document.getElementById('totalReadyForServiceBox').textContent = 'Error';
    }
}


document.addEventListener("DOMContentLoaded", () => {
    fetchReadyForServiceVehicles();
});
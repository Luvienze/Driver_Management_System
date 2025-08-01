async function fetchMalfunctionVehicles() {
    try {
        const response = await fetch('/Vehicle/GetVehicleByStatus', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(1)
        });

        if (!response.ok) {
            throw new Error(`API error: ${response.status} - ${response.statusText}`);
        }

        const count = await response.json();
        document.getElementById('totalMalfunctionBox').textContent = count;
    } catch (error) {
        console.error('Could not fetch malfuntcioned vehicle count:', error);
        document.getElementById('totalMalfunctionBox').textContent = 'Hata';
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchMalfunctionVehicles();
});
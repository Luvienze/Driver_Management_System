async function fetchDamagedVehicles() {
    try {
        const response = await fetch('/Vehicle/GetVehicleByStatus', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(2) // Hasarlı araçlar için status 2
        });

        if (!response.ok) {
            throw new Error(`API hatası: ${response.status} - ${response.statusText}`);
        }

        const count = await response.json();
        document.getElementById('totalDamagedBox').textContent = count;
    } catch (error) {
        console.error('Hasarlı araç sayısı alınamadı:', error);
        document.getElementById('totalDamagedBox').textContent = 'Hata';
    }
}

document.addEventListener("DOMContentLoaded", () => {
    fetchDamagedVehicles();
});